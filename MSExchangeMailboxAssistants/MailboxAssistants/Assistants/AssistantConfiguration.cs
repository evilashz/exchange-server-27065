using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarSync;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Dar;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceDataCollection;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceTraining;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.JunkEmailOptions;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.MailboxProcessor;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.PeopleRelevance;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Search;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.SharePointSignalStore;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.PublicFolder;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SharingPolicy;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SiteMailbox;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.DirectoryServices;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Exchange.Diagnostics.Components.PeopleCentricTriage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200000C RID: 12
	internal sealed class AssistantConfiguration
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003B2A File Offset: 0x00001D2A
		private AssistantConfiguration(string propertyName, AssistantConfiguration.GetConfigurationDelegate getConfiguration, Trace tracer)
		{
			this.propertyName = propertyName;
			this.getConfiguration = getConfiguration;
			this.tracer = tracer;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003B48 File Offset: 0x00001D48
		public TimeSpan Read()
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Reading {0} from AD.", this.propertyName);
			Server localServer = LocalServerCache.LocalServer;
			if (localServer == null)
			{
				this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Unable to read {0} from AD.", this.propertyName);
			}
			else
			{
				EnhancedTimeSpan? enhancedTimeSpan = this.getConfiguration(localServer);
				if (enhancedTimeSpan == null || enhancedTimeSpan == null)
				{
					this.tracer.TraceDebug<string>((long)this.GetHashCode(), "Read {0} from AD but value is null.", this.propertyName);
				}
				else
				{
					timeSpan = enhancedTimeSpan.Value;
					this.tracer.TraceDebug<string, TimeSpan>((long)this.GetHashCode(), "Retrieved {0}. Value: {1}", this.propertyName, timeSpan);
				}
			}
			return timeSpan;
		}

		// Token: 0x04000050 RID: 80
		public static readonly AssistantConfiguration CalendarRepairWorkCycle = new AssistantConfiguration("CalendarRepairWorkCycle", (Server mailboxServer) => mailboxServer.CalendarRepairWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair.ExTraceGlobals.CalendarRepairAssistantTracer);

		// Token: 0x04000051 RID: 81
		public static readonly AssistantConfiguration CalendarRepairWorkCycleCheckpoint = new AssistantConfiguration("CalendarRepairWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.CalendarRepairWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair.ExTraceGlobals.CalendarRepairAssistantTracer);

		// Token: 0x04000052 RID: 82
		public static readonly AssistantConfiguration SharingPolicyWorkCycle = new AssistantConfiguration("SharingPolicyWorkCycle", (Server mailboxServer) => mailboxServer.SharingPolicyWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SharingPolicy.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000053 RID: 83
		public static readonly AssistantConfiguration SharingPolicyWorkCycleCheckpoint = new AssistantConfiguration("SharingPolicyWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.SharingPolicyWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SharingPolicy.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000054 RID: 84
		public static readonly AssistantConfiguration PublicFolderWorkCycle = new AssistantConfiguration("PublicFolderWorkCycle", (Server mailboxServer) => mailboxServer.PublicFolderWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.PublicFolder.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000055 RID: 85
		public static readonly AssistantConfiguration PublicFolderWorkCycleCheckpoint = new AssistantConfiguration("PublicFolderWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.PublicFolderWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.PublicFolder.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000056 RID: 86
		public static readonly AssistantConfiguration SiteMailboxWorkCycle = new AssistantConfiguration("SiteMailboxWorkCycle", (Server mailboxServer) => mailboxServer.SiteMailboxWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SiteMailbox.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000057 RID: 87
		public static readonly AssistantConfiguration SiteMailboxWorkCycleCheckpoint = new AssistantConfiguration("SiteMailboxWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.SiteMailboxWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.SiteMailbox.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000058 RID: 88
		public static readonly AssistantConfiguration SharingSyncWorkCycle = new AssistantConfiguration("SharingSyncWorkCycle", (Server mailboxServer) => mailboxServer.SharingSyncWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarSync.ExTraceGlobals.CalendarSyncAssistantTracer);

		// Token: 0x04000059 RID: 89
		public static readonly AssistantConfiguration SharingSyncWorkCycleCheckpoint = new AssistantConfiguration("SharingSyncWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.SharingSyncWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarSync.ExTraceGlobals.CalendarSyncAssistantTracer);

		// Token: 0x0400005A RID: 90
		public static readonly AssistantConfiguration MailboxAssociationReplicationWorkCycle = new AssistantConfiguration("MailboxAssociationReplicationWorkCycle", (Server mailboxServer) => mailboxServer.MailboxAssociationReplicationWorkCycle, Microsoft.Exchange.Diagnostics.Components.GroupMailbox.ExTraceGlobals.AssociationReplicationAssistantTracer);

		// Token: 0x0400005B RID: 91
		public static readonly AssistantConfiguration MailboxAssociationReplicationWorkCycleCheckpoint = new AssistantConfiguration("MailboxAssociationReplicationWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.MailboxAssociationReplicationWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.GroupMailbox.ExTraceGlobals.AssociationReplicationAssistantTracer);

		// Token: 0x0400005C RID: 92
		public static readonly AssistantConfiguration GroupMailboxWorkCycle = new AssistantConfiguration("GroupMailboxWorkCycle", (Server mailboxServer) => mailboxServer.GroupMailboxWorkCycle, Microsoft.Exchange.Diagnostics.Components.GroupMailbox.ExTraceGlobals.GroupMailboxAssistantTracer);

		// Token: 0x0400005D RID: 93
		public static readonly AssistantConfiguration GroupMailboxWorkCycleCheckpoint = new AssistantConfiguration("GroupMailboxWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.GroupMailboxWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.GroupMailbox.ExTraceGlobals.GroupMailboxAssistantTracer);

		// Token: 0x0400005E RID: 94
		public static readonly AssistantConfiguration ManagedFolderWorkCycle = new AssistantConfiguration("ManagedFolderWorkCycle", (Server mailboxServer) => mailboxServer.ManagedFolderWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC.ExTraceGlobals.ELCAssistantTracer);

		// Token: 0x0400005F RID: 95
		public static readonly AssistantConfiguration ManagedFolderWorkCycleCheckpoint = new AssistantConfiguration("ManagedFolderWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.ManagedFolderWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC.ExTraceGlobals.ELCAssistantTracer);

		// Token: 0x04000060 RID: 96
		public static readonly AssistantConfiguration TopNWorkCycle = new AssistantConfiguration("TopNWorkCycle", (Server mailboxServer) => mailboxServer.TopNWorkCycle, Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN.ExTraceGlobals.TopNAssistantTracer);

		// Token: 0x04000061 RID: 97
		public static readonly AssistantConfiguration TopNWorkCycleCheckpoint = new AssistantConfiguration("TopNWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.TopNWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN.ExTraceGlobals.TopNAssistantTracer);

		// Token: 0x04000062 RID: 98
		public static readonly AssistantConfiguration UMReportingWorkCycle = new AssistantConfiguration("UMReportingWorkCycle", (Server mailboxServer) => mailboxServer.UMReportingWorkCycle, Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging.ExTraceGlobals.UMReportsTracer);

		// Token: 0x04000063 RID: 99
		public static readonly AssistantConfiguration UMReportingWorkCycleCheckpoint = new AssistantConfiguration("UMReportingWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.UMReportingWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging.ExTraceGlobals.UMReportsTracer);

		// Token: 0x04000064 RID: 100
		public static readonly AssistantConfiguration InferenceTrainingWorkCycle = new AssistantConfiguration("InferenceTrainingWorkCycle", (Server mailboxServer) => mailboxServer.InferenceTrainingWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceTraining.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000065 RID: 101
		public static readonly AssistantConfiguration InferenceTrainingWorkCycleCheckpoint = new AssistantConfiguration("InferenceTrainingWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.InferenceTrainingWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceTraining.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000066 RID: 102
		public static readonly AssistantConfiguration DirectoryProcessorWorkCycle = new AssistantConfiguration("DirectoryProcessorWorkCycle", (Server mailboxServer) => mailboxServer.DirectoryProcessorWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000067 RID: 103
		public static readonly AssistantConfiguration DirectoryProcessorWorkCycleCheckpoint = new AssistantConfiguration("DirectoryProcessorWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.DirectoryProcessorWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000068 RID: 104
		public static readonly AssistantConfiguration OABGeneratorWorkCycle = new AssistantConfiguration("OABGeneratorWorkCycle", (Server mailboxServer) => mailboxServer.OABGeneratorWorkCycle, Microsoft.Exchange.Diagnostics.Components.OAB.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000069 RID: 105
		public static readonly AssistantConfiguration OABGeneratorWorkCycleCheckpoint = new AssistantConfiguration("OABGeneratorWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.OABGeneratorWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.OAB.ExTraceGlobals.AssistantTracer);

		// Token: 0x0400006A RID: 106
		public static readonly AssistantConfiguration InferenceDataCollectionWorkCycle = new AssistantConfiguration("InferenceDataCollectionWorkCycle", (Server mailboxServer) => mailboxServer.InferenceDataCollectionWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceDataCollection.ExTraceGlobals.GeneralTracer);

		// Token: 0x0400006B RID: 107
		public static readonly AssistantConfiguration InferenceDataCollectionWorkCycleCheckpoint = new AssistantConfiguration("InferenceDataCollectionWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.InferenceDataCollectionWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceDataCollection.ExTraceGlobals.GeneralTracer);

		// Token: 0x0400006C RID: 108
		public static readonly AssistantConfiguration PeopleRelevanceWorkCycle = new AssistantConfiguration("PeopleRelevanceWorkCycle", (Server mailboxServer) => mailboxServer.PeopleRelevanceWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.PeopleRelevance.ExTraceGlobals.GeneralTracer);

		// Token: 0x0400006D RID: 109
		public static readonly AssistantConfiguration PeopleRelevanceWorkCycleCheckpoint = new AssistantConfiguration("PeopleRelevanceWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.PeopleRelevanceWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.PeopleRelevance.ExTraceGlobals.GeneralTracer);

		// Token: 0x0400006E RID: 110
		public static readonly AssistantConfiguration SharePointSignalStoreWorkCycle = new AssistantConfiguration("SharePointSignalStoreWorkCycle", (Server mailboxServer) => mailboxServer.SharePointSignalStoreWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.SharePointSignalStore.ExTraceGlobals.GeneralTracer);

		// Token: 0x0400006F RID: 111
		public static readonly AssistantConfiguration SharePointSignalStoreWorkCycleCheckpoint = new AssistantConfiguration("SharePointSignalStoreWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.SharePointSignalStoreWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.SharePointSignalStore.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000070 RID: 112
		public static readonly AssistantConfiguration PeopleCentricTriageWorkCycle = new AssistantConfiguration("PeopleCentricTriageWorkCycle", (Server mailboxServer) => mailboxServer.PeopleCentricTriageWorkCycle, Microsoft.Exchange.Diagnostics.Components.PeopleCentricTriage.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000071 RID: 113
		public static readonly AssistantConfiguration PeopleCentricTriageWorkCycleCheckpoint = new AssistantConfiguration("PeopleCentricTriageWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.PeopleCentricTriageWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.PeopleCentricTriage.ExTraceGlobals.AssistantTracer);

		// Token: 0x04000072 RID: 114
		public static readonly AssistantConfiguration ProbeTimeBasedAssistantWorkCycle = new AssistantConfiguration("ProbeTimeBasedAssistantWorkCycle", (Server mailboxServer) => mailboxServer.ProbeTimeBasedAssistantWorkCycle, Microsoft.Exchange.Diagnostics.Components.Assistants.ExTraceGlobals.ProbeTimeBasedAssistantTracer);

		// Token: 0x04000073 RID: 115
		public static readonly AssistantConfiguration ProbeTimeBasedAssistantWorkCycleCheckpoint = new AssistantConfiguration("ProbeTimeBasedAssistantWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.ProbeTimeBasedAssistantWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.Assistants.ExTraceGlobals.ProbeTimeBasedAssistantTracer);

		// Token: 0x04000074 RID: 116
		public static readonly AssistantConfiguration SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint = new AssistantConfiguration("SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Search.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000075 RID: 117
		public static readonly AssistantConfiguration SearchIndexRepairTimeBasedAssistantWorkCycle = new AssistantConfiguration("SearchIndexRepairTimeBasedAssistantWorkCycle", (Server mailboxServer) => mailboxServer.SearchIndexRepairTimeBasedAssistantWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Search.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000076 RID: 118
		public static readonly AssistantConfiguration MailboxProcessorWorkCycle = new AssistantConfiguration("MailboxProcessorWorkCycle", (Server mailboxServer) => mailboxServer.MailboxProcessorWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.MailboxProcessor.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000077 RID: 119
		public static readonly AssistantConfiguration StoreDsMaintenanceWorkCycle = new AssistantConfiguration("StoreDsMaintenanceWorkCycle", (Server mailboxServer) => mailboxServer.StoreDsMaintenanceWorkCycle, Microsoft.Exchange.Diagnostics.Components.ManagedStore.DirectoryServices.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000078 RID: 120
		public static readonly AssistantConfiguration StoreDsMaintenanceWorkCycleCheckpoint = new AssistantConfiguration("StoreDsMaintenanceWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.StoreDsMaintenanceWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.ManagedStore.DirectoryServices.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000079 RID: 121
		public static readonly AssistantConfiguration StoreIntegrityCheckWorkCycle = new AssistantConfiguration("StoreIntegrityCheckWorkCycle", (Server mailboxServer) => mailboxServer.StoreIntegrityCheckWorkCycle, Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck.ExTraceGlobals.OnlineIsintegTracer);

		// Token: 0x0400007A RID: 122
		public static readonly AssistantConfiguration StoreIntegrityCheckWorkCycleCheckpoint = new AssistantConfiguration("StoreIntegrityCheckWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.StoreIntegrityCheckWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck.ExTraceGlobals.OnlineIsintegTracer);

		// Token: 0x0400007B RID: 123
		public static readonly AssistantConfiguration StoreMaintenanceWorkCycle = new AssistantConfiguration("StoreMaintenanceWorkCycle", (Server mailboxServer) => mailboxServer.StoreMaintenanceWorkCycle, Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common.ExTraceGlobals.GeneralTracer);

		// Token: 0x0400007C RID: 124
		public static readonly AssistantConfiguration StoreMaintenanceWorkCycleCheckpoint = new AssistantConfiguration("StoreMaintenanceWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.StoreMaintenanceWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common.ExTraceGlobals.GeneralTracer);

		// Token: 0x0400007D RID: 125
		public static readonly AssistantConfiguration StoreScheduledIntegrityCheckWorkCycle = new AssistantConfiguration("StoreScheduledIntegrityCheckWorkCycle", (Server mailboxServer) => mailboxServer.StoreScheduledIntegrityCheckWorkCycle, Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck.ExTraceGlobals.OnlineIsintegTracer);

		// Token: 0x0400007E RID: 126
		public static readonly AssistantConfiguration StoreScheduledIntegrityCheckWorkCycleCheckpoint = new AssistantConfiguration("StoreScheduledIntegrityCheckWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.StoreScheduledIntegrityCheckWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreIntegrityCheck.ExTraceGlobals.OnlineIsintegTracer);

		// Token: 0x0400007F RID: 127
		public static readonly AssistantConfiguration DarTaskStoreTimeBasedAssistantWorkCycle = new AssistantConfiguration("DarTaskStoreTimeBasedAssistantWorkCycle", (Server mailboxServer) => mailboxServer.DarTaskStoreTimeBasedAssistantWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Dar.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000080 RID: 128
		public static readonly AssistantConfiguration DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint = new AssistantConfiguration("DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Dar.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000081 RID: 129
		public static readonly AssistantConfiguration StoreUrgentMaintenanceWorkCycle = new AssistantConfiguration("StoreUrgentMaintenanceWorkCycle", (Server mailboxServer) => mailboxServer.StoreUrgentMaintenanceWorkCycle, Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000082 RID: 130
		public static readonly AssistantConfiguration StoreUrgentMaintenanceWorkCycleCheckpoint = new AssistantConfiguration("StoreUrgentMaintenanceWorkCycleCheckpoint", (Server mailboxServer) => mailboxServer.StoreUrgentMaintenanceWorkCycleCheckpoint, Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common.ExTraceGlobals.GeneralTracer);

		// Token: 0x04000083 RID: 131
		public static readonly AssistantConfiguration JunkEmailOptionsCommitterWorkCycle = new AssistantConfiguration("JunkEmailOptionsCommitterWorkCycle", (Server mailboxServer) => mailboxServer.JunkEmailOptionsCommitterWorkCycle, Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.JunkEmailOptions.ExTraceGlobals.JEOAssistantTracer);

		// Token: 0x04000084 RID: 132
		private readonly string propertyName;

		// Token: 0x04000085 RID: 133
		private readonly AssistantConfiguration.GetConfigurationDelegate getConfiguration;

		// Token: 0x04000086 RID: 134
		private readonly Trace tracer;

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x06000094 RID: 148
		private delegate EnhancedTimeSpan? GetConfigurationDelegate(Server mailboxServer);
	}
}
