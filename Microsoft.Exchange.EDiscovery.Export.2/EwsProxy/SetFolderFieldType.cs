using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002F6 RID: 758
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SetFolderFieldType : FolderChangeDescriptionType
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00028506 File Offset: 0x00026706
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x0002850E File Offset: 0x0002670E
		[XmlElement("SearchFolder", typeof(SearchFolderType))]
		[XmlElement("Folder", typeof(FolderType))]
		[XmlElement("TasksFolder", typeof(TasksFolderType))]
		[XmlElement("ContactsFolder", typeof(ContactsFolderType))]
		[XmlElement("CalendarFolder", typeof(CalendarFolderType))]
		public BaseFolderType Item1
		{
			get
			{
				return this.item1Field;
			}
			set
			{
				this.item1Field = value;
			}
		}

		// Token: 0x0400111A RID: 4378
		private BaseFolderType item1Field;
	}
}
