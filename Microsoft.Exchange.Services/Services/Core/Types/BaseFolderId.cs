using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E5 RID: 997
	[XmlInclude(typeof(DistinguishedFolderId))]
	[XmlType(TypeName = "BaseFolderIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(AddressListId))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(DistinguishedFolderId))]
	[XmlInclude(typeof(FolderId))]
	[KnownType(typeof(FolderId))]
	public abstract class BaseFolderId : ServiceObjectId
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0009DBE5 File Offset: 0x0009BDE5
		internal override BasicTypes BasicType
		{
			get
			{
				return BasicTypes.Folder;
			}
		}
	}
}
