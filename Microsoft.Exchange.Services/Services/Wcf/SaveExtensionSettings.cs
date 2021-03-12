using System;
using System.Collections;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000935 RID: 2357
	internal class SaveExtensionSettings : ServiceCommand<SaveExtensionSettingsResponse>
	{
		// Token: 0x06004458 RID: 17496 RVA: 0x000EA490 File Offset: 0x000E8690
		public SaveExtensionSettings(CallContext callContext, string extensionId, string extensionVersion, string settings) : base(callContext)
		{
			this.extensionId = extensionId;
			this.extensionVersion = extensionVersion;
			this.settings = settings;
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x000EA4B0 File Offset: 0x000E86B0
		protected override SaveExtensionSettingsResponse InternalExecute()
		{
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			SaveExtensionSettingsResponse result;
			using (UserConfiguration folderConfiguration = UserConfigurationHelper.GetFolderConfiguration(mailboxIdentityMailboxSession, ExtensionPackageManager.GetExtensionFolderId(mailboxIdentityMailboxSession), ExtensionPackageManager.GetFaiName(this.extensionId, this.extensionVersion), UserConfigurationTypes.Dictionary, true, false))
			{
				IDictionary dictionary = folderConfiguration.GetDictionary();
				dictionary["ExtensionSettings"] = this.settings;
				folderConfiguration.Save();
				result = new SaveExtensionSettingsResponse();
			}
			return result;
		}

		// Token: 0x040027D1 RID: 10193
		private readonly string extensionId;

		// Token: 0x040027D2 RID: 10194
		private readonly string extensionVersion;

		// Token: 0x040027D3 RID: 10195
		private readonly string settings;
	}
}
