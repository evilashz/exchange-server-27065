using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000754 RID: 1876
	[XmlType(TypeName = "DistinguishedPropertySetType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DistinguishedPropertySet
	{
		// Token: 0x04001F34 RID: 7988
		Meeting,
		// Token: 0x04001F35 RID: 7989
		Appointment,
		// Token: 0x04001F36 RID: 7990
		Common,
		// Token: 0x04001F37 RID: 7991
		PublicStrings,
		// Token: 0x04001F38 RID: 7992
		Address,
		// Token: 0x04001F39 RID: 7993
		InternetHeaders,
		// Token: 0x04001F3A RID: 7994
		CalendarAssistant,
		// Token: 0x04001F3B RID: 7995
		UnifiedMessaging,
		// Token: 0x04001F3C RID: 7996
		Task,
		// Token: 0x04001F3D RID: 7997
		Sharing
	}
}
