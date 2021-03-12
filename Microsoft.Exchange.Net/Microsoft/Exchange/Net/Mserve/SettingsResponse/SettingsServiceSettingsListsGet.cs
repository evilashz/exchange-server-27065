using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008EF RID: 2287
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DesignerCategory("code")]
	[Serializable]
	public class SettingsServiceSettingsListsGet
	{
		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000734B4 File Offset: 0x000716B4
		// (set) Token: 0x06003148 RID: 12616 RVA: 0x000734BC File Offset: 0x000716BC
		[XmlArrayItem("List", IsNullable = false)]
		public ListsGetResponseTypeList[] Lists
		{
			get
			{
				return this.listsField;
			}
			set
			{
				this.listsField = value;
			}
		}

		// Token: 0x04002A74 RID: 10868
		private ListsGetResponseTypeList[] listsField;
	}
}
