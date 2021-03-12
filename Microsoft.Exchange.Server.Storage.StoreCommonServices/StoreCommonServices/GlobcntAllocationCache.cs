using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200007C RID: 124
	internal class GlobcntAllocationCache : IComponentData
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0001D078 File Offset: 0x0001B278
		public GlobcntAllocationCache(ulong nextUnallocated, ulong maxReserved)
		{
			this.next = nextUnallocated;
			this.max = maxReserved;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001D08E File Offset: 0x0001B28E
		public uint CountAvailable
		{
			get
			{
				return (uint)(this.max - this.next);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001D09E File Offset: 0x0001B29E
		public ulong Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001D0A6 File Offset: 0x0001B2A6
		public void SetMax(ulong maxReserved)
		{
			this.max = maxReserved;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001D0B0 File Offset: 0x0001B2B0
		public ulong Allocate(uint numAllocate)
		{
			ulong result = this.next;
			this.next += (ulong)numAllocate;
			return result;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		bool IComponentData.DoCleanup(Context context)
		{
			return false;
		}

		// Token: 0x040003B8 RID: 952
		private ulong next;

		// Token: 0x040003B9 RID: 953
		private ulong max;
	}
}
