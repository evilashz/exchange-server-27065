using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008BD RID: 2237
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FiltersRequestTypeFilterActionsMoveToFolder
	{
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002FF4 RID: 12276 RVA: 0x0006C9F1 File Offset: 0x0006ABF1
		// (set) Token: 0x06002FF5 RID: 12277 RVA: 0x0006C9F9 File Offset: 0x0006ABF9
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

		// Token: 0x0400296F RID: 10607
		private string folderIdField;
	}
}
