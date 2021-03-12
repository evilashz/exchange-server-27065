using System;
using System.ServiceModel;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200010C RID: 268
	public class OwaDeviceNotProvisionedException : OwaExtendedErrorCodeException
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x00022A6D File Offset: 0x00020C6D
		public OwaDeviceNotProvisionedException(string message, string user) : base(OwaExtendedErrorCode.InvalidDeviceId, message, user, FaultCode.CreateSenderFaultCode("DeviceNotProvisioned", "Owa"))
		{
		}
	}
}
