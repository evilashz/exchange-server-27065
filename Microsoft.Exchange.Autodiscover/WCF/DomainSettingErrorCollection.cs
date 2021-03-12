using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200009E RID: 158
	[CollectionDataContract(Name = "DomainSettingErrors", ItemName = "DomainSettingError", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class DomainSettingErrorCollection : Collection<DomainSettingError>
	{
	}
}
