using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200009C RID: 156
	[CollectionDataContract(Name = "DomainSettings", ItemName = "DomainSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class DomainSettingCollection : Collection<DomainSetting>
	{
	}
}
