using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000D3 RID: 211
	[ServiceContract(Namespace = "ECP", Name = "VoiceMailSettings")]
	public interface IVoiceMailSettings : IEditObjectService<GetVoiceMailSettings, SetVoiceMailSettings>, IGetObjectService<GetVoiceMailSettings>
	{
		// Token: 0x06001D67 RID: 7527
		[OperationContract]
		PowerShellResults ResetPIN(Identity identity);
	}
}
