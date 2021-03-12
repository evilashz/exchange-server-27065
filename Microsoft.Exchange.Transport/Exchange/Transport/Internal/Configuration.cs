using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.Internal
{
	// Token: 0x02000022 RID: 34
	public static class Configuration
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003E43 File Offset: 0x00002043
		public static TransportConfigContainer TransportConfigObject
		{
			get
			{
				return Components.Configuration.TransportSettings.TransportSettings;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003E54 File Offset: 0x00002054
		public static Server TransportServer
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer;
			}
		}
	}
}
