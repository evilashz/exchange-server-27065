using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003DE RID: 990
	[XmlType(TypeName = "ConnectionSettingsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ConnectionSettingsInfoType
	{
		// Token: 0x04001238 RID: 4664
		[XmlEnum("item:Office365")]
		Office365 = 10,
		// Token: 0x04001239 RID: 4665
		[XmlEnum("item:ExchangeActiveSync")]
		ExchangeActiveSync = 20,
		// Token: 0x0400123A RID: 4666
		[XmlEnum("item:Imap")]
		Imap = 30,
		// Token: 0x0400123B RID: 4667
		[XmlEnum("item:Pop")]
		Pop = 40,
		// Token: 0x0400123C RID: 4668
		[XmlEnum("item:Smtp")]
		Smtp = 50
	}
}
