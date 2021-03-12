using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data.Rus
{
	// Token: 0x020001D0 RID: 464
	internal class RusConfigSession
	{
		// Token: 0x06001373 RID: 4979 RVA: 0x0003ACB3 File Offset: 0x00038EB3
		public RusConfigSession()
		{
			this.DataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Directory);
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0003ACCC File Offset: 0x00038ECC
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x0003ACD4 File Offset: 0x00038ED4
		private IConfigDataProvider DataProvider { get; set; }

		// Token: 0x06001376 RID: 4982 RVA: 0x0003ACE0 File Offset: 0x00038EE0
		public RusConfig FindRusConfigProperty()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "RUS_Default");
			return this.DataProvider.Find<RusConfig>(filter, null, false, null).Cast<RusConfig>().FirstOrDefault<RusConfig>();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0003AD17 File Offset: 0x00038F17
		public void UpdateRusConfigUniversalManifestVersion(string newVersion)
		{
			this.UpdateRusConfigUM(newVersion, false);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0003AD21 File Offset: 0x00038F21
		public void UpdateRusConfigUniversalManifestVersionV2(string newVersion)
		{
			this.UpdateRusConfigUM(newVersion, true);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0003AD2C File Offset: 0x00038F2C
		private void UpdateRusConfigUM(string value, bool isV2 = false)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("UMVersionValue");
			}
			RusConfig rusConfig = this.FindRusConfigProperty();
			if (rusConfig == null)
			{
				rusConfig = new RusConfig();
			}
			if (isV2)
			{
				rusConfig.UniversalManifestVersionV2 = value;
			}
			else
			{
				rusConfig.UniversalManifestVersion = value;
			}
			this.DataProvider.Save(rusConfig);
		}
	}
}
