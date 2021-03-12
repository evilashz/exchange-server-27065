using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.InferenceTraining;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.InferenceTraining
{
	// Token: 0x020001C8 RID: 456
	internal sealed class InferenceTrainingAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x00067374 File Offset: 0x00065574
		public InferenceTrainingAssistantType() : this(InferenceTrainingConfiguration.Current, GroupingModelTrainingConfiguration.Current, new InferenceTrainingStatusLogger(InferenceTrainingConfiguration.Current.TrainingStatusLogConfig), new InferenceTruthLabelsStatusLogger(InferenceTrainingConfiguration.Current.TruthLabelsStatusLogConfig), new GroupingModelTrainingStatusLogger(GroupingModelTrainingConfiguration.Current.GroupingModelTrainingStatusLogConfig))
		{
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000673B3 File Offset: 0x000655B3
		public InferenceTrainingAssistantType(InferenceTrainingConfiguration trainingConfig, GroupingModelTrainingConfiguration groupingModelTrainingConfig, InferenceTrainingStatusLogger trainingStatusLogger, InferenceTruthLabelsStatusLogger truthLabelsStatusLogger, GroupingModelTrainingStatusLogger groupingModelStatusLogger)
		{
			this.trainingConfiguration = trainingConfig;
			this.groupingModelTrainingConfiguration = groupingModelTrainingConfig;
			this.trainingStatusLogger = trainingStatusLogger;
			this.truthLabelsStatusLogger = truthLabelsStatusLogger;
			this.groupingModelTrainingStatusLogger = groupingModelStatusLogger;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x000673E0 File Offset: 0x000655E0
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.InferenceTrainingAssistant;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x000673E3 File Offset: 0x000655E3
		public LocalizedString Name
		{
			get
			{
				return Strings.InferenceTrainingAssistantName;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x000673EA File Offset: 0x000655EA
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.InferenceTrainingAssistant;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x000673EE File Offset: 0x000655EE
		public string NonLocalizedName
		{
			get
			{
				return "InferenceTrainingAssistant";
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x000673F5 File Offset: 0x000655F5
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.InferenceTrainingWorkCycle.Read();
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00067401 File Offset: 0x00065601
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.InferenceTrainingWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0006740D File Offset: 0x0006560D
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00067410 File Offset: 0x00065610
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForInferenceTrainingAssistant;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00067417 File Offset: 0x00065617
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return InferenceTrainingAssistantType.mailboxExtendedProperties;
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00067420 File Offset: 0x00065620
		public static bool IsAssistantEnabled()
		{
			if (InferenceTrainingAssistantType.isEnabled == null)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
				{
					if (registryKey == null)
					{
						InferenceTrainingAssistantType.isEnabled = new bool?(false);
					}
					else
					{
						object value = registryKey.GetValue("InferenceTrainingAssistantEnabledOverride");
						InferenceTrainingAssistantType.isEnabled = new bool?(value is int && (int)value == 1);
					}
				}
			}
			return InferenceTrainingAssistantType.isEnabled.Value && VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.InferenceTrainingAssistant.Enabled;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000674C4 File Offset: 0x000656C4
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			bool flag = true;
			string text = null;
			DateTime? dateTime = mailboxInformation.GetMailboxProperty(MailboxSchema.InferenceTrainingLastAttemptTimestamp) as DateTime?;
			DateTime? dateTime2 = mailboxInformation.GetMailboxProperty(MailboxSchema.InferenceTrainingLastSuccessTimestamp) as DateTime?;
			DateTime? dateTime3 = mailboxInformation.GetMailboxProperty(MailboxSchema.InferenceTruthLoggingLastAttemptTimestamp) as DateTime?;
			DateTime? dateTime4 = mailboxInformation.GetMailboxProperty(MailboxSchema.InferenceTruthLoggingLastSuccessTimestamp) as DateTime?;
			if (InferenceCommonUtility.IsNonUserMailbox(mailboxInformation.DisplayName))
			{
				text = string.Format("Reason=NonUserMailbox#Name={0}", mailboxInformation.DisplayName);
				flag = false;
			}
			if (flag && dateTime == null)
			{
				int? num = mailboxInformation.GetMailboxProperty(MailboxSchema.ItemCount) as int?;
				if (num != null && num < this.trainingConfiguration.MinNumberOfItemsForRetrospectiveTraining)
				{
					text = string.Format("Reason=LowItemCount#ItemCount={0}#Threshold={1}", num, this.trainingConfiguration.MinNumberOfItemsForRetrospectiveTraining);
					flag = false;
				}
			}
			if (flag)
			{
				string text2 = string.Format("ControlDataLastProcessedTime={0}", mailboxInformation.LastProcessedDate);
				this.trainingStatusLogger.LogStatus(mailboxInformation.MailboxGuid, 0, null, dateTime, dateTime2, text2);
				this.truthLabelsStatusLogger.LogStatus(mailboxInformation.MailboxGuid, 0, null, dateTime3, dateTime4, text2);
				this.groupingModelTrainingStatusLogger.LogStatus(mailboxInformation.MailboxGuid, 0, text2);
			}
			else
			{
				this.trainingStatusLogger.LogStatus(mailboxInformation.MailboxGuid, 4, new DateTime?(DateTime.UtcNow), null, null, text);
				this.truthLabelsStatusLogger.LogStatus(mailboxInformation.MailboxGuid, 4, new DateTime?(DateTime.UtcNow), null, null, text);
				this.groupingModelTrainingStatusLogger.LogStatus(mailboxInformation.MailboxGuid, 4, text);
			}
			return flag;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000676AF File Offset: 0x000658AF
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new InferenceTrainingAssistant(databaseInfo, this.Name, this.NonLocalizedName, this.trainingStatusLogger, this.truthLabelsStatusLogger, this.groupingModelTrainingConfiguration, this.groupingModelTrainingStatusLogger, InferenceTrainingAssistantType.IsTruthLabelsLoggingEnabled(false));
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000676E1 File Offset: 0x000658E1
		public override void OnWorkCycleStart(DatabaseInfo databaseInfo)
		{
			this.trainingConfiguration = InferenceTrainingConfiguration.Current;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000676EE File Offset: 0x000658EE
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000676F0 File Offset: 0x000658F0
		internal static bool IsTruthLabelsLoggingEnabled(bool forceRefresh)
		{
			if (InferenceTrainingAssistantType.isTruthLabelsLoggingEnabled == null || forceRefresh)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference"))
				{
					if (registryKey == null)
					{
						InferenceTrainingAssistantType.isTruthLabelsLoggingEnabled = new bool?(true);
					}
					else
					{
						object value = registryKey.GetValue("TruthLabelsLoggingEnabled");
						if (value != null && value is int && (int)value == 0)
						{
							InferenceTrainingAssistantType.isTruthLabelsLoggingEnabled = new bool?(false);
						}
						else
						{
							InferenceTrainingAssistantType.isTruthLabelsLoggingEnabled = new bool?(true);
						}
					}
				}
			}
			return InferenceTrainingAssistantType.isTruthLabelsLoggingEnabled.Value;
		}

		// Token: 0x04000AF5 RID: 2805
		internal const string AssistantName = "InferenceTrainingAssistant";

		// Token: 0x04000AF6 RID: 2806
		private const string AssistantEnabledRegKey = "InferenceTrainingAssistantEnabledOverride";

		// Token: 0x04000AF7 RID: 2807
		private const string RegistryKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Inference";

		// Token: 0x04000AF8 RID: 2808
		private const string TruthLabelsLoggingEnabledRegKey = "TruthLabelsLoggingEnabled";

		// Token: 0x04000AF9 RID: 2809
		private static bool? isEnabled = null;

		// Token: 0x04000AFA RID: 2810
		private static bool? isTruthLabelsLoggingEnabled = null;

		// Token: 0x04000AFB RID: 2811
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x04000AFC RID: 2812
		private static readonly PropertyTagPropertyDefinition[] mailboxExtendedProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.ItemCount,
			MailboxSchema.InferenceTrainingLastAttemptTimestamp,
			MailboxSchema.InferenceTrainingLastSuccessTimestamp,
			MailboxSchema.InferenceTruthLoggingLastAttemptTimestamp,
			MailboxSchema.InferenceTruthLoggingLastSuccessTimestamp
		};

		// Token: 0x04000AFD RID: 2813
		private InferenceTrainingConfiguration trainingConfiguration;

		// Token: 0x04000AFE RID: 2814
		private readonly GroupingModelTrainingConfiguration groupingModelTrainingConfiguration;

		// Token: 0x04000AFF RID: 2815
		private readonly InferenceTrainingStatusLogger trainingStatusLogger;

		// Token: 0x04000B00 RID: 2816
		private readonly InferenceTruthLabelsStatusLogger truthLabelsStatusLogger;

		// Token: 0x04000B01 RID: 2817
		private readonly GroupingModelTrainingStatusLogger groupingModelTrainingStatusLogger;
	}
}
