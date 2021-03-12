using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000506 RID: 1286
	public sealed class CommonTestTasks : Task
	{
		// Token: 0x06002E24 RID: 11812 RVA: 0x000B8D54 File Offset: 0x000B6F54
		internal static UserWithCredential GetDefaultTestAccount(CommonTestTasks.ClientAccessContext context)
		{
			SmtpAddress? smtpAddress;
			if (TestConnectivityCredentialsManager.IsExchangeMultiTenant())
			{
				smtpAddress = TestConnectivityCredentialsManager.GetMultiTenantAutomatedTaskUser(context.Instance, context.ConfigurationSession, context.Site);
			}
			else
			{
				smtpAddress = TestConnectivityCredentialsManager.GetEnterpriseAutomatedTaskUser(context.Site, context.WindowsDomain);
			}
			if (smtpAddress == null)
			{
				throw new MailboxNotFoundException(new MailboxIdParameter(), null);
			}
			MailboxIdParameter localMailboxId = new MailboxIdParameter(string.Format("{0}\\{1}", smtpAddress.Value.Domain, smtpAddress.Value.Local));
			ADUser aduser = CommonTestTasks.EnsureSingleObject<ADUser>(() => localMailboxId.GetObjects<ADUser>(null, context.RecipientSession));
			if (aduser == null)
			{
				throw new MailboxNotFoundException(new MailboxIdParameter(smtpAddress.ToString()), null);
			}
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(aduser, null);
			if (exchangePrincipal == null)
			{
				throw new MailboxNotFoundException(new MailboxIdParameter(smtpAddress.ToString()), null);
			}
			NetworkCredential networkCredential = new NetworkCredential(smtpAddress.Value.ToString(), string.Empty, context.WindowsDomain);
			NetworkCredential networkCredential2 = CommonTestTasks.MakeCasCredential(networkCredential);
			bool flag = false;
			LocalizedException ex;
			if (Datacenter.IsLiveIDForExchangeLogin(true) || context.MonitoringContext)
			{
				ex = TestConnectivityCredentialsManager.LoadAutomatedTestCasConnectivityInfo(exchangePrincipal, networkCredential2);
			}
			else
			{
				ex = TestConnectivityCredentialsManager.ResetAutomatedCredentialsAndVerify(exchangePrincipal, networkCredential2, false, out flag);
			}
			if (ex != null)
			{
				throw ex;
			}
			networkCredential.Domain = smtpAddress.Value.Domain;
			networkCredential.Password = networkCredential2.Password;
			return new UserWithCredential
			{
				User = aduser,
				Credential = networkCredential
			};
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000B8F0C File Offset: 0x000B710C
		internal static NetworkCredential MakeCasCredential(NetworkCredential networkCredential)
		{
			NetworkCredential networkCredential2 = new NetworkCredential(networkCredential.UserName, networkCredential.Password, networkCredential.Domain);
			if (!Datacenter.IsMultiTenancyEnabled())
			{
				networkCredential2.UserName = SmtpAddress.Parse(networkCredential.UserName).Local;
			}
			return networkCredential2;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000B8F54 File Offset: 0x000B7154
		internal static T EnsureSingleObject<T>(Func<IEnumerable<T>> getObjects) where T : class
		{
			T t = default(T);
			foreach (T t2 in getObjects())
			{
				if (t != null)
				{
					throw new DataValidationException(new ObjectValidationError(Strings.MoreThanOneObjects(typeof(T).ToString()), null, null));
				}
				t = t2;
			}
			return t;
		}

		// Token: 0x02000507 RID: 1287
		internal struct ClientAccessContext
		{
			// Token: 0x0400210E RID: 8462
			internal Task Instance;

			// Token: 0x0400210F RID: 8463
			internal bool MonitoringContext;

			// Token: 0x04002110 RID: 8464
			internal ITopologyConfigurationSession ConfigurationSession;

			// Token: 0x04002111 RID: 8465
			internal IRecipientSession RecipientSession;

			// Token: 0x04002112 RID: 8466
			internal string WindowsDomain;

			// Token: 0x04002113 RID: 8467
			internal ADSite Site;
		}
	}
}
