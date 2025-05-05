document.addEventListener('DOMContentLoaded', () => {
    // Audio context variables
    let audioContext;
    let analyser;
    let microphone;
    let dataArray;
    let isAnalyzing = false;
    let animationId;

    // DOM elements
    const toggleMicBtn = document.getElementById('toggleMic');
    const statusElement = document.getElementById('status');
    const canvas = document.getElementById('canvas');
    const ctx = canvas.getContext('2d');
    const fftSizeSelect = document.getElementById('fftSize');
    const smoothingInput = document.getElementById('smoothing');
    const volumeMeter = document.getElementById('volumeMeter');
    const dominantFreqElement = document.getElementById('dominantFreq');
    const bassLevelElement = document.getElementById('bassLevel');
    const midLevelElement = document.getElementById('midLevel');
    const trebleLevelElement = document.getElementById('trebleLevel');
    const frequencyLabels = document.getElementById('frequencyLabels');

    // Initialize visualizer
    function initVisualizer() {
        // Set canvas to full size of its container
        const container = document.getElementById('visualizer');
        canvas.width = container.clientWidth;
        canvas.height = container.clientHeight;

        // Set visual properties
        ctx.fillStyle = 'rgb(0, 0, 0)';
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        // Add frequency labels
        addFrequencyLabels();
    }

    // Add frequency labels to the visualizer
    function addFrequencyLabels() {
        // Clear existing labels
        frequencyLabels.innerHTML = '';

        const frequencies = [20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000];
        const height = frequencyLabels.clientHeight;

        frequencies.forEach(freq => {
            // Convert frequency to position (logarithmic scale)
            const minFreq = 20;
            const maxFreq = 20000;
            const minPosition = 0;
            const maxPosition = height;

            // Logarithmic scaling
            const position = height - (
                (Math.log10(freq) - Math.log10(minFreq)) /
                (Math.log10(maxFreq) - Math.log10(minFreq)) * maxPosition
            );

            // Create line
            const line = document.createElement('div');
            line.className = 'frequency-line';
            line.style.top = `${position}px`;
            frequencyLabels.appendChild(line);

            // Create label
            const label = document.createElement('div');
            label.className = 'frequency-label pr-1';
            label.style.top = `${position}px`;
            label.textContent = freq >= 1000 ? `${Math.round(freq / 1000)}k` : freq;
            frequencyLabels.appendChild(label);
        });
    }

    // Start audio analysis
    async function startAnalysis() {
        try {
            // Initialize audio context
            audioContext = new (window.AudioContext || window.webkitAudioContext)();
            analyser = audioContext.createAnalyser();

            // Set FFT size
            analyser.fftSize = parseInt(fftSizeSelect.value);
            const bufferLength = analyser.frequencyBinCount;
            dataArray = new Uint8Array(bufferLength);

            // Get microphone input
            const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
            microphone = audioContext.createMediaStreamSource(stream);
            microphone.connect(analyser);

            // Update status
            statusElement.textContent = 'Analyzing audio...';
            toggleMicBtn.innerHTML = '<i class="fas fa-microphone-slash mr-2"></i><span>Stop Analysis</span>';
            toggleMicBtn.classList.remove('bg-blue-600', 'hover:bg-blue-700');
            toggleMicBtn.classList.add('bg-red-600', 'hover:bg-red-700');
            isAnalyzing = true;

            // Start visualization
            draw();
        } catch (error) {
            console.error('Error accessing microphone:', error);
            statusElement.textContent = 'Error: ' + error.message;
        }
    }

    // Stop audio analysis
    function stopAnalysis() {
        if (microphone && audioContext) {
            microphone.disconnect();
            audioContext.close();
        }

        if (animationId) {
            cancelAnimationFrame(animationId);
        }

        // Clear canvas
        ctx.fillStyle = 'rgb(0, 0, 0)';
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        // Update status
        statusElement.textContent = 'Analysis stopped';
        toggleMicBtn.innerHTML = '<i class="fas fa-microphone mr-2"></i><span>Start Analysis</span>';
        toggleMicBtn.classList.remove('bg-red-600', 'hover:bg-red-700');
        toggleMicBtn.classList.add('bg-blue-600', 'hover:bg-blue-700');
        isAnalyzing = false;

        // Reset indicators
        volumeMeter.innerHTML = '<i class="fas fa-volume-up mr-1"></i><span>0.00</span>';
        dominantFreqElement.textContent = '0 Hz';
        bassLevelElement.textContent = '0.00';
        midLevelElement.textContent = '0.00';
        trebleLevelElement.textContent = '0.00';
    }

    // Draw the frequency visualization
    function draw() {
        animationId = requestAnimationFrame(draw);

        analyser.smoothingTimeConstant = parseFloat(smoothingInput.value);
        analyser.getByteFrequencyData(dataArray);

        // Clear canvas
        ctx.fillStyle = 'rgba(0, 0, 0, 0.1)';
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        const barWidth = canvas.width / dataArray.length;
        let volumeSum = 0;
        let maxValue = 0;
        let maxIndex = 0;

        // Calculate frequency ranges
        let bassSum = 0;
        let midSum = 0;
        let trebleSum = 0;
        const bassEnd = Math.floor(250 / (audioContext.sampleRate / 2) * dataArray.length);
        const midEnd = Math.floor(4000 / (audioContext.sampleRate / 2) * dataArray.length);

        for (let i = 0; i < dataArray.length; i++) {
            const value = dataArray[i] / 255;
            volumeSum += value;

            // Track maximum frequency
            if (value > maxValue) {
                maxValue = value;
                maxIndex = i;
            }

            // Accumulate frequency ranges
            if (i < bassEnd) {
                bassSum += value;
            } else if (i < midEnd) {
                midSum += value;
            } else {
                trebleSum += value;
            }

            // Draw frequency bar
            const barHeight = value * canvas.height;
            const x = i * barWidth;

            // Create gradient for the bar
            const gradient = ctx.createLinearGradient(x, canvas.height - barHeight, x, canvas.height);
            gradient.addColorStop(0, 'rgba(59, 130, 246, 0.8)');
            gradient.addColorStop(1, 'rgba(139, 92, 246, 0.8)');

            ctx.fillStyle = gradient;
            ctx.fillRect(x, canvas.height - barHeight, barWidth - 1, barHeight);
        }

        // Calculate and display volume
        const avgVolume = volumeSum / dataArray.length;
        volumeMeter.innerHTML = `<i class="fas fa-volume-${avgVolume > 0.5 ? 'up' : avgVolume > 0.1 ? 'down' : 'mute'} mr-1"></i><span>${avgVolume.toFixed(2)}</span>`;

        // Calculate dominant frequency
        const dominantFreq = maxIndex * (audioContext.sampleRate / 2) / dataArray.length;
        dominantFreqElement.textContent = `${Math.round(dominantFreq)} Hz`;

        // Calculate and display frequency ranges
        const bassAvg = bassSum / bassEnd;
        const midAvg = midSum / (midEnd - bassEnd);
        const trebleAvg = trebleSum / (dataArray.length - midEnd);

        bassLevelElement.textContent = bassAvg.toFixed(2);
        midLevelElement.textContent = midAvg.toFixed(2);
        trebleLevelElement.textContent = trebleAvg.toFixed(2);
    }

    // Event listeners
    toggleMicBtn.addEventListener('click', () => {
        if (isAnalyzing) {
            stopAnalysis();
        } else {
            startAnalysis();
        }
    });

    fftSizeSelect.addEventListener('change', () => {
        if (analyser && isAnalyzing) {
            analyser.fftSize = parseInt(fftSizeSelect.value);
            const bufferLength = analyser.frequencyBinCount;
            dataArray = new Uint8Array(bufferLength);
        }
    });

    // Handle window resize
    window.addEventListener('resize', () => {
        initVisualizer();
    });

    // Initialize
    initVisualizer();

    // Add FFT size explanation
    const fftSizeLabel = document.createElement('div');
    fftSizeLabel.className = 'text-xs text-gray-400 mt-1';
    fftSizeLabel.textContent = 'Higher values provide more frequency detail';
    fftSizeSelect.parentNode.appendChild(fftSizeLabel);
});