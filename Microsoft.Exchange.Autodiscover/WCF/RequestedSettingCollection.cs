using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000090 RID: 144
	[CollectionDataContract(Name = "RequestedSettings", ItemName = "Setting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class RequestedSettingCollection : Collection<string>
	{
	}
}
