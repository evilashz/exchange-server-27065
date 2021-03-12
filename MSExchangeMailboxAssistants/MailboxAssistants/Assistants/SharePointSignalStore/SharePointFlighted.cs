using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000227 RID: 551
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SharePointFlighted
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x000779CC File Offset: 0x00075BCC
		public SharePointFlighted(ILogger logger)
		{
			this.logger = logger;
			this.IsUserEnabled = new Func<ADUser, bool>(SharePointFlighted.GetIsUserEnabledFromExchangeConfiguration);
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x000779ED File Offset: 0x00075BED
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x000779F5 File Offset: 0x00075BF5
		public Func<ADUser, bool> IsUserEnabled { get; set; }

		// Token: 0x060014CF RID: 5327 RVA: 0x00077A00 File Offset: 0x00075C00
		public bool IsUserFlighted(ADUser user)
		{
			try
			{
				if (!this.IsUserEnabled(user))
				{
					this.logger.LogInfo("User not flighted to send recipient signals to SharePoint", new object[0]);
					return false;
				}
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
				this.logger.LogInfo("Aborting, unable to determine flight information for user: {0}", new object[]
				{
					ex
				});
				return false;
			}
			return true;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00077A88 File Offset: 0x00075C88
		private static bool GetIsUserEnabledFromExchangeConfiguration(ADUser user)
		{
			return VariantConfiguration.GetSnapshot(user.GetContext(null), null, null).MailboxAssistants.SharePointSignalStore.Enabled;
		}

		// Token: 0x04000C90 RID: 3216
		private readonly ILogger logger;
	}
}
