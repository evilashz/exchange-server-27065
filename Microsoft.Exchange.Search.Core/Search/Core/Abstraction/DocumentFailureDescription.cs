using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000002 RID: 2
	internal class DocumentFailureDescription
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private DocumentFailureDescription()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		public ComponentException Exception { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020F1 File Offset: 0x000002F1
		public int FailureCode { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020FA File Offset: 0x000002FA
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002102 File Offset: 0x00000302
		public bool IsWarning { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000210B File Offset: 0x0000030B
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002113 File Offset: 0x00000313
		public bool IsPermanent { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002124 File Offset: 0x00000324
		public string AdditionalInfo { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212D File Offset: 0x0000032D
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002135 File Offset: 0x00000335
		public ExDateTime RetryTime { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public static DocumentFailureDescription CreatePermanentError(ComponentException ex, int errorCode, string additionalInfo)
		{
			Util.ThrowOnNullArgument(ex, "ex");
			return new DocumentFailureDescription
			{
				Exception = ex,
				IsWarning = false,
				IsPermanent = true,
				FailureCode = errorCode,
				AdditionalInfo = additionalInfo,
				RetryTime = ExDateTime.MaxValue
			};
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002190 File Offset: 0x00000390
		public static DocumentFailureDescription CreateTransientError(ComponentException ex, TimeSpan retryDelay)
		{
			Util.ThrowOnNullArgument(ex, "ex");
			return new DocumentFailureDescription
			{
				Exception = ex,
				IsWarning = false,
				IsPermanent = false,
				FailureCode = 0,
				AdditionalInfo = ex.ToString(),
				RetryTime = ExDateTime.UtcNow + retryDelay
			};
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E8 File Offset: 0x000003E8
		public static DocumentFailureDescription CreatePermanentWarning(ComponentException ex, int errorCode, string additionalInfo)
		{
			Util.ThrowOnNullArgument(ex, "ex");
			return new DocumentFailureDescription
			{
				Exception = ex,
				IsWarning = true,
				IsPermanent = true,
				FailureCode = errorCode,
				AdditionalInfo = additionalInfo,
				RetryTime = ExDateTime.MaxValue
			};
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002238 File Offset: 0x00000438
		public static DocumentFailureDescription CreateTransientWarning(ComponentException ex, TimeSpan retryDelay)
		{
			Util.ThrowOnNullArgument(ex, "ex");
			return new DocumentFailureDescription
			{
				Exception = ex,
				IsWarning = true,
				IsPermanent = false,
				FailureCode = 0,
				AdditionalInfo = ex.ToString(),
				RetryTime = ExDateTime.UtcNow + retryDelay
			};
		}
	}
}
