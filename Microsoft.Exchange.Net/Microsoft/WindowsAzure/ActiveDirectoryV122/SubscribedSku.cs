using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005D5 RID: 1493
	[DataServiceKey("objectId")]
	public class SubscribedSku
	{
		// Token: 0x06001830 RID: 6192 RVA: 0x0002F580 File Offset: 0x0002D780
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

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x0002F5B8 File Offset: 0x0002D7B8
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

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0002F5C1 File Offset: 0x0002D7C1
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x0002F5C9 File Offset: 0x0002D7C9
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

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0002F5D2 File Offset: 0x0002D7D2
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x0002F5DA File Offset: 0x0002D7DA
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

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0002F5E3 File Offset: 0x0002D7E3
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x0002F60D File Offset: 0x0002D80D
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

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0002F61D File Offset: 0x0002D81D
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x0002F625 File Offset: 0x0002D825
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

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0002F62E File Offset: 0x0002D82E
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x0002F636 File Offset: 0x0002D836
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

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0002F63F File Offset: 0x0002D83F
		// (set) Token: 0x0600183E RID: 6206 RVA: 0x0002F647 File Offset: 0x0002D847
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

		// Token: 0x04001AF9 RID: 6905
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x04001AFA RID: 6906
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _consumedUnits;

		// Token: 0x04001AFB RID: 6907
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x04001AFC RID: 6908
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private LicenseUnitsDetail _prepaidUnits;

		// Token: 0x04001AFD RID: 6909
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _prepaidUnitsInitialized;

		// Token: 0x04001AFE RID: 6910
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServicePlanInfo> _servicePlans = new Collection<ServicePlanInfo>();

		// Token: 0x04001AFF RID: 6911
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _skuId;

		// Token: 0x04001B00 RID: 6912
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _skuPartNumber;
	}
}
