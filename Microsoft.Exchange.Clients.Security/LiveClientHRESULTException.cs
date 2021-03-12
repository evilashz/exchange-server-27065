using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000015 RID: 21
	public class LiveClientHRESULTException : LiveClientException
	{
		// Token: 0x06000061 RID: 97 RVA: 0x0000320D File Offset: 0x0000140D
		public LiveClientHRESULTException(COMException e, uint errorCode) : base(Strings.LiveClientHRESULTExceptionMessage(errorCode, Enum.GetName(typeof(RPSErrorCode), errorCode) ?? string.Empty), e)
		{
		}
	}
}
