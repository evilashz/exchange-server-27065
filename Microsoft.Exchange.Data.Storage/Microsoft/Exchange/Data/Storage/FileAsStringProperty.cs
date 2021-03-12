using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C5E RID: 3166
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class FileAsStringProperty : AtomRuleCompositeProperty
	{
		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x06006F7D RID: 28541 RVA: 0x001E00F2 File Offset: 0x001DE2F2
		private static Dictionary<FileAsMapping, FormattedSentence> FileAsPatterns
		{
			get
			{
				Dictionary<FileAsMapping, FormattedSentence> result;
				if ((result = FileAsStringProperty.fileAsPatterns) == null)
				{
					result = (FileAsStringProperty.fileAsPatterns = FileAsStringProperty.CompileFileAsPatterns());
				}
				return result;
			}
		}

		// Token: 0x06006F7E RID: 28542 RVA: 0x001E0108 File Offset: 0x001DE308
		internal FileAsStringProperty(NativeStorePropertyDefinition compositeProperty) : base("FileAsStringProperty", compositeProperty, FileAsStringProperty.GetAtomAndRuleProperties())
		{
		}

		// Token: 0x06006F7F RID: 28543 RVA: 0x001E011B File Offset: 0x001DE31B
		internal FileAsStringProperty() : this(InternalSchema.FileAsStringInternal)
		{
		}

		// Token: 0x06006F80 RID: 28544 RVA: 0x001E0128 File Offset: 0x001DE328
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object obj = base.InternalTryGetValue(propertyBag);
			if (PropertyError.IsPropertyError(obj))
			{
				return obj;
			}
			return FileAsStringProperty.TranslateMarkup((string)obj, "{0} ({1})") ?? new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006F81 RID: 28545 RVA: 0x001E0164 File Offset: 0x001DE364
		internal void UpdateFullNameAndSubject(PropertyBag propertyBag)
		{
			StoreSession session = propertyBag.Context.Session;
			FormattedSentence formattedSentence = new FormattedSentence((session != null) ? FileAsStringProperty.GetContactFullNameFormat().ToString(session.InternalPreferedCulture) : FileAsStringProperty.GetContactFullNameFormat());
			FormattedSentence formattedSentence2 = new FormattedSentence((session != null) ? FileAsStringProperty.GetContactSubjectFormat().ToString(session.InternalPreferedCulture) : FileAsStringProperty.GetContactSubjectFormat());
			PropertyBag.BasicPropertyStore propertyBag2 = (PropertyBag.BasicPropertyStore)propertyBag;
			if (base.IsAtomOrRulePropertyDirty(propertyBag2))
			{
				if (!propertyBag.IsPropertyDirty(InternalSchema.DisplayName))
				{
					propertyBag[InternalSchema.DisplayName] = FileAsStringProperty.TranslateMarkup(FileAsStringProperty.GenerateFileAsString(propertyBag2, formattedSentence), "{0} - {1}");
				}
				if (!propertyBag.IsPropertyDirty(InternalSchema.MapiSubject))
				{
					propertyBag[InternalSchema.Subject] = FileAsStringProperty.TranslateMarkup(FileAsStringProperty.GenerateFileAsString(propertyBag2, formattedSentence2), "{0} - {1}");
				}
			}
		}

		// Token: 0x06006F82 RID: 28546 RVA: 0x001E0248 File Offset: 0x001DE448
		private static Dictionary<FileAsMapping, FormattedSentence> CompileFileAsPatterns()
		{
			return new Dictionary<FileAsMapping, FormattedSentence>
			{
				{
					FileAsMapping.Company,
					new FormattedSentence("{Company}")
				},
				{
					FileAsMapping.CompanyLastCommaFirst,
					new FormattedSentence("{Company}\r\n<{Last}, {First} {Middle}>")
				},
				{
					FileAsMapping.CompanyLastFirst,
					new FormattedSentence("{Company}\r\n<{Last}{First} {Middle}>")
				},
				{
					FileAsMapping.CompanyLastSpaceFirst,
					new FormattedSentence("{Company}\r\n<{Last} {First} {Middle}>")
				},
				{
					FileAsMapping.FirstSpaceLast,
					new FormattedSentence("{First} {Middle} {Last} {Suffix}")
				},
				{
					FileAsMapping.LastCommaFirst,
					new FormattedSentence("{Last}, {First} {Middle}")
				},
				{
					FileAsMapping.LastCommaFirstCompany,
					new FormattedSentence("<{Last}, {First} {Middle}>\r\n{Company}")
				},
				{
					FileAsMapping.LastFirst,
					new FormattedSentence("{Last}{First} {Middle}")
				},
				{
					FileAsMapping.LastFirstCompany,
					new FormattedSentence("<{Last}{First} {Middle}>\r\n{Company}")
				},
				{
					FileAsMapping.LastFirstSuffix,
					new FormattedSentence("{Last}{First} {Suffix}")
				},
				{
					FileAsMapping.LastName,
					new FormattedSentence("{Last}")
				},
				{
					FileAsMapping.LastSpaceFirst,
					new FormattedSentence("{Last} {First} {Middle}")
				},
				{
					FileAsMapping.LastSpaceFirstCompany,
					new FormattedSentence("<{Last} {First} {Middle}>\r\n{Company}")
				},
				{
					FileAsMapping.Empty,
					new FormattedSentence(string.Empty)
				},
				{
					FileAsMapping.DisplayName,
					new FormattedSentence("{Display}")
				},
				{
					FileAsMapping.GivenName,
					new FormattedSentence("{First}")
				},
				{
					FileAsMapping.LastFirstMiddleSuffix,
					new FormattedSentence("{Last} {First} {Middle} {Suffix}")
				}
			};
		}

		// Token: 0x06006F83 RID: 28547 RVA: 0x001E03C0 File Offset: 0x001DE5C0
		private static Dictionary<string, NativeStorePropertyDefinition> CreateMapping()
		{
			return Util.AddElements<Dictionary<string, NativeStorePropertyDefinition>, KeyValuePair<string, NativeStorePropertyDefinition>>(new Dictionary<string, NativeStorePropertyDefinition>(), new KeyValuePair<string, NativeStorePropertyDefinition>[]
			{
				new KeyValuePair<string, NativeStorePropertyDefinition>("Title", InternalSchema.DisplayNamePrefix),
				new KeyValuePair<string, NativeStorePropertyDefinition>("First", InternalSchema.GivenName),
				new KeyValuePair<string, NativeStorePropertyDefinition>("Middle", InternalSchema.MiddleName),
				new KeyValuePair<string, NativeStorePropertyDefinition>("Last", InternalSchema.Surname),
				new KeyValuePair<string, NativeStorePropertyDefinition>("Suffix", InternalSchema.Generation),
				new KeyValuePair<string, NativeStorePropertyDefinition>("Company", InternalSchema.CompanyName),
				new KeyValuePair<string, NativeStorePropertyDefinition>("Display", InternalSchema.DisplayName)
			});
		}

		// Token: 0x06006F84 RID: 28548 RVA: 0x001E049C File Offset: 0x001DE69C
		protected override string GenerateCompositePropertyValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			FileAsMapping fileAsMapping = (FileAsMapping)((propertyBag.GetValue(InternalSchema.FileAsId) as int?) ?? -1);
			if (fileAsMapping == FileAsMapping.None || !EnumValidator.IsValidValue<FileAsMapping>(fileAsMapping))
			{
				return (propertyBag.GetValue(InternalSchema.FileAsStringInternal) as string) ?? string.Empty;
			}
			return FileAsStringProperty.GenerateFileAsString(propertyBag, FileAsStringProperty.FileAsPatterns[fileAsMapping]);
		}

		// Token: 0x06006F85 RID: 28549 RVA: 0x001E050C File Offset: 0x001DE70C
		private static string GenerateFileAsString(PropertyBag.BasicPropertyStore propertyBag, FormattedSentence formattedSentence)
		{
			return formattedSentence.Evaluate(new AtomRuleCompositeProperty.FormattedSentenceContext(propertyBag, FileAsStringProperty.placeholderCodeToPropDef));
		}

		// Token: 0x06006F86 RID: 28550 RVA: 0x001E0520 File Offset: 0x001DE720
		private static List<NativeStorePropertyDefinition> GetAtomAndRuleProperties()
		{
			List<NativeStorePropertyDefinition> list = new List<NativeStorePropertyDefinition>(FileAsStringProperty.placeholderCodeToPropDef.Count + 1);
			list.Add(InternalSchema.FileAsId);
			foreach (StorePropertyDefinition storePropertyDefinition in FileAsStringProperty.placeholderCodeToPropDef.Values)
			{
				list.Add((NativeStorePropertyDefinition)storePropertyDefinition);
			}
			return list;
		}

		// Token: 0x06006F87 RID: 28551 RVA: 0x001E059C File Offset: 0x001DE79C
		private static string TranslateMarkup(string rawFileAs, string format)
		{
			if (rawFileAs == null)
			{
				return null;
			}
			string[] array = rawFileAs.Split(new string[]
			{
				"\r\n"
			}, 3, StringSplitOptions.RemoveEmptyEntries);
			switch (array.Length)
			{
			case 1:
				return array[0];
			case 2:
				return string.Format(format, array[0], array[1]);
			default:
				return rawFileAs;
			}
		}

		// Token: 0x040043A0 RID: 17312
		private const string FileAsFormat = "{0} ({1})";

		// Token: 0x040043A1 RID: 17313
		private const string DisplayNameFormat = "{0} - {1}";

		// Token: 0x040043A2 RID: 17314
		private const string SubjectFormat = "{0} - {1}";

		// Token: 0x040043A3 RID: 17315
		private static readonly Dictionary<string, NativeStorePropertyDefinition> placeholderCodeToPropDef = FileAsStringProperty.CreateMapping();

		// Token: 0x040043A4 RID: 17316
		private static Dictionary<FileAsMapping, FormattedSentence> fileAsPatterns;

		// Token: 0x040043A5 RID: 17317
		private static Func<LocalizedString> GetContactFullNameFormat = () => ClientStrings.ContactFullNameFormat;

		// Token: 0x040043A6 RID: 17318
		private static Func<LocalizedString> GetContactSubjectFormat = () => ClientStrings.ContactSubjectFormat;
	}
}
