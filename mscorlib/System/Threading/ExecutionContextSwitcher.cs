using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Principal;

namespace System.Threading
{
	// Token: 0x020004C9 RID: 1225
	internal struct ExecutionContextSwitcher
	{
		// Token: 0x06003ACB RID: 15051 RVA: 0x000DE7BC File Offset: 0x000DC9BC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			try
			{
				this.Undo();
			}
			catch (Exception exception)
			{
				if (!AppContextSwitches.UseLegacyExecutionContextBehaviorUponUndoFailure)
				{
					Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"), exception);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000DE800 File Offset: 0x000DCA00
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void Undo()
		{
			if (this.thread == null)
			{
				return;
			}
			Thread thread = this.thread;
			if (this.hecsw != null)
			{
				HostExecutionContextSwitcher.Undo(this.hecsw);
			}
			ExecutionContext.Reader executionContextReader = thread.GetExecutionContextReader();
			thread.SetExecutionContext(this.outerEC, this.outerECBelongsToScope);
			if (this.scsw.currSC != null)
			{
				this.scsw.Undo();
			}
			if (this.wiIsValid)
			{
				SecurityContext.RestoreCurrentWI(this.outerEC, executionContextReader, this.wi, this.cachedAlwaysFlowImpersonationPolicy);
			}
			this.thread = null;
			ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), this.outerEC.DangerousGetRawExecutionContext());
		}

		// Token: 0x040018C7 RID: 6343
		internal ExecutionContext.Reader outerEC;

		// Token: 0x040018C8 RID: 6344
		internal bool outerECBelongsToScope;

		// Token: 0x040018C9 RID: 6345
		internal SecurityContextSwitcher scsw;

		// Token: 0x040018CA RID: 6346
		internal object hecsw;

		// Token: 0x040018CB RID: 6347
		internal WindowsIdentity wi;

		// Token: 0x040018CC RID: 6348
		internal bool cachedAlwaysFlowImpersonationPolicy;

		// Token: 0x040018CD RID: 6349
		internal bool wiIsValid;

		// Token: 0x040018CE RID: 6350
		internal Thread thread;
	}
}
