using System;
using Microsoft.Exchange.Connections.Common;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x02000016 RID: 22
	internal static class EasServerResponseExtensions
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00003420 File Offset: 0x00001620
		internal static void ThrowIfStatusIsFailed<T>(this IEasServerResponse<T> response, T status) where T : struct, IConvertible
		{
			if (!response.IsSucceeded(status))
			{
				EasServerResponseExtensions.ThrowAppropriateException(response, status.ToInt32(null), status.ToString());
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000344C File Offset: 0x0000164C
		private static void ThrowAppropriateException(IHaveAnHttpStatus response, int statusInt, string statusString)
		{
			EasCommonStatus easCommonStatus = (EasCommonStatus)(statusInt & 65280);
			EasCommonStatus easCommonStatus2 = easCommonStatus;
			if (easCommonStatus2 <= EasCommonStatus.RequiresFolderSync)
			{
				if (easCommonStatus2 == EasCommonStatus.RequiresSyncKeyReset)
				{
					throw new EasRequiresSyncKeyResetException(statusString);
				}
				if (easCommonStatus2 == EasCommonStatus.RequiresFolderSync)
				{
					throw new EasRequiresFolderSyncException(statusString);
				}
			}
			else
			{
				if (easCommonStatus2 == EasCommonStatus.PermanentError)
				{
					throw new EasCommandFailedPermanentException(statusString, response.HttpStatus.ToString());
				}
				if (easCommonStatus2 == EasCommonStatus.BackOff)
				{
					throw new EasRetryAfterException(TimeSpan.FromMinutes(5.0), statusString);
				}
			}
			throw new EasCommandFailedTransientException(statusString, response.HttpStatus.ToString());
		}
	}
}
