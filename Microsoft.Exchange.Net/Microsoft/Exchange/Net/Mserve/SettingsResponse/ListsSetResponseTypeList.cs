using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008D6 RID: 2262
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class ListsSetResponseTypeList
	{
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x00072EFD File Offset: 0x000710FD
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x00072F05 File Offset: 0x00071105
		[XmlElement("Delete", typeof(StatusType))]
		[XmlElement("Add", typeof(StatusType))]
		[XmlElement("Set", typeof(StatusType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		public StatusType[] Items
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

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x00072F0E File Offset: 0x0007110E
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x00072F16 File Offset: 0x00071116
		[XmlIgnore]
		[XmlElement("ItemsElementName")]
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

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x00072F1F File Offset: 0x0007111F
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x00072F27 File Offset: 0x00071127
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

		// Token: 0x040029FC RID: 10748
		private StatusType[] itemsField;

		// Token: 0x040029FD RID: 10749
		private ItemsChoiceType[] itemsElementNameField;

		// Token: 0x040029FE RID: 10750
		private string nameField;
	}
}
