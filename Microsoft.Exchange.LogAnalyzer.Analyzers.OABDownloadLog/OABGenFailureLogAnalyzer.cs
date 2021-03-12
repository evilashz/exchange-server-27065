using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.LogAnalyzer.Extensions.OABDownloadLog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.LogAnalyzer.Analyzers.OABDownloadLog
{
	// Token: 0x02000002 RID: 2
	public sealed class OABGenFailureLogAnalyzer : OABDownloadLogAnalyzer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public OABGenFailureLogAnalyzer(IJob job) : base(job)
		{
			string name = base.GetType().Name;
			base.TimeUpdatePeriod = Configuration.GetConfigTimeSpan(name + "RecurrenceInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(5.0));
			this.monitoringInterval = Configuration.GetConfigInt(name + "MonitoringInterval", 5, 24, 24);
			this.lastRequestedTimeThreshold = Configuration.GetConfigInt(name + "LastRequestedTimeThreshold", 1, 14, 14);
			this.lastTouchedTimeThreshold = Configuration.GetConfigInt(name + "LastTouchedTimeThreshold", 1, 48, 8);
			this.noOfRequestsThreshold = Configuration.GetConfigInt(name + "NoOfRequestsThreshold", 1, 5, 3);
			this.staleRequestTimeThreshold = Configuration.GetConfigInt(name + "StaleRequestTimeThreshold", 1, 2, 2);
			this.tenantsOutOfSLA = new List<OABTenantInfo>();
			this.whiteListedTenants = new List<string>();
			this.GetConfigs("OABDownloadAnalyzerConfiguration.xml");
			this.whiteListedTenantStatus = new List<OrganizationStatus>
			{
				OrganizationStatus.Active,
				OrganizationStatus.Invalid
			};
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000220C File Offset: 0x0000040C
		public override void OnLogLine(OnLogLineSource source, OnLogLineArgs args)
		{
			OABDownloadLogLine oabdownloadLogLine = (OABDownloadLogLine)args.LogLine;
			string organization = oabdownloadLogLine.Organization;
			DateTime d;
			DateTime dateTime;
			if (this.IsCustomerScenario(organization) && DateTime.TryParse(oabdownloadLogLine.LastRequestedTime, out d) && DateTime.TryParse(oabdownloadLogLine.LastTouchedTime, out dateTime))
			{
				OABTenantInfo oabtenantInfo = this.tenantsOutOfSLA.Find((OABTenantInfo x) => x.Organization == organization);
				if (this.whiteListedTenantStatus.Contains(oabdownloadLogLine.OrgStatus) && d.AddDays((double)this.lastRequestedTimeThreshold) > DateTime.UtcNow && dateTime.AddHours((double)this.lastTouchedTimeThreshold) < DateTime.UtcNow && oabdownloadLogLine.Timestamp - d > TimeSpan.FromDays((double)this.staleRequestTimeThreshold) && !oabdownloadLogLine.IsAddressListDeleted)
				{
					if (oabtenantInfo == null)
					{
						oabtenantInfo = new OABTenantInfo(organization, oabdownloadLogLine, TimeSpan.FromHours((double)this.monitoringInterval));
						this.tenantsOutOfSLA.Add(oabtenantInfo);
					}
					oabtenantInfo.NoOfRequests++;
					return;
				}
				if (oabtenantInfo != null)
				{
					this.tenantsOutOfSLA.Remove(oabtenantInfo);
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000238C File Offset: 0x0000058C
		public override void OnTimeUpdate(OnTimeUpdateSource source, OnTimeUpdateArgs args)
		{
			DateTime currentTime = args.Timestamp;
			this.tenantsOutOfSLA.RemoveAll((OABTenantInfo x) => x.LogLine.Timestamp.AddHours((double)this.monitoringInterval) < currentTime);
			foreach (OABTenantInfo oabtenantInfo in this.tenantsOutOfSLA)
			{
				if (oabtenantInfo.IsAlert(currentTime, this.noOfRequestsThreshold, base.TimeUpdatePeriod))
				{
					TriggerHandler.Trigger(OABGenFailureLogAnalyzer.EventName.OABGenTenantOutOfSLA.ToString(), new object[]
					{
						oabtenantInfo.Organization,
						this.lastTouchedTimeThreshold,
						oabtenantInfo.LogLine.LastTouchedTime,
						oabtenantInfo.LogLine.LastRequestedTime
					});
					oabtenantInfo.LastAlertTime = currentTime;
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002480 File Offset: 0x00000680
		private static DateTime TryParseDateTime(string str, DateTime defaultValue)
		{
			DateTime result;
			if (DateTime.TryParse(str, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000024A4 File Offset: 0x000006A4
		private void GetConfigs(string config)
		{
			string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), config);
			if (!File.Exists(path))
			{
				throw new ArgumentException(string.Format("Couldn't find the {0} file in the current directory", config));
			}
			using (XmlReader xmlReader = XmlReader.Create(File.OpenRead(path)))
			{
				XDocument xdocument = XDocument.Load(xmlReader);
				this.whiteListedTenants = (from x in xdocument.Descendants("WhiteListedTenants").Descendants("Tenants")
				select x.Value).ToList<string>();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000255C File Offset: 0x0000075C
		private bool IsCustomerScenario(string organization)
		{
			if (organization == null)
			{
				return false;
			}
			foreach (string value in this.whiteListedTenants)
			{
				if (organization.Contains(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000001 RID: 1
		public const string ConfigurationFile = "OABDownloadAnalyzerConfiguration.xml";

		// Token: 0x04000002 RID: 2
		private readonly int monitoringInterval;

		// Token: 0x04000003 RID: 3
		private readonly int lastTouchedTimeThreshold;

		// Token: 0x04000004 RID: 4
		private readonly int lastRequestedTimeThreshold;

		// Token: 0x04000005 RID: 5
		private readonly int noOfRequestsThreshold;

		// Token: 0x04000006 RID: 6
		private readonly int staleRequestTimeThreshold;

		// Token: 0x04000007 RID: 7
		private List<string> whiteListedTenants;

		// Token: 0x04000008 RID: 8
		private List<OABTenantInfo> tenantsOutOfSLA;

		// Token: 0x04000009 RID: 9
		private List<OrganizationStatus> whiteListedTenantStatus;

		// Token: 0x02000003 RID: 3
		public enum EventName
		{
			// Token: 0x0400000C RID: 12
			OABGenTenantOutOfSLA
		}

		// Token: 0x02000004 RID: 4
		private static class ConfigurationElements
		{
			// Token: 0x0400000D RID: 13
			public const string WhiteListedTenants = "WhiteListedTenants";

			// Token: 0x0400000E RID: 14
			public const string Tenants = "Tenants";
		}
	}
}
