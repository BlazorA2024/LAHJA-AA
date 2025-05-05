
        // Neural Network Implementation
    class NeuralNetwork {
        constructor(inputSize, hiddenSize, outputSize) {
        this.inputSize = inputSize;
    this.hiddenSize = hiddenSize;
    this.outputSize = outputSize;

                // Initialize weights and biases
                const xavierInit = (size) => Math.sqrt(1.0 / size[0]);

                // Input to hidden layer
                this.weights1 = Array(hiddenSize).fill().map(() =>
                    Array(inputSize).fill().map(() => xavierInit([inputSize, hiddenSize]) * (Math.random() * 2 - 1))
    );
    this.bias1 = Array(hiddenSize).fill(0.1);

                // Hidden to output layer
                this.weights2 = Array(outputSize).fill().map(() =>
                    Array(hiddenSize).fill().map(() => xavierInit([hiddenSize, outputSize]) * (Math.random() * 2 - 1))
    );
    this.bias2 = Array(outputSize).fill(0.1);

    this.learningRate = 0.01;
            }

    // Sigmoid activation function
    sigmoid(x) {
                return 1 / (1 + Math.exp(-x));
            }

    // Derivative of sigmoid
    sigmoidDerivative(x) {
                const s = this.sigmoid(x);
    return s * (1 - s);
            }

    // Forward propagation
    forward(input) {
                // Input to hidden
                const hiddenInput = Array(this.hiddenSize).fill(0);
    for (let i = 0; i < this.hiddenSize; i++) {
                    for (let j = 0; j < this.inputSize; j++) {
        hiddenInput[i] += this.weights1[i][j] * input[j];
                    }
    hiddenInput[i] += this.bias1[i];
    hiddenInput[i] = this.sigmoid(hiddenInput[i]);
                }

    // Hidden to output
    const output = Array(this.outputSize).fill(0);
    for (let i = 0; i < this.outputSize; i++) {
                    for (let j = 0; j < this.hiddenSize; j++) {
        output[i] += this.weights2[i][j] * hiddenInput[j];
                    }
    output[i] += this.bias2[i];
    output[i] = this.sigmoid(output[i]);
                }

    return {
        output,
        hidden: hiddenInput
                };
            }

    // Train the network with one sample
    train(input, target) {
                // Forward pass
                const {output, hidden} = this.forward(input);

    // Backpropagation
    // Output layer error
    const outputErrors = Array(this.outputSize).fill(0);
    const outputDeltas = Array(this.outputSize).fill(0);
    for (let i = 0; i < this.outputSize; i++) {
        outputErrors[i] = target[i] - output[i];
    outputDeltas[i] = outputErrors[i] * this.sigmoidDerivative(output[i]);
                }

    // Hidden layer error
    const hiddenErrors = Array(this.hiddenSize).fill(0);
    const hiddenDeltas = Array(this.hiddenSize).fill(0);
    for (let i = 0; i < this.hiddenSize; i++) {
                    for (let j = 0; j < this.outputSize; j++) {
        hiddenErrors[i] += outputDeltas[j] * this.weights2[j][i];
                    }
    hiddenDeltas[i] = hiddenErrors[i] * this.sigmoidDerivative(hidden[i]);
                }

    // Update weights and biases
    for (let i = 0; i < this.outputSize; i++) {
                    for (let j = 0; j < this.hiddenSize; j++) {
        this.weights2[i][j] += this.learningRate * outputDeltas[i] * hidden[j];
                    }
    this.bias2[i] += this.learningRate * outputDeltas[i];
                }

    for (let i = 0; i < this.hiddenSize; i++) {
                    for (let j = 0; j < this.inputSize; j++) {
        this.weights1[i][j] += this.learningRate * hiddenDeltas[i] * input[j];
                    }
    this.bias1[i] += this.learningRate * hiddenDeltas[i];
                }

                // Return error
                return outputErrors.reduce((sum, err) => sum + Math.abs(err), 0) / outputErrors.length;
            }

    // Save model to JSON
    toJSON() {
                return {
        inputSize: this.inputSize,
    hiddenSize: this.hiddenSize,
    outputSize: this.outputSize,
    weights1: this.weights1,
    weights2: this.weights2,
    bias1: this.bias1,
    bias2: this.bias2
                };
            }

    // Load model from JSON
    static fromJSON(json) {
                const net = new NeuralNetwork(json.inputSize, json.hiddenSize, json.outputSize);
    net.weights1 = json.weights1;
    net.weights2 = json.weights2;
    net.bias1 = json.bias1;
    net.bias2 = json.bias2;
    return net;
            }
        }
    // Audio Feature Extractor
    class AudioFeatureExtractor {
        constructor() {
        this.audioContext = new (window.AudioContext || window.webkitAudioContext)();
    this.analyser = this.audioContext.createAnalyser();
    this.analyser.fftSize = 512;
    this.bufferLength = this.analyser.frequencyBinCount;
    this.dataArray = new Uint8Array(this.bufferLength);
    this.sampleRate = this.audioContext.sampleRate;

    // For spectrogram
    this.spectrogramBuffer = [];
    this.maxSpectrogramLength = 30; // Number of frames to keep
            }

    async startRecording(stream, onAudioProcess) {
        this.audioSource = this.audioContext.createMediaStreamSource(stream);
    this.audioSource.connect(this.analyser);

    // For recording audio data
    this.recorder = new MediaRecorder(stream);
    this.chunks = [];
                this.recorder.ondataavailable = e => this.chunks.push(e.data);
    this.recorder.start();

                // Process audio
                const process = () => {
        this.analyser.getByteFrequencyData(this.dataArray);

    // Add to spectrogram buffer
    this.spectrogramBuffer.push(new Uint8Array(this.dataArray));
                    if (this.spectrogramBuffer.length > this.maxSpectrogramLength) {
        this.spectrogramBuffer.shift();
                    }

    onAudioProcess(this.dataArray);
    this.rafId = requestAnimationFrame(process);
                };

    process();
            }

    stopRecording() {
                if (this.rafId) {
        cancelAnimationFrame(this.rafId);
                }

                return new Promise((resolve) => {
                    if (!this.recorder) {
        resolve(null);
    return;
                    }

                    this.recorder.onstop = async () => {
                        const blob = new Blob(this.chunks, {type: 'audio/wav' });
    const audioBuffer = await this.decodeAudioData(blob);
    resolve(audioBuffer);
                    };

    this.recorder.stop();
    if (this.audioSource) {
        this.audioSource.disconnect();
                    }
                });
            }

    async decodeAudioData(blob) {
                const arrayBuffer = await blob.arrayBuffer();
                return new Promise((resolve, reject) => {
        this.audioContext.decodeAudioData(arrayBuffer, resolve, reject);
                });
            }

    extractMFCC(audioBuffer) {
        // Simplified MFCC feature extraction
        // In a real application, you'd want a full MFCC implementation

        // First get FFT data
        this.analyser.getByteFrequencyData(this.dataArray);

                // Convert to power spectrum
                const powerSpectrum = Array.from(this.dataArray).map(val => val / 255);

    // Simple feature extraction - using mean of bands as approximation
    const bands = 13; // Standard number of MFCC coefficients
    const bandSize = Math.floor(powerSpectrum.length / bands);
    const features = [];

    for (let i = 0; i < bands; i++) {
                    const start = i * bandSize;
    const end = (i + 1) * bandSize;
    const band = powerSpectrum.slice(start, end);
                    const mean = band.reduce((sum, val) => sum + val, 0) / band.length;
    features.push(mean);
                }

                // Add delta features (approximation)
                if (features.length > 1) {
                    for (let i = 1; i < features.length; i++) {
        features.push(features[i] - features[i - 1]);
                    }
                }

    return features;
            }

    getSpectrogramData() {
                return this.spectrogramBuffer;
            }
        }
    // Main Application
    class AudioCommandApp {
        constructor() {
        this.featureExtractor = new AudioFeatureExtractor();
    this.model = null;
    this.commands = [];
    this.trainingData = { };
    this.currentCommand = null;
    this.isRecording = false;
    this.isTraining = false;
    this.isPredicting = false;
    this.minSamples = 5;  // Minimum samples per command needed for training
    this.inputSize = 26;  // Number of MFCC features (13 + 13 deltas)
    this.hiddenSize = 16; // Size of hidden layer

    // DOM elements
    this.commandList = document.getElementById('commandList');
    this.newCommandInput = document.getElementById('newCommandInput');
    this.addCommandBtn = document.getElementById('addCommandBtn');
    this.recordTrainBtn = document.getElementById('recordTrainBtn');
    this.trainBtn = document.getElementById('trainBtn');
    this.testBtn = document.getElementById('testBtn');
    this.recordPredictBtn = document.getElementById('recordPredictBtn');
    this.continuousBtn = document.getElementById('continuousBtn');
    this.currentCommandDisplay = document.getElementById('currentCommand');
    this.sampleCount = document.getElementById('sampleCount');
    this.trainingProgressBar = document.getElementById('trainingProgressBar');
    this.trainingProgressText = document.getElementById('trainingProgressText');
    this.recognizedCommand = document.getElementById('recognizedCommand');
    this.predictionConfidence = document.getElementById('predictionConfidence');
    this.confidenceBar = document.getElementById('confidenceBar');
    this.clearStorageBtn = document.getElementById('clearStorageBtn');

    // Visualization canvases
    this.waveformCanvas = document.getElementById('waveformCanvas');
    this.waveformCtx = this.waveformCanvas.getContext('2d');
    this.spectrogramCanvas = document.getElementById('spectrogramCanvas');
    this.spectrogramCtx = this.spectrogramCanvas.getContext('2d');
    this.networkVisualization = document.getElementById('networkVisualization');

    // Setup UI
    this.setupCanvas();
    this.setupEventListeners();
    this.loadFromStorage();
    this.renderCommandList();
    this.visualizeNetwork();
            }

    setupCanvas() {
                const width = this.audioVisualization.clientWidth;
    const height = this.audioVisualization.clientHeight;

    this.waveformCanvas.width = width;
    this.waveformCanvas.height = height;
    this.spectrogramCanvas.width = width;
    this.spectrogramCanvas.height = height;

    // Initially clear canvases
    this.clearVisualizations();
            }

    setupEventListeners() {
        // Add new command
        this.addCommandBtn.addEventListener('click', () => {
            const command = this.newCommandInput.value.trim().toLowerCase();
            if (command && !this.commands.includes(command)) {
                this.commands.push(command);
                this.trainingData[command] = [];
                this.newCommandInput.value = '';
                this.saveToStorage();
                this.renderCommandList();
            }
        });

                // Record training sample
                this.recordTrainBtn.addEventListener('click', () => {
                    if (this.currentCommand) {
        this.toggleTrainRecording();
                    } else {
        alert('Please select a command to train first');
                    }
                });

                // Train model
                this.trainBtn.addEventListener('click', () => this.trainModel());

                // Test model
                this.testBtn.addEventListener('click', () => this.testModel());

                // Record prediction
                this.recordPredictBtn.addEventListener('click', () => this.togglePredictRecording());

                // Continuous recognition mode
                this.continuousBtn.addEventListener('click', () => this.toggleContinuousMode());

                // Clear storage
                this.clearStorageBtn.addEventListener('click', () => {
                    if (confirm('Clear all training data and commands?')) {
        localStorage.clear();
    this.commands = [];
    this.trainingData = { };
    this.model = null;
    this.currentCommand = null;
    this.saveToStorage();
    this.renderCommandList();
    this.updateTrainingUI();
    this.clearVisualizations();
    this.visualizeNetwork();
                    }
                });

                // Handle window resize
                window.addEventListener('resize', () => {
        this.setupCanvas();
    if (this.isRecording) {
        this.drawVisualizations(this.featureExtractor.getSpectrogramData());
                    }
                });
            }

    async toggleTrainRecording() {
                try {
                    if (this.isRecording) {
        // Stop recording
        this.isRecording = false;
    this.recordTrainBtn.innerHTML = '<i class="fas fa-microphone mr-2"></i> Record Sample';
    this.recordTrainBtn.classList.remove('bg-red-600', 'hover:bg-red-500');
    this.recordTrainBtn.classList.add('gradient-bg');

    const audioBuffer = await this.featureExtractor.stopRecording();
    if (audioBuffer) {
                            const features = this.featureExtractor.extractMFCC(audioBuffer);
    this.trainingData[this.currentCommand].push(features);
    this.saveToStorage();
    this.updateTrainingUI();

    // Show notification
    const notification = document.createElement('div');
    notification.className = 'fixed bottom-4 right-4 bg-green-600 text-white px-4 py-2 rounded-lg shadow-lg transition transform translate-y-10 opacity-0';
    notification.innerHTML = 'Sample recorded successfully';
    document.body.appendChild(notification);

                            setTimeout(() => {
        notification.classList.add('opacity-100', 'translate-y-0');
                                setTimeout(() => {
        notification.classList.remove('opacity-100', 'translate-y-0');
                                    setTimeout(() => notification.remove(), 300);
                                }, 2000);
                            }, 10);
                        }

    this.clearVisualizations();
                    } else {
        // Start recording
        this.isRecording = true;
    this.recordTrainBtn.innerHTML = '<i class="fas fa-stop mr-2"></i> Stop Recording';
    this.recordTrainBtn.classList.add('bg-red-600', 'hover:bg-red-500');
    this.recordTrainBtn.classList.remove('gradient-bg');

    const stream = await navigator.mediaDevices.getUserMedia({audio: true });
                        this.featureExtractor.startRecording(stream, (data) => {
        this.drawVisualizations(this.featureExtractor.getSpectrogramData());
                        });
                    }
                } catch (error) {
        console.error('Recording error:', error);
    this.isRecording = false;
    this.recordTrainBtn.innerHTML = '<i class="fas fa-microphone mr-2"></i> Record Sample';
    this.recordTrainBtn.classList.add('gradient-bg');
    this.recordTrainBtn.classList.remove('bg-red-600', 'hover:bg-red-500');
    alert('Error accessing microphone: ' + error.message);
                }
            }

    async togglePredictRecording() {
                try {
                    if (this.isPredicting) {
        // Stop recording
        this.isPredicting = false;
    this.recordPredictBtn.innerHTML = '<i class="fas fa-microphone mr-2"></i> Record Command';
    this.recordPredictBtn.classList.remove('bg-red-600', 'hover:bg-red-500');
    this.recordPredictBtn.classList.add('gradient-bg', 'pulse-animation');

    await this.featureExtractor.stopRecording();
    this.clearVisualizations();
                    } else {
        // Start recording
        this.isPredicting = true;
    this.recordPredictBtn.innerHTML = '<i class="fas fa-stop mr-2"></i> Stop Recording';
    this.recordPredictBtn.classList.add('bg-red-600', 'hover:bg-red-500');
    this.recordPredictBtn.classList.remove('gradient-bg', 'pulse-animation');

    this.recognizedCommand.textContent = 'Listening...';
    this.predictionConfidence.textContent = '--% confidence';
    this.confidenceBar.style.width = '0%';

    const stream = await navigator.mediaDevices.getUserMedia({audio: true });
                        this.featureExtractor.startRecording(stream, (data) => {
        this.drawVisualizations(this.featureExtractor.getSpectrogramData());

    if (this.model) {
                                const features = this.featureExtractor.extractMFCC();
    this.predictCommand(features);
                            }
                        });
                    }
                } catch (error) {
        console.error('Prediction error:', error);
    this.isPredicting = false;
    this.recordPredictBtn.innerHTML = '<i class="fas fa-microphone mr-2"></i> Record Command';
    this.recordPredictBtn.classList.add('gradient-bg', 'pulse-animation');
    this.recordPredictBtn.classList.remove('bg-red-600', 'hover:bg-red-500');
    alert('Error accessing microphone: ' + error.message);
                }
            }

    toggleContinuousMode() {
        // To be implemented
        alert('Continuous mode coming soon!');
            }

    trainModel() {
                if (this.commands.length < 1) {
        alert('Please add at least one command first');
    return;
                }

                // Check if we have enough samples for each command
                const commandsWithEnoughSamples = this.commands.filter(cmd =>
                    this.trainingData[cmd] && this.trainingData[cmd].length >= this.minSamples
    );

    if (commandsWithEnoughSamples.length < 1) {
        alert(`Please record at least ${this.minSamples} samples for each command you want to train`);
    return;
                }

    this.isTraining = true;
    this.trainBtn.disabled = true;
    this.recordTrainBtn.disabled = true;

    // Prepare training data
    const trainingData = [];
    const targets = [];
    const commandIndex = { };
                commandsWithEnoughSamples.forEach((cmd, idx) => {
        commandIndex[cmd] = idx;
                    this.trainingData[cmd].forEach(features => {
        trainingData.push(features);
    // One-hot encoded target
    const target = Array(commandsWithEnoughSamples.length).fill(0);
    target[idx] = 1;
    targets.push(target);
                    });
                });

    // Initialize or reset model
    if (!this.model) {
        this.model = new NeuralNetwork(this.inputSize, this.hiddenSize, commandsWithEnoughSamples.length);
                }

    // Train the model
    const epochs = 200;
    const batchSize = 16;
    const progressStep = Math.ceil(epochs / 20);

                const train = async (epoch = 0) => {
                    if (epoch >= epochs) {
        // Training complete
        this.isTraining = false;
    this.trainBtn.disabled = false;
    this.recordTrainBtn.disabled = false;

    // Visualize the trained network
    this.visualizeNetwork();

    // Show notification
    const notification = document.createElement('div');
    notification.className = 'fixed bottom-4 right-4 bg-green-600 text-white px-4 py-2 rounded-lg shadow-lg transition transform translate-y-10 opacity-0';
    notification.innerHTML = 'Training complete! Model is ready';
    document.body.appendChild(notification);

                        setTimeout(() => {
        notification.classList.add('opacity-100', 'translate-y-0');
                            setTimeout(() => {
        notification.classList.remove('opacity-100', 'translate-y-0');
                                setTimeout(() => notification.remove(), 300);
                            }, 2000);
                        }, 10);

    return;
                    }

    // Shuffle training data
    const shuffledIndices = Array.from({length: trainingData.length }, (_, i) => i);
                    for (let i = shuffledIndices.length - 1; i > 0; i--) {
                        const j = Math.floor(Math.random() * (i + 1));
    [shuffledIndices[i], shuffledIndices[j]] = [shuffledIndices[j], shuffledIndices[i]];
                    }

    // Train in mini-batches
    let totalError = 0;
    for (let i = 0; i < Math.ceil(trainingData.length / batchSize); i++) {
                        const batchIndices = shuffledIndices.slice(i * batchSize, (i + 1) * batchSize);

    for (const idx of batchIndices) {
                            const error = this.model.train(trainingData[idx], targets[idx]);
    totalError += error;
                        }
                    }

    const avgError = totalError / trainingData.length;

    // Update UI
    if (epoch % progressStep === 0 || epoch === epochs - 1) {
                        const progress = Math.floor((epoch / epochs) * 100);
    this.trainingProgressBar.style.width = `${progress}%`;
    this.trainingProgressText.textContent = `Epoch ${epoch + 1}/${epochs} (Error: ${avgError.toFixed(4)})`;

    // Visualize network occasionally
    if (epoch % (progressStep * 2) === 0) {
        this.visualizeNetwork();
                        }
                    }

                    // Schedule next epoch
                    await new Promise(resolve => setTimeout(resolve, 0));
                    requestAnimationFrame(() => train(epoch + 1));
                };

    // Start training
    train();
            }

    testModel() {
                if (!this.model || this.commands.length < 1) {
        alert('Please train at least one command first');
    return;
                }

    // Simple test of the model with training data
    const summary = { };
    let totalCorrect = 0;
    let totalSamples = 0;

                this.commands.forEach(cmd => {
                    if (!this.trainingData[cmd] || this.trainingData[cmd].length === 0) return;

    summary[cmd] = {correct: 0, total: this.trainingData[cmd].length };
    totalSamples += this.trainingData[cmd].length;

                    this.trainingData[cmd].forEach(features => {
                        const prediction = this.model.forward(features).output;
    const predictedIndex = prediction.indexOf(Math.max(...prediction));
    const actualIndex = this.commands.indexOf(cmd);

    if (predictedIndex === actualIndex) {
        summary[cmd].correct++;
    totalCorrect++;
                        }
                    });
                });

    // Display test results
    let resultText = 'Model Test Results\n\n';
                this.commands.forEach(cmd => {
                    if (!summary[cmd]) return;
    const accuracy = Math.round((summary[cmd].correct / summary[cmd].total) * 100);
    resultText += `${cmd}: ${summary[cmd].correct}/${summary[cmd].total} (${accuracy}%)\n`;
                });

    resultText += `\nOverall Accuracy: ${Math.round((totalCorrect / totalSamples) * 100)}%`;
    alert(resultText);
            }

    predictCommand(features) {
                if (!this.model || this.commands.length < 1) return;

    const {output, hidden} = this.model.forward(features);
    const maxConfidence = Math.max(...output);
    const predictedIndex = output.indexOf(maxConfidence);
    const confidence = Math.round(maxConfidence * 100);

                if (confidence > 30) { // Minimum confidence threshold
                    const predictedCommand = this.commands[predictedIndex];
    this.recognizedCommand.textContent = predictedCommand;
    this.predictionConfidence.textContent = `${confidence}% confidence`;
    this.confidenceBar.style.width = `${confidence}%`;

    // Visualize network activation
    this.visualizeNetwork(hidden, predictedIndex, confidence);
                } else {
        this.recognizedCommand.textContent = 'Not recognized';
    this.predictionConfidence.textContent = 'Low confidence';
    this.confidenceBar.style.width = '0%';
                }
            }

    // Visualization methods
    drawVisualizations(spectrogramBuffer) {
                if (!spectrogramBuffer || spectrogramBuffer.length === 0) return;

    const width = this.waveformCanvas.width;
    const height = this.waveformCanvas.height;

    // Clear canvases
    this.waveformCtx.clearRect(0, 0, width, height);
    this.spectrogramCtx.clearRect(0, 0, width, height);

    // Draw waveform (simplified)
    this.waveformCtx.beginPath();
    this.waveformCtx.strokeStyle = '#a777e3';
    this.waveformCtx.lineWidth = 2;

    const currentData = spectrogramBuffer[spectrogramBuffer.length - 1];
    const sliceWidth = width / currentData.length;

    for (let i = 0; i < currentData.length; i++) {
                    const v = currentData[i] / 255.0;
    const y = (1 - v) * height;

    if (i === 0) {
        this.waveformCtx.moveTo(0, y);
                    } else {
        this.waveformCtx.lineTo(i * sliceWidth, y);
                    }
                }

    this.waveformCtx.stroke();

    // Draw spectrogram
    const spectrogramHeight = height;
    const spectrogramWidth = width;
    const binHeight = spectrogramHeight / currentData.length;

    for (let i = 0; i < spectrogramBuffer.length; i++) {
                    const colData = spectrogramBuffer[i];
    const x = spectrogramWidth - (spectrogramBuffer.length - i);

    for (let j = 0; j < colData.length; j++) {
                        const value = colData[j] / 255;
    const h = 240; // Hue (blue)
    const s = 100; // Saturation
    const l = value * 100; // Lightness

    this.spectrogramCtx.fillStyle = `hsl(${h}, ${s}%, ${l}%)`;
    this.spectrogramCtx.fillRect(x, j * binHeight, 1, binHeight);
                    }
                }
            }

    clearVisualizations() {
        this.waveformCtx.clearRect(0, 0, this.waveformCanvas.width, this.waveformCanvas.height);
    this.spectrogramCtx.clearRect(0, 0, this.spectrogramCanvas.width, this.spectrogramCanvas.height);

    // Draw empty state
    this.waveformCtx.fillStyle = 'rgba(255, 255, 255, 0.05)';
    this.waveformCtx.fillRect(0, 0, this.waveformCanvas.width, this.waveformCanvas.height);
    this.spectrogramCtx.fillStyle = 'rgba(255, 255, 255, 0.05)';
    this.spectrogramCtx.fillRect(0, 0, this.spectrogramCanvas.width, this.spectrogramCanvas.height);

    this.waveformCtx.fillStyle = 'white';
    this.waveformCtx.font = '14px Arial';
    this.waveformCtx.textAlign = 'center';
    this.waveformCtx.fillText('No audio data', this.waveformCanvas.width / 2, this.waveformCanvas.height / 2);
            }

    visualizeNetwork(hiddenActivations = null, outputIndex = -1, confidence = 0) {
        // Clear network visualization
        this.networkVisualization.innerHTML = '';

    if (!this.model) {
                    // Show placeholder if no model exists
                    const placeholder = document.createElement('div');
    placeholder.className = 'text-gray-400 text-center py-12';
    placeholder.textContent = 'No trained model. Train with at least 5 samples per command.';
    this.networkVisualization.appendChild(placeholder);
    return;
                }

    // Create layers container
    const layersContainer = document.createElement('div');
    layersContainer.className = 'flex items-center justify-center h-full';
    this.networkVisualization.appendChild(layersContainer);

    // Input layer
    const inputLayer = document.createElement('div');
    inputLayer.className = 'flex flex-col items-center mx-2';
    const inputLabel = document.createElement('div');
    inputLabel.className = 'text-xs text-gray-400 mb-1';
    inputLabel.textContent = 'Input Features';
    inputLayer.appendChild(inputLabel);

    const inputNeurons = document.createElement('div');
    inputNeurons.className = 'flex flex-col items-center';
    for (let i = 0; i < this.model.inputSize; i++) {
                    const neuron = document.createElement('div');
    neuron.className = 'neuron';
    inputNeurons.appendChild(neuron);
                }
    inputLayer.appendChild(inputNeurons);
    layersContainer.appendChild(inputLayer);

    // Connections between input and hidden
    for (let i = 0; i < this.model.inputSize; i++) {
                    for (let j = 0; j < this.model.hiddenSize; j++) {
                        const connection = document.createElement('div');
    connection.className = 'connection';
    connection.style.width = '60px';
    connection.style.left = (30 + i * 0) + 'px';  // Adjusted for display
    connection.style.top = (20 + i * 10) + 'px';   // Simplified positioning
    layersContainer.appendChild(connection);
                    }
                }

    // Hidden layer
    const hiddenLayer = document.createElement('div');
    hiddenLayer.className = 'flex flex-col items-center mx-2';
    const hiddenLabel = document.createElement('div');
    hiddenLabel.className = 'text-xs text-gray-400 mb-1';
    hiddenLabel.textContent = 'Hidden Layer';
    hiddenLayer.appendChild(hiddenLabel);

    const hiddenNeurons = document.createElement('div');
    hiddenNeurons.className = 'flex flex-col items-center';
    for (let i = 0; i < this.model.hiddenSize; i++) {
                    const neuron = document.createElement('div');
    neuron.className = 'neuron';
    if (hiddenActivations) {
                        const activation = hiddenActivations[i];
    const intensity = Math.min(255, Math.floor(activation * 200));
    neuron.style.backgroundColor = `rgba(167, 119, 227, ${activation})`;
                        if (activation > 0.6) neuron.classList.add('active');
                    }
    hiddenNeurons.appendChild(neuron);
                }
    hiddenLayer.appendChild(hiddenNeurons);
    layersContainer.appendChild(hiddenLayer);

    // Connections between hidden and output
    for (let i = 0; i < this.model.hiddenSize; i++) {
                    for (let j = 0; j < this.model.outputSize; j++) {
                        const connection = document.createElement('div');
    connection.className = 'connection';
    connection.style.width = '60px';
    layersContainer.appendChild(connection);
                    }
                }

    // Output layer
    const outputLayer = document.createElement('div');
    outputLayer.className = 'flex flex-col items-center mx-2';
    const outputLabel = document.createElement('div');
    outputLabel.className = 'text-xs text-gray-400 mb-1';
    outputLabel.textContent = 'Output';
    outputLayer.appendChild(outputLabel);

    const outputNeurons = document.createElement('div');
    outputNeurons.className = 'flex flex-col items-center';
    for (let i = 0; i < this.model.outputSize; i++) {
                    const neuron = document.createElement('div');
    neuron.className = 'neuron';

                    if (outputIndex >= 0) {
                        if (i === outputIndex) {
        neuron.style.backgroundColor = `rgba(74, 222, 128, ${confidence / 100})`;
                            if (confidence > 50) neuron.classList.add('active');
                        } else {
        neuron.style.opacity = '0.3';
                        }
                    }

    outputNeurons.appendChild(neuron);

    // Add command labels
    if (this.commands[i]) {
                        const label = document.createElement('div');
    label.className = 'text-xs text-center mt-1';
    label.textContent = this.commands[i];
    outputNeurons.appendChild(label);
                    }
                }
    outputLayer.appendChild(outputNeurons);
    layersContainer.appendChild(outputLayer);
            }

    // Command list rendering
    renderCommandList() {
        this.commandList.innerHTML = '';

                this.commands.forEach(cmd => {
                    const samples = this.trainingData[cmd] ? this.trainingData[cmd].length : 0;
                    const statusColor = samples >= this.minSamples ? 'bg-green-500' :
                        samples > 0 ? 'bg-yellow-500' : 'bg-red-500';
                    const statusText = samples >= this.minSamples ? 'Ready' :
                        samples > 0 ? `${samples}/${this.minSamples}` : 'New';

    const card = document.createElement('div');
    card.className = `command-card bg-gray-700 rounded-lg p-4 cursor-pointer ${this.currentCommand === cmd ? 'glow' : ''}`;
    card.innerHTML = `
    <div class="flex justify-between items-center">
        <h3 class="font-medium">${cmd}</h3>
        <span class="text-xs ${statusColor} px-2 py-1 rounded-full">${statusText}</span>
    </div>
    <div class="waveform mt-2 rounded"></div>
    <div class="confidence-meter mt-2">
        <div class="confidence-fill" style="width: ${samples / this.minSamples * 100}%"></div>
    </div>
    <div class="text-xs text-gray-400 mt-1">${samples} samples</div>
    `;

                    card.addEventListener('click', () => {
        this.currentCommand = cmd;
    this.currentCommandDisplay.textContent = `"${cmd}"`;
    this.updateTrainingUI();

                        // Highlight selected card
                        document.querySelectorAll('.command-card').forEach(c => c.classList.remove('glow'));
    card.classList.add('glow');
                    });

    this.commandList.appendChild(card);
                });

    if (this.commands.length === 0) {
        this.commandList.innerHTML = '<div class="text-center py-8 text-gray-400">No commands added yet</div>';
                }
            }

    updateTrainingUI() {
                if (!this.currentCommand) {
        this.sampleCount.textContent = '0';
    return;
                }

    const samples = this.trainingData[this.currentCommand] ? this.trainingData[this.currentCommand].length : 0;
    this.sampleCount.textContent = samples;

                // Update training button state
                this.trainBtn.disabled = this.commands.every(cmd =>
    !this.trainingData[cmd] || this.trainingData[cmd].length < this.minSamples
                );
            }

    // Storage methods
    saveToStorage() {
                try {
        localStorage.setItem('audioCommands', JSON.stringify(this.commands));
    localStorage.setItem('trainingData', JSON.stringify(this.trainingData));

    if (this.model) {
        localStorage.setItem('nnModel', JSON.stringify(this.model.toJSON()));
                    }
                } catch (e) {
        console.error('Failed to save data:', e);
                }
            }

    loadFromStorage() {
                try {
                    const commands = localStorage.getItem('audioCommands');
    const trainingData = localStorage.getItem('trainingData');
    const modelData = localStorage.getItem('nnModel');

    if (commands) this.commands = JSON.parse(commands);
    if (trainingData) this.trainingData = JSON.parse(trainingData);
    if (modelData) this.model = NeuralNetwork.fromJSON(JSON.parse(modelData));
                } catch (e) {
        console.error('Failed to load data:', e);
                }
            }
        }
        // Initialize the app when DOM is loaded
        document.addEventListener('DOMContentLoaded', () => {
            if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia) {
        alert('Your browser doesn\'t support audio recording. Please try Chrome or Firefox.');
    return;
            }

    const app = new AudioCommandApp();
    window.app = app; // For debugging
        });

