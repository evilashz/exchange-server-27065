using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000092 RID: 146
	[ServiceContract(Namespace = "ECP", Name = "EMailSignature")]
	public interface IEMailSignature : IMessagingBase<EMailSignatureConfiguration, SetEMailSignatureConfiguration>, IEditObjectService<EMailSignatureConfiguration, SetEMailSignatureConfiguration>, IGetObjectService<EMailSignatureConfiguration>
	{
	}
}
