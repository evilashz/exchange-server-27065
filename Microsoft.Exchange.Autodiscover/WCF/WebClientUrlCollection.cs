using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000BB RID: 187
	[CollectionDataContract(Name = "WebClientUrls", ItemName = "WebClientUrl", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class WebClientUrlCollection : Collection<WebClientUrl>
	{
	}
}
