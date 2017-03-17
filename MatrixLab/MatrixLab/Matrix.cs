using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MatrixLab.Utils;

namespace MatrixLab
{
	public class Matrix
	{
		public static Matrix E(int size)
		{
			Matrix result = new Matrix(size, size);
			result.matrix.ForeEach((i, j, _) => i == j ? 1 : 0);
			return result;
		}

		private readonly int collumnCount;
		private readonly int rowCount;
		private readonly double[,] matrix;

		public Matrix(int collumnCount, int rowCount)
		{
			this.rowCount = rowCount;
			this.collumnCount = collumnCount;
			matrix = new double[collumnCount, rowCount];
		}

		public Matrix(string source)
		{
			source = source.Replace("\t\t", "\t");
			var lines = source.Split('\n');
			rowCount = lines.Length;
			collumnCount = (rowCount == 0 ? 0 : lines[0].Count(f => f == '\t')) + 1;
			matrix = new double[collumnCount, rowCount];
			for (int i = 0; i < lines.Length; i++)
			{
				string str = lines[i];
				var puts = str.Split('\t');
				for (int j = 0; j < puts.Length; j++)
				{
					string put = puts[j];
					if (!Double.TryParse(put, out matrix[j, i]))
						matrix[j, i] = Double.NaN;
				}
			}
		}

		public Matrix fillRundom(double min, double max)
		{
			Random rand = new Random();
			double delta = max - min;
			matrix.ConvertInPlace(_ => rand.NextDouble() * delta + min);
			return this;
		}

		public override string ToString()
		{
			string str = matrix.Collapse(new StringBuilder(), (i, j, value, res) =>
							res.Append(j == 0 ? "\n" : "\t").Append(value.ToString())).
				  ToString().Trim();
			return $"{str}";
		}

	}
}