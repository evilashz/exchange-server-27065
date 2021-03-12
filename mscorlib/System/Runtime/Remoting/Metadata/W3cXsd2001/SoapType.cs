using System;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007AF RID: 1967
	internal static class SoapType
	{
		// Token: 0x060055FB RID: 22011 RVA: 0x001301FC File Offset: 0x0012E3FC
		internal static string FilterBin64(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] != ' ' && value[i] != '\n' && value[i] != '\r')
				{
					stringBuilder.Append(value[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x00130258 File Offset: 0x0012E458
		internal static string LineFeedsBin64(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (i % 76 == 0)
				{
					stringBuilder.Append('\n');
				}
				stringBuilder.Append(value[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x001302A0 File Offset: 0x0012E4A0
		internal static string Escape(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = value.IndexOf('&');
			if (num > -1)
			{
				stringBuilder.Append(value);
				stringBuilder.Replace("&", "&#38;", num, stringBuilder.Length - num);
			}
			num = value.IndexOf('"');
			if (num > -1)
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Replace("\"", "&#34;", num, stringBuilder.Length - num);
			}
			num = value.IndexOf('\'');
			if (num > -1)
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Replace("'", "&#39;", num, stringBuilder.Length - num);
			}
			num = value.IndexOf('<');
			if (num > -1)
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Replace("<", "&#60;", num, stringBuilder.Length - num);
			}
			num = value.IndexOf('>');
			if (num > -1)
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Replace(">", "&#62;", num, stringBuilder.Length - num);
			}
			num = value.IndexOf('\0');
			if (num > -1)
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Replace('\0'.ToString(), "&#0;", num, stringBuilder.Length - num);
			}
			string result;
			if (stringBuilder.Length > 0)
			{
				result = stringBuilder.ToString();
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x0400272C RID: 10028
		internal static Type typeofSoapTime = typeof(SoapTime);

		// Token: 0x0400272D RID: 10029
		internal static Type typeofSoapDate = typeof(SoapDate);

		// Token: 0x0400272E RID: 10030
		internal static Type typeofSoapYearMonth = typeof(SoapYearMonth);

		// Token: 0x0400272F RID: 10031
		internal static Type typeofSoapYear = typeof(SoapYear);

		// Token: 0x04002730 RID: 10032
		internal static Type typeofSoapMonthDay = typeof(SoapMonthDay);

		// Token: 0x04002731 RID: 10033
		internal static Type typeofSoapDay = typeof(SoapDay);

		// Token: 0x04002732 RID: 10034
		internal static Type typeofSoapMonth = typeof(SoapMonth);

		// Token: 0x04002733 RID: 10035
		internal static Type typeofSoapHexBinary = typeof(SoapHexBinary);

		// Token: 0x04002734 RID: 10036
		internal static Type typeofSoapBase64Binary = typeof(SoapBase64Binary);

		// Token: 0x04002735 RID: 10037
		internal static Type typeofSoapInteger = typeof(SoapInteger);

		// Token: 0x04002736 RID: 10038
		internal static Type typeofSoapPositiveInteger = typeof(SoapPositiveInteger);

		// Token: 0x04002737 RID: 10039
		internal static Type typeofSoapNonPositiveInteger = typeof(SoapNonPositiveInteger);

		// Token: 0x04002738 RID: 10040
		internal static Type typeofSoapNonNegativeInteger = typeof(SoapNonNegativeInteger);

		// Token: 0x04002739 RID: 10041
		internal static Type typeofSoapNegativeInteger = typeof(SoapNegativeInteger);

		// Token: 0x0400273A RID: 10042
		internal static Type typeofSoapAnyUri = typeof(SoapAnyUri);

		// Token: 0x0400273B RID: 10043
		internal static Type typeofSoapQName = typeof(SoapQName);

		// Token: 0x0400273C RID: 10044
		internal static Type typeofSoapNotation = typeof(SoapNotation);

		// Token: 0x0400273D RID: 10045
		internal static Type typeofSoapNormalizedString = typeof(SoapNormalizedString);

		// Token: 0x0400273E RID: 10046
		internal static Type typeofSoapToken = typeof(SoapToken);

		// Token: 0x0400273F RID: 10047
		internal static Type typeofSoapLanguage = typeof(SoapLanguage);

		// Token: 0x04002740 RID: 10048
		internal static Type typeofSoapName = typeof(SoapName);

		// Token: 0x04002741 RID: 10049
		internal static Type typeofSoapIdrefs = typeof(SoapIdrefs);

		// Token: 0x04002742 RID: 10050
		internal static Type typeofSoapEntities = typeof(SoapEntities);

		// Token: 0x04002743 RID: 10051
		internal static Type typeofSoapNmtoken = typeof(SoapNmtoken);

		// Token: 0x04002744 RID: 10052
		internal static Type typeofSoapNmtokens = typeof(SoapNmtokens);

		// Token: 0x04002745 RID: 10053
		internal static Type typeofSoapNcName = typeof(SoapNcName);

		// Token: 0x04002746 RID: 10054
		internal static Type typeofSoapId = typeof(SoapId);

		// Token: 0x04002747 RID: 10055
		internal static Type typeofSoapIdref = typeof(SoapIdref);

		// Token: 0x04002748 RID: 10056
		internal static Type typeofSoapEntity = typeof(SoapEntity);

		// Token: 0x04002749 RID: 10057
		internal static Type typeofISoapXsd = typeof(ISoapXsd);
	}
}
