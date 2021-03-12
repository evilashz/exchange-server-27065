using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000399 RID: 921
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CreateItemType : BaseRequestType
	{
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x0002A400 File Offset: 0x00028600
		// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x0002A408 File Offset: 0x00028608
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

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x0002A411 File Offset: 0x00028611
		// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x0002A419 File Offset: 0x00028619
		public NonEmptyArrayOfAllItemsType Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0002A422 File Offset: 0x00028622
		// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x0002A42A File Offset: 0x0002862A
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

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0002A433 File Offset: 0x00028633
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x0002A43B File Offset: 0x0002863B
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

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0002A444 File Offset: 0x00028644
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x0002A44C File Offset: 0x0002864C
		[XmlAttribute]
		public CalendarItemCreateOrDeleteOperationType SendMeetingInvitations
		{
			get
			{
				return this.sendMeetingInvitationsField;
			}
			set
			{
				this.sendMeetingInvitationsField = value;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0002A455 File Offset: 0x00028655
		// (set) Token: 0x06001CFE RID: 7422 RVA: 0x0002A45D File Offset: 0x0002865D
		[XmlIgnore]
		public bool SendMeetingInvitationsSpecified
		{
			get
			{
				return this.sendMeetingInvitationsFieldSpecified;
			}
			set
			{
				this.sendMeetingInvitationsFieldSpecified = value;
			}
		}

		// Token: 0x0400133B RID: 4923
		private TargetFolderIdType savedItemFolderIdField;

		// Token: 0x0400133C RID: 4924
		private NonEmptyArrayOfAllItemsType itemsField;

		// Token: 0x0400133D RID: 4925
		private MessageDispositionType messageDispositionField;

		// Token: 0x0400133E RID: 4926
		private bool messageDispositionFieldSpecified;

		// Token: 0x0400133F RID: 4927
		private CalendarItemCreateOrDeleteOperationType sendMeetingInvitationsField;

		// Token: 0x04001340 RID: 4928
		private bool sendMeetingInvitationsFieldSpecified;
	}
}
