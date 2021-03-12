using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200008D RID: 141
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ResourceTracker : IResourceTracker
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x000286F0 File Offset: 0x000268F0
		public ResourceTracker(int streamSizeLimit)
		{
			this.streamSizeLimit = streamSizeLimit;
			this.sharedMemoryLimit = (long)streamSizeLimit * 5L;
			if (this.sharedMemoryLimit > 2147483647L)
			{
				this.sharedMemoryLimit = 2147483647L;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00028724 File Offset: 0x00026924
		public bool TryReserveMemory(int size)
		{
			if (this.reservedSharedMemory + (long)size < 0L)
			{
				throw new ArgumentOutOfRangeException("size", string.Format("Attempted to return more memory than was originally reserved. Current reserved memory = {0}. Reserve memory request = {1}.", this.reservedSharedMemory, size));
			}
			if (this.reservedSharedMemory + (long)size > this.sharedMemoryLimit)
			{
				return false;
			}
			this.reservedSharedMemory += (long)size;
			return true;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00028787 File Offset: 0x00026987
		public int StreamSizeLimit
		{
			get
			{
				return this.streamSizeLimit;
			}
		}

		// Token: 0x04000264 RID: 612
		private readonly long sharedMemoryLimit;

		// Token: 0x04000265 RID: 613
		private readonly int streamSizeLimit;

		// Token: 0x04000266 RID: 614
		private long reservedSharedMemory;
	}
}
