using System;
using System.Globalization;
using System.Web;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Services.DispatchPipe.Ews;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008C8 RID: 2248
	internal class MSACallContext : CallContext
	{
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003F9B RID: 16283 RVA: 0x000DBF56 File Offset: 0x000DA156
		internal string MemberName
		{
			get
			{
				return this.memberName;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003F9C RID: 16284 RVA: 0x000DBF5E File Offset: 0x000DA15E
		internal override string EffectiveCallerNetId
		{
			get
			{
				return this.effectiveCallerNetId;
			}
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x000DBF66 File Offset: 0x000DA166
		internal MSACallContext(HttpContext httpContext, AppWideStoreSessionCache mailboxSessionCache, AcceptedDomainCache acceptedDomainCache, MSAIdentity msaIdentity, UserWorkloadManager workloadManager, CultureInfo clientCulture) : this(httpContext, mailboxSessionCache, acceptedDomainCache, msaIdentity, workloadManager, clientCulture, false)
		{
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x000DBF78 File Offset: 0x000DA178
		internal MSACallContext(HttpContext httpContext, AppWideStoreSessionCache mailboxSessionCache, AcceptedDomainCache acceptedDomainCache, MSAIdentity msaIdentity, UserWorkloadManager workloadManager, CultureInfo clientCulture, bool isMock) : base(httpContext, EwsOperationContextBase.Current, RequestDetailsLogger.Current, isMock)
		{
			this.memberName = msaIdentity.MemberName;
			this.userKind = CallContext.UserKind.MSA;
			this.effectiveCallerNetId = msaIdentity.NetId;
			this.clientCulture = clientCulture;
			this.sessionCache = new SessionCache(mailboxSessionCache, this);
			this.workloadManager = workloadManager;
			this.acceptedDomainCache = acceptedDomainCache;
			this.serverCulture = EWSSettings.ServerCulture;
			this.userAgent = httpContext.Request.UserAgent;
			this.requestedLogonType = RequestedLogonType.Default;
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000DC004 File Offset: 0x000DA204
		public override string ToString()
		{
			return "MSA call context for " + this.memberName;
		}

		// Token: 0x04002466 RID: 9318
		private readonly string memberName;

		// Token: 0x04002467 RID: 9319
		private readonly string effectiveCallerNetId;
	}
}
