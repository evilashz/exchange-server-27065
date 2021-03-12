using System;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000675 RID: 1653
	[Serializable]
	public class SettingsForestScope : SettingsScope
	{
		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06004D2B RID: 19755 RVA: 0x0011D0AE File Offset: 0x0011B2AE
		internal override int DefaultPriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0011D0B2 File Offset: 0x0011B2B2
		internal override QueryFilter ConstructScopeFilter(IConfigSchema schema)
		{
			return QueryFilter.True;
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0011D0B9 File Offset: 0x0011B2B9
		public override string ToString()
		{
			return "Forest";
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0011D0C0 File Offset: 0x0011B2C0
		internal override void Validate(IConfigSchema schema)
		{
			if (base.Restriction != null)
			{
				throw new ConfigurationSettingsRestrictionNotExpectedException(base.GetType().Name);
			}
		}
	}
}
