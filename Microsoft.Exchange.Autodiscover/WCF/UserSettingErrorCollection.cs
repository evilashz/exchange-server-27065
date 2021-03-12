using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B9 RID: 185
	[CollectionDataContract(Name = "UserSettingErrors", ItemName = "UserSettingError", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class UserSettingErrorCollection : Collection<UserSettingError>
	{
	}
}
