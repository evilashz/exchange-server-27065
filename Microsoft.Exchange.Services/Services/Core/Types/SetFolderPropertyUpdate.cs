using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200088C RID: 2188
	[DataContract(Name = "SetFolderField", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SetFolderFieldType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class SetFolderPropertyUpdate : SetPropertyUpdate
	{
		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000D9258 File Offset: 0x000D7458
		// (set) Token: 0x06003EAF RID: 16047 RVA: 0x000D9260 File Offset: 0x000D7460
		[XmlElement("CalendarFolder", typeof(CalendarFolderType))]
		[XmlElement("ContactsFolder", typeof(ContactsFolderType))]
		[XmlElement("Folder", typeof(FolderType))]
		[XmlElement("SearchFolder", typeof(SearchFolderType))]
		[XmlElement("TasksFolder", typeof(TasksFolderType))]
		[DataMember(IsRequired = true)]
		public BaseFolderType Folder { get; set; }

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x000D9269 File Offset: 0x000D7469
		internal override ServiceObject ServiceObject
		{
			get
			{
				return this.Folder;
			}
		}
	}
}
