using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005B8 RID: 1464
	[DataServiceKey("objectId")]
	public class SubscribedSku
	{
		// Token: 0x0600162E RID: 5678 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static SubscribedSku CreateSubscribedSku(string objectId, Collection<ServicePlanInfo> servicePlans)
		{
			SubscribedSku subscribedSku = new SubscribedSku();
			subscribedSku.objectId = objectId;
			if (servicePlans == null)
			{
				throw new ArgumentNullException("servicePlans");
			}
			subscribedSku.servicePlans = servicePlans;
			return subscribedSku;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0002DCE4 File Offset: 0x0002BEE4
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x0002DCEC File Offset: 0x0002BEEC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string capabilityStatus
		{
			get
			{
				return this._capabilityStatus;
			}
			set
			{
				this._capabilityStatus = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x0002DCF5 File Offset: 0x0002BEF5
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x0002DCFD File Offset: 0x0002BEFD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? consumedUnits
		{
			get
			{
				return this._consumedUnits;
			}
			set
			{
				this._consumedUnits = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0002DD06 File Offset: 0x0002BF06
		// (set) Token: 0x06001634 RID: 5684 RVA: 0x0002DD0E File Offset: 0x0002BF0E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string objectId
		{
			get
			{
				return this._objectId;
			}
			set
			{
				this._objectId = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0002DD17 File Offset: 0x0002BF17
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x0002DD41 File Offset: 0x0002BF41
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public LicenseUnitsDetail prepaidUnits
		{
			get
			{
				if (this._prepaidUnits == null && !this._prepaidUnitsInitialized)
				{
					this._prepaidUnits = new LicenseUnitsDetail();
					this._prepaidUnitsInitialized = true;
				}
				return this._prepaidUnits;
			}
			set
			{
				this._prepaidUnits = value;
				this._prepaidUnitsInitialized = true;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0002DD51 File Offset: 0x0002BF51
		// (set) Token: 0x06001638 RID: 5688 RVA: 0x0002DD59 File Offset: 0x0002BF59
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ServicePlanInfo> servicePlans
		{
			get
			{
				return this._servicePlans;
			}
			set
			{
				this._servicePlans = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0002DD62 File Offset: 0x0002BF62
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x0002DD6A File Offset: 0x0002BF6A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? skuId
		{
			get
			{
				return this._skuId;
			}
			set
			{
				this._skuId = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0002DD73 File Offset: 0x0002BF73
		// (set) Token: 0x0600163C RID: 5692 RVA: 0x0002DD7B File Offset: 0x0002BF7B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string skuPartNumber
		{
			get
			{
				return this._skuPartNumber;
			}
			set
			{
				this._skuPartNumber = value;
			}
		}

		// Token: 0x04001A0D RID: 6669
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x04001A0E RID: 6670
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _consumedUnits;

		// Token: 0x04001A0F RID: 6671
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x04001A10 RID: 6672
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private LicenseUnitsDetail _prepaidUnits;

		// Token: 0x04001A11 RID: 6673
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _prepaidUnitsInitialized;

		// Token: 0x04001A12 RID: 6674
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServicePlanInfo> _servicePlans = new Collection<ServicePlanInfo>();

		// Token: 0x04001A13 RID: 6675
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _skuId;

		// Token: 0x04001A14 RID: 6676
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _skuPartNumber;
	}
}
