using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200066A RID: 1642
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SyncFolderItemsChangesType
	{
		// Token: 0x0600323C RID: 12860 RVA: 0x000B7954 File Offset: 0x000B5B54
		public SyncFolderItemsChangesType()
		{
			this.changes = new List<SyncFolderItemsChangeTypeBase>();
			this.changesElementName = new List<SyncFolderItemsChangesEnum>();
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000B7972 File Offset: 0x000B5B72
		public void AddChange(SyncFolderItemsChangeTypeBase change)
		{
			this.changes.Add(change);
			this.changesElementName.Add(change.ChangeType);
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000B7991 File Offset: 0x000B5B91
		public int Count
		{
			get
			{
				return this.changes.Count;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000B799E File Offset: 0x000B5B9E
		// (set) Token: 0x06003240 RID: 12864 RVA: 0x000B79AB File Offset: 0x000B5BAB
		[XmlElement("ReadFlagChange", typeof(SyncFolderItemsReadFlagType))]
		[XmlChoiceIdentifier("ChangesElementName")]
		[DataMember(Name = "Changes", EmitDefaultValue = true)]
		[XmlElement("Create", typeof(SyncFolderItemsCreateOrUpdateType))]
		[XmlElement("Delete", typeof(SyncFolderItemsDeleteType))]
		[XmlElement("Update", typeof(SyncFolderItemsCreateOrUpdateType))]
		public SyncFolderItemsChangeTypeBase[] Changes
		{
			get
			{
				return this.changes.ToArray();
			}
			set
			{
				this.changes = new List<SyncFolderItemsChangeTypeBase>(value);
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000B79B9 File Offset: 0x000B5BB9
		// (set) Token: 0x06003242 RID: 12866 RVA: 0x000B79C6 File Offset: 0x000B5BC6
		[XmlElement("ChangesElementName")]
		[IgnoreDataMember]
		[XmlIgnore]
		public SyncFolderItemsChangesEnum[] ChangesElementName
		{
			get
			{
				return this.changesElementName.ToArray();
			}
			set
			{
				this.changesElementName = new List<SyncFolderItemsChangesEnum>(value);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000B79D4 File Offset: 0x000B5BD4
		// (set) Token: 0x06003244 RID: 12868 RVA: 0x000B79DC File Offset: 0x000B5BDC
		[XmlIgnore]
		[IgnoreDataMember]
		public int TotalCount { get; set; }

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x000B79E5 File Offset: 0x000B5BE5
		// (set) Token: 0x06003246 RID: 12870 RVA: 0x000B79ED File Offset: 0x000B5BED
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IncludesLastItemInRange { get; set; }

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000B79F6 File Offset: 0x000B5BF6
		// (set) Token: 0x06003248 RID: 12872 RVA: 0x000B79FE File Offset: 0x000B5BFE
		[IgnoreDataMember]
		[DateTimeString]
		[XmlIgnore]
		public string OldestReceivedTime { get; set; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x000B7A07 File Offset: 0x000B5C07
		// (set) Token: 0x0600324A RID: 12874 RVA: 0x000B7A0F File Offset: 0x000B5C0F
		[XmlIgnore]
		[IgnoreDataMember]
		public bool MoreItemsOnServer { get; set; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000B7A18 File Offset: 0x000B5C18
		// (set) Token: 0x0600324C RID: 12876 RVA: 0x000B7A20 File Offset: 0x000B5C20
		[IgnoreDataMember]
		[XmlIgnore]
		public string SyncState { get; set; }

		// Token: 0x04001CA5 RID: 7333
		private List<SyncFolderItemsChangeTypeBase> changes;

		// Token: 0x04001CA6 RID: 7334
		private List<SyncFolderItemsChangesEnum> changesElementName;
	}
}
