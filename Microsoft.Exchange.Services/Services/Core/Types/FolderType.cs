using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005DB RID: 1499
	[KnownType(typeof(SearchFolderType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Folder")]
	[KnownType(typeof(TasksFolderType))]
	[XmlInclude(typeof(TasksFolderType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(SearchFolderType))]
	[Serializable]
	public class FolderType : BaseFolderType
	{
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x000B1DBC File Offset: 0x000AFFBC
		// (set) Token: 0x06002D28 RID: 11560 RVA: 0x000B1DCE File Offset: 0x000AFFCE
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public PermissionSetType PermissionSet
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PermissionSetType>(FolderSchema.PermissionSet);
			}
			set
			{
				base.PropertyBag[FolderSchema.PermissionSet] = value;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x000B1DE1 File Offset: 0x000AFFE1
		// (set) Token: 0x06002D2A RID: 11562 RVA: 0x000B1DF3 File Offset: 0x000AFFF3
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public int? UnreadCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(FolderSchema.UnreadCount);
			}
			set
			{
				base.PropertyBag[FolderSchema.UnreadCount] = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000B1E0B File Offset: 0x000B000B
		// (set) Token: 0x06002D2C RID: 11564 RVA: 0x000B1E18 File Offset: 0x000B0018
		[XmlIgnore]
		[IgnoreDataMember]
		public bool UnreadCountSpecified
		{
			get
			{
				return base.IsSet(FolderSchema.UnreadCount);
			}
			set
			{
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x000B1E1A File Offset: 0x000B001A
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.Folder;
			}
		}
	}
}
