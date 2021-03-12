using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.MessageSecurity.MessageClassifications
{
	// Token: 0x0200000C RID: 12
	internal class ClassificationConfig
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002C59 File Offset: 0x00000E59
		public ClassificationConfig()
		{
			ClassificationConfig.messageClassificationCache = new TenantConfigurationCache<ClassificationConfig.MessageClassificationPerTenantSettings>(ClassificationConfig.cacheSizeInBytes, ClassificationConfig.cacheExpiry, ClassificationConfig.cacheCleanup, null, null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C7C File Offset: 0x00000E7C
		public List<ClassificationSummary> GetClassifications(OrganizationId organizationId, CultureInfo locale)
		{
			ClassificationConfig.MessageClassificationPerTenantSettings messageClassificationPerTenantSettings;
			if (organizationId != null && ClassificationConfig.messageClassificationCache.TryGetValue(organizationId, out messageClassificationPerTenantSettings))
			{
				List<ClassificationSummary> list = new List<ClassificationSummary>(messageClassificationPerTenantSettings.Classifications.Count);
				foreach (ClassificationSummary classificationSummary in messageClassificationPerTenantSettings.Classifications)
				{
					if (string.IsNullOrEmpty(classificationSummary.Locale))
					{
						ClassificationSummary classification = this.GetClassification(organizationId, classificationSummary.ClassificationID, locale);
						if (classification != null)
						{
							list.Add(classification);
						}
					}
				}
				return list;
			}
			return ClassificationConfig.EmptyClassificationList;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D20 File Offset: 0x00000F20
		public ClassificationSummary GetClassification(OrganizationId organizationId, Guid messageClassificationGuid, CultureInfo locale)
		{
			ClassificationConfig.MessageClassificationPerTenantSettings messageClassificationPerTenantSettings;
			ClassificationSummary result;
			if (organizationId != null && ClassificationConfig.messageClassificationCache.TryGetValue(organizationId, out messageClassificationPerTenantSettings) && messageClassificationPerTenantSettings.TryGetClassification(messageClassificationGuid, locale, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D54 File Offset: 0x00000F54
		public ClassificationSummary Summarize(OrganizationId organizationId, List<string> messageClassificationIds, CultureInfo locale)
		{
			if (messageClassificationIds == null || messageClassificationIds.Count == 0)
			{
				return ClassificationSummary.Empty;
			}
			ClassificationConfig.MessageClassificationPerTenantSettings messageClassificationPerTenantSettings;
			if (organizationId == null || !ClassificationConfig.messageClassificationCache.TryGetValue(organizationId, out messageClassificationPerTenantSettings))
			{
				return ClassificationSummary.Invalid;
			}
			List<ClassificationSummary> list = new List<ClassificationSummary>(messageClassificationIds.Count);
			foreach (string g in messageClassificationIds)
			{
				Guid empty = Guid.Empty;
				ClassificationSummary item;
				if (!GuidHelper.TryParseGuid(g, out empty) || !messageClassificationPerTenantSettings.TryGetClassification(empty, locale, out item))
				{
					return ClassificationSummary.Invalid;
				}
				list.Add(item);
			}
			list.Sort(ClassificationConfig.classificationSummaryComparer);
			string recipientDescription = ClassificationConfig.ConcatDescriptions(list);
			return new ClassificationSummary(list[0])
			{
				RecipientDescription = recipientDescription
			};
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E34 File Offset: 0x00001034
		private static string ConcatDescriptions(IEnumerable<ClassificationSummary> sortedClassificationSummaries)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Guid a = Guid.Empty;
			foreach (ClassificationSummary classificationSummary in sortedClassificationSummaries)
			{
				if (a != classificationSummary.ClassificationID && !string.IsNullOrEmpty(classificationSummary.RecipientDescription))
				{
					a = classificationSummary.ClassificationID;
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(classificationSummary.RecipientDescription);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400003C RID: 60
		private static readonly List<ClassificationSummary> EmptyClassificationList = new List<ClassificationSummary>(0);

		// Token: 0x0400003D RID: 61
		private static readonly long cacheSizeInBytes = (long)ByteQuantifiedSize.FromMB(1UL).ToBytes();

		// Token: 0x0400003E RID: 62
		private static readonly TimeSpan cacheExpiry = TimeSpan.FromHours(8.0);

		// Token: 0x0400003F RID: 63
		private static readonly TimeSpan cacheCleanup = TimeSpan.FromHours(8.0);

		// Token: 0x04000040 RID: 64
		private static readonly ClassificationConfig.ClassificationSummaryComparer classificationSummaryComparer = new ClassificationConfig.ClassificationSummaryComparer();

		// Token: 0x04000041 RID: 65
		private static TenantConfigurationCache<ClassificationConfig.MessageClassificationPerTenantSettings> messageClassificationCache;

		// Token: 0x0200000D RID: 13
		private class MessageClassificationPerTenantSettings : TenantConfigurationCacheableItem<MessageClassification>
		{
			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000038 RID: 56 RVA: 0x00002F28 File Offset: 0x00001128
			public override long ItemSize
			{
				get
				{
					return (long)this.itemSize;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000039 RID: 57 RVA: 0x00002F31 File Offset: 0x00001131
			public List<ClassificationSummary> Classifications
			{
				get
				{
					return this.messageClassificationSummaries.Values.ToList<ClassificationSummary>();
				}
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00002F44 File Offset: 0x00001144
			public bool TryGetClassification(Guid messageClassificationId, CultureInfo locale, out ClassificationSummary classification)
			{
				CultureInfo cultureInfo = locale;
				while (cultureInfo != null && !cultureInfo.Equals(CultureInfo.InvariantCulture))
				{
					if (this.messageClassificationSummaries.TryGetValue(cultureInfo.ToString() + messageClassificationId.ToString(), out classification))
					{
						return true;
					}
					cultureInfo = cultureInfo.Parent;
				}
				if (this.messageClassificationSummaries.TryGetValue(messageClassificationId.ToString(), out classification))
				{
					return true;
				}
				classification = null;
				return false;
			}

			// Token: 0x0600003B RID: 59 RVA: 0x00002FB8 File Offset: 0x000011B8
			public override void ReadData(IConfigurationSession session)
			{
				if (SharedConfiguration.IsDehydratedConfiguration(session))
				{
					session = SharedConfiguration.CreateScopedToSharedConfigADSession(session.SessionSettings.CurrentOrganizationId);
				}
				QueryFilter filter = new NotFilter(new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ClassificationSchema.ClassificationID, ClassificationConfig.MessageClassificationPerTenantSettings.InternetConfidentialGuid),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "ExInternetConfidential")
				}));
				MessageClassification[] array = session.Find<MessageClassification>(null, QueryScope.SubTree, filter, null, 0);
				if (array == null || array.Length == 0)
				{
					return;
				}
				Dictionary<string, ClassificationSummary> dictionary = new Dictionary<string, ClassificationSummary>(array.Length, StringComparer.OrdinalIgnoreCase);
				CultureInfo[] installedLanguagePackCultures = LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.Client);
				foreach (MessageClassification classification in array)
				{
					if (installedLanguagePackCultures != null)
					{
						foreach (CultureInfo locale in installedLanguagePackCultures)
						{
							ClassificationSummary classificationSummary = SystemClassificationSummary.GetClassificationSummary(classification, locale);
							if (classificationSummary != null)
							{
								dictionary[classificationSummary.Locale + classificationSummary.ClassificationID.ToString()] = classificationSummary;
							}
						}
					}
				}
				foreach (MessageClassification messageClassification in array)
				{
					ClassificationSummary classificationSummary2 = new ClassificationSummary(messageClassification);
					dictionary[classificationSummary2.Locale + classificationSummary2.ClassificationID.ToString()] = classificationSummary2;
				}
				this.itemSize = 4;
				foreach (KeyValuePair<string, ClassificationSummary> keyValuePair in dictionary)
				{
					this.itemSize += keyValuePair.Key.Length * 2 + keyValuePair.Value.Size;
				}
				Interlocked.Exchange<Dictionary<string, ClassificationSummary>>(ref this.messageClassificationSummaries, dictionary);
			}

			// Token: 0x04000042 RID: 66
			private static readonly Guid InternetConfidentialGuid = new Guid("103a41b0-6d8d-4be5-a866-da3c25d3d679");

			// Token: 0x04000043 RID: 67
			private int itemSize;

			// Token: 0x04000044 RID: 68
			private Dictionary<string, ClassificationSummary> messageClassificationSummaries = new Dictionary<string, ClassificationSummary>();
		}

		// Token: 0x0200000E RID: 14
		private class ClassificationSummaryComparer : IComparer<ClassificationSummary>
		{
			// Token: 0x0600003E RID: 62 RVA: 0x000031B0 File Offset: 0x000013B0
			public int Compare(ClassificationSummary x, ClassificationSummary y)
			{
				if (x.DisplayPrecedence < y.DisplayPrecedence)
				{
					return -1;
				}
				if (x.DisplayPrecedence > y.DisplayPrecedence)
				{
					return 1;
				}
				return x.DisplayName.CompareTo(y.DisplayName);
			}
		}
	}
}
