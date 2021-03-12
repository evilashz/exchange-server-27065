using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000040 RID: 64
	internal class RpcHttpConnectionRegistrationCacheEntry : DisposeTrackableBase
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public RpcHttpConnectionRegistrationCacheEntry(Guid associationGroupId, ClientSecurityContext clientSecurityContext, string authIdentifier, string serverTarget, string sessionCookie, string clientIp)
		{
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			this.associationGroupId = associationGroupId;
			this.clientSecurityContext = clientSecurityContext;
			this.authIdentifier = authIdentifier;
			this.serverTarget = serverTarget;
			this.sessionCookie = sessionCookie;
			this.clientIp = clientIp;
			this.requestIds = new List<Guid>(4);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000CB76 File Offset: 0x0000AD76
		public Guid AssociationGroupId
		{
			get
			{
				base.CheckDisposed();
				return this.associationGroupId;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public string AuthIdentifier
		{
			get
			{
				base.CheckDisposed();
				return this.authIdentifier;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000CB92 File Offset: 0x0000AD92
		public string ClientIp
		{
			get
			{
				base.CheckDisposed();
				return this.clientIp;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		public IList<Guid> RequestIds
		{
			get
			{
				base.CheckDisposed();
				return this.requestIds;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		public string SessionCookie
		{
			get
			{
				base.CheckDisposed();
				return this.sessionCookie;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public string ServerTarget
		{
			get
			{
				base.CheckDisposed();
				return this.serverTarget;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000CBCA File Offset: 0x0000ADCA
		public void AddRequest(Guid requestId)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("requestId", requestId);
			this.requestIds.Add(requestId);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public void AddRequest(Guid requestId, ClientSecurityContext requestClientSecurityContext)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("requestId", requestId);
			ArgumentValidator.ThrowIfNull("requestClientSecurityContext", requestClientSecurityContext);
			if (this.clientSecurityContext.UserSid != requestClientSecurityContext.UserSid)
			{
				string message = string.Format("AssociationGroupId '{0}' already exists with user sid '{1}'.  Cannot replace with user sid '{2}'.", this.associationGroupId, this.clientSecurityContext.UserSid, requestClientSecurityContext.UserSid);
				throw new ConnectionRegistrationException(message);
			}
			this.requestIds.Add(requestId);
			if (!object.ReferenceEquals(requestClientSecurityContext, this.clientSecurityContext))
			{
				ClientSecurityContext clientSecurityContext = this.clientSecurityContext;
				this.clientSecurityContext = requestClientSecurityContext;
				clientSecurityContext.Dispose();
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000CC92 File Offset: 0x0000AE92
		public void RemoveRequest(Guid requestId)
		{
			base.CheckDisposed();
			this.requestIds.Remove(requestId);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000CCA7 File Offset: 0x0000AEA7
		public ClientSecurityContext GetClientSecurityContext()
		{
			base.CheckDisposed();
			return this.clientSecurityContext.Clone();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000CCBA File Offset: 0x0000AEBA
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.clientSecurityContext != null)
			{
				this.clientSecurityContext.Dispose();
				this.clientSecurityContext = null;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000CCD9 File Offset: 0x0000AED9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RpcHttpConnectionRegistrationCacheEntry>(this);
		}

		// Token: 0x04000121 RID: 289
		private readonly Guid associationGroupId;

		// Token: 0x04000122 RID: 290
		private readonly string authIdentifier;

		// Token: 0x04000123 RID: 291
		private readonly string sessionCookie;

		// Token: 0x04000124 RID: 292
		private readonly string serverTarget;

		// Token: 0x04000125 RID: 293
		private readonly string clientIp;

		// Token: 0x04000126 RID: 294
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x04000127 RID: 295
		private List<Guid> requestIds;
	}
}
