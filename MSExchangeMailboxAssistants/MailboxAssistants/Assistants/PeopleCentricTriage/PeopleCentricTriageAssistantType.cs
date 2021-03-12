using System;
using System.Globalization;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.ApplicationLogic.PeopleCentricTriage;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PeopleCentricTriage
{
	// Token: 0x0200021A RID: 538
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleCentricTriageAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x0600147C RID: 5244 RVA: 0x000763B0 File Offset: 0x000745B0
		public PeopleCentricTriageAssistantType()
		{
			this.logger = new PeopleCentricTriageLogger(PeopleCentricTriageAssistantType.PeopleCentricTriageConfiguration.AssistantLoggingPath, PeopleCentricTriageAssistantType.PeopleCentricTriageConfiguration.LogFileMaxAge, PeopleCentricTriageAssistantType.PeopleCentricTriageConfiguration.LogDirectoryMaxSize, PeopleCentricTriageAssistantType.PeopleCentricTriageConfiguration.LogFileMaxSize, "PeopleCentricTriageAssistant", "PeopleCentricTriageAssistant");
			this.logger.TraceDebug((long)this.GetHashCode(), "PeopleCentricTriageAssistantType: initialized.");
			this.isInterestingProcessor = new MailboxProcessor(PeopleCentricTriageAssistantType.PeopleCentricTriageConfiguration, NullPeopleIKnowPublisherFactory.Instance, this.logger, this.logger);
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00076438 File Offset: 0x00074638
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.PeopleCentricTriageAssistant;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0007643C File Offset: 0x0007463C
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForPeopleCentricTriageAssistant;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00076443 File Offset: 0x00074643
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return PeopleCentricTriageAssistantType.NoMailboxExtendedProperties;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0007644A File Offset: 0x0007464A
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.PeopleCentricTriageWorkCycle.Read();
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00076456 File Offset: 0x00074656
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.PeopleCentricTriageWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00076462 File Offset: 0x00074662
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.PeopleCentricTriageAssistant;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00076466 File Offset: 0x00074666
		public LocalizedString Name
		{
			get
			{
				return Strings.PeopleCentricTriageAssistantName;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0007646D File Offset: 0x0007466D
		public string NonLocalizedName
		{
			get
			{
				return "PeopleCentricTriageAssistant";
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00076474 File Offset: 0x00074674
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00076477 File Offset: 0x00074677
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new PeopleCentricTriageAssistant(databaseInfo, this.Name, this.NonLocalizedName, PeopleCentricTriageAssistantType.PeopleCentricTriageConfiguration, this.logger, this.logger);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0007649C File Offset: 0x0007469C
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			ArgumentValidator.ThrowIfNull("mailboxInformation", mailboxInformation);
			return this.isInterestingProcessor.IsInteresting(new MailboxProcessorRequest
			{
				LastLogonTime = new DateTime?(mailboxInformation.LastLogonTime),
				IsPublicFolderMailbox = mailboxInformation.IsPublicFolderMailbox(),
				IsGroupMailbox = mailboxInformation.IsGroupMailbox(),
				IsSharedMailbox = mailboxInformation.IsSharedMailbox(),
				IsTeamSiteMailbox = mailboxInformation.IsTeamSiteMailbox(),
				DiagnosticsText = PeopleCentricTriageAssistantType.GetDiagnosticsText(mailboxInformation)
			}, DateTime.UtcNow);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00076518 File Offset: 0x00074718
		public void OnWorkCycleCheckpoint()
		{
			this.logger.TraceDebug((long)this.GetHashCode(), "PeopleCentricTriageAssistantType: work cycle checkpoint.");
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00076531 File Offset: 0x00074731
		public override void OnWorkCycleStart(DatabaseInfo databaseInfo)
		{
			this.logger.TraceDebug<DatabaseInfo>((long)this.GetHashCode(), "PeopleCentricTriageAssistantType: work cycle start for database '{0}'", databaseInfo);
			base.OnWorkCycleStart(databaseInfo);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00076554 File Offset: 0x00074754
		private static string GetDiagnosticsText(MailboxInformation mailboxInformation)
		{
			return string.Format(CultureInfo.InvariantCulture, "{{ Mailbox Display Name:{0}; Mailbox Guid:{1}; }}", new object[]
			{
				mailboxInformation.DisplayName,
				mailboxInformation.MailboxGuid
			});
		}

		// Token: 0x04000C5A RID: 3162
		internal const string AssistantName = "PeopleCentricTriageAssistant";

		// Token: 0x04000C5B RID: 3163
		private const string LoggingComponentName = "PeopleCentricTriageAssistant";

		// Token: 0x04000C5C RID: 3164
		private const string LogFilePrefix = "PeopleCentricTriageAssistant";

		// Token: 0x04000C5D RID: 3165
		private static readonly PeopleCentricTriageConfiguration PeopleCentricTriageConfiguration = new PeopleCentricTriageConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x04000C5E RID: 3166
		private static readonly PropertyTagPropertyDefinition[] NoMailboxExtendedProperties = Array<PropertyTagPropertyDefinition>.Empty;

		// Token: 0x04000C5F RID: 3167
		private readonly PeopleCentricTriageLogger logger;

		// Token: 0x04000C60 RID: 3168
		private readonly MailboxProcessor isInterestingProcessor;
	}
}
