using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000869 RID: 2153
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "RootItemId")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "RootItemIdType")]
	[Serializable]
	public class RootItemIdType : BaseItemId
	{
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x000D7CEF File Offset: 0x000D5EEF
		// (set) Token: 0x06003DBC RID: 15804 RVA: 0x000D7CF7 File Offset: 0x000D5EF7
		[XmlAttribute]
		[DataMember(EmitDefaultValue = false)]
		public string RootItemId { get; set; }

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x000D7D00 File Offset: 0x000D5F00
		// (set) Token: 0x06003DBE RID: 15806 RVA: 0x000D7D08 File Offset: 0x000D5F08
		[DataMember(EmitDefaultValue = false)]
		[XmlAttribute]
		public string RootItemChangeKey { get; set; }

		// Token: 0x06003DBF RID: 15807 RVA: 0x000D7D11 File Offset: 0x000D5F11
		public RootItemIdType()
		{
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000D7D19 File Offset: 0x000D5F19
		public RootItemIdType(string id, string changeKey)
		{
			this.RootItemId = id;
			this.RootItemChangeKey = changeKey;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000D7D2F File Offset: 0x000D5F2F
		public override string GetId()
		{
			return this.RootItemId;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000D7D37 File Offset: 0x000D5F37
		public override string GetChangeKey()
		{
			return this.RootItemChangeKey;
		}
	}
}
