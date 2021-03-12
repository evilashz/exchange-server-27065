using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020002E8 RID: 744
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[Serializable]
	public class FindMessageTrackingSearchResultType
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00066E41 File Offset: 0x00065041
		// (set) Token: 0x060015C3 RID: 5571 RVA: 0x00066E49 File Offset: 0x00065049
		public string Subject
		{
			get
			{
				return this.subjectField;
			}
			set
			{
				this.subjectField = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00066E52 File Offset: 0x00065052
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x00066E5A File Offset: 0x0006505A
		public EmailAddressType Sender
		{
			get
			{
				return this.senderField;
			}
			set
			{
				this.senderField = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x00066E63 File Offset: 0x00065063
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x00066E6B File Offset: 0x0006506B
		public EmailAddressType PurportedSender
		{
			get
			{
				return this.purportedSenderField;
			}
			set
			{
				this.purportedSenderField = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00066E74 File Offset: 0x00065074
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x00066E7C File Offset: 0x0006507C
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] Recipients
		{
			get
			{
				return this.recipientsField;
			}
			set
			{
				this.recipientsField = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x00066E85 File Offset: 0x00065085
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x00066E8D File Offset: 0x0006508D
		public DateTime SubmittedTime
		{
			get
			{
				return this.submittedTimeField;
			}
			set
			{
				this.submittedTimeField = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00066E96 File Offset: 0x00065096
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x00066E9E File Offset: 0x0006509E
		public string MessageTrackingReportId
		{
			get
			{
				return this.messageTrackingReportIdField;
			}
			set
			{
				this.messageTrackingReportIdField = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00066EA7 File Offset: 0x000650A7
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x00066EAF File Offset: 0x000650AF
		public string PreviousHopServer
		{
			get
			{
				return this.previousHopServerField;
			}
			set
			{
				this.previousHopServerField = value;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00066EB8 File Offset: 0x000650B8
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00066EC0 File Offset: 0x000650C0
		public string FirstHopServer
		{
			get
			{
				return this.firstHopServerField;
			}
			set
			{
				this.firstHopServerField = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x00066EC9 File Offset: 0x000650C9
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x00066ED1 File Offset: 0x000650D1
		[XmlArrayItem(IsNullable = false)]
		public TrackingPropertyType[] Properties
		{
			get
			{
				return this.propertiesField;
			}
			set
			{
				this.propertiesField = value;
			}
		}

		// Token: 0x04000E33 RID: 3635
		private string subjectField;

		// Token: 0x04000E34 RID: 3636
		private EmailAddressType senderField;

		// Token: 0x04000E35 RID: 3637
		private EmailAddressType purportedSenderField;

		// Token: 0x04000E36 RID: 3638
		private EmailAddressType[] recipientsField;

		// Token: 0x04000E37 RID: 3639
		private DateTime submittedTimeField;

		// Token: 0x04000E38 RID: 3640
		private string messageTrackingReportIdField;

		// Token: 0x04000E39 RID: 3641
		private string previousHopServerField;

		// Token: 0x04000E3A RID: 3642
		private string firstHopServerField;

		// Token: 0x04000E3B RID: 3643
		private TrackingPropertyType[] propertiesField;
	}
}
