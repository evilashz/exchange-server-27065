using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000006 RID: 6
	internal class DatabaseInfo
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002273 File Offset: 0x00000473
		internal DatabaseInfo(IADDatabase db, AmDbStateInfo stateInfo)
		{
			this.Database = db;
			this.StateInfo = stateInfo;
			this.StoreStatus = new Dictionary<AmServerName, MdbStatusFlags?>();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002294 File Offset: 0x00000494
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000229C File Offset: 0x0000049C
		internal IADDatabase Database { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022A5 File Offset: 0x000004A5
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000022AD File Offset: 0x000004AD
		internal AmDbStateInfo StateInfo { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022B6 File Offset: 0x000004B6
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000022BE File Offset: 0x000004BE
		internal Dictionary<AmServerName, MdbStatusFlags?> StoreStatus { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022C7 File Offset: 0x000004C7
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022CF File Offset: 0x000004CF
		internal List<AmDbOperation> OperationsQueued { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022D8 File Offset: 0x000004D8
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022E0 File Offset: 0x000004E0
		internal AmServerName ActiveServer { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022E9 File Offset: 0x000004E9
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000022F1 File Offset: 0x000004F1
		internal bool IsMountedOnActive { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000022FA File Offset: 0x000004FA
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002302 File Offset: 0x00000502
		internal bool IsActiveOnDisabledServer { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000230B File Offset: 0x0000050B
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002313 File Offset: 0x00000513
		internal bool IsMismounted { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000231C File Offset: 0x0000051C
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002324 File Offset: 0x00000524
		internal bool IsMountedButAdminRequestedDismount { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000232D File Offset: 0x0000052D
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002335 File Offset: 0x00000535
		internal List<AmServerName> MisMountedServerList { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000233E File Offset: 0x0000053E
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002346 File Offset: 0x00000546
		internal AmServerName OwningServer { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000234F File Offset: 0x0000054F
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002357 File Offset: 0x00000557
		internal bool IsAdPropertiesOutOfSync { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002360 File Offset: 0x00000560
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002368 File Offset: 0x00000568
		internal bool IsClusterDatabaseOutOfSync { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002371 File Offset: 0x00000571
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002379 File Offset: 0x00000579
		internal bool IsPeriodicMountRequired { get; private set; }

		// Token: 0x0600003B RID: 59 RVA: 0x00002384 File Offset: 0x00000584
		internal void Analyze()
		{
			this.OwningServer = new AmServerName(this.Database.Server.Name);
			this.ActiveServer = this.StateInfo.ActiveServer;
			if (AmServerName.IsNullOrEmpty(this.ActiveServer))
			{
				this.ActiveServer = this.OwningServer;
			}
			if (!AmServerName.IsEqual(this.ActiveServer, this.OwningServer))
			{
				this.IsAdPropertiesOutOfSync = true;
			}
			if (this.Database.Servers.Length > 1 && AmBestCopySelectionHelper.IsActivationDisabled(this.ActiveServer))
			{
				this.IsActiveOnDisabledServer = true;
			}
			this.MisMountedServerList = new List<AmServerName>();
			foreach (AmServerName amServerName in this.StoreStatus.Keys)
			{
				if ((this.StoreStatus[amServerName] & MdbStatusFlags.Online) == MdbStatusFlags.Online)
				{
					if (AmServerName.IsEqual(amServerName, this.ActiveServer))
					{
						this.IsMountedOnActive = true;
					}
					else
					{
						this.MisMountedServerList.Add(amServerName);
					}
				}
				else if ((this.StoreStatus[amServerName] & MdbStatusFlags.MountInProgress) == MdbStatusFlags.MountInProgress && AmServerName.IsEqual(amServerName, this.ActiveServer))
				{
					this.IsMountedOnActive = true;
				}
			}
			this.IsMismounted = (this.MisMountedServerList != null && this.MisMountedServerList.Count > 0);
			this.IsMountedButAdminRequestedDismount = false;
			if (this.IsMountedOnActive)
			{
				if (this.StateInfo.IsAdminDismounted)
				{
					this.IsMountedButAdminRequestedDismount = true;
				}
				if (!this.StateInfo.IsMounted)
				{
					this.IsClusterDatabaseOutOfSync = true;
				}
			}
			else if (this.StateInfo.IsMounted)
			{
				this.IsClusterDatabaseOutOfSync = true;
			}
			this.IsPeriodicMountRequired = true;
			if (!this.IsMountedOnActive)
			{
				bool flag = AmSystemManager.Instance.StoreStateMarker.IsStoreGracefullyStoppedOn(this.ActiveServer);
				if (!this.Database.MountAtStartup || this.StateInfo.IsAdminDismounted || !this.StateInfo.IsMountAttemptedAtleastOnce || flag)
				{
					this.IsPeriodicMountRequired = false;
					ReplayCrimsonEvents.PeriodicCheckerSkippedMount.LogPeriodic<string, Guid, AmServerName, bool, bool, bool, bool>(this.Database.Guid, TimeSpan.FromMinutes(30.0), this.Database.Name, this.Database.Guid, this.ActiveServer, this.Database.MountAtStartup, this.StateInfo.IsAdminDismounted, this.StateInfo.IsMountAttemptedAtleastOnce, flag);
				}
			}
			else
			{
				this.IsPeriodicMountRequired = false;
			}
			if (this.StateInfo.IsMountSucceededAtleastOnce && !AmServerName.IsEqual(this.StateInfo.LastMountedServer, this.StateInfo.ActiveServer))
			{
				this.IsClusterDatabaseOutOfSync = true;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000269C File Offset: 0x0000089C
		internal bool IsActionsEqual(DatabaseInfo dbInfo)
		{
			return this.IsMismounted == dbInfo.IsMismounted && this.IsAdPropertiesOutOfSync == dbInfo.IsAdPropertiesOutOfSync && this.IsClusterDatabaseOutOfSync == dbInfo.IsClusterDatabaseOutOfSync && this.IsPeriodicMountRequired == dbInfo.IsPeriodicMountRequired && this.IsMountedOnActive == dbInfo.IsMountedOnActive && this.IsActiveOnDisabledServer == dbInfo.IsActiveOnDisabledServer && this.IsMountedButAdminRequestedDismount == dbInfo.IsMountedButAdminRequestedDismount;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000270E File Offset: 0x0000090E
		internal bool IsActiveOnServerAndReplicated(AmServerName serverName)
		{
			return this.Database.ReplicationType == ReplicationType.Remote && AmServerName.IsEqual(this.ActiveServer, serverName);
		}
	}
}
