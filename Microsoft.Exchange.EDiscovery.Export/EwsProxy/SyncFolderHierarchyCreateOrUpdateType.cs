using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000202 RID: 514
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SyncFolderHierarchyCreateOrUpdateType
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00025CA9 File Offset: 0x00023EA9
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x00025CB1 File Offset: 0x00023EB1
		[XmlElement("TasksFolder", typeof(TasksFolderType))]
		[XmlElement("ContactsFolder", typeof(ContactsFolderType))]
		[XmlElement("SearchFolder", typeof(SearchFolderType))]
		[XmlElement("Folder", typeof(FolderType))]
		[XmlElement("CalendarFolder", typeof(CalendarFolderType))]
		public BaseFolderType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04000E1C RID: 3612
		private BaseFolderType itemField;
	}
}
