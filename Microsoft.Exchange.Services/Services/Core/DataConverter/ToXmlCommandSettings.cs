using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000FB RID: 251
	internal class ToXmlCommandSettings : ToXmlCommandSettingsBase
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x00022B85 File Offset: 0x00020D85
		public ToXmlCommandSettings()
		{
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00022B8D File Offset: 0x00020D8D
		public ToXmlCommandSettings(PropertyPath propertyPath) : base(propertyPath)
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00022B96 File Offset: 0x00020D96
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x00022B9E File Offset: 0x00020D9E
		public IdAndSession IdAndSession
		{
			get
			{
				return this.idAndSession;
			}
			set
			{
				this.idAndSession = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00022BA7 File Offset: 0x00020DA7
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00022BAF File Offset: 0x00020DAF
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00022BB8 File Offset: 0x00020DB8
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00022BC0 File Offset: 0x00020DC0
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

		// Token: 0x040006DE RID: 1758
		private IdAndSession idAndSession;

		// Token: 0x040006DF RID: 1759
		private StoreObject storeObject;

		// Token: 0x040006E0 RID: 1760
		private ResponseShape responseShape;
	}
}
