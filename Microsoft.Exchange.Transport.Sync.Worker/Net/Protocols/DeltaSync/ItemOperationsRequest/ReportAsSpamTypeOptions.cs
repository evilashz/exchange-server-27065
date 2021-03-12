using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest
{
	// Token: 0x020000C1 RID: 193
	[XmlType(TypeName = "ReportAsSpamTypeOptions", Namespace = "ItemOperations:")]
	[Serializable]
	public class ReportAsSpamTypeOptions
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001A1CA File Offset: 0x000183CA
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0001A1E5 File Offset: 0x000183E5
		[XmlIgnore]
		public ReportAsSpamTypeOptionsReport Report
		{
			get
			{
				if (this.internalReport == null)
				{
					this.internalReport = new ReportAsSpamTypeOptionsReport();
				}
				return this.internalReport;
			}
			set
			{
				this.internalReport = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001A1EE File Offset: 0x000183EE
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001A209 File Offset: 0x00018409
		[XmlIgnore]
		public BounceMessage BounceMessage
		{
			get
			{
				if (this.internalBounceMessage == null)
				{
					this.internalBounceMessage = new BounceMessage();
				}
				return this.internalBounceMessage;
			}
			set
			{
				this.internalBounceMessage = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001A212 File Offset: 0x00018412
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x0001A22D File Offset: 0x0001842D
		[XmlIgnore]
		public UnsubscribeFromMailingList UnsubscribeFromMailingList
		{
			get
			{
				if (this.internalUnsubscribeFromMailingList == null)
				{
					this.internalUnsubscribeFromMailingList = new UnsubscribeFromMailingList();
				}
				return this.internalUnsubscribeFromMailingList;
			}
			set
			{
				this.internalUnsubscribeFromMailingList = value;
			}
		}

		// Token: 0x040003A6 RID: 934
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ReportAsSpamTypeOptionsReport), ElementName = "Report", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public ReportAsSpamTypeOptionsReport internalReport;

		// Token: 0x040003A7 RID: 935
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(BounceMessage), ElementName = "BounceMessage", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public BounceMessage internalBounceMessage;

		// Token: 0x040003A8 RID: 936
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(UnsubscribeFromMailingList), ElementName = "UnsubscribeFromMailingList", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public UnsubscribeFromMailingList internalUnsubscribeFromMailingList;
	}
}
