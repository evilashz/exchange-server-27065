using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200021A RID: 538
	internal sealed class PerTenantPolicyTipMessageConfig : TenantConfigurationCacheableItem<PolicyTipMessageConfig>
	{
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0004974C File Offset: 0x0004794C
		public override long ItemSize
		{
			get
			{
				if (this.tenantPolicyTipMessageConfigsDictionary == null)
				{
					return 18L;
				}
				long num = 18L;
				return num + (this.estimatedTenantPolicyTipMessageConfigsDictionarySize + 8L);
			}
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00049794 File Offset: 0x00047994
		public override void ReadData(IConfigurationSession session)
		{
			ADPagedReader<PolicyTipMessageConfig> adpagedReader = session.FindPaged<PolicyTipMessageConfig>(session.GetOrgContainerId().GetDescendantId(PolicyTipMessageConfig.PolicyTipMessageConfigContainer), QueryScope.SubTree, null, null, 0);
			IEnumerable<PolicyTipMessageConfig> source = adpagedReader.ReadAllPages();
			this.tenantPolicyTipMessageConfigsDictionary = source.ToDictionary((PolicyTipMessageConfig m) => Tuple.Create<string, PolicyTipMessageConfigAction>(m.Locale, m.Action), (PolicyTipMessageConfig m) => m.Value);
			long num = 0L;
			foreach (KeyValuePair<Tuple<string, PolicyTipMessageConfigAction>, string> keyValuePair in this.tenantPolicyTipMessageConfigsDictionary)
			{
				Tuple<string, PolicyTipMessageConfigAction> key = keyValuePair.Key;
				num += (long)key.Item1.Length;
				num += 4L;
				num += (long)keyValuePair.Value.Length;
			}
			this.estimatedTenantPolicyTipMessageConfigsDictionarySize = num;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00049884 File Offset: 0x00047A84
		public string GetPolicyTipMessage(string locale, PolicyTipMessageConfigAction action)
		{
			return PerTenantPolicyTipMessageConfig.GetPolicyTipMessage(locale, action, this.tenantPolicyTipMessageConfigsDictionary);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00049894 File Offset: 0x00047A94
		public static string GetPolicyTipMessage(string locale, PolicyTipMessageConfigAction action, Dictionary<Tuple<string, PolicyTipMessageConfigAction>, string> tenantPolicyTipMessageConfigsDictionary)
		{
			if (tenantPolicyTipMessageConfigsDictionary == null || (string.IsNullOrEmpty(locale) && action != PolicyTipMessageConfigAction.Url))
			{
				return null;
			}
			if (action == PolicyTipMessageConfigAction.Url)
			{
				locale = string.Empty;
			}
			string text = null;
			Tuple<string, PolicyTipMessageConfigAction> key = new Tuple<string, PolicyTipMessageConfigAction>(locale, action);
			tenantPolicyTipMessageConfigsDictionary.TryGetValue(key, out text);
			if (text == null)
			{
				int num = locale.IndexOf('-');
				if (num > 0)
				{
					locale = locale.Substring(0, num);
					text = PerTenantPolicyTipMessageConfig.GetPolicyTipMessage(locale, action, tenantPolicyTipMessageConfigsDictionary);
				}
			}
			return text;
		}

		// Token: 0x04000B3B RID: 2875
		private const int FixedClrObjectOverhead = 18;

		// Token: 0x04000B3C RID: 2876
		private long estimatedTenantPolicyTipMessageConfigsDictionarySize;

		// Token: 0x04000B3D RID: 2877
		private Dictionary<Tuple<string, PolicyTipMessageConfigAction>, string> tenantPolicyTipMessageConfigsDictionary;
	}
}
