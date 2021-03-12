using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x02000204 RID: 516
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SimpleDatabaseSettingsContext : SettingsContextBase
	{
		// Token: 0x060011FC RID: 4604 RVA: 0x000361CB File Offset: 0x000343CB
		public SimpleDatabaseSettingsContext(Guid mdbGuid, SettingsContextBase nextContext = null) : base(nextContext)
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x000361DB File Offset: 0x000343DB
		public override Guid? DatabaseGuid
		{
			get
			{
				return new Guid?(this.mdbGuid);
			}
		}

		// Token: 0x04000ADB RID: 2779
		private readonly Guid mdbGuid;
	}
}
