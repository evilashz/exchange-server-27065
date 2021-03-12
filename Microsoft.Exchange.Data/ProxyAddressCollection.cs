using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public sealed class ProxyAddressCollection : ProxyAddressBaseCollection<ProxyAddress>
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000124C4 File Offset: 0x000106C4
		public new static ProxyAddressCollection Empty
		{
			get
			{
				return ProxyAddressCollection.empty;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000124CB File Offset: 0x000106CB
		public ProxyAddressCollection()
		{
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000124D3 File Offset: 0x000106D3
		public ProxyAddressCollection(object value) : base(value)
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000124DC File Offset: 0x000106DC
		public ProxyAddressCollection(ICollection values) : base(values)
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000124E5 File Offset: 0x000106E5
		public ProxyAddressCollection(Dictionary<string, object> table) : base(table)
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000124EE File Offset: 0x000106EE
		internal ProxyAddressCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : base(readOnly, propertyDefinition, values)
		{
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000124F9 File Offset: 0x000106F9
		internal ProxyAddressCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : base(readOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage)
		{
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00012508 File Offset: 0x00010708
		public new static implicit operator ProxyAddressCollection(object[] array)
		{
			return new ProxyAddressCollection(false, null, array);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00012514 File Offset: 0x00010714
		internal string GetSipUri()
		{
			foreach (ProxyAddress proxyAddress in this)
			{
				if (proxyAddress.ProxyAddressString.StartsWith("sip:", StringComparison.OrdinalIgnoreCase))
				{
					return proxyAddress.ProxyAddressString.ToLowerInvariant();
				}
			}
			return null;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00012580 File Offset: 0x00010780
		public ProxyAddressCollection(Hashtable table) : base(table)
		{
		}

		// Token: 0x0400030A RID: 778
		private static ProxyAddressCollection empty = new ProxyAddressCollection(true, null, new object[0]);
	}
}
