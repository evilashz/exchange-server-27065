using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020005BA RID: 1466
	[Cmdlet("Remove", "GlobalMonitoringOverride", SupportsShouldProcess = true)]
	public sealed class RemoveGlobalMonitoringOverride : SingletonSystemConfigurationObjectActionTask<MonitoringOverride>
	{
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x000D0EEF File Offset: 0x000CF0EF
		// (set) Token: 0x0600336C RID: 13164 RVA: 0x000D0F06 File Offset: 0x000CF106
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, Position = 0)]
		public string Identity
		{
			get
			{
				return (string)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000D0F19 File Offset: 0x000CF119
		// (set) Token: 0x0600336E RID: 13166 RVA: 0x000D0F30 File Offset: 0x000CF130
		[Parameter(Mandatory = true)]
		public MonitoringItemTypeEnum ItemType
		{
			get
			{
				return (MonitoringItemTypeEnum)base.Fields["ItemType"];
			}
			set
			{
				base.Fields["ItemType"] = value;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000D0F48 File Offset: 0x000CF148
		// (set) Token: 0x06003370 RID: 13168 RVA: 0x000D0F5F File Offset: 0x000CF15F
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string PropertyName
		{
			get
			{
				return (string)base.Fields["PropertyName"];
			}
			set
			{
				base.Fields["PropertyName"] = value;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000D0F72 File Offset: 0x000CF172
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMonitoringOverride(this.PropertyName, this.helper.MonitoringItemIdentity, this.ItemType.ToString());
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000D0F9C File Offset: 0x000CF19C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.helper.ParseAndValidateIdentity(this.Identity, true);
			this.configSession = (ITopologyConfigurationSession)base.CreateSession();
			ADObjectId descendantId = base.RootOrgContainerId.GetDescendantId(MonitoringOverride.RdnContainer);
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, MonitoringOverride.ContainerName);
			Container[] array = this.configSession.Find<Container>(descendantId, QueryScope.SubTree, filter, null, 0);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new FailedToRunGlobalMonitoringOverrideException(MonitoringOverride.ContainerName), ErrorCategory.ObjectNotFound, null);
			}
			this.overridesContainer = array[0];
			Container container = this.GetContainer(this.overridesContainer, this.ItemType.ToString());
			this.healthSetContainer = this.GetContainer(container, this.helper.HealthSet);
			this.monitoringItemContainer = this.GetContainer(this.healthSetContainer, this.helper.MonitoringItemName);
			QueryFilter filter2 = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, this.PropertyName);
			this.monitoringItem = this.GetChildren(this.monitoringItemContainer.Id, filter2)[0];
			TaskLogger.LogExit();
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000D10B0 File Offset: 0x000CF2B0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			IConfigurable[] children = this.GetChildren(this.monitoringItemContainer.Id, null);
			if (children.Length == 1)
			{
				IConfigurable[] children2 = this.GetChildren(this.healthSetContainer.Id, null);
				if (children2.Length == 1)
				{
					this.configSession.DeleteTree(this.healthSetContainer, null);
				}
				else
				{
					this.configSession.DeleteTree(this.monitoringItemContainer, null);
				}
			}
			else
			{
				base.DataSession.Delete(this.monitoringItem);
			}
			this.overridesContainer.EncryptionKey0 = Guid.NewGuid().ToByteArray();
			base.DataSession.Save(this.overridesContainer);
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.SuccessRemoveGlobalMonitoringOverride(this.helper.MonitoringItemIdentity));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000D1179 File Offset: 0x000CF379
		protected override bool IsKnownException(Exception exception)
		{
			return exception is InvalidIdentityException || DataAccessHelper.IsDataAccessKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000D1194 File Offset: 0x000CF394
		private Container GetContainer(Container parentContainer, string containerName)
		{
			Container childContainer = parentContainer.GetChildContainer(containerName);
			if (childContainer == null)
			{
				base.WriteError(new FailedToRunGlobalMonitoringOverrideException(containerName), ErrorCategory.ObjectNotFound, null);
			}
			return childContainer;
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000D11BC File Offset: 0x000CF3BC
		private IConfigurable[] GetChildren(ADObjectId containerId, QueryFilter filter)
		{
			IConfigurable[] array = this.configSession.Find<MonitoringOverride>(containerId, QueryScope.SubTree, filter, null, 0);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new OverrideNotFoundException(this.helper.MonitoringItemIdentity, this.ItemType.ToString(), this.PropertyName), ErrorCategory.ObjectNotFound, null);
			}
			return array;
		}

		// Token: 0x040023D0 RID: 9168
		private ITopologyConfigurationSession configSession;

		// Token: 0x040023D1 RID: 9169
		private IConfigurable monitoringItem;

		// Token: 0x040023D2 RID: 9170
		private Container overridesContainer;

		// Token: 0x040023D3 RID: 9171
		private Container healthSetContainer;

		// Token: 0x040023D4 RID: 9172
		private Container monitoringItemContainer;

		// Token: 0x040023D5 RID: 9173
		private MonitoringOverrideHelpers helper = new MonitoringOverrideHelpers();
	}
}
