using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000390 RID: 912
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class DeleteItemType : BaseRequestType
	{
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0002A248 File Offset: 0x00028448
		// (set) Token: 0x06001CC0 RID: 7360 RVA: 0x0002A250 File Offset: 0x00028450
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseItemIdType[] ItemIds
		{
			get
			{
				return this.itemIdsField;
			}
			set
			{
				this.itemIdsField = value;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0002A259 File Offset: 0x00028459
		// (set) Token: 0x06001CC2 RID: 7362 RVA: 0x0002A261 File Offset: 0x00028461
		[XmlAttribute]
		public DisposalType DeleteType
		{
			get
			{
				return this.deleteTypeField;
			}
			set
			{
				this.deleteTypeField = value;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0002A26A File Offset: 0x0002846A
		// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x0002A272 File Offset: 0x00028472
		[XmlAttribute]
		public CalendarItemCreateOrDeleteOperationType SendMeetingCancellations
		{
			get
			{
				return this.sendMeetingCancellationsField;
			}
			set
			{
				this.sendMeetingCancellationsField = value;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x0002A27B File Offset: 0x0002847B
		// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x0002A283 File Offset: 0x00028483
		[XmlIgnore]
		public bool SendMeetingCancellationsSpecified
		{
			get
			{
				return this.sendMeetingCancellationsFieldSpecified;
			}
			set
			{
				this.sendMeetingCancellationsFieldSpecified = value;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0002A28C File Offset: 0x0002848C
		// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x0002A294 File Offset: 0x00028494
		[XmlAttribute]
		public AffectedTaskOccurrencesType AffectedTaskOccurrences
		{
			get
			{
				return this.affectedTaskOccurrencesField;
			}
			set
			{
				this.affectedTaskOccurrencesField = value;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x0002A29D File Offset: 0x0002849D
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x0002A2A5 File Offset: 0x000284A5
		[XmlIgnore]
		public bool AffectedTaskOccurrencesSpecified
		{
			get
			{
				return this.affectedTaskOccurrencesFieldSpecified;
			}
			set
			{
				this.affectedTaskOccurrencesFieldSpecified = value;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0002A2AE File Offset: 0x000284AE
		// (set) Token: 0x06001CCC RID: 7372 RVA: 0x0002A2B6 File Offset: 0x000284B6
		[XmlAttribute]
		public bool SuppressReadReceipts
		{
			get
			{
				return this.suppressReadReceiptsField;
			}
			set
			{
				this.suppressReadReceiptsField = value;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0002A2BF File Offset: 0x000284BF
		// (set) Token: 0x06001CCE RID: 7374 RVA: 0x0002A2C7 File Offset: 0x000284C7
		[XmlIgnore]
		public bool SuppressReadReceiptsSpecified
		{
			get
			{
				return this.suppressReadReceiptsFieldSpecified;
			}
			set
			{
				this.suppressReadReceiptsFieldSpecified = value;
			}
		}

		// Token: 0x0400130E RID: 4878
		private BaseItemIdType[] itemIdsField;

		// Token: 0x0400130F RID: 4879
		private DisposalType deleteTypeField;

		// Token: 0x04001310 RID: 4880
		private CalendarItemCreateOrDeleteOperationType sendMeetingCancellationsField;

		// Token: 0x04001311 RID: 4881
		private bool sendMeetingCancellationsFieldSpecified;

		// Token: 0x04001312 RID: 4882
		private AffectedTaskOccurrencesType affectedTaskOccurrencesField;

		// Token: 0x04001313 RID: 4883
		private bool affectedTaskOccurrencesFieldSpecified;

		// Token: 0x04001314 RID: 4884
		private bool suppressReadReceiptsField;

		// Token: 0x04001315 RID: 4885
		private bool suppressReadReceiptsFieldSpecified;
	}
}
