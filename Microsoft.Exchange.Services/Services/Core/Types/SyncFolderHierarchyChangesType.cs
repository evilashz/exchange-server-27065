using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000665 RID: 1637
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SyncFolderHierarchyChangesType
	{
		// Token: 0x06003221 RID: 12833 RVA: 0x000B7823 File Offset: 0x000B5A23
		public SyncFolderHierarchyChangesType()
		{
			this.changes = new List<SyncFolderHierarchyChangeBase>();
			this.changesElementName = new List<SyncFolderHierarchyChangesEnum>();
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000B7841 File Offset: 0x000B5A41
		public void AddChange(SyncFolderHierarchyChangeBase change)
		{
			this.changes.Add(change);
			this.changesElementName.Add(change.ChangeType);
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000B7860 File Offset: 0x000B5A60
		public int Count
		{
			get
			{
				return this.changes.Count;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000B786D File Offset: 0x000B5A6D
		// (set) Token: 0x06003225 RID: 12837 RVA: 0x000B787A File Offset: 0x000B5A7A
		[XmlElement("Delete", typeof(SyncFolderHierarchyDeleteType))]
		[XmlElement("Create", typeof(SyncFolderHierarchyCreateOrUpdateType))]
		[XmlElement("Update", typeof(SyncFolderHierarchyCreateOrUpdateType))]
		[XmlChoiceIdentifier("ChangesElementName")]
		[DataMember(Name = "Changes", EmitDefaultValue = true)]
		public SyncFolderHierarchyChangeBase[] Changes
		{
			get
			{
				return this.changes.ToArray();
			}
			set
			{
				this.changes = new List<SyncFolderHierarchyChangeBase>(value);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000B7888 File Offset: 0x000B5A88
		// (set) Token: 0x06003227 RID: 12839 RVA: 0x000B7895 File Offset: 0x000B5A95
		[XmlIgnore]
		[XmlElement("ChangesElementName")]
		[IgnoreDataMember]
		public SyncFolderHierarchyChangesEnum[] ChangesElementName
		{
			get
			{
				return this.changesElementName.ToArray();
			}
			set
			{
				this.changesElementName = new List<SyncFolderHierarchyChangesEnum>(value);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000B78A3 File Offset: 0x000B5AA3
		// (set) Token: 0x06003229 RID: 12841 RVA: 0x000B78AB File Offset: 0x000B5AAB
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IncludesLastFolderInRange { get; set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000B78B4 File Offset: 0x000B5AB4
		// (set) Token: 0x0600322B RID: 12843 RVA: 0x000B78BC File Offset: 0x000B5ABC
		[XmlIgnore]
		[IgnoreDataMember]
		public string SyncState { get; set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x000B78C5 File Offset: 0x000B5AC5
		// (set) Token: 0x0600322D RID: 12845 RVA: 0x000B78CD File Offset: 0x000B5ACD
		[IgnoreDataMember]
		[XmlIgnore]
		public BaseFolderType RootFolder { get; set; }

		// Token: 0x04001C99 RID: 7321
		private List<SyncFolderHierarchyChangeBase> changes;

		// Token: 0x04001C9A RID: 7322
		private List<SyncFolderHierarchyChangesEnum> changesElementName;
	}
}
