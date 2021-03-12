using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000048 RID: 72
	internal class SearchSource
	{
		// Token: 0x0600033E RID: 830 RVA: 0x000154E9 File Offset: 0x000136E9
		public SearchSource()
		{
			this.ExtendedAttributes = new SearchSource.ExtendedAttributeStore();
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600033F RID: 831 RVA: 0x000154FC File Offset: 0x000136FC
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00015504 File Offset: 0x00013704
		public string ReferenceId { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0001550D File Offset: 0x0001370D
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00015515 File Offset: 0x00013715
		public string OriginalReferenceId { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0001551E File Offset: 0x0001371E
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00015526 File Offset: 0x00013726
		public string FolderSpec { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0001552F File Offset: 0x0001372F
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00015537 File Offset: 0x00013737
		public SourceLocation SourceLocation { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00015540 File Offset: 0x00013740
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00015548 File Offset: 0x00013748
		public SourceType SourceType { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00015551 File Offset: 0x00013751
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00015559 File Offset: 0x00013759
		public SearchSource.ExtendedAttributeStore ExtendedAttributes { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00015562 File Offset: 0x00013762
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0001556A File Offset: 0x0001376A
		public SearchRecipient Recipient { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00015573 File Offset: 0x00013773
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0001557B File Offset: 0x0001377B
		public MailboxInfo MailboxInfo { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00015584 File Offset: 0x00013784
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0001558C File Offset: 0x0001378C
		public bool CanBeCrossPremise { get; set; }

		// Token: 0x06000351 RID: 849 RVA: 0x00015598 File Offset: 0x00013798
		public static SourceType GetSourceType(SearchSource source)
		{
			if (string.IsNullOrEmpty(source.ReferenceId))
			{
				return source.SourceType;
			}
			if (source.ReferenceId.StartsWith("/", StringComparison.InvariantCultureIgnoreCase) && (source.ReferenceId.IndexOf("o=", StringComparison.InvariantCultureIgnoreCase) != -1 || source.ReferenceId.IndexOf("ou=", StringComparison.InvariantCultureIgnoreCase) != -1 || source.ReferenceId.IndexOf("cn=", StringComparison.InvariantCultureIgnoreCase) != -1))
			{
				return SourceType.LegacyExchangeDN;
			}
			if (source.ReferenceId.StartsWith("\\", StringComparison.InvariantCultureIgnoreCase))
			{
				return SourceType.PublicFolder;
			}
			return SourceType.Recipient;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00015620 File Offset: 0x00013820
		public virtual object GetProperty(PropertyDefinition propertyDefinition)
		{
			if (this.Recipient != null && this.Recipient.ADEntry != null)
			{
				return this.Recipient.ADEntry[propertyDefinition];
			}
			return this.MailboxInfo[propertyDefinition];
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00015658 File Offset: 0x00013858
		public SearchSource Clone()
		{
			SearchSource searchSource = (SearchSource)base.MemberwiseClone();
			searchSource.ExtendedAttributes = new SearchSource.ExtendedAttributeStore(this.ExtendedAttributes);
			searchSource.MailboxInfo = null;
			return searchSource;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001568C File Offset: 0x0001388C
		public string GetPrimarySmtpAddress()
		{
			string result = null;
			if (this.MailboxInfo != null)
			{
				SmtpAddress primarySmtpAddress = this.MailboxInfo.PrimarySmtpAddress;
				result = this.MailboxInfo.PrimarySmtpAddress.ToString();
			}
			return result;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000156CC File Offset: 0x000138CC
		internal MailboxSearchScope GetScope()
		{
			MailboxSearchScopeType searchScopeType = 0;
			Enum.TryParse<MailboxSearchScopeType>(this.SourceType.ToString(), out searchScopeType);
			MailboxSearchLocation mailboxSearchLocation = 2;
			if (this.SourceLocation == SourceLocation.ArchiveOnly)
			{
				mailboxSearchLocation = 1;
			}
			else if (this.SourceLocation == SourceLocation.PrimaryOnly)
			{
				mailboxSearchLocation = 0;
			}
			MailboxSearchScope mailboxSearchScope = new MailboxSearchScope(this.ReferenceId, mailboxSearchLocation);
			mailboxSearchScope.SearchScopeType = searchScopeType;
			this.TrySaveMailboxInfo();
			foreach (KeyValuePair<string, string> keyValuePair in this.ExtendedAttributes)
			{
				mailboxSearchScope.ExtendedAttributes.Add(new ExtendedAttribute(keyValuePair.Key, keyValuePair.Value));
			}
			return mailboxSearchScope;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00015788 File Offset: 0x00013988
		internal bool TrySaveMailboxInfo()
		{
			Recorder.Trace(4L, TraceType.InfoTrace, new object[]
			{
				"SearchSource.TrySaveMailboxInfo Source:",
				this.ReferenceId,
				"SourceType:",
				this.SourceType,
				"Location:",
				this.SourceLocation,
				"MailboxInfo:",
				this.MailboxInfo
			});
			try
			{
				if (this.MailboxInfo != null)
				{
					int num = 0;
					this.ExtendedAttributes[this.GetMailboxInfoKey(num++)] = SearchHelper.ConvertToString(this.OriginalReferenceId, typeof(string));
					this.ExtendedAttributes[this.GetMailboxInfoKey(num++)] = SearchHelper.ConvertToString(this.FolderSpec, typeof(string));
					this.ExtendedAttributes[this.GetMailboxInfoKey(num++)] = SearchHelper.ConvertToString(this.MailboxInfo.Type, typeof(MailboxType));
					for (int i = 0; i < MailboxInfo.PropertyDefinitionCollection.Length; i++)
					{
						if (this.MailboxInfo.PropertyMap.ContainsKey(MailboxInfo.PropertyDefinitionCollection[i]))
						{
							this.ExtendedAttributes[this.GetMailboxInfoKey(num++)] = SearchHelper.ConvertToString(this.MailboxInfo.PropertyMap[MailboxInfo.PropertyDefinitionCollection[i]], MailboxInfo.PropertyDefinitionCollection[i]);
						}
						else
						{
							this.ExtendedAttributes[this.GetMailboxInfoKey(num++)] = string.Empty;
						}
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				Recorder.Trace(4L, TraceType.WarningTrace, new object[]
				{
					"SearchSource.TrySaveMailboxInfo Failed Source:",
					this.ReferenceId,
					"Error:",
					ex
				});
			}
			return false;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00015974 File Offset: 0x00013B74
		internal bool TryLoadMailboxInfo()
		{
			Recorder.Trace(4L, TraceType.InfoTrace, new object[]
			{
				"SearchSource.TryLoadMailboxInfo Source:",
				this.ReferenceId,
				"SourceType:",
				this.SourceType,
				"Location:",
				this.SourceLocation,
				"ExtendedAttributes:",
				this.ExtendedAttributes
			});
			try
			{
				if (this.MailboxInfo == null)
				{
					int index = 0;
					if (this.ExtendedAttributes.ContainsKey(this.GetMailboxInfoKey(index)))
					{
						this.OriginalReferenceId = (string)SearchHelper.ConvertFromString(this.ExtendedAttributes[this.GetMailboxInfoKey(index++)], typeof(string));
						this.FolderSpec = (string)SearchHelper.ConvertFromString(this.ExtendedAttributes[this.GetMailboxInfoKey(index++)], typeof(string));
						MailboxType type = SearchHelper.ConvertFromString<MailboxType>(this.ExtendedAttributes[this.GetMailboxInfoKey(index++)]);
						Dictionary<PropertyDefinition, object> dictionary = new Dictionary<PropertyDefinition, object>();
						for (int i = 0; i < MailboxInfo.PropertyDefinitionCollection.Length; i++)
						{
							dictionary[MailboxInfo.PropertyDefinitionCollection[i]] = SearchHelper.ConvertFromString(this.ExtendedAttributes[this.GetMailboxInfoKey(index++)], MailboxInfo.PropertyDefinitionCollection[i]);
						}
						this.MailboxInfo = new MailboxInfo(dictionary, type);
						this.MailboxInfo.SourceMailbox = this;
						this.MailboxInfo.Folder = this.FolderSpec;
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Recorder.Trace(4L, TraceType.WarningTrace, new object[]
				{
					"SearchSource.TryLoadMailboxInfo Failed Source:",
					this.ReferenceId,
					"Error:",
					ex
				});
			}
			return false;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00015B5C File Offset: 0x00013D5C
		private string GetMailboxInfoKey(int index)
		{
			return string.Format("{0}{1}", "SerializedMailbox", index);
		}

		// Token: 0x04000179 RID: 377
		public const string SerializedMailboxAttribute = "SerializedMailbox";

		// Token: 0x02000049 RID: 73
		internal class ExtendedAttributeStore : Dictionary<string, string>
		{
			// Token: 0x06000359 RID: 857 RVA: 0x00015B73 File Offset: 0x00013D73
			public ExtendedAttributeStore()
			{
			}

			// Token: 0x0600035A RID: 858 RVA: 0x00015B7B File Offset: 0x00013D7B
			public ExtendedAttributeStore(SearchSource.ExtendedAttributeStore existingStore) : base(existingStore)
			{
			}
		}
	}
}
