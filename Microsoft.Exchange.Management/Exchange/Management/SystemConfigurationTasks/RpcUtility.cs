using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200031E RID: 798
	internal class RpcUtility
	{
		// Token: 0x06001AFF RID: 6911 RVA: 0x00077AE4 File Offset: 0x00075CE4
		internal static LocalizedString MapRpcErrorCodeToMessage(int errorCode, string serverName)
		{
			int num = errorCode;
			if (num == 1753)
			{
				return Strings.ElcExpirationServiceUnavailable(serverName, errorCode.ToString());
			}
			return Strings.ElcServiceCallFailed(serverName, errorCode.ToString());
		}
	}
}
