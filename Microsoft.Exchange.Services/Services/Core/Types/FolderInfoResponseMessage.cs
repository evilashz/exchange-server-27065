using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E3 RID: 1251
	[XmlType("FolderInfoResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FolderInfoResponseMessage : ResponseMessage
	{
		// Token: 0x0600248D RID: 9357 RVA: 0x000A4F97 File Offset: 0x000A3197
		public FolderInfoResponseMessage()
		{
			this.Folders = null;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000A4FA8 File Offset: 0x000A31A8
		internal FolderInfoResponseMessage(ServiceResultCode code, ServiceError error, BaseFolderType folder) : base(code, error)
		{
			this.Folders = new BaseFolderType[]
			{
				folder
			};
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x000A4FCF File Offset: 0x000A31CF
		// (set) Token: 0x06002490 RID: 9360 RVA: 0x000A4FD7 File Offset: 0x000A31D7
		[XmlArrayItem("Folder", typeof(FolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("SearchFolder", typeof(SearchFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(EmitDefaultValue = false)]
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderType[] Folders { get; set; }
	}
}
