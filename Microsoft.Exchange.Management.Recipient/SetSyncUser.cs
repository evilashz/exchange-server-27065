using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D5 RID: 213
	[Cmdlet("Set", "SyncUser", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSyncUser : SetADUserBase<NonMailEnabledUserIdParameter, SyncUser>
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0003C53C File Offset: 0x0003A73C
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0003C544 File Offset: 0x0003A744
		[Parameter(Mandatory = false)]
		public SwitchParameter SoftDeletedUser { get; set; }

		// Token: 0x060010A7 RID: 4263 RVA: 0x0003C550 File Offset: 0x0003A750
		protected override IConfigDataProvider CreateSession()
		{
			if (this.SoftDeletedUser.IsPresent)
			{
				base.SessionSettings.IncludeSoftDeletedObjects = true;
			}
			return base.CreateSession();
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0003C580 File Offset: 0x0003A780
		protected override void ResolveLocalSecondaryIdentities()
		{
			bool includeSoftDeletedObjects = base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects;
			try
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = this.SoftDeletedUser;
				base.ResolveLocalSecondaryIdentities();
			}
			finally
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = includeSoftDeletedObjects;
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0003C5E4 File Offset: 0x0003A7E4
		protected override bool ShouldCheckAcceptedDomains()
		{
			return false;
		}
	}
}
