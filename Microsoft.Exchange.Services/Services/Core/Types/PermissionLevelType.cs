using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200060C RID: 1548
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum PermissionLevelType
	{
		// Token: 0x04001BD8 RID: 7128
		None,
		// Token: 0x04001BD9 RID: 7129
		Owner,
		// Token: 0x04001BDA RID: 7130
		PublishingEditor,
		// Token: 0x04001BDB RID: 7131
		Editor,
		// Token: 0x04001BDC RID: 7132
		PublishingAuthor,
		// Token: 0x04001BDD RID: 7133
		Author,
		// Token: 0x04001BDE RID: 7134
		NoneditingAuthor,
		// Token: 0x04001BDF RID: 7135
		Reviewer,
		// Token: 0x04001BE0 RID: 7136
		Contributor,
		// Token: 0x04001BE1 RID: 7137
		Custom
	}
}
