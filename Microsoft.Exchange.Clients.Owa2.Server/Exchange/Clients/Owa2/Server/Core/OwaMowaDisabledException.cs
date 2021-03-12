using System;
using System.ServiceModel;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200011B RID: 283
	public class OwaMowaDisabledException : OwaExtendedErrorCodeException
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x00022C2E File Offset: 0x00020E2E
		public OwaMowaDisabledException(string message, string user) : base(OwaExtendedErrorCode.MowaDisabled, message, user, FaultCode.CreateSenderFaultCode("MowaDisabled", "Owa"))
		{
		}
	}
}
