using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001B5 RID: 437
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregatedAccountHelper
	{
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x00074B68 File Offset: 0x00072D68
		// (set) Token: 0x060017DA RID: 6106 RVA: 0x00074B70 File Offset: 0x00072D70
		public Guid AccountMailboxGuid { get; set; }

		// Token: 0x060017DB RID: 6107 RVA: 0x00074B79 File Offset: 0x00072D79
		public AggregatedAccountHelper()
		{
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00074B81 File Offset: 0x00072D81
		public AggregatedAccountHelper(MailboxSession session, ADUser adUser)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(adUser, "adUser");
			this.session = session;
			this.adUser = adUser;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00074BB0 File Offset: 0x00072DB0
		public virtual AggregatedAccountInfo AddAccount(SmtpAddress smtpAddress, Guid aggregatedMailboxGuid, Guid requestGuid)
		{
			Util.ThrowOnNullArgument(smtpAddress, "smtpAddress");
			AggregatedAccountListConfiguration aggregatedAccountListConfiguration = new AggregatedAccountListConfiguration();
			aggregatedAccountListConfiguration.Principal = ExchangePrincipal.FromADUser(this.adUser, null);
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(this.adUser)
			{
				MailboxSession = this.session
			};
			this.AccountMailboxGuid = aggregatedMailboxGuid;
			AggregatedAccountInfo result = new AggregatedAccountInfo(this.AccountMailboxGuid, smtpAddress, requestGuid);
			aggregatedAccountListConfiguration.RequestGuid = requestGuid;
			aggregatedAccountListConfiguration.SmtpAddress = smtpAddress;
			aggregatedAccountListConfiguration.AggregatedMailboxGuid = this.AccountMailboxGuid;
			aggregatedAccountListConfiguration.Save(mailboxStoreTypeProvider);
			return result;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00074C7C File Offset: 0x00072E7C
		public virtual AggregatedAccountInfo GetAccount(SmtpAddress smtpAddress)
		{
			Util.ThrowOnNullArgument(smtpAddress, "smtpAddress");
			List<AggregatedAccountInfo> listOfAccounts = this.GetListOfAccounts();
			return listOfAccounts.FirstOrDefault((AggregatedAccountInfo o) => StringComparer.OrdinalIgnoreCase.Equals(o.SmtpAddress.ToString(), smtpAddress.ToString()));
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00074CC4 File Offset: 0x00072EC4
		public virtual List<AggregatedAccountInfo> GetListOfAccounts()
		{
			AggregatedAccountListConfiguration aggregatedAccountListConfiguration = new AggregatedAccountListConfiguration
			{
				Principal = ExchangePrincipal.FromADUser(this.adUser, null)
			};
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(this.adUser)
			{
				MailboxSession = this.session
			};
			aggregatedAccountListConfiguration = (aggregatedAccountListConfiguration.Read(mailboxStoreTypeProvider, null) as AggregatedAccountListConfiguration);
			if (aggregatedAccountListConfiguration.AggregatedAccountList == null)
			{
				return new List<AggregatedAccountInfo>();
			}
			return aggregatedAccountListConfiguration.AggregatedAccountList;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00074D44 File Offset: 0x00072F44
		public virtual void RemoveAccount(Guid mailboxGuid)
		{
			Util.ThrowOnNullArgument(mailboxGuid, "id");
			AggregatedAccountListConfiguration aggregatedAccountListConfiguration = new AggregatedAccountListConfiguration();
			aggregatedAccountListConfiguration.Principal = ExchangePrincipal.FromADUser(this.adUser, null);
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(this.adUser)
			{
				MailboxSession = this.session
			};
			List<AggregatedAccountInfo> listOfAccounts = this.GetListOfAccounts();
			AggregatedAccountInfo aggregatedAccountInfo = listOfAccounts.Find((AggregatedAccountInfo o) => o.MailboxGuid == mailboxGuid);
			if (aggregatedAccountInfo == null)
			{
				return;
			}
			aggregatedAccountListConfiguration.RequestGuid = aggregatedAccountInfo.RequestGuid;
			aggregatedAccountListConfiguration.Delete(mailboxStoreTypeProvider);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00074DF8 File Offset: 0x00072FF8
		public virtual void RemoveAccount(SmtpAddress smtpAddress)
		{
			Util.ThrowOnNullArgument(smtpAddress, "smtpAddress");
			AggregatedAccountListConfiguration aggregatedAccountListConfiguration = new AggregatedAccountListConfiguration();
			aggregatedAccountListConfiguration.Principal = ExchangePrincipal.FromADUser(this.adUser, null);
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(this.adUser)
			{
				MailboxSession = this.session
			};
			List<AggregatedAccountInfo> listOfAccounts = this.GetListOfAccounts();
			AggregatedAccountInfo aggregatedAccountInfo = listOfAccounts.Find((AggregatedAccountInfo o) => o.SmtpAddress == smtpAddress);
			if (aggregatedAccountInfo == null)
			{
				return;
			}
			aggregatedAccountListConfiguration.RequestGuid = aggregatedAccountInfo.RequestGuid;
			aggregatedAccountListConfiguration.Delete(mailboxStoreTypeProvider);
		}

		// Token: 0x04000C93 RID: 3219
		private readonly MailboxSession session;

		// Token: 0x04000C94 RID: 3220
		private readonly ADUser adUser;
	}
}
