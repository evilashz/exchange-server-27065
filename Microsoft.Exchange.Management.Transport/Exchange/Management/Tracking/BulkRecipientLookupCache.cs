using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x02000096 RID: 150
	internal class BulkRecipientLookupCache
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00014AB5 File Offset: 0x00012CB5
		internal BulkRecipientLookupCache(int capacity)
		{
			this.lookupCache = new Dictionary<string, string>(capacity, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00014ACE File Offset: 0x00012CCE
		private BulkRecipientLookupCache()
		{
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00014AD6 File Offset: 0x00012CD6
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00014ADE File Offset: 0x00012CDE
		internal int AddressesLookedUp { get; private set; }

		// Token: 0x06000556 RID: 1366 RVA: 0x00014AF8 File Offset: 0x00012CF8
		internal IEnumerable<string> Resolve(IEnumerable<string> addresses, IRecipientSession session)
		{
			if (addresses == null)
			{
				return null;
			}
			int num = 0;
			List<ProxyAddress> list = new List<ProxyAddress>();
			foreach (string text in addresses)
			{
				num++;
				if (!this.lookupCache.ContainsKey(text))
				{
					list.Add(ProxyAddress.Parse(text));
					this.lookupCache[text] = null;
				}
			}
			this.AddressesLookedUp = list.Count;
			if (list.Count > 0)
			{
				ProxyAddress[] array = list.ToArray();
				Result<ADRawEntry>[] array2 = session.FindByProxyAddresses(array, BulkRecipientLookupCache.displayNameProperty);
				for (int i = 0; i < array.Length; i++)
				{
					ADRawEntry data = array2[i].Data;
					string addressString = array[i].AddressString;
					string value = null;
					if (data != null)
					{
						value = (data[ADRecipientSchema.DisplayName] as string);
					}
					if (string.IsNullOrEmpty(value))
					{
						ProxyAddress proxyAddress;
						if (SmtpProxyAddress.TryDeencapsulate(array[i].AddressString, out proxyAddress) && !string.IsNullOrEmpty(proxyAddress.AddressString))
						{
							value = proxyAddress.AddressString;
						}
						else
						{
							value = array[i].AddressString;
						}
					}
					this.lookupCache[addressString] = value;
				}
			}
			return from address in addresses
			select this.lookupCache[address];
		}

		// Token: 0x040001C2 RID: 450
		private static PropertyDefinition[] displayNameProperty = new PropertyDefinition[]
		{
			ADRecipientSchema.DisplayName
		};

		// Token: 0x040001C3 RID: 451
		private Dictionary<string, string> lookupCache;
	}
}
