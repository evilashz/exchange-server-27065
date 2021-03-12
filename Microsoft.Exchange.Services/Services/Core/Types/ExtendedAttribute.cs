using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F4 RID: 2036
	[DataContract(Name = "ExtendedAttribute", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ExtendedAttributeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ExtendedAttribute
	{
		// Token: 0x06003B88 RID: 15240 RVA: 0x000D023D File Offset: 0x000CE43D
		public ExtendedAttribute()
		{
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000D0245 File Offset: 0x000CE445
		public ExtendedAttribute(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003B8A RID: 15242 RVA: 0x000D025B File Offset: 0x000CE45B
		// (set) Token: 0x06003B8B RID: 15243 RVA: 0x000D0263 File Offset: 0x000CE463
		[DataMember(Name = "Name", IsRequired = false, Order = 0)]
		[XmlElement("Name")]
		public string Name { get; set; }

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003B8C RID: 15244 RVA: 0x000D026C File Offset: 0x000CE46C
		// (set) Token: 0x06003B8D RID: 15245 RVA: 0x000D0274 File Offset: 0x000CE474
		[DataMember(Name = "Value", IsRequired = false, Order = 1)]
		[XmlElement("Value")]
		public string Value { get; set; }
	}
}
