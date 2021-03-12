using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200056B RID: 1387
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SyncFolderItemsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SyncFolderItemsResponseMessage : ResponseMessage
	{
		// Token: 0x060026CC RID: 9932 RVA: 0x000A6A30 File Offset: 0x000A4C30
		public SyncFolderItemsResponseMessage()
		{
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000A6A38 File Offset: 0x000A4C38
		internal SyncFolderItemsResponseMessage(ServiceResultCode code, ServiceError error, SyncFolderItemsChangesType value) : base(code, error)
		{
			if (value != null)
			{
				this.SyncState = value.SyncState;
				this.IncludesLastItemInRange = value.IncludesLastItemInRange;
				this.Changes = value;
				this.MoreItemsOnServer = value.MoreItemsOnServer;
				this.OldestReceivedTime = value.OldestReceivedTime;
				this.TotalCount = value.TotalCount;
				return;
			}
			ExTraceGlobals.SynchronizationTracer.TraceDebug((long)this.GetHashCode(), "SyncFolderItemsChangesType value is null.");
			this.SyncState = string.Empty;
			this.IncludesLastItemInRange = true;
			this.MoreItemsOnServer = true;
			this.OldestReceivedTime = ExDateTime.UtcNow.ToISOString();
			this.TotalCount = 0;
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x000A6ADD File Offset: 0x000A4CDD
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x000A6AE5 File Offset: 0x000A4CE5
		[XmlElement("SyncState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "SyncState", IsRequired = true)]
		public string SyncState { get; set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x000A6AEE File Offset: 0x000A4CEE
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x000A6AF6 File Offset: 0x000A4CF6
		[XmlElement("IncludesLastItemInRange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "IncludesLastItemInRange", IsRequired = false, EmitDefaultValue = false)]
		public bool IncludesLastItemInRange
		{
			get
			{
				return this.includesLastItemInRange;
			}
			set
			{
				this.IncludesLastItemInRangeSpecified = true;
				this.includesLastItemInRange = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x000A6B06 File Offset: 0x000A4D06
		// (set) Token: 0x060026D3 RID: 9939 RVA: 0x000A6B0E File Offset: 0x000A4D0E
		[DataMember(Name = "TotalCount", EmitDefaultValue = true)]
		[XmlIgnore]
		public int TotalCount { get; set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x000A6B17 File Offset: 0x000A4D17
		// (set) Token: 0x060026D5 RID: 9941 RVA: 0x000A6B1F File Offset: 0x000A4D1F
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified { get; set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x000A6B28 File Offset: 0x000A4D28
		// (set) Token: 0x060026D7 RID: 9943 RVA: 0x000A6B30 File Offset: 0x000A4D30
		[XmlIgnore]
		[DateTimeString]
		[DataMember(Name = "OldestReceivedTime", IsRequired = false, EmitDefaultValue = false)]
		public string OldestReceivedTime { get; set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x000A6B39 File Offset: 0x000A4D39
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x000A6B41 File Offset: 0x000A4D41
		[XmlIgnore]
		[DataMember(Name = "MoreItemsOnServer", IsRequired = false, EmitDefaultValue = false)]
		public bool MoreItemsOnServer { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x000A6B4A File Offset: 0x000A4D4A
		// (set) Token: 0x060026DB RID: 9947 RVA: 0x000A6B52 File Offset: 0x000A4D52
		[XmlElement("Changes")]
		[DataMember(Name = "Changes", IsRequired = false, EmitDefaultValue = false)]
		public SyncFolderItemsChangesType Changes { get; set; }

		// Token: 0x040018C1 RID: 6337
		private bool includesLastItemInRange;
	}
}
