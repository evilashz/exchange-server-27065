using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000009 RID: 9
	public class FailFastException : Exception
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000023C4 File Offset: 0x000005C4
		internal FailFastException(string message, string stackTrace) : base(message)
		{
			this.stackTrace = stackTrace;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023D4 File Offset: 0x000005D4
		public override string StackTrace
		{
			get
			{
				if (string.IsNullOrEmpty(base.StackTrace))
				{
					return this.stackTrace;
				}
				return base.StackTrace;
			}
		}

		// Token: 0x0400000D RID: 13
		private string stackTrace;
	}
}
