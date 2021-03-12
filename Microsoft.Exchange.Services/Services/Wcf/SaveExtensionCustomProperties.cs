using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000934 RID: 2356
	internal class SaveExtensionCustomProperties : ServiceCommand<SaveExtensionCustomPropertiesResponse>
	{
		// Token: 0x06004456 RID: 17494 RVA: 0x000EA2DA File Offset: 0x000E84DA
		public SaveExtensionCustomProperties(CallContext callContext, string extensionId, string itemId, string customProperties) : base(callContext)
		{
			this.extensionId = extensionId;
			this.itemId = itemId;
			this.customProperties = customProperties;
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x000EA2FC File Offset: 0x000E84FC
		protected override SaveExtensionCustomPropertiesResponse InternalExecute()
		{
			Item item = null;
			try
			{
				MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
				IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(this.itemId, BasicTypes.Item, null, false);
				StoreObjectId storeId = idHeaderInformation.ToStoreObjectId();
				string text = "cecp-" + this.extensionId;
				GuidNamePropertyDefinition guidNamePropertyDefinition = GuidNamePropertyDefinition.CreateCustom(text, typeof(string), LoadExtensionCustomProperties.ClientExtensibilityGuid, text, PropertyFlags.None);
				item = Item.Bind(mailboxIdentityMailboxSession, storeId, new PropertyDefinition[]
				{
					guidNamePropertyDefinition,
					LoadExtensionCustomProperties.CustomPropertyNamesListDefinition
				});
				item.OpenAsReadWrite();
				item.SafeSetProperty(guidNamePropertyDefinition, this.customProperties);
				try
				{
					string valueOrDefault = item.GetValueOrDefault<string>(LoadExtensionCustomProperties.CustomPropertyNamesListDefinition, string.Empty);
					if (valueOrDefault.IndexOf(text, StringComparison.OrdinalIgnoreCase) == -1)
					{
						string text2 = valueOrDefault + text + ";";
						if (text2.Length < 16000)
						{
							item.SafeSetProperty(LoadExtensionCustomProperties.CustomPropertyNamesListDefinition, text2);
						}
					}
				}
				catch (StoragePermanentException)
				{
				}
				item.Save(SaveMode.ResolveConflicts);
			}
			catch (StorageTransientException)
			{
				return new SaveExtensionCustomPropertiesResponse(CoreResources.SaveExtensionCustomPropertiesFailed);
			}
			catch (StoragePermanentException)
			{
				return new SaveExtensionCustomPropertiesResponse(CoreResources.SaveExtensionCustomPropertiesFailed);
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
				}
			}
			return new SaveExtensionCustomPropertiesResponse();
		}

		// Token: 0x040027CE RID: 10190
		private readonly string extensionId;

		// Token: 0x040027CF RID: 10191
		private readonly string itemId;

		// Token: 0x040027D0 RID: 10192
		private readonly string customProperties;
	}
}
