using System;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020008A7 RID: 2215
	internal sealed class FailedAddress
	{
		// Token: 0x06002F73 RID: 12147 RVA: 0x0006BC82 File Offset: 0x00069E82
		public FailedAddress(string name, int errorCode, bool isTransientError)
		{
			this.name = name;
			this.errorCode = errorCode;
			this.isTransientError = isTransientError;
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x0006BC9F File Offset: 0x00069E9F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x0006BCA7 File Offset: 0x00069EA7
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x0006BCAF File Offset: 0x00069EAF
		public bool IsTransientError
		{
			get
			{
				return this.isTransientError;
			}
		}

		// Token: 0x04002921 RID: 10529
		private readonly string name;

		// Token: 0x04002922 RID: 10530
		private readonly int errorCode;

		// Token: 0x04002923 RID: 10531
		private readonly bool isTransientError;
	}
}
