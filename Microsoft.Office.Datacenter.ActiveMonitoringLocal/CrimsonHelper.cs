using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000073 RID: 115
	public static class CrimsonHelper
	{
		// Token: 0x0600069C RID: 1692 RVA: 0x0001BA44 File Offset: 0x00019C44
		public static string GetChannelName<T>()
		{
			return string.Format("Microsoft-Exchange-ActiveMonitoring/{0}", typeof(T).Name);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001BA60 File Offset: 0x00019C60
		public static ResultSeverityLevel ConvertResultTypeToSeverityLevel(ResultType resultType)
		{
			ResultSeverityLevel result;
			switch (resultType)
			{
			case ResultType.Succeeded:
				result = ResultSeverityLevel.Informational;
				break;
			case ResultType.Failed:
				result = ResultSeverityLevel.Error;
				break;
			default:
				result = ResultSeverityLevel.Warning;
				break;
			}
			return result;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001BA8C File Offset: 0x00019C8C
		public static bool ParseIntStringAsBool(string strValue)
		{
			return int.Parse(strValue, CultureInfo.InvariantCulture) != 0;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001BAAC File Offset: 0x00019CAC
		public static CrimsonBookmarker ConstructBookmarker(string resultClassName, string serviceName)
		{
			if (string.IsNullOrEmpty(serviceName))
			{
				serviceName = "*";
			}
			return new CrimsonBookmarker(string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\V15\\ActiveMonitoring\\{1}\\{0}\\Bookmark", resultClassName, serviceName));
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001BADC File Offset: 0x00019CDC
		public static EventBookmark ReadBookmark(string resultClassName, string serviceName, string bookmarkName)
		{
			if (string.IsNullOrEmpty(bookmarkName))
			{
				bookmarkName = "Default";
			}
			CrimsonBookmarker crimsonBookmarker = CrimsonHelper.ConstructBookmarker(resultClassName, serviceName);
			return crimsonBookmarker.Read(bookmarkName);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001BB0C File Offset: 0x00019D0C
		public static void WriteBookmark(string resultClassName, string serviceName, string bookmarkName, EventBookmark bookmark)
		{
			if (string.IsNullOrEmpty(bookmarkName))
			{
				bookmarkName = "Default";
			}
			CrimsonBookmarker crimsonBookmarker = CrimsonHelper.ConstructBookmarker(resultClassName, serviceName);
			crimsonBookmarker.Write(bookmarkName, bookmark);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001BB38 File Offset: 0x00019D38
		public static void DeleteBookmark(string resultClassName, string serviceName, string bookmarkName)
		{
			if (string.IsNullOrEmpty(bookmarkName))
			{
				bookmarkName = "Default";
			}
			CrimsonBookmarker crimsonBookmarker = CrimsonHelper.ConstructBookmarker(resultClassName, serviceName);
			crimsonBookmarker.Delete(bookmarkName);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001BB64 File Offset: 0x00019D64
		public static string BuildXPathQueryString(string channelName, string serviceName, DateTime? startTime, DateTime? endTime, string propertyConstraint)
		{
			string text = string.Empty;
			if (startTime != null)
			{
				text = string.Format("@SystemTime&gt;='{0}'", startTime.Value.ToUniversalTime().ToString("o"));
			}
			string text2 = string.Empty;
			if (endTime != null)
			{
				text2 = string.Format("@SystemTime&lt;='{0}'", endTime.Value.ToUniversalTime().ToString("o"));
			}
			string text3 = null;
			if (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2))
			{
				text3 = string.Format("System[TimeCreated[{0}{1}{2}]]", text, (startTime != null && endTime != null) ? " and " : string.Empty, text2);
			}
			string str = string.Empty;
			if (!string.IsNullOrEmpty(text3) && (!string.IsNullOrEmpty(serviceName) || !string.IsNullOrEmpty(propertyConstraint)))
			{
				str = " and ";
			}
			string text4 = string.Empty;
			string arg = string.Empty;
			if (!string.IsNullOrEmpty(serviceName))
			{
				text4 = string.Format("(ServiceName='{0}')", serviceName);
				if (!string.IsNullOrEmpty(propertyConstraint))
				{
					arg = " and ";
				}
			}
			string str2 = string.Empty;
			if (!string.IsNullOrEmpty(text4) || !string.IsNullOrEmpty(propertyConstraint))
			{
				str2 = string.Format("UserData[EventXML[{0}{1}{2}]]", text4, arg, propertyConstraint);
			}
			string text5 = text3 + str + str2;
			string result = "*";
			if (!string.IsNullOrEmpty(text5))
			{
				string format = "\r\n                    <QueryList>\r\n                      <Query Id=\"0\" Path=\"{0}\">\r\n                        <Select Path=\"{0}\">\r\n                        *[{1}]\r\n                        </Select>\r\n                      </Query>\r\n                    </QueryList>";
				result = string.Format(format, channelName, text5);
			}
			return result;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		public static string NullDecode(string inputStr)
		{
			string result = inputStr;
			if (!string.IsNullOrEmpty(inputStr))
			{
				char c = "[null]"[0];
				if (inputStr[0] == c)
				{
					int num = inputStr.IndexOf("[null]");
					if (num != -1)
					{
						if (num == 0 && inputStr.Length == "[null]".Length)
						{
							result = null;
						}
						else
						{
							result = inputStr.Substring(1, inputStr.Length - 2);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001BD3C File Offset: 0x00019F3C
		public static string NullCode(string inputStr)
		{
			string result = inputStr;
			if (inputStr == null)
			{
				result = "[null]";
			}
			else if (!string.IsNullOrEmpty(inputStr))
			{
				char c = "[null]"[0];
				char c2 = "[null]"["[null]".Length - 1];
				if (inputStr[0] == c && inputStr.Contains("[null]"))
				{
					result = string.Format("{0}{1}{2}", c, inputStr, c2);
				}
			}
			return result;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001BDB4 File Offset: 0x00019FB4
		public static double ParseDouble(string input)
		{
			double result = 0.0;
			try
			{
				result = double.Parse(input, CultureInfo.InvariantCulture);
			}
			catch (FormatException ex)
			{
				string arg = string.Format("'{0}' not a valid double value. Defaulting to 0.0", input);
				WTFDiagnostics.TraceError<string, string>(WTFLog.DataAccess, TracingContext.Default, "[ParseError] : {0} {1}.", arg, ex.ToString(), null, "ParseDouble", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\CrimsonHelper.cs", 364);
			}
			return result;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001BE24 File Offset: 0x0001A024
		public static float ParseFloat(string input)
		{
			float result = 0f;
			try
			{
				result = float.Parse(input, CultureInfo.InvariantCulture);
			}
			catch (FormatException ex)
			{
				string arg = string.Format("'{1}' not a valid float value. Defaulting to 0", input);
				WTFDiagnostics.TraceError<string, string>(WTFLog.DataAccess, TracingContext.Default, "[ParseError] : {0} {1}.", arg, ex.ToString(), null, "ParseFloat", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\CrimsonHelper.cs", 386);
			}
			return result;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001BE90 File Offset: 0x0001A090
		public static void ClearCrimsonChannel<T>()
		{
			if (!NativeMethods.EvtClearLog(IntPtr.Zero, CrimsonHelper.GetChannelName<T>(), null, 0))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		public static string ConvertDictionaryToXml(Dictionary<string, string> properties)
		{
			string result = string.Empty;
			if (properties != null && properties.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				stringBuilder.AppendFormat("<Properties>\n", new object[0]);
				foreach (string text in properties.Keys)
				{
					string arg = SecurityElement.Escape(properties[text].ToString());
					stringBuilder.AppendFormat("   <{0}>{1}</{0}>", text, arg);
				}
				stringBuilder.AppendFormat("</Properties>", new object[0]);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001BF6C File Offset: 0x0001A16C
		public static Dictionary<string, string> ConvertXmlToDictionary(string customXml)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(customXml))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(customXml);
				using (XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Properties"))
				{
					if (elementsByTagName != null && elementsByTagName.Count > 0)
					{
						XmlNode xmlNode = elementsByTagName.Item(0);
						using (XmlNodeList childNodes = xmlNode.ChildNodes)
						{
							foreach (object obj in childNodes)
							{
								XmlNode xmlNode2 = (XmlNode)obj;
								dictionary.Add(xmlNode2.Name, xmlNode2.InnerText);
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001C050 File Offset: 0x0001A250
		public static string Serialize(Dictionary<string, object> tempResults, bool replaceFlag)
		{
			StringBuilder stringBuilder = new StringBuilder(tempResults.Count * 16);
			foreach (KeyValuePair<string, object> keyValuePair in tempResults)
			{
				string text = "[null]";
				if (keyValuePair.Value != null)
				{
					if (keyValuePair.Value is DateTime)
					{
						text = ((DateTime)keyValuePair.Value).ToString("o", CultureInfo.InvariantCulture);
					}
					else if (keyValuePair.Value is int)
					{
						text = ((int)keyValuePair.Value).ToString(CultureInfo.InvariantCulture);
					}
					else if (keyValuePair.Value is float)
					{
						text = ((float)keyValuePair.Value).ToString(CultureInfo.InvariantCulture);
					}
					else if (keyValuePair.Value is double)
					{
						text = ((double)keyValuePair.Value).ToString(CultureInfo.InvariantCulture);
					}
					else
					{
						text = keyValuePair.Value.ToString();
					}
					text = text.Replace('|', '_');
				}
				stringBuilder.Append(text);
				stringBuilder.Append('|');
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length--;
			}
			if (replaceFlag)
			{
				stringBuilder.Replace("\"", "\"\"");
				stringBuilder.Replace(Environment.NewLine, "\\\\r\\\\n");
				stringBuilder.Replace("\n", "\\\\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		public static string ClearResultString(string str)
		{
			StringBuilder stringBuilder = new StringBuilder(str);
			if (str.StartsWith("\""))
			{
				stringBuilder = stringBuilder.Remove(0, 1);
			}
			if (str.EndsWith("\""))
			{
				stringBuilder.Length--;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000445 RID: 1093
		private const string NullCodeString = "[null]";

		// Token: 0x04000446 RID: 1094
		private const string BookmarkBaseLocationFormat = "SOFTWARE\\Microsoft\\ExchangeServer\\V15\\ActiveMonitoring\\{1}\\{0}\\Bookmark";
	}
}
