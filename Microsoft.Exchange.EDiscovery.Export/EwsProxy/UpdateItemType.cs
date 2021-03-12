using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000394 RID: 916
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class UpdateItemType : BaseRequestType
	{
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0002A335 File Offset: 0x00028535
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0002A33D File Offset: 0x0002853D
		public TargetFolderIdType SavedItemFolderId
		{
			get
			{
				return this.savedItemFolderIdField;
			}
			set
			{
				this.savedItemFolderIdField = value;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0002A346 File Offset: 0x00028546
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0002A34E File Offset: 0x0002854E
		[XmlArrayItem("ItemChange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemChangeType[] ItemChanges
		{
			get
			{
				return this.itemChangesField;
			}
			set
			{
				this.itemChangesField = value;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0002A357 File Offset: 0x00028557
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x0002A35F File Offset: 0x0002855F
		[XmlAttribute]
		public ConflictResolutionType ConflictResolution
		{
			get
			{
				return this.conflictResolutionField;
			}
			set
			{
				this.conflictResolutionField = value;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0002A368 File Offset: 0x00028568
		// (set) Token: 0x06001CE2 RID: 7394 RVA: 0x0002A370 File Offset: 0x00028570
		[XmlAttribute]
		public MessageDispositionType MessageDisposition
		{
			get
			{
				return this.messageDispositionField;
			}
			set
			{
				this.messageDispositionField = value;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0002A379 File Offset: 0x00028579
		// (set) Token: 0x06001CE4 RID: 7396 RVA: 0x0002A381 File Offset: 0x00028581
		[XmlIgnore]
		public bool MessageDispositionSpecified
		{
			get
			{
				return this.messageDispositionFieldSpecified;
			}
			set
			{
				this.messageDispositionFieldSpecified = value;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0002A38A File Offset: 0x0002858A
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x0002A392 File Offset: 0x00028592
		[XmlAttribute]
		public CalendarItemUpdateOperationType SendMeetingInvitationsOrCancellations
		{
			get
			{
				return this.sendMeetingInvitationsOrCancellationsField;
			}
			set
			{
				this.sendMeetingInvitationsOrCancellationsField = value;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x0002A39B File Offset: 0x0002859B
		// (set) Token: 0x06001CE8 RID: 7400 RVA: 0x0002A3A3 File Offset: 0x000285A3
		[XmlIgnore]
		public bool SendMeetingInvitationsOrCancellationsSpecified
		{
			get
			{
				return this.sendMeetingInvitationsOrCancellationsFieldSpecified;
			}
			set
			{
				this.sendMeetingInvitationsOrCancellationsFieldSpecified = value;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x0002A3AC File Offset: 0x000285AC
		// (set) Token: 0x06001CEA RID: 7402 RVA: 0x0002A3B4 File Offset: 0x000285B4
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

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0002A3BD File Offset: 0x000285BD
		// (set) Token: 0x06001CEC RID: 7404 RVA: 0x0002A3C5 File Offset: 0x000285C5
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

		// Token: 0x04001322 RID: 4898
		private TargetFolderIdType savedItemFolderIdField;

		// Token: 0x04001323 RID: 4899
		private ItemChangeType[] itemChangesField;

		// Token: 0x04001324 RID: 4900
		private ConflictResolutionType conflictResolutionField;

		// Token: 0x04001325 RID: 4901
		private MessageDispositionType messageDispositionField;

		// Token: 0x04001326 RID: 4902
		private bool messageDispositionFieldSpecified;

		// Token: 0x04001327 RID: 4903
		private CalendarItemUpdateOperationType sendMeetingInvitationsOrCancellationsField;

		// Token: 0x04001328 RID: 4904
		private bool sendMeetingInvitationsOrCancellationsFieldSpecified;

		// Token: 0x04001329 RID: 4905
		private bool suppressReadReceiptsField;

		// Token: 0x0400132A RID: 4906
		private bool suppressReadReceiptsFieldSpecified;
	}
}
