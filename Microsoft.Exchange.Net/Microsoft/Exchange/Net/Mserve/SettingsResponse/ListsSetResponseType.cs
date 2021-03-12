using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008D5 RID: 2261
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ListsSetResponseType
	{
		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x00072ED3 File Offset: 0x000710D3
		// (set) Token: 0x06003096 RID: 12438 RVA: 0x00072EDB File Offset: 0x000710DB
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x00072EE4 File Offset: 0x000710E4
		// (set) Token: 0x06003098 RID: 12440 RVA: 0x00072EEC File Offset: 0x000710EC
		[XmlElement("List")]
		public ListsSetResponseTypeList[] List
		{
			get
			{
				return this.listField;
			}
			set
			{
				this.listField = value;
			}
		}

		// Token: 0x040029FA RID: 10746
		private int statusField;

		// Token: 0x040029FB RID: 10747
		private ListsSetResponseTypeList[] listField;
	}
}
