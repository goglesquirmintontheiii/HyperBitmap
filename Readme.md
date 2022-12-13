90% of methods have an explanation. Methods and properties return the same as they would with a Bitmap object and accept the same parameters.

NOTE: You may need to use FlipGetPixel or FlipSetPixel. I swapped R and B values in plain GetPixel and SetPixel because they appeared swapped on my device. If you find they're swapped on your device too, use FlipGetPixel/FlipSetPixel. If this is the case, you will need to manually swap R and B values for DrawRect, LocateColor, LocateColors, and Paint (Probably.)

WARNING: Only supports the 32BppArgb pixel format. You will likely run into errors if you create a FastBmp out of a Bitmap with any other format.

Expect problems. If you run into swapped colors or any bug/error please create an issue.

If you have questions or concerns message me on Discord @ cloud pfp#6907

