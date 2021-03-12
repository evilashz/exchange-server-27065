using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000064 RID: 100
	internal static class MigrationEndpointLog
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x0001CC4C File Offset: 0x0001AE4C
		public static void LogStatusEvent(MigrationEndpoint migrationObject, MigrationEndpointLog.EndpointState state)
		{
			MigrationEndpointLog.MigrationEndpointLogEntry migrationObject2 = new MigrationEndpointLog.MigrationEndpointLogEntry(migrationObject, state);
			MigrationEndpointLog.MigrationEndpointLogger.LogEvent(migrationObject2);
		}

		// Token: 0x02000065 RID: 101
		internal enum EndpointState
		{
			// Token: 0x04000220 RID: 544
			Created,
			// Token: 0x04000221 RID: 545
			Updated,
			// Token: 0x04000222 RID: 546
			Deleted
		}

		// Token: 0x02000066 RID: 102
		internal class MigrationEndpointLogEntry
		{
			// Token: 0x06000621 RID: 1569 RVA: 0x0001CC67 File Offset: 0x0001AE67
			public MigrationEndpointLogEntry(MigrationEndpoint endpoint, MigrationEndpointLog.EndpointState state)
			{
				this.Endpoint = endpoint;
				this.State = state;
			}

			// Token: 0x04000223 RID: 547
			public readonly MigrationEndpoint Endpoint;

			// Token: 0x04000224 RID: 548
			public readonly MigrationEndpointLog.EndpointState State;
		}

		// Token: 0x02000067 RID: 103
		private class MigrationEndpointLogger : MigrationObjectLog<MigrationEndpointLog.MigrationEndpointLogEntry, MigrationEndpointLog.MigrationEndpointLogSchema, MigrationEndpointLog.MigrationEndpointLogConfiguration>
		{
			// Token: 0x06000622 RID: 1570 RVA: 0x0001CC7D File Offset: 0x0001AE7D
			public static void LogEvent(MigrationEndpointLog.MigrationEndpointLogEntry migrationObject)
			{
				MigrationObjectLog<MigrationEndpointLog.MigrationEndpointLogEntry, MigrationEndpointLog.MigrationEndpointLogSchema, MigrationEndpointLog.MigrationEndpointLogConfiguration>.Write(migrationObject);
			}
		}

		// Token: 0x02000068 RID: 104
		private class MigrationEndpointLogSchema : ObjectLogSchema
		{
			// Token: 0x17000203 RID: 515
			// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001CC8D File Offset: 0x0001AE8D
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Migration";
				}
			}

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001CC94 File Offset: 0x0001AE94
			public override string LogType
			{
				get
				{
					return "Migration Endpoint";
				}
			}

			// Token: 0x04000225 RID: 549
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointGuid = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointGuid", (MigrationEndpointLog.MigrationEndpointLogEntry d) => d.Endpoint.Guid);

			// Token: 0x04000226 RID: 550
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointName = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointName", (MigrationEndpointLog.MigrationEndpointLogEntry d) => d.Endpoint.Identity.Id);

			// Token: 0x04000227 RID: 551
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointType = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointType", (MigrationEndpointLog.MigrationEndpointLogEntry d) => d.Endpoint.EndpointType);

			// Token: 0x04000228 RID: 552
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointRemoteServer = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointRemoteServer", delegate(MigrationEndpointLog.MigrationEndpointLogEntry d)
			{
				if (d.Endpoint.EndpointType != MigrationType.ExchangeOutlookAnywhere && d.Endpoint.EndpointType != MigrationType.PublicFolder)
				{
					return d.Endpoint.RemoteServer;
				}
				return d.Endpoint.RpcProxyServer;
			});

			// Token: 0x04000229 RID: 553
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointMailboxPermission = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointMailboxPermission", (MigrationEndpointLog.MigrationEndpointLogEntry d) => d.Endpoint.MailboxPermission);

			// Token: 0x0400022A RID: 554
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointMaxConcurrentMigrations = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointMaxConcurrentMigrations", (MigrationEndpointLog.MigrationEndpointLogEntry d) => d.Endpoint.MaxConcurrentMigrations);

			// Token: 0x0400022B RID: 555
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointMaxConcurrentIncrementalSyncs = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointMaxConcurrentIncrementalSyncs", (MigrationEndpointLog.MigrationEndpointLogEntry d) => d.Endpoint.MaxConcurrentIncrementalSyncs);

			// Token: 0x0400022C RID: 556
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointLastModifiedTime = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointLastModifiedTime", (MigrationEndpointLog.MigrationEndpointLogEntry d) => d.Endpoint.LastModifiedTime);

			// Token: 0x0400022D RID: 557
			public static readonly ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry> EndpointState = new ObjectLogSimplePropertyDefinition<MigrationEndpointLog.MigrationEndpointLogEntry>("EndpointState", (MigrationEndpointLog.MigrationEndpointLogEntry d) => d.State);
		}

		// Token: 0x02000069 RID: 105
		private class MigrationEndpointLogConfiguration : MigrationObjectLogConfiguration
		{
			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000631 RID: 1585 RVA: 0x0001CF01 File Offset: 0x0001B101
			public override string LoggingFolder
			{
				get
				{
					return base.LoggingFolder + "\\MigrationEndpoint";
				}
			}

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001CF13 File Offset: 0x0001B113
			public override long MaxLogDirSize
			{
				get
				{
					return ConfigBase<MigrationServiceConfigSchema>.GetConfig<long>("MigrationReportingEndpointMaxDirSizeKey");
				}
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001CF1F File Offset: 0x0001B11F
			public override long MaxLogFileSize
			{
				get
				{
					return ConfigBase<MigrationServiceConfigSchema>.GetConfig<long>("MigrationReportingEndpointMaxFileSize");
				}
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001CF2B File Offset: 0x0001B12B
			public override string LogComponentName
			{
				get
				{
					return "MigrationEndpointLog";
				}
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001CF32 File Offset: 0x0001B132
			public override string FilenamePrefix
			{
				get
				{
					return "MigrationEndpoint_Log_";
				}
			}
		}
	}
}
