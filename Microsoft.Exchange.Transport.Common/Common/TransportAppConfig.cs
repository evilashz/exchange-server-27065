using System;
using System.Collections.Specialized;

namespace Microsoft.Exchange.Transport.Common
{
	// Token: 0x0200000B RID: 11
	internal abstract class TransportAppConfig : AppConfig
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002F55 File Offset: 0x00001155
		protected TransportAppConfig(NameValueCollection appSettings = null) : base(appSettings)
		{
		}
	}
}
