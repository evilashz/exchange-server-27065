using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200096B RID: 2411
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[Serializable]
	public enum UpdateResultCode
	{
		// Token: 0x04004947 RID: 18759
		Success,
		// Token: 0x04004948 RID: 18760
		PartitionUnavailable,
		// Token: 0x04004949 RID: 18761
		ObjectNotFound,
		// Token: 0x0400494A RID: 18762
		ProvisionedPlanPublishNotAllowed,
		// Token: 0x0400494B RID: 18763
		ServiceInfoVersionOutdated,
		// Token: 0x0400494C RID: 18764
		ChangeSubscriptionUpdateFailed,
		// Token: 0x0400494D RID: 18765
		UnspecifiedError,
		// Token: 0x0400494E RID: 18766
		ValidationErrorPublishNotAllowed,
		// Token: 0x0400494F RID: 18767
		CloudSipProxyAddressPublishNotAllowed
	}
}
