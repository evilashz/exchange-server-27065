using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	internal class ADObjectIdEqualityComparer : IEqualityComparer<ADObjectId>
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00017A5D File Offset: 0x00015C5D
		public bool Equals(ADObjectId x, ADObjectId y)
		{
			return ADObjectId.Equals(x, y);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00017A66 File Offset: 0x00015C66
		public int GetHashCode(ADObjectId obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return obj.GetHashCode();
		}

		// Token: 0x0400016F RID: 367
		internal static readonly ADObjectIdEqualityComparer Instance = new ADObjectIdEqualityComparer();
	}
}
