using System;
using System.ServiceModel;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000114 RID: 276
	public class OwaInvalidMobileDevicePolicyException : OwaExtendedErrorCodeException
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x00022B9A File Offset: 0x00020D9A
		public OwaInvalidMobileDevicePolicyException(string message, string user, string expectedPolicyId) : base(OwaExtendedErrorCode.InvalidMobileDevicePolicy, message, user, FaultCode.CreateSenderFaultCode("InvalidMobileDevicePolicy", "Owa"), expectedPolicyId)
		{
		}
	}
}
