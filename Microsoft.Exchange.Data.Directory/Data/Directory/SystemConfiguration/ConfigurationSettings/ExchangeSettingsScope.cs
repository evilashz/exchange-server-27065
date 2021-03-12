using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000681 RID: 1665
	[Serializable]
	public enum ExchangeSettingsScope
	{
		// Token: 0x040034B7 RID: 13495
		Forest = 100,
		// Token: 0x040034B8 RID: 13496
		Dag = 150,
		// Token: 0x040034B9 RID: 13497
		Server = 200,
		// Token: 0x040034BA RID: 13498
		Process = 250,
		// Token: 0x040034BB RID: 13499
		Database = 300,
		// Token: 0x040034BC RID: 13500
		Organization = 400,
		// Token: 0x040034BD RID: 13501
		User = 500,
		// Token: 0x040034BE RID: 13502
		Generic = 600
	}
}
