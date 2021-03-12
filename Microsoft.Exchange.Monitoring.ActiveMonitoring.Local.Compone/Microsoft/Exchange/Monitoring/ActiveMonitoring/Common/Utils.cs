using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Cryptography;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200012A RID: 298
	public static class Utils
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x000336AC File Offset: 0x000318AC
		public static string GetCertificateSubjectNameFromThumbprint(TracingContext traceContext, string certificateThumbprint)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, traceContext, string.Format("Finding Certificate with thumbprint {0} so that its SubjectCommonName can be determined.", certificateThumbprint), null, "GetCertificateSubjectNameFromThumbprint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\Utils.cs", 45);
			X509Certificate2 certificate = Utils.FindCertificate(traceContext, StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, certificateThumbprint);
			return CapiNativeMethods.GetCertNameInfo(certificate, 0U, CapiNativeMethods.CertNameType.Attr);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000336F0 File Offset: 0x000318F0
		public static X509Certificate2 FindCertificate(TracingContext traceContext, StoreLocation location, StoreName name, X509FindType findType, string findValue)
		{
			if (string.IsNullOrWhiteSpace(findValue))
			{
				throw new ArgumentException("Cannot be null or white-space characters.", "findValue");
			}
			X509Store x509Store = new X509Store(name, location);
			X509Certificate2 result;
			try
			{
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2 x509Certificate = TlsCertificateInfo.FindCertificate(x509Store, findType, findValue);
				if (x509Certificate == null)
				{
					WTFDiagnostics.TraceInformation<StoreLocation, StoreName, X509FindType, string>(ExTraceGlobals.CommonComponentsTracer, traceContext, "Unable to find a valid certificate using criteria '{0}', '{1}', '{2}', '{3}'.", location, name, findType, findValue, null, "FindCertificate", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\Utils.cs", 76);
					throw new Exception(string.Format("Unable to find a valid certificate using criteria '{0}', '{1}', '{2}', '{3}'.", new object[]
					{
						location,
						name,
						findType,
						findValue
					}));
				}
				result = x509Certificate;
			}
			finally
			{
				x509Store.Close();
			}
			return result;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000337A8 File Offset: 0x000319A8
		public static IPAddress GetLocalHostIPv4()
		{
			IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
			for (int i = 0; i < addressList.Length; i++)
			{
				if (addressList[i].AddressFamily.ToString() == "InterNetwork")
				{
					return addressList[i];
				}
			}
			throw new Exception("Unable to find IPv4 address for local host");
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000337FF File Offset: 0x000319FF
		public static string CheckNullOrWhiteSpace(string s, string elementName)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				throw new ArgumentException(string.Format("Work definition error -  attribute or element '{0}' missing or empty.", elementName));
			}
			return s;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0003381B File Offset: 0x00031A1B
		public static XmlNode CheckNode(XmlNode node, string elementName)
		{
			if (node == null)
			{
				throw new ArgumentException(string.Format("Work definition error - node '{0}' missing.", elementName));
			}
			return node;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00033834 File Offset: 0x00031A34
		public static XmlElement CheckXmlElement(XmlNode node, string elementName)
		{
			Utils.CheckNode(node, elementName);
			XmlElement xmlElement = node as XmlElement;
			if (xmlElement == null)
			{
				throw new ArgumentException(string.Format("Work definition error - node '{0}' is not an XML element.", elementName));
			}
			return xmlElement;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00033865 File Offset: 0x00031A65
		public static bool GetBoolean(string strValue, string elementName, bool defaultValue)
		{
			if (string.IsNullOrWhiteSpace(strValue))
			{
				return defaultValue;
			}
			return Utils.GetBoolean(strValue, elementName);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00033878 File Offset: 0x00031A78
		public static bool GetBoolean(string strValue, string elementName)
		{
			bool result;
			if (bool.TryParse(strValue, out result))
			{
				return result;
			}
			throw new ArgumentException(string.Format("Work definition error - the attribute or element '{0}' has an invalid value '{1}' of type '{2}'.", elementName, strValue, typeof(bool).Name));
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000338B4 File Offset: 0x00031AB4
		public static int LoadAppconfigIntSetting(string settingName, int defaultValue)
		{
			try
			{
				string text = ConfigurationManager.AppSettings[settingName];
				int result;
				if (text != null && int.TryParse(text, out result))
				{
					return result;
				}
			}
			catch (Exception)
			{
			}
			return defaultValue;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000338F8 File Offset: 0x00031AF8
		public static void Measure(string operationString, Microsoft.Exchange.Diagnostics.Trace tracer, TracingContext context, Action action)
		{
			bool flag = true;
			DateTime dateTime = DateTime.UtcNow.ToLocalTime();
			Stopwatch stopwatch = new Stopwatch();
			try
			{
				stopwatch.Start();
				action();
				flag = false;
			}
			finally
			{
				TimeSpan elapsed = stopwatch.Elapsed;
				WTFDiagnostics.TracePerformance(tracer, context, string.Format("Measured Operation: '{0}' Duration: {1} Started: {2} Ended: {3} IsUnhandled: {4}", new object[]
				{
					operationString,
					elapsed,
					dateTime,
					dateTime + elapsed,
					flag
				}), null, "Measure", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\Utils.cs", 263);
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000339A4 File Offset: 0x00031BA4
		public static void Measure<T>(Func<T> action, out T result, out TimeSpan duration)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				result = action();
			}
			finally
			{
				stopwatch.Stop();
				duration = stopwatch.Elapsed;
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000339E8 File Offset: 0x00031BE8
		public static string GetExchangeScriptsPath()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
			{
				if (registryKey == null)
				{
					result = string.Empty;
				}
				else
				{
					object value = registryKey.GetValue("MsiInstallPath");
					if (value == null)
					{
						result = string.Empty;
					}
					else
					{
						result = Path.Combine(value.ToString(), "Scripts");
					}
				}
			}
			return result;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00033A58 File Offset: 0x00031C58
		public static string GetExchangeLogPath()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
			{
				if (registryKey == null)
				{
					result = string.Empty;
				}
				else
				{
					object value = registryKey.GetValue("MsiInstallPath");
					if (value == null)
					{
						result = string.Empty;
					}
					else
					{
						result = Path.Combine(value.ToString(), "Logging");
					}
				}
			}
			return result;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00033AC8 File Offset: 0x00031CC8
		public static string GetExchangeBuildFromRegistry()
		{
			string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";
			string result = string.Empty;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				result = string.Format("{0:00}.{1:00}.{2:0000}.{3:000}", new object[]
				{
					(int)registryKey.GetValue("MsiProductMajor"),
					(int)registryKey.GetValue("MsiProductMinor"),
					(int)registryKey.GetValue("MsiBuildMajor"),
					(int)registryKey.GetValue("MsiBuildMinor")
				});
			}
			return result;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00033B80 File Offset: 0x00031D80
		public static bool EnableResponderForCurrentEnvironment(bool responderEnabledInConfig, bool responderEnabledForTest)
		{
			return responderEnabledInConfig && (!ExEnvironment.IsTest || responderEnabledForTest);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00033B94 File Offset: 0x00031D94
		public static string FormatPropertyNamesToString(string typeName, Dictionary<string, string> props)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (props != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in props)
				{
					stringBuilder.AppendLine(string.Format("{0} = {{{1}.{2}}}<br>", keyValuePair.Key, typeName, keyValuePair.Value));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00033C0C File Offset: 0x00031E0C
		public static string GetEspMonitorResultLink(MonitorDefinition monitor)
		{
			string text = Settings.IsProductionEnvironment ? "esp" : "sdf.esp";
			string arg = string.Format("http://{0}.outlook.com/#/Optics/Monitors/MonitorResultPage?Monitor={1}&SampleMask={2}&ID={{Monitor.WorkItemId}}&TargetInstance={3}", new object[]
			{
				text,
				monitor.Name,
				monitor.SampleMask,
				Settings.HostedServiceName
			});
			return string.Format("Active Monitoring Diagnostics <a href='{0}'>Click Here</a>", arg);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00033C6C File Offset: 0x00031E6C
		public static string GetOspOutsideInProbeResultLink(string probeName, string scope)
		{
			string text = Settings.IsProductionEnvironment ? "osp" : "ospbeta";
			ExDateTime exDateTime = ExDateTime.UtcNow.AddHours(1.0);
			return string.Format("https://{0}.outlook.com/ecp/osp/Health/ChartGardenPopUp.aspx?title=outlook&ProbeName={1}&Service=Exchange&StartDate={2:yyyy/MM/dd HH:mm:ss}&EndDate={3:yyyy/MM/dd HH:mm:ss}&HourCount={4}&scope={5}", new object[]
			{
				text,
				probeName,
				exDateTime.AddHours(-2.0),
				exDateTime,
				2,
				scope
			});
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00033CEC File Offset: 0x00031EEC
		public static string ObjectToXml(object obj)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				xmlSerializer.Serialize(stringWriter, obj);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00033D38 File Offset: 0x00031F38
		public static bool IsFlagSet(long configurationFlags, long testFlag)
		{
			return (configurationFlags & testFlag) == testFlag;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00033D40 File Offset: 0x00031F40
		public static string GetAccountForest(string account)
		{
			if (!string.IsNullOrEmpty(account))
			{
				Match match = Regex.Match(account, "@(?<forest>.[^.]+)E[0-9]+.", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					return match.Groups["forest"].Value;
				}
			}
			return string.Empty;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00033D88 File Offset: 0x00031F88
		internal static int CalculateMinimumRequiredServersFromFraction(int totalCafeServers, double fractionRequired, bool forceApplyFraction)
		{
			int num;
			if (forceApplyFraction)
			{
				if (fractionRequired < 0.0 || fractionRequired > 1.0)
				{
					throw new ArgumentException(string.Format("Input fraction '{0}' is outside of range 0.0 <= x <= 1.0", fractionRequired), "fraction");
				}
				num = (int)((double)(totalCafeServers + 1) * fractionRequired);
			}
			else
			{
				switch (totalCafeServers)
				{
				case 1:
					num = 0;
					break;
				case 2:
				case 3:
					num = 1;
					break;
				case 4:
					num = 2;
					break;
				case 5:
					num = 3;
					break;
				case 6:
					num = 4;
					break;
				default:
					num = (int)((double)(totalCafeServers + 1) * fractionRequired);
					break;
				}
			}
			if (num > totalCafeServers)
			{
				return totalCafeServers;
			}
			return num;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00033E1C File Offset: 0x0003201C
		internal static string ExtractServerNameFromFDQN(string fdqn)
		{
			if (!string.IsNullOrEmpty(fdqn))
			{
				int num = fdqn.IndexOf('.');
				if (num > 0)
				{
					return fdqn.Substring(0, num);
				}
			}
			return fdqn;
		}
	}
}
