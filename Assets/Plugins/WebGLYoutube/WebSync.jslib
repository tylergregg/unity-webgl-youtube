mergeInto(LibraryManager.library, {

    CreateIframe: function(id, videoIdArrayJSON, listType, listId, 
		autoplay, loop, shuffle, width, height, xPosition, yPosition, zPosition, yRotation) {

		if (yRotation == 90 || yRotation == 270) { // strange Safari bug where videos are blank with only audio
			yRotation += .0001;
		}

        addYoutubeIframe(UTF8ToString(id), UTF8ToString(videoIdArrayJSON), UTF8ToString(listType), UTF8ToString(listId),
			autoplay, loop, shuffle, width, height, xPosition, yPosition, zPosition, -yRotation * Math.PI / 180 );
    },

    SyncCameraTransform: function (x, y, z, rx, ry, rz, rw) {
        cameraPositionVector.set(x, y, z);
        cameraRotationQuaternion.set(rx, ry, rz, rw);
    },

    SetupYoutube: function() {
        initializeYoutube();
    },

	ClickIframe: function(iframeId) {
		handlePlayerClick(UTF8ToString(iframeId));
	},

	ClickNextButton: function(iframeId) {
		handleNextButtonClick(UTF8ToString(iframeId));
	},

	ClickPreviousButton: function(iframeId) {
		handlePreviousButtonClick(UTF8ToString(iframeId));
	},

});