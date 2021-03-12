using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000263 RID: 611
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OperationRetryManagerResult
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x0004085C File Offset: 0x0003EA5C
		public OperationRetryManagerResult(OperationRetryManagerResultCode resultCode, Exception ex)
		{
			if (ex == null && resultCode != OperationRetryManagerResultCode.Success)
			{
				throw new ArgumentNullException("ex", "ex can't be null if resultCode != ResultCode.Success");
			}
			this.resultCode = resultCode;
			this.exception = ex;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00040888 File Offset: 0x0003EA88
		public static OperationRetryManagerResult Success
		{
			get
			{
				return OperationRetryManagerResult.successOperationResult;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0004088F File Offset: 0x0003EA8F
		public bool Succeeded
		{
			get
			{
				return this.resultCode == OperationRetryManagerResultCode.Success;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0004089A File Offset: 0x0003EA9A
		public OperationRetryManagerResultCode ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x000408A2 File Offset: 0x0003EAA2
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000C08 RID: 3080
		private readonly OperationRetryManagerResultCode resultCode;

		// Token: 0x04000C09 RID: 3081
		private static OperationRetryManagerResult successOperationResult = new OperationRetryManagerResult(OperationRetryManagerResultCode.Success, null);

		// Token: 0x04000C0A RID: 3082
		private Exception exception;
	}
}
