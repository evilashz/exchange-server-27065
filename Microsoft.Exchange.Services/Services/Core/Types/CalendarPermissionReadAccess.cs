using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000608 RID: 1544
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CalendarPermissionReadAccess
	{
		// Token: 0x04001BBF RID: 7103
		None,
		// Token: 0x04001BC0 RID: 7104
		TimeOnly,
		// Token: 0x04001BC1 RID: 7105
		TimeAndSubjectAndLocation,
		// Token: 0x04001BC2 RID: 7106
		FullDetails
	}
}
