using System;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B6E RID: 2926
	[MessageContract(IsWrapped = false)]
	public abstract class BaseSoapResponse
	{
		// Token: 0x060052CB RID: 21195 RVA: 0x0010BEB0 File Offset: 0x0010A0B0
		public BaseSoapResponse()
		{
			this.ServerVersionInfo = ServerVersionInfo.CurrentAssemblyVersion;
		}

		// Token: 0x04002E18 RID: 11800
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[MessageHeader(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public ServerVersionInfo ServerVersionInfo;
	}
}
