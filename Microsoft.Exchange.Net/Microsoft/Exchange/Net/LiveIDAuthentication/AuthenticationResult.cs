using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Net.LiveIDAuthentication
{
	// Token: 0x02000760 RID: 1888
	internal sealed class AuthenticationResult : ResultData
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x0004D8CC File Offset: 0x0004BACC
		internal AuthenticationResult(BaseAuthenticationToken token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			this.token = token;
			this.exception = null;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0004D8F0 File Offset: 0x0004BAF0
		internal AuthenticationResult(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.exception = exception;
			this.token = null;
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x0004D914 File Offset: 0x0004BB14
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x0004D91C File Offset: 0x0004BB1C
		public AuthenticationToken Token
		{
			get
			{
				return this.token as AuthenticationToken;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x0004D929 File Offset: 0x0004BB29
		public SamlAuthenticationToken SamlToken
		{
			get
			{
				return this.token as SamlAuthenticationToken;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x0004D936 File Offset: 0x0004BB36
		public bool IsCanceled
		{
			get
			{
				return this.exception is OperationCanceledException;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x0004D946 File Offset: 0x0004BB46
		public bool IsSucceeded
		{
			get
			{
				return this.exception == null;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x0004D951 File Offset: 0x0004BB51
		public bool IsRetryable
		{
			get
			{
				return this.exception is TransientException || this.IsCanceled;
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0004D968 File Offset: 0x0004BB68
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

		// Token: 0x0400228F RID: 8847
		private readonly BaseAuthenticationToken token;

		// Token: 0x04002290 RID: 8848
		private readonly Exception exception;
	}
}
