using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x0200049A RID: 1178
	[Serializable]
	internal sealed class ShortEnumEqualityComparer<T> : EnumEqualityComparer<T>, ISerializable where T : struct
	{
		// Token: 0x06003980 RID: 14720 RVA: 0x000DAC42 File Offset: 0x000D8E42
		public ShortEnumEqualityComparer()
		{
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000DAC4A File Offset: 0x000D8E4A
		public ShortEnumEqualityComparer(SerializationInfo information, StreamingContext context)
		{
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000DAC54 File Offset: 0x000D8E54
		public override int GetHashCode(T obj)
		{
			int num = JitHelpers.UnsafeEnumCast<T>(obj);
			return ((short)num).GetHashCode();
		}
	}
}
