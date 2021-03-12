using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000EF RID: 239
	public abstract class PrivatePropertyBag : PropertyBag
	{
		// Token: 0x06000982 RID: 2434 RVA: 0x0002C1CD File Offset: 0x0002A3CD
		protected PrivatePropertyBag(bool changeTrackingEnabled) : base(changeTrackingEnabled)
		{
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0002C1D6 File Offset: 0x0002A3D6
		protected override Dictionary<ushort, KeyValuePair<StorePropTag, object>> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0002C1DE File Offset: 0x0002A3DE
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x0002C1E6 File Offset: 0x0002A3E6
		protected override bool PropertiesDirty
		{
			get
			{
				return this.propertiesDirty;
			}
			set
			{
				this.propertiesDirty = value;
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002C1EF File Offset: 0x0002A3EF
		protected override void AssignPropertiesToUse(Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties)
		{
			this.properties = properties;
		}

		// Token: 0x04000552 RID: 1362
		private Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties;

		// Token: 0x04000553 RID: 1363
		private bool propertiesDirty;
	}
}
