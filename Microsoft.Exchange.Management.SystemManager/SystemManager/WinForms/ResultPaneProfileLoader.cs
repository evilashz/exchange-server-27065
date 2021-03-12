using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001FF RID: 511
	public static class ResultPaneProfileLoader
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x000629F2 File Offset: 0x00060BF2
		public static ObjectPickerProfileLoader Loader
		{
			get
			{
				return ResultPaneProfileLoader.loader;
			}
		}

		// Token: 0x040008BB RID: 2235
		private static readonly ObjectPickerProfileLoader loader = new ObjectPickerProfileLoader(1);
	}
}
