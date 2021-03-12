using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001BC RID: 444
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class RecipientTrackingEventType
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000250A8 File Offset: 0x000232A8
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x000250B0 File Offset: 0x000232B0
		public DateTime Date
		{
			get
			{
				return this.dateField;
			}
			set
			{
				this.dateField = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000250B9 File Offset: 0x000232B9
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x000250C1 File Offset: 0x000232C1
		public EmailAddressType Recipient
		{
			get
			{
				return this.recipientField;
			}
			set
			{
				this.recipientField = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000250CA File Offset: 0x000232CA
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x000250D2 File Offset: 0x000232D2
		public string DeliveryStatus
		{
			get
			{
				return this.deliveryStatusField;
			}
			set
			{
				this.deliveryStatusField = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x000250DB File Offset: 0x000232DB
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x000250E3 File Offset: 0x000232E3
		public string EventDescription
		{
			get
			{
				return this.eventDescriptionField;
			}
			set
			{
				this.eventDescriptionField = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x000250EC File Offset: 0x000232EC
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x000250F4 File Offset: 0x000232F4
		[XmlArrayItem("String", IsNullable = false)]
		public string[] EventData
		{
			get
			{
				return this.eventDataField;
			}
			set
			{
				this.eventDataField = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x000250FD File Offset: 0x000232FD
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x00025105 File Offset: 0x00023305
		public string Server
		{
			get
			{
				return this.serverField;
			}
			set
			{
				this.serverField = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x0002510E File Offset: 0x0002330E
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x00025116 File Offset: 0x00023316
		[XmlElement(DataType = "nonNegativeInteger")]
		public string InternalId
		{
			get
			{
				return this.internalIdField;
			}
			set
			{
				this.internalIdField = value;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0002511F File Offset: 0x0002331F
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x00025127 File Offset: 0x00023327
		public bool BccRecipient
		{
			get
			{
				return this.bccRecipientField;
			}
			set
			{
				this.bccRecipientField = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00025130 File Offset: 0x00023330
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x00025138 File Offset: 0x00023338
		[XmlIgnore]
		public bool BccRecipientSpecified
		{
			get
			{
				return this.bccRecipientFieldSpecified;
			}
			set
			{
				this.bccRecipientFieldSpecified = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00025141 File Offset: 0x00023341
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x00025149 File Offset: 0x00023349
		public bool HiddenRecipient
		{
			get
			{
				return this.hiddenRecipientField;
			}
			set
			{
				this.hiddenRecipientField = value;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00025152 File Offset: 0x00023352
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x0002515A File Offset: 0x0002335A
		[XmlIgnore]
		public bool HiddenRecipientSpecified
		{
			get
			{
				return this.hiddenRecipientFieldSpecified;
			}
			set
			{
				this.hiddenRecipientFieldSpecified = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00025163 File Offset: 0x00023363
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x0002516B File Offset: 0x0002336B
		public string UniquePathId
		{
			get
			{
				return this.uniquePathIdField;
			}
			set
			{
				this.uniquePathIdField = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00025174 File Offset: 0x00023374
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x0002517C File Offset: 0x0002337C
		public string RootAddress
		{
			get
			{
				return this.rootAddressField;
			}
			set
			{
				this.rootAddressField = value;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00025185 File Offset: 0x00023385
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0002518D File Offset: 0x0002338D
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

		// Token: 0x04000D47 RID: 3399
		private DateTime dateField;

		// Token: 0x04000D48 RID: 3400
		private EmailAddressType recipientField;

		// Token: 0x04000D49 RID: 3401
		private string deliveryStatusField;

		// Token: 0x04000D4A RID: 3402
		private string eventDescriptionField;

		// Token: 0x04000D4B RID: 3403
		private string[] eventDataField;

		// Token: 0x04000D4C RID: 3404
		private string serverField;

		// Token: 0x04000D4D RID: 3405
		private string internalIdField;

		// Token: 0x04000D4E RID: 3406
		private bool bccRecipientField;

		// Token: 0x04000D4F RID: 3407
		private bool bccRecipientFieldSpecified;

		// Token: 0x04000D50 RID: 3408
		private bool hiddenRecipientField;

		// Token: 0x04000D51 RID: 3409
		private bool hiddenRecipientFieldSpecified;

		// Token: 0x04000D52 RID: 3410
		private string uniquePathIdField;

		// Token: 0x04000D53 RID: 3411
		private string rootAddressField;

		// Token: 0x04000D54 RID: 3412
		private TrackingPropertyType[] propertiesField;
	}
}
