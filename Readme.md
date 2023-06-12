Methods and properties return the same as they would with a Bitmap object and accept the same parameters; FastBmp was made to be 1:1 with the Bitmap class (with some extra features).
You may use more memory when using FastBmp than when using Bitmap, since FastBmp contains some code to prevent memory errors as much as possible.

If you run into R and A values being swapped, you may need to use .FlipSetPixel and .FlipGetPixel .

Designed to use RGBA / 32bpprgba format only. Expect unexpected behaviour if using other image formats.-

Expect issues, this package is not perfect (yet). If you run into swapped colors or any bug/error please create an issue.

If you have questions, concerns, or suggestions message me on Discord (@animepfp)
