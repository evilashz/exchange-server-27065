using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B1 RID: 177
	[CollectionDataContract(Name = "DocumentSharingLocations", ItemName = "DocumentSharingLocation", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class DocumentSharingLocationCollection : Collection<DocumentSharingLocation>
	{
	}
}
