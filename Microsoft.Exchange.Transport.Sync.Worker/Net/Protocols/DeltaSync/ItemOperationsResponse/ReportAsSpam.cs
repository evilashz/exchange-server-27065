using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse
{
	// Token: 0x020000D1 RID: 209
	[XmlType(TypeName = "ReportAsSpam", Namespace = "ItemOperations:")]
	[Serializable]
	public class ReportAsSpam
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001A614 File Offset: 0x00018814
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x0001A61C File Offset: 0x0001881C
		[XmlIgnore]
		public string ServerId
		{
			get
			{
				return this.internalServerId;
			}
			set
			{
				this.internalServerId = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001A625 File Offset: 0x00018825
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001A62D File Offset: 0x0001882D
		[XmlIgnore]
		public int Status
		{
			get
			{
				return this.internalStatus;
			}
			set
			{
				this.internalStatus = value;
				this.internalStatusSpecified = true;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001A63D File Offset: 0x0001883D
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0001A658 File Offset: 0x00018858
		[XmlIgnore]
		public SubResultType Report
		{
			get
			{
				if (this.internalReport == null)
				{
					this.internalReport = new SubResultType();
				}
				return this.internalReport;
			}
			set
			{
				this.internalReport = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001A661 File Offset: 0x00018861
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001A67C File Offset: 0x0001887C
		[XmlIgnore]
		public SubResultType BounceMessage
		{
			get
			{
				if (this.internalBounceMessage == null)
				{
					this.internalBounceMessage = new SubResultType();
				}
				return this.internalBounceMessage;
			}
			set
			{
				this.internalBounceMessage = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001A685 File Offset: 0x00018885
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001A6A0 File Offset: 0x000188A0
		[XmlIgnore]
		public SubResultType UnsubscribeFromMailingList
		{
			get
			{
				if (this.internalUnsubscribeFromMailingList == null)
				{
					this.internalUnsubscribeFromMailingList = new SubResultType();
				}
				return this.internalUnsubscribeFromMailingList;
			}
			set
			{
				this.internalUnsubscribeFromMailingList = value;
			}
		}

		// Token: 0x040003C8 RID: 968
		[XmlElement(ElementName = "ServerId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalServerId;

		// Token: 0x040003C9 RID: 969
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040003CA RID: 970
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040003CB RID: 971
		[XmlElement(Type = typeof(SubResultType), ElementName = "Report", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubResultType internalReport;

		// Token: 0x040003CC RID: 972
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(SubResultType), ElementName = "BounceMessage", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		public SubResultType internalBounceMessage;

		// Token: 0x040003CD RID: 973
		[XmlElement(Type = typeof(SubResultType), ElementName = "UnsubscribeFromMailingList", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "ItemOperations:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SubResultType internalUnsubscribeFromMailingList;
	}
}
