using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008FA RID: 2298
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersResponseTypeFilterActionsMoveToFolder
	{
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x0007367B File Offset: 0x0007187B
		// (set) Token: 0x0600317E RID: 12670 RVA: 0x00073683 File Offset: 0x00071883
		public string FolderId
		{
			get
			{
				return this.folderIdField;
			}
			set
			{
				this.folderIdField = value;
			}
		}

		// Token: 0x04002A9A RID: 10906
		private string folderIdField;
	}
}
