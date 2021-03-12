using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000187 RID: 391
	internal class UMRpcException : LocalizedException
	{
		// Token: 0x06000C7D RID: 3197 RVA: 0x0002DCE7 File Offset: 0x0002BEE7
		internal UMRpcException(int result, string userName, string serverName, Exception innerException) : base(UMRpcException.GetLocalizedMessage(userName, serverName, result), innerException)
		{
			base.ErrorCode = result;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002DD00 File Offset: 0x0002BF00
		internal UMRpcException(Exception innerException) : this(UMRpcException.GetHRFromException(innerException), null, null, innerException)
		{
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002DD14 File Offset: 0x0002BF14
		private static LocalizedString GetLocalizedMessage(string userName, string serverName, int errorCode)
		{
			switch (errorCode)
			{
			case -2147466751:
				return Strings.UMRpcTransientError(userName, serverName);
			case -2147466750:
				break;
			case -2147466749:
				return Strings.UMRPCIncompatibleVersionError(serverName);
			default:
				if (errorCode == 5)
				{
					return Strings.UMRpcAccessDeniedError(serverName);
				}
				break;
			}
			return Strings.UMRpcGenericError(userName, errorCode, serverName);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002DD68 File Offset: 0x0002BF68
		private static int GetHRFromException(Exception exception)
		{
			int result = -2147466752;
			if (exception is StorageTransientException || exception is DataSourceTransientException)
			{
				result = -2147466751;
			}
			else if (exception is UMRPCIncompabibleVersionException)
			{
				result = -2147466749;
			}
			else if (exception is UMInvalidPartnerMessageException)
			{
				result = -2147466750;
			}
			return result;
		}
	}
}
