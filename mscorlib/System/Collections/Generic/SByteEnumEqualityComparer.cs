using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000499 RID: 1177
	[Serializable]
	internal sealed class SByteEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x0600397D RID: 14717 RVA: 0x000DAC12 File Offset: 0x000D8E12
		public SByteEnumEqualityComparer()
		{
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x000DAC1A File Offset: 0x000D8E1A
		public SByteEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000DAC24 File Offset: 0x000D8E24
		public override int GetHashCode(T obj)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(obj);
			return ((sbyte)num).GetHashCode();
		}
	}
}
