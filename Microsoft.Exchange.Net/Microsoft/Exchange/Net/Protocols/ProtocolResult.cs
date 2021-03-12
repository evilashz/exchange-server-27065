using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.Protocols
{
	// Token: 0x02000829 RID: 2089
	internal sealed class ProtocolResult
	{
		// Token: 0x06002C4E RID: 11342 RVA: 0x000649CC File Offset: 0x00062BCC
		internal ProtocolResult(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.exception = exception;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000649E9 File Offset: 0x00062BE9
		internal ProtocolResult(ResultData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.data = data;
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x00064A06 File Offset: 0x00062C06
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x00064A0E File Offset: 0x00062C0E
		public ResultData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x00064A16 File Offset: 0x00062C16
		public bool IsCanceled
		{
			get
			{
				return this.exception is OperationCanceledException;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x00064A26 File Offset: 0x00062C26
		public bool IsSucceeded
		{
			get
			{
				return this.exception == null;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x00064A31 File Offset: 0x00062C31
		public bool IsRetryable
		{
			get
			{
				return this.exception is TransientException;
			}
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x00064A41 File Offset: 0x00062C41
		public override string ToString()
		{
			if (this.IsCanceled)
			{
				return "Canceled";
			}
			if (this.IsSucceeded)
			{
				return "Success";
			}
			return this.exception.GetType().FullName;
		}

		// Token: 0x0400268F RID: 9871
		private readonly ResultData data;

		// Token: 0x04002690 RID: 9872
		private readonly Exception exception;
	}
}
