using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000674 RID: 1652
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PeriodType
	{
		// Token: 0x060032C2 RID: 12994 RVA: 0x000B805F File Offset: 0x000B625F
		public PeriodType()
		{
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000B8067 File Offset: 0x000B6267
		public PeriodType(string bias, string name, string id)
		{
			this.Bias = bias;
			this.Name = name;
			this.Id = id;
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000B8084 File Offset: 0x000B6284
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x000B808C File Offset: 0x000B628C
		[XmlAttribute(DataType = "duration")]
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public string Bias { get; set; }

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000B8095 File Offset: 0x000B6295
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x000B809D File Offset: 0x000B629D
		[DataMember(EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string Name { get; set; }

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000B80A6 File Offset: 0x000B62A6
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x000B80AE File Offset: 0x000B62AE
		[DataMember(EmitDefaultValue = false, Order = 0)]
		[XmlAttribute]
		public string Id { get; set; }
	}
}
