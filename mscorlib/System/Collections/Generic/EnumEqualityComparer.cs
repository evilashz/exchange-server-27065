using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000498 RID: 1176
	[Serializable]
	internal class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003976 RID: 14710 RVA: 0x000DAB70 File Offset: 0x000D8D70
		public override bool Equals(T x, T y)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(x);
			int num2 = JitHelpers.UnsafeEnumCast<T>(y);
			return num == num2;
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000DAB90 File Offset: 0x000D8D90
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCast<T>(obj).GetHashCode();
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000DABAB File Offset: 0x000D8DAB
		public EnumEqualityComparer()
		{
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000DABB3 File Offset: 0x000D8DB3
		protected EnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000DABBB File Offset: 0x000D8DBB
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T))) != TypeCode.Int32)
			{
				info.SetType(typeof(ObjectEqualityComparer<T>));
			}
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000DABE8 File Offset: 0x000D8DE8
		public override bool Equals(object obj)
		{
			EnumEqualityComparer<T> enumEqualityComparer = obj as EnumEqualityComparer<T>;
			return enumEqualityComparer != null;
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000DAC00 File Offset: 0x000D8E00
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
