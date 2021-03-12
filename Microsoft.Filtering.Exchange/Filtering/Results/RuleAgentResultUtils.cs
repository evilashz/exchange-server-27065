using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Filtering.Streams;

namespace Microsoft.Filtering.Results
{
	// Token: 0x02000019 RID: 25
	public class RuleAgentResultUtils
	{
		// Token: 0x0600005F RID: 95 RVA: 0x0000325D File Offset: 0x0000145D
		public static bool HasExceededProcessingLimit(StreamIdentity identity)
		{
			return identity.Notifications.Any((Notification n) => RuleAgentResultUtils.IsExceededProcessingLimitCode(n.Code));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003294 File Offset: 0x00001494
		public static bool IsUnsupported(StreamIdentity identity)
		{
			bool flag = identity.Content == null || !identity.Content.IsTextAvailable;
			bool flag2 = identity.Properties.ContainsKey("StreamIdentity.Leaf");
			if (!flag2)
			{
				return false;
			}
			if (!flag)
			{
				return identity.Notifications.Any((Notification n) => RuleAgentResultUtils.IsUnsupportedCode(n.Code));
			}
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000330B File Offset: 0x0000150B
		public static bool IsEncrypted(StreamIdentity identity)
		{
			return identity.Notifications.Any((Notification n) => RuleAgentResultUtils.IsEncryptedCode(n.Code));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003342 File Offset: 0x00001542
		public static bool HasPermanentError(StreamIdentity identity)
		{
			return identity.Notifications.Any((Notification n) => RuleAgentResultUtils.IsPermanentErrorCode(n.Code));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000336C File Offset: 0x0000156C
		public static void ValidateResults(FilteringResults results)
		{
			if (results.Streams.Any(new Func<StreamIdentity, bool>(RuleAgentResultUtils.HasPermanentError)))
			{
				throw new ResultsValidationException("A permanent text extraction error was encountered while getting attachment content", results);
			}
			foreach (ScanResult scanResult in results.ScanResults)
			{
				if (scanResult.ErrorInfo.RawReturnCode == -2147220991)
				{
					throw new ClassificationEngineInvalidOobConfigurationException("A permanent text extraction error was encountered while scanning. Scan engine failed to load OOB classifications", results);
				}
				if (scanResult.ErrorInfo.RawReturnCode == -2147220981)
				{
					throw new ClassificationEngineInvalidCustomConfigurationException("A permanent text extraction error was encountered while scanning. Scan engine failed to load custom classifications", results);
				}
			}
			if (ResultsExtensions.HasCategoryErrorForType(results, 16))
			{
				throw new ResultsValidationException("The Classification Engine encountered an error while scanning", results);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000034AC File Offset: 0x000016AC
		public static FilteringElapsedTimes CalculateElapsedTimes(FilteringResponse response)
		{
			FilteringResults results = response.Results;
			Func<string, long> func = (string key) => results.Streams.Sum(delegate(StreamIdentity si)
			{
				long result;
				if (!si.Properties.TryGetInt64(key, ref result))
				{
					return 0L;
				}
				return result;
			});
			FilteringElapsedTimes filteringElapsedTimes = new FilteringElapsedTimes();
			filteringElapsedTimes.Total = response.ElapsedTime;
			filteringElapsedTimes.Scanning = TimeSpan.FromMilliseconds((double)results.ScanResults.Sum((ScanResult sr) => sr.ElapsedTime));
			filteringElapsedTimes.Parsing = TimeSpan.FromMilliseconds((double)func("ScanningPipeline::ElapsedTimeKeys::Parsing"));
			filteringElapsedTimes.TextExtraction = TimeSpan.FromMilliseconds((double)func("ScanningPipeline::ElapsedTimeKeys::TextExtraction"));
			return filteringElapsedTimes;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003552 File Offset: 0x00001752
		public static StreamContent GetSubjectPrependedStreamContent(StreamIdentity identity)
		{
			return new StreamContent(RuleAgentResultUtils.GetSubjectPrependedStream(identity));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000355F File Offset: 0x0000175F
		public static TextReader GetSubjectPrependedReader(StreamIdentity identity)
		{
			return new StreamReader(RuleAgentResultUtils.GetSubjectPrependedStream(identity), Encoding.Unicode, false, 1024, true);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003578 File Offset: 0x00001778
		public static IDictionary<string, string> GetCustomProperties(StreamIdentity identity)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in identity.Properties.Keys)
			{
				if (text.StartsWith(RuleAgentResultUtils.customPropertiesPrefix))
				{
					dictionary.Add(text.Substring(RuleAgentResultUtils.customPropertiesPrefix.Length), identity.Properties.GetString(text));
				}
			}
			return dictionary;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035FC File Offset: 0x000017FC
		private static Stream GetSubjectPrependedStream(StreamIdentity identity)
		{
			if (identity.Properties.ContainsKey("Parsing::ParsingKeys::MessageBody"))
			{
				string subject = identity.Properties.ContainsKey("Parsing::ParsingKeys::TruncatedSubject") ? identity.Properties.GetString("Parsing::ParsingKeys::TruncatedSubject") : (identity.Properties.GetString("Parsing::ParsingKeys::Subject") + " ");
				return new SubjectPrependedStream(subject, identity.Content.TextReadStream);
			}
			return identity.Content.TextReadStream;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003678 File Offset: 0x00001878
		private static bool IsExceededProcessingLimitCode(NotificationCode code)
		{
			switch (code)
			{
			case 67141633:
			case 67141634:
			case 67141635:
			case 67141638:
			case 67141641:
				return true;
			}
			return false;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000036BC File Offset: 0x000018BC
		private static bool IsUnsupportedCode(NotificationCode code)
		{
			switch (code)
			{
			case -2080374783:
			case -2080374781:
				break;
			case -2080374782:
				return false;
			default:
				switch (code)
				{
				case 67141636:
				case 67141637:
				case 67141639:
				case 67141640:
					break;
				case 67141638:
					return false;
				default:
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003708 File Offset: 0x00001908
		private static bool IsPermanentErrorCode(NotificationCode code)
		{
			return -2080374782 == code;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003712 File Offset: 0x00001912
		private static bool IsEncryptedCode(NotificationCode code)
		{
			return 67141636 == code;
		}

		// Token: 0x0400003F RID: 63
		private static string customPropertiesPrefix = "CustomProperty::";
	}
}
