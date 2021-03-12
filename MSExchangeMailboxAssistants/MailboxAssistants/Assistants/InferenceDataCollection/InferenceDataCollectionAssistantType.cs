using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceDataCollection;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Common.Diagnostics;
using Microsoft.Exchange.Inference.DataAnalysis;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.InferenceDataCollection
{
	// Token: 0x02000216 RID: 534
	internal sealed class InferenceDataCollectionAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x0600145F RID: 5215 RVA: 0x00075C98 File Offset: 0x00073E98
		public InferenceDataCollectionAssistantType()
		{
			this.config = Configuration.Current;
			this.diagnosticLogger = new DiagnosticLogger(this.config.DiagnosticLogConfig, InferenceDataCollectionAssistantType.tracer, 0);
			this.collectionContext = new CollectionContext(this.config, this.diagnosticLogger);
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00075CF4 File Offset: 0x00073EF4
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.InferenceDataCollectionAssistant;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00075CF8 File Offset: 0x00073EF8
		public LocalizedString Name
		{
			get
			{
				return Strings.inferenceDataCollectionAssistantName;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00075CFF File Offset: 0x00073EFF
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.InferenceDataCollectionAssistant;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00075D03 File Offset: 0x00073F03
		public string NonLocalizedName
		{
			get
			{
				return "InferenceDataCollectionAssistant";
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00075D0A File Offset: 0x00073F0A
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.InferenceDataCollectionWorkCycle.Read();
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00075D16 File Offset: 0x00073F16
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.InferenceDataCollectionWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00075D22 File Offset: 0x00073F22
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00075D25 File Offset: 0x00073F25
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForInferenceDataCollectionAssistant;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00075D2C File Offset: 0x00073F2C
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return InferenceDataCollectionAssistantType.mailboxExtendedProperties;
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00075D34 File Offset: 0x00073F34
		public static bool IsAssistantEnabled()
		{
			if (InferenceDataCollectionAssistantType.isEnabled == null)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters", false))
				{
					if (registryKey == null)
					{
						InferenceDataCollectionAssistantType.isEnabled = new bool?(false);
					}
					else
					{
						object value = registryKey.GetValue("InferenceDataCollectionAssistantEnabledOverride", null);
						InferenceDataCollectionAssistantType.isEnabled = new bool?(value is int && (int)value == 1);
					}
				}
			}
			return InferenceDataCollectionAssistantType.isEnabled.Value;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00075DC0 File Offset: 0x00073FC0
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			InferenceDataCollectionAssistantType.tracer.TraceFunction((long)this.GetHashCode(), "InferenceDataCollectionAssistantType.IsMailboxInteresting");
			if (InferenceCommonUtility.IsNonUserMailbox(mailboxInformation.DisplayName))
			{
				this.diagnosticLogger.LogDebug("Skipping mailbox with guid {0} and display name {1} since it is identified as a non-user mailbox.", new object[]
				{
					mailboxInformation.MailboxGuid,
					mailboxInformation.DisplayName
				});
				return false;
			}
			if (this.collectionContext.Configuration.IsOutputSanitized)
			{
				if (!this.collectionContext.HashProvider.IsInitialized && !this.hashProviderInitializeAttemptedInWorkcycle)
				{
					try
					{
						bool flag = this.collectionContext.HashProvider.Initialize();
						this.diagnosticLogger.LogDebug("Hash provider initialization success = {0}", new object[]
						{
							flag
						});
					}
					catch (Exception ex)
					{
						this.diagnosticLogger.LogDebug("Hash provider initialization failed with exception {0}", new object[]
						{
							ex.ToString()
						});
					}
					this.hashProviderInitializeAttemptedInWorkcycle = true;
				}
				if (!this.collectionContext.HashProvider.IsInitialized)
				{
					this.diagnosticLogger.LogDebug("Skipping mailbox with guid {0} and display name {1} as the hash provider is not initialized.", new object[]
					{
						mailboxInformation.MailboxGuid,
						mailboxInformation.DisplayName
					});
					return false;
				}
			}
			object mailboxProperty = mailboxInformation.GetMailboxProperty(MailboxSchema.InferenceDataCollectionProcessingState);
			if (mailboxProperty != null)
			{
				byte[] array = mailboxProperty as byte[];
				ExDateTime t = new ExDateTime(ExTimeZone.UtcTimeZone, mailboxInformation.LastProcessedDate).Add(this.config.MailboxReprocessAge);
				if (array[0] == 1 && ExDateTime.UtcNow < t)
				{
					this.diagnosticLogger.LogDebug("Skipping mailbox with guid {0} and display name {1} since it reached {2} collection state on {3}", new object[]
					{
						mailboxInformation.MailboxGuid,
						mailboxInformation.DisplayName,
						(InferenceDataCollectionAssistantType.DataCollectionProcessingState)array[0],
						mailboxInformation.LastProcessedDate
					});
					return false;
				}
			}
			mailboxProperty = mailboxInformation.GetMailboxProperty(MailboxSchema.ItemCount);
			if (mailboxProperty != null && (int)mailboxProperty > this.config.MinimumItemCountInMailbox)
			{
				int num = InferenceDataCollectionAssistantType.IsGroupMailbox(mailboxInformation) ? this.config.ModuloNumberToRandomizeForGroups : this.config.ModuloNumberToRandomize;
				int num2 = this.randomizer.Next(1, int.MaxValue);
				bool flag2 = num2 % num == 0;
				this.diagnosticLogger.LogDebug("{0} mailbox with guid {1} and display name {2} based on random selection (random number = {3}, modulo arithmetic number = {4})", new object[]
				{
					flag2 ? "Picking" : "Skipping",
					mailboxInformation.MailboxGuid,
					mailboxInformation.DisplayName,
					num2,
					this.config.ModuloNumberToRandomize
				});
				return flag2;
			}
			this.diagnosticLogger.LogDebug("Skipping mailbox with guid {0} and display name {1} since ContentCount property value {2} is less than the required minimum {3}", new object[]
			{
				mailboxInformation.MailboxGuid,
				mailboxInformation.DisplayName,
				(mailboxProperty != null) ? ((int)mailboxProperty) : -1,
				this.config.MinimumItemCountInMailbox
			});
			return false;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000760E0 File Offset: 0x000742E0
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			InferenceDataCollectionAssistantType.tracer.TraceFunction((long)this.GetHashCode(), "InferenceDataCollectionAssistantType.CreateInstance");
			return new InferenceDataCollectionAssistant(databaseInfo, this.Name, this.NonLocalizedName, this.diagnosticLogger, this.collectionContext);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00076116 File Offset: 0x00074316
		public void OnWorkCycleCheckpoint()
		{
			InferenceDataCollectionAssistantType.tracer.TraceFunction((long)this.GetHashCode(), "InferenceDataCollectionAssistantType.OnWorkCycleCheckpoint");
			this.hashProviderInitializeAttemptedInWorkcycle = false;
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00076135 File Offset: 0x00074335
		private static bool IsGroupMailbox(MailboxInformation mailboxInformation)
		{
			return mailboxInformation.IsSharedMailbox() || mailboxInformation.IsGroupMailbox() || mailboxInformation.IsTeamSiteMailbox();
		}

		// Token: 0x04000C4A RID: 3146
		internal const string AssistantName = "InferenceDataCollectionAssistant";

		// Token: 0x04000C4B RID: 3147
		private static bool? isEnabled = null;

		// Token: 0x04000C4C RID: 3148
		private static readonly PropertyTagPropertyDefinition[] mailboxExtendedProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.InferenceDataCollectionProcessingState,
			MailboxSchema.ItemCount
		};

		// Token: 0x04000C4D RID: 3149
		private static readonly Trace tracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x04000C4E RID: 3150
		private readonly Configuration config;

		// Token: 0x04000C4F RID: 3151
		private readonly IDiagnosticLogger diagnosticLogger;

		// Token: 0x04000C50 RID: 3152
		private readonly ICollectionContext collectionContext;

		// Token: 0x04000C51 RID: 3153
		private Random randomizer = new Random();

		// Token: 0x04000C52 RID: 3154
		private bool hashProviderInitializeAttemptedInWorkcycle;

		// Token: 0x02000217 RID: 535
		[Flags]
		internal enum DataCollectionProcessingState : byte
		{
			// Token: 0x04000C54 RID: 3156
			Processing = 0,
			// Token: 0x04000C55 RID: 3157
			Processed = 1
		}
	}
}
