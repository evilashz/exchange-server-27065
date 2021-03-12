using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000221 RID: 545
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ADWithDictFallbackSharePointUrl : ISharePointUrl
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x0007722A File Offset: 0x0007542A
		public ADWithDictFallbackSharePointUrl(ISharePointUrl adSharePointUrl, ISharePointUrl fallbackSharePointUrl, ILogger logger)
		{
			this.adUrl = adSharePointUrl;
			this.fallbackUrl = fallbackSharePointUrl;
			this.logger = logger2;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0007726C File Offset: 0x0007546C
		public string GetUrl(IExchangePrincipal userIdentity, IRecipientSession recipientSession)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			string url = this.adUrl.GetUrl(userIdentity, recipientSession);
			this.LogTenantIdAction(this.logger, userIdentity, recipientSession);
			if (string.IsNullOrWhiteSpace(url))
			{
				this.logger.LogInfo("No SharePoint URL available in AD, using local dictionary as fallback", new object[0]);
				stopwatch.Restart();
				url = this.fallbackUrl.GetUrl(userIdentity, recipientSession);
				if (string.IsNullOrWhiteSpace(url))
				{
					this.logger.LogInfo("No SharePoint URL available, skipping processing", new object[0]);
					return null;
				}
				this.logger.LogInfo("Retrieved SharePoint Url from local dictionary (used {0} seconds)", new object[]
				{
					stopwatch.Elapsed.TotalSeconds
				});
			}
			else
			{
				this.logger.LogInfo("Retrieved SharePoint Url from AD (used {0} seconds)", new object[]
				{
					stopwatch.Elapsed.TotalSeconds
				});
			}
			return url;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00077358 File Offset: 0x00075558
		private static void LogTenantId(ILogger logger, IExchangePrincipal userIdentity, IRecipientSession recipientSession)
		{
			Guid tenantGuid = ADWithDictFallbackSharePointUrl.GetTenantGuid(userIdentity, recipientSession);
			logger.LogInfo("Fetching SharePoint URL for tenant {0}", new object[]
			{
				tenantGuid
			});
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0007738C File Offset: 0x0007558C
		private static Guid GetTenantGuid(IExchangePrincipal userIdentity, IRecipientSession recipientSession)
		{
			ADUser aduser = (ADUser)DirectoryHelper.ReadADRecipient(userIdentity.MailboxInfo.MailboxGuid, userIdentity.MailboxInfo.IsArchive, recipientSession);
			if (aduser == null)
			{
				throw new ArgumentNullException("user");
			}
			if (aduser.OrganizationId == null)
			{
				throw new ArgumentNullException("user.OrganizationId");
			}
			return aduser.OrganizationId.GetTenantGuid();
		}

		// Token: 0x04000C79 RID: 3193
		internal Action<ILogger, IExchangePrincipal, IRecipientSession> LogTenantIdAction = delegate(ILogger logger, IExchangePrincipal userIdentity, IRecipientSession recipientSession)
		{
			ADWithDictFallbackSharePointUrl.LogTenantId(logger, userIdentity, recipientSession);
		};

		// Token: 0x04000C7A RID: 3194
		private readonly ILogger logger;

		// Token: 0x04000C7B RID: 3195
		private readonly ISharePointUrl fallbackUrl;

		// Token: 0x04000C7C RID: 3196
		private readonly ISharePointUrl adUrl;
	}
}
