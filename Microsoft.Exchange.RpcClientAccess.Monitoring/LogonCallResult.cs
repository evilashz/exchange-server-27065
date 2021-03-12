using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LogonCallResult : EmsmdbCallResult
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000336C File Offset: 0x0000156C
		public LogonCallResult(Exception exception) : this(exception, null)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003376 File Offset: 0x00001576
		public LogonCallResult(Exception exception, IPropertyBag httpResponseInformation) : base(exception, httpResponseInformation)
		{
			this.logonErrorCode = ErrorCode.None;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003387 File Offset: 0x00001587
		public LogonCallResult(ErrorCode errorCode, ExceptionTraceAuxiliaryBlock remoteExceptionTrace, ErrorCode logonErrorCode) : this(errorCode, remoteExceptionTrace, null, logonErrorCode, null)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003394 File Offset: 0x00001594
		public LogonCallResult(ErrorCode errorCode, ExceptionTraceAuxiliaryBlock remoteExceptionTrace, MonitoringActivityAuxiliaryBlock activityContext, ErrorCode logonErrorCode, IPropertyBag httpResponseInformation) : base(null, errorCode, remoteExceptionTrace, activityContext, httpResponseInformation)
		{
			this.logonErrorCode = logonErrorCode;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000033AA File Offset: 0x000015AA
		private LogonCallResult() : base(null, ErrorCode.None, null)
		{
			this.logonErrorCode = ErrorCode.None;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000033BC File Offset: 0x000015BC
		public override bool IsSuccessful
		{
			get
			{
				return base.IsSuccessful && this.logonErrorCode == ErrorCode.None;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000033D1 File Offset: 0x000015D1
		public ErrorCode LogonErrorCode
		{
			get
			{
				return this.logonErrorCode;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000033D9 File Offset: 0x000015D9
		public static LogonCallResult CreateSuccessfulResult()
		{
			return LogonCallResult.successResult;
		}

		// Token: 0x04000032 RID: 50
		private static readonly LogonCallResult successResult = new LogonCallResult();

		// Token: 0x04000033 RID: 51
		private readonly ErrorCode logonErrorCode;
	}
}
