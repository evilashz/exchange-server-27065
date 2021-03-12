using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000CA RID: 202
	[ServiceContract(Namespace = "ECP", Name = "VoiceMail")]
	public interface IVoiceMail : IEditObjectService<GetVoiceMailConfiguration, SetVoiceMailConfiguration>, IGetObjectService<GetVoiceMailConfiguration>
	{
		// Token: 0x06001D38 RID: 7480
		[OperationContract]
		PowerShellResults<GetVoiceMailConfiguration> ClearSettings(Identity identity);

		// Token: 0x06001D39 RID: 7481
		[OperationContract]
		PowerShellResults<GetVoiceMailConfiguration> RegisterPhone(Identity identity, SetVoiceMailConfiguration properties);
	}
}
