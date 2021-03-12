using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200081F RID: 2079
	[XmlType(TypeName = "NonEmptyArrayOfPropertyValuesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class NonEmptyArrayOfPropertyValues
	{
		// Token: 0x06003C3B RID: 15419 RVA: 0x000D5A6E File Offset: 0x000D3C6E
		public NonEmptyArrayOfPropertyValues()
		{
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x000D5A76 File Offset: 0x000D3C76
		internal NonEmptyArrayOfPropertyValues(string[] values)
		{
			this.Values = values;
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x000D5A85 File Offset: 0x000D3C85
		// (set) Token: 0x06003C3E RID: 15422 RVA: 0x000D5A8D File Offset: 0x000D3C8D
		[XmlElement(ElementName = "Value")]
		[DataMember(Name = "Values")]
		public string[] Values { get; set; }
	}
}
