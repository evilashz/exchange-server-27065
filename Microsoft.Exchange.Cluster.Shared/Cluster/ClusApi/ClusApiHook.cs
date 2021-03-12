using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000017 RID: 23
	internal class ClusApiHook : IClusApiHook
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00003954 File Offset: 0x00001B54
		public static void SetClusApiHook(IClusApiHook newHook)
		{
			ClusApiHook.instance = newHook;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000395C File Offset: 0x00001B5C
		public int CallBack(ClusApiHooks api, string hintStr, Func<int> func)
		{
			return func();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003964 File Offset: 0x00001B64
		public static int CallBackDriver(ClusApiHooks api, string hintStr, Func<int> func)
		{
			return ClusApiHook.instance.CallBack(api, hintStr, func);
		}

		// Token: 0x04000031 RID: 49
		private static IClusApiHook instance = new ClusApiHook();
	}
}
