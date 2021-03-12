using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000FD RID: 253
	internal class UpdateCommandSettings : CommandSettings
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00022BFC File Offset: 0x00020DFC
		public UpdateCommandSettings(PropertyUpdate propertyUpdate, StoreObject storeObject, bool suppressReadReceipts, IFeaturesManager featuresManager)
		{
			this.storeObject = storeObject;
			this.propertyUpdate = propertyUpdate;
			this.suppressReadReceipts = suppressReadReceipts;
			this.featuresManager = featuresManager;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00022C21 File Offset: 0x00020E21
		public StoreObject StoreObject
		{
			get
			{
				return this.storeObject;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00022C29 File Offset: 0x00020E29
		public PropertyUpdate PropertyUpdate
		{
			get
			{
				return this.propertyUpdate;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00022C31 File Offset: 0x00020E31
		public bool SuppressReadReceipts
		{
			get
			{
				return this.suppressReadReceipts;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00022C39 File Offset: 0x00020E39
		public IFeaturesManager FeaturesManager
		{
			get
			{
				return this.featuresManager;
			}
		}

		// Token: 0x040006E3 RID: 1763
		private readonly bool suppressReadReceipts;

		// Token: 0x040006E4 RID: 1764
		private StoreObject storeObject;

		// Token: 0x040006E5 RID: 1765
		private PropertyUpdate propertyUpdate;

		// Token: 0x040006E6 RID: 1766
		private IFeaturesManager featuresManager;
	}
}
