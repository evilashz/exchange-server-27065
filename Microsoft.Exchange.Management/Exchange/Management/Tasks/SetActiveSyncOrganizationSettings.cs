using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000083 RID: 131
	[Cmdlet("Set", "ActiveSyncOrganizationSettings", SupportsShouldProcess = true, DefaultParameterSetName = "Default")]
	public sealed class SetActiveSyncOrganizationSettings : SetSystemConfigurationObjectTask<ActiveSyncOrganizationSettingsIdParameter, ActiveSyncOrganizationSettings>
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00010802 File Offset: 0x0000EA02
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00010819 File Offset: 0x0000EA19
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetRemoveDeviceFilter", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetRemoveDeviceFilterRule", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRule", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRuleForAllDevices", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = false, ParameterSetName = "Default", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override ActiveSyncOrganizationSettingsIdParameter Identity
		{
			get
			{
				return (ActiveSyncOrganizationSettingsIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001082C File Offset: 0x0000EA2C
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0001084D File Offset: 0x0000EA4D
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRule")]
		public SwitchParameter AddDeviceFilterRule
		{
			get
			{
				return (SwitchParameter)(base.Fields["ParameterSetAddDeviceFilterRule"] ?? false);
			}
			set
			{
				base.Fields["ParameterSetAddDeviceFilterRule"] = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00010865 File Offset: 0x0000EA65
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00010886 File Offset: 0x0000EA86
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetRemoveDeviceFilterRule")]
		public SwitchParameter RemoveDeviceFilterRule
		{
			get
			{
				return (SwitchParameter)(base.Fields["ParameterSetRemoveDeviceFilterRule"] ?? false);
			}
			set
			{
				base.Fields["ParameterSetRemoveDeviceFilterRule"] = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001089E File Offset: 0x0000EA9E
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x000108BF File Offset: 0x0000EABF
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetRemoveDeviceFilter")]
		public SwitchParameter RemoveDeviceFilter
		{
			get
			{
				return (SwitchParameter)(base.Fields["ParameterSetRemoveDeviceFilter"] ?? false);
			}
			set
			{
				base.Fields["ParameterSetRemoveDeviceFilter"] = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000108D7 File Offset: 0x0000EAD7
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x000108F8 File Offset: 0x0000EAF8
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRuleForAllDevices")]
		public SwitchParameter AddDeviceFilterRuleForAllDevices
		{
			get
			{
				return (SwitchParameter)(base.Fields["ParameterSetAddDeviceFilterRuleForAllDevices"] ?? false);
			}
			set
			{
				base.Fields["ParameterSetAddDeviceFilterRuleForAllDevices"] = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00010910 File Offset: 0x0000EB10
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00010927 File Offset: 0x0000EB27
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetRemoveDeviceFilter")]
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetRemoveDeviceFilterRule")]
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRuleForAllDevices")]
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRule")]
		public string DeviceFilterName
		{
			get
			{
				return (string)base.Fields["DeviceFilterName"];
			}
			set
			{
				base.Fields["DeviceFilterName"] = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001093A File Offset: 0x0000EB3A
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00010951 File Offset: 0x0000EB51
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRule")]
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetRemoveDeviceFilterRule")]
		public string DeviceFilterRuleName
		{
			get
			{
				return (string)base.Fields["DeviceFilterRuleName"];
			}
			set
			{
				base.Fields["DeviceFilterRuleName"] = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00010964 File Offset: 0x0000EB64
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0001097B File Offset: 0x0000EB7B
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetRemoveDeviceFilterRule")]
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRule")]
		public DeviceAccessCharacteristic DeviceFilterCharacteristic
		{
			get
			{
				return (DeviceAccessCharacteristic)base.Fields["DeviceFilterCharacteristic"];
			}
			set
			{
				base.Fields["DeviceFilterCharacteristic"] = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00010993 File Offset: 0x0000EB93
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x000109AA File Offset: 0x0000EBAA
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRule")]
		public DeviceFilterOperator DeviceFilterOperator
		{
			get
			{
				return (DeviceFilterOperator)base.Fields["DeviceFilterOperator"];
			}
			set
			{
				base.Fields["DeviceFilterOperator"] = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x000109C2 File Offset: 0x0000EBC2
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x000109D9 File Offset: 0x0000EBD9
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetAddDeviceFilterRule")]
		public string DeviceFilterValue
		{
			get
			{
				return (string)base.Fields["DeviceFilterValue"];
			}
			set
			{
				base.Fields["DeviceFilterValue"] = value;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000109EC File Offset: 0x0000EBEC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (Datacenter.IsMultiTenancyEnabled() && this.DataObject.OrganizationId == OrganizationId.ForestWideOrgId && this.DataObject.DefaultAccessLevel != DeviceAccessLevel.Allow)
			{
				base.WriteError(new ArgumentException(Strings.ErrorOnlyForestWideAllowIsAllowed), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00010A41 File Offset: 0x0000EC41
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetActiveSyncOrganizationSettings;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010A48 File Offset: 0x0000EC48
		protected override IConfigurable ResolveDataObject()
		{
			if (this.Identity == null)
			{
				IConfigurable[] array = null;
				try
				{
					array = base.DataSession.Find<ActiveSyncOrganizationSettings>(null, this.RootId, false, null);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, (ErrorCategory)1002, null);
				}
				if (array == null)
				{
					array = new IConfigurable[0];
				}
				IConfigurable result = null;
				switch (array.Length)
				{
				case 0:
					base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(null, typeof(ActiveSyncOrganizationSettings).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, null);
					break;
				case 1:
					result = array[0];
					break;
				default:
					base.WriteError(new ManagementObjectAmbiguousException(Strings.ActiveSyncOrganizationSettingsAmbiguous), (ErrorCategory)1003, null);
					break;
				}
				return result;
			}
			return base.ResolveDataObject();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00010B1C File Offset: 0x0000ED1C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ActiveSyncOrganizationSettings activeSyncOrganizationSettings = (ActiveSyncOrganizationSettings)base.PrepareDataObject();
			if (base.Fields.IsModified("ParameterSetAddDeviceFilterRule") && this.AddDeviceFilterRule)
			{
				this.ValidateDeviceFilterNameParameter();
				this.ValidateDeviceFilterRuleNameParameter();
				ActiveSyncDeviceFilterRule newRule = new ActiveSyncDeviceFilterRule(this.DeviceFilterRuleName, this.DeviceFilterCharacteristic, this.DeviceFilterOperator, this.DeviceFilterValue);
				activeSyncOrganizationSettings.DeviceFiltering = this.AddDeviceFilterRuleToExisting(activeSyncOrganizationSettings.DeviceFiltering, this.DeviceFilterName, newRule, false);
			}
			else if (base.Fields.IsModified("ParameterSetAddDeviceFilterRuleForAllDevices") && this.AddDeviceFilterRuleForAllDevices)
			{
				this.ValidateDeviceFilterNameParameter();
				activeSyncOrganizationSettings.DeviceFiltering = this.AddDeviceFilterRuleToExisting(activeSyncOrganizationSettings.DeviceFiltering, this.DeviceFilterName, null, true);
			}
			else if (base.Fields.IsModified("ParameterSetRemoveDeviceFilterRule") && this.RemoveDeviceFilterRule)
			{
				this.ValidateDeviceFilterNameParameter();
				this.ValidateDeviceFilterRuleNameParameter();
				activeSyncOrganizationSettings.DeviceFiltering = this.RemoveDeviceFilterRuleFromExisting(activeSyncOrganizationSettings.DeviceFiltering, this.DeviceFilterName, this.DeviceFilterRuleName, this.DeviceFilterCharacteristic);
			}
			else if (base.Fields.IsModified("ParameterSetRemoveDeviceFilter") && this.RemoveDeviceFilter)
			{
				this.ValidateDeviceFilterNameParameter();
				activeSyncOrganizationSettings.DeviceFiltering = this.RemoveDeviceFilterFromExisting(activeSyncOrganizationSettings.DeviceFiltering, this.DeviceFilterName);
			}
			TaskLogger.LogExit();
			return activeSyncOrganizationSettings;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010C7A File Offset: 0x0000EE7A
		private void ValidateDeviceFilterNameParameter()
		{
			if (string.IsNullOrEmpty(this.DeviceFilterName))
			{
				base.WriteError(new ArgumentException(Strings.NullOrEmptyStringNotAllowed("DeviceFilterName")), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010CA5 File Offset: 0x0000EEA5
		private void ValidateDeviceFilterRuleNameParameter()
		{
			if (string.IsNullOrEmpty(this.DeviceFilterRuleName))
			{
				base.WriteError(new ArgumentException(Strings.NullOrEmptyStringNotAllowed("DeviceFilterRuleName")), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00010D1C File Offset: 0x0000EF1C
		private ActiveSyncDeviceFilterArray AddDeviceFilterRuleToExisting(ActiveSyncDeviceFilterArray existingDeviceFilterArray, string deviceFilterName, ActiveSyncDeviceFilterRule newRule, bool applyFilterForAllDevices)
		{
			ActiveSyncDeviceFilterArray activeSyncDeviceFilterArray = new ActiveSyncDeviceFilterArray();
			if (existingDeviceFilterArray != null)
			{
				activeSyncDeviceFilterArray.DeviceFilters = existingDeviceFilterArray.DeviceFilters;
			}
			List<ActiveSyncDeviceFilter> list = activeSyncDeviceFilterArray.DeviceFilters;
			if (list == null)
			{
				list = new List<ActiveSyncDeviceFilter>();
				activeSyncDeviceFilterArray.DeviceFilters = list;
			}
			ActiveSyncDeviceFilter activeSyncDeviceFilter = list.FirstOrDefault((ActiveSyncDeviceFilter existingDeviceFilter) => existingDeviceFilter.Name.Equals(deviceFilterName, StringComparison.InvariantCultureIgnoreCase));
			if (activeSyncDeviceFilter == null)
			{
				activeSyncDeviceFilter = new ActiveSyncDeviceFilter(deviceFilterName, new List<ActiveSyncDeviceFilterRule>());
				list.Add(activeSyncDeviceFilter);
			}
			if (applyFilterForAllDevices)
			{
				activeSyncDeviceFilter.ApplyForAllDevices = applyFilterForAllDevices;
				base.WriteVerbose(Strings.AddedDeviceFilterRule(deviceFilterName));
				return activeSyncDeviceFilterArray;
			}
			if (activeSyncDeviceFilter.ApplyForAllDevices)
			{
				base.WriteError(new InvalidOperationException(Strings.CantAddDeviceFilterRuleSinceApplyForAllDevicesSetToTrue(deviceFilterName)), ErrorCategory.InvalidOperation, null);
			}
			List<ActiveSyncDeviceFilterRule> rules = activeSyncDeviceFilter.Rules;
			if (!rules.Any((ActiveSyncDeviceFilterRule rule) => rule.Characteristic == newRule.Characteristic && rule.Name.Equals(newRule.Name, StringComparison.InvariantCultureIgnoreCase)))
			{
				rules.Add(newRule);
				base.WriteVerbose(Strings.AddedDeviceFilterRule(deviceFilterName));
			}
			else
			{
				base.WriteError(new ArgumentException(Strings.DeviceFilterRuleAlreadyExists(deviceFilterName)), ErrorCategory.InvalidArgument, null);
			}
			return activeSyncDeviceFilterArray;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00010E84 File Offset: 0x0000F084
		private ActiveSyncDeviceFilterArray RemoveDeviceFilterRuleFromExisting(ActiveSyncDeviceFilterArray existingDeviceFilterArray, string deviceFilterName, string deviceFilterRuleName, DeviceAccessCharacteristic deviceFilterCharacteristic)
		{
			ActiveSyncDeviceFilterArray activeSyncDeviceFilterArray = new ActiveSyncDeviceFilterArray();
			if (existingDeviceFilterArray == null || existingDeviceFilterArray.DeviceFilters == null)
			{
				base.WriteVerbose(new LocalizedString("There are no device filters to remove"));
				return existingDeviceFilterArray;
			}
			activeSyncDeviceFilterArray.DeviceFilters = existingDeviceFilterArray.DeviceFilters;
			ActiveSyncDeviceFilter activeSyncDeviceFilter = activeSyncDeviceFilterArray.DeviceFilters.FirstOrDefault((ActiveSyncDeviceFilter existingDeviceFilter) => existingDeviceFilter.Name.Equals(deviceFilterName, StringComparison.InvariantCultureIgnoreCase));
			if (activeSyncDeviceFilter == null || activeSyncDeviceFilter.Rules == null)
			{
				base.WriteError(new ArgumentException(Strings.NoDeviceFilterRuleByName(deviceFilterName)), ErrorCategory.InvalidArgument, null);
			}
			List<ActiveSyncDeviceFilterRule> rules = activeSyncDeviceFilter.Rules;
			int num = rules.FindIndex((ActiveSyncDeviceFilterRule rule) => rule.Characteristic == deviceFilterCharacteristic && rule.Name.Equals(deviceFilterRuleName, StringComparison.InvariantCultureIgnoreCase));
			if (num >= 0)
			{
				rules.RemoveAt(num);
				base.WriteVerbose(Strings.RemovedDeviceFilterRuleByNameAndCharacteristic(deviceFilterName, deviceFilterRuleName, deviceFilterCharacteristic.ToString()));
			}
			else
			{
				base.WriteVerbose(Strings.NoDeviceFilterRuleByNameAndCharacteristic(deviceFilterName, deviceFilterRuleName, deviceFilterCharacteristic.ToString()));
			}
			if (activeSyncDeviceFilter.Rules.Count == 0)
			{
				base.WriteVerbose(Strings.EmptyDeviceFilterRemoved(deviceFilterName));
				activeSyncDeviceFilterArray.DeviceFilters.Remove(activeSyncDeviceFilter);
			}
			return activeSyncDeviceFilterArray;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		private ActiveSyncDeviceFilterArray RemoveDeviceFilterFromExisting(ActiveSyncDeviceFilterArray existingDeviceFilterArray, string deviceFilterName)
		{
			ActiveSyncDeviceFilterArray activeSyncDeviceFilterArray = new ActiveSyncDeviceFilterArray();
			if (existingDeviceFilterArray == null || existingDeviceFilterArray.DeviceFilters == null)
			{
				base.WriteVerbose(Strings.NoDeviceFilters);
				return existingDeviceFilterArray;
			}
			activeSyncDeviceFilterArray.DeviceFilters = existingDeviceFilterArray.DeviceFilters;
			int num = activeSyncDeviceFilterArray.DeviceFilters.RemoveAll((ActiveSyncDeviceFilter existingDeviceFilter) => existingDeviceFilter.Name.Equals(deviceFilterName, StringComparison.InvariantCultureIgnoreCase));
			if (num > 0)
			{
				base.WriteVerbose(Strings.RemovedDeviceFilter(deviceFilterName));
			}
			else
			{
				base.WriteVerbose(Strings.NoDeviceFilterByName(deviceFilterName));
			}
			return activeSyncDeviceFilterArray;
		}

		// Token: 0x04000217 RID: 535
		private const string ParameterSetAddDeviceFilterRule = "ParameterSetAddDeviceFilterRule";

		// Token: 0x04000218 RID: 536
		private const string ParameterSetAddDeviceFilterRuleForAllDevices = "ParameterSetAddDeviceFilterRuleForAllDevices";

		// Token: 0x04000219 RID: 537
		private const string ParameterSetRemoveDeviceFilterRule = "ParameterSetRemoveDeviceFilterRule";

		// Token: 0x0400021A RID: 538
		private const string ParameterSetRemoveDeviceFilter = "ParameterSetRemoveDeviceFilter";

		// Token: 0x0400021B RID: 539
		private const string DeviceFilterNameParameter = "DeviceFilterName";

		// Token: 0x0400021C RID: 540
		private const string DeviceFilterRuleNameParameter = "DeviceFilterRuleName";

		// Token: 0x0400021D RID: 541
		private const string DeviceFilterCharacteristicParameter = "DeviceFilterCharacteristic";

		// Token: 0x0400021E RID: 542
		private const string DeviceFilterOperatorParameter = "DeviceFilterOperator";

		// Token: 0x0400021F RID: 543
		private const string DeviceFilterValueParameter = "DeviceFilterValue";
	}
}
