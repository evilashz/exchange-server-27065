using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LanguagePackConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000A126 File Offset: 0x00008326
		public override string Name
		{
			get
			{
				return "LanguagePacks";
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000A12D File Offset: 0x0000832D
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.LanguagePacksDisplayName;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000A134 File Offset: 0x00008334
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
				}
				return this.totalSize / 1048576m;
			}
		}

		// Token: 0x040000AB RID: 171
		private const int megaByte = 1048576;

		// Token: 0x040000AC RID: 172
		private decimal totalSize = -1.0m;
	}
}
