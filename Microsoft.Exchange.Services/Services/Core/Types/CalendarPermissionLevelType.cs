using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000609 RID: 1545
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CalendarPermissionLevelType
	{
		// Token: 0x04001BC4 RID: 7108
		None,
		// Token: 0x04001BC5 RID: 7109
		Owner,
		// Token: 0x04001BC6 RID: 7110
		PublishingEditor,
		// Token: 0x04001BC7 RID: 7111
		Editor,
		// Token: 0x04001BC8 RID: 7112
		PublishingAuthor,
		// Token: 0x04001BC9 RID: 7113
		Author,
		// Token: 0x04001BCA RID: 7114
		NoneditingAuthor,
		// Token: 0x04001BCB RID: 7115
		Reviewer,
		// Token: 0x04001BCC RID: 7116
		Contributor,
		// Token: 0x04001BCD RID: 7117
		Custom,
		// Token: 0x04001BCE RID: 7118
		FreeBusyTimeOnly,
		// Token: 0x04001BCF RID: 7119
		FreeBusyTimeAndSubjectAndLocation
	}
}
