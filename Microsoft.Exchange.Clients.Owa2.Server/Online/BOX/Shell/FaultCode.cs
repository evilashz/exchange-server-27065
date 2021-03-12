using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x02000078 RID: 120
	[DataContract(Name = "FaultCode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum FaultCode
	{
		// Token: 0x04000213 RID: 531
		[EnumMember]
		InvalidCertificate,
		// Token: 0x04000214 RID: 532
		[EnumMember]
		InvalidHeader,
		// Token: 0x04000215 RID: 533
		[EnumMember]
		InvalidBrandInfo,
		// Token: 0x04000216 RID: 534
		[EnumMember]
		InvalidUserInfo,
		// Token: 0x04000217 RID: 535
		[EnumMember]
		InvalidOptions,
		// Token: 0x04000218 RID: 536
		[EnumMember]
		OperationFailure,
		// Token: 0x04000219 RID: 537
		[EnumMember]
		InvalidWorkloadId,
		// Token: 0x0400021A RID: 538
		[EnumMember]
		InvalidCultureName,
		// Token: 0x0400021B RID: 539
		[EnumMember]
		ParameterNotSupplied,
		// Token: 0x0400021C RID: 540
		[EnumMember]
		InvalidTenantInfo,
		// Token: 0x0400021D RID: 541
		[EnumMember]
		OperationDisabled
	}
}
