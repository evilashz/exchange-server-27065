using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000657 RID: 1623
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullCoreItemContext : ICoreItemContext
	{
		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x0011E08A File Offset: 0x0011C28A
		public static ICoreItemContext Instance
		{
			get
			{
				return NullCoreItemContext.instance;
			}
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x0011E091 File Offset: 0x0011C291
		public void GetContextCharsetDetectionData(StringBuilder stringBuilder, CharsetDetectionDataFlags flags)
		{
			Util.ThrowOnNullArgument(stringBuilder, "stringBuilder");
			EnumValidator.ThrowIfInvalid<CharsetDetectionDataFlags>(flags, "flags");
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x0011E0A9 File Offset: 0x0011C2A9
		private NullCoreItemContext()
		{
		}

		// Token: 0x040024C1 RID: 9409
		private static NullCoreItemContext instance = new NullCoreItemContext();
	}
}
