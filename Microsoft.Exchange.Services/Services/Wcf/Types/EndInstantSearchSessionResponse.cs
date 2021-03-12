using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B29 RID: 2857
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "EndInstantSearchSessionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public sealed class EndInstantSearchSessionResponse : ResponseMessage
	{
		// Token: 0x060050E9 RID: 20713 RVA: 0x0010A05B File Offset: 0x0010825B
		public EndInstantSearchSessionResponse()
		{
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x0010A064 File Offset: 0x00108264
		public EndInstantSearchSessionResponse(Dictionary<long, List<SearchPathSnapshotType>> dataDictionary)
		{
			int count = dataDictionary.Count;
			this.RequestIds = new long[count];
			this.SearchPathSnapshots = new SearchPathSnapshotType[count][];
			int num = 0;
			foreach (long num2 in dataDictionary.Keys)
			{
				this.RequestIds[num] = num2;
				this.SearchPathSnapshots[num] = dataDictionary[num2].ToArray();
				num++;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x060050EB RID: 20715 RVA: 0x0010A0F8 File Offset: 0x001082F8
		// (set) Token: 0x060050EC RID: 20716 RVA: 0x0010A100 File Offset: 0x00108300
		[XmlIgnore]
		[DataMember]
		public long[] RequestIds { get; set; }

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x060050ED RID: 20717 RVA: 0x0010A109 File Offset: 0x00108309
		// (set) Token: 0x060050EE RID: 20718 RVA: 0x0010A111 File Offset: 0x00108311
		[XmlIgnore]
		[DataMember]
		public SearchPathSnapshotType[][] SearchPathSnapshots { get; set; }
	}
}
