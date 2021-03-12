using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C63 RID: 3171
	public class PswsAppSettings : AutoLoadAppSettings, IAppSettings
	{
		// Token: 0x06004643 RID: 17987 RVA: 0x000BBFDC File Offset: 0x000BA1DC
		private PswsAppSettings()
		{
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x000BBFE4 File Offset: 0x000BA1E4
		public new static PswsAppSettings Instance
		{
			get
			{
				PswsAppSettings result;
				if ((result = PswsAppSettings.instance) == null)
				{
					result = (PswsAppSettings.instance = new PswsAppSettings());
				}
				return result;
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x000BBFFA File Offset: 0x000BA1FA
		bool IAppSettings.FailFastEnabled
		{
			get
			{
				throw new NotSupportedException("FailFastEnabled is not supported to be used in PswsAppSettings.");
			}
		}

		// Token: 0x04003AA8 RID: 15016
		private static PswsAppSettings instance;
	}
}
