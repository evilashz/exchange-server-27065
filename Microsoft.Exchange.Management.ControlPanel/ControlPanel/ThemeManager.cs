using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200055F RID: 1375
	public static class ThemeManager
	{
		// Token: 0x06004031 RID: 16433 RVA: 0x000C3748 File Offset: 0x000C1948
		public static string GetDefaultThemeName(FeatureSet featureSet)
		{
			switch (featureSet)
			{
			case FeatureSet.Options:
				return "options";
			}
			return "default";
		}

		// Token: 0x04002AC2 RID: 10946
		private const string AdminDefaultTheme = "default";

		// Token: 0x04002AC3 RID: 10947
		private const string OptionsDefaultTheme = "options";
	}
}
