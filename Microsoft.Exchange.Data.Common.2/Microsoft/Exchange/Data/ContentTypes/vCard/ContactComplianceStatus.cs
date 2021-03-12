using System;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000BF RID: 191
	[Flags]
	public enum ContactComplianceStatus
	{
		// Token: 0x0400065B RID: 1627
		Compliant = 0,
		// Token: 0x0400065C RID: 1628
		StreamTruncated = 1,
		// Token: 0x0400065D RID: 1629
		PropertyTruncated = 2,
		// Token: 0x0400065E RID: 1630
		InvalidCharacterInPropertyName = 4,
		// Token: 0x0400065F RID: 1631
		InvalidCharacterInParameterName = 8,
		// Token: 0x04000660 RID: 1632
		InvalidCharacterInParameterText = 16,
		// Token: 0x04000661 RID: 1633
		InvalidCharacterInQuotedString = 32,
		// Token: 0x04000662 RID: 1634
		InvalidCharacterInPropertyValue = 64,
		// Token: 0x04000663 RID: 1635
		NotAllComponentsClosed = 128,
		// Token: 0x04000664 RID: 1636
		ParametersOnComponentTag = 256,
		// Token: 0x04000665 RID: 1637
		EndTagWithoutBegin = 512,
		// Token: 0x04000666 RID: 1638
		ComponentNameMismatch = 1024,
		// Token: 0x04000667 RID: 1639
		InvalidValueFormat = 2048,
		// Token: 0x04000668 RID: 1640
		EmptyParameterName = 4096,
		// Token: 0x04000669 RID: 1641
		EmptyPropertyName = 8192,
		// Token: 0x0400066A RID: 1642
		EmptyComponentName = 16384,
		// Token: 0x0400066B RID: 1643
		PropertyOutsideOfComponent = 32768,
		// Token: 0x0400066C RID: 1644
		InvalidParameterValue = 65536,
		// Token: 0x0400066D RID: 1645
		ParameterNameMissing = 131072
	}
}
