using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.ConfigurationCache
{
	// Token: 0x0200001F RID: 31
	internal abstract class SimpleConfigCache<ConfigClass, SourceObject> : IConfigCache where ConfigClass : ADConfigurationObject, new()
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00006C3C File Offset: 0x00004E3C
		internal static Dictionary<string, ConfigClass> CacheFactory()
		{
			return new Dictionary<string, ConfigClass>(StringComparer.CurrentCultureIgnoreCase);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006C48 File Offset: 0x00004E48
		internal ConfigClass GetConfigFromSourceObject(SourceObject src)
		{
			ConfigClass result = default(ConfigClass);
			string text = this.KeyFromSourceObject(src);
			if (text != null)
			{
				this.cache.TryGetValue(text, out result);
			}
			return result;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006C78 File Offset: 0x00004E78
		internal virtual IEnumerable<ConfigClass> StartSearch(IConfigurationSession session)
		{
			return session.FindAllPaged<ConfigClass>();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006C80 File Offset: 0x00004E80
		public virtual void Refresh(IConfigurationSession session)
		{
			Dictionary<string, ConfigClass> dictionary = SimpleConfigCache<ConfigClass, SourceObject>.CacheFactory();
			int num = 0;
			IEnumerable<ConfigClass> enumerable = this.StartSearch(session);
			foreach (ConfigClass configClass in enumerable)
			{
				foreach (string key in this.KeysFromConfig(configClass))
				{
					ConfigClass configClass2 = default(ConfigClass);
					this.cache.TryGetValue(key, out configClass2);
					if (configClass2 != null && configClass2.WhenChanged == configClass.WhenChanged)
					{
						dictionary[key] = configClass2;
					}
					else
					{
						dictionary[key] = configClass;
						num++;
					}
				}
			}
			if (num > 0 || dictionary.Count != this.cache.Count)
			{
				this.cache = dictionary;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006DB0 File Offset: 0x00004FB0
		protected virtual string[] KeysFromConfig(ConfigClass config)
		{
			if (config != null && config.Id != null)
			{
				return new string[]
				{
					config.Id.ToString()
				};
			}
			return new string[0];
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006DF8 File Offset: 0x00004FF8
		protected virtual string KeyFromSourceObject(SourceObject src)
		{
			if (src == null)
			{
				return null;
			}
			return src.ToString();
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006E11 File Offset: 0x00005011
		protected Dictionary<string, ConfigClass> Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x0400013A RID: 314
		protected Dictionary<string, ConfigClass> cache = SimpleConfigCache<ConfigClass, SourceObject>.CacheFactory();
	}
}
