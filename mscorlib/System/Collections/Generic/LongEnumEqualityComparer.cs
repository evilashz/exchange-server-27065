using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x0200049B RID: 1179
	[Serializable]
	internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003983 RID: 14723 RVA: 0x000DAC74 File Offset: 0x000D8E74
		public override bool Equals(T x, T y)
		{
			long num = JitHelpers.UnsafeEnumCastLong<T>(x);
			long num2 = JitHelpers.UnsafeEnumCastLong<T>(y);
			return num == num2;
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000DAC94 File Offset: 0x000D8E94
		public override int GetHashCode(T obj)
		{
			return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000DACB0 File Offset: 0x000D8EB0
		public override bool Equals(object obj)
		{
			LongEnumEqualityComparer<T> longEnumEqualityComparer = obj as LongEnumEqualityComparer<T>;
			return longEnumEqualityComparer != null;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000DACC8 File Offset: 0x000D8EC8
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000DACDA File Offset: 0x000D8EDA
		public LongEnumEqualityComparer()
		{
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000DACE2 File Offset: 0x000D8EE2
		public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000DACEA File Offset: 0x000D8EEA
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(typeof(ObjectEqualityComparer<T>));
		}
	}
}
