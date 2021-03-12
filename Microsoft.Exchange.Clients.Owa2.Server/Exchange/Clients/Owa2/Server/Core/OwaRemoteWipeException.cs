using System;
using System.ServiceModel;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200011F RID: 287
	public class OwaRemoteWipeException : OwaExtendedErrorCodeException
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x00022C6F File Offset: 0x00020E6F
		public OwaRemoteWipeException(string message, string user) : base(OwaExtendedErrorCode.RemoteWipe, message, user, FaultCode.CreateSenderFaultCode("RemoteWipe", "Owa"))
		{
		}
	}
}
