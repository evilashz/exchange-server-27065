using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000316 RID: 790
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceRequestSuspendAndFail : ReplicaInstanceQueuedItem
	{
		// Token: 0x0600208B RID: 8331 RVA: 0x00096CE5 File Offset: 0x00094EE5
		public ReplicaInstanceRequestSuspendAndFail(ReplicaInstance instance) : base(instance)
		{
			this.SuspendCopy = true;
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x00096CF5 File Offset: 0x00094EF5
		// (set) Token: 0x0600208D RID: 8333 RVA: 0x00096CFD File Offset: 0x00094EFD
		internal uint ErrorEventId { get; set; }

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x00096D06 File Offset: 0x00094F06
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x00096D0E File Offset: 0x00094F0E
		internal string ErrorMessage { get; set; }

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x00096D17 File Offset: 0x00094F17
		// (set) Token: 0x06002091 RID: 8337 RVA: 0x00096D1F File Offset: 0x00094F1F
		internal string SuspendComment { get; set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x00096D28 File Offset: 0x00094F28
		// (set) Token: 0x06002093 RID: 8339 RVA: 0x00096D30 File Offset: 0x00094F30
		internal bool PreserveExistingError { get; set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x00096D39 File Offset: 0x00094F39
		// (set) Token: 0x06002095 RID: 8341 RVA: 0x00096D41 File Offset: 0x00094F41
		internal bool SuspendCopy { get; set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x00096D4A File Offset: 0x00094F4A
		// (set) Token: 0x06002097 RID: 8343 RVA: 0x00096D52 File Offset: 0x00094F52
		internal bool BlockResume { get; set; }

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x00096D5B File Offset: 0x00094F5B
		// (set) Token: 0x06002099 RID: 8345 RVA: 0x00096D63 File Offset: 0x00094F63
		internal bool BlockReseed { get; set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x00096D6C File Offset: 0x00094F6C
		// (set) Token: 0x0600209B RID: 8347 RVA: 0x00096D74 File Offset: 0x00094F74
		internal bool BlockInPlaceReseed { get; set; }

		// Token: 0x0600209C RID: 8348 RVA: 0x00096D7D File Offset: 0x00094F7D
		protected override void CheckOperationApplicable()
		{
			if (base.ReplicaInstance.CurrentContext.IsFailoverPending())
			{
				throw new ReplayServiceSuspendInvalidDuringMoveException(base.DbCopyName);
			}
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00096D9D File Offset: 0x00094F9D
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			base.ReplicaInstance.RequestSuspendAndFail(this);
		}
	}
}
