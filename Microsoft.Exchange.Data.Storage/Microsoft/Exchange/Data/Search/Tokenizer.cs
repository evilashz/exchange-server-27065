using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Search
{
	// Token: 0x02000CF1 RID: 3313
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Tokenizer
	{
		// Token: 0x0600723B RID: 29243 RVA: 0x001F9468 File Offset: 0x001F7668
		static Tokenizer()
		{
			Tokenizer.languageWordBreakerGuidMapping = new Dictionary<string, Guid>();
			Tokenizer.languageWordBreakerGuidMapping["de"] = Tokenizer.GermanWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["de-DE"] = Tokenizer.GermanWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["en"] = Tokenizer.EnglishUSWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["en-US"] = Tokenizer.EnglishUSWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["en-GB"] = Tokenizer.EnglishBreatBritainWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["es"] = Tokenizer.SpanishWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["es-ES"] = Tokenizer.SpanishWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["fr"] = Tokenizer.FrenchWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["fr-FR"] = Tokenizer.FrenchWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["it"] = Tokenizer.ItalianWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["it-IT"] = Tokenizer.ItalianWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["nl"] = Tokenizer.DutchWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["nl-NL"] = Tokenizer.DutchWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["sv"] = Tokenizer.SweidishWordBreakerGuid;
			Tokenizer.languageWordBreakerGuidMapping["sv-SE"] = Tokenizer.SweidishWordBreakerGuid;
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x001F978C File Offset: 0x001F798C
		public static void ReleaseWordBreakers()
		{
			try
			{
				Tokenizer.cacheLock.AcquireWriterLock(60000);
				foreach (Guid key in Tokenizer.guidWordBreakerMapping.Keys)
				{
					IWordBreaker o = Tokenizer.guidWordBreakerMapping[key];
					Marshal.ReleaseComObject(o);
				}
				Tokenizer.guidWordBreakerMapping.Clear();
			}
			catch (ApplicationException)
			{
				ExTraceGlobals.CcGenericTracer.TraceError(0L, "Unable to acquire lock to release word breakers");
			}
			finally
			{
				Tokenizer.cacheLock.ReleaseLock();
			}
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x001F9844 File Offset: 0x001F7A44
		public List<Token> Tokenize(CultureInfo cultureInfo, string text)
		{
			List<Token> list = null;
			IWordBreaker wordBreaker = this.LoadWordBreaker(cultureInfo);
			if (wordBreaker != null)
			{
				IWordSink wordSink = new WordSink();
				TEXT_SOURCE text_SOURCE = default(TEXT_SOURCE);
				text_SOURCE.FillTextBuffer = new FillTextBuffer(this.FillBuffer);
				text_SOURCE.Buffer = text;
				text_SOURCE.Current = 0;
				text_SOURCE.End = text_SOURCE.Buffer.Length;
				if (wordBreaker.BreakText(ref text_SOURCE, wordSink, null) == 0)
				{
					list = ((WordSink)wordSink).Tokens;
				}
			}
			if (list == null)
			{
				list = new List<Token>();
				list.Add(new Token(0, text.Length));
			}
			return list;
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x001F98D6 File Offset: 0x001F7AD6
		private uint FillBuffer(ref TEXT_SOURCE textSource)
		{
			return 2147751808U;
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x001F98E0 File Offset: 0x001F7AE0
		private IWordBreaker LoadWordBreaker(CultureInfo cultureInfo)
		{
			object obj = null;
			Guid guid;
			if (Tokenizer.languageWordBreakerGuidMapping.ContainsKey(cultureInfo.Name))
			{
				guid = Tokenizer.languageWordBreakerGuidMapping[cultureInfo.Name];
			}
			else
			{
				guid = Tokenizer.NeutralWordBreakerGuid;
			}
			try
			{
				Tokenizer.cacheLock.AcquireReaderLock(1000);
				if (Tokenizer.guidWordBreakerMapping.ContainsKey(guid))
				{
					return Tokenizer.guidWordBreakerMapping[guid];
				}
				Tokenizer.cacheLock.UpgradeToWriterLock(1000);
				if (Tokenizer.guidWordBreakerMapping.ContainsKey(guid))
				{
					return Tokenizer.guidWordBreakerMapping[guid];
				}
				int num = NativeMethods.CoCreateInstance(guid, null, 1U, Tokenizer.WordBreakerInterfaceGuid, out obj);
				if (num != 0)
				{
					if (guid != Tokenizer.NeutralWordBreakerGuid)
					{
						num = NativeMethods.CoCreateInstance(Tokenizer.NeutralWordBreakerGuid, null, 1U, Tokenizer.WordBreakerInterfaceGuid, out obj);
						if (num != 0)
						{
							ExTraceGlobals.CcGenericTracer.TraceError<string>((long)this.GetHashCode(), "Unable to load word breaker for: {0}", cultureInfo.Name);
							obj = null;
						}
					}
					else
					{
						ExTraceGlobals.CcGenericTracer.TraceError<string>((long)this.GetHashCode(), "Unable to load word breaker for: {0}", cultureInfo.Name);
						obj = null;
					}
				}
				bool flag = true;
				((IWordBreaker)obj).Init(true, 1024, out flag);
				Tokenizer.guidWordBreakerMapping[guid] = (IWordBreaker)obj;
			}
			catch (ApplicationException)
			{
				ExTraceGlobals.CcGenericTracer.TraceError((long)this.GetHashCode(), "Unable to acquire lock to access word breaker cache");
				return null;
			}
			finally
			{
				Tokenizer.cacheLock.ReleaseLock();
			}
			return obj as IWordBreaker;
		}

		// Token: 0x04004F91 RID: 20369
		private const int BufferSize = 1024;

		// Token: 0x04004F92 RID: 20370
		private static readonly Guid WordBreakerInterfaceGuid = new Guid("D53552C8-77E3-101A-B552-08002B33B0E6");

		// Token: 0x04004F93 RID: 20371
		private static readonly Guid GermanWordBreakerGuid = new Guid(2601050640U, 58651, 4557, 188, 127, 0, 170, 0, 61, 177, 142);

		// Token: 0x04004F94 RID: 20372
		private static readonly Guid EnglishBreatBritainWordBreakerGuid = new Guid(2158225840U, 41542, 4563, 187, 140, 0, 144, 39, 47, 163, 98);

		// Token: 0x04004F95 RID: 20373
		private static readonly Guid EnglishUSWordBreakerGuid = new Guid(2158225840U, 41542, 4563, 187, 140, 0, 144, 39, 47, 163, 98);

		// Token: 0x04004F96 RID: 20374
		private static readonly Guid SpanishWordBreakerGuid = new Guid(42317248, 4807, 4558, 189, 49, 0, 170, 0, 75, 187, 31);

		// Token: 0x04004F97 RID: 20375
		private static readonly Guid FrenchWordBreakerGuid = new Guid(1507891272U, 32921, 4123, 141, 243, 0, 0, 11, 101, 195, 181);

		// Token: 0x04004F98 RID: 20376
		private static readonly Guid ItalianWordBreakerGuid = new Guid(4253464016U, 4806, 4558, 189, 49, 0, 170, 0, 75, 187, 31);

		// Token: 0x04004F99 RID: 20377
		private static readonly Guid DutchWordBreakerGuid = new Guid(1723035920U, 35826, 4558, 190, 89, 0, 170, 0, 81, 254, 32);

		// Token: 0x04004F9A RID: 20378
		private static readonly Guid SweidishWordBreakerGuid = new Guid(29799248, 4807, 4558, 189, 49, 0, 170, 0, 75, 187, 31);

		// Token: 0x04004F9B RID: 20379
		private static readonly Guid NeutralWordBreakerGuid = new Guid(915818464, 6064, 4558, 153, 80, 0, 170, 0, 75, 187, 31);

		// Token: 0x04004F9C RID: 20380
		private static Dictionary<string, Guid> languageWordBreakerGuidMapping;

		// Token: 0x04004F9D RID: 20381
		private static Dictionary<Guid, IWordBreaker> guidWordBreakerMapping = new Dictionary<Guid, IWordBreaker>();

		// Token: 0x04004F9E RID: 20382
		private static ReaderWriterLock cacheLock = new ReaderWriterLock();
	}
}
