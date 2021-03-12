using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.SearchService
{
	// Token: 0x02000248 RID: 584
	[Serializable]
	internal class SearchServerException : LocalizedException
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x0004CD80 File Offset: 0x0004AF80
		internal SearchServerException(int errorCode) : base(SearchServerException.FormatErrorCode(errorCode))
		{
			base.ErrorCode = errorCode;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0004CD95 File Offset: 0x0004AF95
		internal SearchServerException(int errorCode, string serverMessage) : base(SearchServerException.FormatErrorCode(errorCode))
		{
			base.ErrorCode = errorCode;
			this.ServerMessage = serverMessage;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0004CDB1 File Offset: 0x0004AFB1
		internal SearchServerException(int errorCode, LocalizedString message) : base(message)
		{
			base.ErrorCode = errorCode;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0004CDC1 File Offset: 0x0004AFC1
		internal SearchServerException(int errorCode, LocalizedString message, Exception innerException) : base(message, innerException)
		{
			base.ErrorCode = errorCode;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0004CDD2 File Offset: 0x0004AFD2
		protected SearchServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0004CDDC File Offset: 0x0004AFDC
		public override string Message
		{
			get
			{
				if (string.IsNullOrEmpty(this.ServerMessage))
				{
					return base.Message;
				}
				return base.Message + " " + Strings.SearchServerErrorMessage(this.ServerMessage);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0004CE12 File Offset: 0x0004B012
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0004CE1A File Offset: 0x0004B01A
		internal string ServerMessage
		{
			get
			{
				return this.serverMessage;
			}
			set
			{
				this.serverMessage = value;
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0004CE24 File Offset: 0x0004B024
		private static LocalizedString FormatErrorCode(int errorCode)
		{
			switch (errorCode)
			{
			case -2147220991:
				return Strings.UnknownError;
			case -2147220990:
				return Strings.SearchObjectNotFound;
			case -2147220989:
				return Strings.WrongTargetServer;
			case -2147220988:
				return Strings.SearchArgument;
			case -2147220987:
				return Strings.ObjectNotFound;
			case -2147220986:
				return Strings.StorePermanantError;
			case -2147220985:
				return Strings.StoreTransientError;
			case -2147220984:
				return Strings.AqsParserError;
			case -2147220983:
				return Strings.ServerShutdown;
			case -2147220981:
				return Strings.SearchQueryEmpty;
			case -2147220979:
				return Strings.SearchThrottled;
			case -2147220978:
				return Strings.SearchDisabled;
			}
			return Strings.SearchServerError(errorCode);
		}

		// Token: 0x04000B5B RID: 2907
		private string serverMessage;
	}
}
