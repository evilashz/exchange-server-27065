using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000011 RID: 17
	internal static class WinRMDataExchangeHelper
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003E5C File Offset: 0x0000205C
		internal static string Serialize(Dictionary<string, string> items)
		{
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				dataContractJsonSerializer.WriteObject(memoryStream, items);
				@string = Encoding.UTF8.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length);
			}
			return @string;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003EC0 File Offset: 0x000020C0
		internal static Dictionary<string, string> DeSerialize(string serializedString)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(serializedString);
			DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
			Dictionary<string, string> result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = (dataContractJsonSerializer.ReadObject(memoryStream) as Dictionary<string, string>);
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003F1C File Offset: 0x0000211C
		internal static string HydrateAuthenticationType(string originalAuthType, string serializedData)
		{
			return string.Format("{0};{1}", originalAuthType, serializedData);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003F2C File Offset: 0x0000212C
		internal static void DehydrateAuthenticationType(string authenticationType, out string realAuthenticationType, out string serializedData)
		{
			int num = authenticationType.IndexOf(';');
			realAuthenticationType = authenticationType.Substring(0, num);
			serializedData = authenticationType.Substring(num + 1, authenticationType.Length - num - 1);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003F64 File Offset: 0x00002164
		internal static bool IsExchangeDataUseAuthenticationType()
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.WinRMExchangeDataUseAuthenticationType.Enabled;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003F88 File Offset: 0x00002188
		internal static bool IsExchangeDataUseNamedPipe()
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.WinRMExchangeDataUseTypeNamedPipe.Enabled;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003FAC File Offset: 0x000021AC
		internal static string GetWinRMDataIdentity(string connectionUrl, string userName, string authenticationType)
		{
			string arg = string.Empty;
			if (connectionUrl != null)
			{
				try
				{
					arg = HttpUtility.UrlEncode(new Uri(connectionUrl).Query);
				}
				catch (Exception ex)
				{
					CoreLogger.TraceInformation("[WinRMDataExchangeHelper.GetWinRMDataIdentity]Error on parse connectionUrl {0}: {1}", new object[]
					{
						connectionUrl,
						ex.ToString()
					});
				}
			}
			return string.Format("<{0}><{1}><{2}>", arg, userName, authenticationType);
		}

		// Token: 0x04000042 RID: 66
		private static readonly ExEventLog RbacEventLogger = new ExEventLog(ExTraceGlobals.LogTracer.Category, "MSExchange RBAC");
	}
}
