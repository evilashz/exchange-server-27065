using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000CC RID: 204
	internal interface IOrganizationSettingsData
	{
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000BC0 RID: 3008
		DeviceAccessLevel DefaultAccessLevel { get; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000BC1 RID: 3009
		string UserMailInsert { get; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000BC2 RID: 3010
		IList<SmtpAddress> AdminMailRecipients { get; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000BC3 RID: 3011
		bool AllowAccessForUnSupportedPlatform { get; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000BC4 RID: 3012
		bool IsIntuneManaged { get; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000BC5 RID: 3013
		string OtaNotificationMailInsert { get; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000BC6 RID: 3014
		Dictionary<string, ActiveSyncDeviceFilter> DeviceFiltering { get; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000BC7 RID: 3015
		bool IsRulesListEmpty { get; }

		// Token: 0x06000BC8 RID: 3016
		MicrosoftExchangeRecipient GetExchangeRecipient();

		// Token: 0x06000BC9 RID: 3017
		DeviceAccessRuleData EvaluateDevice(DeviceAccessCharacteristic characteristic, string queryString);

		// Token: 0x06000BCA RID: 3018
		void AddOrUpdateDeviceAccessRule(ActiveSyncDeviceAccessRule deviceAccessRule);

		// Token: 0x06000BCB RID: 3019
		void RemoveDeviceAccessRule(string distinguishedName);
	}
}
