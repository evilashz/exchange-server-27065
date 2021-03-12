using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000563 RID: 1379
	internal class RemoteDomainMap : RemoteDomainCollection
	{
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000EBAF9 File Offset: 0x000E9CF9
		public RemoteDomainMap(IList<RemoteDomainEntry> remoteDomainEntryList)
		{
			this.map = new DomainMatchMap<RemoteDomainEntry>(remoteDomainEntryList ?? ((IList<RemoteDomainEntry>)RemoteDomainMap.EmptyRemoteDomainEntryList));
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x000EBB1B File Offset: 0x000E9D1B
		public static RemoteDomainMap Empty
		{
			get
			{
				return RemoteDomainMap.emptyRemoteDomainMap;
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06003DFB RID: 15867 RVA: 0x000EBB22 File Offset: 0x000E9D22
		public RemoteDomainEntry Star
		{
			get
			{
				return this.map.Star;
			}
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000EBB2F File Offset: 0x000E9D2F
		public RemoteDomainEntry GetDomainEntry(SmtpDomain domain)
		{
			return this.map.GetBestMatch(domain);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000EBB3D File Offset: 0x000E9D3D
		public override RemoteDomain Find(string domainName)
		{
			return this.map.GetBestMatch(domainName);
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000EBB4B File Offset: 0x000E9D4B
		public override IEnumerator<RemoteDomain> GetEnumerator()
		{
			return this.map.GetAllDomains<RemoteDomain>();
		}

		// Token: 0x04002A08 RID: 10760
		private static readonly RemoteDomainEntry[] EmptyRemoteDomainEntryList = new RemoteDomainEntry[0];

		// Token: 0x04002A09 RID: 10761
		private static readonly RemoteDomainMap emptyRemoteDomainMap = new RemoteDomainMap(null);

		// Token: 0x04002A0A RID: 10762
		private readonly DomainMatchMap<RemoteDomainEntry> map;
	}
}
