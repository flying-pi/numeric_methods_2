﻿using System;
using System.Collections.Generic;

namespace MatrixLab
{
	public static class Utils
	{
		public static void ConvertInPlace<T>(this T[,] source, Func<T, T> projection)
		{
			for (int i = 0; i < source.GetLength(0); i++)
			{
				for (int j = 0; j < source.GetLength(1); j++)
				{
					source[i, j] = projection(source[i, j]);
				}
			}
		}

		public static void ForeEach<T>(this T[,] source, Func<int, int,T, T> projection)
		{
			for (int i = 0; i < source.GetLength(0); i++)
			{
				for (int j = 0; j < source.GetLength(1); j++)
				{
					source[i, j] = projection(i,j,source[i, j]);
				}
			}
		}


		public static R Collapse<T,R>(this T[,] source, R init, Func<int, int, T, R, R> func)
		{
			for (int i = 0; i < source.GetLength(0); i++)
				for (int j = 0; j < source.GetLength(1); j++)
					init = func(i, j, source[i, j], init);
			return init;
		}

	}

}
