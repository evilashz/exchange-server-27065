using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000106 RID: 262
	public class DatabaseCopyConnectivity
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x0002F228 File Offset: 0x0002D428
		public DatabaseCopyConnectivity(Guid databaseGuid)
		{
			if (databaseGuid == Guid.Empty)
			{
				throw new ArgumentException("Guid of the database to be validated cannot be null or empty", "DatabaseGuid");
			}
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0002F276 File Offset: 0x0002D476
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0002F27E File Offset: 0x0002D47E
		public bool? IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0002F286 File Offset: 0x0002D486
		public LocalizedException Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0002F28E File Offset: 0x0002D48E
		public TimeSpan Latency
		{
			get
			{
				return this.latency;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0002F296 File Offset: 0x0002D496
		internal string DiagnosticContext
		{
			get
			{
				return this.diagnosticContext;
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002F2A0 File Offset: 0x0002D4A0
		public DatabaseCopyConnectivity.DatabaseAvailable Validate(Guid systemMailboxGuid, bool activeDatabase)
		{
			if (systemMailboxGuid == Guid.Empty)
			{
				throw new ArgumentException("Guid of the system mailbox to use for database availability validation cannot be null or empty", "systemMailboxGuid");
			}
			if (DirectoryAccessor.Instance == null)
			{
				throw new ArgumentException("DirectoryAccessor instance cannot be null", "DirectoryAccessor.Instance");
			}
			this.isActive = new bool?(DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(this.DatabaseGuid));
			if (this.isActive == null)
			{
				throw new UnableToGetDatabaseStateException(this.databaseGuid.ToString());
			}
			if (activeDatabase != this.isActive.Value)
			{
				return DatabaseCopyConnectivity.DatabaseAvailable.NoOp;
			}
			DatabaseLocationInfo databaseLocationInfo = new DatabaseLocationInfo(LocalServer.GetServer(), false);
			ExchangePrincipal exPrincipal = ExchangePrincipal.FromMailboxData(systemMailboxGuid, this.DatabaseGuid, null, new CultureInfo[]
			{
				CultureInfo.CurrentCulture
			}, RemotingOptions.LocalConnectionsOnly, databaseLocationInfo);
			if (!this.isActive.Value && !this.IsDatabaseHealthyHAPerspective())
			{
				return DatabaseCopyConnectivity.DatabaseAvailable.PassiveUnhealthyHA;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			DatabaseCopyConnectivity.DatabaseAvailable databaseAvailable = this.ValidateDatabaseCopy(exPrincipal);
			stopwatch.Stop();
			this.latency = stopwatch.Elapsed;
			if (!this.isActive.Value && databaseAvailable == DatabaseCopyConnectivity.DatabaseAvailable.Failure && !this.IsDatabaseAttachedReadOnly())
			{
				return DatabaseCopyConnectivity.DatabaseAvailable.PassiveDatabaseNotAttached;
			}
			if (this.exception != null)
			{
				this.diagnosticContext = this.GetDiagnosticContext(this.exception);
			}
			return databaseAvailable;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002F3D0 File Offset: 0x0002D5D0
		private DatabaseCopyConnectivity.DatabaseAvailable ValidateDatabaseCopy(ExchangePrincipal exPrincipal)
		{
			try
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(exPrincipal, CultureInfo.InvariantCulture, "Client=StoreActiveMonitoring", true, false, !this.isActive.Value))
				{
					using (Folder.Bind(mailboxSession, DefaultFolderType.Inbox, new PropertyDefinition[]
					{
						FolderSchema.ItemCount
					}))
					{
					}
				}
			}
			catch (MailboxOfflineException ex)
			{
				this.exception = ex;
				return DatabaseCopyConnectivity.DatabaseAvailable.Failure;
			}
			catch (MailboxInSiteFailoverException ex2)
			{
				this.exception = ex2;
				return DatabaseCopyConnectivity.DatabaseAvailable.Failure;
			}
			catch (StorageTransientException ex3)
			{
				this.exception = ex3;
				return DatabaseCopyConnectivity.DatabaseAvailable.Failure;
			}
			catch (StoragePermanentException ex4)
			{
				this.exception = ex4;
				return DatabaseCopyConnectivity.DatabaseAvailable.Failure;
			}
			return DatabaseCopyConnectivity.DatabaseAvailable.Success;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002F4BC File Offset: 0x0002D6BC
		private string GetDiagnosticContext(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			string result = string.Empty;
			Exception innerException = exception.InnerException;
			MapiPermanentException ex = innerException as MapiPermanentException;
			MapiRetryableException ex2 = innerException as MapiRetryableException;
			if (ex != null)
			{
				result = ex.DiagCtx.ToCompactString();
			}
			else if (ex2 != null)
			{
				result = ex2.DiagCtx.ToCompactString();
			}
			return result;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002F514 File Offset: 0x0002D714
		private bool IsDatabaseHealthyHAPerspective()
		{
			Exception ex;
			CopyStatusClientCachedEntry[] copyStatus = CopyStatusHelper.GetCopyStatus(AmServerName.LocalComputerName, RpcGetDatabaseCopyStatusFlags2.None, new Guid[]
			{
				this.databaseGuid
			}, 5000, null, out ex);
			if (ex != null)
			{
				this.exception = (LocalizedException)ex;
				return false;
			}
			if (copyStatus == null || copyStatus.Length != 1 || copyStatus[0].CopyStatus.CopyStatus != CopyStatusEnum.Healthy)
			{
				this.exception = new HAPassiveCopyUnhealthyException(copyStatus[0].CopyStatus.CopyStatus.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002F5A0 File Offset: 0x0002D7A0
		private bool IsDatabaseAttachedReadOnly()
		{
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=StoreActiveMonitoring", Environment.MachineName, null, null, null))
			{
				MdbStatus[] array = exRpcAdmin.ListMdbStatus(new Guid[]
				{
					this.databaseGuid
				});
				if (array != null && array.Length > 0 && (array[0].Status & MdbStatusFlags.AttachedReadOnly) != MdbStatusFlags.AttachedReadOnly)
				{
					this.exception = new DatabaseNotAttachedReadOnlyException();
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000564 RID: 1380
		private readonly Guid databaseGuid;

		// Token: 0x04000565 RID: 1381
		private bool? isActive = null;

		// Token: 0x04000566 RID: 1382
		private LocalizedException exception;

		// Token: 0x04000567 RID: 1383
		private TimeSpan latency = TimeSpan.Zero;

		// Token: 0x04000568 RID: 1384
		private string diagnosticContext;

		// Token: 0x02000107 RID: 263
		public enum DatabaseAvailable
		{
			// Token: 0x0400056A RID: 1386
			Success,
			// Token: 0x0400056B RID: 1387
			Failure,
			// Token: 0x0400056C RID: 1388
			PassiveUnhealthyHA,
			// Token: 0x0400056D RID: 1389
			PassiveDatabaseNotAttached,
			// Token: 0x0400056E RID: 1390
			NoOp
		}
	}
}
