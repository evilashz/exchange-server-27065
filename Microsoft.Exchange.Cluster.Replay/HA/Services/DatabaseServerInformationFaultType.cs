using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x02000328 RID: 808
	[DataContract(Name = "DatabaseServerInformationType", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public enum DatabaseServerInformationFaultType
	{
		// Token: 0x04000D6C RID: 3436
		[EnumMember(Value = "TransientError")]
		TransientError = 1000,
		// Token: 0x04000D6D RID: 3437
		[EnumMember(Value = "PermanentError")]
		PermanentError = 2000
	}
}
