using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200007B RID: 123
	public class ReadOnceConfigurationSchema<TConfig> : ConfigurationSchema<TConfig>
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x000134C7 File Offset: 0x000116C7
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue) : base(name, defaultValue)
		{
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000134D1 File Offset: 0x000116D1
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, string registryKey, string registryValue) : base(name, defaultValue, registryKey, registryValue)
		{
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000134DE File Offset: 0x000116DE
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse) : base(name, defaultValue, tryParse)
		{
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000134E9 File Offset: 0x000116E9
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, string registryKey, string registryValue) : base(name, defaultValue, tryParse, registryKey, registryValue)
		{
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000134F8 File Offset: 0x000116F8
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess) : base(name, defaultValue, postProcess)
		{
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00013503 File Offset: 0x00011703
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess, string registryKey, string registryValue) : base(name, defaultValue, postProcess, registryKey, registryValue)
		{
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00013512 File Offset: 0x00011712
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator) : base(name, defaultValue, postProcess, validator)
		{
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001351F File Offset: 0x0001171F
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator, string registryKey, string registryValue) : base(name, defaultValue, postProcess, validator, registryKey, registryValue)
		{
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00013530 File Offset: 0x00011730
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, Func<TConfig, TConfig> postProcess) : base(name, defaultValue, tryParse, postProcess)
		{
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001353D File Offset: 0x0001173D
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, Func<TConfig, TConfig> postProcess, string registryKey, string registryValue) : base(name, defaultValue, tryParse, postProcess, registryKey, registryValue)
		{
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001354E File Offset: 0x0001174E
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator) : base(name, defaultValue, tryParse, postProcess, validator)
		{
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001355D File Offset: 0x0001175D
		public ReadOnceConfigurationSchema(string name, TConfig defaultValue, ConfigurationSchema<TConfig>.TryParse tryParse, Func<TConfig, TConfig> postProcess, Func<TConfig, bool> validator, string registryKey, string registryValue) : base(name, defaultValue, tryParse, postProcess, validator, registryKey, registryValue)
		{
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00013570 File Offset: 0x00011770
		public override void Reload()
		{
			if (!this.loaded)
			{
				this.loaded = true;
				base.Reload();
			}
		}

		// Token: 0x0400064F RID: 1615
		private bool loaded;
	}
}
