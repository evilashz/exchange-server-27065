using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F0 RID: 1008
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "Subscription", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class Subscription : IExtensibleDataObject
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0008D4DF File Offset: 0x0008B6DF
		// (set) Token: 0x060018E1 RID: 6369 RVA: 0x0008D4E7 File Offset: 0x0008B6E7
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0008D4F0 File Offset: 0x0008B6F0
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0008D4F8 File Offset: 0x0008B6F8
		[DataMember]
		public DateTime? DateCreated
		{
			get
			{
				return this.DateCreatedField;
			}
			set
			{
				this.DateCreatedField = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0008D501 File Offset: 0x0008B701
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x0008D509 File Offset: 0x0008B709
		[DataMember]
		public DateTime? NextLifecycleDate
		{
			get
			{
				return this.NextLifecycleDateField;
			}
			set
			{
				this.NextLifecycleDateField = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0008D512 File Offset: 0x0008B712
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x0008D51A File Offset: 0x0008B71A
		[DataMember]
		public Guid? ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0008D523 File Offset: 0x0008B723
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x0008D52B File Offset: 0x0008B72B
		[DataMember]
		public Guid? OcpSubscriptionId
		{
			get
			{
				return this.OcpSubscriptionIdField;
			}
			set
			{
				this.OcpSubscriptionIdField = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0008D534 File Offset: 0x0008B734
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x0008D53C File Offset: 0x0008B73C
		[DataMember]
		public ServiceStatus[] ServiceStatus
		{
			get
			{
				return this.ServiceStatusField;
			}
			set
			{
				this.ServiceStatusField = value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0008D545 File Offset: 0x0008B745
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x0008D54D File Offset: 0x0008B74D
		[DataMember]
		public Guid? SkuId
		{
			get
			{
				return this.SkuIdField;
			}
			set
			{
				this.SkuIdField = value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0008D556 File Offset: 0x0008B756
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0008D55E File Offset: 0x0008B75E
		[DataMember]
		public string SkuPartNumber
		{
			get
			{
				return this.SkuPartNumberField;
			}
			set
			{
				this.SkuPartNumberField = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0008D567 File Offset: 0x0008B767
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x0008D56F File Offset: 0x0008B76F
		[DataMember]
		public SubscriptionStatus Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0008D578 File Offset: 0x0008B778
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0008D580 File Offset: 0x0008B780
		[DataMember]
		public int TotalLicenses
		{
			get
			{
				return this.TotalLicensesField;
			}
			set
			{
				this.TotalLicensesField = value;
			}
		}

		// Token: 0x04001172 RID: 4466
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001173 RID: 4467
		private DateTime? DateCreatedField;

		// Token: 0x04001174 RID: 4468
		private DateTime? NextLifecycleDateField;

		// Token: 0x04001175 RID: 4469
		private Guid? ObjectIdField;

		// Token: 0x04001176 RID: 4470
		private Guid? OcpSubscriptionIdField;

		// Token: 0x04001177 RID: 4471
		private ServiceStatus[] ServiceStatusField;

		// Token: 0x04001178 RID: 4472
		private Guid? SkuIdField;

		// Token: 0x04001179 RID: 4473
		private string SkuPartNumberField;

		// Token: 0x0400117A RID: 4474
		private SubscriptionStatus StatusField;

		// Token: 0x0400117B RID: 4475
		private int TotalLicensesField;
	}
}
