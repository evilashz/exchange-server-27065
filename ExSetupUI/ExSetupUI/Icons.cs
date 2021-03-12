using System;
using System.Drawing;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000035 RID: 53
	internal class Icons
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		public static Icon EXCHANGE
		{
			get
			{
				return Icons.iconEXCHANGE;
			}
		}

		// Token: 0x04000189 RID: 393
		private static Icon iconEXCHANGE = new Icon(typeof(Icons), "Icons.EXCHANGE.ico");
	}
}
