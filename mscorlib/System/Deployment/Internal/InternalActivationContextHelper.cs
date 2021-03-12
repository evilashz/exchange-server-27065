using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
	// Token: 0x02000640 RID: 1600
	[ComVisible(false)]
	public static class InternalActivationContextHelper
	{
		// Token: 0x06004DF6 RID: 19958 RVA: 0x001178D8 File Offset: 0x00115AD8
		[SecuritySafeCritical]
		public static object GetActivationContextData(ActivationContext appInfo)
		{
			return appInfo.ActivationContextData;
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x001178E0 File Offset: 0x00115AE0
		[SecuritySafeCritical]
		public static object GetApplicationComponentManifest(ActivationContext appInfo)
		{
			return appInfo.ApplicationComponentManifest;
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x001178E8 File Offset: 0x00115AE8
		[SecuritySafeCritical]
		public static object GetDeploymentComponentManifest(ActivationContext appInfo)
		{
			return appInfo.DeploymentComponentManifest;
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x001178F0 File Offset: 0x00115AF0
		public static void PrepareForExecution(ActivationContext appInfo)
		{
			appInfo.PrepareForExecution();
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x001178F8 File Offset: 0x00115AF8
		public static bool IsFirstRun(ActivationContext appInfo)
		{
			return appInfo.LastApplicationStateResult == ActivationContext.ApplicationStateDisposition.RunningFirstTime;
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x00117907 File Offset: 0x00115B07
		public static byte[] GetApplicationManifestBytes(ActivationContext appInfo)
		{
			if (appInfo == null)
			{
				throw new ArgumentNullException("appInfo");
			}
			return appInfo.GetApplicationManifestBytes();
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x0011791D File Offset: 0x00115B1D
		public static byte[] GetDeploymentManifestBytes(ActivationContext appInfo)
		{
			if (appInfo == null)
			{
				throw new ArgumentNullException("appInfo");
			}
			return appInfo.GetDeploymentManifestBytes();
		}
	}
}
