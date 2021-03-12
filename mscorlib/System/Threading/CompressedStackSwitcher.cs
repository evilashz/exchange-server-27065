using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004C2 RID: 1218
	internal struct CompressedStackSwitcher : IDisposable
	{
		// Token: 0x06003A84 RID: 14980 RVA: 0x000DDD90 File Offset: 0x000DBF90
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is CompressedStackSwitcher))
			{
				return false;
			}
			CompressedStackSwitcher compressedStackSwitcher = (CompressedStackSwitcher)obj;
			return this.curr_CS == compressedStackSwitcher.curr_CS && this.prev_CS == compressedStackSwitcher.prev_CS && this.prev_ADStack == compressedStackSwitcher.prev_ADStack;
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000DDDE0 File Offset: 0x000DBFE0
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000DDDF3 File Offset: 0x000DBFF3
		public static bool operator ==(CompressedStackSwitcher c1, CompressedStackSwitcher c2)
		{
			return c1.Equals(c2);
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000DDE08 File Offset: 0x000DC008
		public static bool operator !=(CompressedStackSwitcher c1, CompressedStackSwitcher c2)
		{
			return !c1.Equals(c2);
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000DDE20 File Offset: 0x000DC020
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Undo();
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000DDE28 File Offset: 0x000DC028
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

		// Token: 0x06003A8A RID: 14986 RVA: 0x000DDE6C File Offset: 0x000DC06C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Undo()
		{
			if (this.curr_CS == null && this.prev_CS == null)
			{
				return;
			}
			if (this.prev_ADStack != (IntPtr)0)
			{
				CompressedStack.RestoreAppDomainStack(this.prev_ADStack);
			}
			CompressedStack.SetCompressedStackThread(this.prev_CS);
			this.prev_CS = null;
			this.curr_CS = null;
			this.prev_ADStack = (IntPtr)0;
		}

		// Token: 0x040018BA RID: 6330
		internal CompressedStack curr_CS;

		// Token: 0x040018BB RID: 6331
		internal CompressedStack prev_CS;

		// Token: 0x040018BC RID: 6332
		internal IntPtr prev_ADStack;
	}
}
