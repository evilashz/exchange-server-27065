using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Contract
{
	// Token: 0x0200000E RID: 14
	internal static class DataConstants
	{
		// Token: 0x04000015 RID: 21
		public const string Namespace = "http://schemas.microsoft.com/informationprotection/computefabric";

		// Token: 0x04000016 RID: 22
		public const string ComplianceEndpointNetTcpAddressFormat = "net.tcp://{0}/complianceservice";

		// Token: 0x04000017 RID: 23
		public const string ComplianceEndpointHttpsAddressFormat = "https://{0}/complianceservice";

		// Token: 0x04000018 RID: 24
		public const int MaxReceivedMessageSize = 524288;

		// Token: 0x04000019 RID: 25
		public const int MaxBufferPoolSize = 1048576;
	}
}
