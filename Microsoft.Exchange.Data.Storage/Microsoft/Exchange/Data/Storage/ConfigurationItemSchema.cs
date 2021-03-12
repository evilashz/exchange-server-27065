using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C3F RID: 3135
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigurationItemSchema : ItemSchema
	{
		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x06006EF6 RID: 28406 RVA: 0x001DD870 File Offset: 0x001DBA70
		public new static ConfigurationItemSchema Instance
		{
			get
			{
				if (ConfigurationItemSchema.instance == null)
				{
					ConfigurationItemSchema.instance = new ConfigurationItemSchema();
				}
				return ConfigurationItemSchema.instance;
			}
		}

		// Token: 0x06006EF7 RID: 28407 RVA: 0x001DD888 File Offset: 0x001DBA88
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			base.CoreObjectUpdate(coreItem, operation);
			this.PrepareAggregatedUserConfigurationUpdate(coreItem);
		}

		// Token: 0x06006EF8 RID: 28408 RVA: 0x001DD899 File Offset: 0x001DBA99
		internal override void CoreObjectUpdateComplete(CoreItem coreItem, SaveResult saveResult)
		{
			base.CoreObjectUpdateComplete(coreItem, saveResult);
			this.CompleteAggregatedUserConfigurationUpdates(coreItem, saveResult);
		}

		// Token: 0x06006EF9 RID: 28409 RVA: 0x001DD8AC File Offset: 0x001DBAAC
		private void PrepareAggregatedUserConfigurationUpdate(CoreItem coreItem)
		{
			if (!coreItem.CoreObjectUpdateContext.ContainsKey(this) && this.IsEnabledForConfigurationAggregation(coreItem))
			{
				IList<IAggregatedUserConfigurationWriter> writers = AggregatedUserConfiguration.GetWriters(AggregatedUserConfigurationSchema.Instance, coreItem.Session as IMailboxSession, coreItem);
				if (writers != null)
				{
					coreItem.CoreObjectUpdateContext[this] = writers;
					foreach (IAggregatedUserConfigurationWriter aggregatedUserConfigurationWriter in writers)
					{
						aggregatedUserConfigurationWriter.Prepare();
					}
				}
			}
		}

		// Token: 0x06006EFA RID: 28410 RVA: 0x001DD934 File Offset: 0x001DBB34
		private void CompleteAggregatedUserConfigurationUpdates(CoreItem coreItem, SaveResult saveResult)
		{
			if (saveResult == SaveResult.Success || saveResult == SaveResult.SuccessWithConflictResolution)
			{
				object obj = null;
				if (coreItem.CoreObjectUpdateContext.TryGetValue(this, out obj))
				{
					coreItem.CoreObjectUpdateContext.Remove(this);
					IList<IAggregatedUserConfigurationWriter> list = obj as IList<IAggregatedUserConfigurationWriter>;
					if (list != null)
					{
						foreach (IAggregatedUserConfigurationWriter aggregatedUserConfigurationWriter in list)
						{
							aggregatedUserConfigurationWriter.Commit();
						}
					}
				}
			}
		}

		// Token: 0x06006EFB RID: 28411 RVA: 0x001DD9AC File Offset: 0x001DBBAC
		private bool IsEnabledForConfigurationAggregation(CoreItem coreItem)
		{
			return coreItem.Session != null && coreItem.Session.MailboxOwner != null && ConfigurationItemSchema.IsEnabledForConfigurationAggregation(coreItem.Session.MailboxOwner);
		}

		// Token: 0x06006EFC RID: 28412 RVA: 0x001DD9D8 File Offset: 0x001DBBD8
		internal static bool IsEnabledForConfigurationAggregation(IExchangePrincipal principal)
		{
			bool result = false;
			if (principal != null && !principal.ObjectId.IsNullOrEmpty())
			{
				result = principal.GetConfiguration().DataStorage.UserConfigurationAggregation.Enabled;
			}
			return result;
		}

		// Token: 0x0400422B RID: 16939
		[Autoload]
		internal static readonly StorePropertyDefinition UserConfigurationType = InternalSchema.UserConfigurationType;

		// Token: 0x0400422C RID: 16940
		[Autoload]
		internal static readonly StorePropertyDefinition UserConfigurationDictionary = InternalSchema.UserConfigurationDictionary;

		// Token: 0x0400422D RID: 16941
		[Autoload]
		internal static readonly StorePropertyDefinition UserConfigurationStream = InternalSchema.UserConfigurationStream;

		// Token: 0x0400422E RID: 16942
		[Autoload]
		public static readonly StorePropertyDefinition UserConfigurationXml = InternalSchema.UserConfigurationXml;

		// Token: 0x0400422F RID: 16943
		private static ConfigurationItemSchema instance = null;
	}
}
