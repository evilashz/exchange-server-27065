using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002EF RID: 751
	[ServiceContract(Namespace = "ECP", Name = "UserPhoto")]
	public interface IUserPhotoService
	{
		// Token: 0x06002D2A RID: 11562
		[OperationContract]
		PowerShellResults SavePhoto(Identity identity);

		// Token: 0x06002D2B RID: 11563
		[OperationContract]
		PowerShellResults CancelPhoto(Identity identity);

		// Token: 0x06002D2C RID: 11564
		[OperationContract]
		PowerShellResults RemovePhoto(Identity identity);
	}
}
