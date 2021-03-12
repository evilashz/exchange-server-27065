using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020000D2 RID: 210
	[ComVisible(true)]
	[Serializable]
	public sealed class DBNull : ISerializable, IConvertible
	{
		// Token: 0x06000CF9 RID: 3321 RVA: 0x00027A6C File Offset: 0x00025C6C
		private DBNull()
		{
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00027A74 File Offset: 0x00025C74
		private DBNull(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DBNullSerial"));
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00027A8B File Offset: 0x00025C8B
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetUnitySerializationInfo(info, 2, null, null);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00027A96 File Offset: 0x00025C96
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00027A9D File Offset: 0x00025C9D
		public string ToString(IFormatProvider provider)
		{
			return string.Empty;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00027AA4 File Offset: 0x00025CA4
		public TypeCode GetTypeCode()
		{
			return TypeCode.DBNull;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00027AA7 File Offset: 0x00025CA7
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00027AB8 File Offset: 0x00025CB8
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00027AC9 File Offset: 0x00025CC9
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00027ADA File Offset: 0x00025CDA
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00027AEB File Offset: 0x00025CEB
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00027AFC File Offset: 0x00025CFC
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00027B0D File Offset: 0x00025D0D
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00027B1E File Offset: 0x00025D1E
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00027B2F File Offset: 0x00025D2F
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00027B40 File Offset: 0x00025D40
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00027B51 File Offset: 0x00025D51
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00027B62 File Offset: 0x00025D62
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00027B73 File Offset: 0x00025D73
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00027B84 File Offset: 0x00025D84
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromDBNull"));
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00027B95 File Offset: 0x00025D95
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0400054A RID: 1354
		public static readonly DBNull Value = new DBNull();
	}
}
