using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000122 RID: 290
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ReminderMessageDataType
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00021DDC File Offset: 0x0001FFDC
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x00021DE4 File Offset: 0x0001FFE4
		public string ReminderText
		{
			get
			{
				return this.reminderTextField;
			}
			set
			{
				this.reminderTextField = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00021DED File Offset: 0x0001FFED
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00021DF5 File Offset: 0x0001FFF5
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

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00021DFE File Offset: 0x0001FFFE
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00021E06 File Offset: 0x00020006
		public DateTime StartTime
		{
			get
			{
				return this.startTimeField;
			}
			set
			{
				this.startTimeField = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00021E0F File Offset: 0x0002000F
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00021E17 File Offset: 0x00020017
		[XmlIgnore]
		public bool StartTimeSpecified
		{
			get
			{
				return this.startTimeFieldSpecified;
			}
			set
			{
				this.startTimeFieldSpecified = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00021E20 File Offset: 0x00020020
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x00021E28 File Offset: 0x00020028
		public DateTime EndTime
		{
			get
			{
				return this.endTimeField;
			}
			set
			{
				this.endTimeField = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00021E31 File Offset: 0x00020031
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x00021E39 File Offset: 0x00020039
		[XmlIgnore]
		public bool EndTimeSpecified
		{
			get
			{
				return this.endTimeFieldSpecified;
			}
			set
			{
				this.endTimeFieldSpecified = value;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00021E42 File Offset: 0x00020042
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x00021E4A File Offset: 0x0002004A
		public ItemIdType AssociatedCalendarItemId
		{
			get
			{
				return this.associatedCalendarItemIdField;
			}
			set
			{
				this.associatedCalendarItemIdField = value;
			}
		}

		// Token: 0x04000913 RID: 2323
		private string reminderTextField;

		// Token: 0x04000914 RID: 2324
		private string locationField;

		// Token: 0x04000915 RID: 2325
		private DateTime startTimeField;

		// Token: 0x04000916 RID: 2326
		private bool startTimeFieldSpecified;

		// Token: 0x04000917 RID: 2327
		private DateTime endTimeField;

		// Token: 0x04000918 RID: 2328
		private bool endTimeFieldSpecified;

		// Token: 0x04000919 RID: 2329
		private ItemIdType associatedCalendarItemIdField;
	}
}
