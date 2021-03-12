using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LanguagePackInstallInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000A20D File Offset: 0x0000840D
		public override string Name
		{
			get
			{
				return "LanguagePacks";
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000A214 File Offset: 0x00008414
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.CopyLanguagePacksDisplayName;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000A21C File Offset: 0x0000841C
		public override decimal Size
		{
			get
			{
				if (this.totalSize < 0.0m && InstallableUnitConfigurationInfo.SetupContext != null && InstallableUnitConfigurationInfo.SetupContext.LanguagesToInstall != null)
				{
					this.totalSize = 0.0m;
					foreach (KeyValuePair<string, long> keyValuePair in InstallableUnitConfigurationInfo.SetupContext.LanguagesToInstall)
					{
						this.totalSize += keyValuePair.Value;
					}
					this.totalSize /= 1048576m;
				}
				return this.totalSize;
			}
		}

		// Token: 0x040000AD RID: 173
		private const int megaByte = 1048576;

		// Token: 0x040000AE RID: 174
		private decimal totalSize = -1.0m;
	}
}
