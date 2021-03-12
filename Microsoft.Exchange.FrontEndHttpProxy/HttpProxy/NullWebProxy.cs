using System;
using System.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000096 RID: 150
	internal class NullWebProxy : IWebProxy
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00019F1B File Offset: 0x0001811B
		public static NullWebProxy Instance
		{
			get
			{
				return NullWebProxy.instance;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00019F22 File Offset: 0x00018122
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00019F2A File Offset: 0x0001812A
		public ICredentials Credentials { get; set; }

		// Token: 0x0600046C RID: 1132 RVA: 0x00019F33 File Offset: 0x00018133
		public Uri GetProxy(Uri destination)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00019F3A File Offset: 0x0001813A
		public bool IsBypassed(Uri host)
		{
			return true;
		}

		// Token: 0x04000384 RID: 900
		private static NullWebProxy instance = new NullWebProxy();
	}
}
