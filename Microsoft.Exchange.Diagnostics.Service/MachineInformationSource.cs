using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Timers;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000010 RID: 16
	public class MachineInformationSource : LogStreamSource
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000573C File Offset: 0x0000393C
		public MachineInformationSource(string sourceName, TimeSpan refreshInterval) : base(sourceName, new LogCsvSchema(), null, null, null)
		{
			if (refreshInterval.TotalMilliseconds < 0.0 || refreshInterval.TotalMilliseconds > 2147483647.0)
			{
				throw new ArgumentOutOfRangeException("refreshInterval");
			}
			this.refreshInterval = refreshInterval;
			StringBuilder stringBuilder = new StringBuilder(255);
			StringUtils.DoubleQuoteAndAppendColumn<string>(stringBuilder, "DateTime");
			MachineInformationSource.MachineInformation.Columns(stringBuilder);
			this.SourceHeader = stringBuilder.ToString();
			this.lastUpdated = DateTime.MinValue;
			this.refreshTimer = new Timer(1.0);
			this.refreshTimer.Elapsed += this.TimerEvent;
			this.refreshTimer.Start();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00005808 File Offset: 0x00003A08
		public string GetMachineInformationLine(DateTime timestamp, MachineInformationSource.MachineInformation machineInformation)
		{
			if (machineInformation == null)
			{
				throw new ArgumentNullException("machineInformation");
			}
			machineInformation.Validate();
			string text = machineInformation.ToString();
			if (this.lastLineOfData == null || !this.lastLineOfData.Equals(text, StringComparison.OrdinalIgnoreCase) || DateTime.UtcNow.Subtract(this.lastUpdated) > TimeSpan.FromHours(12.0))
			{
				this.lastUpdated = DateTime.UtcNow;
				this.lastLineOfData = text;
				StringBuilder stringBuilder = new StringBuilder(64);
				StringUtils.DoubleQuoteAndAppendColumn<string>(stringBuilder, DateTimeUtils.Floor(timestamp, this.refreshInterval).ToString("O"));
				return stringBuilder.ToString() + text;
			}
			return null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000058B8 File Offset: 0x00003AB8
		public MachineInformationSource.MachineInformation PollMachineInformation()
		{
			string text = Environment.MachineName;
			Version machineVersion = null;
			List<string> roleAndVersion = ServerRole.GetRoleAndVersion(out machineVersion);
			string text2 = string.Join(",", roleAndVersion.ToArray());
			string text3 = "Unknown";
			string text4 = "Unknown";
			bool flag = MachineInformationSource.IsWorkgroup();
			try
			{
				if (!flag)
				{
					using (Forest currentForest = Forest.GetCurrentForest())
					{
						text3 = currentForest.Name;
					}
					using (ActiveDirectorySite computerSite = ActiveDirectorySite.GetComputerSite())
					{
						text4 = computerSite.Name;
						goto IL_7D;
					}
				}
				text3 = "<Workgroup>";
				IL_7D:;
			}
			catch (Exception ex)
			{
				if (!MachineInformationSource.IsHandledException(ex))
				{
					throw;
				}
				Logger.LogErrorMessage("Unable to connect to the machine's Active Directory. Exception: {0}", new object[]
				{
					ex
				});
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ActiveDirectoryUnavailable, new object[]
				{
					ex
				});
			}
			string text5 = "true";
			text5 = MachineInformationSource.GetRegistryValue("InMaintenanceMode", text5);
			text = MachineInformationSource.GetRegistryValue("MachineName", text);
			text3 = MachineInformationSource.GetRegistryValue("ForestName", text3);
			text4 = MachineInformationSource.GetRegistryValue("SiteName", text4);
			text2 = MachineInformationSource.GetRegistryValue("RoleName", text2);
			int maintenanceStatus = Convert.ToBoolean(text5) ? 1 : 0;
			return new MachineInformationSource.MachineInformation(text, text3, text4, text2, maintenanceStatus, machineVersion);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00005A18 File Offset: 0x00003C18
		internal void TimerEvent(object state, ElapsedEventArgs args)
		{
			if (this.refreshTimer.Interval != this.refreshInterval.TotalMilliseconds)
			{
				this.refreshTimer.Interval = this.refreshInterval.TotalMilliseconds;
			}
			MachineInformationSource.MachineInformation machineInformation = this.PollMachineInformation();
			string machineInformationLine = this.GetMachineInformationLine(DateTime.UtcNow, machineInformation);
			if (machineInformationLine != null)
			{
				base.AddLineToStream(machineInformationLine);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005A77 File Offset: 0x00003C77
		protected override void DoDispose()
		{
			base.DoDispose();
			if (this.refreshTimer != null)
			{
				this.refreshTimer.Enabled = false;
				this.refreshTimer.Dispose();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005A9E File Offset: 0x00003C9E
		private static bool IsHandledException(Exception exception)
		{
			return exception is ActiveDirectoryObjectNotFoundException || exception is ActiveDirectoryOperationException || exception is ActiveDirectoryServerDownException || exception is AuthenticationException || exception is ArgumentException || exception is TargetInvocationException;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005AD4 File Offset: 0x00003CD4
		private static bool IsWorkgroup()
		{
			bool result = false;
			try
			{
				using (Domain.GetComputerDomain())
				{
				}
			}
			catch (ActiveDirectoryObjectNotFoundException)
			{
				result = true;
			}
			catch (Exception ex)
			{
				if (!MachineInformationSource.IsHandledException(ex))
				{
					throw;
				}
				Logger.LogErrorMessage("Unable to connect to the machine's Active Directory. Exception: {0}", new object[]
				{
					ex
				});
				Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_ActiveDirectoryUnavailable, new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00005B64 File Offset: 0x00003D64
		private static string GetRegistryValue(string valueName, string currentValue)
		{
			object obj;
			CommonUtils.TryGetRegistryValue(DiagnosticsService.DiagnosticsRegistryKey, valueName, string.Empty, out obj);
			string text = obj.ToString().Trim();
			if (text.Length <= 0)
			{
				return currentValue;
			}
			return text;
		}

		// Token: 0x04000039 RID: 57
		private readonly TimeSpan refreshInterval;

		// Token: 0x0400003A RID: 58
		private readonly Timer refreshTimer;

		// Token: 0x0400003B RID: 59
		private DateTime lastUpdated;

		// Token: 0x0400003C RID: 60
		private string lastLineOfData;

		// Token: 0x02000011 RID: 17
		public class MachineInformation
		{
			// Token: 0x0600005B RID: 91 RVA: 0x00005B9C File Offset: 0x00003D9C
			public MachineInformation(string machineName, string forestName, string siteName, string roleNames, int maintenanceStatus, Version machineVersion)
			{
				this.machineName = machineName;
				this.forestName = forestName;
				this.siteName = siteName;
				this.roleNames = roleNames;
				this.maintenanceStatus = maintenanceStatus;
				this.machineVersion = machineVersion;
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600005C RID: 92 RVA: 0x00005BD1 File Offset: 0x00003DD1
			public string MachineName
			{
				get
				{
					return this.machineName;
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600005D RID: 93 RVA: 0x00005BD9 File Offset: 0x00003DD9
			public string RoleNames
			{
				get
				{
					return this.roleNames;
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600005E RID: 94 RVA: 0x00005BE1 File Offset: 0x00003DE1
			public string ForestName
			{
				get
				{
					return this.forestName;
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00005BE9 File Offset: 0x00003DE9
			public string SiteName
			{
				get
				{
					return this.siteName;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00005BF1 File Offset: 0x00003DF1
			public int MaintenanceStatus
			{
				get
				{
					return this.maintenanceStatus;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000061 RID: 97 RVA: 0x00005BF9 File Offset: 0x00003DF9
			public Version MachineVersion
			{
				get
				{
					return this.machineVersion;
				}
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00005C04 File Offset: 0x00003E04
			public static MachineInformationSource.MachineInformation GetCurrent()
			{
				MachineInformationSource.MachineInformation result;
				using (MachineInformationSource machineInformationSource = new MachineInformationSource("currentMachineInformationSource", TimeSpan.FromHours(1.0)))
				{
					result = machineInformationSource.PollMachineInformation();
				}
				return result;
			}

			// Token: 0x06000063 RID: 99 RVA: 0x00005C50 File Offset: 0x00003E50
			public static void Columns(StringBuilder header)
			{
				StringUtils.DoubleQuoteAndAppendColumn<string>(header, "MachineName");
				StringUtils.DoubleQuoteAndAppendColumn<string>(header, "Role");
				StringUtils.DoubleQuoteAndAppendColumn<string>(header, "ForestName");
				StringUtils.DoubleQuoteAndAppendColumn<string>(header, "SiteName");
				StringUtils.DoubleQuoteAndAppendColumn<string>(header, "MaintenanceStatus");
				StringUtils.DoubleQuoteAndAppendColumn<string>(header, "ProductMajor");
				StringUtils.DoubleQuoteAndAppendColumn<string>(header, "ProductMinor");
				StringUtils.DoubleQuoteAndAppendColumn<string>(header, "BuildMajor");
				StringUtils.DoubleQuoteAndAppend<string>(header, "BuildMinor");
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00005CCC File Offset: 0x00003ECC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(255);
				StringUtils.DoubleQuoteAndAppendColumn<string>(stringBuilder, this.machineName);
				StringUtils.DoubleQuoteAndAppendColumn<string>(stringBuilder, this.roleNames);
				StringUtils.DoubleQuoteAndAppendColumn<string>(stringBuilder, this.forestName);
				StringUtils.DoubleQuoteAndAppendColumn<string>(stringBuilder, this.siteName);
				StringUtils.DoubleQuoteAndAppendColumn<int>(stringBuilder, this.maintenanceStatus);
				StringUtils.DoubleQuoteAndAppendColumn<int>(stringBuilder, this.machineVersion.Major);
				StringUtils.DoubleQuoteAndAppendColumn<int>(stringBuilder, this.machineVersion.Minor);
				StringUtils.DoubleQuoteAndAppendColumn<int>(stringBuilder, this.machineVersion.Build);
				StringUtils.DoubleQuoteAndAppend<int>(stringBuilder, this.machineVersion.Revision);
				return stringBuilder.ToString();
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00005D74 File Offset: 0x00003F74
			public void Validate()
			{
				if (string.IsNullOrEmpty(this.machineName))
				{
					throw new ArgumentNullException("machineName");
				}
				if (string.IsNullOrEmpty(this.roleNames))
				{
					throw new ArgumentNullException("roleName");
				}
				if (string.IsNullOrEmpty(this.forestName))
				{
					throw new ArgumentNullException("forestName");
				}
				if (string.IsNullOrEmpty(this.siteName))
				{
					throw new ArgumentNullException("siteName");
				}
				if (this.maintenanceStatus < 0 || this.maintenanceStatus > 1)
				{
					throw new ArgumentOutOfRangeException("maintenanceStatus");
				}
				if (this.machineVersion == null)
				{
					throw new ArgumentNullException("machineVersion");
				}
			}

			// Token: 0x0400003D RID: 61
			private readonly string machineName;

			// Token: 0x0400003E RID: 62
			private readonly string roleNames;

			// Token: 0x0400003F RID: 63
			private readonly string forestName;

			// Token: 0x04000040 RID: 64
			private readonly string siteName;

			// Token: 0x04000041 RID: 65
			private readonly int maintenanceStatus;

			// Token: 0x04000042 RID: 66
			private readonly Version machineVersion;
		}
	}
}
