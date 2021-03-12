using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000582 RID: 1410
	[Cmdlet("Add", "GlobalMonitoringOverride", SupportsShouldProcess = true, DefaultParameterSetName = "Duration")]
	public sealed class AddGlobalMonitoringOverride : SingletonSystemConfigurationObjectActionTask<MonitoringOverride>
	{
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000C992B File Offset: 0x000C7B2B
		// (set) Token: 0x0600319A RID: 12698 RVA: 0x000C9942 File Offset: 0x000C7B42
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

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000C9955 File Offset: 0x000C7B55
		// (set) Token: 0x0600319C RID: 12700 RVA: 0x000C996C File Offset: 0x000C7B6C
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

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000C9984 File Offset: 0x000C7B84
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x000C999B File Offset: 0x000C7B9B
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

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000C99AE File Offset: 0x000C7BAE
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x000C99C5 File Offset: 0x000C7BC5
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string PropertyValue
		{
			get
			{
				return (string)base.Fields["PropertyValue"];
			}
			set
			{
				base.Fields["PropertyValue"] = value;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000C99D8 File Offset: 0x000C7BD8
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x000C9A16 File Offset: 0x000C7C16
		[Parameter(Mandatory = false, ParameterSetName = "Duration")]
		public EnhancedTimeSpan? Duration
		{
			get
			{
				if (!base.Fields.Contains("Duration"))
				{
					return null;
				}
				return (EnhancedTimeSpan?)base.Fields["Duration"];
			}
			set
			{
				base.Fields["Duration"] = value;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000C9A2E File Offset: 0x000C7C2E
		// (set) Token: 0x060031A4 RID: 12708 RVA: 0x000C9A45 File Offset: 0x000C7C45
		[Parameter(Mandatory = true, ParameterSetName = "ApplyVersion")]
		public Version ApplyVersion
		{
			get
			{
				return (Version)base.Fields["ApplyVersion"];
			}
			set
			{
				base.Fields["ApplyVersion"] = value;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000C9A58 File Offset: 0x000C7C58
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddMonitoringOverride(this.PropertyName, this.helper.MonitoringItemIdentity, this.ItemType.ToString());
			}
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000C9A80 File Offset: 0x000C7C80
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.helper.ParseAndValidateIdentity(this.Identity, true);
			if (base.Fields.IsModified("ApplyVersion"))
			{
				MonitoringOverrideHelpers.ValidateApplyVersion(this.ApplyVersion);
			}
			if (base.Fields.IsModified("Duration"))
			{
				MonitoringOverrideHelpers.ValidateOverrideDuration(this.Duration);
			}
			else
			{
				this.Duration = new EnhancedTimeSpan?(EnhancedTimeSpan.FromDays(365.0));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000C9B04 File Offset: 0x000C7D04
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			ADObjectId descendantId = base.RootOrgContainerId.GetDescendantId(MonitoringOverride.RdnContainer);
			ITopologyConfigurationSession topologyConfigurationSession = (ITopologyConfigurationSession)base.CreateSession();
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, MonitoringOverride.ContainerName);
			Container[] array = topologyConfigurationSession.Find<Container>(descendantId, QueryScope.SubTree, filter, null, 0);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new FailedToRunGlobalMonitoringOverrideException(MonitoringOverride.ContainerName), ErrorCategory.ObjectNotFound, null);
			}
			this.overridesContainer = array[0];
			Container childContainer = this.overridesContainer.GetChildContainer(this.ItemType.ToString());
			if (childContainer == null)
			{
				base.WriteError(new FailedToRunGlobalMonitoringOverrideException(this.ItemType.ToString()), ErrorCategory.ObjectNotFound, null);
			}
			this.typeContainerId = childContainer.Id;
			Container childContainer2 = childContainer.GetChildContainer(this.helper.HealthSet);
			if (childContainer2 != null)
			{
				this.healthSetContainerId = childContainer2.Id;
				Container childContainer3 = childContainer2.GetChildContainer(this.helper.MonitoringItemName);
				if (childContainer3 != null)
				{
					this.monitoringItemContainerId = childContainer3.Id;
					QueryFilter filter2 = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, this.PropertyName);
					MonitoringOverride[] array2 = topologyConfigurationSession.Find<MonitoringOverride>(this.monitoringItemContainerId, QueryScope.OneLevel, filter2, null, 1);
					if (array2.Length > 0)
					{
						base.WriteError(new PropertyAlreadyHasAnOverrideException(this.PropertyName, this.helper.MonitoringItemIdentity, this.ItemType.ToString()), ErrorCategory.ResourceExists, null);
					}
				}
			}
			TaskLogger.LogExit();
			base.InternalValidate();
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000C9C74 File Offset: 0x000C7E74
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			MonitoringOverride monitoringOverride = new MonitoringOverride();
			if (base.HasErrors)
			{
				return null;
			}
			if (this.healthSetContainerId == null)
			{
				this.healthSetContainerId = this.CreateContainer(this.typeContainerId, this.helper.HealthSet);
			}
			if (this.monitoringItemContainerId == null)
			{
				this.monitoringItemContainerId = this.CreateContainer(this.healthSetContainerId, this.helper.MonitoringItemName);
			}
			monitoringOverride.HealthSet = this.helper.HealthSet;
			monitoringOverride.MonitoringItemName = this.helper.MonitoringItemName;
			monitoringOverride.PropertyValue = this.PropertyValue;
			monitoringOverride.CreatedBy = base.ExecutingUserIdentityName;
			if (base.Fields.IsModified("Duration"))
			{
				monitoringOverride.ExpirationTime = new DateTime?(DateTime.UtcNow.AddSeconds(this.Duration.Value.TotalSeconds));
			}
			if (base.Fields.IsModified("ApplyVersion"))
			{
				monitoringOverride.ApplyVersion = this.ApplyVersion;
			}
			monitoringOverride.SetId(this.monitoringItemContainerId.GetChildId(this.PropertyName));
			TaskLogger.LogExit();
			return monitoringOverride;
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000C9D9C File Offset: 0x000C7F9C
		protected override void InternalProcessRecord()
		{
			this.overridesContainer.EncryptionKey0 = Guid.NewGuid().ToByteArray();
			base.DataSession.Save(this.overridesContainer);
			base.InternalProcessRecord();
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.SuccessAddGlobalMonitoringOverride(this.helper.MonitoringItemIdentity));
			}
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000C9DF6 File Offset: 0x000C7FF6
		protected override bool IsKnownException(Exception exception)
		{
			return exception is InvalidVersionException || exception is InvalidIdentityException || exception is InvalidDurationException || DataAccessHelper.IsDataAccessKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000C9E24 File Offset: 0x000C8024
		private ADObjectId CreateContainer(ADObjectId baseContainer, string coantainerName)
		{
			Container container = new Container();
			container.SetId(baseContainer.GetChildId(coantainerName));
			base.DataSession.Save(container);
			return container.Id;
		}

		// Token: 0x0400232C RID: 9004
		private ADObjectId typeContainerId;

		// Token: 0x0400232D RID: 9005
		private ADObjectId healthSetContainerId;

		// Token: 0x0400232E RID: 9006
		private ADObjectId monitoringItemContainerId;

		// Token: 0x0400232F RID: 9007
		private Container overridesContainer;

		// Token: 0x04002330 RID: 9008
		private MonitoringOverrideHelpers helper = new MonitoringOverrideHelpers();
	}
}
