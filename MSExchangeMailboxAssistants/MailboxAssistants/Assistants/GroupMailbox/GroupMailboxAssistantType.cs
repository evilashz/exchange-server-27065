using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.UnifiedGroups;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.GroupMailbox
{
	// Token: 0x02000231 RID: 561
	internal sealed class GroupMailboxAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00078C68 File Offset: 0x00076E68
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.GroupMailboxWorkCycle.Read();
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00078C74 File Offset: 0x00076E74
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.GroupMailboxWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00078C80 File Offset: 0x00076E80
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.GroupMailboxAssistant;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00078C84 File Offset: 0x00076E84
		public LocalizedString Name
		{
			get
			{
				return Strings.groupMailboxAssistantName;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00078C8B File Offset: 0x00076E8B
		public string NonLocalizedName
		{
			get
			{
				return "GroupMailboxAssistant";
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00078C92 File Offset: 0x00076E92
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.GroupMailboxAssistant;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00078C96 File Offset: 0x00076E96
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return GroupMailboxAssistantType.ExtendedProperties;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00078CA0 File Offset: 0x00076EA0
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			GroupMailboxAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "GroupMailboxAssistantType.IsMailboxInteresting");
			if (!mailboxInformation.IsGroupMailbox())
			{
				GroupMailboxAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[{0}]: mailbox is a not group mailbox.", mailboxInformation.MailboxGuid);
				return false;
			}
			int? num = mailboxInformation.GetMailboxProperty(MailboxSchema.GroupMailboxPermissionsVersion) as int?;
			int? photoVersion = mailboxInformation.GetMailboxProperty(MailboxSchema.GroupMailboxGeneratedPhotoVersion) as int?;
			mailboxInformation.GetMailboxProperty(MailboxSchema.GroupMailboxExchangeResourcesPublishedVersion);
			return GroupMailboxAssistantType.IsGroupMailboxGeneratedPhotoOutdated(photoVersion, mailboxInformation.MailboxGuid) || GroupMailboxAssistantType.IsGroupMailboxPermissionsVersionOutdated(num, mailboxInformation.MailboxGuid) || GroupMailboxAssistantType.IsGroupMailboxExchangeResourcesVersionOutdated(num, mailboxInformation.MailboxGuid);
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00078D4B File Offset: 0x00076F4B
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForGroupMailboxAssistant;
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00078D52 File Offset: 0x00076F52
		public void OnWorkCycleCheckpoint()
		{
			GroupMailboxAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "GroupMailboxAssistantType.OnWorkCycleCheckpoint");
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00078D6A File Offset: 0x00076F6A
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			GroupMailboxAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "GroupMailboxAssistantType.CreateInstance");
			return new GroupMailboxAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00078D94 File Offset: 0x00076F94
		internal static bool IsGroupMailboxGeneratedPhotoOutdated(int? photoVersion, Guid mailboxGuid)
		{
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MailboxAssistants.GenerateGroupPhoto.Enabled)
			{
				GroupMailboxAssistantType.Tracer.TraceDebug<Guid>(0L, "Flight GenerateGroupPhoto disabled for {0}, skipping photo update.", mailboxGuid);
				return false;
			}
			if (photoVersion == null || photoVersion < 1)
			{
				GroupMailboxAssistantType.Tracer.TraceDebug<Guid>(0L, "GroupMailboxAssistantType.IsGroupMailboxGeneratedPhotoOutdated: Mailbox {0} requires default photo update.", mailboxGuid);
				return true;
			}
			GroupMailboxAssistantType.Tracer.TraceDebug<Guid>(0L, "GroupMailboxAssistantType.IsGroupMailboxGeneratedPhotoOutdated: Mailbox {0} does not require default photo update.", mailboxGuid);
			return false;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00078E20 File Offset: 0x00077020
		internal static bool IsGroupMailboxExchangeResourcesVersionOutdated(int? resourcesVersion, Guid mailboxGuid)
		{
			if (!AADClientFactory.IsAADEnabled())
			{
				GroupMailboxAssistantType.Tracer.TraceDebug<Guid>(0L, "GroupMailboxAssistantType.IsGroupMailboxExchangeResourcesVersionOutdated: AAD is not enabled for mailbox {0}, skipping resource publishing.", mailboxGuid);
				return false;
			}
			bool flag = GroupMailboxExchangeResourcesPublisher.IsPublishedVersionOutdated(resourcesVersion);
			GroupMailboxAssistantType.Tracer.TraceDebug<Guid, bool>(0L, "GroupMailboxAssistantType.IsGroupMailboxExchangeResourcesVersionOutdated:  {0} - {1}", mailboxGuid, flag);
			return flag;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00078E64 File Offset: 0x00077064
		internal static bool IsGroupMailboxPermissionsVersionOutdated(int? groupMailboxVersion, Guid mailboxGuid)
		{
			if (groupMailboxVersion == null || groupMailboxVersion < GroupMailboxPermissionHandler.GroupMailboxPermissionVersion)
			{
				GroupMailboxAssistantType.Tracer.TraceDebug<Guid>(0L, "GroupMailboxAssistantType.IsGroupMailboxVersionOutdated: Mailbox {0} requires permission update.", mailboxGuid);
				return true;
			}
			GroupMailboxAssistantType.Tracer.TraceDebug<Guid>(0L, "GroupMailboxAssistantType.IsGroupMailboxVersionOutdated: Mailbox {0} does not require permission update.", mailboxGuid);
			return false;
		}

		// Token: 0x04000CA6 RID: 3238
		internal const string AssistantName = "GroupMailboxAssistant";

		// Token: 0x04000CA7 RID: 3239
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAssistantTracer;

		// Token: 0x04000CA8 RID: 3240
		internal static readonly PropertyTagPropertyDefinition[] ExtendedProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.GroupMailboxPermissionsVersion,
			MailboxSchema.GroupMailboxGeneratedPhotoVersion,
			MailboxSchema.GroupMailboxGeneratedPhotoSignature,
			MailboxSchema.GroupMailboxExchangeResourcesPublishedVersion
		};
	}
}
