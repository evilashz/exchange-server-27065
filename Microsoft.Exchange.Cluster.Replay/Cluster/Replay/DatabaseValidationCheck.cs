using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000202 RID: 514
	internal abstract class DatabaseValidationCheck
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00051A1E File Offset: 0x0004FC1E
		protected static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00051A25 File Offset: 0x0004FC25
		protected DatabaseValidationCheck(DatabaseValidationCheck.ID checkId)
		{
			this.m_checkId = checkId;
			this.m_checkName = checkId.ToString();
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00051A45 File Offset: 0x0004FC45
		public DatabaseValidationCheck.ID CheckId
		{
			get
			{
				return this.m_checkId;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00051A4D File Offset: 0x0004FC4D
		public string CheckName
		{
			get
			{
				return this.m_checkName;
			}
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00051A58 File Offset: 0x0004FC58
		public DatabaseValidationCheck.Result Validate(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			DiagCore.RetailAssert(args.Status != null, "args.Status cannot be null!", new object[0]);
			DatabaseValidationCheck.Tracer.TraceDebug((long)this.GetHashCode(), "Check '{0}' is starting against database copy '{1}'. [ActiveServer: {2}, TargetServer: {3}, IsActive: {4}]", new object[]
			{
				this.CheckName,
				args.DatabaseCopyName,
				args.ActiveServer.NetbiosName,
				args.TargetServer.NetbiosName,
				args.Status.IsActive
			});
			if (!this.IsPrerequisiteMetForCheck(args))
			{
				DatabaseValidationCheck.Tracer.TraceError<string, string>((long)this.GetHashCode(), "Check '{0}' is skipping for database copy '{1}' because prereqs were not met. Returning Passed result.", this.CheckName, args.DatabaseCopyName);
				return DatabaseValidationCheck.Result.Passed;
			}
			DatabaseValidationCheck.Result result = this.ValidateInternal(args, ref error);
			if (result == DatabaseValidationCheck.Result.Passed)
			{
				DatabaseValidationCheck.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check '{0}' for database copy '{1}' Passed.", this.CheckName, args.DatabaseCopyName);
			}
			else
			{
				DatabaseValidationCheck.Tracer.TraceError((long)this.GetHashCode(), "Check '{0}' for database copy '{1}' returned result '{2}'. Error: {3}", new object[]
				{
					this.CheckName,
					args.DatabaseCopyName,
					result,
					error
				});
			}
			return result;
		}

		// Token: 0x06001445 RID: 5189
		protected abstract bool IsPrerequisiteMetForCheck(DatabaseValidationCheck.Arguments args);

		// Token: 0x06001446 RID: 5190
		protected abstract DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error);

		// Token: 0x040007C9 RID: 1993
		private readonly string m_checkName;

		// Token: 0x040007CA RID: 1994
		private readonly DatabaseValidationCheck.ID m_checkId;

		// Token: 0x02000203 RID: 515
		internal enum Result
		{
			// Token: 0x040007CC RID: 1996
			Passed,
			// Token: 0x040007CD RID: 1997
			Warning,
			// Token: 0x040007CE RID: 1998
			Failed
		}

		// Token: 0x02000204 RID: 516
		internal enum ID
		{
			// Token: 0x040007D0 RID: 2000
			DatabaseCheckCopyStatusRpcSuccessful,
			// Token: 0x040007D1 RID: 2001
			DatabaseCheckClusterNodeUp,
			// Token: 0x040007D2 RID: 2002
			DatabaseCheckServerInMaintenanceMode,
			// Token: 0x040007D3 RID: 2003
			DatabaseCheckServerHasTooManyActives,
			// Token: 0x040007D4 RID: 2004
			DatabaseCheckServerAllowedForActivation,
			// Token: 0x040007D5 RID: 2005
			DatabaseCheckActivationDisfavored,
			// Token: 0x040007D6 RID: 2006
			DatabaseCheckActiveMountState,
			// Token: 0x040007D7 RID: 2007
			DatabaseCheckActiveCopyNotActivationSuspended,
			// Token: 0x040007D8 RID: 2008
			DatabaseCheckPassiveCopyStatusIsOkForAvailability,
			// Token: 0x040007D9 RID: 2009
			DatabaseCheckPassiveCopyStatusIsOkForRedundancy,
			// Token: 0x040007DA RID: 2010
			DatabaseCheckPassiveCopyTotalQueueLength,
			// Token: 0x040007DB RID: 2011
			DatabaseCheckPassiveCopyRealCopyQueueLength,
			// Token: 0x040007DC RID: 2012
			DatabaseCheckPassiveCopyInspectorQueueLength,
			// Token: 0x040007DD RID: 2013
			DatabaseCheckReplayServiceUpOnActiveCopy,
			// Token: 0x040007DE RID: 2014
			DatabaseCheckDatabaseIsReplicated,
			// Token: 0x040007DF RID: 2015
			DatabaseCheckCopyStatusNotStale,
			// Token: 0x040007E0 RID: 2016
			DatabaseCheckActiveConnected,
			// Token: 0x040007E1 RID: 2017
			DatabaseCheckPassiveConnected
		}

		// Token: 0x02000205 RID: 517
		internal class Arguments
		{
			// Token: 0x06001447 RID: 5191 RVA: 0x00051B88 File Offset: 0x0004FD88
			public Arguments(AmServerName targetServer, AmServerName activeServer, IADDatabase database, CopyStatusClientCachedEntry copyStatus, CopyStatusClientCachedEntry activeCopyStatus, ICopyStatusClientLookup statusLookup, IMonitoringADConfig adConfig, PropertyUpdateTracker propertyUpdateTracker = null, bool ignoreActivationDisfavored = true, bool isCopyRemoval = false, bool ignoreMaintenanceChecks = true, bool ignoreTooManyActivesCheck = true)
			{
				this.m_targetServer = targetServer;
				this.m_activeServer = activeServer;
				this.m_database = database;
				this.m_copyStatus = copyStatus;
				this.m_activeCopyStatus = activeCopyStatus;
				this.m_statusLookup = statusLookup;
				this.m_adConfig = adConfig;
				this.m_propertyUpdateTracker = propertyUpdateTracker;
				this.m_ignoreActivationDisfavored = ignoreActivationDisfavored;
				this.m_isCopyRemoval = isCopyRemoval;
				this.m_ignoreMaintenanceChecks = ignoreMaintenanceChecks;
				this.m_ignoreTooManyActivesCheck = ignoreTooManyActivesCheck;
				if (this.m_activeServer == null)
				{
					this.m_activeServer = AmServerName.Empty;
				}
				this.m_dbName = database.Name;
				this.m_dbCopyName = string.Format("{0}\\{1}", database.Name, targetServer.NetbiosName);
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06001448 RID: 5192 RVA: 0x00051C33 File Offset: 0x0004FE33
			public IMonitoringADConfig ADConfig
			{
				get
				{
					return this.m_adConfig;
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06001449 RID: 5193 RVA: 0x00051C3B File Offset: 0x0004FE3B
			public AmServerName TargetServer
			{
				get
				{
					return this.m_targetServer;
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x0600144A RID: 5194 RVA: 0x00051C43 File Offset: 0x0004FE43
			public AmServerName ActiveServer
			{
				get
				{
					return this.m_activeServer;
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x0600144B RID: 5195 RVA: 0x00051C4B File Offset: 0x0004FE4B
			public IADDatabase Database
			{
				get
				{
					return this.m_database;
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x0600144C RID: 5196 RVA: 0x00051C53 File Offset: 0x0004FE53
			public CopyStatusClientCachedEntry Status
			{
				get
				{
					return this.m_copyStatus;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x0600144D RID: 5197 RVA: 0x00051C5B File Offset: 0x0004FE5B
			public CopyStatusClientCachedEntry ActiveStatus
			{
				get
				{
					return this.m_activeCopyStatus;
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x0600144E RID: 5198 RVA: 0x00051C63 File Offset: 0x0004FE63
			public ICopyStatusClientLookup StatusLookup
			{
				get
				{
					return this.m_statusLookup;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x0600144F RID: 5199 RVA: 0x00051C6B File Offset: 0x0004FE6B
			public PropertyUpdateTracker PropertyUpdateTracker
			{
				get
				{
					return this.m_propertyUpdateTracker;
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x06001450 RID: 5200 RVA: 0x00051C73 File Offset: 0x0004FE73
			public bool IgnoreActivationDisfavored
			{
				get
				{
					return this.m_ignoreActivationDisfavored;
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x06001451 RID: 5201 RVA: 0x00051C7B File Offset: 0x0004FE7B
			public bool IsCopyRemoval
			{
				get
				{
					return this.m_isCopyRemoval;
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x06001452 RID: 5202 RVA: 0x00051C83 File Offset: 0x0004FE83
			public bool IgnoreMaintenanceChecks
			{
				get
				{
					return this.m_ignoreMaintenanceChecks;
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x06001453 RID: 5203 RVA: 0x00051C8B File Offset: 0x0004FE8B
			public bool IgnoreTooManyActivesCheck
			{
				get
				{
					return this.m_ignoreTooManyActivesCheck;
				}
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x06001454 RID: 5204 RVA: 0x00051C93 File Offset: 0x0004FE93
			public string DatabaseName
			{
				get
				{
					return this.m_dbName;
				}
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x06001455 RID: 5205 RVA: 0x00051C9B File Offset: 0x0004FE9B
			public string DatabaseCopyName
			{
				get
				{
					return this.m_dbCopyName;
				}
			}

			// Token: 0x040007E2 RID: 2018
			private readonly IMonitoringADConfig m_adConfig;

			// Token: 0x040007E3 RID: 2019
			private readonly AmServerName m_targetServer;

			// Token: 0x040007E4 RID: 2020
			private readonly AmServerName m_activeServer;

			// Token: 0x040007E5 RID: 2021
			private readonly IADDatabase m_database;

			// Token: 0x040007E6 RID: 2022
			private readonly CopyStatusClientCachedEntry m_copyStatus;

			// Token: 0x040007E7 RID: 2023
			private readonly CopyStatusClientCachedEntry m_activeCopyStatus;

			// Token: 0x040007E8 RID: 2024
			private readonly ICopyStatusClientLookup m_statusLookup;

			// Token: 0x040007E9 RID: 2025
			private readonly PropertyUpdateTracker m_propertyUpdateTracker;

			// Token: 0x040007EA RID: 2026
			private readonly string m_dbName;

			// Token: 0x040007EB RID: 2027
			private readonly string m_dbCopyName;

			// Token: 0x040007EC RID: 2028
			private readonly bool m_ignoreActivationDisfavored;

			// Token: 0x040007ED RID: 2029
			private readonly bool m_isCopyRemoval;

			// Token: 0x040007EE RID: 2030
			private readonly bool m_ignoreMaintenanceChecks;

			// Token: 0x040007EF RID: 2031
			private readonly bool m_ignoreTooManyActivesCheck;
		}
	}
}
