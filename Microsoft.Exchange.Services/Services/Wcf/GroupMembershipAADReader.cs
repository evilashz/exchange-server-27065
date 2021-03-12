using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.AAD;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200008D RID: 141
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupMembershipAADReader : IGroupMembershipReader<string>
	{
		// Token: 0x06000379 RID: 889 RVA: 0x00011325 File Offset: 0x0000F525
		public GroupMembershipAADReader(ADUser user, IGroupsLogger logger)
		{
			ArgumentValidator.ThrowIfNull("user", user);
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.user = user;
			this.logger = logger;
			this.stopwatch = new Stopwatch();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0001135C File Offset: 0x0000F55C
		public TimeSpan AADLatency
		{
			get
			{
				return this.stopwatch.Elapsed;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000113E4 File Offset: 0x0000F5E4
		public IEnumerable<string> GetJoinedGroups()
		{
			List<string> groups = null;
			this.stopwatch.Start();
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					IAadClient aadClient = AADClientFactory.CreateIAadClient(this.user);
					if (aadClient != null)
					{
						aadClient.Timeout = 10;
						groups = aadClient.GetGroupMembership(this.user.ExternalDirectoryObjectId).ToList<string>();
						return;
					}
					this.logger.LogTrace("GroupMembershipAADReader.GetJoinedGroups - Unable to read groups from AAD: AADClient is null.", new object[0]);
				}, (Exception e) => GrayException.IsSystemGrayException(e));
			}
			finally
			{
				this.stopwatch.Stop();
			}
			return groups;
		}

		// Token: 0x040005E9 RID: 1513
		private const int AADTimeoutInSeconds = 10;

		// Token: 0x040005EA RID: 1514
		private readonly ADUser user;

		// Token: 0x040005EB RID: 1515
		private readonly Stopwatch stopwatch;

		// Token: 0x040005EC RID: 1516
		private readonly IGroupsLogger logger;
	}
}
