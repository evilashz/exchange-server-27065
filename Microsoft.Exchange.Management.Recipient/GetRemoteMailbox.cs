using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000B3 RID: 179
	[Cmdlet("get", "RemoteMailbox", DefaultParameterSetName = "Identity")]
	public sealed class GetRemoteMailbox : GetMailUserBase<RemoteMailboxIdParameter>
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x000302F1 File Offset: 0x0002E4F1
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x000302F9 File Offset: 0x0002E4F9
		[Parameter]
		public OrganizationalUnitIdParameter OnPremisesOrganizationalUnit
		{
			get
			{
				return this.OrganizationalUnit;
			}
			set
			{
				this.OrganizationalUnit = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00030302 File Offset: 0x0002E502
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00030328 File Offset: 0x0002E528
		[Parameter(Mandatory = false)]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00030340 File Offset: 0x0002E540
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<RemoteMailboxSchema>();
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00030348 File Offset: 0x0002E548
		protected override QueryFilter InternalFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.InternalFilter,
					RemoteMailboxIdParameter.GetRemoteMailboxFilter(),
					this.Archive.IsPresent ? GetMailboxOrSyncMailbox.RemoteArchiveFilter : null
				});
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0003038E File Offset: 0x0002E58E
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00030396 File Offset: 0x0002E596
		private new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
			set
			{
				base.OrganizationalUnit = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0003039F File Offset: 0x0002E59F
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x000303A7 File Offset: 0x0002E5A7
		private new OrganizationIdParameter Organization
		{
			get
			{
				return base.Organization;
			}
			set
			{
				base.Organization = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000303B0 File Offset: 0x0002E5B0
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x000303B8 File Offset: 0x0002E5B8
		private new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return base.AccountPartition;
			}
			set
			{
				base.AccountPartition = value;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000303C1 File Offset: 0x0002E5C1
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return RemoteMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000303D0 File Offset: 0x0002E5D0
		protected override void InternalBeginProcessing()
		{
			base.OptionalIdentityData.AdditionalFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				base.OptionalIdentityData.AdditionalFilter,
				this.Archive.IsPresent ? GetMailboxOrSyncMailbox.RemoteArchiveFilter : null
			});
			base.InternalBeginProcessing();
		}
	}
}
