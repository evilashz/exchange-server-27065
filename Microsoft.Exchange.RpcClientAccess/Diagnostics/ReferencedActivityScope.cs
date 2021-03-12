using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ReferencedActivityScope : ReferenceCount<ReferencedActivityScope.ActivityScopeGuard>
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00008B4B File Offset: 0x00006D4B
		private ReferencedActivityScope(ReferencedActivityScope.ActivityScopeGuard activityScopeGuard) : base(activityScopeGuard)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008B54 File Offset: 0x00006D54
		public static ReferencedActivityScope Create(IEnumerable<KeyValuePair<Enum, object>> initialMetadata)
		{
			ReferencedActivityScope referencedActivityScope = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				try
				{
					ActivityContext.ClearThreadScope();
					IActivityScope activityScope = ActivityContext.Start(null);
					ReferencedActivityScope.ActivityScopeGuard activityScopeGuard = new ReferencedActivityScope.ActivityScopeGuard(activityScope);
					disposeGuard.Add<ReferencedActivityScope.ActivityScopeGuard>(activityScopeGuard);
					referencedActivityScope = new ReferencedActivityScope(activityScopeGuard);
					if (initialMetadata != null)
					{
						referencedActivityScope.SetMetadata(initialMetadata);
					}
					activityScope.UserState = referencedActivityScope;
				}
				finally
				{
					ActivityContext.SetThreadScope(currentActivityScope);
				}
				disposeGuard.Success();
			}
			return referencedActivityScope;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public static ReferencedActivityScope Current
		{
			get
			{
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null)
				{
					return currentActivityScope.UserState as ReferencedActivityScope;
				}
				return null;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00008C0B File Offset: 0x00006E0B
		public IActivityScope ActivityScope
		{
			get
			{
				return base.ReferencedObject.ActivityScope;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00008C18 File Offset: 0x00006E18
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00008C25 File Offset: 0x00006E25
		public string TenantId
		{
			get
			{
				return this.ActivityScope.TenantId;
			}
			set
			{
				this.ActivityScope.TenantId = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00008C33 File Offset: 0x00006E33
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00008C40 File Offset: 0x00006E40
		public string Protocol
		{
			get
			{
				return this.ActivityScope.Protocol;
			}
			set
			{
				this.ActivityScope.Protocol = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00008C4E File Offset: 0x00006E4E
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00008C5B File Offset: 0x00006E5B
		public string UserEmail
		{
			get
			{
				return this.ActivityScope.UserEmail;
			}
			set
			{
				this.ActivityScope.UserEmail = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00008C69 File Offset: 0x00006E69
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00008C76 File Offset: 0x00006E76
		public string UserId
		{
			get
			{
				return this.ActivityScope.UserId;
			}
			set
			{
				this.ActivityScope.UserId = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00008C84 File Offset: 0x00006E84
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00008C91 File Offset: 0x00006E91
		public string Puid
		{
			get
			{
				return this.ActivityScope.Puid;
			}
			set
			{
				this.ActivityScope.Puid = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00008C9F File Offset: 0x00006E9F
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00008CAC File Offset: 0x00006EAC
		public string Component
		{
			get
			{
				return this.ActivityScope.Component;
			}
			set
			{
				this.ActivityScope.Component = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00008CBA File Offset: 0x00006EBA
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00008CC7 File Offset: 0x00006EC7
		public string ClientInfo
		{
			get
			{
				return this.ActivityScope.ClientInfo;
			}
			set
			{
				this.ActivityScope.ClientInfo = value;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008CD8 File Offset: 0x00006ED8
		private void SetMetadata(IEnumerable<KeyValuePair<Enum, object>> metadata)
		{
			foreach (KeyValuePair<Enum, object> keyValuePair in metadata)
			{
				string text = keyValuePair.Value as string;
				if (keyValuePair.Value == null || text != null)
				{
					this.ActivityScope.SetProperty(keyValuePair.Key, text);
				}
			}
		}

		// Token: 0x02000040 RID: 64
		internal sealed class ActivityScopeGuard : BaseObject
		{
			// Token: 0x06000274 RID: 628 RVA: 0x00008D48 File Offset: 0x00006F48
			public ActivityScopeGuard(IActivityScope activityScope)
			{
				if (activityScope == null)
				{
					throw new ArgumentNullException("activityScope");
				}
				this.activityScope = activityScope;
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000275 RID: 629 RVA: 0x00008D65 File Offset: 0x00006F65
			public IActivityScope ActivityScope
			{
				get
				{
					return this.activityScope;
				}
			}

			// Token: 0x06000276 RID: 630 RVA: 0x00008D6D File Offset: 0x00006F6D
			protected override void InternalDispose()
			{
				this.activityScope.End();
				base.InternalDispose();
			}

			// Token: 0x06000277 RID: 631 RVA: 0x00008D80 File Offset: 0x00006F80
			protected override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ReferencedActivityScope.ActivityScopeGuard>(this);
			}

			// Token: 0x040001E4 RID: 484
			private readonly IActivityScope activityScope;
		}
	}
}
