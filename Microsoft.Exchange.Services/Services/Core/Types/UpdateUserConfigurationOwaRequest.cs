using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200049D RID: 1181
	[XmlType("UpdateUserConfigurationOwaRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateUserConfigurationOwaRequest : UpdateUserConfigurationRequest
	{
		// Token: 0x0600236D RID: 9069 RVA: 0x000A3EBE File Offset: 0x000A20BE
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateUserConfiguration(callContext, this);
		}
	}
}
