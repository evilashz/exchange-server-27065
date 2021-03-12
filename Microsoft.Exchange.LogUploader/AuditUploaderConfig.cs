using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000007 RID: 7
	public static class AuditUploaderConfig
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000328C File Offset: 0x0000148C
		public static int RuleCount
		{
			get
			{
				return AuditUploaderConfig.filteringRules.Count;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003298 File Offset: 0x00001498
		public static void Initialize(string fileName)
		{
			try
			{
				using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
				{
					XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());
					AuditUploaderConfig.InitializeWithReader(reader);
					fileStream.Close();
				}
			}
			catch (DirectoryNotFoundException ex)
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_ConfigFileNotFound, fileName, new object[]
				{
					ex.Message
				});
				AuditUploaderConfig.InitializeWithReader(null);
			}
			catch (FileNotFoundException ex2)
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_ConfigFileNotFound, fileName, new object[]
				{
					ex2.Message
				});
				AuditUploaderConfig.InitializeWithReader(null);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003358 File Offset: 0x00001558
		public static void InitializeWithReader(XmlDictionaryReader reader)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(AuditUploaderConfigSchema));
			if (reader != null)
			{
				try
				{
					try
					{
						AuditUploaderConfig.config = (AuditUploaderConfigSchema)dataContractSerializer.ReadObject(reader, true);
					}
					catch (SerializationException ex)
					{
						EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_InvalidConfigFile, ex.Message, new object[0]);
						AuditUploaderConfig.InitializeDefault();
					}
					goto IL_58;
				}
				finally
				{
					reader.Close();
				}
			}
			AuditUploaderConfig.InitializeDefault();
			IL_58:
			AuditUploaderConfig.InitializeParsingRules();
			AuditUploaderConfig.GenerateDictionary();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000033E4 File Offset: 0x000015E4
		public static void InitializeDefault()
		{
			FilteringPredicate filteringPredicate = new FilteringPredicate();
			filteringPredicate.Initialize();
			FilteringRule filteringRule = new FilteringRule();
			filteringRule.ActionToPerform = Actions.LetThrough;
			filteringRule.Predicate = filteringPredicate;
			filteringRule.Throttle = null;
			List<FilteringRule> list = new List<FilteringRule>();
			list.Add(filteringRule);
			AuditUploaderConfig.config = new AuditUploaderConfigSchema();
			AuditUploaderConfig.config.FilteringSection = list;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000343A File Offset: 0x0000163A
		public static Actions GetAction(string component, string tenant, string user, string operation)
		{
			if (AuditUploaderConfig.filteringRules == null)
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_InvalidRuleCollection, "Audit Uploader rules collection was not properly generated.", new object[0]);
				AuditUploaderConfig.InitializeWithReader(null);
			}
			return AuditUploaderConfig.filteringRules.GetOperation(component, tenant, user, operation, DateTime.UtcNow);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003476 File Offset: 0x00001676
		public static Actions GetAction(string component, string tenant, string user, string operation, DateTime currentTime)
		{
			if (AuditUploaderConfig.filteringRules == null)
			{
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_InvalidRuleCollection, "Audit Uploader rules collection was not properly generated.", new object[0]);
				AuditUploaderConfig.InitializeWithReader(null);
			}
			return AuditUploaderConfig.filteringRules.GetOperation(component, tenant, user, operation, currentTime);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000034AF File Offset: 0x000016AF
		public static Actions ParsingCheck(Parsing outcome)
		{
			return AuditUploaderConfig.parsingRules[outcome];
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000034BC File Offset: 0x000016BC
		public static void Reset()
		{
			AuditUploaderConfig.config = null;
			AuditUploaderConfig.filteringRules = null;
			AuditUploaderConfig.parsingRules[Parsing.Failed] = Actions.LetThrough;
			AuditUploaderConfig.parsingRules[Parsing.Passed] = Actions.LetThrough;
			AuditUploaderConfig.parsingRules[Parsing.PartiallyPassed] = Actions.LetThrough;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000034F0 File Offset: 0x000016F0
		private static void InitializeParsingRules()
		{
			if (AuditUploaderConfig.config.ParsingSection != null)
			{
				foreach (ParsingRule parsingRule in AuditUploaderConfig.config.ParsingSection)
				{
					AuditUploaderConfig.parsingRules[parsingRule.Predicate.ParsingOutcome] = parsingRule.ActionToPerform;
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003568 File Offset: 0x00001768
		private static void GenerateDictionary()
		{
			if (AuditUploaderConfig.filteringRules == null)
			{
				AuditUploaderConfig.filteringRules = new AuditUploaderConfigRules();
			}
			foreach (FilteringRule filteringRule in AuditUploaderConfig.config.FilteringSection)
			{
				AuditUploaderDictionaryKey key = new AuditUploaderDictionaryKey((filteringRule.Predicate.Component != null) ? filteringRule.Predicate.Component : AuditUploaderDictionaryKey.WildCard, (filteringRule.Predicate.Tenant != null) ? filteringRule.Predicate.Tenant : AuditUploaderDictionaryKey.WildCard, (filteringRule.Predicate.User != null) ? filteringRule.Predicate.User : AuditUploaderDictionaryKey.WildCard, (filteringRule.Predicate.Operation != null) ? filteringRule.Predicate.Operation : AuditUploaderDictionaryKey.WildCard);
				if (!AuditUploaderConfig.filteringRules.Contains(key))
				{
					AuditUploaderConfig.filteringRules.Add(key, new AuditUploaderAction(filteringRule.ActionToPerform, (filteringRule.Throttle != null) ? filteringRule.Throttle.Interval : null));
				}
				else
				{
					EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_InvalidConfigFile, "Duplicate rule detected in the configuration", new object[0]);
				}
			}
			if (!AuditUploaderConfig.filteringRules.Contains(new AuditUploaderDictionaryKey(AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard)))
			{
				AuditUploaderConfig.filteringRules.Add(new AuditUploaderDictionaryKey(AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard, AuditUploaderDictionaryKey.WildCard), new AuditUploaderAction(Actions.LetThrough, null));
			}
		}

		// Token: 0x0400003B RID: 59
		private static AuditUploaderConfigSchema config;

		// Token: 0x0400003C RID: 60
		private static Dictionary<Parsing, Actions> parsingRules = new Dictionary<Parsing, Actions>(Enum.GetNames(typeof(Parsing)).Length)
		{
			{
				Parsing.Passed,
				Actions.LetThrough
			},
			{
				Parsing.Failed,
				Actions.LetThrough
			},
			{
				Parsing.PartiallyPassed,
				Actions.LetThrough
			}
		};

		// Token: 0x0400003D RID: 61
		private static AuditUploaderConfigRules filteringRules;
	}
}
