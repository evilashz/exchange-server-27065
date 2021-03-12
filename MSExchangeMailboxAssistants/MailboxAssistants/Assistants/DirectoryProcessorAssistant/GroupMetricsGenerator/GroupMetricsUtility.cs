using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001A9 RID: 425
	public static class GroupMetricsUtility
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00061E79 File Offset: 0x00060079
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x00061E80 File Offset: 0x00060080
		internal static ICollection<DirectoryProcessorMailboxData> Mailboxes { get; set; }

		// Token: 0x060010AF RID: 4271 RVA: 0x00061E88 File Offset: 0x00060088
		internal static ICollection<DirectoryProcessorMailboxData> GetMailboxesToGenerateGroupMetrics()
		{
			return GroupMetricsUtility.Mailboxes;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00061EDC File Offset: 0x000600DC
		public static bool LocalServerMustGenerateGroupMetrics()
		{
			GroupMetricsUtility.Tracer.TraceFunction(0L, "Entering LocalServerMustGenerateGroupMetrics");
			bool generate = false;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 119, "LocalServerMustGenerateGroupMetrics", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\DirectoryProcessor\\GroupMetricsGenerator\\GroupMetricsUtility.cs");
				Server dataObject = topologyConfigurationSession.FindLocalServer();
				MailboxServer mailboxServer = new MailboxServer(dataObject);
				generate = mailboxServer.ForceGroupMetricsGeneration;
			}, 3);
			if (adoperationResult.Succeeded)
			{
				GroupMetricsUtility.Tracer.TraceDebug<bool>(0L, "LocalServerMustGenerateGroupMetrics: {0}", generate);
				return generate;
			}
			GroupMetricsUtility.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToReadConfigurationFromAD, null, new object[]
			{
				adoperationResult.Exception.GetType().FullName,
				adoperationResult.Exception.Message
			});
			return false;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00061FC4 File Offset: 0x000601C4
		public static void RemoveGroupMetricsDirectory()
		{
			string path = GroupMetricsUtility.GetGroupMetricsDirectoryPath();
			GroupMetricsUtility.TryDiskOperation(delegate
			{
				Directory.Delete(path, true);
				GroupMetricsUtility.Tracer.TraceDebug<string>(0L, "RemoveDistributionDirectory succeeded for {0}", path);
			}, delegate(Exception exception)
			{
				GroupMetricsUtility.Tracer.TraceDebug<string>(0L, "RemoveGroupMetricsDirectory: Unable to remove directory: {0}", exception.Message);
			});
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0006209C File Offset: 0x0006029C
		public static bool CreateGroupMetricsDirectory()
		{
			string path = GroupMetricsUtility.GetGroupMetricsDirectoryPath();
			return GroupMetricsUtility.TryDiskOperation(delegate
			{
				if (GroupMetricsUtility.Fault == GroupMetricsFault.DistributionDirectoryCreationException)
				{
					throw new IOException("Fault Injection");
				}
				Directory.CreateDirectory(path);
				GroupMetricsUtility.Tracer.TraceDebug<string>(0L, "CreateGroupMetricsDirectory succeeded: {0}", path);
			}, delegate(Exception exception)
			{
				GroupMetricsUtility.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToCreateGroupMetricsDirectory, null, new object[]
				{
					path,
					exception.GetType().FullName,
					exception.Message
				});
			});
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000620D8 File Offset: 0x000602D8
		internal static string GetGroupMetricsDirectoryPath()
		{
			string exchangeDirectory = GroupMetricsUtility.GetExchangeDirectory();
			return Path.Combine(exchangeDirectory, "GroupMetrics");
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000620F8 File Offset: 0x000602F8
		internal static string GetTenantDirectoryPath(OrganizationId organizationId)
		{
			string groupMetricsDirectoryPath = GroupMetricsUtility.GetGroupMetricsDirectoryPath();
			return GroupMetricsUtility.AppendTenantSubdirectory(groupMetricsDirectoryPath, organizationId);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00062114 File Offset: 0x00060314
		internal static ulong GetHash(Guid guid)
		{
			byte[] bytes = guid.ToByteArray();
			return GroupMetricsUtility.GetHash(bytes);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00062130 File Offset: 0x00060330
		internal static ulong GetHash(byte[] bytes)
		{
			ulong result;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				byte[] array = sha256Cng.ComputeHash(bytes);
				ulong num = 0UL;
				for (int i = 0; i < 8; i++)
				{
					num <<= 8;
					num |= (ulong)array[i];
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000621F8 File Offset: 0x000603F8
		internal static void DeleteDirectory(string path)
		{
			GroupMetricsUtility.TryDiskOperation(delegate
			{
				Directory.Delete(path, true);
			}, delegate(Exception exception)
			{
				if (!(exception is DirectoryNotFoundException))
				{
					GroupMetricsUtility.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToRemoveDirectory, exception.GetType().FullName, new object[]
					{
						path,
						exception.GetType().FullName,
						exception.Message
					});
				}
			});
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00062230 File Offset: 0x00060430
		internal static string AppendTenantSubdirectory(string path, OrganizationId organizationId)
		{
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
				Guid objectGuid = organizationId.OrganizationalUnit.ObjectGuid;
				byte[] array = objectGuid.ToByteArray();
				string path2 = string.Format("{0:X2}\\{1:X2}\\{2}", array[0], array[1], objectGuid.ToString());
				path = Path.Combine(path, path2);
			}
			return path;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000622E8 File Offset: 0x000604E8
		internal static bool GroupMetricsEnabledByOrgConfig(OrganizationId organizationId)
		{
			bool enabled = false;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				CachedOrganizationConfiguration instance = CachedOrganizationConfiguration.GetInstance(organizationId, CachedOrganizationConfiguration.ConfigurationTypes.OrganizationConfiguration);
				Organization configuration = instance.OrganizationConfiguration.Configuration;
				enabled = configuration.MailTipsGroupMetricsEnabled;
				GroupMetricsUtility.Tracer.TraceDebug<OrganizationId, bool>(0L, "GroupMetricsEnabledByOrgConfig: OrgId {0} result {1}", organizationId, enabled);
			}, 3);
			if (adoperationResult.Succeeded)
			{
				return enabled;
			}
			GroupMetricsUtility.Tracer.TraceDebug<string, string>(0L, "GroupMetricsEnabledByOrgConfig failed: {0}, {1}", adoperationResult.Exception.GetType().FullName, adoperationResult.Exception.Message);
			GroupMetricsUtility.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToReadConfigurationFromAD, null, new object[]
			{
				adoperationResult.Exception.GetType().FullName,
				adoperationResult.Exception.Message
			});
			return false;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00062394 File Offset: 0x00060594
		internal static bool TryDiskOperation(Action operation, Action<Exception> exceptionHandler)
		{
			Exception ex2;
			try
			{
				operation();
				return true;
			}
			catch (UnauthorizedAccessException ex)
			{
				ex2 = ex;
			}
			catch (SecurityException ex3)
			{
				ex2 = ex3;
			}
			catch (IOException ex4)
			{
				ex2 = ex4;
			}
			catch (InvalidOperationException ex5)
			{
				ex2 = ex5;
			}
			if (ex2 != null)
			{
				exceptionHandler(ex2);
			}
			return false;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00062404 File Offset: 0x00060604
		private static string GetExchangeDirectory()
		{
			return ConfigurationContext.Setup.InstallPath;
		}

		// Token: 0x04000A84 RID: 2692
		internal const int RetryADLimit = 3;

		// Token: 0x04000A85 RID: 2693
		public const string GroupMetricsDirectoryName = "GroupMetrics";

		// Token: 0x04000A86 RID: 2694
		internal static readonly Trace Tracer = ExTraceGlobals.GroupMetricsTracer;

		// Token: 0x04000A87 RID: 2695
		internal static readonly string EventSource = "MSExchange MailTips";

		// Token: 0x04000A88 RID: 2696
		internal static readonly ExEventLog Logger = new ExEventLog(GroupMetricsUtility.Tracer.Category, GroupMetricsUtility.EventSource);

		// Token: 0x04000A89 RID: 2697
		internal static GroupMetricsFault Fault = GroupMetricsFault.None;
	}
}
