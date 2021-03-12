using System;
using System.Globalization;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200001A RID: 26
	internal static class OperationStatusCodeClassification
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002658 File Offset: 0x00000858
		public static bool IsLogonFailedDueToInvalidConnectionSettings(OperationStatusCode result)
		{
			OperationStatusCodeClassification.ThrowIfStatusIsNotFailure(result);
			switch (result)
			{
			case OperationStatusCode.ErrorInvalidCredentials:
			case OperationStatusCode.ErrorCannotCommunicateWithRemoteServer:
				return false;
			case OperationStatusCode.ErrorInvalidRemoteServer:
			case OperationStatusCode.ErrorUnsupportedProtocolVersion:
				return true;
			default:
				throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "Unexpected OperationStatusCode value encountered in IsLogonFailedDueToInvalidConnectionSettings(): {0}", new object[]
				{
					result
				}));
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000026B0 File Offset: 0x000008B0
		public static bool IsLogonFailedDueToInvalidCredentials(OperationStatusCode result)
		{
			OperationStatusCodeClassification.ThrowIfStatusIsNotFailure(result);
			switch (result)
			{
			case OperationStatusCode.ErrorInvalidCredentials:
				return true;
			case OperationStatusCode.ErrorCannotCommunicateWithRemoteServer:
			case OperationStatusCode.ErrorInvalidRemoteServer:
			case OperationStatusCode.ErrorUnsupportedProtocolVersion:
				return false;
			default:
				throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "Unexpected OperationStatusCode value encountered in IsLogonFailedDueToInvalidCredentials(): {0}", new object[]
				{
					result
				}));
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002708 File Offset: 0x00000908
		public static bool IsLogonFailedDueToServerHavingTransientProblems(OperationStatusCode result)
		{
			OperationStatusCodeClassification.ThrowIfStatusIsNotFailure(result);
			switch (result)
			{
			case OperationStatusCode.ErrorInvalidCredentials:
			case OperationStatusCode.ErrorInvalidRemoteServer:
			case OperationStatusCode.ErrorUnsupportedProtocolVersion:
				return false;
			case OperationStatusCode.ErrorCannotCommunicateWithRemoteServer:
				return true;
			default:
				throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "Unexpected OperationStatusCode value encountered in IsLogonFailedDueToServerHavingTransientProblems(): {0}", new object[]
				{
					result
				}));
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002760 File Offset: 0x00000960
		private static void ThrowIfStatusIsNotFailure(OperationStatusCode result)
		{
			if (result == OperationStatusCode.None || result == OperationStatusCode.Success)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The specified status code does not indicate a failure: {0}.", new object[]
				{
					result
				}));
			}
		}
	}
}
