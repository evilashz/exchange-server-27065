using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000229 RID: 553
	public class MigrationHelper
	{
		// Token: 0x0600279F RID: 10143 RVA: 0x0007C9F8 File Offset: 0x0007ABF8
		public static byte[] ToMigrationCsv(object input, string migrationType)
		{
			if (input != null)
			{
				string[] value = Array.ConvertAll<object, string>((object[])input, (object x) => (string)x);
				string text = string.Join(Environment.NewLine, value);
				text = "EmailAddress" + Environment.NewLine + text;
				return Encoding.UTF8.GetBytes(text);
			}
			return null;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0007CA5C File Offset: 0x0007AC5C
		public static byte[] DecodeCsv(string base64String)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Stream stream = FileEncodeUploadHandler.DecodeContent(base64String))
				{
					stream.CopyTo(memoryStream);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x0007CAB8 File Offset: 0x0007ACB8
		public static Identity[] GetUserSmtpAddress()
		{
			SmtpAddress executingUserPrimarySmtpAddress = EacRbacPrincipal.Instance.ExecutingUserPrimarySmtpAddress;
			string name = EacRbacPrincipal.Instance.Name;
			if (!(executingUserPrimarySmtpAddress == SmtpAddress.Empty))
			{
				return new Identity[]
				{
					new Identity(executingUserPrimarySmtpAddress.ToString(), name)
				};
			}
			return null;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x0007CB08 File Offset: 0x0007AD08
		public static bool TestFlag(int flagValue, int flag)
		{
			return (flagValue & flag) == flag;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0007CB10 File Offset: 0x0007AD10
		public static string GetMigrationType(object endpoint)
		{
			if (endpoint is MigrationEndpoint)
			{
				MigrationType endpointType = ((MigrationEndpoint)endpoint).EndpointType;
				if (endpointType <= MigrationType.ExchangeOutlookAnywhere)
				{
					if (endpointType == MigrationType.IMAP)
					{
						return "IMAP";
					}
					if (endpointType == MigrationType.ExchangeOutlookAnywhere)
					{
						return "Staged";
					}
				}
				else
				{
					if (endpointType == MigrationType.ExchangeRemoteMove)
					{
						return "RemoteMove";
					}
					if (endpointType == MigrationType.ExchangeLocalMove)
					{
						return "LocalMove";
					}
					if (endpointType == MigrationType.PublicFolder)
					{
						return "PublicFolder";
					}
				}
				throw new Exception("Unexpected endpoint type " + ((MigrationEndpoint)endpoint).EndpointType.ToString());
			}
			return "LocalMove";
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0007CB9B File Offset: 0x0007AD9B
		public static string GetCurrentTimeString()
		{
			return ExDateTime.UtcNow.ToUserDateTimeString();
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x0007CBA8 File Offset: 0x0007ADA8
		public static DateTime LocalTimeStringToUniversalTime(object localTimeString)
		{
			ExDateTime? exDateTime = ((string)localTimeString).ToEcpExDateTime("yyyy/MM/dd HH:mm:ss");
			if (exDateTime != null)
			{
				return exDateTime.Value.UniversalTime;
			}
			return DateTime.UtcNow;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x0007CBE4 File Offset: 0x0007ADE4
		public static List<object> ToMigrationReportEntries(object reports)
		{
			ICollection collection = reports as ICollection;
			List<object> list = null;
			if (collection != null && collection.Count > 0)
			{
				list = new List<object>();
				foreach (object obj in collection)
				{
					MigrationReportSet migrationReportSet = (MigrationReportSet)obj;
					string creationTime = migrationReportSet.CreationTimeUTC.UtcToUserDateTimeString();
					if (!string.IsNullOrEmpty(migrationReportSet.SuccessUrl))
					{
						list.Add(new MigrationReportEntry(creationTime, migrationReportSet.SuccessUrl, Strings.Success));
					}
					if (!string.IsNullOrEmpty(migrationReportSet.ErrorUrl))
					{
						list.Add(new MigrationReportEntry(creationTime, migrationReportSet.ErrorUrl, Strings.Error));
					}
				}
			}
			return list;
		}
	}
}
