using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000B RID: 11
	internal class MRSProxyRequestContext
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00007BD9 File Offset: 0x00005DD9
		public MRSProxyRequestContext()
		{
			this.Id = Guid.NewGuid();
			this.HttpHeaders = new Dictionary<string, string>();
			MRSProxyRequestContext.activeProxyClientContexts[this.Id] = this;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00007C08 File Offset: 0x00005E08
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00007C10 File Offset: 0x00005E10
		public Guid Id { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00007C19 File Offset: 0x00005E19
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00007C21 File Offset: 0x00005E21
		public Dictionary<string, string> HttpHeaders { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00007C2A File Offset: 0x00005E2A
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00007C32 File Offset: 0x00005E32
		public Uri EndpointUri { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00007C3B File Offset: 0x00005E3B
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00007C43 File Offset: 0x00005E43
		public Cookie BackendCookie { get; set; }

		// Token: 0x060000CD RID: 205 RVA: 0x00007C4C File Offset: 0x00005E4C
		public static MRSProxyRequestContext Find(Guid id)
		{
			MRSProxyRequestContext result;
			if (MRSProxyRequestContext.activeProxyClientContexts.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007C6C File Offset: 0x00005E6C
		public void Unregister()
		{
			MRSProxyRequestContext mrsproxyRequestContext;
			MRSProxyRequestContext.activeProxyClientContexts.TryRemove(this.Id, out mrsproxyRequestContext);
		}

		// Token: 0x0400002D RID: 45
		private static readonly ConcurrentDictionary<Guid, MRSProxyRequestContext> activeProxyClientContexts = new ConcurrentDictionary<Guid, MRSProxyRequestContext>();
	}
}
