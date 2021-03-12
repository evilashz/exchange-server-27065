using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C1 RID: 1217
	internal class ContentFilterPhraseDataProvider : IConfigDataProvider
	{
		// Token: 0x06003750 RID: 14160 RVA: 0x000D83C2 File Offset: 0x000D65C2
		public ContentFilterPhraseDataProvider() : this(DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 36, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\MessageHygieneContentFilterPhraseDataProvider.cs"))
		{
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000D83E7 File Offset: 0x000D65E7
		public ContentFilterPhraseDataProvider(IConfigurationSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			this.session = session;
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06003752 RID: 14162 RVA: 0x000D8404 File Offset: 0x000D6604
		public string Source
		{
			get
			{
				if (this.session == null)
				{
					return string.Empty;
				}
				return this.session.Source;
			}
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000D8420 File Offset: 0x000D6620
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			ContentFilterConfig contentFilterConfig = this.session.FindSingletonConfigurationObject<ContentFilterConfig>();
			if (identity != null && contentFilterConfig != null)
			{
				string @string = Encoding.Unicode.GetString(identity.GetBytes());
				foreach (ContentFilterPhrase contentFilterPhrase in contentFilterConfig.GetPhrases())
				{
					if (string.Equals(contentFilterPhrase.Phrase, @string, StringComparison.OrdinalIgnoreCase))
					{
						return contentFilterPhrase;
					}
				}
			}
			return null;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000D84A8 File Offset: 0x000D66A8
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			return (IEnumerable<T>)this.Find(filter, rootId, deepSearch, sortBy);
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000D84BA File Offset: 0x000D66BA
		public IConfigurable[] Find<T>(QueryFilter queryFilter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return this.Find(queryFilter, rootId, deepSearch, sortBy).ToArray();
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000D84CC File Offset: 0x000D66CC
		private List<ContentFilterPhrase> Find(QueryFilter queryFilter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			List<ContentFilterPhrase> list = new List<ContentFilterPhrase>();
			ContentFilterPhraseQueryFilter contentFilterPhraseQueryFilter = queryFilter as ContentFilterPhraseQueryFilter;
			ContentFilterConfig contentFilterConfig = this.session.FindSingletonConfigurationObject<ContentFilterConfig>();
			if (contentFilterConfig != null)
			{
				foreach (ContentFilterPhrase contentFilterPhrase in contentFilterConfig.GetPhrases())
				{
					if (contentFilterPhraseQueryFilter != null)
					{
						if (string.Equals(contentFilterPhrase.Phrase, contentFilterPhraseQueryFilter.Phrase, StringComparison.OrdinalIgnoreCase))
						{
							list.Add(contentFilterPhrase);
							break;
						}
					}
					else
					{
						list.Add(contentFilterPhrase);
					}
				}
			}
			return list;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000D8560 File Offset: 0x000D6760
		public void Save(IConfigurable instance)
		{
			ContentFilterPhrase contentFilterPhrase = instance as ContentFilterPhrase;
			ContentFilterConfig contentFilterConfig = this.session.FindSingletonConfigurationObject<ContentFilterConfig>();
			if (contentFilterPhrase != null && contentFilterConfig != null)
			{
				contentFilterConfig.AddPhrase(contentFilterPhrase);
				this.session.Save(contentFilterConfig);
			}
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000D859C File Offset: 0x000D679C
		public void Delete(IConfigurable instance)
		{
			ContentFilterPhrase contentFilterPhrase = instance as ContentFilterPhrase;
			ContentFilterConfig contentFilterConfig = this.session.FindSingletonConfigurationObject<ContentFilterConfig>();
			if (contentFilterPhrase != null && contentFilterConfig != null)
			{
				contentFilterConfig.RemovePhrase(contentFilterPhrase);
				this.session.Save(contentFilterConfig);
			}
		}

		// Token: 0x04002569 RID: 9577
		private IConfigurationSession session;
	}
}
