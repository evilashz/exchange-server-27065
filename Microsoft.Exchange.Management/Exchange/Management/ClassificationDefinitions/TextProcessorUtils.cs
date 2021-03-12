using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200084E RID: 2126
	internal static class TextProcessorUtils
	{
		// Token: 0x060049C5 RID: 18885 RVA: 0x0012F778 File Offset: 0x0012D978
		private static Dictionary<TextProcessorType, TextProcessorGrouping> GetDiskOobProcessorsGroupedByType()
		{
			return TextProcessorUtils.GetOnDiskMceConfigData<Dictionary<TextProcessorType, TextProcessorGrouping>>((TextProcessorUtils.MceConfigManagerBase diskMceConfigManager) => diskMceConfigManager.OobTextProcessorGrouping);
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x0012F79C File Offset: 0x0012D99C
		private static T GetOnDiskMceConfigData<T>(Func<TextProcessorUtils.MceConfigManagerBase, T> onDiskMceConfigDataExtractorFunc) where T : class
		{
			T result;
			try
			{
				TextProcessorUtils.MceConfigManagerBase value = TextProcessorUtils.onDiskMceConfigManager.Value;
				T t = onDiskMceConfigDataExtractorFunc(value);
				result = t;
			}
			catch (SystemException underlyingException)
			{
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteClassificationEngineConfigurationErrorInformation(0, underlyingException);
				result = default(T);
			}
			return result;
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x0012F7EC File Offset: 0x0012D9EC
		internal static Dictionary<TextProcessorType, TextProcessorGrouping> OobProcessorsGroupedByType
		{
			get
			{
				return TextProcessorUtils.GetDiskOobProcessorsGroupedByType() ?? TextProcessorUtils.embeddedMceConfigManager.Value.OobTextProcessorGrouping;
			}
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x0012F808 File Offset: 0x0012DA08
		internal static IDictionary<string, ExchangeBuild> GetTextProcessorsFromTextProcessorGrouping(IDictionary<TextProcessorType, TextProcessorGrouping> textProcessorsGroupings, Func<TextProcessorType, bool> predicate = null)
		{
			if (textProcessorsGroupings == null)
			{
				throw new ArgumentNullException("textProcessorsGroupings");
			}
			Dictionary<string, ExchangeBuild> dictionary = new Dictionary<string, ExchangeBuild>(ClassificationDefinitionConstants.TextProcessorIdComparer);
			foreach (KeyValuePair<TextProcessorType, TextProcessorGrouping> keyValuePair in textProcessorsGroupings)
			{
				if (predicate == null || predicate(keyValuePair.Key))
				{
					foreach (KeyValuePair<string, ExchangeBuild> keyValuePair2 in ((IEnumerable<KeyValuePair<string, ExchangeBuild>>)keyValuePair.Value))
					{
						dictionary.Add(keyValuePair2.Key, keyValuePair2.Value);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x0012FB60 File Offset: 0x0012DD60
		internal static IEnumerable<KeyValuePair<string, ExchangeBuild>> GetTextProcessorReferences(XElement rulePackElement, ISet<string> matchElementNames)
		{
			if (rulePackElement == null)
			{
				throw new ArgumentNullException("rulePackElement");
			}
			if (matchElementNames == null)
			{
				throw new ArgumentNullException("matchElementNames");
			}
			return from rulePackChildElement in rulePackElement.Descendants().AsParallel<XElement>()
			where matchElementNames.Contains(rulePackChildElement.Name.LocalName)
			let textProcessorId = rulePackChildElement.Attribute("idRef").Value
			let version = XmlProcessingUtils.GetRulePackElementVersion(rulePackChildElement)
			select new KeyValuePair<string, ExchangeBuild>(textProcessorId, version);
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x0012FC1D File Offset: 0x0012DE1D
		internal static IEnumerable<KeyValuePair<string, ExchangeBuild>> GetTextProcessorReferences(XElement rulePackElement)
		{
			return TextProcessorUtils.GetTextProcessorReferences(rulePackElement, ClassificationDefinitionConstants.MceMatchElementNames);
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x0012FC2A File Offset: 0x0012DE2A
		internal static IEnumerable<TextProcessorGrouping> GetRulePackScopedTextProcessorsGroupedByType(XDocument rulePackXDoc)
		{
			if (rulePackXDoc == null)
			{
				throw new ArgumentNullException("rulePackXDoc");
			}
			return TextProcessorUtils.MceConfigManagerBase.GetTextProcessorsGroupedByType(rulePackXDoc, ClassificationDefinitionConstants.MceCustomProcessorElementNames);
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x0012FC48 File Offset: 0x0012DE48
		internal static ExchangeBuild GetTextProcessorVersion(TextProcessorType textProcessorType, XElement textProcessorElement)
		{
			if (textProcessorElement == null)
			{
				throw new ArgumentNullException("textProcessorElement");
			}
			ExAssert.RetailAssert(TextProcessorType.Function <= textProcessorType && textProcessorType <= TextProcessorType.Fingerprint, "The specified textProcessorType '{0}' is out-of-range", new object[]
			{
				textProcessorType.ToString()
			});
			ExchangeBuild result = ClassificationDefinitionConstants.DefaultVersion;
			if (ClassificationDefinitionConstants.TextProcessorTypeToVersions.ContainsKey(textProcessorType))
			{
				result = ClassificationDefinitionConstants.TextProcessorTypeToVersions[textProcessorType];
			}
			else if (textProcessorType == TextProcessorType.Function)
			{
				string value = textProcessorElement.Attribute("id").Value;
				ExAssert.RetailAssert(value != null, "The functionName in the specfied textProcessorElement is null", new object[]
				{
					textProcessorElement.ToString()
				});
				if (ClassificationDefinitionConstants.FunctionNameToVersions.ContainsKey(value))
				{
					result = ClassificationDefinitionConstants.FunctionNameToVersions[value];
				}
			}
			return result;
		}

		// Token: 0x04002C70 RID: 11376
		private static readonly Lazy<TextProcessorUtils.MceConfigManagerBase> embeddedMceConfigManager = new Lazy<TextProcessorUtils.MceConfigManagerBase>(new Func<TextProcessorUtils.MceConfigManagerBase>(TextProcessorUtils.EmbeddedMceConfigManager.Create), LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x04002C71 RID: 11377
		private static readonly Lazy<TextProcessorUtils.MceConfigManagerBase> onDiskMceConfigManager = new Lazy<TextProcessorUtils.MceConfigManagerBase>(new Func<TextProcessorUtils.MceConfigManagerBase>(TextProcessorUtils.OnDiskMceConfigManager.Create), LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x0200084F RID: 2127
		private abstract class MceConfigManagerBase
		{
			// Token: 0x17001629 RID: 5673
			// (get) Token: 0x060049D2 RID: 18898 RVA: 0x0012FD37 File Offset: 0x0012DF37
			// (set) Token: 0x060049D3 RID: 18899 RVA: 0x0012FD3F File Offset: 0x0012DF3F
			internal virtual Dictionary<TextProcessorType, TextProcessorGrouping> OobTextProcessorGrouping
			{
				get
				{
					return this.cachedOobTextProcessorsGroupedByType;
				}
				private set
				{
					this.cachedOobTextProcessorsGroupedByType = value;
				}
			}

			// Token: 0x060049D4 RID: 18900 RVA: 0x0012FD48 File Offset: 0x0012DF48
			private static TextProcessorType ParseTextProcessorType(string textProcessorElementName)
			{
				ExAssert.RetailAssert(textProcessorElementName != null, "The text processor element name passed to ParseTextProcessorType should not be null!");
				TextProcessorType result;
				bool condition = Enum.TryParse<TextProcessorType>(textProcessorElementName, false, out result);
				ExAssert.RetailAssert(condition, "The text processor element name '{0}' cannot be parsed into TextProcessorType enum", new object[]
				{
					textProcessorElementName
				});
				return result;
			}

			// Token: 0x060049D5 RID: 18901 RVA: 0x00130180 File Offset: 0x0012E380
			internal static IEnumerable<TextProcessorGrouping> GetTextProcessorsGroupedByType(XDocument rulePackXDoc, ISet<string> textProcessorElementNames)
			{
				ExAssert.RetailAssert(rulePackXDoc != null, "The rule pack XDocument instance passed to GetTextProcessorIdsGroupedByType cannot be null!");
				ExAssert.RetailAssert(textProcessorElementNames != null, "Must specify the text processor element names when calling GetTextProcessorIdsGroupedByType");
				return from rulePackElement in rulePackXDoc.Descendants().AsParallel<XElement>()
				let rulePackElementName = rulePackElement.Name.LocalName
				where textProcessorElementNames.Contains(rulePackElementName)
				let textProcessorType = TextProcessorUtils.MceConfigManagerBase.ParseTextProcessorType(rulePackElementName)
				let versionedTextProcessor = new KeyValuePair<string, ExchangeBuild>(rulePackElement.Attribute("id").Value, TextProcessorUtils.GetTextProcessorVersion(textProcessorType, rulePackElement))
				group versionedTextProcessor by textProcessorType into textProcessorGrouping
				select new TextProcessorGrouping(textProcessorGrouping, null);
			}

			// Token: 0x060049D6 RID: 18902 RVA: 0x001302A4 File Offset: 0x0012E4A4
			private static IEnumerable<TextProcessorGrouping> GetOobProcessorsGroupedByType(XDocument rulePackXDoc)
			{
				return TextProcessorUtils.MceConfigManagerBase.GetTextProcessorsGroupedByType(rulePackXDoc, ClassificationDefinitionConstants.MceProcessorElementNames);
			}

			// Token: 0x060049D7 RID: 18903 RVA: 0x001302B4 File Offset: 0x0012E4B4
			private XDocument DeserializeMceConfig()
			{
				XmlReaderSettings xmlReaderSettings = ClassificationDefinitionUtils.CreateSafeXmlReaderSettings();
				xmlReaderSettings.IgnoreComments = true;
				xmlReaderSettings.ValidationType = ValidationType.None;
				XDocument result;
				using (Stream stream = this.OpenSourceStream())
				{
					using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
					{
						result = XDocument.Load(xmlReader);
					}
				}
				return result;
			}

			// Token: 0x060049D8 RID: 18904 RVA: 0x00130328 File Offset: 0x0012E528
			protected virtual void ReadConfigStream()
			{
				XDocument rulePackXDoc = this.DeserializeMceConfig();
				try
				{
					Dictionary<TextProcessorType, TextProcessorGrouping> dictionary = TextProcessorUtils.MceConfigManagerBase.GetOobProcessorsGroupedByType(rulePackXDoc).ToDictionary((TextProcessorGrouping textProcessorGroup) => textProcessorGroup.Key);
					this.cachedOobTextProcessorsGroupedByType = dictionary;
				}
				catch (AggregateException ex)
				{
					throw new XmlException(Strings.ClassificationRuleCollectionConfigValidationUnexpectedContents, ex.Flatten());
				}
			}

			// Token: 0x060049D9 RID: 18905
			protected abstract Stream OpenSourceStream();

			// Token: 0x04002C76 RID: 11382
			private Dictionary<TextProcessorType, TextProcessorGrouping> cachedOobTextProcessorsGroupedByType;
		}

		// Token: 0x02000850 RID: 2128
		private class EmbeddedMceConfigManager : TextProcessorUtils.MceConfigManagerBase
		{
			// Token: 0x060049E2 RID: 18914 RVA: 0x001303A0 File Offset: 0x0012E5A0
			protected override Stream OpenSourceStream()
			{
				return ClassificationDefinitionUtils.LoadStreamFromEmbeddedResource("MceConfig.xml");
			}

			// Token: 0x060049E3 RID: 18915 RVA: 0x001303AC File Offset: 0x0012E5AC
			internal static TextProcessorUtils.EmbeddedMceConfigManager Create()
			{
				TextProcessorUtils.EmbeddedMceConfigManager embeddedMceConfigManager = new TextProcessorUtils.EmbeddedMceConfigManager();
				embeddedMceConfigManager.ReadConfigStream();
				return embeddedMceConfigManager;
			}
		}

		// Token: 0x02000851 RID: 2129
		private class OnDiskMceConfigManager : TextProcessorUtils.MceConfigManagerBase
		{
			// Token: 0x060049E5 RID: 18917 RVA: 0x001303CE File Offset: 0x0012E5CE
			private OnDiskMceConfigManager(FileInfo configFileInfo)
			{
				ExAssert.RetailAssert(configFileInfo != null, "The FileInfo passed into OnDiskMceConfigManager should not be null!");
				this.readerWriterLock = new ReaderWriterLockSlim();
				this.configFileInfo = configFileInfo;
			}

			// Token: 0x1700162A RID: 5674
			// (get) Token: 0x060049E6 RID: 18918 RVA: 0x00130401 File Offset: 0x0012E601
			internal override Dictionary<TextProcessorType, TextProcessorGrouping> OobTextProcessorGrouping
			{
				get
				{
					return this.GetCachedConfigData<Dictionary<TextProcessorType, TextProcessorGrouping>>(() => this.<>n__FabricatedMethod22());
				}
			}

			// Token: 0x060049E7 RID: 18919 RVA: 0x00130418 File Offset: 0x0012E618
			private T GetCachedConfigData<T>(Func<T> getCachedDataFunc)
			{
				T result;
				using (UpgradeableReadLockScope upgradeableReadLockScope = UpgradeableReadLockScope.Create(this.readerWriterLock))
				{
					if (this.IsDirty())
					{
						using (upgradeableReadLockScope.Upgrade())
						{
							this.configFileInfo.Refresh();
							this.ReadConfigStream();
							return getCachedDataFunc();
						}
					}
					using (upgradeableReadLockScope.Downgrade())
					{
						result = getCachedDataFunc();
					}
				}
				return result;
			}

			// Token: 0x060049E8 RID: 18920 RVA: 0x001304B4 File Offset: 0x0012E6B4
			private bool IsDirty()
			{
				FileInfo fileInfo = new FileInfo(this.configFileInfo.FullName);
				return !fileInfo.Exists || !this.configFileInfo.LastWriteTimeUtc.Equals(fileInfo.LastWriteTimeUtc);
			}

			// Token: 0x060049E9 RID: 18921 RVA: 0x001304F8 File Offset: 0x0012E6F8
			private static string GetOnDiskMceConfigFilePath()
			{
				string localMachineMceConfigDirectory = ClassificationDefinitionUtils.GetLocalMachineMceConfigDirectory(false);
				return Path.Combine(localMachineMceConfigDirectory, ClassificationDefinitionConstants.OnDiskMceConfigFileName);
			}

			// Token: 0x060049EA RID: 18922 RVA: 0x00130518 File Offset: 0x0012E718
			internal static TextProcessorUtils.OnDiskMceConfigManager Create()
			{
				string onDiskMceConfigFilePath = TextProcessorUtils.OnDiskMceConfigManager.GetOnDiskMceConfigFilePath();
				FileInfo fileInfo = new FileInfo(onDiskMceConfigFilePath);
				if (!fileInfo.Exists)
				{
					throw new FileNotFoundException(new FileNotFoundException().Message, onDiskMceConfigFilePath);
				}
				TextProcessorUtils.OnDiskMceConfigManager onDiskMceConfigManager = new TextProcessorUtils.OnDiskMceConfigManager(fileInfo);
				onDiskMceConfigManager.ReadConfigStream();
				return onDiskMceConfigManager;
			}

			// Token: 0x060049EB RID: 18923 RVA: 0x00130559 File Offset: 0x0012E759
			protected override Stream OpenSourceStream()
			{
				return this.configFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
			}

			// Token: 0x04002C7E RID: 11390
			private readonly ReaderWriterLockSlim readerWriterLock;

			// Token: 0x04002C7F RID: 11391
			private readonly FileInfo configFileInfo;
		}
	}
}
