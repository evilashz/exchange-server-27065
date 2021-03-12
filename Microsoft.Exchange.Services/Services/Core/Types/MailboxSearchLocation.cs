using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F3 RID: 2035
	[Flags]
	[XmlType(TypeName = "MailboxSearchLocationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MailboxSearchLocation
	{
		// Token: 0x040020D4 RID: 8404
		PrimaryOnly = 1,
		// Token: 0x040020D5 RID: 8405
		ArchiveOnly = 2,
		// Token: 0x040020D6 RID: 8406
		All = 3
	}
}
