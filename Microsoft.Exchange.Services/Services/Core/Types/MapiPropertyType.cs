using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000800 RID: 2048
	[XmlType(TypeName = "MapiPropertyTypeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MapiPropertyType
	{
		// Token: 0x040020FF RID: 8447
		ApplicationTime = 7,
		// Token: 0x04002100 RID: 8448
		ApplicationTimeArray = 4103,
		// Token: 0x04002101 RID: 8449
		Binary = 258,
		// Token: 0x04002102 RID: 8450
		BinaryArray = 4354,
		// Token: 0x04002103 RID: 8451
		Boolean = 11,
		// Token: 0x04002104 RID: 8452
		CLSID = 72,
		// Token: 0x04002105 RID: 8453
		CLSIDArray = 4168,
		// Token: 0x04002106 RID: 8454
		Currency = 6,
		// Token: 0x04002107 RID: 8455
		CurrencyArray = 4102,
		// Token: 0x04002108 RID: 8456
		Double = 5,
		// Token: 0x04002109 RID: 8457
		DoubleArray = 4101,
		// Token: 0x0400210A RID: 8458
		Error = 10,
		// Token: 0x0400210B RID: 8459
		Float = 4,
		// Token: 0x0400210C RID: 8460
		FloatArray = 4100,
		// Token: 0x0400210D RID: 8461
		Integer = 3,
		// Token: 0x0400210E RID: 8462
		IntegerArray = 4099,
		// Token: 0x0400210F RID: 8463
		Long = 20,
		// Token: 0x04002110 RID: 8464
		LongArray = 4116,
		// Token: 0x04002111 RID: 8465
		Null = 1,
		// Token: 0x04002112 RID: 8466
		Object = 13,
		// Token: 0x04002113 RID: 8467
		ObjectArray = 4109,
		// Token: 0x04002114 RID: 8468
		Short = 2,
		// Token: 0x04002115 RID: 8469
		ShortArray = 4098,
		// Token: 0x04002116 RID: 8470
		SystemTime = 64,
		// Token: 0x04002117 RID: 8471
		SystemTimeArray = 4160,
		// Token: 0x04002118 RID: 8472
		String = 31,
		// Token: 0x04002119 RID: 8473
		StringArray = 4127
	}
}
