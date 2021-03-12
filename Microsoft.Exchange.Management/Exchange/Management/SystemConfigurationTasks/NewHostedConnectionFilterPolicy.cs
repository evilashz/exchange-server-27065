using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A3D RID: 2621
	[Cmdlet("New", "HostedConnectionFilterPolicy", SupportsShouldProcess = true)]
	public sealed class NewHostedConnectionFilterPolicy : NewMultitenancySystemConfigurationObjectTask<HostedConnectionFilterPolicy>
	{
		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x06005D8B RID: 23947 RVA: 0x00189CBD File Offset: 0x00187EBD
		// (set) Token: 0x06005D8C RID: 23948 RVA: 0x00189CC5 File Offset: 0x00187EC5
		[Parameter]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x06005D8D RID: 23949 RVA: 0x00189CCE File Offset: 0x00187ECE
		// (set) Token: 0x06005D8E RID: 23950 RVA: 0x00189CD6 File Offset: 0x00187ED6
		[Parameter(Mandatory = true, Position = 0)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x00189CDF File Offset: 0x00187EDF
		// (set) Token: 0x06005D90 RID: 23952 RVA: 0x00189CEC File Offset: 0x00187EEC
		[Parameter]
		public string AdminDisplayName
		{
			get
			{
				return this.DataObject.AdminDisplayName;
			}
			set
			{
				this.DataObject.AdminDisplayName = value;
			}
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x00189CFA File Offset: 0x00187EFA
		// (set) Token: 0x06005D92 RID: 23954 RVA: 0x00189D07 File Offset: 0x00187F07
		[Parameter]
		public MultiValuedProperty<IPRange> IPAllowList
		{
			get
			{
				return this.DataObject.IPAllowList;
			}
			set
			{
				this.DataObject.IPAllowList = value;
			}
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x06005D93 RID: 23955 RVA: 0x00189D15 File Offset: 0x00187F15
		// (set) Token: 0x06005D94 RID: 23956 RVA: 0x00189D22 File Offset: 0x00187F22
		[Parameter]
		public MultiValuedProperty<IPRange> IPBlockList
		{
			get
			{
				return this.DataObject.IPBlockList;
			}
			set
			{
				this.DataObject.IPBlockList = value;
			}
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x06005D95 RID: 23957 RVA: 0x00189D30 File Offset: 0x00187F30
		// (set) Token: 0x06005D96 RID: 23958 RVA: 0x00189D3D File Offset: 0x00187F3D
		[Parameter]
		public bool EnableSafeList
		{
			get
			{
				return this.DataObject.EnableSafeList;
			}
			set
			{
				this.DataObject.EnableSafeList = value;
			}
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x06005D97 RID: 23959 RVA: 0x00189D4B File Offset: 0x00187F4B
		// (set) Token: 0x06005D98 RID: 23960 RVA: 0x00189D58 File Offset: 0x00187F58
		[Parameter]
		public DirectoryBasedEdgeBlockMode DirectoryBasedEdgeBlockMode
		{
			get
			{
				return this.DataObject.DirectoryBasedEdgeBlockMode;
			}
			set
			{
				this.DataObject.DirectoryBasedEdgeBlockMode = value;
			}
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x06005D99 RID: 23961 RVA: 0x00189D66 File Offset: 0x00187F66
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewHostedConnectionFilterPolicy(this.Name);
			}
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x06005D9A RID: 23962 RVA: 0x00189D73 File Offset: 0x00187F73
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x00189D88 File Offset: 0x00187F88
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			HostedConnectionFilterPolicy hostedConnectionFilterPolicy = (HostedConnectionFilterPolicy)base.PrepareDataObject();
			hostedConnectionFilterPolicy.SetId((IConfigurationSession)base.DataSession, this.Name);
			if (!NewHostedConnectionFilterPolicy.HostedConnectionFilterPolicyExist((IConfigurationSession)base.DataSession, null))
			{
				this.DataObject.IsDefault = true;
			}
			TaskLogger.LogExit();
			return hostedConnectionFilterPolicy;
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x00189DE4 File Offset: 0x00187FE4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.CreateParentContainerIfNeeded(this.DataObject);
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<HostedConnectionFilterPolicy>(this, this.DataObject, null);
			TaskLogger.LogExit();
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x00189E54 File Offset: 0x00188054
		private static bool HostedConnectionFilterPolicyExist(IConfigurationSession session, QueryFilter filter)
		{
			HostedConnectionFilterPolicy[] array = session.Find<HostedConnectionFilterPolicy>(null, QueryScope.SubTree, filter, null, 1);
			return array.Length != 0;
		}
	}
}
