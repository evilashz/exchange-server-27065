using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002F4 RID: 756
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class AppendToFolderFieldType : FolderChangeDescriptionType
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x000284E5 File Offset: 0x000266E5
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x000284ED File Offset: 0x000266ED
		[XmlElement("TasksFolder", typeof(TasksFolderType))]
		[XmlElement("CalendarFolder", typeof(CalendarFolderType))]
		[XmlElement("ContactsFolder", typeof(ContactsFolderType))]
		[XmlElement("Folder", typeof(FolderType))]
		[XmlElement("SearchFolder", typeof(SearchFolderType))]
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

		// Token: 0x04001119 RID: 4377
		private BaseFolderType item1Field;
	}
}
