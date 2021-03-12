using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001BD RID: 445
	internal class NameNormalizer
	{
		// Token: 0x06001149 RID: 4425 RVA: 0x00064EA0 File Offset: 0x000630A0
		public NameNormalizer(CultureInfo culture, SpeechRecognitionEngine engine, string cacheFileNamePrefix, string grammarFolderPath, Logger logger, INormalizationCacheFileStore cacheFileStore)
		{
			ValidateArgument.NotNull(culture, "culture");
			ValidateArgument.NotNull(engine, "engine");
			ValidateArgument.NotNullOrEmpty(cacheFileNamePrefix, "cacheFileNamePrefix");
			ValidateArgument.NotNullOrEmpty(grammarFolderPath, "grammarFolderPath");
			ValidateArgument.NotNull(logger, "logger");
			this.logger = logger;
			this.logger.TraceDebug(this, "Entering NameNormalizer constructor culture='{0}', cacheFileNamePrefix='{1}', grammarFolderPath='{2}'", new object[]
			{
				culture,
				cacheFileNamePrefix,
				grammarFolderPath
			});
			this.culture = culture;
			this.engine = engine;
			this.normalizationCache = new NormalizationCache(culture, cacheFileNamePrefix, grammarFolderPath, logger, cacheFileStore);
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00064F3B File Offset: 0x0006313B
		public NormalizationCache NormalizationCache
		{
			get
			{
				return this.normalizationCache;
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00064F44 File Offset: 0x00063144
		public bool IsNameAcceptable(string encodedDisplayName)
		{
			ValidateArgument.NotNullOrEmpty(encodedDisplayName, "encodedDisplayName");
			this.logger.TraceDebug(this, "Entering NameNormalizer.IsNameAcceptable encodedDisplayName='{0}'", new object[]
			{
				encodedDisplayName
			});
			bool result = true;
			try
			{
				foreach (string text in encodedDisplayName.Split(new char[]
				{
					' '
				}))
				{
					if (Utils.TrimSpaces(text) != null)
					{
						bool flag = true;
						if (this.normalizationCache.TryGetValue(text, out flag))
						{
							this.logger.TraceDebug(this, "Found word='{0}' in cache, acceptWord='{1}'", new object[]
							{
								text,
								flag
							});
						}
						else
						{
							flag = this.IsWordAcceptable(text);
							this.logger.TraceDebug(this, "Adding word='{0}' to normalization cache, acceptWord='{1}'", new object[]
							{
								text,
								flag
							});
							this.normalizationCache[text] = flag;
						}
						if (!flag)
						{
							this.logger.TraceDebug(this, "Skipping name because word='{0}' didn't pass normalization check", new object[]
							{
								text
							});
							result = false;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				result = false;
				this.logger.TraceError(this, "NameNormalizer.IsNameAcceptable input='{0}' exception='{1}'", new object[]
				{
					encodedDisplayName,
					ex
				});
				if (!NameNormalizer.IsExpectedException(ex) && !GrayException.IsGrayException(ex))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000650B8 File Offset: 0x000632B8
		public void UpdateDiskCache()
		{
			this.logger.TraceDebug(this, "Entering NameNormalizer.UpdateDiskCache", new object[0]);
			this.normalizationCache.UpdateDiskCache();
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x000650DC File Offset: 0x000632DC
		private static bool IsExpectedException(Exception e)
		{
			return e is COMException || e is ArgumentException || e is OverflowException || e is FormatException;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00065104 File Offset: 0x00063304
		private bool CanAcceptNormalizedForms(string word, IList<string> normalizedForms, CultureInfo c, bool requiresSpecialHandling)
		{
			bool result = false;
			if (normalizedForms.Count == 2 && normalizedForms[0] != null && normalizedForms[1] != null && requiresSpecialHandling && normalizedForms[0].ToLower(c).Equals(normalizedForms[1].ToLower(c), StringComparison.Ordinal))
			{
				this.logger.TraceDebug(this, "NameNormalizer.CanAcceptNormalizedForms - Accept NormalizedForms = 2 for '{0}' in culture '{1}'", new object[]
				{
					word,
					c
				});
				result = true;
			}
			return result;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00065178 File Offset: 0x00063378
		private bool IsWordAcceptable(string word)
		{
			bool result = true;
			IList<string> list = null;
			try
			{
				list = SpeechRecognizerInfo.GetNormalizedForms(this.engine, word);
			}
			catch (COMException ex)
			{
				this.logger.TraceError(this, "NameNormalizer.IsWordAcceptable - word='{0}', exception='{1}'", new object[]
				{
					word,
					ex
				});
				throw;
			}
			ExAssert.RetailAssert(list != null, "normalizedForms is null");
			ExAssert.RetailAssert(list.Count > 0, "normalizedForms is empty");
			if (list.Count > 1 && !this.CanAcceptNormalizedForms(word, list, this.culture, SpeechRecognizerInfo.TextNormalizationRequiresSpecialHandling(this.culture)))
			{
				if (ExTraceGlobals.UMGrammarGeneratorTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(128);
					foreach (string value in list)
					{
						stringBuilder.Append(value);
						stringBuilder.Append(" ");
					}
					this.logger.TraceDebug(this, "NameNormalizer:IsWordAcceptable word='{0}' normalized to '{1}' count='{2}'", new object[]
					{
						word,
						stringBuilder.ToString(),
						list.Count
					});
					stringBuilder.Length = 0;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x04000AC6 RID: 2758
		private const int MaxNormalizedForms = 1;

		// Token: 0x04000AC7 RID: 2759
		private CultureInfo culture;

		// Token: 0x04000AC8 RID: 2760
		private SpeechRecognitionEngine engine;

		// Token: 0x04000AC9 RID: 2761
		private NormalizationCache normalizationCache;

		// Token: 0x04000ACA RID: 2762
		private Logger logger;
	}
}
