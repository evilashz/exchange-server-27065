using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class InstallableUnitConfigurationInfo
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000581C File Offset: 0x00003A1C
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00005823 File Offset: 0x00003A23
		public static SetupContext SetupContext
		{
			get
			{
				return InstallableUnitConfigurationInfo.setupContext;
			}
			set
			{
				InstallableUnitConfigurationInfo.setupContext = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000128 RID: 296
		public abstract string Name { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000129 RID: 297
		public abstract LocalizedString DisplayName { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600012A RID: 298
		public abstract decimal Size { get; }

		// Token: 0x04000042 RID: 66
		private static SetupContext setupContext;
	}
}
