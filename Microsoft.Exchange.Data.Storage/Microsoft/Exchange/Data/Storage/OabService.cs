using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D54 RID: 3412
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OabService : HttpService
	{
		// Token: 0x06007630 RID: 30256 RVA: 0x0020A1EA File Offset: 0x002083EA
		private OabService(TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, MiniVirtualDirectory virtualDirectory, HashSet<string> linkedOfflineAddressBookDistinguishedNames) : base(serverInfo, ServiceType.OfflineAddressBook, url, clientAccessType, authenticationMethod, virtualDirectory)
		{
			this.LinkedOfflineAddressBookDistinguishedNames = linkedOfflineAddressBookDistinguishedNames;
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x06007631 RID: 30257 RVA: 0x0020A202 File Offset: 0x00208402
		// (set) Token: 0x06007632 RID: 30258 RVA: 0x0020A20A File Offset: 0x0020840A
		public HashSet<string> LinkedOfflineAddressBookDistinguishedNames { get; private set; }

		// Token: 0x06007633 RID: 30259 RVA: 0x0020A214 File Offset: 0x00208414
		internal static bool TryCreateOabService(MiniVirtualDirectory virtualDirectory, TopologyServerInfo serverInfo, Uri url, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			if (virtualDirectory.IsOab)
			{
				MultiValuedProperty<ADObjectId> offlineAddressBooks = virtualDirectory.OfflineAddressBooks;
				HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
				for (int i = 0; i < offlineAddressBooks.Count; i++)
				{
					hashSet.Add(offlineAddressBooks[i].DistinguishedName);
				}
				service = new OabService(serverInfo, url, clientAccessType, authenticationMethod, virtualDirectory, hashSet);
				return true;
			}
			service = null;
			return false;
		}
	}
}
