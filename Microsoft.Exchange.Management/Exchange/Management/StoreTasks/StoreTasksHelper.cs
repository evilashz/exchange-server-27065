using System;
using System.Globalization;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000798 RID: 1944
	public static class StoreTasksHelper
	{
		// Token: 0x06004484 RID: 17540 RVA: 0x00118F47 File Offset: 0x00117147
		internal static MailboxSession OpenMailboxSession(ExchangePrincipal principal, string taskName)
		{
			return StoreTasksHelper.OpenMailboxSession(principal, taskName, false);
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x00118F54 File Offset: 0x00117154
		internal static MailboxSession OpenMailboxSession(ExchangePrincipal principal, string taskName, bool allowAdminLocalization)
		{
			TaskLogger.LogEnter();
			MailboxSession result = MailboxSession.OpenAsAdmin(principal, CultureInfo.InvariantCulture, string.Format("Client=Management;Action={0};Privilege:ActAsAdmin", taskName), false, false, null, allowAdminLocalization);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x00118F88 File Offset: 0x00117188
		internal static MailboxSession OpenMailboxSessionAsOwner(ExchangePrincipal principal, ISecurityAccessToken userToken, string taskName)
		{
			TaskLogger.LogEnter();
			MailboxSession result = null;
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (string.IsNullOrEmpty(taskName))
			{
				throw new ArgumentNullException("taskName");
			}
			if (userToken == null)
			{
				result = MailboxSession.Open(principal, new WindowsPrincipal(WindowsIdentity.GetCurrent()), CultureInfo.InvariantCulture, string.Format("Client=Management;Action={0}", taskName));
			}
			else
			{
				try
				{
					using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(userToken, AuthzFlags.AuthzSkipTokenGroups))
					{
						clientSecurityContext.SetSecurityAccessToken(userToken);
						result = MailboxSession.Open(principal, clientSecurityContext, CultureInfo.InvariantCulture, string.Format("Client=Management;Action={0}", taskName));
					}
				}
				catch (AuthzException ex)
				{
					throw new AccessDeniedException(new LocalizedString(ex.Message));
				}
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x0011904C File Offset: 0x0011724C
		internal static void CleanupMailboxStoreTypeProvider(IConfigDataProvider provider)
		{
			if (provider != null)
			{
				MailboxStoreTypeProvider mailboxStoreTypeProvider = (MailboxStoreTypeProvider)provider;
				if (mailboxStoreTypeProvider.MailboxSession != null)
				{
					mailboxStoreTypeProvider.MailboxSession.Dispose();
					mailboxStoreTypeProvider.MailboxSession = null;
				}
			}
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x0011907D File Offset: 0x0011727D
		internal static void CheckUserVersion(ADUser user, Task.TaskErrorLoggingDelegate writeError)
		{
			if (user.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				writeError(new InvalidOperationException(Strings.ErrorCannotOpenLegacyMailbox(user.Identity.ToString())), ErrorCategory.InvalidOperation, user.Identity);
			}
		}
	}
}
