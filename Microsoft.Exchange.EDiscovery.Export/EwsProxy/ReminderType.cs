using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D5 RID: 469
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ReminderType
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000255FE File Offset: 0x000237FE
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x00025606 File Offset: 0x00023806
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

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0002560F File Offset: 0x0002380F
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x00025617 File Offset: 0x00023817
		public string Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00025620 File Offset: 0x00023820
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00025628 File Offset: 0x00023828
		public DateTime ReminderTime
		{
			get
			{
				return this.reminderTimeField;
			}
			set
			{
				this.reminderTimeField = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00025631 File Offset: 0x00023831
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x00025639 File Offset: 0x00023839
		public DateTime StartDate
		{
			get
			{
				return this.startDateField;
			}
			set
			{
				this.startDateField = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00025642 File Offset: 0x00023842
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x0002564A File Offset: 0x0002384A
		public DateTime EndDate
		{
			get
			{
				return this.endDateField;
			}
			set
			{
				this.endDateField = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x00025653 File Offset: 0x00023853
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x0002565B File Offset: 0x0002385B
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x00025664 File Offset: 0x00023864
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x0002566C File Offset: 0x0002386C
		public ItemIdType RecurringMasterItemId
		{
			get
			{
				return this.recurringMasterItemIdField;
			}
			set
			{
				this.recurringMasterItemIdField = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00025675 File Offset: 0x00023875
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x0002567D File Offset: 0x0002387D
		public ReminderGroupType ReminderGroup
		{
			get
			{
				return this.reminderGroupField;
			}
			set
			{
				this.reminderGroupField = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00025686 File Offset: 0x00023886
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0002568E File Offset: 0x0002388E
		[XmlIgnore]
		public bool ReminderGroupSpecified
		{
			get
			{
				return this.reminderGroupFieldSpecified;
			}
			set
			{
				this.reminderGroupFieldSpecified = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00025697 File Offset: 0x00023897
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x0002569F File Offset: 0x0002389F
		public string UID
		{
			get
			{
				return this.uIDField;
			}
			set
			{
				this.uIDField = value;
			}
		}

		// Token: 0x04000D9B RID: 3483
		private string subjectField;

		// Token: 0x04000D9C RID: 3484
		private string locationField;

		// Token: 0x04000D9D RID: 3485
		private DateTime reminderTimeField;

		// Token: 0x04000D9E RID: 3486
		private DateTime startDateField;

		// Token: 0x04000D9F RID: 3487
		private DateTime endDateField;

		// Token: 0x04000DA0 RID: 3488
		private ItemIdType itemIdField;

		// Token: 0x04000DA1 RID: 3489
		private ItemIdType recurringMasterItemIdField;

		// Token: 0x04000DA2 RID: 3490
		private ReminderGroupType reminderGroupField;

		// Token: 0x04000DA3 RID: 3491
		private bool reminderGroupFieldSpecified;

		// Token: 0x04000DA4 RID: 3492
		private string uIDField;
	}
}
