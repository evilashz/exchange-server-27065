using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200006B RID: 107
	internal sealed class AutoDiscoverResult
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000D002 File Offset: 0x0000B202
		public AutoDiscoverResult(LocalizedException exception)
		{
			this.Exception = exception;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000D011 File Offset: 0x0000B211
		public AutoDiscoverResult(WebServiceUri webServiceUri, ProxyAuthenticator proxyAuthenticator)
		{
			this.ProxyAuthenticator = proxyAuthenticator;
			this.WebServiceUri = webServiceUri;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000D027 File Offset: 0x0000B227
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000D02F File Offset: 0x0000B22F
		public WebServiceUri WebServiceUri { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000D038 File Offset: 0x0000B238
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000D040 File Offset: 0x0000B240
		public ProxyAuthenticator ProxyAuthenticator { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000D049 File Offset: 0x0000B249
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000D051 File Offset: 0x0000B251
		public LocalizedException Exception { get; private set; }
	}
}
