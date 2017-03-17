using System;
using Gtk;

namespace MatrixLab
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			CreateMatrixDialog win = new CreateMatrixDialog();
			win.Show();
			Console.Write(win.EnteredMatrix);
			var v = new Matrix(12, 8).fillRundom(1, 5);
			Console.Write(v);
			Application.Run();
		}
	}
}
