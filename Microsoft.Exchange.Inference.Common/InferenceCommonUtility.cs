using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000033 RID: 51
	internal static class InferenceCommonUtility
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003596 File Offset: 0x00001796
		public static string ServerVersion
		{
			get
			{
				return "15.00.1497.010";
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000359D File Offset: 0x0000179D
		public static bool ConfigTryParseHelper<T>(IPipelineComponentConfig config, InferenceCommonUtility.TryParseFunction<T> tryParseFunction, string keyName, out T value, IDiagnosticsSession trace)
		{
			value = default(T);
			if (config == null || !tryParseFunction(config[keyName], out value))
			{
				if (trace != null)
				{
					trace.TraceDebug<string>("Failed to parse config value referenced by keyName: {0}.", keyName);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000035CD File Offset: 0x000017CD
		public static bool ConfigTryParseHelper<T>(IPipelineComponentConfig config, InferenceCommonUtility.TryParseFunction<T> tryParseFunction, string keyName, out T value, IDiagnosticsSession trace, T defaultValue)
		{
			if (!InferenceCommonUtility.ConfigTryParseHelper<T>(config, tryParseFunction, keyName, out value, trace))
			{
				value = defaultValue;
				if (trace != null)
				{
					trace.TraceDebug<string, T>("Defaulting config value referenced by keyName: {0} to: {1}", keyName, defaultValue);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000035FC File Offset: 0x000017FC
		public static string GetExecutablePath(string fullPathWithParameters)
		{
			string result = null;
			if (!string.IsNullOrWhiteSpace(fullPathWithParameters))
			{
				char c;
				if (fullPathWithParameters.StartsWith("\""))
				{
					c = '"';
				}
				else
				{
					c = ' ';
				}
				string[] array = fullPathWithParameters.Split(new char[]
				{
					c
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length > 0)
				{
					result = array[0];
				}
			}
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003648 File Offset: 0x00001848
		internal static bool IsNonUserMailbox(string mailboxName)
		{
			return !string.IsNullOrEmpty(mailboxName) && (mailboxName.StartsWith("SystemMailbox", StringComparison.OrdinalIgnoreCase) || mailboxName.StartsWith("HealthMailbox", StringComparison.OrdinalIgnoreCase) || mailboxName.StartsWith("DiscoverySearchMailbox", StringComparison.OrdinalIgnoreCase) || mailboxName.StartsWith("Migration.", StringComparison.OrdinalIgnoreCase) || mailboxName.StartsWith("OrganizationalWorkflow", StringComparison.OrdinalIgnoreCase) || mailboxName.Equals("Microsoft Exchange", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000036B4 File Offset: 0x000018B4
		public static bool MatchBulkHeader(string data)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(data))
			{
				result = Regex.IsMatch(data, "SRV:\\s?(SWL|BULK)");
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000036D8 File Offset: 0x000018D8
		public static string StringizeException(Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			int num = 0;
			while (ex != null && num < 5)
			{
				string text = string.Format("{0}:{1}:{2}", ex.GetType().FullName, ex.Message, ex.StackTrace);
				stringBuilder.Append(text.Replace("\n", "|").Replace("\r", "|").Replace("=", ":"));
				stringBuilder.Append("-----");
				num++;
				ex = ex.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003778 File Offset: 0x00001978
		public static string StringizeExceptions(List<Exception> exceptions)
		{
			if (exceptions == null || exceptions.Count == 0)
			{
				return string.Empty;
			}
			return string.Join("&", from ex in exceptions
			select InferenceCommonUtility.StringizeException(ex));
		}

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x060000F2 RID: 242
		public delegate bool TryParseFunction<T>(string s, out T t);
	}
}
