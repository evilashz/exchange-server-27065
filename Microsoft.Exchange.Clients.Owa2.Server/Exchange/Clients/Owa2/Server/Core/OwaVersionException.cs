using System;
using System.ServiceModel;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000127 RID: 295
	public class OwaVersionException : OwaExtendedErrorCodeException
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x00022D44 File Offset: 0x00020F44
		public OwaVersionException(string message, string user) : base(OwaExtendedErrorCode.VersionMismatch, message, user, FaultCode.CreateSenderFaultCode("Version", "Owa"))
		{
		}
	}
}
