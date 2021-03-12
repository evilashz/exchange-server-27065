using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000215 RID: 533
	internal class EmptyRecipientCache : IADRecipientCache, IEnumerable<KeyValuePair<ProxyAddress, Result<ADRawEntry>>>, IEnumerable
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00076044 File Offset: 0x00074244
		public OrganizationId OrganizationId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0007604B File Offset: 0x0007424B
		public IRecipientSession ADSession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00076052 File Offset: 0x00074252
		public ReadOnlyCollection<ADPropertyDefinition> CachedADProperties
		{
			get
			{
				return new ReadOnlyCollection<ADPropertyDefinition>(new List<ADPropertyDefinition>());
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0007605E File Offset: 0x0007425E
		public ICollection<ProxyAddress> Keys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00076065 File Offset: 0x00074265
		public IEnumerable<Result<ADRawEntry>> Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0007606C File Offset: 0x0007426C
		public int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000760C4 File Offset: 0x000742C4
		IEnumerator<KeyValuePair<ProxyAddress, Result<ADRawEntry>>> IEnumerable<KeyValuePair<ProxyAddress, Result<ADRawEntry>>>.GetEnumerator()
		{
			yield break;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00076128 File Offset: 0x00074328
		IEnumerator IEnumerable.GetEnumerator()
		{
			yield break;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00076144 File Offset: 0x00074344
		public Result<ADRawEntry> FindAndCacheRecipient(ProxyAddress proxyAddress)
		{
			return new Result<ADRawEntry>(null, ProviderError.NotFound);
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00076151 File Offset: 0x00074351
		public Result<ADRawEntry> FindAndCacheRecipient(ADObjectId objectId)
		{
			return new Result<ADRawEntry>(null, ProviderError.NotFound);
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00076160 File Offset: 0x00074360
		public IList<Result<ADRawEntry>> FindAndCacheRecipients(IList<ProxyAddress> proxyAddresses)
		{
			Result<ADRawEntry>[] array = new Result<ADRawEntry>[proxyAddresses.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.FindAndCacheRecipient(proxyAddresses[i]);
			}
			return array;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000761A1 File Offset: 0x000743A1
		public void AddCacheEntry(ProxyAddress proxyAddress, Result<ADRawEntry> result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000761A8 File Offset: 0x000743A8
		public bool ContainsKey(ProxyAddress proxyAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000761AF File Offset: 0x000743AF
		public bool CopyEntryFrom(IADRecipientCache cacheToCopyFrom, string smtpAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000761B6 File Offset: 0x000743B6
		public bool CopyEntryFrom(IADRecipientCache cacheToCopyFrom, ProxyAddress proxyAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000761BD File Offset: 0x000743BD
		public Result<ADRawEntry> ReadSecurityDescriptor(ProxyAddress proxyAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000761C4 File Offset: 0x000743C4
		public void DropSecurityDescriptor(ProxyAddress proxyAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000761CB File Offset: 0x000743CB
		public Result<ADRawEntry> ReloadRecipient(ProxyAddress proxyAddress, IEnumerable<ADPropertyDefinition> extraProperties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000761D2 File Offset: 0x000743D2
		public bool Remove(ProxyAddress proxyAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000761D9 File Offset: 0x000743D9
		public bool TryGetValue(ProxyAddress proxyAddress, out Result<ADRawEntry> result)
		{
			throw new NotImplementedException();
		}
	}
}
