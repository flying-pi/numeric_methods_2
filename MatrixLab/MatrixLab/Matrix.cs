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

		public double Trace
		{
			get
			{
				int diagSize = collumnCount < rowCount ? collumnCount : rowCount;
				return AccomulateFor(0, i => i < diagSize, i => ++i, 0.0, (i, sum) => sum + matrix[i, i]);
			}
		}

		public Matrix(int rowCount, int collumnCount)
		{
			this.rowCount = rowCount;
			this.collumnCount = collumnCount;
			matrix = new double[rowCount, collumnCount];
		}

		public Matrix(string source)
		{
			source = source.Replace("\t\t", "\t");
			var lines = source.Split('\n');
			rowCount = lines.Length;
			collumnCount = (rowCount == 0 ? 0 : lines[0].Count(f => f == '\t')) + 1;
			matrix = new double[rowCount, collumnCount];
			for (int i = 0; i < lines.Length; i++)
			{
				string str = lines[i];
				var puts = str.Split('\t');
				for (int j = 0; j < puts.Length; j++)
				{
					string put = puts[j];
					if (!Double.TryParse(put, out matrix[i, j]))
						matrix[i, j] = Double.NaN;
				}
			}

			Console.Write(this);
		}

		public Matrix(Matrix otherMatrix)
		{
			this.collumnCount = otherMatrix.collumnCount;
			this.rowCount = otherMatrix.rowCount;
			this.matrix = otherMatrix.matrix;
		}

		public static Matrix operator +(Matrix m1, Matrix m2)
		{
			Matrix result = new Matrix(m1);
			result.addMatrix(m2);
			return result;
		}

		public void addMatrix(Matrix m)
		{
			if (m.collumnCount != this.collumnCount || m.rowCount != this.rowCount)
				throw new WrongMatrixSizeException();
			this.matrix.ForeEach((i, j, data) => m.matrix[i, j] + data);
		}

		public static Matrix operator -(Matrix m1, Matrix m2)
		{
			Matrix result = new Matrix(m1);
			result.minusMatrix(m2);
			return result;
		}


		public void minusMatrix(Matrix m)
		{
			if (m.collumnCount != this.collumnCount || m.rowCount != this.rowCount)
				throw new WrongMatrixSizeException();
			this.matrix.ForeEach((i, j, data) => data - m.matrix[i, j]);
		}

		public static Matrix operator *(Matrix m1, double scalar)
		{
			Matrix result = new Matrix(m1);
			result.mullMatrix(scalar);
			return result;
		}

		public static Matrix operator *(Matrix m1, Matrix m2)
		{
			if (m1.collumnCount != m2.rowCount)
				throw new WrongMatrixSizeException();
			Matrix result = new Matrix(m1.rowCount, m2.collumnCount);
			result.matrix.ForeEach((i, j, _) => AccomulateFor(0, (c => c < m1.collumnCount), (c => ++c), 0.0
																	, ((c, state) => state + (m1.matrix[i, c] * m2.matrix[c, j]))));
			return result;
		}


		public void mullMatrix(double scalar)
		{
			this.matrix.ForeEach((i, j, data) => data * scalar);
		}

		public static Matrix operator /(Matrix m1, double scalar)
		{
			Matrix result = new Matrix(m1);
			result.divMatrix(scalar);
			return result;
		}

		public void divMatrix(double scalar)
		{
			this.matrix.ForeEach((i, j, data) => data / scalar);
		}

		public Matrix Transponent()
		{
			Matrix result = new Matrix(collumnCount, rowCount);
			matrix.ForeEach((i, j, item) => result.matrix[j,i] = item);
			return result;
		}

		public Matrix pow(int s)
		{
			Matrix result = new Matrix(this);
			while (--s > 0)
				result *= this;
			return result;
		}

		public Matrix fillRundom(double min, double max)
		{
			Random rand = new Random();
			double delta = max - min;
			matrix.ConvertInPlace(_ => rand.NextDouble() * delta + min);
			return this;
		}

		public Matrix fillRundom(int min, int max)
		{
			Random rand = new Random();
			int delta = max - min;
			matrix.ConvertInPlace(_ => rand.Next() % delta + min);
			return this;
		}

		public override string ToString()
		{
			string str = "Matrix :: \n\t" + matrix.Collapse(new StringBuilder(), (i, j, value, res) =>
							  res.Append(j == 0 ? "\n\t" : "\t").Append(value.ToString())).
							   ToString().TrimStart();
			return $"{str}";
		}

	}
	public class WrongMatrixSizeException : Exception
	{
		public WrongMatrixSizeException() : this("the matrxes has different size", null)
		{
		}

		public WrongMatrixSizeException(string description) : this(description, null)
		{

		}

		public WrongMatrixSizeException(string description, Exception parentException) : base(description, parentException)
		{
		}
	}
}