using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime.ExceptionServices
{
	// Token: 0x0200077C RID: 1916
	public class FirstChanceExceptionEventArgs : EventArgs
	{
		// Token: 0x060053E2 RID: 21474 RVA: 0x00129733 File Offset: 0x00127933
		public FirstChanceExceptionEventArgs(Exception exception)
		{
			this.m_Exception = exception;
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060053E3 RID: 21475 RVA: 0x00129742 File Offset: 0x00127942
		public Exception Exception
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x04002664 RID: 9828
		private Exception m_Exception;
	}
}
