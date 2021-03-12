using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay.Dumpster
{
	// Token: 0x0200017F RID: 383
	internal class SafetyNetRequestKeyComparer : IEqualityComparer<SafetyNetRequestKey>
	{
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00042546 File Offset: 0x00040746
		public static SafetyNetRequestKeyComparer Instance
		{
			get
			{
				return SafetyNetRequestKeyComparer.s_instance;
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0004254D File Offset: 0x0004074D
		private SafetyNetRequestKeyComparer()
		{
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00042558 File Offset: 0x00040758
		public bool Equals(SafetyNetRequestKey x, SafetyNetRequestKey y)
		{
			return SharedHelper.StringIEquals(x.ServerName, y.ServerName) && SharedHelper.StringIEquals(x.UniqueStr, y.UniqueStr) && object.Equals(x.RequestCreationTimeUtc, y.RequestCreationTimeUtc);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000425A8 File Offset: 0x000407A8
		public int GetHashCode(SafetyNetRequestKey key)
		{
			return key.ServerName.GetHashCode() ^ key.RequestCreationTimeUtc.GetHashCode() ^ key.UniqueStr.GetHashCode();
		}

		// Token: 0x04000659 RID: 1625
		private static readonly SafetyNetRequestKeyComparer s_instance = new SafetyNetRequestKeyComparer();
	}
}
