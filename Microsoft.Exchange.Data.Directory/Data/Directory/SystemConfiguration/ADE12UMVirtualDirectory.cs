using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200039F RID: 927
	[Serializable]
	public sealed class ADE12UMVirtualDirectory : ADExchangeServiceVirtualDirectory
	{
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x000B2D76 File Offset: 0x000B0F76
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADE12UMVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x040019BC RID: 6588
		public static readonly string MostDerivedClass = "msExchUMVirtualDirectory";
	}
}
