using System;
using Microsoft.Exchange.Rpc.SharedCache;

namespace Microsoft.Exchange.SharedCache.Exceptions
{
	// Token: 0x0200001B RID: 27
	public class ValueTimestampTooFarIntoFutureException : SharedCacheExceptionBase
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public ValueTimestampTooFarIntoFutureException(string key, DateTime dateTime, DateTime maxDateTime) : base(ResponseCode.InvalidInsertTimestamp, string.Concat(new string[]
		{
			"Value timestamp is too far into the future. (received=",
			dateTime.ToString(),
			", max=",
			maxDateTime.ToString(),
			")"
		}))
		{
			this.key = key;
			this.dateTime = dateTime;
			this.maxDateTime = maxDateTime;
		}

		// Token: 0x0400004E RID: 78
		private readonly string key;

		// Token: 0x0400004F RID: 79
		private readonly DateTime dateTime;

		// Token: 0x04000050 RID: 80
		private readonly DateTime maxDateTime;
	}
}
