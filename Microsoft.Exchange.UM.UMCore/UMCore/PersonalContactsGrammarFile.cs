using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000189 RID: 393
	internal abstract class PersonalContactsGrammarFile : SearchGrammarFile
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x000323ED File Offset: 0x000305ED
		internal PersonalContactsGrammarFile(UMMailboxRecipient caller, CultureInfo culture) : base(culture)
		{
			this.caller = caller;
			this.GenerateCompiledGrammar();
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0003240E File Offset: 0x0003060E
		public override Uri BaseUri
		{
			get
			{
				return new Uri(Utils.GrammarPathFromCulture(base.Culture));
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B9B RID: 2971
		internal abstract string ContactsRulePrefix { get; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B9C RID: 2972
		internal abstract bool ShouldGenerateEmptyGrammar { get; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00032420 File Offset: 0x00030620
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x00032428 File Offset: 0x00030628
		internal bool MaxEntriesExceeded { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00032431 File Offset: 0x00030631
		internal override string FilePath
		{
			get
			{
				return this.grammarFile.FilePath;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0003243E File Offset: 0x0003063E
		internal override bool HasEntries
		{
			get
			{
				return this.hasEntries;
			}
		}

		// Token: 0x06000BA1 RID: 2977
		protected abstract List<ContactSearchItem> GetContactsList();

		// Token: 0x06000BA2 RID: 2978
		protected abstract void AppendContactNode(XmlWriter namesGrammarWriter, ContactSearchItem contact, string entryName, string entryDisplayName);

		// Token: 0x06000BA3 RID: 2979
		protected abstract void GenerateContactsCompiledGrammar(List<ContactSearchItem> list);

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00032448 File Offset: 0x00030648
		protected List<ContactSearchItem> GetContactsList(PersonalContactsGrammarFile.AddSearchItemDelegate addSearchItem)
		{
			List<ContactSearchItem> list = new List<ContactSearchItem>();
			int num = Utils.RunningInTestMode ? 500 : Constants.DirectorySearch.MaxPersonalContacts;
			IDictionary<PropertyDefinition, object> dictionary = new SortedDictionary<PropertyDefinition, object>();
			dictionary.Add(StoreObjectSchema.ItemClass, "IPM.Contact");
			addSearchItem(this.caller, dictionary, list, num + 1);
			CallIdTracer.TraceDebug(ExTraceGlobals.AsrContactsTracer, this, "contacts max limit = {0}, number of entries after retrieving personal contacts = {1}", new object[]
			{
				num,
				list.Count
			});
			IDictionary<PropertyDefinition, object> dictionary2 = new SortedDictionary<PropertyDefinition, object>();
			dictionary2.Add(StoreObjectSchema.ItemClass, "IPM.DistList");
			addSearchItem(this.caller, dictionary2, list, num + 1);
			this.MaxEntriesExceeded = (list.Count > num);
			CallIdTracer.TraceDebug(ExTraceGlobals.AsrContactsTracer, this, "contacts max limit = {0}, number of entries after retrieving groups = {1}, maxEntriesExceeded = {2}", new object[]
			{
				num,
				list.Count,
				this.MaxEntriesExceeded
			});
			return list;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00032544 File Offset: 0x00030744
		protected virtual void GenerateGrammar(List<ContactSearchItem> list)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(this.FilePath))
			{
				xmlWriter.WriteRaw(string.Format(CultureInfo.InvariantCulture, "\r\n<grammar root=\"Names\" xml:lang=\"{0}\" version=\"1.0\" xmlns=\"http://www.w3.org/2001/06/grammar\" xmlns:sapi=\"http://schemas.microsoft.com/Speech/2002/06/SRGSExtensions\" tag-format=\"semantics-ms/1.0\">", new object[]
				{
					UmCultures.GetGrxmlCulture(base.Culture)
				}));
				Utils.CopyPeopleGrammarRules(xmlWriter, base.Culture);
				this.GenerateNamesGrammar(xmlWriter, list);
				xmlWriter.WriteRaw("\r\n</grammar>");
			}
			if (this.HasEntries || this.ShouldGenerateEmptyGrammar)
			{
				using (ITempFile tempFile = TempFileFactory.CreateTempCompiledGrammarFile())
				{
					Platform.Utilities.CompileGrammar(this.FilePath, tempFile.FilePath, base.Culture);
					File.Copy(tempFile.FilePath, this.FilePath, true);
				}
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00032620 File Offset: 0x00030820
		protected void GenerateNamesGrammar(XmlWriter namesGrammarWriter, List<ContactSearchItem> list)
		{
			ExclusionList instance = ExclusionList.Instance;
			foreach (ContactSearchItem contactSearchItem in list)
			{
				if (string.IsNullOrEmpty(contactSearchItem.FullName))
				{
					CallIdTracer.TraceError(ExTraceGlobals.AsrContactsTracer, this, "Found a contact which has no full name. Disregarding this contact and continuing...", new object[0]);
				}
				else
				{
					PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, contactSearchItem.FullName);
					try
					{
						if (instance != null)
						{
							List<Replacement> list2 = null;
							switch (instance.GetReplacementStrings(contactSearchItem.FullName, RecipientType.UserMailbox, out list2))
							{
							case MatchResult.NoMatch:
								this.WriteEntry(namesGrammarWriter, contactSearchItem.FullName, contactSearchItem);
								continue;
							case MatchResult.MatchWithReplacements:
								using (List<Replacement>.Enumerator enumerator2 = list2.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										Replacement replacement = enumerator2.Current;
										this.WriteEntry(namesGrammarWriter, replacement.ReplacementString, contactSearchItem);
									}
									continue;
								}
								break;
							case MatchResult.MatchWithNoReplacements:
							case MatchResult.NotFound:
								break;
							default:
								continue;
							}
							CallIdTracer.TraceDebug(ExTraceGlobals.AsrContactsTracer, this, data, "Recipient '_UserDisplayName' will be discarded because no replacement strings were specified in the filter.", new object[0]);
						}
						else
						{
							this.WriteEntry(namesGrammarWriter, contactSearchItem.FullName, contactSearchItem);
						}
					}
					catch (FormatException ex)
					{
						CallIdTracer.TraceError(ExTraceGlobals.AsrContactsTracer, this, data, "An error was encountered while writing personal contact entry '_UserDisplayName' in grammar file. The error was '{0}'. Continuing...", new object[]
						{
							ex.Message
						});
					}
				}
			}
			if (!this.hasEntries && this.ShouldGenerateEmptyGrammar)
			{
				namesGrammarWriter.WriteRaw("\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<ruleref special=\"VOID\" />\r\n\t</rule>");
			}
			if (this.hasEntries)
			{
				namesGrammarWriter.WriteRaw("\t\t</one-of>\r\n");
				namesGrammarWriter.WriteRaw(string.Format(CultureInfo.InvariantCulture, "\r\n\t\t<!-- the following will add an option politeending to the recognition -->\r\n\t\t<item repeat=\"0-1\">\r\n\t\t\t<ruleref uri=\"{0}#politeEndPhrases\"/>\r\n\t\t</item>\r\n\t</rule>", new object[]
				{
					Utils.GetCommonGrammarFilePath(base.Culture)
				}));
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0003281C File Offset: 0x00030A1C
		protected void DisposeGrammarFile()
		{
			this.grammarFile.Dispose();
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0003282C File Offset: 0x00030A2C
		protected static string SrgsSanitizeString(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			foreach (char c in input)
			{
				if (char.IsLetterOrDigit(c))
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append(" ");
				}
			}
			string text = SpeechUtils.SrgsEncode(stringBuilder.ToString());
			Platform.Utilities.CheckGrammarEntryFormat(text);
			return text;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000328A4 File Offset: 0x00030AA4
		protected virtual void WriteNamesRulePrefix(XmlWriter namesGrammarWriter)
		{
			namesGrammarWriter.WriteRaw(string.Format(CultureInfo.InvariantCulture, this.ContactsRulePrefix, new object[]
			{
				this.caller.ADRecipient.Guid.ToString()
			}));
			namesGrammarWriter.WriteRaw("\t\t<one-of>\r\n");
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000328FC File Offset: 0x00030AFC
		private void WriteEntry(XmlWriter namesGrammarWriter, string contactName, ContactSearchItem contact)
		{
			if (!this.hasEntries)
			{
				this.WriteNamesRulePrefix(namesGrammarWriter);
			}
			this.hasEntries = true;
			string text = string.IsNullOrEmpty(contact.Email1DisplayName) ? contact.FullName : contact.Email1DisplayName;
			string text2 = PersonalContactsGrammarFile.SrgsSanitizeString(contactName);
			string text3 = PersonalContactsGrammarFile.SrgsSanitizeString(text);
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, contact.FullName);
			CallIdTracer.TraceDebug(ExTraceGlobals.AsrContactsTracer, this, data, "Mapped Contact Name '_UserDisplayName' to '{0}'", new object[]
			{
				text2
			});
			PIIMessage[] data2 = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._UserDisplayName, text),
				PIIMessage.Create(PIIType._UserDisplayName, text3)
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.AsrContactsTracer, this, data2, "Mapped Contact DisplayName '_UserDisplayName1' to '_UserDisplayName2'", new object[0]);
			this.AppendContactNode(namesGrammarWriter, contact, text2, text3);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000329BC File Offset: 0x00030BBC
		private void GenerateCompiledGrammar()
		{
			if (!this.caller.HasContactsFolder)
			{
				PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, this.caller.DisplayName);
				CallIdTracer.TraceError(ExTraceGlobals.AsrContactsTracer, this, data, "PersonalContactsGrammarFile.GenerateGrammar::User _UserDisplayName does not have a contacts folder.", new object[0]);
				return;
			}
			ExDateTime utcNow = ExDateTime.UtcNow;
			List<ContactSearchItem> contactsList = this.GetContactsList();
			TimeSpan timeSpan = ExDateTime.UtcNow - utcNow;
			utcNow = ExDateTime.UtcNow;
			this.GenerateContactsCompiledGrammar(contactsList);
			TimeSpan timeSpan2 = ExDateTime.UtcNow - utcNow;
			CallIdTracer.TraceError(ExTraceGlobals.AsrContactsTracer, this, "list.Count = {0} GetContactsList took {1} milliseconds. GenerateGrammar took {2} milliseconds", new object[]
			{
				contactsList.Count,
				timeSpan.TotalMilliseconds,
				timeSpan2.TotalMilliseconds
			});
		}

		// Token: 0x040009DA RID: 2522
		private ITempFile grammarFile = TempFileFactory.CreateTempCompiledGrammarFile();

		// Token: 0x040009DB RID: 2523
		private UMMailboxRecipient caller;

		// Token: 0x040009DC RID: 2524
		private bool hasEntries;

		// Token: 0x0200018A RID: 394
		// (Invoke) Token: 0x06000BAD RID: 2989
		protected delegate void AddSearchItemDelegate(UMMailboxRecipient caller, IDictionary<PropertyDefinition, object> searchContactFilter, List<ContactSearchItem> list, int maxPersonalContacts);
	}
}
