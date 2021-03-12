using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000A0 RID: 160
	internal class OpenMessageInstance
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x0004D3E4 File Offset: 0x0004B5E4
		public OpenMessageInstance(OpenMessageStates.OpenMessageState state, DataRow dataRow)
		{
			this.state = state;
			this.dataRow = dataRow;
			this.referenced = true;
			this.current = true;
			this.tentative = false;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0004D40F File Offset: 0x0004B60F
		public bool Referenced
		{
			get
			{
				return this.referenced;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0004D417 File Offset: 0x0004B617
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0004D41F File Offset: 0x0004B61F
		public bool Current
		{
			get
			{
				return this.current;
			}
			internal set
			{
				this.current = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0004D428 File Offset: 0x0004B628
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0004D430 File Offset: 0x0004B630
		public bool Tentative
		{
			get
			{
				return this.tentative;
			}
			internal set
			{
				this.tentative = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0004D439 File Offset: 0x0004B639
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0004D441 File Offset: 0x0004B641
		public bool Moved
		{
			get
			{
				return this.moved;
			}
			internal set
			{
				this.moved = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0004D44A File Offset: 0x0004B64A
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0004D452 File Offset: 0x0004B652
		public bool Deleted
		{
			get
			{
				return this.deleted;
			}
			internal set
			{
				this.deleted = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0004D45B File Offset: 0x0004B65B
		public DataRow DataRow
		{
			get
			{
				return this.dataRow;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0004D463 File Offset: 0x0004B663
		public OpenMessageStates.OpenMessageState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0004D46B File Offset: 0x0004B66B
		public void MarkUnreferenced()
		{
			this.referenced = false;
		}

		// Token: 0x0400047E RID: 1150
		private OpenMessageStates.OpenMessageState state;

		// Token: 0x0400047F RID: 1151
		private DataRow dataRow;

		// Token: 0x04000480 RID: 1152
		private bool referenced;

		// Token: 0x04000481 RID: 1153
		private bool current;

		// Token: 0x04000482 RID: 1154
		private bool tentative;

		// Token: 0x04000483 RID: 1155
		private bool moved;

		// Token: 0x04000484 RID: 1156
		private bool deleted;
	}
}
