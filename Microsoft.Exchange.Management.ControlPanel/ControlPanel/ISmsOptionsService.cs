using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000485 RID: 1157
	[ServiceContract(Namespace = "ECP", Name = "SmsOptions")]
	public interface ISmsOptionsService : IEditObjectService<SmsOptions, SetSmsOptions>, IGetObjectService<SmsOptions>
	{
		// Token: 0x060039F0 RID: 14832
		[OperationContract]
		PowerShellResults<SmsOptions> DisableObject(Identity identity);

		// Token: 0x060039F1 RID: 14833
		[OperationContract]
		PowerShellResults<SmsOptions> SendVerificationCode(Identity identity, SetSmsOptions properties);
	}
}
