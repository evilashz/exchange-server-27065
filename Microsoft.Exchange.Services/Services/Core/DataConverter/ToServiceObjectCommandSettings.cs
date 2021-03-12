using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F8 RID: 248
	internal class ToServiceObjectCommandSettings : ToServiceObjectCommandSettingsBase
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x00022B00 File Offset: 0x00020D00
		public ToServiceObjectCommandSettings()
		{
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00022B08 File Offset: 0x00020D08
		public ToServiceObjectCommandSettings(PropertyPath propertyPath) : base(propertyPath)
		{
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00022B11 File Offset: 0x00020D11
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x00022B19 File Offset: 0x00020D19
		public StoreObject StoreObject
		{
			get
			{
				return this.storeObject;
			}
			set
			{
				this.storeObject = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00022B22 File Offset: 0x00020D22
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00022B2A File Offset: 0x00020D2A
		public ResponseShape ResponseShape
		{
			get
			{
				return this.responseShape;
			}
			set
			{
				this.responseShape = value;
			}
		}

		// Token: 0x040006D9 RID: 1753
		private StoreObject storeObject;

		// Token: 0x040006DA RID: 1754
		private ResponseShape responseShape;
	}
}
