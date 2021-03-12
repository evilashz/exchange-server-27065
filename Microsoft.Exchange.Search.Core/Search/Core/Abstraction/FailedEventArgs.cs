using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000011 RID: 17
	internal class FailedEventArgs : EventArgs
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000025EA File Offset: 0x000007EA
		internal FailedEventArgs(ComponentFailedException exception)
		{
			this.exception = exception;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000025F9 File Offset: 0x000007F9
		internal ComponentFailedException Reason
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000011 RID: 17
		private readonly ComponentFailedException exception;
	}
}
