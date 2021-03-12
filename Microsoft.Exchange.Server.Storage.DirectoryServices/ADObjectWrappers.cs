using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.DirectoryServices;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000013 RID: 19
	internal static class ADObjectWrappers
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000259D File Offset: 0x0000079D
		public static ADObjectWrappers.IConcreteFactory Factory
		{
			get
			{
				return ADObjectWrappers.hookableFactory.Value;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000025A9 File Offset: 0x000007A9
		public static ADObjectWrappers.IADSystemConfigurationSession CreateADSystemConfigurationSession(IExecutionContext context, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, string domainController)
		{
			return ADObjectWrappers.Factory.CreateADSystemConfigurationSession(context, consistencyMode, sessionSettings, domainController);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000025B9 File Offset: 0x000007B9
		public static ADObjectWrappers.IADRecipientSession CreateADRecipientSession(IExecutionContext context, ConsistencyMode consistencyMode, TenantHint tenantHint, string domainController, bool bypassSharedCache)
		{
			return ADObjectWrappers.Factory.CreateADRecipientSession(context, consistencyMode, tenantHint, domainController, bypassSharedCache);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000025CB File Offset: 0x000007CB
		public static ADObjectWrappers.IADOrganizationContainer GetOrganizationContainer(IExecutionContext context, OrganizationId organizationId, string domainController)
		{
			return ADObjectWrappers.Factory.GetOrganizationContainer(context, organizationId, domainController);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000025DA File Offset: 0x000007DA
		internal static IDisposable SetTestHook(ADObjectWrappers.IConcreteFactory factory)
		{
			return ADObjectWrappers.hookableFactory.SetTestHook(factory);
		}

		// Token: 0x04000014 RID: 20
		private static Hookable<ADObjectWrappers.IConcreteFactory> hookableFactory = Hookable<ADObjectWrappers.IConcreteFactory>.Create(true, new ADObjectWrappers.ADObjectWrapperFactory());

		// Token: 0x02000014 RID: 20
		public interface IConcreteFactory
		{
			// Token: 0x06000065 RID: 101
			ADObjectWrappers.IADSystemConfigurationSession CreateADSystemConfigurationSession(IExecutionContext context, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, string domainController);

			// Token: 0x06000066 RID: 102
			ADObjectWrappers.IADRecipientSession CreateADRecipientSession(IExecutionContext context, ConsistencyMode consistencyMode, TenantHint tenantHint, string domainController, bool bypassSharedCache);

			// Token: 0x06000067 RID: 103
			ADObjectWrappers.IADOrganizationContainer GetOrganizationContainer(IExecutionContext context, OrganizationId organizationId, string domainController);
		}

		// Token: 0x02000015 RID: 21
		public interface IADRecipientSession
		{
			// Token: 0x06000068 RID: 104
			ADRecipient FindByObjectGuid(IExecutionContext context, Guid guid);

			// Token: 0x06000069 RID: 105
			ADRecipient FindByObjectId(IExecutionContext context, Guid objectId);

			// Token: 0x0600006A RID: 106
			ADRecipient FindByExchangeGuidIncludingAlternate(IExecutionContext context, Guid exchangeGuid);

			// Token: 0x0600006B RID: 107
			ADRecipient FindByLegacyExchangeDN(IExecutionContext context, string legacyExchangeDN);

			// Token: 0x0600006C RID: 108
			SecurityDescriptor ReadSecurityDescriptor(IExecutionContext context, ADRecipient adRecipient);

			// Token: 0x0600006D RID: 109
			bool IsMemberOfDistributionList(IExecutionContext context, ADRecipient adRecipient, Guid distributionListObjectGuid);
		}

		// Token: 0x02000016 RID: 22
		public interface IADSystemConfigurationSession
		{
			// Token: 0x0600006E RID: 110
			ADObjectWrappers.IADServer FindLocalServer(IExecutionContext context);

			// Token: 0x0600006F RID: 111
			ADObjectWrappers.IADInformationStore FindLocalInformationStore(IExecutionContext context, ADObjectWrappers.IADServer adServer);

			// Token: 0x06000070 RID: 112
			ADObjectWrappers.IADMailboxDatabase FindDatabaseByGuid(IExecutionContext context, Guid databaseGuid);

			// Token: 0x06000071 RID: 113
			ADObjectWrappers.IADTransportConfigContainer FindTransportConfigContainer(IExecutionContext context);

			// Token: 0x06000072 RID: 114
			ADObjectWrappers.IADOrganizationContainer GetOrganizationContainer(IExecutionContext context);
		}

		// Token: 0x02000017 RID: 23
		public interface IADTransportConfigContainer
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000073 RID: 115
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> MaxSendSize { get; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000074 RID: 116
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> MaxReceiveSize { get; }
		}

		// Token: 0x02000018 RID: 24
		public interface IADOrganizationContainer
		{
			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000075 RID: 117
			Guid ObjectGuid { get; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000076 RID: 118
			Guid HierarchyMailboxGuid { get; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000077 RID: 119
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota { get; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000078 RID: 120
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota { get; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000079 RID: 121
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize { get; }
		}

		// Token: 0x02000019 RID: 25
		public interface IADServer
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600007A RID: 122
			ADObjectId Id { get; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600007B RID: 123
			Guid Guid { get; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600007C RID: 124
			string ExchangeLegacyDN { get; }

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x0600007D RID: 125
			string Fqdn { get; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600007E RID: 126
			LocalLongFullPath InstallPath { get; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600007F RID: 127
			ServerRole ServerRole { get; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000080 RID: 128
			int TotalDatabases { get; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000081 RID: 129
			int? MaxActiveDatabases { get; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000082 RID: 130
			long ContinuousReplicationMaxMemoryPerDatabase { get; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000083 RID: 131
			Microsoft.Exchange.Server.Storage.Common.ServerEditionType Edition { get; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000084 RID: 132
			bool IsDAGMember { get; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000085 RID: 133
			string Forest { get; }

			// Token: 0x06000086 RID: 134
			SecurityDescriptor ReadSecurityDescriptor(IExecutionContext context);
		}

		// Token: 0x0200001A RID: 26
		public interface IADInformationStore
		{
			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000087 RID: 135
			int? MaxRpcThreads { get; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000088 RID: 136
			int MaxRecoveryDatabases { get; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000089 RID: 137
			int MaxTotalDatabases { get; }

			// Token: 0x0600008A RID: 138
			void LoadDatabaseOptions(IExecutionContext context, DatabaseOptions options);
		}

		// Token: 0x0200001B RID: 27
		public interface IADMailboxDatabase
		{
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600008B RID: 139
			Guid Guid { get; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600008C RID: 140
			string Name { get; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x0600008D RID: 141
			Guid DagOrServerGuid { get; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x0600008E RID: 142
			string ExchangeLegacyDN { get; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x0600008F RID: 143
			string Description { get; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000090 RID: 144
			string ServerName { get; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x06000091 RID: 145
			bool Recovery { get; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x06000092 RID: 146
			bool CircularLoggingEnabled { get; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000093 RID: 147
			bool AllowFileRestore { get; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000094 RID: 148
			EdbFilePath EdbFilePath { get; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000095 RID: 149
			NonRootLocalLongFullPath LogFolderPath { get; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x06000096 RID: 150
			EnhancedTimeSpan EventHistoryRetentionPeriod { get; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x06000097 RID: 151
			EnhancedTimeSpan MailboxRetention { get; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x06000098 RID: 152
			string[] HostServerNames { get; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000099 RID: 153
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> IssueWarningQuota { get; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x0600009A RID: 154
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> ProhibitSendQuota { get; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600009B RID: 155
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota { get; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x0600009C RID: 156
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota { get; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600009D RID: 157
			Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> RecoverableItemsQuota { get; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600009E RID: 158
			int DataMoveReplicationConstraint { get; }

			// Token: 0x0600009F RID: 159
			SecurityDescriptor ReadSecurityDescriptor(IExecutionContext context);

			// Token: 0x060000A0 RID: 160
			void LoadDatabaseOptions(IExecutionContext context, DatabaseOptions options);
		}

		// Token: 0x0200001C RID: 28
		internal class ADObjectWrapperFactory : ADObjectWrappers.IConcreteFactory
		{
			// Token: 0x060000A1 RID: 161 RVA: 0x000025F9 File Offset: 0x000007F9
			public ADObjectWrappers.IADSystemConfigurationSession CreateADSystemConfigurationSession(IExecutionContext context, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, string domainController)
			{
				return new ADObjectWrappers.ADSystemConfigurationSession(context, consistencyMode, sessionSettings, domainController);
			}

			// Token: 0x060000A2 RID: 162 RVA: 0x00002605 File Offset: 0x00000805
			public ADObjectWrappers.IADRecipientSession CreateADRecipientSession(IExecutionContext context, ConsistencyMode consistencyMode, TenantHint tenantHint, string domainController, bool bypassSharedCache)
			{
				return new ADObjectWrappers.ADRecipientSession(context, consistencyMode, tenantHint, domainController, bypassSharedCache);
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x00002614 File Offset: 0x00000814
			public ADObjectWrappers.IADOrganizationContainer GetOrganizationContainer(IExecutionContext context, OrganizationId organizationId, string domainController)
			{
				if (organizationId == null)
				{
					organizationId = OrganizationId.ForestWideOrgId;
				}
				ADObjectId rootOrgContainerIdForLocalForest = Microsoft.Exchange.Data.Directory.SystemConfiguration.ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, organizationId, organizationId, false);
				ADObjectWrappers.IADSystemConfigurationSession iadsystemConfigurationSession = this.CreateADSystemConfigurationSession(context, ConsistencyMode.FullyConsistent, sessionSettings, domainController);
				return iadsystemConfigurationSession.GetOrganizationContainer(context);
			}
		}

		// Token: 0x0200001D RID: 29
		internal class ADSystemConfigurationSession : ADObjectWrappers.IADSystemConfigurationSession
		{
			// Token: 0x060000A5 RID: 165 RVA: 0x0000265C File Offset: 0x0000085C
			public ADSystemConfigurationSession(IExecutionContext context, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, string domainController)
			{
				this.wrappee = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, true, consistencyMode, sessionSettings, 668, ".ctor", "f:\\15.00.1497\\sources\\dev\\ManagedStore\\src\\DirectoryServices\\ADObjectWrappers.cs");
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00002694 File Offset: 0x00000894
			public ADObjectWrappers.IADMailboxDatabase FindDatabaseByGuid(IExecutionContext context, Guid databaseGuid)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<Guid>(0, 0L, "ADSystemConfigurationSession.FindDatabaseByGuid(databaseGuid={0})", databaseGuid);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				MailboxDatabase mailboxDatabase = null;
				using (ADExecutionTracker.TrackCall(context, "FindDatabaseByGuid"))
				{
					mailboxDatabase = ((ITopologyConfigurationSession)this.wrappee).FindDatabaseByGuid<MailboxDatabase>(databaseGuid);
				}
				if (mailboxDatabase == null)
				{
					return null;
				}
				return new ADObjectWrappers.DDSCMailboxDatabaseWrapper(mailboxDatabase);
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x00002734 File Offset: 0x00000934
			public ADObjectWrappers.IADServer FindLocalServer(IExecutionContext context)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation(0, 0L, "ADSystemConfigurationSession.FindLocalServer");
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				Server server = null;
				using (ADExecutionTracker.TrackCall(context, "FindLocalServer"))
				{
					server = ((ITopologyConfigurationSession)this.wrappee).FindLocalServer();
				}
				return new ADObjectWrappers.DDSCServerWrapper(server);
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x000027CC File Offset: 0x000009CC
			public ADObjectWrappers.IADInformationStore FindLocalInformationStore(IExecutionContext context, ADObjectWrappers.IADServer adServer)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation(0, 0L, "ADSystemConfigurationSession.FindLocalInformationStore");
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				ADObjectWrappers.IADServer iadserver = adServer;
				if (iadserver == null)
				{
					iadserver = this.FindLocalServer(context);
				}
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, this.wrappee.SessionSettings, 790, "FindLocalInformationStore", "f:\\15.00.1497\\sources\\dev\\ManagedStore\\src\\DirectoryServices\\ADObjectWrappers.cs");
				InformationStore informationStore = null;
				using (ADExecutionTracker.TrackCall(context, "FindLocalInformationStore"))
				{
					IEnumerable<InformationStore> source = topologyConfigurationSession.FindPaged<InformationStore>(iadserver.Id, QueryScope.SubTree, null, null, 0);
					informationStore = source.FirstOrDefault<InformationStore>();
				}
				if (informationStore != null)
				{
					return new ADObjectWrappers.DDSCInformationStoreWrapper(informationStore);
				}
				return null;
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x000028A4 File Offset: 0x00000AA4
			public ADObjectWrappers.IADTransportConfigContainer FindTransportConfigContainer(IExecutionContext context)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation(0, 0L, "ADSystemConfigurationSession.FindTransportConfigContainer");
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				TransportConfigContainer transportConfigContainer = null;
				using (ADExecutionTracker.TrackCall(context, "FindTransportConfigContainer"))
				{
					transportConfigContainer = this.wrappee.FindSingletonConfigurationObject<TransportConfigContainer>();
				}
				if (transportConfigContainer != null)
				{
					return new ADObjectWrappers.DDSCTransportConfigContainerWrapper(transportConfigContainer);
				}
				return null;
			}

			// Token: 0x060000AA RID: 170 RVA: 0x0000293C File Offset: 0x00000B3C
			public ADObjectWrappers.IADOrganizationContainer GetOrganizationContainer(IExecutionContext context)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation(0, 0L, "ADSystemConfigurationSession.GetOrganizationContainer");
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				Organization orgContainer;
				using (ADExecutionTracker.TrackCall(context, "GetOrganizationContainer"))
				{
					orgContainer = this.wrappee.GetOrgContainer();
				}
				if (orgContainer != null)
				{
					return new ADObjectWrappers.DDSCOrganizationContainerWrapper(orgContainer);
				}
				return null;
			}

			// Token: 0x04000015 RID: 21
			private IConfigurationSession wrappee;
		}

		// Token: 0x0200001E RID: 30
		internal class ADRecipientSession : ADObjectWrappers.IADRecipientSession
		{
			// Token: 0x060000AB RID: 171 RVA: 0x000029D0 File Offset: 0x00000BD0
			public ADRecipientSession(IExecutionContext context, ConsistencyMode consistencyMode, TenantHint tenantHint, string domainController, bool bypassSharedCache)
			{
				using (ADExecutionTracker.TrackCall(context, "SessionSettingsFromScopeSet"))
				{
					ADSessionSettings adsessionSettings = tenantHint.IsRootOrg ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromTenantPartitionHint(TenantPartitionHint.FromPersistablePartitionHint(tenantHint.TenantHintBlob));
					adsessionSettings.IncludeSoftDeletedObjects = true;
					if (bypassSharedCache || !ConfigurationSchema.UseDirectorySharedCache.Value)
					{
						this.wrappee = DirectorySessionFactory.NonCacheSessionFactory.GetTenantOrRootOrgRecipientSession(domainController, true, consistencyMode, adsessionSettings, 930, ".ctor", "f:\\15.00.1497\\sources\\dev\\ManagedStore\\src\\DirectoryServices\\ADObjectWrappers.cs");
					}
					else
					{
						this.wrappee = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, consistencyMode, adsessionSettings, 935, ".ctor", "f:\\15.00.1497\\sources\\dev\\ManagedStore\\src\\DirectoryServices\\ADObjectWrappers.cs");
					}
				}
			}

			// Token: 0x060000AC RID: 172 RVA: 0x00002A94 File Offset: 0x00000C94
			public ADRecipient FindByObjectGuid(IExecutionContext context, Guid guid)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<Guid>(0, 0L, "IRecipientSession.FindByObjectGuid(Guid={0})", guid);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				ADRecipient result = null;
				using (ADExecutionTracker.TrackCall(context, "FindByObjectGuid"))
				{
					result = this.wrappee.FindByObjectGuid(guid);
				}
				return result;
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00002B24 File Offset: 0x00000D24
			public ADRecipient FindByObjectId(IExecutionContext context, Guid objectId)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<Guid>(0, 0L, "IRecipientSession.FindByObjectId(Id={0})", objectId);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				ADRecipient result = null;
				using (ADExecutionTracker.TrackCall(context, "FindByObjectId"))
				{
					result = this.wrappee.FindByExchangeObjectId(objectId);
				}
				return result;
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00002BB4 File Offset: 0x00000DB4
			public ADRecipient FindByExchangeGuidIncludingAlternate(IExecutionContext context, Guid exchangeGuid)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<Guid>(0, 0L, "IRecipientSession.FindByExchangeGuidIncludingAlternate(exchangeGuid={0})", exchangeGuid);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				ADRecipient result = null;
				using (ADExecutionTracker.TrackCall(context, "FindByExchangeGuidIncludingAlternate"))
				{
					result = this.wrappee.FindByExchangeGuidIncludingAlternate(exchangeGuid);
				}
				return result;
			}

			// Token: 0x060000AF RID: 175 RVA: 0x00002C44 File Offset: 0x00000E44
			public ADRecipient FindByLegacyExchangeDN(IExecutionContext context, string legacyExchangeDN)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<string>(0, 0L, "IRecipientSession.FindByLegacyExchangeDN(legacyExchangeDN={0})", legacyExchangeDN);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				ADRecipient result = null;
				using (ADExecutionTracker.TrackCall(context, "FindByLegacyExchangeDN"))
				{
					result = this.wrappee.FindByLegacyExchangeDN(legacyExchangeDN);
				}
				return result;
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x00002CD4 File Offset: 0x00000ED4
			public SecurityDescriptor ReadSecurityDescriptor(IExecutionContext context, ADRecipient adRecipient)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<string>(0, 0L, "IRecipientSession.ReadSecurityDescriptor(ADRecipient.LegacyExchangeDN={0})", adRecipient.LegacyExchangeDN);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				SecurityDescriptor result;
				using (ADExecutionTracker.TrackCall(context, "ReadSecurityDescriptor"))
				{
					result = adRecipient.ReadSecurityDescriptorBlob();
				}
				return result;
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00002D60 File Offset: 0x00000F60
			public bool IsMemberOfDistributionList(IExecutionContext context, ADRecipient adRecipient, Guid distributionListObjectGuid)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<string, Guid>(0, 0L, "IRecipientSession.IsMemberOfDistributionList(ADRecipient.LegacyExchangeDN={0}, DistributionListObjectGuid={1})", adRecipient.LegacyExchangeDN, distributionListObjectGuid);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				bool result;
				using (ADExecutionTracker.TrackCall(context, "IsMemberOfDistributionList"))
				{
					result = adRecipient.IsMemberOf(new ADObjectId(distributionListObjectGuid), false);
				}
				return result;
			}

			// Token: 0x04000016 RID: 22
			private IRecipientSession wrappee;
		}

		// Token: 0x0200001F RID: 31
		internal class DDSCServerWrapper : ADObjectWrappers.IADServer
		{
			// Token: 0x060000B2 RID: 178 RVA: 0x00002DF4 File Offset: 0x00000FF4
			public DDSCServerWrapper(Server wrappee)
			{
				this.wrappee = wrappee;
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002E03 File Offset: 0x00001003
			public ADObjectId Id
			{
				get
				{
					return this.wrappee.Id;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002E10 File Offset: 0x00001010
			public Guid Guid
			{
				get
				{
					return this.wrappee.Guid;
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002E1D File Offset: 0x0000101D
			public string ExchangeLegacyDN
			{
				get
				{
					return this.wrappee.ExchangeLegacyDN;
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002E2A File Offset: 0x0000102A
			public string Fqdn
			{
				get
				{
					return this.wrappee.Fqdn;
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002E37 File Offset: 0x00001037
			public LocalLongFullPath InstallPath
			{
				get
				{
					return this.wrappee.InstallPath;
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002E44 File Offset: 0x00001044
			public ServerRole ServerRole
			{
				get
				{
					return this.wrappee.CurrentServerRole;
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002E51 File Offset: 0x00001051
			public Microsoft.Exchange.Server.Storage.Common.ServerEditionType Edition
			{
				get
				{
					return (Microsoft.Exchange.Server.Storage.Common.ServerEditionType)this.wrappee.Edition;
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000BA RID: 186 RVA: 0x00002E60 File Offset: 0x00001060
			public int TotalDatabases
			{
				get
				{
					ADObjectId[] databases = this.wrappee.Databases;
					if (databases == null)
					{
						return 0;
					}
					return databases.Length;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000BB RID: 187 RVA: 0x00002E81 File Offset: 0x00001081
			public int? MaxActiveDatabases
			{
				get
				{
					return this.wrappee.MaximumActiveDatabases;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000BC RID: 188 RVA: 0x00002E90 File Offset: 0x00001090
			public long ContinuousReplicationMaxMemoryPerDatabase
			{
				get
				{
					long? continuousReplicationMaxMemoryPerDatabase = this.wrappee.ContinuousReplicationMaxMemoryPerDatabase;
					if (continuousReplicationMaxMemoryPerDatabase != null)
					{
						return continuousReplicationMaxMemoryPerDatabase.Value;
					}
					return 10485760L;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00002EC0 File Offset: 0x000010C0
			public bool IsDAGMember
			{
				get
				{
					ADObjectId databaseAvailabilityGroup = this.wrappee.DatabaseAvailabilityGroup;
					return databaseAvailabilityGroup != null;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000BE RID: 190 RVA: 0x00002EE0 File Offset: 0x000010E0
			public string Forest
			{
				get
				{
					return ADObjectWrappers.DDSCServerWrapper.GetForest(this.wrappee.Fqdn);
				}
			}

			// Token: 0x060000BF RID: 191 RVA: 0x00002EF4 File Offset: 0x000010F4
			public SecurityDescriptor ReadSecurityDescriptor(IExecutionContext context)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<string>(0, 0L, "DDSCServerWrapper.ReadSecurityDescriptor(legacyExchangeDN={0})", this.ExchangeLegacyDN);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				SecurityDescriptor result = null;
				using (ADExecutionTracker.TrackCall(context, "ReadSecurityDescriptor"))
				{
					result = this.wrappee.ReadSecurityDescriptorBlob();
				}
				return result;
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00002F88 File Offset: 0x00001188
			private static string GetForest(string fqdn)
			{
				if (fqdn != null && fqdn.IndexOf('.') >= 0)
				{
					string[] array = fqdn.Split(new char[]
					{
						'.'
					});
					if (array.Length > 1)
					{
						return array[1].ToUpper();
					}
				}
				return string.Empty;
			}

			// Token: 0x04000017 RID: 23
			private Server wrappee;
		}

		// Token: 0x02000020 RID: 32
		internal class DDSCTransportConfigContainerWrapper : ADObjectWrappers.IADTransportConfigContainer
		{
			// Token: 0x060000C1 RID: 193 RVA: 0x00002FCB File Offset: 0x000011CB
			public DDSCTransportConfigContainerWrapper(TransportConfigContainer wrappee)
			{
				this.wrappee = wrappee;
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002FDA File Offset: 0x000011DA
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				get
				{
					return this.wrappee.MaxSendSize;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002FE7 File Offset: 0x000011E7
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				get
				{
					return this.wrappee.MaxReceiveSize;
				}
			}

			// Token: 0x04000018 RID: 24
			private TransportConfigContainer wrappee;
		}

		// Token: 0x02000021 RID: 33
		internal class DDSCInformationStoreWrapper : ADObjectWrappers.IADInformationStore
		{
			// Token: 0x060000C4 RID: 196 RVA: 0x00002FF4 File Offset: 0x000011F4
			public DDSCInformationStoreWrapper(InformationStore wrappee)
			{
				this.wrappee = wrappee;
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003003 File Offset: 0x00001203
			public int? MaxRpcThreads
			{
				get
				{
					return this.wrappee.MaxRpcThreads;
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003010 File Offset: 0x00001210
			public int MaxRecoveryDatabases
			{
				get
				{
					return this.wrappee.MaxRestoreStorageGroups;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000301D File Offset: 0x0000121D
			public int MaxTotalDatabases
			{
				get
				{
					return this.wrappee.MaxStoresTotal;
				}
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x0000302C File Offset: 0x0000122C
			public void LoadDatabaseOptions(IExecutionContext context, DatabaseOptions options)
			{
				options.MinCachePages = this.wrappee.MinCachePages;
				options.MaxCachePages = this.wrappee.MaxCachePages;
				options.EnableOnlineDefragmentation = this.wrappee.EnableOnlineDefragmentation;
				if (options.MinCachePages != null && options.MaxCachePages != null)
				{
					Directory.CheckADObjectIsNotCorrupt((LID)36776U, context, options.MinCachePages <= options.MaxCachePages, "MinCachePages is greater than MaxCachePages for object {0}", this.wrappee.Identity);
				}
			}

			// Token: 0x04000019 RID: 25
			private InformationStore wrappee;
		}

		// Token: 0x02000022 RID: 34
		internal class DDSCOrganizationContainerWrapper : ADObjectWrappers.IADOrganizationContainer
		{
			// Token: 0x060000C9 RID: 201 RVA: 0x000030D6 File Offset: 0x000012D6
			public DDSCOrganizationContainerWrapper(Organization wrappee)
			{
				this.wrappee = wrappee;
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000CA RID: 202 RVA: 0x000030E5 File Offset: 0x000012E5
			public Guid ObjectGuid
			{
				get
				{
					return this.wrappee.Guid;
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000CB RID: 203 RVA: 0x000030F2 File Offset: 0x000012F2
			public Guid HierarchyMailboxGuid
			{
				get
				{
					return this.wrappee.DefaultPublicFolderMailbox.HierarchyMailboxGuid;
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000CC RID: 204 RVA: 0x00003104 File Offset: 0x00001304
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				get
				{
					return this.wrappee.DefaultPublicFolderIssueWarningQuota;
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000CD RID: 205 RVA: 0x00003111 File Offset: 0x00001311
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				get
				{
					return this.wrappee.DefaultPublicFolderProhibitPostQuota;
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000CE RID: 206 RVA: 0x0000311E File Offset: 0x0000131E
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				get
				{
					return this.wrappee.DefaultPublicFolderMaxItemSize;
				}
			}

			// Token: 0x0400001A RID: 26
			private Organization wrappee;
		}

		// Token: 0x02000023 RID: 35
		internal class DDSCMailboxDatabaseWrapper : ADObjectWrappers.IADMailboxDatabase
		{
			// Token: 0x060000CF RID: 207 RVA: 0x0000312B File Offset: 0x0000132B
			public DDSCMailboxDatabaseWrapper(MailboxDatabase wrappee)
			{
				this.wrappee = wrappee;
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000313A File Offset: 0x0000133A
			public bool AllowFileRestore
			{
				get
				{
					return this.wrappee.AllowFileRestore;
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003147 File Offset: 0x00001347
			public Guid Guid
			{
				get
				{
					return this.wrappee.Guid;
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003154 File Offset: 0x00001354
			public string Name
			{
				get
				{
					return this.wrappee.Name;
				}
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003161 File Offset: 0x00001361
			public Guid DagOrServerGuid
			{
				get
				{
					if (this.wrappee.MasterServerOrAvailabilityGroup != null)
					{
						return this.wrappee.MasterServerOrAvailabilityGroup.ObjectGuid;
					}
					return Guid.Empty;
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003186 File Offset: 0x00001386
			public string ExchangeLegacyDN
			{
				get
				{
					return this.wrappee.ExchangeLegacyDN;
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003193 File Offset: 0x00001393
			public string Description
			{
				get
				{
					return this.wrappee.Description;
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000D6 RID: 214 RVA: 0x000031A0 File Offset: 0x000013A0
			public string ServerName
			{
				get
				{
					return this.wrappee.ServerName;
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x000031AD File Offset: 0x000013AD
			public bool Recovery
			{
				get
				{
					return this.wrappee.Recovery;
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000D8 RID: 216 RVA: 0x000031BA File Offset: 0x000013BA
			public bool CircularLoggingEnabled
			{
				get
				{
					return this.wrappee.CircularLoggingEnabled;
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x000031C7 File Offset: 0x000013C7
			public EdbFilePath EdbFilePath
			{
				get
				{
					return this.wrappee.EdbFilePath;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000DA RID: 218 RVA: 0x000031D4 File Offset: 0x000013D4
			public NonRootLocalLongFullPath LogFolderPath
			{
				get
				{
					return this.wrappee.LogFolderPath;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060000DB RID: 219 RVA: 0x000031E1 File Offset: 0x000013E1
			public EnhancedTimeSpan EventHistoryRetentionPeriod
			{
				get
				{
					return this.wrappee.EventHistoryRetentionPeriod;
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060000DC RID: 220 RVA: 0x000031EE File Offset: 0x000013EE
			public EnhancedTimeSpan MailboxRetention
			{
				get
				{
					return this.wrappee.MailboxRetention;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060000DD RID: 221 RVA: 0x000031FC File Offset: 0x000013FC
			public string[] HostServerNames
			{
				get
				{
					string[] array = Array<string>.Empty;
					DatabaseCopy[] databaseCopies = this.wrappee.DatabaseCopies;
					if (databaseCopies != null && databaseCopies.Length > 0)
					{
						array = new string[databaseCopies.Length];
						for (int i = 0; i < databaseCopies.Length; i++)
						{
							array[i] = databaseCopies[i].HostServerName;
						}
					}
					return array;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00003247 File Offset: 0x00001447
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> IssueWarningQuota
			{
				get
				{
					return this.wrappee.IssueWarningQuota;
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060000DF RID: 223 RVA: 0x00003254 File Offset: 0x00001454
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> ProhibitSendQuota
			{
				get
				{
					return this.wrappee.ProhibitSendQuota;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003261 File Offset: 0x00001461
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
			{
				get
				{
					return this.wrappee.ProhibitSendReceiveQuota;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000326E File Offset: 0x0000146E
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				get
				{
					return this.wrappee.RecoverableItemsWarningQuota;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000327B File Offset: 0x0000147B
			public Microsoft.Exchange.Data.Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				get
				{
					return this.wrappee.RecoverableItemsQuota;
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003288 File Offset: 0x00001488
			public int DataMoveReplicationConstraint
			{
				get
				{
					return (int)this.wrappee.DataMoveReplicationConstraint;
				}
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00003298 File Offset: 0x00001498
			public SecurityDescriptor ReadSecurityDescriptor(IExecutionContext context)
			{
				if (ExTraceGlobals.ADCallsTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ADCallsTracer.TraceInformation<string>(0, 0L, "DDSCMailboxDatabaseWrapper.ReadSecurityDescriptor(legacyExchangeDN={0})", this.ExchangeLegacyDN);
				}
				if (ExTraceGlobals.CallStackTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.CallStackTracer.TraceInformation(0, 0L, new StackTrace(true).ToString());
				}
				SecurityDescriptor result = null;
				using (ADExecutionTracker.TrackCall(context, "ReadSecurityDescriptor"))
				{
					result = this.wrappee.ReadSecurityDescriptorBlob();
				}
				return result;
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x0000332C File Offset: 0x0000152C
			public void LoadDatabaseOptions(IExecutionContext context, DatabaseOptions options)
			{
				bool? flag = new bool?(this.wrappee.BackgroundDatabaseMaintenance);
				if (flag != null)
				{
					options.BackgroundDatabaseMaintenance = flag.Value;
				}
				flag = this.wrappee.ReplayBackgroundDatabaseMaintenance;
				if (flag != null)
				{
					options.ReplayBackgroundDatabaseMaintenance = flag;
				}
				flag = this.wrappee.BackgroundDatabaseMaintenanceSerialization;
				if (flag != null)
				{
					options.BackgroundDatabaseMaintenanceSerialization = flag;
				}
				int? num = this.wrappee.BackgroundDatabaseMaintenanceDelay;
				if (num != null)
				{
					options.BackgroundDatabaseMaintenanceDelay = num;
				}
				num = this.wrappee.ReplayBackgroundDatabaseMaintenanceDelay;
				if (num != null)
				{
					options.ReplayBackgroundDatabaseMaintenanceDelay = num;
				}
				num = this.wrappee.MimimumBackgroundDatabaseMaintenanceInterval;
				if (num != null)
				{
					options.MimimumBackgroundDatabaseMaintenanceInterval = num;
				}
				num = this.wrappee.MaximumBackgroundDatabaseMaintenanceInterval;
				if (num != null)
				{
					options.MaximumBackgroundDatabaseMaintenanceInterval = num;
				}
				if (this.wrappee.TemporaryDataFolderPath != null)
				{
					string pathName = this.wrappee.TemporaryDataFolderPath.PathName;
					if (!string.IsNullOrEmpty(pathName))
					{
						options.TemporaryDataFolderPath = pathName;
					}
				}
				if (this.wrappee.LogFilePrefix != null)
				{
					string logFilePrefix = this.wrappee.LogFilePrefix;
					if (!string.IsNullOrEmpty(logFilePrefix))
					{
						options.LogFilePrefix = logFilePrefix;
					}
				}
				num = this.wrappee.LogBuffers;
				if (num != null)
				{
					options.LogBuffers = num;
				}
				num = this.wrappee.MaximumOpenTables;
				if (num != null)
				{
					options.MaximumOpenTables = num;
				}
				num = this.wrappee.MaximumTemporaryTables;
				if (num != null)
				{
					options.MaximumTemporaryTables = num;
				}
				num = this.wrappee.MaximumCursors;
				if (num != null)
				{
					options.MaximumCursors = num;
				}
				num = this.wrappee.MaximumSessions;
				if (num != null)
				{
					options.MaximumSessions = num;
				}
				num = this.wrappee.MaximumVersionStorePages;
				if (num != null)
				{
					options.MaximumVersionStorePages = num;
				}
				num = this.wrappee.PreferredVersionStorePages;
				if (num != null)
				{
					options.PreferredVersionStorePages = num;
				}
				num = this.wrappee.DatabaseExtensionSize;
				if (num != null)
				{
					options.DatabaseExtensionSize = num;
				}
				num = this.wrappee.LogCheckpointDepth;
				if (num != null)
				{
					options.LogCheckpointDepth = num;
				}
				num = this.wrappee.ReplayCheckpointDepth;
				if (num != null)
				{
					options.ReplayCheckpointDepth = num;
				}
				num = this.wrappee.CachedClosedTables;
				if (num != null)
				{
					options.CachedClosedTables = num;
				}
				num = this.wrappee.CachePriority;
				if (num != null)
				{
					options.CachePriority = num;
				}
				num = this.wrappee.ReplayCachePriority;
				if (num != null)
				{
					options.ReplayCachePriority = num;
				}
				num = this.wrappee.MaximumPreReadPages;
				if (num != null)
				{
					options.MaximumPreReadPages = num;
				}
				num = this.wrappee.MaximumReplayPreReadPages;
				if (num != null)
				{
					options.MaximumReplayPreReadPages = num;
				}
			}

			// Token: 0x0400001B RID: 27
			private MailboxDatabase wrappee;
		}
	}
}
