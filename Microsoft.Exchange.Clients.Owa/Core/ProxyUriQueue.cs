using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000221 RID: 545
	public class ProxyUriQueue
	{
		// Token: 0x06001259 RID: 4697 RVA: 0x0006FD99 File Offset: 0x0006DF99
		public ProxyUriQueue(string uriString) : this(ProxyUri.Create(uriString))
		{
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0006FDA7 File Offset: 0x0006DFA7
		public ProxyUriQueue(ProxyUri uri)
		{
			this.head = -1;
			base..ctor();
			if (uri == null)
			{
				throw new ArgumentNullException();
			}
			this.data = new ProxyUri[1];
			this.data[0] = uri;
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0006FDD4 File Offset: 0x0006DFD4
		public ProxyUriQueue(ProxyUri[] proxyUris)
		{
			this.head = -1;
			base..ctor();
			if (proxyUris == null)
			{
				throw new ArgumentNullException("proxyUris");
			}
			this.data = proxyUris;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0006FDF8 File Offset: 0x0006DFF8
		public ProxyUri Pop()
		{
			this.head++;
			if (this.head >= this.data.Length)
			{
				this.head = 0;
			}
			ProxyUri proxyUri = this.data[this.head];
			if (!proxyUri.IsParsed)
			{
				proxyUri.Parse();
			}
			return proxyUri;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0006FE48 File Offset: 0x0006E048
		public ProxyUri Head
		{
			get
			{
				return this.data[this.head];
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0006FE57 File Offset: 0x0006E057
		public int Count
		{
			get
			{
				return this.data.Length;
			}
		}

		// Token: 0x04000C9B RID: 3227
		private int head;

		// Token: 0x04000C9C RID: 3228
		private ProxyUri[] data;
	}
}
