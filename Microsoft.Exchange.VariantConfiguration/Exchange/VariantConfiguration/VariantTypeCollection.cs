using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200014B RID: 331
	public class VariantTypeCollection
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x000269D8 File Offset: 0x00024BD8
		private VariantTypeCollection(IEnumerable<VariantType> variants)
		{
			this.variants = new Dictionary<string, VariantType>(StringComparer.OrdinalIgnoreCase);
			foreach (VariantType variantType in variants)
			{
				this.variants.Add(variantType.Name, variantType);
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00026A44 File Offset: 0x00024C44
		public static VariantTypeCollection Create(IEnumerable<VariantType> variants)
		{
			return new VariantTypeCollection(variants);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00026A70 File Offset: 0x00024C70
		public IEnumerable<string> GetNames(bool includeInternal)
		{
			if (includeInternal)
			{
				return this.variants.Keys;
			}
			return from key in this.variants.Keys
			where this.variants[key].Flags.HasFlag(VariantTypeFlags.Public)
			select key;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00026AB0 File Offset: 0x00024CB0
		public bool Contains(string name, bool includeInternal)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (name.Contains('.'))
			{
				name = name.Split(new char[]
				{
					'.'
				})[0];
				return this.variants.ContainsKey(name) && this.variants[name].Flags.HasFlag(VariantTypeFlags.Prefix) && (includeInternal || this.variants[name].Flags.HasFlag(VariantTypeFlags.Public));
			}
			return this.variants.ContainsKey(name) && (includeInternal || this.variants[name].Flags.HasFlag(VariantTypeFlags.Public));
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00026B98 File Offset: 0x00024D98
		public VariantType GetVariantByName(string name, bool includeInternal)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			name = name.Split(new char[]
			{
				'.'
			})[0];
			name = this.GetNames(includeInternal).First((string entry) => name.Equals(entry, StringComparison.InvariantCultureIgnoreCase));
			return this.variants[name];
		}

		// Token: 0x04000532 RID: 1330
		private readonly IDictionary<string, VariantType> variants;
	}
}
