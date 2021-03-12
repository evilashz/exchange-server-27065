using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Clutter;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003DF RID: 991
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InferenceSettingsType : UserConfigurationBaseType
	{
		// Token: 0x06001FC5 RID: 8133 RVA: 0x00078106 File Offset: 0x00076306
		public InferenceSettingsType() : base(InferenceSettingsType.ConfigurationName)
		{
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00078113 File Offset: 0x00076313
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x00078125 File Offset: 0x00076325
		[DataMember]
		public bool? IsClutterUIEnabled
		{
			get
			{
				return (bool?)base[UserConfigurationPropertyId.IsClutterUIEnabled];
			}
			set
			{
				base[UserConfigurationPropertyId.IsClutterUIEnabled] = value;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x00078138 File Offset: 0x00076338
		internal override UserConfigurationPropertySchemaBase Schema
		{
			get
			{
				return InferenceSettingsPropertySchema.Instance;
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0007813F File Offset: 0x0007633F
		internal override IUserConfigurationAccessStrategy CreateConfigurationAccessStrategy()
		{
			return new DefaultFolderConfigurationAccessStrategy(DefaultFolderType.Inbox);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00078148 File Offset: 0x00076348
		internal static bool GetFeatureSupportedState(MailboxSession mailboxSession, UserContext userContext)
		{
			bool enabled = userContext.FeaturesManager.ServerSettings.InferenceUI.Enabled;
			bool flag = false;
			if (enabled)
			{
				flag = true;
			}
			return enabled && flag;
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0007817C File Offset: 0x0007637C
		internal bool TryMigrateUserOptionsValue(MailboxSession mailboxSession, UserOptionsType userOptions)
		{
			bool result = false;
			if ((userOptions.UserOptionsMigrationState & UserOptionsMigrationState.ShowInferenceUiElementsMigrated) == UserOptionsMigrationState.None)
			{
				try
				{
					if (this.IsClutterUIEnabled == null && !userOptions.ShowInferenceUiElements)
					{
						ExTraceGlobals.UserOptionsDataTracer.TraceDebug((long)this.GetHashCode(), "Updating Inference.Settings IsClutterUIEnabled for upgrade scenario in TryMigrateUserOptionsValue.");
						this.IsClutterUIEnabled = new bool?(userOptions.ShowInferenceUiElements);
						base.Commit(mailboxSession, new UserConfigurationPropertyDefinition[]
						{
							this.Schema.GetPropertyDefinition(UserConfigurationPropertyId.IsClutterUIEnabled)
						});
					}
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug((long)this.GetHashCode(), "Updating OWA.Settings UserOptionsMigrationState to reflect new ShowInferenceUiElementsMigrated value in TryMigrateUserOptionsValue.");
					userOptions.UserOptionsMigrationState |= UserOptionsMigrationState.ShowInferenceUiElementsMigrated;
					userOptions.Commit(mailboxSession, new UserConfigurationPropertyDefinition[]
					{
						UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.UserOptionsMigrationState)
					});
					result = true;
				}
				catch (StoragePermanentException arg)
				{
					ExTraceGlobals.UserOptionsDataTracer.TraceError<StoragePermanentException, string>(0L, "Permanent error while trying to migrate Inference settings in TryMigrateUserOptionsValue. Error: {0}. User: {1}", arg, mailboxSession.DisplayAddress);
				}
				catch (StorageTransientException arg2)
				{
					ExTraceGlobals.UserOptionsDataTracer.TraceError<StorageTransientException, string>(0L, "Transient error while trying to migrate Inference settings in TryMigrateUserOptionsValue. Error: {0}. User: {1}.", arg2, mailboxSession.DisplayAddress);
				}
				catch (Exception arg3)
				{
					ExTraceGlobals.UserContextCallTracer.TraceError<Exception, string>(0L, "Unexpected error while trying to migrate Inference settings in TryMigrateUserOptionsValue. Error: {0}. User: {1}.", arg3, mailboxSession.DisplayAddress);
				}
			}
			return result;
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000782C0 File Offset: 0x000764C0
		internal void LoadAll(MailboxSession session)
		{
			IList<UserConfigurationPropertyDefinition> properties = new List<UserConfigurationPropertyDefinition>(base.OptionProperties.Keys);
			base.Load(session, properties, true);
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000782E8 File Offset: 0x000764E8
		internal static void UpdateUserPreferenceFlag(MailboxSession mailboxSession, UserContext userContext, bool enable)
		{
			if (userContext.FeaturesManager.ClientServerSettings.FolderBasedClutter.Enabled)
			{
				InferenceSettingsType.UpdateClutterClassificationEnabled(mailboxSession, userContext.FeaturesManager.ConfigurationSnapshot, enable);
				return;
			}
			if (InferenceSettingsType.GetFeatureSupportedState(mailboxSession, userContext))
			{
				new InferenceSettingsType
				{
					IsClutterUIEnabled = new bool?(enable)
				}.Commit(mailboxSession, new UserConfigurationPropertyDefinition[]
				{
					InferenceSettingsPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.IsClutterUIEnabled)
				});
			}
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x00078360 File Offset: 0x00076560
		internal static void UpdateClutterClassificationEnabled(MailboxSession mailboxSession, VariantConfigurationSnapshot configurationSnapshot, bool enable)
		{
			bool flag = ClutterUtilities.IsClassificationEnabled(mailboxSession, configurationSnapshot);
			if (!flag && enable)
			{
				ClutterUtilities.OptUserIn(mailboxSession, configurationSnapshot, new FrontEndLocator());
				return;
			}
			if (flag && !enable)
			{
				ClutterUtilities.OptUserOut(mailboxSession, configurationSnapshot, new FrontEndLocator());
			}
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0007839B File Offset: 0x0007659B
		internal static void ReadFolderBasedClutterSettings(MailboxSession mailboxSession, VariantConfigurationSnapshot configurationSnapshot, OwaUserConfiguration userConfiguration)
		{
			userConfiguration.SegmentationSettings.PredictedActions = ClutterUtilities.IsClutterEnabled(mailboxSession, configurationSnapshot);
			userConfiguration.UserOptions.ShowInferenceUiElements = ClutterUtilities.IsClassificationEnabled(mailboxSession, configurationSnapshot);
		}

		// Token: 0x04001208 RID: 4616
		private static string ConfigurationName = "Inference.Settings";
	}
}
