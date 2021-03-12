using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200005A RID: 90
	public abstract class GetTenantADObjectWithIdentityTaskBase<TIdentity, TDataObject> : GetObjectWithIdentityTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : IConfigurable, new()
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000E2D1 File Offset: 0x0000C4D1
		internal ADSessionSettings SessionSettings
		{
			get
			{
				return this.sessionSettings;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000E2D9 File Offset: 0x0000C4D9
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000E2F0 File Offset: 0x0000C4F0
		protected virtual AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return (AccountPartitionIdParameter)base.Fields["AccountPartition"];
			}
			set
			{
				base.Fields["AccountPartition"] = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000E304 File Offset: 0x0000C504
		protected override int PageSize
		{
			get
			{
				if (base.InternalResultSize.IsUnlimited)
				{
					return 1000;
				}
				return (int)Math.Min(base.InternalResultSize.Value - base.WriteObjectCount, 1000U);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E348 File Offset: 0x0000C548
		internal override void AdjustPageSize(IPageInformation pageInfo)
		{
			ADGenericPagedReader<TDataObject> adgenericPagedReader = pageInfo as ADGenericPagedReader<TDataObject>;
			if (adgenericPagedReader != null)
			{
				adgenericPagedReader.PageSize = this.PageSize;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E36C File Offset: 0x0000C56C
		protected override void InternalStateReset()
		{
			if (this.AccountPartition != null)
			{
				PartitionId partitionId = RecipientTaskHelper.ResolvePartitionId(this.AccountPartition, new Task.TaskErrorLoggingDelegate(base.WriteError));
				this.sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
			}
			else
			{
				this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
				this.sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			}
			base.InternalStateReset();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000E3E2 File Offset: 0x0000C5E2
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			TaskLogger.LogExit();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E400 File Offset: 0x0000C600
		protected virtual OrganizationId ResolveCurrentOrganization()
		{
			this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E423 File Offset: 0x0000C623
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			this.sessionSettings = null;
		}

		// Token: 0x040000F3 RID: 243
		internal const uint LdapDefaultPageSize = 1000U;

		// Token: 0x040000F4 RID: 244
		private ADSessionSettings sessionSettings;
	}
}
