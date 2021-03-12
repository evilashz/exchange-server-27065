using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001F3 RID: 499
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullResourceTracker : IResourceTracker
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00020AA4 File Offset: 0x0001ECA4
		private NullResourceTracker()
		{
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00020AAC File Offset: 0x0001ECAC
		public bool TryReserveMemory(int size)
		{
			return true;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00020AAF File Offset: 0x0001ECAF
		public int StreamSizeLimit
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00020AB6 File Offset: 0x0001ECB6
		public static IResourceTracker Instance
		{
			get
			{
				return NullResourceTracker.instance;
			}
		}

		// Token: 0x040004A1 RID: 1185
		private static readonly IResourceTracker instance = new NullResourceTracker();
	}
}
