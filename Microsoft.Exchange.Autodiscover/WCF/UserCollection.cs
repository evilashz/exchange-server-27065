using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000092 RID: 146
	[CollectionDataContract(Name = "Users", ItemName = "User", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class UserCollection : Collection<User>
	{
	}
}
