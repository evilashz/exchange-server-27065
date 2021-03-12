using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200018F RID: 399
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class NonIndexableItemDetailType
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000240BA File Offset: 0x000222BA
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x000240C2 File Offset: 0x000222C2
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x000240CB File Offset: 0x000222CB
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x000240D3 File Offset: 0x000222D3
		public ItemIndexErrorType ErrorCode
		{
			get
			{
				return this.errorCodeField;
			}
			set
			{
				this.errorCodeField = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x000240DC File Offset: 0x000222DC
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x000240E4 File Offset: 0x000222E4
		public string ErrorDescription
		{
			get
			{
				return this.errorDescriptionField;
			}
			set
			{
				this.errorDescriptionField = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x000240ED File Offset: 0x000222ED
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x000240F5 File Offset: 0x000222F5
		public bool IsPartiallyIndexed
		{
			get
			{
				return this.isPartiallyIndexedField;
			}
			set
			{
				this.isPartiallyIndexedField = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x000240FE File Offset: 0x000222FE
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x00024106 File Offset: 0x00022306
		public bool IsPermanentFailure
		{
			get
			{
				return this.isPermanentFailureField;
			}
			set
			{
				this.isPermanentFailureField = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0002410F File Offset: 0x0002230F
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x00024117 File Offset: 0x00022317
		public string SortValue
		{
			get
			{
				return this.sortValueField;
			}
			set
			{
				this.sortValueField = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00024120 File Offset: 0x00022320
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x00024128 File Offset: 0x00022328
		public int AttemptCount
		{
			get
			{
				return this.attemptCountField;
			}
			set
			{
				this.attemptCountField = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00024131 File Offset: 0x00022331
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x00024139 File Offset: 0x00022339
		public DateTime LastAttemptTime
		{
			get
			{
				return this.lastAttemptTimeField;
			}
			set
			{
				this.lastAttemptTimeField = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00024142 File Offset: 0x00022342
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x0002414A File Offset: 0x0002234A
		[XmlIgnore]
		public bool LastAttemptTimeSpecified
		{
			get
			{
				return this.lastAttemptTimeFieldSpecified;
			}
			set
			{
				this.lastAttemptTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00024153 File Offset: 0x00022353
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x0002415B File Offset: 0x0002235B
		public string AdditionalInfo
		{
			get
			{
				return this.additionalInfoField;
			}
			set
			{
				this.additionalInfoField = value;
			}
		}

		// Token: 0x04000BD3 RID: 3027
		private ItemIdType itemIdField;

		// Token: 0x04000BD4 RID: 3028
		private ItemIndexErrorType errorCodeField;

		// Token: 0x04000BD5 RID: 3029
		private string errorDescriptionField;

		// Token: 0x04000BD6 RID: 3030
		private bool isPartiallyIndexedField;

		// Token: 0x04000BD7 RID: 3031
		private bool isPermanentFailureField;

		// Token: 0x04000BD8 RID: 3032
		private string sortValueField;

		// Token: 0x04000BD9 RID: 3033
		private int attemptCountField;

		// Token: 0x04000BDA RID: 3034
		private DateTime lastAttemptTimeField;

		// Token: 0x04000BDB RID: 3035
		private bool lastAttemptTimeFieldSpecified;

		// Token: 0x04000BDC RID: 3036
		private string additionalInfoField;
	}
}
