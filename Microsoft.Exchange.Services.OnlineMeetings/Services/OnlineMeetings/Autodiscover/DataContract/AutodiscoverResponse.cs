using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Services.OnlineMeetings.ResourceContract;

namespace Microsoft.Exchange.Services.OnlineMeetings.Autodiscover.DataContract
{
	// Token: 0x02000034 RID: 52
	internal class AutodiscoverResponse
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000077A8 File Offset: 0x000059A8
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000077B0 File Offset: 0x000059B0
		public AccessLocation AccessLocation { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000077B9 File Offset: 0x000059B9
		public ICollection<Link> UserLinks
		{
			get
			{
				return new LinksCollection(this.userLinks);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000077C6 File Offset: 0x000059C6
		public ICollection<Link> RootLinks
		{
			get
			{
				return new LinksCollection(this.rootLinks);
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000077D4 File Offset: 0x000059D4
		public static AutodiscoverResponse FromDictionary(Dictionary<string, object> dictionary)
		{
			dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
			AutodiscoverResponse autodiscoverResponse = new AutodiscoverResponse();
			if (dictionary.ContainsKey("AccessLocation"))
			{
				AccessLocation accessLocation = AccessLocation.Internal;
				Enum.TryParse<AccessLocation>(dictionary["AccessLocation"].ToString(), out accessLocation);
				autodiscoverResponse.AccessLocation = accessLocation;
			}
			AutodiscoverResponse.ExtractLinks(dictionary, autodiscoverResponse.rootLinks, "root");
			AutodiscoverResponse.ExtractLinks(dictionary, autodiscoverResponse.userLinks, "user");
			return autodiscoverResponse;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007848 File Offset: 0x00005A48
		private static void ExtractLinks(Dictionary<string, object> dictionary, List<Link> linksCollection, string keyToUse)
		{
			if (dictionary.ContainsKey(keyToUse))
			{
				IDictionary dictionary2 = dictionary[keyToUse] as IDictionary;
				if (dictionary2 != null)
				{
					IEnumerable enumerable = dictionary2["Links"] as IEnumerable;
					IDictionary dictionary3 = enumerable as IDictionary;
					if (dictionary3 != null)
					{
						enumerable = new IDictionary[]
						{
							dictionary3
						};
					}
					foreach (object obj in enumerable)
					{
						IDictionary dictionary4 = (IDictionary)obj;
						string href = dictionary4["href"] as string;
						string token = dictionary4["token"] as string;
						Link item = new Link(token, href, string.Empty);
						linksCollection.Add(item);
					}
				}
			}
		}

		// Token: 0x0400015C RID: 348
		private const string AccessLocationString = "AccessLocation";

		// Token: 0x0400015D RID: 349
		private readonly List<Link> userLinks = new List<Link>();

		// Token: 0x0400015E RID: 350
		private readonly List<Link> rootLinks = new List<Link>();
	}
}
