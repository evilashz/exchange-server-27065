using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000026 RID: 38
	internal static class SmtpResponseParser
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00005930 File Offset: 0x00003B30
		public static bool Split(string text, out string[] result)
		{
			result = null;
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			if (text.Length < 3)
			{
				return false;
			}
			List<string> lines = SmtpResponseParser.SplitLines(text);
			return SmtpResponseParser.Split(lines, out result);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005963 File Offset: 0x00003B63
		public static bool Split(List<string> lines, out string[] result)
		{
			return SmtpResponseParser.ParseResponseArray(lines, out result);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000596C File Offset: 0x00003B6C
		public static bool IsValidStatusCode(string status)
		{
			return !string.IsNullOrEmpty(status) && status.Length >= 3 && (status.Length <= 3 || status[3] == '-' || status[3] == ' ') && (char.IsDigit(status[0]) && char.IsDigit(status[1])) && char.IsDigit(status[2]);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000059D8 File Offset: 0x00003BD8
		private static bool ParseResponseArray(List<string> lines, out string[] result)
		{
			result = null;
			if (lines == null || lines.Count == 0)
			{
				return false;
			}
			if (!SmtpResponseParser.IsValidStatusCode(lines[0]))
			{
				return false;
			}
			string text = lines[0].Substring(0, 3);
			EnhancedStatusCodeImpl enhancedStatusCodeImpl;
			string text2 = EnhancedStatusCodeImpl.TryParse(lines[0], 4, out enhancedStatusCodeImpl) ? enhancedStatusCodeImpl.Value : string.Empty;
			if (!SmtpResponseParser.ValidateLines(lines, text, ref text2))
			{
				return false;
			}
			int num = 4;
			if (!string.IsNullOrEmpty(text2))
			{
				num += text2.Length;
				if (num < lines[0].Length && lines[0][num] == ' ')
				{
					num++;
				}
			}
			List<string> list = new List<string>(lines.Count);
			for (int i = 0; i < lines.Count; i++)
			{
				string text3 = (lines[i].Length > num) ? lines[i].Substring(num) : string.Empty;
				if (!string.IsNullOrEmpty(text3))
				{
					list.Add(text3);
				}
			}
			if (list.Count == 0 && lines.Count > 1)
			{
				return false;
			}
			result = new string[list.Count + 2];
			result[0] = text;
			result[1] = text2;
			list.CopyTo(result, 2);
			return true;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005B0C File Offset: 0x00003D0C
		private static bool ValidateLines(List<string> lines, string statusCode, ref string enhancedStatusCode)
		{
			for (int i = 0; i < lines.Count; i++)
			{
				SmtpResponseParser.ValidationResult validationResult = SmtpResponseParser.ValidateLine(lines[i], statusCode, ref enhancedStatusCode);
				if (validationResult == SmtpResponseParser.ValidationResult.Error)
				{
					return false;
				}
				if (validationResult == SmtpResponseParser.ValidationResult.LastLine && i != lines.Count - 1)
				{
					return false;
				}
				if (validationResult == SmtpResponseParser.ValidationResult.HasMoreLines && i == lines.Count - 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005B64 File Offset: 0x00003D64
		private static SmtpResponseParser.ValidationResult ValidateLine(string line, string statusCode, ref string enhancedStatusCode)
		{
			bool flag = false;
			if (line.Length < 3)
			{
				return SmtpResponseParser.ValidationResult.Error;
			}
			if (string.CompareOrdinal(statusCode, 0, line, 0, 3) != 0)
			{
				return SmtpResponseParser.ValidationResult.Error;
			}
			if (line.Length == 3)
			{
				enhancedStatusCode = string.Empty;
				return SmtpResponseParser.ValidationResult.LastLine;
			}
			if (line[3] == ' ')
			{
				flag = true;
			}
			else if (line[3] != '-')
			{
				return SmtpResponseParser.ValidationResult.Error;
			}
			int num = 0;
			if (!string.IsNullOrEmpty(enhancedStatusCode))
			{
				EnhancedStatusCodeImpl enhancedStatusCodeImpl;
				string text = EnhancedStatusCodeImpl.TryParse(line, 4, out enhancedStatusCodeImpl) ? enhancedStatusCodeImpl.Value : null;
				if (string.IsNullOrEmpty(text) || !text.Equals(enhancedStatusCode, StringComparison.Ordinal))
				{
					enhancedStatusCode = string.Empty;
				}
				num = enhancedStatusCode.Length;
			}
			for (int i = 4 + num; i < line.Length; i++)
			{
				if (line[i] < '\u0001' || line[i] > '\u007f')
				{
					return SmtpResponseParser.ValidationResult.Error;
				}
			}
			if (!flag)
			{
				return SmtpResponseParser.ValidationResult.HasMoreLines;
			}
			return SmtpResponseParser.ValidationResult.LastLine;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005C34 File Offset: 0x00003E34
		private static List<string> SplitLines(string response)
		{
			List<string> list = new List<string>();
			int num = 0;
			int num2;
			while ((num2 = response.IndexOf("\r\n", num, StringComparison.Ordinal)) != -1)
			{
				list.Add(response.Substring(num, num2 - num));
				num = num2 + "\r\n".Length;
				if (num >= response.Length)
				{
					break;
				}
			}
			if (num < response.Length)
			{
				list.Add(response.Substring(num));
			}
			return list;
		}

		// Token: 0x0400013A RID: 314
		private const string CRLF = "\r\n";

		// Token: 0x02000027 RID: 39
		private enum ValidationResult
		{
			// Token: 0x0400013C RID: 316
			Error,
			// Token: 0x0400013D RID: 317
			LastLine,
			// Token: 0x0400013E RID: 318
			HasMoreLines
		}
	}
}
