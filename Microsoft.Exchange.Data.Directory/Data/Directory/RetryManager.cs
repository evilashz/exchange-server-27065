using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200017D RID: 381
	[Serializable]
	internal class RetryManager
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0004F674 File Offset: 0x0004D874
		public void Tried(string serverFqdn)
		{
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			string text = serverFqdn.ToLowerInvariant();
			if (this.triedServers == null)
			{
				this.triedServers = new Dictionary<string, int>();
			}
			this.totalRetries++;
			if (this.triedServers.ContainsKey(text))
			{
				Dictionary<string, int> dictionary;
				string key;
				(dictionary = this.triedServers)[key = text] = dictionary[key] + 1;
				return;
			}
			this.triedServers.Add(text, 1);
		}

		// Token: 0x170002B5 RID: 693
		public int this[string serverFqdn]
		{
			get
			{
				if (string.IsNullOrEmpty(serverFqdn))
				{
					throw new ArgumentNullException("serverFqdn");
				}
				string key = serverFqdn.ToLowerInvariant();
				if (this.triedServers == null || !this.triedServers.ContainsKey(key))
				{
					return 0;
				}
				return this.triedServers[key];
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0004F73F File Offset: 0x0004D93F
		public int TotalRetries
		{
			get
			{
				return this.totalRetries;
			}
		}

		// Token: 0x0400095D RID: 2397
		private int totalRetries;

		// Token: 0x0400095E RID: 2398
		private Dictionary<string, int> triedServers;
	}
}
