using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.PublicFolder
{
	// Token: 0x02000938 RID: 2360
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExchangePrincipalCache : LazyLookupTimeoutCache<MultiValueKey, ExchangePrincipal>
	{
		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x060057E3 RID: 22499 RVA: 0x00169EBB File Offset: 0x001680BB
		public static ExchangePrincipalCache Instance
		{
			get
			{
				return ExchangePrincipalCache.instance;
			}
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x00169EC2 File Offset: 0x001680C2
		private ExchangePrincipalCache() : base(5, 1000, false, TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(15.0))
		{
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x00169EF0 File Offset: 0x001680F0
		protected override ExchangePrincipal CreateOnCacheMiss(MultiValueKey cacheKey, ref bool shouldAdd)
		{
			if (cacheKey.KeyLength != 2)
			{
				throw new ArgumentException(string.Format("Unexpected cacheKey length: {0}", cacheKey.KeyLength), "cacheKey");
			}
			OrganizationId organizationId = (OrganizationId)cacheKey.GetKey(0);
			Guid mailboxGuid = (Guid)cacheKey.GetKey(1);
			ExchangePrincipal result = ExchangePrincipal.FromMailboxGuid(organizationId.ToADSessionSettings(), mailboxGuid, RemotingOptions.AllowCrossSite, null);
			shouldAdd = true;
			return result;
		}

		// Token: 0x04002FFE RID: 12286
		private static ExchangePrincipalCache instance = new ExchangePrincipalCache();
	}
}
