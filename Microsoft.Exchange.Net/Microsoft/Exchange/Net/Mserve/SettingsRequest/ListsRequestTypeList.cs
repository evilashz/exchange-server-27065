using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008BE RID: 2238
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DesignerCategory("code")]
	[Serializable]
	public class ListsRequestTypeList
	{
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x0006CA0A File Offset: 0x0006AC0A
		// (set) Token: 0x06002FF8 RID: 12280 RVA: 0x0006CA12 File Offset: 0x0006AC12
		[XmlElement("Add", typeof(AddressesAndDomainsType))]
		[XmlElement("Delete", typeof(AddressesAndDomainsType))]
		[XmlElement("Set", typeof(AddressesAndDomainsType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		public AddressesAndDomainsType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x0006CA1B File Offset: 0x0006AC1B
		// (set) Token: 0x06002FFA RID: 12282 RVA: 0x0006CA23 File Offset: 0x0006AC23
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType[] ItemsElementName
		{
			get
			{
				return this.itemsElementNameField;
			}
			set
			{
				this.itemsElementNameField = value;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x0006CA2C File Offset: 0x0006AC2C
		// (set) Token: 0x06002FFC RID: 12284 RVA: 0x0006CA34 File Offset: 0x0006AC34
		[XmlAttribute]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x04002970 RID: 10608
		private AddressesAndDomainsType[] itemsField;

		// Token: 0x04002971 RID: 10609
		private ItemsChoiceType[] itemsElementNameField;

		// Token: 0x04002972 RID: 10610
		private string nameField;
	}
}
