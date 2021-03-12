using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000945 RID: 2373
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderConnectionLimitsTracker
	{
		// Token: 0x0600585D RID: 22621 RVA: 0x0016B5DC File Offset: 0x001697DC
		private PublicFolderConnectionLimitsTracker()
		{
			this.maxTokens = StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder", "MaxCrossServerConnections", 20, null);
			this.minTokensPerServer = StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder", "MinCrossServerConnections", 5, null);
			this.reservedTokensPerActiveServer = StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\PublicFolder", "ReservedConnectionsPerActiveServer", 3, null);
			this.availableTokenCount = this.maxTokens;
			this.tokenTracker = new Dictionary<string, int>();
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x0600585E RID: 22622 RVA: 0x0016B64C File Offset: 0x0016984C
		public static PublicFolderConnectionLimitsTracker Instance
		{
			get
			{
				return PublicFolderConnectionLimitsTracker.instance;
			}
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x0016B670 File Offset: 0x00169870
		public DisposableFrame GetToken(string server)
		{
			if (string.IsNullOrWhiteSpace(server))
			{
				throw new ArgumentNullException("server");
			}
			string key = server.ToLowerInvariant();
			DisposableFrame result;
			lock (this.tokenTracker)
			{
				this.ThrowIfOverLimit(key);
				if (!this.tokenTracker.ContainsKey(key))
				{
					this.tokenTracker[key] = 1;
				}
				else
				{
					Dictionary<string, int> dictionary;
					string key2;
					(dictionary = this.tokenTracker)[key2 = key] = dictionary[key2] + 1;
				}
				this.availableTokenCount--;
				result = new DisposableFrame(delegate()
				{
					this.ReturnToken(key);
				});
			}
			return result;
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x0016B758 File Offset: 0x00169958
		private void ReturnToken(string server)
		{
			lock (this.tokenTracker)
			{
				if (this.tokenTracker.ContainsKey(server))
				{
					this.availableTokenCount++;
					Dictionary<string, int> dictionary;
					if (((dictionary = this.tokenTracker)[server] = dictionary[server] - 1) == 0)
					{
						this.tokenTracker.Remove(server);
					}
				}
			}
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x0016B7DC File Offset: 0x001699DC
		private void ThrowIfOverLimit(string server)
		{
			if (this.availableTokenCount == 0)
			{
				throw new LimitExceededException(ServerStrings.PublicFolderConnectionThreadLimitExceeded(this.maxTokens));
			}
			int num;
			this.tokenTracker.TryGetValue(server, out num);
			int count = this.tokenTracker.Count;
			int num2 = Math.Max(this.minTokensPerServer, this.maxTokens - this.reservedTokensPerActiveServer * count);
			if (num >= num2)
			{
				throw new LimitExceededException(ServerStrings.PublicFolderPerServerThreadLimitExceeded(server, num2, count));
			}
		}

		// Token: 0x04003020 RID: 12320
		private static readonly PublicFolderConnectionLimitsTracker instance = new PublicFolderConnectionLimitsTracker();

		// Token: 0x04003021 RID: 12321
		private readonly int maxTokens;

		// Token: 0x04003022 RID: 12322
		private readonly int minTokensPerServer;

		// Token: 0x04003023 RID: 12323
		private readonly int reservedTokensPerActiveServer;

		// Token: 0x04003024 RID: 12324
		private Dictionary<string, int> tokenTracker;

		// Token: 0x04003025 RID: 12325
		private int availableTokenCount;
	}
}
