using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.UM.UMCommon.FaultInjection
{
	// Token: 0x02000074 RID: 116
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RMSFaultInjection
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		internal static bool TryCreateException(string exceptionType, ref Exception exception)
		{
			if (exceptionType != null)
			{
				if (RMSFaultInjection.RMSPermanentException.Equals(exceptionType))
				{
					exception = new RightsManagementPermanentException(RightsManagementFailureCode.UnknownFailure, new LocalizedString("This is a test purpose exception for testing"));
					return true;
				}
				if (RMSFaultInjection.RMSTransientException.Equals(exceptionType))
				{
					exception = new RightsManagementTransientException(new LocalizedString("This is a test purpose exception for testing"));
					return true;
				}
			}
			return false;
		}

		// Token: 0x040002D6 RID: 726
		internal const uint RMSEncryptVoiceMail = 3034983741U;

		// Token: 0x040002D7 RID: 727
		internal const uint RMSDecryptVoiceMail = 4108725565U;

		// Token: 0x040002D8 RID: 728
		internal const uint RMSDecryptAttachement = 2900766013U;

		// Token: 0x040002D9 RID: 729
		internal const uint StageNotRetry = 2162568509U;

		// Token: 0x040002DA RID: 730
		private static readonly string RMSPermanentException = "RightsManagementPermanentException";

		// Token: 0x040002DB RID: 731
		private static readonly string RMSTransientException = "RightsManagementTransientException";
	}
}
