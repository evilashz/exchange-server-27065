using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x0200006A RID: 106
	[Flags]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Filters", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	public enum Filters
	{
		// Token: 0x040001B0 RID: 432
		[EnumMember]
		None = 0,
		// Token: 0x040001B1 RID: 433
		[EnumMember]
		Images = 1,
		// Token: 0x040001B2 RID: 434
		[EnumMember]
		StaticText = 2,
		// Token: 0x040001B3 RID: 435
		[EnumMember]
		UserSpecificText = 4,
		// Token: 0x040001B4 RID: 436
		[EnumMember]
		Text = 6,
		// Token: 0x040001B5 RID: 437
		[EnumMember]
		StaticLinkUrls = 8,
		// Token: 0x040001B6 RID: 438
		[EnumMember]
		UserSpecificLinkUrls = 16,
		// Token: 0x040001B7 RID: 439
		[EnumMember]
		LinkUrls = 24,
		// Token: 0x040001B8 RID: 440
		[EnumMember]
		Css = 32,
		// Token: 0x040001B9 RID: 441
		[EnumMember]
		BecContextToken = 64,
		// Token: 0x040001BA RID: 442
		[EnumMember]
		UserSpecificAll = 84,
		// Token: 0x040001BB RID: 443
		[EnumMember]
		NonUserSpecificAll = 43,
		// Token: 0x040001BC RID: 444
		[EnumMember]
		All = -1
	}
}
