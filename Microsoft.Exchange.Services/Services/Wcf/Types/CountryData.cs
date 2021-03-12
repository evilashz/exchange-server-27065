using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A74 RID: 2676
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CountryData : OptionsPropertyChangeTracker
	{
		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x00105E8A File Offset: 0x0010408A
		// (set) Token: 0x06004BE8 RID: 19432 RVA: 0x00105E92 File Offset: 0x00104092
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06004BE9 RID: 19433 RVA: 0x00105E9B File Offset: 0x0010409B
		// (set) Token: 0x06004BEA RID: 19434 RVA: 0x00105EA3 File Offset: 0x001040A3
		[DataMember]
		public string LocalizedDisplayName { get; set; }
	}
}
