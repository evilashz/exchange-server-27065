using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000376 RID: 886
	internal static class ClientRbac
	{
		// Token: 0x0600303B RID: 12347 RVA: 0x00093134 File Offset: 0x00091334
		internal static JsonDictionary<bool> GetRbacData()
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(ClientRbac.rbacRoles.Count + ClientRbac.HybridEcpFeatures.Count, StringComparer.OrdinalIgnoreCase);
			RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
			foreach (string text in ClientRbac.rbacRoles)
			{
				dictionary.Add(text, rbacPrincipal.IsInRole(text));
			}
			foreach (EcpFeature ecpFeature in ClientRbac.HybridEcpFeatures)
			{
				string name = ecpFeature.GetName();
				dictionary.Add(name, rbacPrincipal.IsInRole(name));
			}
			return new JsonDictionary<bool>(dictionary);
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x00093214 File Offset: 0x00091414
		[Conditional("DEBUG")]
		private static void IsValidKeyCheck(Dictionary<string, bool> dictionary, string key)
		{
			if (dictionary.ContainsKey(key))
			{
				throw new InvalidOperationException("Key '" + key + "' already added before. Please note the RBAC role on server side is not case sensitive.");
			}
			if (key.Contains("+") || key.Contains(","))
			{
				throw new InvalidOperationException("Key '" + key + "' contains '+' or ',', which is not supported. Please split them and add one by one.");
			}
		}

		// Token: 0x04002352 RID: 9042
		public static List<EcpFeature> HybridEcpFeatures = new List<EcpFeature>
		{
			EcpFeature.Onboarding,
			EcpFeature.SetupHybridConfiguration
		};

		// Token: 0x04002353 RID: 9043
		private static List<string> rbacRoles = new List<string>
		{
			"Enterprise",
			"LiveID",
			"HybridAdmin",
			"MyBaseOptions",
			"Get-Notification"
		};
	}
}
