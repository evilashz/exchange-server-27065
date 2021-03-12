using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000317 RID: 791
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceRequestResume : ReplicaInstanceQueuedItem
	{
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00096DB1 File Offset: 0x00094FB1
		public override string Name
		{
			get
			{
				return ReplayStrings.ResumeOperationName;
			}
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00096DBD File Offset: 0x00094FBD
		public ReplicaInstanceRequestResume(ReplicaInstance instance) : base(instance)
		{
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00096DC6 File Offset: 0x00094FC6
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x00096DCE File Offset: 0x00094FCE
		internal DatabaseCopyActionFlags Flags { get; set; }

		// Token: 0x060020A2 RID: 8354 RVA: 0x00096DD7 File Offset: 0x00094FD7
		protected override void CheckOperationApplicable()
		{
			if (base.ReplicaInstance.CurrentContext.IsFailoverPending())
			{
				throw new ReplayServiceResumeInvalidDuringMoveException(base.DbCopyName);
			}
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00096DF7 File Offset: 0x00094FF7
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();
			base.ReplicaInstance.RequestResume(this.Flags);
		}
	}
}
