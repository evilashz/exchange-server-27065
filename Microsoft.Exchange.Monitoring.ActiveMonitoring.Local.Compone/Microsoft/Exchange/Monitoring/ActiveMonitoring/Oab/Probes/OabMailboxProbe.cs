using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.OAB;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab.Probes
{
	// Token: 0x0200023E RID: 574
	public sealed class OabMailboxProbe : OabBaseLocalProbe
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x0006B610 File Offset: 0x00069810
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (this.ShouldRun())
			{
				try
				{
					HttpWebRequest request = base.GetRequest();
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "OabMailboxProbe:Sending request to OAB " + base.Definition.TargetResource, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabMailboxProbe.cs", 67);
					Task<WebResponse> task = base.WebRequestUtil.SendRequest(request);
					HttpWebResponse response = (HttpWebResponse)task.Result;
					OABManifest oabManifest = this.CheckManifestFile(response);
					this.VerifyOABFiles(oabManifest);
				}
				catch (AggregateException ex)
				{
					WebException ex2 = ex.Flatten().InnerException as WebException;
					HttpWebResponse httpWebResponse = (HttpWebResponse)ex2.Response;
					base.Result.StateAttribute5 = httpWebResponse.StatusCode.ToString();
					throw ex2;
				}
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0006B6E0 File Offset: 0x000698E0
		private static string InitializeOABFolderPath()
		{
			string text = null;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				text = (registryKey.GetValue("OABGeneratorPath") as string);
			}
			if (text == null || !Directory.Exists(text))
			{
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
				{
					text = (registryKey2.GetValue("MsiInstallPath") as string);
				}
				if (text == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						text = currentProcess.MainModule.FileName;
						text = Directory.GetParent(text).FullName;
						DirectoryInfo parent = Directory.GetParent(text);
						text = parent.FullName;
					}
				}
				text = Path.Combine(text, "ClientAccess\\OAB");
			}
			return text;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0006B7C8 File Offset: 0x000699C8
		private void VerifyOABFiles(OABManifest oabManifest)
		{
			string targetResource = base.Definition.TargetResource;
			foreach (OABManifestAddressList oabmanifestAddressList in oabManifest.AddressLists)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "Verifying OAB files for " + oabmanifestAddressList.DN, null, "VerifyOABFiles", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabMailboxProbe.cs", 143);
				foreach (OABManifestFile oabmanifestFile in oabmanifestAddressList.Files)
				{
					string text = string.Format("{0}\\{1}\\{2}", OabMailboxProbe.oabFolderPath, targetResource, oabmanifestFile.FileName);
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "Verifying file " + text, null, "VerifyOABFiles", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabMailboxProbe.cs", 147);
					if (!File.Exists(text))
					{
						throw new FileNotFoundException(Strings.OabMailboxFileNotFound(text), text);
					}
				}
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0006B8B8 File Offset: 0x00069AB8
		private OABManifest CheckManifestFile(HttpWebResponse response)
		{
			string responseBody = this.GetResponseBody(response);
			if (responseBody.Length <= 0)
			{
				throw new InvalidDataException(Strings.OabMailboxManifestEmpty);
			}
			OABManifest oabmanifest = OABManifest.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(responseBody)));
			if (oabmanifest.AddressLists == null || oabmanifest.AddressLists.Length == 0)
			{
				throw new InvalidDataException(Strings.OabMailboxManifestAddressListEmpty(string.Empty));
			}
			foreach (OABManifestAddressList oabmanifestAddressList in oabmanifest.AddressLists)
			{
				if (oabmanifestAddressList.Files == null || oabmanifestAddressList.Files.Length == 0)
				{
					throw new InvalidDataException(Strings.OabMailboxManifestAddressListEmpty(oabmanifestAddressList.Name));
				}
			}
			return oabmanifest;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0006B96C File Offset: 0x00069B6C
		private string GetResponseBody(HttpWebResponse response)
		{
			base.Result.FailureContext = response.ResponseUri.ToString();
			string httpResponseBody = base.WebRequestUtil.GetHttpResponseBody(response);
			if (HttpStatusCode.OK != response.StatusCode)
			{
				string message = string.Format("{0}:{1}", response.StatusCode, httpResponseBody);
				throw new WebException(message);
			}
			string text = response.Headers["X-DiagInfo"];
			if (!string.IsNullOrEmpty(text))
			{
				base.Result.ExecutionContext = text;
			}
			return httpResponseBody;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0006B9F0 File Offset: 0x00069BF0
		private bool ShouldRun()
		{
			string text = base.Definition.Attributes["OrgMbxDBId"];
			string[] array = text.Split(new string[]
			{
				","
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string g in array)
			{
				Guid databaseGuid = new Guid(g);
				using (MailboxServerLocator mailboxServerLocator = MailboxServerLocator.CreateWithResourceForestFqdn(databaseGuid, null))
				{
					BackEndServer server = mailboxServerLocator.GetServer();
					if (server.Fqdn.Contains(Environment.MachineName))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000C0E RID: 3086
		public const string OabOrgMailboxDatabaseId = "OrgMbxDBId";

		// Token: 0x04000C0F RID: 3087
		public const string DatabaseListSeperator = ",";

		// Token: 0x04000C10 RID: 3088
		private const string OABDirectoryPartialPath = "ClientAccess\\OAB";

		// Token: 0x04000C11 RID: 3089
		private static readonly string oabFolderPath = OabMailboxProbe.InitializeOABFolderPath();
	}
}
