using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000209 RID: 521
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum CalendarPermissionLevelType
	{
		// Token: 0x04000E49 RID: 3657
		None,
		// Token: 0x04000E4A RID: 3658
		Owner,
		// Token: 0x04000E4B RID: 3659
		PublishingEditor,
		// Token: 0x04000E4C RID: 3660
		Editor,
		// Token: 0x04000E4D RID: 3661
		PublishingAuthor,
		// Token: 0x04000E4E RID: 3662
		Author,
		// Token: 0x04000E4F RID: 3663
		NoneditingAuthor,
		// Token: 0x04000E50 RID: 3664
		Reviewer,
		// Token: 0x04000E51 RID: 3665
		Contributor,
		// Token: 0x04000E52 RID: 3666
		FreeBusyTimeOnly,
		// Token: 0x04000E53 RID: 3667
		FreeBusyTimeAndSubjectAndLocation,
		// Token: 0x04000E54 RID: 3668
		Custom
	}
}
