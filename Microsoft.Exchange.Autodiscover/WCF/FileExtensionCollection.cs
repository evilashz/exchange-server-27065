using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A1 RID: 161
	[CollectionDataContract(Name = "FileExtensionCollection", ItemName = "FileExtension", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class FileExtensionCollection : Collection<string>
	{
	}
}
