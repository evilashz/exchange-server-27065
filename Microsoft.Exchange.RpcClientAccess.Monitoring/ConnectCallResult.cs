using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConnectCallResult : EmsmdbCallResult
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000026BC File Offset: 0x000008BC
		public ConnectCallResult(Exception exception) : this(exception, null)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026C6 File Offset: 0x000008C6
		public ConnectCallResult(Exception exception, IPropertyBag httpResponseInformation) : base(exception, httpResponseInformation)
		{
			this.serverVersion = null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026DC File Offset: 0x000008DC
		public ConnectCallResult(ErrorCode errorCode, ExceptionTraceAuxiliaryBlock remoteExceptionTrace, MapiVersion? serverVersion) : this(errorCode, remoteExceptionTrace, null, serverVersion, null)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026E9 File Offset: 0x000008E9
		public ConnectCallResult(ErrorCode errorCode, ExceptionTraceAuxiliaryBlock remoteExceptionTrace, MonitoringActivityAuxiliaryBlock activityContext, MapiVersion? serverVersion, IPropertyBag httpResponseInformation) : base(null, errorCode, remoteExceptionTrace, activityContext, httpResponseInformation)
		{
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026FF File Offset: 0x000008FF
		private ConnectCallResult() : base(null, ErrorCode.None, null)
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000270A File Offset: 0x0000090A
		public MapiVersion? ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002712 File Offset: 0x00000912
		public static ConnectCallResult CreateSuccessfulResult()
		{
			return ConnectCallResult.successResult;
		}

		// Token: 0x0400001A RID: 26
		private static readonly ConnectCallResult successResult = new ConnectCallResult();

		// Token: 0x0400001B RID: 27
		private readonly MapiVersion? serverVersion;
	}
}
