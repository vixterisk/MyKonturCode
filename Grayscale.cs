namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
        {
            int amountOfRows = original.GetLength(0);
            int amountOfCols = original.GetLength(1);
            double[,] greyScale = new double[amountOfRows, amountOfCols];
            for (int i = 0; i < amountOfRows; i++)
                for (int j = 0; j < amountOfCols; j++)
                {
					double brightR = 0.299 * original[i, j].R;
					double brightG = 0.587 * original[i, j].G;
					double brightB = 0.114 * original[i, j].B;
                    greyScale[i, j] = (brightR + brightG + brightB) / 255;
                }
			return greyScale;
		}
	}
}
