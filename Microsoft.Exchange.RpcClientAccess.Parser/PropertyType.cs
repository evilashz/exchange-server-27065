using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001FA RID: 506
	internal enum PropertyType : ushort
	{
		// Token: 0x04000622 RID: 1570
		[AlternativeName(AlternativeNaming.MAPI, "PT_UNSPECIFIED")]
		Unspecified,
		// Token: 0x04000623 RID: 1571
		[AlternativeName(AlternativeNaming.MAPI, "PT_NULL")]
		Null,
		// Token: 0x04000624 RID: 1572
		[AlternativeName(AlternativeNaming.MAPI, "PT_I2")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_SHORT")]
		Int16,
		// Token: 0x04000625 RID: 1573
		[AlternativeName(AlternativeNaming.MAPI, "PT_I4")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_LONG")]
		Int32,
		// Token: 0x04000626 RID: 1574
		[AlternativeName(AlternativeNaming.MAPI, "PT_FLOAT")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_R4")]
		Float,
		// Token: 0x04000627 RID: 1575
		[AlternativeName(AlternativeNaming.MAPI, "PT_R8")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_DOULE")]
		Double,
		// Token: 0x04000628 RID: 1576
		[AlternativeName(AlternativeNaming.MAPI, "PT_CURRENCY")]
		Currency,
		// Token: 0x04000629 RID: 1577
		[AlternativeName(AlternativeNaming.MAPI, "PT_APPTIME")]
		AppTime,
		// Token: 0x0400062A RID: 1578
		[AlternativeName(AlternativeNaming.MAPI, "PT_ERROR")]
		Error = 10,
		// Token: 0x0400062B RID: 1579
		[AlternativeName(AlternativeNaming.MAPI, "PT_BOOLEAN")]
		Bool,
		// Token: 0x0400062C RID: 1580
		[AlternativeName(AlternativeNaming.MAPI, "PT_OBJECT")]
		Object = 13,
		// Token: 0x0400062D RID: 1581
		[AlternativeName(AlternativeNaming.MAPI, "PT_I8")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_LONGLONG")]
		Int64 = 20,
		// Token: 0x0400062E RID: 1582
		[AlternativeName(AlternativeNaming.MAPI, "PT_STRING8")]
		String8 = 30,
		// Token: 0x0400062F RID: 1583
		[AlternativeName(AlternativeNaming.MAPI, "PT_UNICODE")]
		Unicode,
		// Token: 0x04000630 RID: 1584
		[AlternativeName(AlternativeNaming.MAPI, "PT_SYSTIME")]
		SysTime = 64,
		// Token: 0x04000631 RID: 1585
		[AlternativeName(AlternativeNaming.MAPI, "PT_CLSID")]
		Guid = 72,
		// Token: 0x04000632 RID: 1586
		[AlternativeName(AlternativeNaming.MAPI, "PT_BINARY")]
		Binary = 258,
		// Token: 0x04000633 RID: 1587
		[AlternativeName(AlternativeNaming.MAPI, "PT_RESTRICTION")]
		Restriction = 253,
		// Token: 0x04000634 RID: 1588
		[AlternativeName(AlternativeNaming.MAPI, "PT_ACTIONS")]
		Actions,
		// Token: 0x04000635 RID: 1589
		[AlternativeName(AlternativeNaming.MAPI, "PT_SVREID")]
		ServerId = 251,
		// Token: 0x04000636 RID: 1590
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_I2")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_SHORT")]
		MultiValueInt16 = 4098,
		// Token: 0x04000637 RID: 1591
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_LONG")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_I4")]
		MultiValueInt32,
		// Token: 0x04000638 RID: 1592
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_FLOAT")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_R4")]
		MultiValueFloat,
		// Token: 0x04000639 RID: 1593
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_DOUBLE")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_R8")]
		MultiValueDouble,
		// Token: 0x0400063A RID: 1594
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_CURRENCY")]
		MultiValueCurrency,
		// Token: 0x0400063B RID: 1595
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_APPTIME")]
		MultiValueAppTime,
		// Token: 0x0400063C RID: 1596
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_SYSTIME")]
		MultiValueSysTime = 4160,
		// Token: 0x0400063D RID: 1597
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_STRING8")]
		MultiValueString8 = 4126,
		// Token: 0x0400063E RID: 1598
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_BINARY")]
		MultiValueBinary = 4354,
		// Token: 0x0400063F RID: 1599
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_UNICODE")]
		MultiValueUnicode = 4127,
		// Token: 0x04000640 RID: 1600
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_CLSID")]
		MultiValueGuid = 4168,
		// Token: 0x04000641 RID: 1601
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_LONGLONG")]
		[AlternativeName(AlternativeNaming.MAPI, "PT_MV_I8")]
		MultiValueInt64 = 4116
	}
}
