using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F9 RID: 2297
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FiltersResponseTypeFilterActions
	{
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x00073662 File Offset: 0x00071862
		// (set) Token: 0x0600317B RID: 12667 RVA: 0x0007366A File Offset: 0x0007186A
		public FiltersResponseTypeFilterActionsMoveToFolder MoveToFolder
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

		// Token: 0x04002A99 RID: 10905
		private FiltersResponseTypeFilterActionsMoveToFolder moveToFolderField;
	}
}
