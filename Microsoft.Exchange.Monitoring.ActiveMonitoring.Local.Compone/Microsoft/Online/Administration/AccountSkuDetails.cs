using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F2 RID: 1010
	[DataContract(Name = "AccountSkuDetails", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class AccountSkuDetails : IExtensibleDataObject
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0008D591 File Offset: 0x0008B791
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0008D599 File Offset: 0x0008B799
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

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0008D5A2 File Offset: 0x0008B7A2
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0008D5AA File Offset: 0x0008B7AA
		[DataMember]
		public string AccountName
		{
			get
			{
				return this.AccountNameField;
			}
			set
			{
				this.AccountNameField = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0008D5B3 File Offset: 0x0008B7B3
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0008D5BB File Offset: 0x0008B7BB
		[DataMember]
		public Guid AccountObjectId
		{
			get
			{
				return this.AccountObjectIdField;
			}
			set
			{
				this.AccountObjectIdField = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0008D5C4 File Offset: 0x0008B7C4
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x0008D5CC File Offset: 0x0008B7CC
		[DataMember]
		public string AccountSkuId
		{
			get
			{
				return this.AccountSkuIdField;
			}
			set
			{
				this.AccountSkuIdField = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0008D5D5 File Offset: 0x0008B7D5
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x0008D5DD File Offset: 0x0008B7DD
		[DataMember]
		public int ActiveUnits
		{
			get
			{
				return this.ActiveUnitsField;
			}
			set
			{
				this.ActiveUnitsField = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0008D5E6 File Offset: 0x0008B7E6
		// (set) Token: 0x06001900 RID: 6400 RVA: 0x0008D5EE File Offset: 0x0008B7EE
		[DataMember]
		public int ConsumedUnits
		{
			get
			{
				return this.ConsumedUnitsField;
			}
			set
			{
				this.ConsumedUnitsField = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0008D5F7 File Offset: 0x0008B7F7
		// (set) Token: 0x06001902 RID: 6402 RVA: 0x0008D5FF File Offset: 0x0008B7FF
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

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0008D608 File Offset: 0x0008B808
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x0008D610 File Offset: 0x0008B810
		[DataMember]
		public Guid SkuId
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

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0008D619 File Offset: 0x0008B819
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x0008D621 File Offset: 0x0008B821
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

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0008D62A File Offset: 0x0008B82A
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0008D632 File Offset: 0x0008B832
		[DataMember]
		public Guid[] SubscriptionIds
		{
			get
			{
				return this.SubscriptionIdsField;
			}
			set
			{
				this.SubscriptionIdsField = value;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0008D63B File Offset: 0x0008B83B
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0008D643 File Offset: 0x0008B843
		[DataMember]
		public int SuspendedUnits
		{
			get
			{
				return this.SuspendedUnitsField;
			}
			set
			{
				this.SuspendedUnitsField = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0008D64C File Offset: 0x0008B84C
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0008D654 File Offset: 0x0008B854
		[DataMember]
		public SkuTargetClass TargetClass
		{
			get
			{
				return this.TargetClassField;
			}
			set
			{
				this.TargetClassField = value;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0008D65D File Offset: 0x0008B85D
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0008D665 File Offset: 0x0008B865
		[DataMember]
		public int WarningUnits
		{
			get
			{
				return this.WarningUnitsField;
			}
			set
			{
				this.WarningUnitsField = value;
			}
		}

		// Token: 0x04001182 RID: 4482
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001183 RID: 4483
		private string AccountNameField;

		// Token: 0x04001184 RID: 4484
		private Guid AccountObjectIdField;

		// Token: 0x04001185 RID: 4485
		private string AccountSkuIdField;

		// Token: 0x04001186 RID: 4486
		private int ActiveUnitsField;

		// Token: 0x04001187 RID: 4487
		private int ConsumedUnitsField;

		// Token: 0x04001188 RID: 4488
		private ServiceStatus[] ServiceStatusField;

		// Token: 0x04001189 RID: 4489
		private Guid SkuIdField;

		// Token: 0x0400118A RID: 4490
		private string SkuPartNumberField;

		// Token: 0x0400118B RID: 4491
		private Guid[] SubscriptionIdsField;

		// Token: 0x0400118C RID: 4492
		private int SuspendedUnitsField;

		// Token: 0x0400118D RID: 4493
		private SkuTargetClass TargetClassField;

		// Token: 0x0400118E RID: 4494
		private int WarningUnitsField;
	}
}
