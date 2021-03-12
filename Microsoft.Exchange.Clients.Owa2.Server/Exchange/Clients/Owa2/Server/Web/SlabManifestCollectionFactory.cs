using System;
using System.Collections.Concurrent;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200049C RID: 1180
	public class SlabManifestCollectionFactory
	{
		// Token: 0x06002849 RID: 10313 RVA: 0x00094A74 File Offset: 0x00092C74
		public static SlabManifestCollection GetInstance(string owaVersion)
		{
			if (!SlabManifestCollectionFactory.slabManifestCollections.ContainsKey(owaVersion))
			{
				SlabManifestCollection value = SlabManifestCollection.Create(owaVersion);
				SlabManifestCollectionFactory.slabManifestCollections.TryAdd(owaVersion, value);
			}
			return SlabManifestCollectionFactory.slabManifestCollections[owaVersion];
		}

		// Token: 0x0400175E RID: 5982
		private static ConcurrentDictionary<string, SlabManifestCollection> slabManifestCollections = new ConcurrentDictionary<string, SlabManifestCollection>();
	}
}
