using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000511 RID: 1297
	[Serializable]
	public class NtdsService : ADNonExchangeObject
	{
		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000DD13A File Offset: 0x000DB33A
		internal override ADObjectSchema Schema
		{
			get
			{
				return NtdsService.schema;
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x000DD141 File Offset: 0x000DB341
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "nTDSService";
			}
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000DD148 File Offset: 0x000DB348
		internal static object DoListObjectGetter(IPropertyBag propertyBag)
		{
			char heuristicsChar = NtdsService.GetHeuristicsChar((string)propertyBag[NtdsServiceSchema.Heuristics], 2, '0');
			return heuristicsChar == '1';
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000DD178 File Offset: 0x000DB378
		private static char GetHeuristicsChar(string heuristics, int position, char defaultChar)
		{
			if (!string.IsNullOrEmpty(heuristics) && position < heuristics.Length)
			{
				return heuristics[position];
			}
			return defaultChar;
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000DD194 File Offset: 0x000DB394
		public bool DoListObject
		{
			get
			{
				return (bool)this[NtdsServiceSchema.DoListObject];
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x000DD1A6 File Offset: 0x000DB3A6
		public int TombstoneLifetime
		{
			get
			{
				return (int)this[NtdsServiceSchema.TombstoneLifetime];
			}
		}

		// Token: 0x04002724 RID: 10020
		private const string MostDerivedClass = "nTDSService";

		// Token: 0x04002725 RID: 10021
		internal static readonly ADObjectId ContainerId = new ADObjectId("CN=Directory Service,CN=Windows NT,CN=Services");

		// Token: 0x04002726 RID: 10022
		private static readonly NtdsServiceSchema schema = ObjectSchema.GetInstance<NtdsServiceSchema>();
	}
}
