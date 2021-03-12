using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000124 RID: 292
	public sealed class VariantConfigurationUCCComponent : VariantConfigurationComponent
	{
		// Token: 0x06000DB2 RID: 3506 RVA: 0x00021230 File Offset: 0x0001F430
		internal VariantConfigurationUCCComponent() : base("UCC")
		{
			base.Add(new VariantConfigurationSection("UCC.settings.ini", "UCC", typeof(IFeature), false));
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0002125D File Offset: 0x0001F45D
		public VariantConfigurationSection UCC
		{
			get
			{
				return base["UCC"];
			}
		}
	}
}
