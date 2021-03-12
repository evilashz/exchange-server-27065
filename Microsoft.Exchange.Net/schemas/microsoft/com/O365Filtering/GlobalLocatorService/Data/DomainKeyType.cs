using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C41 RID: 3137
	[Flags]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainKeyType", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public enum DomainKeyType
	{
		// Token: 0x04003A1E RID: 14878
		[EnumMember]
		NotDefined = 0,
		// Token: 0x04003A1F RID: 14879
		[EnumMember]
		InitialDomain = 1,
		// Token: 0x04003A20 RID: 14880
		[EnumMember]
		CustomDomain = 2,
		// Token: 0x04003A21 RID: 14881
		[EnumMember]
		UseExisting = 3
	}
}
