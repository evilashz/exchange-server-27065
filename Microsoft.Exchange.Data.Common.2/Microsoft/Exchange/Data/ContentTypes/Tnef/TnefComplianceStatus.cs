using System;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000F0 RID: 240
	[Flags]
	public enum TnefComplianceStatus
	{
		// Token: 0x04000CD9 RID: 3289
		Compliant = 0,
		// Token: 0x04000CDA RID: 3290
		InvalidAttribute = 1,
		// Token: 0x04000CDB RID: 3291
		InvalidAttributeLevel = 2,
		// Token: 0x04000CDC RID: 3292
		InvalidAttributeLength = 16,
		// Token: 0x04000CDD RID: 3293
		StreamTruncated = 32,
		// Token: 0x04000CDE RID: 3294
		InvalidTnefSignature = 64,
		// Token: 0x04000CDF RID: 3295
		InvalidTnefVersion = 256,
		// Token: 0x04000CE0 RID: 3296
		InvalidMessageClass = 512,
		// Token: 0x04000CE1 RID: 3297
		InvalidRowCount = 1024,
		// Token: 0x04000CE2 RID: 3298
		InvalidAttributeValue = 2048,
		// Token: 0x04000CE3 RID: 3299
		AttributeOverflow = 4096,
		// Token: 0x04000CE4 RID: 3300
		InvalidAttributeChecksum = 8192,
		// Token: 0x04000CE5 RID: 3301
		InvalidMessageCodepage = 16384,
		// Token: 0x04000CE6 RID: 3302
		UnsupportedPropertyType = 32768,
		// Token: 0x04000CE7 RID: 3303
		InvalidPropertyLength = 65536,
		// Token: 0x04000CE8 RID: 3304
		InvalidDate = 131072,
		// Token: 0x04000CE9 RID: 3305
		NestingTooDeep = 262144
	}
}
