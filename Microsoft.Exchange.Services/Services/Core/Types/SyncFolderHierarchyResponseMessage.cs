using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000569 RID: 1385
	[XmlType("SyncFolderHierarchyResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncFolderHierarchyResponseMessage : ResponseMessage
	{
		// Token: 0x060026BD RID: 9917 RVA: 0x000A6937 File Offset: 0x000A4B37
		public SyncFolderHierarchyResponseMessage()
		{
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000A6940 File Offset: 0x000A4B40
		internal SyncFolderHierarchyResponseMessage(ServiceResultCode code, ServiceError error, SyncFolderHierarchyChangesType value) : base(code, error)
		{
			if (value != null)
			{
				this.Changes = value;
				this.SyncState = value.SyncState;
				this.IncludesLastFolderInRange = value.IncludesLastFolderInRange;
				this.RootFolder = value.RootFolder;
				return;
			}
			this.SyncState = string.Empty;
			this.IncludesLastFolderInRange = true;
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000A6996 File Offset: 0x000A4B96
		// (set) Token: 0x060026C0 RID: 9920 RVA: 0x000A699E File Offset: 0x000A4B9E
		[DataMember(Name = "SyncState", IsRequired = true, EmitDefaultValue = true)]
		[XmlElement("SyncState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SyncState { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x000A69A7 File Offset: 0x000A4BA7
		// (set) Token: 0x060026C2 RID: 9922 RVA: 0x000A69AF File Offset: 0x000A4BAF
		[XmlElement("IncludesLastFolderInRange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "IncludesLastFolderInRange", IsRequired = true, EmitDefaultValue = true)]
		public bool IncludesLastFolderInRange
		{
			get
			{
				return this.includesLastFolderInRange;
			}
			set
			{
				this.IncludesLastFolderInRangeSpecified = true;
				this.includesLastFolderInRange = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000A69BF File Offset: 0x000A4BBF
		// (set) Token: 0x060026C4 RID: 9924 RVA: 0x000A69C7 File Offset: 0x000A4BC7
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IncludesLastFolderInRangeSpecified { get; set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000A69D0 File Offset: 0x000A4BD0
		// (set) Token: 0x060026C6 RID: 9926 RVA: 0x000A69D8 File Offset: 0x000A4BD8
		[XmlElement("Changes")]
		[DataMember(Name = "Changes", IsRequired = true, EmitDefaultValue = true)]
		public SyncFolderHierarchyChangesType Changes { get; set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x000A69E1 File Offset: 0x000A4BE1
		// (set) Token: 0x060026C8 RID: 9928 RVA: 0x000A69E9 File Offset: 0x000A4BE9
		[DataMember(Name = "RootFolder", IsRequired = false, EmitDefaultValue = false)]
		[XmlIgnore]
		private BaseFolderType RootFolder { get; set; }

		// Token: 0x040018BC RID: 6332
		private bool includesLastFolderInRange;
	}
}
