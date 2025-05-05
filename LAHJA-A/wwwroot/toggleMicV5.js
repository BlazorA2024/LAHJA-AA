
        document.addEventListener('DOMContentLoaded', () => {
        // Audio context and nodes
        let audioContext;
    let analyser;
    let microphone;
    let isActive = false;
    let lastTime = 0;
    let frames = 0;
    let lastFpsUpdate = 0;
    let fftSize = 256;
    let barCount = 32;

    // DOM elements
    const toggleMicBtn = document.getElementById('toggleMic');
    const statusElement = document.getElementById('status');
    const inputLevelElement = document.getElementById('inputLevel');
    const levelMeterElement = document.getElementById('levelMeter');
    const visualizerCanvas = document.getElementById('visualizer');
    const visualizerCtx = visualizerCanvas.getContext('2d');
    const barVisualizer = document.getElementById('barVisualizer');
    const dominantFreqElement = document.getElementById('dominantFreq');
    const fftSizeSelect = document.getElementById('fftSize');
    const frequencyRangeSelect = document.getElementById('frequencyRange');
    const visualStyleSelect = document.getElementById('visualStyle');
    const fpsCounter = document.getElementById('fpsCounter');

    // Frequency data arrays
    let frequencyData;
    let timeDomainData;

    // Initialize bars
    const bars = [];
    for (let i = 0; i < barCount; i++) {
                const bar = document.createElement('div');
    bar.className = 'frequency-bar';
    bar.style.height = '0px';
    barVisualizer.appendChild(bar);
    bars.push(bar);
            }

    // Resize canvas
    function resizeCanvas() {
        visualizerCanvas.width = visualizerCanvas.offsetWidth;
    visualizerCanvas.height = visualizerCanvas.offsetHeight;
            }

    window.addEventListener('resize', resizeCanvas);
    resizeCanvas();

    // FPS counter
    function updateFps() {
                const now = performance.now();
    frames++;

                if (now >= lastFpsUpdate + 1000) {
        fpsCounter.textContent = `FPS: ${Math.round(frames * 1000 / (now - lastFpsUpdate))}`;
    frames = 0;
    lastFpsUpdate = now;
                }
            }

    // Visualizer functions
    function drawBars() {
                const width = visualizerCanvas.width;
    const height = visualizerCanvas.height;
    const barWidth = width / barCount;
    let maxFreq = 0;

    visualizerCtx.clearRect(0, 0, width, height);

    for (let i = 0; i < barCount; i++) {
                    const value = frequencyData[i] || 0;
    const percent = value / 255;
    const barHeight = percent * height;
    maxFreq = Math.max(maxFreq, value);

    // Set color based on frequency band
    const hue = 240 - (i / barCount * 240);
    visualizerCtx.fillStyle = `hsl(${hue}, 100%, 60%)`;

    // Draw bar
    visualizerCtx.fillRect(
    i * barWidth,
    height - barHeight,
    barWidth * 0.8,
    barHeight
    );
                }

    // Update the bars in the secondary visualizer
    for (let i = 0; i < barCount; i++) {
                    const value = frequencyData[i] || 0;
    const percent = value / 255;
    bars[i].style.height = `${percent * 100}%`;
                }

    // Find dominant frequency
    let maxIndex = 0;
    let maxValue = 0;
    for (let i = 0; i < frequencyData.length; i++) {
                    if (frequencyData[i] > maxValue) {
        maxValue = frequencyData[i];
    maxIndex = i;
                    }
                }

    // Calculate dominant frequency in Hz
    const sampleRate = audioContext.sampleRate;
    const dominantFrequency = maxIndex * sampleRate / fftSize;

                if (dominantFrequency > 50 && dominantFrequency < 4000) {
        dominantFreqElement.textContent = Math.round(dominantFrequency);
                }
            }

    function drawWaveform() {
                const width = visualizerCanvas.width;
    const height = visualizerCanvas.height;
    const centerY = height / 2;

    visualizerCtx.clearRect(0, 0, width, height);

    visualizerCtx.beginPath();
    visualizerCtx.strokeStyle = '#3b82f6';
    visualizerCtx.lineWidth = 2;

    for (let i = 0; i < timeDomainData.length; i++) {
                    const x = i / timeDomainData.length * width;
    const y = (timeDomainData[i] / 255) * height;

    if (i === 0) {
        visualizerCtx.moveTo(x, y);
                    } else {
        visualizerCtx.lineTo(x, y);
                    }
                }

    visualizerCtx.stroke();
            }

    function drawCircle() {
                const width = visualizerCanvas.width;
    const height = visualizerCanvas.height;
    const centerX = width / 2;
    const centerY = height / 2;
    const radius = Math.min(width, height) * 0.4;

    visualizerCtx.clearRect(0, 0, width, height);

    // Draw frequency data as a circular spectrum
    visualizerCtx.beginPath();

    for (let i = 0; i < frequencyData.length; i++) {
                    const angle = (i / frequencyData.length) * Math.PI * 2;
    const value = frequencyData[i] / 255;
    const pointRadius = radius * (1 + value * 0.5);

    const x = centerX + Math.cos(angle) * pointRadius;
    const y = centerY + Math.sin(angle) * pointRadius;

    if (i === 0) {
        visualizerCtx.moveTo(x, y);
                    } else {
        visualizerCtx.lineTo(x, y);
                    }
                }

    visualizerCtx.closePath();
    visualizerCtx.fillStyle = 'rgba(59, 130, 246, 0.5)';
    visualizerCtx.fill();
    visualizerCtx.strokeStyle = '#3b82f6';
    visualizerCtx.stroke();

    // Draw center circle
    visualizerCtx.beginPath();
    visualizerCtx.arc(centerX, centerY, radius * 0.1, 0, Math.PI * 2);
    visualizerCtx.fillStyle = 'rgba(59, 130, 246, 0.7)';
    visualizerCtx.fill();
            }

    // Main visualization loop
    function visualize() {
                if (!isActive) return;

    requestAnimationFrame(visualize);

    analyser.getByteFrequencyData(frequencyData);
    analyser.getByteTimeDomainData(timeDomainData);

    // Calculate input level
    let sum = 0;
    for (let i = 0; i < timeDomainData.length; i++) {
                    const value = (timeDomainData[i] - 128) / 128;
    sum += value * value;
                }
    const rms = Math.sqrt(sum / timeDomainData.length);
    const level = Math.min(1, rms * 2);

    levelMeterElement.style.width = `${level * 100}%`;
    inputLevelElement.textContent = `${Math.round(level * 100)}%`;

                // Update level meter color
                levelMeterElement.className = level > 0.9 ?
    'h-2.5 bg-red-500 rounded-full' :
                    level > 0.7 ?
    'h-2.5 bg-yellow-500 rounded-full' :
    'h-2.5 bg-green-500 rounded-full';

    // Draw based on selected visual style
    const style = visualStyleSelect.value;
    if (style === 'wave') {
        drawWaveform();
                } else if (style === 'circle') {
        drawCircle();
                } else {
        drawBars();
                }

    updateFps();
            }

    // Toggle microphone
    async function toggleMicrophone() {
                try {
                    if (!isActive) {
        // Start analysis
        audioContext = new (window.AudioContext || window.webkitAudioContext)();
    analyser = audioContext.createAnalyser();
    analyser.fftSize = fftSize;

    frequencyData = new Uint8Array(analyser.frequencyBinCount);
    timeDomainData = new Uint8Array(analyser.frequencyBinCount);

    const stream = await navigator.mediaDevices.getUserMedia({audio: true });
    microphone = audioContext.createMediaStreamSource(stream);
    microphone.connect(analyser);

    isActive = true;
    toggleMicBtn.innerHTML = '<i class="fas fa-stop mr-2"></i> Stop Analyzer';
    toggleMicBtn.className = 'w-full py-3 px-4 bg-red-600 hover:bg-red-700 rounded-lg font-medium mb-4 flex items-center justify-center';

    statusElement.innerHTML = '<span class="h-3 w-3 rounded-full bg-green-500 mr-2 pulse"></span><span class="text-sm">Active</span>';

    visualize();
                    } else {
                        // Stop analysis
                        if (microphone) {
        microphone.disconnect();
                        }
    if (audioContext) {
        audioContext.close();
                        }

    isActive = false;
    toggleMicBtn.innerHTML = '<i class="fas fa-microphone mr-2"></i> Start Analyzer';
    toggleMicBtn.className = 'w-full py-3 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg font-medium mb-4 flex items-center justify-center';

    statusElement.innerHTML = '<span class="h-3 w-3 rounded-full bg-red-500 mr-2"></span><span class="text-sm">Inactive</span>';

    // Clear visualizer
    visualizerCtx.clearRect(0, 0, visualizerCanvas.width, visualizerCanvas.height);
                        bars.forEach(bar => bar.style.height = '0px');

    // Reset indicators
    inputLevelElement.textContent = '0%';
    levelMeterElement.style.width = '0%';
    dominantFreqElement.textContent = '0';
    fpsCounter.textContent = 'FPS: 0';
                    }
                } catch (error) {
        console.error('Error accessing microphone:', error);
    alert('Could not access microphone. Please ensure you have granted microphone permissions.');

    isActive = false;
    toggleMicBtn.innerHTML = '<i class="fas fa-microphone mr-2"></i> Start Analyzer';
    toggleMicBtn.className = 'w-full py-3 px-4 bg-blue-600 hover:bg-blue-700 rounded-lg font-medium mb-4 flex items-center justify-center';

    statusElement.innerHTML = '<span class="h-3 w-3 rounded-full bg-red-500 mr-2"></span><span class="text-sm">Error</span>';
                }
            }

    // Event listeners
    toggleMicBtn.addEventListener('click', toggleMicrophone);

            fftSizeSelect.addEventListener('change', () => {
        fftSize = parseInt(fftSizeSelect.value);
    if (analyser) {
        analyser.fftSize = fftSize;
    frequencyData = new Uint8Array(analyser.frequencyBinCount);
    timeDomainData = new Uint8Array(analyser.frequencyBinCount);
                }
            });

            // Inform users about microphone access
            toggleMicBtn.addEventListener('mouseover', () => {
                if (!isActive) {
        toggleMicBtn.title = 'Click to start. You will be asked for microphone permission.';
                }
            });
        });

//navigator.mediaDevices.getUserMedia({ audio: true })
//    .then(function (stream) {
//        console.log("Microphone access granted.");
//        // «” Œœ«„ «·„Ìﬂ—Ê›Ê‰ Â‰«
//    })
//    .catch(function (err) {
//        alert("Could not access microphone. Please ensure you have granted microphone permissions.");
//        console.error(err);
//    });
