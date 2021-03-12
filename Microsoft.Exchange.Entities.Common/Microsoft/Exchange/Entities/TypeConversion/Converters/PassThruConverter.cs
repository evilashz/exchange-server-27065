using System;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x02000060 RID: 96
	internal sealed class PassThruConverter<TValue> : IConverter<TValue, TValue>
	{
		// Token: 0x06000221 RID: 545 RVA: 0x000079C3 File Offset: 0x00005BC3
		private PassThruConverter()
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000079CB File Offset: 0x00005BCB
		public static PassThruConverter<TValue> SingletonInstance
		{
			get
			{
				return PassThruConverter<TValue>.Instance;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000079D2 File Offset: 0x00005BD2
		public TValue Convert(TValue value)
		{
			return value;
		}

		// Token: 0x040000A7 RID: 167
		private static readonly PassThruConverter<TValue> Instance = new PassThruConverter<TValue>();
	}
}
