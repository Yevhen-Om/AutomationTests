function simulateDragAndDrop(sourceSelector, targetSelector) {
    const source = document.querySelector(sourceSelector);
    const target = document.querySelector(targetSelector);

    if (!source || !target) {
        return 'Source or target element not found';
    }

    const dataTransfer = new DataTransfer();
    const eventInit = { bubbles: true, cancelable: true, dataTransfer };

    source.dispatchEvent(new DragEvent('dragstart', eventInit));
    target.dispatchEvent(new DragEvent('dragover', eventInit));
    target.dispatchEvent(new DragEvent('drop', eventInit));
    source.dispatchEvent(new DragEvent('dragend', eventInit));

    return 'Drag and drop successful';
}