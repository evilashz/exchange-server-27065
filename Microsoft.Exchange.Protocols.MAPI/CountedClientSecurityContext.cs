using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000093 RID: 147
	internal class CountedClientSecurityContext : DisposableBase
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000265D1 File Offset: 0x000247D1
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x000265D9 File Offset: 0x000247D9
		public DateTime WhenCreated { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000265E2 File Offset: 0x000247E2
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x000265EA File Offset: 0x000247EA
		public bool MarkedAsStale { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x000265F3 File Offset: 0x000247F3
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x000265FB File Offset: 0x000247FB
		public ulong Cookie { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00026604 File Offset: 0x00024804
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0002660C File Offset: 0x0002480C
		public int ActiveSessions { get; internal set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00026615 File Offset: 0x00024815
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0002661D File Offset: 0x0002481D
		public int ActiveRPCThreads { get; internal set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00026626 File Offset: 0x00024826
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0002662E File Offset: 0x0002482E
		public ClientSecurityContext SecurityContext { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00026637 File Offset: 0x00024837
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0002663F File Offset: 0x0002483F
		public SecurityContextKey SecurityContextKey { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00026648 File Offset: 0x00024848
		public SecurityIdentifier UserSid
		{
			get
			{
				return this.SecurityContext.UserSid;
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00026658 File Offset: 0x00024858
		public CountedClientSecurityContext(ClientSecurityContext clientSecurityContext, SecurityContextKey securityContextKey, ulong cookie)
		{
			Globals.AssertRetail(clientSecurityContext != null, "clientSecurityContext can't be null.");
			Globals.AssertRetail(securityContextKey != null, "securityContextKey can't be null.");
			this.WhenCreated = DateTime.UtcNow;
			this.MarkedAsStale = false;
			this.Cookie = cookie;
			this.ActiveSessions = 1;
			this.ActiveRPCThreads = 1;
			this.SecurityContext = clientSecurityContext;
			this.SecurityContextKey = securityContextKey;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000266C4 File Offset: 0x000248C4
		public bool IsStale()
		{
			if (this.UserSid.IsWellKnown(WellKnownSidType.LocalSystemSid) || this.UserSid.IsWellKnown(WellKnownSidType.NetworkServiceSid))
			{
				return false;
			}
			TimeSpan t = DateTime.UtcNow - this.WhenCreated;
			return t > TimeSpan.FromMinutes(5.0);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00026716 File Offset: 0x00024916
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CountedClientSecurityContext>(this);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00026720 File Offset: 0x00024920
		protected override void InternalDispose(bool calledFromDispose)
		{
			Trace securityContextManagerTracer = ExTraceGlobals.SecurityContextManagerTracer;
			bool flag = securityContextManagerTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (calledFromDispose && this.SecurityContext != null)
			{
				if (flag)
				{
					securityContextManagerTracer.TraceDebug<string>(0L, "INTERNALDISPOSE: disposing context{0}", this.ToString());
				}
				this.SecurityContext.Dispose();
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00026768 File Offset: 0x00024968
		public override string ToString()
		{
			return string.Format("\nSID:{0} CK:{1} CTR:{2} RPC:{3} SI:{4} CREATED:{5}", new object[]
			{
				this.SecurityContext.UserSid,
				this.Cookie,
				this.ActiveSessions,
				this.ActiveRPCThreads,
				!this.MarkedAsStale,
				this.WhenCreated
			});
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000267DE File Offset: 0x000249DE
		internal void MarkStaleForTest()
		{
			this.WhenCreated = DateTime.UtcNow - TimeSpan.FromMinutes(10.0);
		}

		// Token: 0x04000316 RID: 790
		private const ushort StaleTTLInMinutes = 5;
	}
}
