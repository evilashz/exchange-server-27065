using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000999 RID: 2457
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TeamMailboxADUserResolver : LazyLookupTimeoutCache<ADObjectId, ADUser>
	{
		// Token: 0x06005A97 RID: 23191 RVA: 0x001790F8 File Offset: 0x001772F8
		private TeamMailboxADUserResolver() : base(10, 1000, false, TimeSpan.FromHours(1.0), TimeSpan.FromHours(1.0))
		{
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x00179124 File Offset: 0x00177324
		public static ADUser ResolveBypassCache(IRecipientSession dataSession, ADObjectId id, out Exception ex)
		{
			TeamMailboxADUserResolver.RemoveIdIfExists(id);
			return TeamMailboxADUserResolver.Resolve(dataSession, id, out ex);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x00179134 File Offset: 0x00177334
		public static ADUser Resolve(IRecipientSession dataSession, ADObjectId id, out Exception ex)
		{
			if (dataSession == null)
			{
				throw new ArgumentNullException("dataSession");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			TeamMailboxADUserResolver.instance.dataSession = dataSession;
			ADUser result = null;
			ex = null;
			try
			{
				result = TeamMailboxADUserResolver.instance.Get(id);
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADExternalException ex3)
			{
				ex = ex3;
			}
			catch (ADOperationException ex4)
			{
				ex = ex4;
			}
			return result;
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x001791B8 File Offset: 0x001773B8
		public static void RemoveIdIfExists(ADObjectId id)
		{
			if (TeamMailboxADUserResolver.instance.Contains(id))
			{
				TeamMailboxADUserResolver.instance.Remove(id);
			}
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x001791D4 File Offset: 0x001773D4
		protected override ADUser CreateOnCacheMiss(ADObjectId key, ref bool shouldAdd)
		{
			ADUser aduser = this.dataSession.FindADUserByObjectId(key);
			shouldAdd = (aduser != null);
			return aduser;
		}

		// Token: 0x04003202 RID: 12802
		private static TeamMailboxADUserResolver instance = new TeamMailboxADUserResolver();

		// Token: 0x04003203 RID: 12803
		private IRecipientSession dataSession;
	}
}
