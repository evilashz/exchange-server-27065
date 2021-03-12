using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200092A RID: 2346
	internal class LoadExtensionCustomProperties : ServiceCommand<LoadExtensionCustomPropertiesResponse>
	{
		// Token: 0x060043F8 RID: 17400 RVA: 0x000E7DB7 File Offset: 0x000E5FB7
		public LoadExtensionCustomProperties(CallContext callContext, string extensionId, string itemId) : base(callContext)
		{
			this.extensionId = extensionId;
			this.itemId = itemId;
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x000E7DD0 File Offset: 0x000E5FD0
		protected override LoadExtensionCustomPropertiesResponse InternalExecute()
		{
			string text = null;
			string customPropertyNames = null;
			Item item = null;
			try
			{
				MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
				IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(this.itemId, BasicTypes.Item, null, false);
				StoreObjectId storeId = idHeaderInformation.ToStoreObjectId();
				string text2 = "cecp-" + this.extensionId;
				GuidNamePropertyDefinition guidNamePropertyDefinition = GuidNamePropertyDefinition.CreateCustom(text2, typeof(string), LoadExtensionCustomProperties.ClientExtensibilityGuid, text2, PropertyFlags.None);
				item = Item.Bind(mailboxIdentityMailboxSession, storeId, new PropertyDefinition[]
				{
					guidNamePropertyDefinition,
					LoadExtensionCustomProperties.CustomPropertyNamesListDefinition
				});
				text = item.GetValueOrDefault<string>(guidNamePropertyDefinition, "{}");
				try
				{
					customPropertyNames = item.GetValueOrDefault<string>(LoadExtensionCustomProperties.CustomPropertyNamesListDefinition, string.Empty);
				}
				catch (StoragePermanentException)
				{
				}
				if (text.Length > 2500)
				{
					return new LoadExtensionCustomPropertiesResponse(null, null, CoreResources.LoadExtensionCustomPropertiesFailed);
				}
			}
			catch (StorageTransientException)
			{
				return new LoadExtensionCustomPropertiesResponse(null, null, CoreResources.LoadExtensionCustomPropertiesFailed);
			}
			catch (StoragePermanentException)
			{
				return new LoadExtensionCustomPropertiesResponse(null, null, CoreResources.LoadExtensionCustomPropertiesFailed);
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
				}
			}
			return new LoadExtensionCustomPropertiesResponse(text, customPropertyNames, null);
		}

		// Token: 0x040027A4 RID: 10148
		internal const string CustomPropertyNamesPropertyName = "cecp-propertyNames";

		// Token: 0x040027A5 RID: 10149
		internal const int CustomPropertyNamesCharacterLimit = 16000;

		// Token: 0x040027A6 RID: 10150
		internal const string CustomPropertyNamesDelimiter = ";";

		// Token: 0x040027A7 RID: 10151
		internal const string NamePrefix = "cecp-";

		// Token: 0x040027A8 RID: 10152
		internal static readonly Guid ClientExtensibilityGuid = ExtendedPropertyUri.PSETIDPublicStrings;

		// Token: 0x040027A9 RID: 10153
		internal static readonly GuidNamePropertyDefinition CustomPropertyNamesListDefinition = GuidNamePropertyDefinition.CreateCustom("cecp-propertyNames", typeof(string), LoadExtensionCustomProperties.ClientExtensibilityGuid, "cecp-propertyNames", PropertyFlags.None);

		// Token: 0x040027AA RID: 10154
		private readonly string extensionId;

		// Token: 0x040027AB RID: 10155
		private readonly string itemId;
	}
}
