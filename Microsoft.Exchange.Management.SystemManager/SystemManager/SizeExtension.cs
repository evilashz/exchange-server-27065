using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200002F RID: 47
	public static class SizeExtension
	{
		// Token: 0x0600020A RID: 522 RVA: 0x000084E4 File Offset: 0x000066E4
		public static Padding Scale(this Padding padding, SizeF factor)
		{
			return new Padding((int)((float)padding.Left * factor.Width), (int)((float)padding.Top * factor.Height), (int)((float)padding.Right * factor.Width), (int)((float)padding.Bottom * factor.Height));
		}
	}
}
