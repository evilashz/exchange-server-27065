using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006B4 RID: 1716
	[DataContract(Name = "AppendToFolderField", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "AppendToFolderFieldType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class AppendFolderPropertyUpdate : AppendPropertyUpdate
	{
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000BE2FF File Offset: 0x000BC4FF
		// (set) Token: 0x060034DF RID: 13535 RVA: 0x000BE307 File Offset: 0x000BC507
		[XmlElement("Folder", typeof(FolderType))]
		[XmlElement("ContactsFolder", typeof(ContactsFolderType))]
		[DataMember(IsRequired = true)]
		[XmlElement("SearchFolder", typeof(SearchFolderType))]
		[XmlElement("TasksFolder", typeof(TasksFolderType))]
		[XmlElement("CalendarFolder", typeof(CalendarFolderType))]
		public BaseFolderType Folder { get; set; }

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000BE310 File Offset: 0x000BC510
		internal override ServiceObject ServiceObject
		{
			get
			{
				return this.Folder;
			}
		}
	}
}
