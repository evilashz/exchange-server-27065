using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public sealed class UnifiedCompliancePrerequisite
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x000260F8 File Offset: 0x000242F8
		public UnifiedCompliancePrerequisite(Uri spRootSiteUrl, Uri spTenantAdminUrl, IEnumerable<string> prerequisiteList)
		{
			this.SharepointRootSiteUrl = spRootSiteUrl;
			this.SharepointTenantAdminUrl = spTenantAdminUrl;
			if (prerequisiteList != null)
			{
				char[] separator = new char[]
				{
					':'
				};
				foreach (string text in prerequisiteList)
				{
					if (!string.IsNullOrEmpty(text))
					{
						string[] array = text.Split(separator, 2);
						if (array != null && array.Length > 0 && !string.IsNullOrEmpty(array[0]))
						{
							string key = array[0];
							string value = string.Empty;
							if (array.Length > 1)
							{
								value = array[1];
							}
							if (!this.prerequisiteDictionary.ContainsKey(key))
							{
								this.prerequisiteDictionary.Add(key, value);
							}
						}
					}
				}
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x000261CC File Offset: 0x000243CC
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x000261D4 File Offset: 0x000243D4
		public Uri SharepointRootSiteUrl { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x000261DD File Offset: 0x000243DD
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x000261E5 File Offset: 0x000243E5
		public Uri SharepointTenantAdminUrl { get; private set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000261EE File Offset: 0x000243EE
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x000261FB File Offset: 0x000243FB
		public string SharepointSuccessInitializedUtc
		{
			get
			{
				return this.GetValue("SharepointSuccessInitializedUtc");
			}
			set
			{
				this.SetValue("SharepointSuccessInitializedUtc", value);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00026209 File Offset: 0x00024409
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x00026216 File Offset: 0x00024416
		public string SharepointPolicyCenterSiteUrl
		{
			get
			{
				return this.GetValue("SharepointPolicyCenterSiteUrl");
			}
			set
			{
				this.SetValue("SharepointPolicyCenterSiteUrl", value);
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00026224 File Offset: 0x00024424
		private void SetValue(string key, string value)
		{
			if (this.prerequisiteDictionary.ContainsKey(key))
			{
				this.prerequisiteDictionary[key] = value;
				return;
			}
			this.prerequisiteDictionary.Add(key, value);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002624F File Offset: 0x0002444F
		private string GetValue(string key)
		{
			if (this.prerequisiteDictionary.ContainsKey(key))
			{
				return this.prerequisiteDictionary[key];
			}
			return string.Empty;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00026271 File Offset: 0x00024471
		internal bool IsSharepointInitialized
		{
			get
			{
				return !string.IsNullOrEmpty(this.SharepointSuccessInitializedUtc) && !string.IsNullOrEmpty(this.SharepointPolicyCenterSiteUrl);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00026290 File Offset: 0x00024490
		internal bool CanInitializeSharepoint
		{
			get
			{
				return this.SharepointRootSiteUrl != null && this.SharepointTenantAdminUrl != null;
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000262B0 File Offset: 0x000244B0
		internal IEnumerable<string> ToPrerequisiteList()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in this.prerequisiteDictionary)
			{
				list.Add(string.Format("{0}{1}{2}", keyValuePair.Key, ':', keyValuePair.Value));
			}
			return list;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00026328 File Offset: 0x00024528
		internal void Redact()
		{
			string text;
			string text2;
			this.SharepointTenantAdminUrl = SuppressingPiiData.Redact(this.SharepointTenantAdminUrl, out text, out text2);
			this.SharepointRootSiteUrl = SuppressingPiiData.Redact(this.SharepointRootSiteUrl, out text, out text2);
			foreach (string key in this.prerequisiteDictionary.Keys.ToList<string>())
			{
				this.prerequisiteDictionary[key] = SuppressingPiiData.Redact(this.prerequisiteDictionary[key]);
			}
		}

		// Token: 0x040003E3 RID: 995
		private const char ColonSeparator = ':';

		// Token: 0x040003E4 RID: 996
		private Dictionary<string, string> prerequisiteDictionary = new Dictionary<string, string>();
	}
}
