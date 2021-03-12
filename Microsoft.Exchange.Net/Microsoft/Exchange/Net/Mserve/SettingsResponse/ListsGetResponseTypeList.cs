using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F0 RID: 2288
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ListsGetResponseTypeList
	{
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000734CD File Offset: 0x000716CD
		// (set) Token: 0x0600314B RID: 12619 RVA: 0x000734D5 File Offset: 0x000716D5
		[XmlArrayItem("Address", IsNullable = false)]
		public string[] Addresses
		{
			get
			{
				return this.addressesField;
			}
			set
			{
				this.addressesField = value;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000734DE File Offset: 0x000716DE
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x000734E6 File Offset: 0x000716E6
		[XmlArrayItem("Domain", IsNullable = false)]
		public string[] Domains
		{
			get
			{
				return this.domainsField;
			}
			set
			{
				this.domainsField = value;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000734EF File Offset: 0x000716EF
		// (set) Token: 0x0600314F RID: 12623 RVA: 0x000734F7 File Offset: 0x000716F7
		[XmlArrayItem("LocalPart", IsNullable = false)]
		public string[] LocalParts
		{
			get
			{
				return this.localPartsField;
			}
			set
			{
				this.localPartsField = value;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x00073500 File Offset: 0x00071700
		// (set) Token: 0x06003151 RID: 12625 RVA: 0x00073508 File Offset: 0x00071708
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

		// Token: 0x04002A75 RID: 10869
		private string[] addressesField;

		// Token: 0x04002A76 RID: 10870
		private string[] domainsField;

		// Token: 0x04002A77 RID: 10871
		private string[] localPartsField;

		// Token: 0x04002A78 RID: 10872
		private string nameField;
	}
}
