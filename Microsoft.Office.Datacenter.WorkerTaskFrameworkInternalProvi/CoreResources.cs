using System;
using System.Reflection;
using System.Resources;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200003B RID: 59
	internal static class CoreResources
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000C71C File Offset: 0x0000A91C
		public static string WorkItemFailedDefaultError(string result)
		{
			return string.Format(CoreResources.ResourceManager.GetString("WorkItemFailedDefaultError"), result);
		}

		// Token: 0x04000177 RID: 375
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Office.Datacenter.WorkerTaskFramework.CoreResources", typeof(CoreResources).GetTypeInfo().Assembly);

		// Token: 0x0200003C RID: 60
		private enum ParamIDs
		{
			// Token: 0x04000179 RID: 377
			WorkItemFailedDefaultError
		}
	}
}
