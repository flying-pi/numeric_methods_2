using System;
namespace MatrixLab
{
	public partial class CreateMatrixDialog : Gtk.Dialog
	{
		private Matrix matrix = null;

		public Matrix EnteredMatrix
		{
			private set{}
			get
			{
				return matrix;
			}
		}
		public CreateMatrixDialog()
		{
			this.Build();
			this.Close += closed;
			this.buttonOk.Clicked += onOKClicked;
			this.buttonCancel.Clicked += onCancelClicked;
		}

		void closed(object sender, EventArgs e)
		{
			
		}

		void onOKClicked(object sender, EventArgs e)
		{
			matrix = new Matrix(matrixText.Buffer.Text);
			this.Destroy();
		}

		void onCancelClicked(object sender, EventArgs e)
		{
			this.Destroy();
		}
}
}
