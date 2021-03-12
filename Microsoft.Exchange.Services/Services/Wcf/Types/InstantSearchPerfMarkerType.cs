using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B2E RID: 2862
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "InstantSearchPerfMarkerType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class InstantSearchPerfMarkerType
	{
		// Token: 0x06005110 RID: 20752 RVA: 0x0010A274 File Offset: 0x00108474
		public InstantSearchPerfMarkerType()
		{
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x0010A27C File Offset: 0x0010847C
		public InstantSearchPerfMarkerType(InstantSearchPerfKey perfKey, double value)
		{
			this.PerfKey = perfKey;
			this.PerfValueMS = Math.Round(value, 3);
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x0010A299 File Offset: 0x00108499
		// (set) Token: 0x06005113 RID: 20755 RVA: 0x0010A2A1 File Offset: 0x001084A1
		[DataMember]
		public InstantSearchPerfKey PerfKey { get; set; }

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06005114 RID: 20756 RVA: 0x0010A2AA File Offset: 0x001084AA
		// (set) Token: 0x06005115 RID: 20757 RVA: 0x0010A2B2 File Offset: 0x001084B2
		[DataMember]
		public double PerfValueMS { get; set; }
	}
}
