using System;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000012 RID: 18
	public class LiveConfigurationHRESULTException : LiveConfigurationException
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003109 File Offset: 0x00001309
		public LiveConfigurationHRESULTException(Exception e, uint errorCode) : base(Strings.LiveConfigurationHRESULTExceptionMessage(errorCode, Enum.GetName(typeof(RPSErrorCode), errorCode) ?? string.Empty), e)
		{
		}
	}
}
