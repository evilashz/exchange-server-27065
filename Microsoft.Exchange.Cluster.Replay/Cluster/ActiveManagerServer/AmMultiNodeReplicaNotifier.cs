using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000057 RID: 87
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmMultiNodeReplicaNotifier : AmMultiNodeRpcMap
	{
		// Token: 0x060003CA RID: 970 RVA: 0x00014983 File Offset: 0x00012B83
		public AmMultiNodeReplicaNotifier(IADDatabase database, AmDbActionCode actionCode, bool isHighPriority) : base("AmMultiNodeReplicaNotifier")
		{
			this.Database = database;
			this.ActionCode = actionCode;
			this.IsHighPriority = isHighPriority;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000149A5 File Offset: 0x00012BA5
		// (set) Token: 0x060003CC RID: 972 RVA: 0x000149AD File Offset: 0x00012BAD
		private bool IsHighPriority { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000149B6 File Offset: 0x00012BB6
		// (set) Token: 0x060003CE RID: 974 RVA: 0x000149BE File Offset: 0x00012BBE
		private AmDbActionCode ActionCode { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000149C7 File Offset: 0x00012BC7
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x000149CF File Offset: 0x00012BCF
		private IADDatabase Database
		{
			get
			{
				return this.m_database;
			}
			set
			{
				this.m_database = value;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000149E0 File Offset: 0x00012BE0
		public void SendAllNotifications()
		{
			ThreadPool.QueueUserWorkItem(delegate(object stateNotUsed)
			{
				this.SendAllNotificationsInternal();
			});
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000149F4 File Offset: 0x00012BF4
		protected override Exception RunServerRpc(AmServerName node, out object result)
		{
			result = null;
			Exception result2 = null;
			try
			{
				Dependencies.ReplayRpcClientWrapper.NotifyChangedReplayConfiguration(node.Fqdn, this.Database.Guid, AmHelper.GetServerVersion(node), false, this.IsHighPriority, ReplayConfigChangeHints.AmMultiNodeReplicaNotifier);
			}
			catch (TransientException ex)
			{
				result2 = ex;
				AmTrace.Error("RunServerRpc(): Exception occurred: {0}", new object[]
				{
					ex
				});
			}
			catch (AmServerException ex2)
			{
				result2 = ex2;
				AmTrace.Error("RunServerRpc(): Exception occurred: {0}", new object[]
				{
					ex2
				});
			}
			catch (TaskServerException ex3)
			{
				result2 = ex3;
				AmTrace.Error("RunServerRpc(): Exception occurred: {0}", new object[]
				{
					ex3
				});
			}
			return result2;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00014AB8 File Offset: 0x00012CB8
		protected override void UpdateStatus(AmServerName node, object result)
		{
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00014ABC File Offset: 0x00012CBC
		private void SendAllNotificationsInternal()
		{
			try
			{
				if (!this.m_fInitialized)
				{
					List<AmServerName> nodeList = this.DetermineServersToContact();
					base.Initialize(nodeList);
					this.m_fInitialized = true;
				}
				base.RunAllRpcs();
			}
			catch (TransientException ex)
			{
				AmTrace.Error("SendAllNotificationsInternal(): Exception occurred: {0}", new object[]
				{
					ex
				});
			}
			catch (AmServerException ex2)
			{
				AmTrace.Error("SendAllNotificationsInternal(): Exception occurred: {0}", new object[]
				{
					ex2
				});
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00014B88 File Offset: 0x00012D88
		private List<AmServerName> DetermineServersToContact()
		{
			Guid guid = this.Database.Guid;
			IADDatabase db = this.Database;
			IADDatabaseCopy[] databaseCopies = AmBestCopySelectionHelper.GetDatabaseCopies(guid, ref db);
			if (db != null)
			{
				this.Database = db;
			}
			AmConfig amConfig = AmSystemManager.Instance.Config;
			if (amConfig.IsUnknown)
			{
				AmTrace.Error("AmMultiNodeRpcNotifier: DB {0}: Invalid AM configuration", new object[]
				{
					db.Name
				});
				throw new AmInvalidConfiguration(amConfig.LastError ?? string.Empty);
			}
			IAmBcsErrorLogger errorLogger = new AmBcsSingleCopyFailureLogger();
			AmBcsServerChecks checksToRun = AmBcsServerChecks.ClusterNodeUp;
			if (this.ActionCode.IsAutomaticOperation)
			{
				checksToRun |= AmBcsServerChecks.DebugOptionDisabled;
			}
			IEnumerable<AmServerName> source = from dbCopy in databaseCopies
			where this.ValidateServer(new AmServerName(dbCopy.HostServerName), db, amConfig, checksToRun, errorLogger)
			select new AmServerName(dbCopy.HostServerName);
			return source.ToList<AmServerName>();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00014CA0 File Offset: 0x00012EA0
		private bool ValidateServer(AmServerName serverName, IADDatabase db, AmConfig amConfig, AmBcsServerChecks checksToRun, IAmBcsErrorLogger errorLogger)
		{
			if (serverName.IsLocalComputerName)
			{
				return true;
			}
			LocalizedString empty = LocalizedString.Empty;
			AmBcsServerValidation amBcsServerValidation = new AmBcsServerValidation(serverName, null, db, amConfig, errorLogger, null);
			bool flag = amBcsServerValidation.RunChecks(checksToRun, ref empty);
			if (!flag)
			{
				AmTrace.Error("AmMultiNodeRpcNotifier: DB {0}: ValidateServer() returned error: {1}", new object[]
				{
					db.Name,
					empty
				});
			}
			return flag;
		}

		// Token: 0x040001B9 RID: 441
		private bool m_fInitialized;

		// Token: 0x040001BA RID: 442
		private IADDatabase m_database;
	}
}
