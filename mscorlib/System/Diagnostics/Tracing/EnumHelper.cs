using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000413 RID: 1043
	internal static class EnumHelper<UnderlyingType>
	{
		// Token: 0x060034D7 RID: 13527 RVA: 0x000CD958 File Offset: 0x000CBB58
		public static UnderlyingType Cast<ValueType>(ValueType value)
		{
			return EnumHelper<UnderlyingType>.Caster<ValueType>.Instance(value);
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000CD965 File Offset: 0x000CBB65
		internal static UnderlyingType Identity(UnderlyingType value)
		{
			return value;
		}

		// Token: 0x04001761 RID: 5985
		private static readonly MethodInfo IdentityInfo = Statics.GetDeclaredStaticMethod(typeof(EnumHelper<UnderlyingType>), "Identity");

		// Token: 0x02000B63 RID: 2915
		// (Invoke) Token: 0x06006B68 RID: 27496
		private delegate UnderlyingType Transformer<ValueType>(ValueType value);

		// Token: 0x02000B64 RID: 2916
		private static class Caster<ValueType>
		{
			// Token: 0x04003440 RID: 13376
			public static readonly EnumHelper<UnderlyingType>.Transformer<ValueType> Instance = (EnumHelper<UnderlyingType>.Transformer<ValueType>)Statics.CreateDelegate(typeof(EnumHelper<UnderlyingType>.Transformer<ValueType>), EnumHelper<UnderlyingType>.IdentityInfo);
		}
	}
}
