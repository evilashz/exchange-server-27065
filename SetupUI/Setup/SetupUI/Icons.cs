using System;
using System.Drawing;

namespace Microsoft.Exchange.Setup.SetupUI
{
	// Token: 0x02000003 RID: 3
	internal class Icons
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000263B File Offset: 0x0000083B
		public static Icon EXCHANGE
		{
			get
			{
				return Icons.iconEXCHANGE;
			}
		}

		// Token: 0x04000002 RID: 2
		private static Icon iconEXCHANGE = new Icon(typeof(Icons), "Icons.EXCHANGE.ico");
	}
}
