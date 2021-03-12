using System;
using System.Reflection;
using System.Web;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x0200000D RID: 13
	internal static class NativeIisInteropBackend
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000254C File Offset: 0x0000074C
		public static void SetServerVariable(this HttpContextBase httpContext, string name, string value)
		{
			HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)((IServiceProvider)httpContext).GetService(typeof(HttpWorkerRequest));
			if (httpWorkerRequest == null)
			{
				return;
			}
			if (NativeIisInteropBackend.setServerVariableMethod == null)
			{
				NativeIisInteropBackend.Initialize(httpWorkerRequest);
			}
			object[] parameters = new object[]
			{
				name,
				value
			};
			NativeIisInteropBackend.setServerVariableMethod.Invoke(httpWorkerRequest, parameters);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025A4 File Offset: 0x000007A4
		private static void Initialize(HttpWorkerRequest httpWorkerRequest)
		{
			lock (NativeIisInteropBackend.initializationLock)
			{
				BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
				NativeIisInteropBackend.setServerVariableMethod = httpWorkerRequest.GetType().GetMethod(NativeIisInteropBackend.setServerVariableMethodName, bindingAttr);
			}
		}

		// Token: 0x0400000B RID: 11
		private static readonly object initializationLock = new object();

		// Token: 0x0400000C RID: 12
		private static MethodInfo setServerVariableMethod;

		// Token: 0x0400000D RID: 13
		private static string setServerVariableMethodName = "SetServerVariable";
	}
}
