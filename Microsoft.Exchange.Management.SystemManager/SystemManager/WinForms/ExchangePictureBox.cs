using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E2 RID: 482
	internal class ExchangePictureBox : PictureBox
	{
		// Token: 0x060015C5 RID: 5573 RVA: 0x000596BC File Offset: 0x000578BC
		public ExchangePictureBox()
		{
			base.Name = "ExchangePictureBox";
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000596CF File Offset: 0x000578CF
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			if (specified == BoundsSpecified.All)
			{
				factor.Height = Math.Min(factor.Height, factor.Width);
				factor.Width = factor.Height;
			}
			base.ScaleControl(factor, specified);
		}
	}
}
