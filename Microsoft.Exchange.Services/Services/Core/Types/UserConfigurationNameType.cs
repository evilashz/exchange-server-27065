using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003EB RID: 1003
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "UserConfigurationNameType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class UserConfigurationNameType : TargetFolderId
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0009DD43 File Offset: 0x0009BF43
		// (set) Token: 0x06001C1D RID: 7197 RVA: 0x0009DD4B File Offset: 0x0009BF4B
		[XmlAttribute("Name")]
		[DataMember(IsRequired = true)]
		public string Name { get; set; }
	}
}
