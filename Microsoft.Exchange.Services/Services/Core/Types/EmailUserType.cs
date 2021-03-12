using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005BB RID: 1467
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class EmailUserType
	{
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000B132D File Offset: 0x000AF52D
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x000B1335 File Offset: 0x000AF535
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000B133E File Offset: 0x000AF53E
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x000B1346 File Offset: 0x000AF546
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string UserId { get; set; }
	}
}
