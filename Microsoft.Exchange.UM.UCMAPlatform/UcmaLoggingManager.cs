using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000047 RID: 71
	internal class UcmaLoggingManager : UMLoggingManager
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		public UcmaLoggingManager(DiagnosticHelper diag)
		{
			this.diag = diag;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
		internal override void EnterTurn(string turnName)
		{
			this.diag.Trace("Enter turn: {0}", new object[]
			{
				turnName
			});
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000D6E1 File Offset: 0x0000B8E1
		internal override void ExitTurn()
		{
			this.diag.Trace("Exit turn", new object[0]);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000D6FC File Offset: 0x0000B8FC
		internal override void EnterTask(string name)
		{
			this.diag.Trace("Enter task: {0}", new object[]
			{
				name
			});
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000D728 File Offset: 0x0000B928
		internal override void ExitTask(UMNavigationState state, string message)
		{
			this.diag.Trace("Exit task state={0} message={1}", new object[]
			{
				state,
				message
			});
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000D75A File Offset: 0x0000B95A
		internal override void LogApplicationInformation(string format, params object[] args)
		{
			this.diag.Trace(format, args);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000D769 File Offset: 0x0000B969
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UcmaLoggingManager>(this);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000D771 File Offset: 0x0000B971
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x04000108 RID: 264
		private DiagnosticHelper diag;
	}
}
