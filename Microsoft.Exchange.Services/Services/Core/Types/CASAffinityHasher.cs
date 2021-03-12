using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000243 RID: 579
	internal static class CASAffinityHasher
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0004B5DD File Offset: 0x000497DD
		internal static long ComputeIndex(string value, int numberOfBuckets)
		{
			return (long)(Math.Abs(value.ToUpperInvariant().GetHashCode()) % numberOfBuckets);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0004B5F2 File Offset: 0x000497F2
		internal static long ComputeIndex(SecurityIdentifier sid, int numberOfBuckets)
		{
			return CASAffinityHasher.ComputeIndex(sid.Value, numberOfBuckets);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0004B600 File Offset: 0x00049800
		internal static long ComputeIndex(Guid guid, int numberOfBuckets)
		{
			return CASAffinityHasher.ComputeIndex(guid.ToString("D"), numberOfBuckets);
		}
	}
}
