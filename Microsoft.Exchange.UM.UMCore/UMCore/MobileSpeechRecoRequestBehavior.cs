using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000279 RID: 633
	internal abstract class MobileSpeechRecoRequestBehavior : IMobileSpeechRecoRequestBehavior
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x00053FE4 File Offset: 0x000521E4
		public static IMobileSpeechRecoRequestBehavior Create(MobileSpeechRecoRequestType requestType, Guid requestId, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid, ExTimeZone timeZone)
		{
			ValidateArgument.NotNull(culture, "culture");
			MobileSpeechRecoTracer.TraceDebug(null, requestId, "Entering MobileSpeechRecoRequestBehavior.Create culture='{0}' userObjectGuid='{1}' tenantGuid='{2}' timeZone='{3}'", new object[]
			{
				culture,
				userObjectGuid,
				tenantGuid,
				timeZone
			});
			IMobileSpeechRecoRequestBehavior result = null;
			switch (requestType)
			{
			case MobileSpeechRecoRequestType.FindInGAL:
				return new FindInGALRequestBehavior(requestId, culture, userObjectGuid, tenantGuid);
			case MobileSpeechRecoRequestType.FindInPersonalContacts:
				return new FindInPersonalContactsRequestBehavior(requestId, culture, userObjectGuid, tenantGuid);
			case MobileSpeechRecoRequestType.StaticGrammarsCombined:
				return new CombinedStaticGrammarScenariosRequestBehavior(requestId, culture, userObjectGuid, tenantGuid, timeZone);
			case MobileSpeechRecoRequestType.FindPeople:
				throw new ArgumentOutOfRangeException("requestType", requestType, "Invalid value");
			case MobileSpeechRecoRequestType.DaySearch:
				return new DaySearchBehavior(requestId, culture, userObjectGuid, tenantGuid, timeZone);
			case MobileSpeechRecoRequestType.AppointmentCreation:
				return new DateTimeAndDurationRecognitionBehavior(requestId, culture, userObjectGuid, tenantGuid, timeZone);
			}
			ExAssert.RetailAssert(false, "Invalid scenario value '{0}'", new object[]
			{
				requestType
			});
			return result;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000540D0 File Offset: 0x000522D0
		public static Dictionary<string, string> GetKeywordsFromGrammar(string keywordGrammarId, CultureInfo culture)
		{
			string text = GrammarUtils.GetLocString(keywordGrammarId, culture);
			text = MobileSpeechRecoRequestBehavior.keywordsSpecialCharReplacer.Replace(text, " ");
			string[] array = text.Split(null, StringSplitOptions.RemoveEmptyEntries);
			Dictionary<string, string> dictionary = new Dictionary<string, string>(array.Length, StringComparer.OrdinalIgnoreCase);
			foreach (string key in array)
			{
				if (!dictionary.ContainsKey(key))
				{
					dictionary.Add(key, "1");
				}
			}
			return dictionary;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00054140 File Offset: 0x00052340
		public MobileSpeechRecoRequestBehavior(Guid id, CultureInfo culture, Guid userObjectGuid, Guid tenantGuid)
		{
			ValidateArgument.NotNull(culture, "culture");
			ExAssert.RetailAssert(userObjectGuid != Guid.Empty, "userObjectGuid = '{0}' (Guid.Empty)", new object[]
			{
				userObjectGuid
			});
			MobileSpeechRecoTracer.TraceDebug(this, id, "Entering MobileSpeechRecoRequestBehavior constructor culture='{0}' userObjectGuid='{1}', tenantGuid='{2}'", new object[]
			{
				culture,
				userObjectGuid,
				tenantGuid
			});
			this.id = id;
			this.culture = culture;
			this.userObjectGuid = userObjectGuid;
			this.tenantGuid = tenantGuid;
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000541CC File Offset: 0x000523CC
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x000541D4 File Offset: 0x000523D4
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060012D3 RID: 4819
		public abstract SpeechRecognitionEngineType EngineType { get; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060012D4 RID: 4820
		public abstract int MaxAlternates { get; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060012D5 RID: 4821
		public abstract int MaxProcessingTime { get; }

		// Token: 0x060012D6 RID: 4822
		public abstract List<UMGrammar> PrepareGrammars();

		// Token: 0x060012D7 RID: 4823
		public abstract string ProcessRecoResults(List<IMobileRecognitionResult> results);

		// Token: 0x060012D8 RID: 4824 RVA: 0x000541DC File Offset: 0x000523DC
		public bool CanProcessResultType(MobileSpeechRecoResultType resultType)
		{
			return this.SupportedResultTypes.Contains(resultType);
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060012D9 RID: 4825
		protected abstract MobileSpeechRecoResultType[] SupportedResultTypes { get; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x000541EF File Offset: 0x000523EF
		protected Guid UserObjectGuid
		{
			get
			{
				return this.userObjectGuid;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000541F7 File Offset: 0x000523F7
		protected Guid TenantGuid
		{
			get
			{
				return this.tenantGuid;
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000541FF File Offset: 0x000523FF
		protected virtual void ProcessSemanticTags(Dictionary<string, string> semanticTags)
		{
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00054201 File Offset: 0x00052401
		protected virtual bool ShouldAcceptBasedOnSmartConfidenceThreshold(IUMRecognitionPhrase phrase, MobileSpeechRecoResultType resultType)
		{
			return true;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00054204 File Offset: 0x00052404
		protected string ConvertResultsToXml(List<IMobileRecognitionResult> results, List<string> requiredTags)
		{
			string result = string.Empty;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding()))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlTextWriter.WriteStartDocument();
					xmlTextWriter.WriteStartElement("MobileReco");
					if (results.Count > 0)
					{
						IMobileRecognitionResult mobileRecognitionResult = results[0];
						List<IUMRecognitionPhrase> list = new List<IUMRecognitionPhrase>();
						foreach (IUMRecognitionPhrase iumrecognitionPhrase in mobileRecognitionResult.GetRecognitionResults())
						{
							if (this.ShouldAcceptBasedOnSmartConfidenceThreshold(iumrecognitionPhrase, mobileRecognitionResult.MobileScenarioResultType))
							{
								MobileSpeechRecoTracer.TraceDebug(this, this.Id, "ConvertResultsToXml, Adding recognition phrase '{0}', confidence '{1}' to result because it is above the smart Confidence threshold", new object[]
								{
									iumrecognitionPhrase.Text,
									iumrecognitionPhrase.Confidence
								});
								list.Add(iumrecognitionPhrase);
							}
							else
							{
								MobileSpeechRecoTracer.TraceDebug(this, this.Id, "ConvertResultsToXml, phrase '{0}', confidence '{1}' will not be added to result because it is below the smart Confidence threshold", new object[]
								{
									iumrecognitionPhrase.Text,
									iumrecognitionPhrase.Confidence
								});
							}
						}
						if (list.Count == 0)
						{
							xmlTextWriter.WriteAttributeString("ResultType", MobileSpeechRecoResultType.None.ToString());
							goto IL_28A;
						}
						xmlTextWriter.WriteAttributeString("ResultType", mobileRecognitionResult.MobileScenarioResultType.ToString());
						int num = 0;
						using (List<IUMRecognitionPhrase>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								IUMRecognitionPhrase iumrecognitionPhrase2 = enumerator2.Current;
								if (num >= this.MaxAlternates)
								{
									break;
								}
								num++;
								xmlTextWriter.WriteStartElement("Alternate");
								xmlTextWriter.WriteAttributeString("text", iumrecognitionPhrase2.Text);
								xmlTextWriter.WriteAttributeString("confidence", iumrecognitionPhrase2.Confidence.ToString("F4", CultureInfo.InvariantCulture));
								Dictionary<string, string> dictionary = new Dictionary<string, string>(requiredTags.Count);
								foreach (string key in requiredTags)
								{
									dictionary.Add(key, iumrecognitionPhrase2[key].ToString());
								}
								this.ProcessSemanticTags(dictionary);
								foreach (string text in dictionary.Keys)
								{
									xmlTextWriter.WriteElementString(text, dictionary[text]);
								}
								xmlTextWriter.WriteEndElement();
							}
							goto IL_28A;
						}
					}
					xmlTextWriter.WriteAttributeString("ResultType", MobileSpeechRecoResultType.None.ToString());
					IL_28A:
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndDocument();
					xmlTextWriter.Flush();
					using (StreamReader streamReader = new StreamReader(memoryStream))
					{
						memoryStream.Seek(0L, SeekOrigin.Begin);
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0005459C File Offset: 0x0005279C
		protected ADRecipient GetADRecipient()
		{
			Guid guid = this.UserObjectGuid;
			ADRecipient result;
			try
			{
				MobileSpeechRecoTracer.TracePerformance(this, this.Id, "Entering MobileSpeechRecoRequestBehavior.GetADRecipient for '{0}'", new object[]
				{
					guid
				});
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromTenantGuid(this.TenantGuid);
				ADRecipient adrecipient = iadrecipientLookup.LookupByObjectId(new ADObjectId(guid));
				if (adrecipient == null)
				{
					MobileSpeechRecoTracer.TraceError(this, this.Id, "User with object guid '{0}' was not found", new object[]
					{
						guid
					});
					throw new UserNotFoundException(guid);
				}
				result = adrecipient;
			}
			catch (LocalizedException ex)
			{
				MobileSpeechRecoTracer.TraceError(this, this.Id, "Error looking up user with object guid '{0}' -- {1}", new object[]
				{
					guid,
					ex
				});
				throw new UserNotFoundException(guid, ex);
			}
			finally
			{
				MobileSpeechRecoTracer.TracePerformance(this, this.Id, "Leaving MobileSpeechRecoRequestBehaviorr.GetADRecipient for '{0}'", new object[]
				{
					guid
				});
			}
			return result;
		}

		// Token: 0x04000C37 RID: 3127
		private static Regex keywordsSpecialCharReplacer = new Regex("({\\d+})|;|\\(|\\)");

		// Token: 0x04000C38 RID: 3128
		private readonly Guid tenantGuid;

		// Token: 0x04000C39 RID: 3129
		private Guid id;

		// Token: 0x04000C3A RID: 3130
		private CultureInfo culture;

		// Token: 0x04000C3B RID: 3131
		private Guid userObjectGuid;
	}
}
