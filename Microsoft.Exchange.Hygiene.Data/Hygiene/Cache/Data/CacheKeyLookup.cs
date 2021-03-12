using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	internal class CacheKeyLookup : ConfigurablePropertyBag
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00009F8D File Offset: 0x0000818D
		public CacheKeyLookup()
		{
			this.ReferenceKeys = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00009FA5 File Offset: 0x000081A5
		public CacheKeyLookup(string entityName, string primaryKey)
		{
			this.EntityName = entityName;
			this.PrimaryKey = primaryKey;
			this.ReferenceKeys = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00009FCB File Offset: 0x000081CB
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.CacheIdentity);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00009FD8 File Offset: 0x000081D8
		public string CacheIdentity
		{
			get
			{
				return CacheKeyLookup.ConstructCacheIdentity(this.EntityName, this.PrimaryKey);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00009FEB File Offset: 0x000081EB
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00009FFD File Offset: 0x000081FD
		public string EntityName
		{
			get
			{
				return this[CacheKeyLookup.EntityNameProp] as string;
			}
			set
			{
				this[CacheKeyLookup.EntityNameProp] = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000A00B File Offset: 0x0000820B
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000A01D File Offset: 0x0000821D
		public string PrimaryKey
		{
			get
			{
				return this[CacheKeyLookup.PrimaryKeyProp] as string;
			}
			set
			{
				this[CacheKeyLookup.PrimaryKeyProp] = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000A02B File Offset: 0x0000822B
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000A03D File Offset: 0x0000823D
		public HashSet<string> ReferenceKeys
		{
			get
			{
				return this[CacheKeyLookup.ReferenceKeysProp] as HashSet<string>;
			}
			set
			{
				this[CacheKeyLookup.ReferenceKeysProp] = value;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A04B File Offset: 0x0000824B
		public static string ConstructCacheIdentity(string entityName, string primaryKey)
		{
			return string.Format("{0}:{1}", entityName, primaryKey);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A059 File Offset: 0x00008259
		internal bool IsReferenceKeyInLookup(string referenceKey)
		{
			return this.ReferenceKeys.Contains(referenceKey);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A067 File Offset: 0x00008267
		internal void AddReferenceKeysToLookup(IEnumerable<string> referenceKeys)
		{
			this.ReferenceKeys.UnionWith(referenceKeys);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A075 File Offset: 0x00008275
		internal void RemoveReferenceKeysFromLookup(IEnumerable<string> referenceKeys)
		{
			this.ReferenceKeys.ExceptWith(referenceKeys);
		}

		// Token: 0x0400020F RID: 527
		private static readonly HygienePropertyDefinition EntityNameProp = CacheObjectSchema.EntityNameProp;

		// Token: 0x04000210 RID: 528
		private static readonly HygienePropertyDefinition PrimaryKeyProp = new HygienePropertyDefinition("PrimaryKey", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000211 RID: 529
		private static readonly HygienePropertyDefinition ReferenceKeysProp = new HygienePropertyDefinition("ReferenceKeys", typeof(ISet<string>));
	}
}
