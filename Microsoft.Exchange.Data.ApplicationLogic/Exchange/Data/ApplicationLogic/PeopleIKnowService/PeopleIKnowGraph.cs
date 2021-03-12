using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x02000187 RID: 391
	[DataContract]
	public sealed class PeopleIKnowGraph : IExtensibleDataObject
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0003D32A File Offset: 0x0003B52A
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x0003D332 File Offset: 0x0003B532
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x040007FD RID: 2045
		[DataMember(Name = "RelevantPeople")]
		public RelevantPerson[] RelevantPeople;
	}
}
