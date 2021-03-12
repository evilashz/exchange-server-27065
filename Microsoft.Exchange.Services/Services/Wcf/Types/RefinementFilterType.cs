using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B3A RID: 2874
	[XmlType(TypeName = "RefinementFilterType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class RefinementFilterType
	{
		// Token: 0x06005173 RID: 20851 RVA: 0x0010A7F5 File Offset: 0x001089F5
		public RefinementFilterType()
		{
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x0010A7FD File Offset: 0x001089FD
		public RefinementFilterType(string[] refinerQueries)
		{
			this.RefinerQueries = refinerQueries;
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06005175 RID: 20853 RVA: 0x0010A80C File Offset: 0x00108A0C
		// (set) Token: 0x06005176 RID: 20854 RVA: 0x0010A814 File Offset: 0x00108A14
		[DataMember(IsRequired = true)]
		public string[] RefinerQueries { get; set; }
	}
}
