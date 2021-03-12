using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200066C RID: 1644
	[KnownType(typeof(SyncFolderItemsReadFlagType))]
	[KnownType(typeof(SyncFolderItemsCreateOrUpdateType))]
	[KnownType(typeof(SyncFolderItemsDeleteType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class SyncFolderItemsChangeTypeBase
	{
		// Token: 0x0600324D RID: 12877 RVA: 0x000B7A29 File Offset: 0x000B5C29
		public SyncFolderItemsChangeTypeBase()
		{
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x0600324E RID: 12878
		public abstract SyncFolderItemsChangesEnum ChangeType { get; }

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x000B7A31 File Offset: 0x000B5C31
		// (set) Token: 0x06003250 RID: 12880 RVA: 0x000B7A3E File Offset: 0x000B5C3E
		[XmlIgnore]
		[DataMember(Name = "ChangeType", IsRequired = true)]
		public string ChangeTypeString
		{
			get
			{
				return EnumUtilities.ToString<SyncFolderItemsChangesEnum>(this.ChangeType);
			}
			set
			{
			}
		}
	}
}
