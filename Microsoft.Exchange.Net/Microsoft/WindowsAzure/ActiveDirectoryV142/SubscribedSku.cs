using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x02000605 RID: 1541
	[DataServiceKey("objectId")]
	public class SubscribedSku
	{
		// Token: 0x06001B8A RID: 7050 RVA: 0x00032040 File Offset: 0x00030240
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

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00032070 File Offset: 0x00030270
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x00032078 File Offset: 0x00030278
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x00032081 File Offset: 0x00030281
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00032089 File Offset: 0x00030289
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

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x00032092 File Offset: 0x00030292
		// (set) Token: 0x06001B90 RID: 7056 RVA: 0x0003209A File Offset: 0x0003029A
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

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x000320A3 File Offset: 0x000302A3
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x000320CD File Offset: 0x000302CD
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

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000320DD File Offset: 0x000302DD
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x000320E5 File Offset: 0x000302E5
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

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x000320EE File Offset: 0x000302EE
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x000320F6 File Offset: 0x000302F6
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

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000320FF File Offset: 0x000302FF
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x00032107 File Offset: 0x00030307
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

		// Token: 0x04001C88 RID: 7304
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x04001C89 RID: 7305
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _consumedUnits;

		// Token: 0x04001C8A RID: 7306
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x04001C8B RID: 7307
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private LicenseUnitsDetail _prepaidUnits;

		// Token: 0x04001C8C RID: 7308
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _prepaidUnitsInitialized;

		// Token: 0x04001C8D RID: 7309
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServicePlanInfo> _servicePlans = new Collection<ServicePlanInfo>();

		// Token: 0x04001C8E RID: 7310
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _skuId;

		// Token: 0x04001C8F RID: 7311
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _skuPartNumber;
	}
}
