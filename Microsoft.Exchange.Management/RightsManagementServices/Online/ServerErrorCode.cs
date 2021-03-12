using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x0200073C RID: 1852
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ServerErrorCode", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	public enum ServerErrorCode
	{
		// Token: 0x0400296C RID: 10604
		[EnumMember]
		None,
		// Token: 0x0400296D RID: 10605
		[EnumMember]
		InternalFailure,
		// Token: 0x0400296E RID: 10606
		[EnumMember]
		InvalidArgument,
		// Token: 0x0400296F RID: 10607
		[EnumMember]
		TenantIdNotFound,
		// Token: 0x04002970 RID: 10608
		[EnumMember]
		TenantIsDeprovisioned,
		// Token: 0x04002971 RID: 10609
		[EnumMember]
		TenantIdExist,
		// Token: 0x04002972 RID: 10610
		[EnumMember]
		InvalidClientAuthCertificate,
		// Token: 0x04002973 RID: 10611
		[EnumMember]
		KeySvcCreateOrUpdateFailure,
		// Token: 0x04002974 RID: 10612
		[EnumMember]
		KeySvcCreateTenantKeyFailure,
		// Token: 0x04002975 RID: 10613
		[EnumMember]
		ExportCertificateNotFound,
		// Token: 0x04002976 RID: 10614
		[EnumMember]
		InvalidExportCertificate,
		// Token: 0x04002977 RID: 10615
		[EnumMember]
		UntrustedExportCertificate,
		// Token: 0x04002978 RID: 10616
		[EnumMember]
		OutdatedRequest
	}
}
