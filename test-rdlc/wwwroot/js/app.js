window.downloadFileFromStream = async (fileName, contentType, content) => {
    const blob = new Blob([content], { type: contentType });
    const url = window.URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName;
    anchorElement.click();
    anchorElement.remove();
    window.URL.revokeObjectURL(url);
}