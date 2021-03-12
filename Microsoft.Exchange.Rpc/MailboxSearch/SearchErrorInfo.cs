using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x0200027E RID: 638
	internal class SearchErrorInfo
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x0002A3AC File Offset: 0x000297AC
		public SearchErrorInfo(int errorCode, Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.m_errorCode = errorCode;
			this.m_message = exception.Message;
			this.m_exception = exception;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002A384 File Offset: 0x00029784
		public SearchErrorInfo(int errorCode, string message)
		{
			this.m_errorCode = errorCode;
			this.m_message = message;
			this.m_exception = null;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002A35C File Offset: 0x0002975C
		public SearchErrorInfo()
		{
			this.m_errorCode = 0;
			this.m_message = null;
			this.m_exception = null;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002A3E8 File Offset: 0x000297E8
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0002A3FC File Offset: 0x000297FC
		public int ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
			set
			{
				this.m_errorCode = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0002A410 File Offset: 0x00029810
		public bool Succeeded
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return ((this.m_errorCode >= 0) ? 1 : 0) != 0;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002A42C File Offset: 0x0002982C
		public bool Failed
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return ((this.m_errorCode < 0) ? 1 : 0) != 0;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002A444 File Offset: 0x00029844
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0002A458 File Offset: 0x00029858
		public string Message
		{
			get
			{
				return this.m_message;
			}
			set
			{
				this.m_message = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0002A46C File Offset: 0x0002986C
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0002A480 File Offset: 0x00029880
		public Exception Exception
		{
			get
			{
				return this.m_exception;
			}
			set
			{
				this.m_exception = value;
			}
		}

		// Token: 0x04000D0E RID: 3342
		private int m_errorCode;

		// Token: 0x04000D0F RID: 3343
		private string m_message;

		// Token: 0x04000D10 RID: 3344
		private Exception m_exception;

		// Token: 0x04000D11 RID: 3345
		public const int IMS_S_OK = 0;

		// Token: 0x04000D12 RID: 3346
		public const int IMS_E_UNKNOWN_ERROR = -2147220991;

		// Token: 0x04000D13 RID: 3347
		public const int IMS_E_SEARCH_NOT_FOUND = -2147220990;

		// Token: 0x04000D14 RID: 3348
		public const int IMS_E_WRONG_TARGET_SERVER = -2147220989;

		// Token: 0x04000D15 RID: 3349
		public const int IMS_E_SEARCH_ARGUMENT = -2147220988;

		// Token: 0x04000D16 RID: 3350
		public const int IMS_E_OBJECT_NOT_FOUND = -2147220987;

		// Token: 0x04000D17 RID: 3351
		public const int IMS_E_STORE_PERMANENT_ERROR = -2147220986;

		// Token: 0x04000D18 RID: 3352
		public const int IMS_E_STORE_TRANSIENT_ERROR = -2147220985;

		// Token: 0x04000D19 RID: 3353
		public const int IMS_E_AQS_PARSER_ERROR = -2147220984;

		// Token: 0x04000D1A RID: 3354
		public const int IMS_E_SERVER_SHUTDOWN = -2147220983;

		// Token: 0x04000D1B RID: 3355
		public const int IMS_E_ACCESS_ERROR = -2147220982;

		// Token: 0x04000D1C RID: 3356
		public const int IMS_E_EMPTY_QUERY_ERROR = -2147220981;

		// Token: 0x04000D1D RID: 3357
		public const int IMS_E_REMOVE_ONGOING_SEARCH = -2147220980;

		// Token: 0x04000D1E RID: 3358
		public const int IMS_E_OVER_BUDGET_ERROR = -2147220979;

		// Token: 0x04000D1F RID: 3359
		public const int IMS_E_DISABLED_ERROR = -2147220978;

		// Token: 0x04000D20 RID: 3360
		public const int IMS_S_SEARCH_ALREADY_STARTED = 262657;

		// Token: 0x04000D21 RID: 3361
		public const int IMS_S_SEARCH_NOT_STARTED = 262658;
	}
}
