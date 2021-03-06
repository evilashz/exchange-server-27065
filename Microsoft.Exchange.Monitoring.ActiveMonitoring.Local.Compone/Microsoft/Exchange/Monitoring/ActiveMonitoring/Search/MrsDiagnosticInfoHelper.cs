using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.ApplicationLogic;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search
{
	// Token: 0x0200048D RID: 1165
	public class MrsDiagnosticInfoHelper
	{
		// Token: 0x06001D73 RID: 7539 RVA: 0x000B0648 File Offset: 0x000AE848
		static MrsDiagnosticInfoHelper()
		{
			Array values = Enum.GetValues(typeof(MrsDiagnosticInfoHelper.Argument));
			foreach (object obj in values)
			{
				int num = (int)obj;
				MrsDiagnosticInfoHelper.Argument key = (MrsDiagnosticInfoHelper.Argument)num;
				MrsDiagnosticInfoHelper.diagnosticInfoCacheLocks.Add(key, new object());
				MrsDiagnosticInfoHelper.diagnosticInfoCacheTimeoutTime.Add(key, DateTime.MinValue);
			}
			MrsDiagnosticInfoHelper.argumentComponentDictionary.Add(MrsDiagnosticInfoHelper.Argument.Job, "MailboxReplicationService");
			MrsDiagnosticInfoHelper.argumentComponentDictionary.Add(MrsDiagnosticInfoHelper.Argument.Resource, "SystemWorkLoadManager");
			MrsDiagnosticInfoHelper.argumentComponentDictionary.Add(MrsDiagnosticInfoHelper.Argument.History, "SystemWorkLoadManager");
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000B07D8 File Offset: 0x000AE9D8
		internal static XmlDocument GetDiagnosticInfoWithCaching(MrsDiagnosticInfoHelper.Argument argument)
		{
			if (!MrsDiagnosticInfoHelper.diagnosticInfoCache.ContainsKey(argument) || DateTime.UtcNow > MrsDiagnosticInfoHelper.diagnosticInfoCacheTimeoutTime[argument])
			{
				lock (MrsDiagnosticInfoHelper.diagnosticInfoCacheLocks[argument])
				{
					if (!MrsDiagnosticInfoHelper.diagnosticInfoCache.ContainsKey(argument) || DateTime.UtcNow > MrsDiagnosticInfoHelper.diagnosticInfoCacheTimeoutTime[argument])
					{
						string diagnosticInfoXml = null;
						Exception ex = null;
						Action delegateGetDiagnosticInfo = delegate()
						{
							try
							{
								string componentName = null;
								MrsDiagnosticInfoHelper.argumentComponentDictionary.TryGetValue(argument, out componentName);
								diagnosticInfoXml = ProcessAccessManager.ClientRunProcessCommand(null, "msexchangemailboxreplication", componentName, argument.ToString(), false, true, null);
							}
							catch (Exception ex)
							{
								ex = ex;
							}
						};
						IAsyncResult asyncResult = delegateGetDiagnosticInfo.BeginInvoke(delegate(IAsyncResult r)
						{
							delegateGetDiagnosticInfo.EndInvoke(r);
						}, null);
						if (!asyncResult.AsyncWaitHandle.WaitOne(MrsDiagnosticInfoHelper.GetDiagnosticInfoCallTimeout))
						{
							ex = new TimeoutException();
						}
						if (ex != null)
						{
							MrsDiagnosticInfoHelper.diagnosticInfoCache[argument] = null;
							MrsDiagnosticInfoHelper.diagnosticInfoCacheTimeoutTime[argument] = DateTime.UtcNow + MrsDiagnosticInfoHelper.DiagnosticInfoCacheTimeout;
							throw ex;
						}
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(diagnosticInfoXml);
						MrsDiagnosticInfoHelper.diagnosticInfoCache[argument] = xmlDocument;
						MrsDiagnosticInfoHelper.diagnosticInfoCacheTimeoutTime[argument] = DateTime.UtcNow + MrsDiagnosticInfoHelper.DiagnosticInfoCacheTimeout;
					}
				}
			}
			return MrsDiagnosticInfoHelper.diagnosticInfoCache[argument];
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000B099C File Offset: 0x000AEB9C
		internal static XmlNodeList GetMoveJobs()
		{
			XmlDocument diagnosticInfoWithCaching = MrsDiagnosticInfoHelper.GetDiagnosticInfoWithCaching(MrsDiagnosticInfoHelper.Argument.Job);
			if (diagnosticInfoWithCaching == null)
			{
				return null;
			}
			return diagnosticInfoWithCaching.SelectNodes("/Diagnostics/Components/MailboxReplicationService/Jobs/Move");
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x000B09C4 File Offset: 0x000AEBC4
		internal static XmlNode GetResource(string resourceKey, string classification)
		{
			XmlDocument diagnosticInfoWithCaching = MrsDiagnosticInfoHelper.GetDiagnosticInfoWithCaching(MrsDiagnosticInfoHelper.Argument.Resource);
			if (diagnosticInfoWithCaching == null)
			{
				return null;
			}
			string xpath = string.Format("/Diagnostics/Components/SystemWorkloadManager/Resources/Resource[ResourceKey='{0}' and Classification='{1}']", resourceKey, classification);
			return diagnosticInfoWithCaching.SelectSingleNode(xpath);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x000B09F4 File Offset: 0x000AEBF4
		internal static XmlNodeList GetHistory(string resourceKey, string classification)
		{
			XmlDocument diagnosticInfoWithCaching = MrsDiagnosticInfoHelper.GetDiagnosticInfoWithCaching(MrsDiagnosticInfoHelper.Argument.History);
			if (diagnosticInfoWithCaching == null)
			{
				return null;
			}
			string xpath = string.Format("/Diagnostics/Components/SystemWorkloadManager/History/Entry[Type='Monitor' and ResourceKey='{0}' and Classification='{1}']", resourceKey, classification);
			return diagnosticInfoWithCaching.SelectNodes(xpath);
		}

		// Token: 0x04001468 RID: 5224
		private const string MailboxReplicationServiceProcess = "msexchangemailboxreplication";

		// Token: 0x04001469 RID: 5225
		internal static readonly TimeSpan GetDiagnosticInfoCallTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x0400146A RID: 5226
		private static readonly TimeSpan DiagnosticInfoCacheTimeout = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400146B RID: 5227
		private static Dictionary<MrsDiagnosticInfoHelper.Argument, XmlDocument> diagnosticInfoCache = new Dictionary<MrsDiagnosticInfoHelper.Argument, XmlDocument>();

		// Token: 0x0400146C RID: 5228
		private static Dictionary<MrsDiagnosticInfoHelper.Argument, DateTime> diagnosticInfoCacheTimeoutTime = new Dictionary<MrsDiagnosticInfoHelper.Argument, DateTime>();

		// Token: 0x0400146D RID: 5229
		private static Dictionary<MrsDiagnosticInfoHelper.Argument, object> diagnosticInfoCacheLocks = new Dictionary<MrsDiagnosticInfoHelper.Argument, object>();

		// Token: 0x0400146E RID: 5230
		private static Dictionary<MrsDiagnosticInfoHelper.Argument, string> argumentComponentDictionary = new Dictionary<MrsDiagnosticInfoHelper.Argument, string>();

		// Token: 0x0200048E RID: 1166
		internal enum Argument
		{
			// Token: 0x04001470 RID: 5232
			Resource,
			// Token: 0x04001471 RID: 5233
			Job,
			// Token: 0x04001472 RID: 5234
			History
		}
	}
}
