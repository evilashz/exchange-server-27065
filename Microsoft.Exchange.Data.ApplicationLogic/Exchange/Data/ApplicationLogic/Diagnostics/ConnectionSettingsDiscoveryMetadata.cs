using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000D7 RID: 215
	internal enum ConnectionSettingsDiscoveryMetadata
	{
		// Token: 0x04000450 RID: 1104
		[DisplayName("CSD", "RDE")]
		RequestDataFromEss,
		// Token: 0x04000451 RID: 1105
		[DisplayName("CSD", "PER")]
		ProcessEssResponse,
		// Token: 0x04000452 RID: 1106
		[DisplayName("CSD", "GOCS")]
		GetOffice365ConnectionSettings,
		// Token: 0x04000453 RID: 1107
		[DisplayName("CSD", "ECSF")]
		EssConnectionSettingsFound,
		// Token: 0x04000454 RID: 1108
		[DisplayName("CSD", "EX")]
		EssException,
		// Token: 0x04000455 RID: 1109
		[DisplayName("CSD", "OCSF")]
		Office365ConnectionSettingsFound,
		// Token: 0x04000456 RID: 1110
		[DisplayName("CSD", "D")]
		Domain
	}
}
