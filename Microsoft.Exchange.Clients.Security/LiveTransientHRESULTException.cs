using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000013 RID: 19
	public class LiveTransientHRESULTException : LiveTransientException
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003136 File Offset: 0x00001336
		public LiveTransientHRESULTException(COMException e, uint errorCode) : base(Strings.LiveTransientHRESULTExceptionMessage(errorCode, Enum.GetName(typeof(RPSErrorCode), errorCode) ?? string.Empty), e)
		{
		}
	}
}
