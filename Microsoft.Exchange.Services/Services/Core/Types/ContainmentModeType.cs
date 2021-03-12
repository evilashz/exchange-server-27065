using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200064C RID: 1612
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ContainmentModeType
	{
		// Token: 0x04001C7F RID: 7295
		FullString,
		// Token: 0x04001C80 RID: 7296
		Prefixed,
		// Token: 0x04001C81 RID: 7297
		Substring,
		// Token: 0x04001C82 RID: 7298
		PrefixOnWords,
		// Token: 0x04001C83 RID: 7299
		ExactPhrase
	}
}
