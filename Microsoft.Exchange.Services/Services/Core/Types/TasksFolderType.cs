using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000671 RID: 1649
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "TasksFolder")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TasksFolderType : FolderType
	{
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x000B7B1C File Offset: 0x000B5D1C
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.TasksFolder;
			}
		}
	}
}
