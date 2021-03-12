using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000706 RID: 1798
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface IFormatterConverter
	{
		// Token: 0x0600508F RID: 20623
		object Convert(object value, Type type);

		// Token: 0x06005090 RID: 20624
		object Convert(object value, TypeCode typeCode);

		// Token: 0x06005091 RID: 20625
		bool ToBoolean(object value);

		// Token: 0x06005092 RID: 20626
		char ToChar(object value);

		// Token: 0x06005093 RID: 20627
		sbyte ToSByte(object value);

		// Token: 0x06005094 RID: 20628
		byte ToByte(object value);

		// Token: 0x06005095 RID: 20629
		short ToInt16(object value);

		// Token: 0x06005096 RID: 20630
		ushort ToUInt16(object value);

		// Token: 0x06005097 RID: 20631
		int ToInt32(object value);

		// Token: 0x06005098 RID: 20632
		uint ToUInt32(object value);

		// Token: 0x06005099 RID: 20633
		long ToInt64(object value);

		// Token: 0x0600509A RID: 20634
		ulong ToUInt64(object value);

		// Token: 0x0600509B RID: 20635
		float ToSingle(object value);

		// Token: 0x0600509C RID: 20636
		double ToDouble(object value);

		// Token: 0x0600509D RID: 20637
		decimal ToDecimal(object value);

		// Token: 0x0600509E RID: 20638
		DateTime ToDateTime(object value);

		// Token: 0x0600509F RID: 20639
		string ToString(object value);
	}
}
