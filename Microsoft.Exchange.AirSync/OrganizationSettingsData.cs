using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000F7 RID: 247
	internal class OrganizationSettingsData : IOrganizationSettingsData
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x0004AA24 File Offset: 0x00048C24
		public OrganizationSettingsData(ActiveSyncOrganizationSettings organizationSettings, IConfigurationSession scopedSession)
		{
			if (scopedSession == null)
			{
				throw new ArgumentNullException("scopedSession");
			}
			if (organizationSettings == null)
			{
				throw new ArgumentNullException("organizationSettings");
			}
			this.Identity = organizationSettings.OriginalId;
			this.defaultAccessLevel = organizationSettings.DefaultAccessLevel;
			this.userMailInsert = organizationSettings.UserMailInsert;
			this.allowAccessForUnSupportedPlatform = organizationSettings.AllowAccessForUnSupportedPlatform;
			this.adminMailRecipients = organizationSettings.AdminMailRecipients;
			this.otaNotificationMailInsert = organizationSettings.OtaNotificationMailInsert;
			this.deviceFiltering = null;
			this.IsIntuneManaged = organizationSettings.IsIntuneManaged;
			if (organizationSettings.DeviceFiltering != null && organizationSettings.DeviceFiltering.DeviceFilters != null)
			{
				this.deviceFiltering = organizationSettings.DeviceFiltering.DeviceFilters.ToDictionary((ActiveSyncDeviceFilter deviceFilter) => deviceFilter.Name);
			}
			ADPagedReader<ActiveSyncDeviceAccessRule> adpagedReader = scopedSession.FindPaged<ActiveSyncDeviceAccessRule>(organizationSettings.Id, QueryScope.OneLevel, null, null, 0);
			foreach (ActiveSyncDeviceAccessRule deviceAccessRule in adpagedReader)
			{
				((IOrganizationSettingsData)this).AddOrUpdateDeviceAccessRule(deviceAccessRule);
			}
			this.organizationId = organizationSettings.OrganizationId;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0004AB60 File Offset: 0x00048D60
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x0004AB68 File Offset: 0x00048D68
		public ADObjectId Identity { get; private set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0004AB71 File Offset: 0x00048D71
		DeviceAccessLevel IOrganizationSettingsData.DefaultAccessLevel
		{
			get
			{
				return this.defaultAccessLevel;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0004AB79 File Offset: 0x00048D79
		string IOrganizationSettingsData.UserMailInsert
		{
			get
			{
				return this.userMailInsert;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0004AB81 File Offset: 0x00048D81
		bool IOrganizationSettingsData.AllowAccessForUnSupportedPlatform
		{
			get
			{
				return this.allowAccessForUnSupportedPlatform;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0004AB89 File Offset: 0x00048D89
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x0004AB91 File Offset: 0x00048D91
		public bool IsIntuneManaged { get; private set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0004AB9A File Offset: 0x00048D9A
		IList<SmtpAddress> IOrganizationSettingsData.AdminMailRecipients
		{
			get
			{
				return this.adminMailRecipients;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0004ABA2 File Offset: 0x00048DA2
		string IOrganizationSettingsData.OtaNotificationMailInsert
		{
			get
			{
				return this.otaNotificationMailInsert;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0004ABAA File Offset: 0x00048DAA
		Dictionary<string, ActiveSyncDeviceFilter> IOrganizationSettingsData.DeviceFiltering
		{
			get
			{
				return this.deviceFiltering;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0004ABB4 File Offset: 0x00048DB4
		bool IOrganizationSettingsData.IsRulesListEmpty
		{
			get
			{
				foreach (List<DeviceAccessRuleData> list in this.deviceAccessRules)
				{
					if (list != null && list.Count > 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0004ABF0 File Offset: 0x00048DF0
		MicrosoftExchangeRecipient IOrganizationSettingsData.GetExchangeRecipient()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId), 220, "GetExchangeRecipient", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\OrganizationSettingsData.cs");
			return tenantOrTopologyConfigurationSession.FindMicrosoftExchangeRecipient();
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0004AC2C File Offset: 0x00048E2C
		DeviceAccessRuleData IOrganizationSettingsData.EvaluateDevice(DeviceAccessCharacteristic characteristic, string queryString)
		{
			List<DeviceAccessRuleData> list = this.deviceAccessRules[(int)characteristic];
			if (list == null)
			{
				return null;
			}
			foreach (DeviceAccessRuleData deviceAccessRuleData in list)
			{
				if (string.Equals(deviceAccessRuleData.QueryString, queryString, StringComparison.OrdinalIgnoreCase))
				{
					return deviceAccessRuleData;
				}
			}
			return null;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0004AC98 File Offset: 0x00048E98
		void IOrganizationSettingsData.AddOrUpdateDeviceAccessRule(ActiveSyncDeviceAccessRule deviceAccessRule)
		{
			if (this.deviceAccessRules[(int)deviceAccessRule.Characteristic] == null)
			{
				this.deviceAccessRules[(int)deviceAccessRule.Characteristic] = new List<DeviceAccessRuleData>(10);
			}
			List<DeviceAccessRuleData> list = this.deviceAccessRules[(int)deviceAccessRule.Characteristic];
			DeviceAccessRuleData deviceAccessRuleData = new DeviceAccessRuleData(deviceAccessRule);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Identity.Equals(deviceAccessRuleData.Identity))
				{
					AirSyncDiagnostics.TraceInfo<DeviceAccessRuleData, ADObjectId>(ExTraceGlobals.RequestsTracer, this, "Updating rule {0} for Org {1} cache.", deviceAccessRuleData, this.Identity);
					list[i] = deviceAccessRuleData;
					return;
				}
			}
			AirSyncDiagnostics.TraceInfo<DeviceAccessRuleData, ADObjectId>(ExTraceGlobals.RequestsTracer, this, "Adding new rule {0} for Org {1} cache.", deviceAccessRuleData, this.Identity);
			list.Add(deviceAccessRuleData);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0004AD48 File Offset: 0x00048F48
		void IOrganizationSettingsData.RemoveDeviceAccessRule(string distinguishedName)
		{
			for (int i = 0; i < this.deviceAccessRules.Length; i++)
			{
				List<DeviceAccessRuleData> list = this.deviceAccessRules[i];
				if (list != null && list.Count > 0)
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j].Identity.Equals(distinguishedName))
						{
							AirSyncDiagnostics.TraceInfo<ADObjectId, ADObjectId>(ExTraceGlobals.RequestsTracer, this, "Removing rule {0} from Org {1} cache.", list[j].Identity, this.Identity);
							list.RemoveAt(j);
							return;
						}
					}
				}
			}
			AirSyncDiagnostics.TraceError<ADObjectId, string>(ExTraceGlobals.RequestsTracer, this, "Trying to remove a rule not in this Organization. OrganizationId: {0}, rule: {1}", this.Identity, distinguishedName);
		}

		// Token: 0x0400087F RID: 2175
		private static readonly int numberOfDeviceAccessCharacteristics = Enum.GetValues(typeof(DeviceAccessCharacteristic)).Length;

		// Token: 0x04000880 RID: 2176
		private List<DeviceAccessRuleData>[] deviceAccessRules = new List<DeviceAccessRuleData>[OrganizationSettingsData.numberOfDeviceAccessCharacteristics];

		// Token: 0x04000881 RID: 2177
		private OrganizationId organizationId;

		// Token: 0x04000882 RID: 2178
		private readonly DeviceAccessLevel defaultAccessLevel;

		// Token: 0x04000883 RID: 2179
		private readonly string userMailInsert;

		// Token: 0x04000884 RID: 2180
		private readonly bool allowAccessForUnSupportedPlatform;

		// Token: 0x04000885 RID: 2181
		private readonly IList<SmtpAddress> adminMailRecipients;

		// Token: 0x04000886 RID: 2182
		private readonly string otaNotificationMailInsert;

		// Token: 0x04000887 RID: 2183
		private readonly Dictionary<string, ActiveSyncDeviceFilter> deviceFiltering;
	}
}
