using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A61 RID: 2657
	[Flags]
	[DataContract]
	public enum PlacesSource
	{
		// Token: 0x04002ADE RID: 10974
		None = 0,
		// Token: 0x04002ADF RID: 10975
		[EnumMember]
		History = 1,
		// Token: 0x04002AE0 RID: 10976
		[EnumMember]
		LocationServices = 2,
		// Token: 0x04002AE1 RID: 10977
		[EnumMember]
		PhonebookServices = 4
	}
}
