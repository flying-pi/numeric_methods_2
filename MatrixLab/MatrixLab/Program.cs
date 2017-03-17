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
			var a = new Matrix("6\t4\t6\n8\t2\t7");
			//var b = new Matrix("9\t6\t5\t8\n8\t4\t7\t2\n9\t2\t2\t6");
			var b = new Matrix(2, 2).fillRundom(1,10);

			Console.WriteLine(a);
			Console.WriteLine(b);
			Console.WriteLine(b.pow(2));
			Application.Run();

		}
	}

}
