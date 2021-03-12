using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000672 RID: 1650
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class TaskSuggestionType : BaseEntityType
	{
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000B7B1F File Offset: 0x000B5D1F
		// (set) Token: 0x0600326A RID: 12906 RVA: 0x000B7B27 File Offset: 0x000B5D27
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string TaskString { get; set; }

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000B7B30 File Offset: 0x000B5D30
		// (set) Token: 0x0600326C RID: 12908 RVA: 0x000B7B38 File Offset: 0x000B5D38
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "EmailUser", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(EmailUserType))]
		public EmailUserType[] Assignees { get; set; }
	}
}
