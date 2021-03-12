using System;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000CB RID: 203
	[Flags]
	internal enum ComplianceStatus
	{
		// Token: 0x040006BF RID: 1727
		Compliant = 0,
		// Token: 0x040006C0 RID: 1728
		StreamTruncated = 1,
		// Token: 0x040006C1 RID: 1729
		PropertyTruncated = 2,
		// Token: 0x040006C2 RID: 1730
		InvalidCharacterInPropertyName = 4,
		// Token: 0x040006C3 RID: 1731
		InvalidCharacterInParameterName = 8,
		// Token: 0x040006C4 RID: 1732
		InvalidCharacterInParameterText = 16,
		// Token: 0x040006C5 RID: 1733
		InvalidCharacterInQuotedString = 32,
		// Token: 0x040006C6 RID: 1734
		InvalidCharacterInPropertyValue = 64,
		// Token: 0x040006C7 RID: 1735
		NotAllComponentsClosed = 128,
		// Token: 0x040006C8 RID: 1736
		ParametersOnComponentTag = 256,
		// Token: 0x040006C9 RID: 1737
		EndTagWithoutBegin = 512,
		// Token: 0x040006CA RID: 1738
		ComponentNameMismatch = 1024,
		// Token: 0x040006CB RID: 1739
		InvalidValueFormat = 2048,
		// Token: 0x040006CC RID: 1740
		EmptyParameterName = 4096,
		// Token: 0x040006CD RID: 1741
		EmptyPropertyName = 8192,
		// Token: 0x040006CE RID: 1742
		EmptyComponentName = 16384,
		// Token: 0x040006CF RID: 1743
		PropertyOutsideOfComponent = 32768,
		// Token: 0x040006D0 RID: 1744
		InvalidParameterValue = 65536,
		// Token: 0x040006D1 RID: 1745
		ParameterNameMissing = 131072
	}
}
