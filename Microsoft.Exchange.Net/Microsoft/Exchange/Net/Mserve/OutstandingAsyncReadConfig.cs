using System;
using System.IO;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x0200088C RID: 2188
	internal sealed class OutstandingAsyncReadConfig
	{
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x00066783 File Offset: 0x00064983
		public HttpClient Client
		{
			get
			{
				return this.client;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x0006678B File Offset: 0x0006498B
		public HttpSessionConfig SessionConfig
		{
			get
			{
				return this.sessionConfig;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x00066793 File Offset: 0x00064993
		public StreamWriter XmlStreamWriter
		{
			get
			{
				return this.xmlStreamWriter;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x0006679B File Offset: 0x0006499B
		public CancelableAsyncCallback ClientCallback
		{
			get
			{
				return this.clientCallback;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000667A3 File Offset: 0x000649A3
		public object ClientState
		{
			get
			{
				return this.clientState;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000667AB File Offset: 0x000649AB
		public int CachePartnerId
		{
			get
			{
				return this.cachePartnerId;
			}
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000667B4 File Offset: 0x000649B4
		public OutstandingAsyncReadConfig(HttpClient client, HttpSessionConfig sessionConfig, StreamWriter xmlStreamWriter, CancelableAsyncCallback clientCallback, int cachePartnerId, object clientState)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			if (sessionConfig == null)
			{
				throw new ArgumentNullException("sessionConfig");
			}
			if (xmlStreamWriter == null)
			{
				throw new ArgumentNullException("xmlStreamWriter");
			}
			this.client = client;
			this.sessionConfig = sessionConfig;
			this.xmlStreamWriter = xmlStreamWriter;
			this.clientCallback = clientCallback;
			this.cachePartnerId = cachePartnerId;
			this.clientState = clientState;
		}

		// Token: 0x04002887 RID: 10375
		private readonly HttpClient client;

		// Token: 0x04002888 RID: 10376
		private readonly HttpSessionConfig sessionConfig;

		// Token: 0x04002889 RID: 10377
		private readonly StreamWriter xmlStreamWriter;

		// Token: 0x0400288A RID: 10378
		private readonly CancelableAsyncCallback clientCallback;

		// Token: 0x0400288B RID: 10379
		private readonly object clientState;

		// Token: 0x0400288C RID: 10380
		private readonly int cachePartnerId;
	}
}
