using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008BC RID: 2236
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FiltersRequestTypeFilterActions
	{
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x0006C9D8 File Offset: 0x0006ABD8
		// (set) Token: 0x06002FF2 RID: 12274 RVA: 0x0006C9E0 File Offset: 0x0006ABE0
		public FiltersRequestTypeFilterActionsMoveToFolder MoveToFolder
		{
			get
			{
				return this.moveToFolderField;
			}
			set
			{
				this.moveToFolderField = value;
			}
		}

		// Token: 0x0400296E RID: 10606
		private FiltersRequestTypeFilterActionsMoveToFolder moveToFolderField;
	}
}
