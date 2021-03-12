using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AE6 RID: 2790
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AcrPropertyProfile
	{
		// Token: 0x06006526 RID: 25894 RVA: 0x001AD780 File Offset: 0x001AB980
		internal AcrPropertyProfile(AcrPropertyResolver resolver, bool requireChangeTracking, PropertyDefinition[] propertiesToResolve)
		{
			this.resolver = resolver;
			this.requireChangeTracking = requireChangeTracking;
			this.propertiesToResolve = (propertiesToResolve ?? Array<PropertyDefinition>.Empty);
			this.allProperties = Util.MergeArrays<PropertyDefinition>(new ICollection<PropertyDefinition>[]
			{
				propertiesToResolve,
				resolver.Dependencies
			});
		}

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x06006527 RID: 25895 RVA: 0x001AD7D1 File Offset: 0x001AB9D1
		internal PropertyDefinition[] AllProperties
		{
			get
			{
				return this.allProperties;
			}
		}

		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x06006528 RID: 25896 RVA: 0x001AD7D9 File Offset: 0x001AB9D9
		internal PropertyDefinition[] PropertiesToResolve
		{
			get
			{
				return this.propertiesToResolve;
			}
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x06006529 RID: 25897 RVA: 0x001AD7E1 File Offset: 0x001AB9E1
		internal bool RequireChangeTracking
		{
			get
			{
				return this.requireChangeTracking;
			}
		}

		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x0600652A RID: 25898 RVA: 0x001AD7E9 File Offset: 0x001AB9E9
		internal AcrPropertyResolver Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x04003999 RID: 14745
		private readonly AcrPropertyResolver resolver;

		// Token: 0x0400399A RID: 14746
		private readonly bool requireChangeTracking;

		// Token: 0x0400399B RID: 14747
		private readonly PropertyDefinition[] allProperties;

		// Token: 0x0400399C RID: 14748
		private readonly PropertyDefinition[] propertiesToResolve;

		// Token: 0x02000AE7 RID: 2791
		internal class ValuesToResolve
		{
			// Token: 0x0600652B RID: 25899 RVA: 0x001AD7F1 File Offset: 0x001AB9F1
			public ValuesToResolve(object clientValue, object serverValue, object originalValue)
			{
				this.ClientValue = clientValue;
				this.ServerValue = serverValue;
				this.OriginalValue = originalValue;
			}

			// Token: 0x0400399D RID: 14749
			public readonly object ClientValue;

			// Token: 0x0400399E RID: 14750
			public readonly object ServerValue;

			// Token: 0x0400399F RID: 14751
			public readonly object OriginalValue;
		}
	}
}
