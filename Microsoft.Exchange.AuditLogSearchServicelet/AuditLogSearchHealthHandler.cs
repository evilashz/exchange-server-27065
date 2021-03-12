using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.AuditLogSearchServicelet;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02000003 RID: 3
	internal class AuditLogSearchHealthHandler : ExchangeDiagnosableWrapper<HealthHandlerResult>
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002208 File Offset: 0x00000408
		public static AuditLogSearchHealthHandler GetInstance()
		{
			if (AuditLogSearchHealthHandler.instance == null)
			{
				lock (AuditLogSearchHealthHandler.instanceLock)
				{
					if (AuditLogSearchHealthHandler.instance == null)
					{
						AuditLogSearchHealthHandler.instance = new AuditLogSearchHealthHandler();
					}
				}
			}
			return AuditLogSearchHealthHandler.instance;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002260 File Offset: 0x00000460
		private static void CheckMailboxConnectivity(EwsAuditClient client, bool isArchive)
		{
			FolderIdType folderIdType;
			client.FindFolder("Audits", isArchive ? AuditLogSearchHealthHandler.ArchiveRecoverableItemsRootId : AuditLogSearchHealthHandler.RecoverableItemsRootId, out folderIdType);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000228A File Offset: 0x0000048A
		private AuditLogSearchHealthHandler()
		{
			this.RunSearchNowEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000229F File Offset: 0x0000049F
		internal AuditLogSearchHealth AuditLogSearchHealth
		{
			get
			{
				if (this.auditLogSearchHealth == null)
				{
					this.auditLogSearchHealth = new AuditLogSearchHealth();
				}
				return this.auditLogSearchHealth;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022BA File Offset: 0x000004BA
		protected override string ComponentName
		{
			get
			{
				return "AuditLogSearchHealth";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022C1 File Offset: 0x000004C1
		protected override string UsageText
		{
			get
			{
				return "Returns information about the recent audit log searches.";
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022C8 File Offset: 0x000004C8
		protected override string UsageSample
		{
			get
			{
				return base.UsageSample + "\r\n\t-Argument /run - run searches now, \r\n\t-Argument /check <tenant accepted domain> <MailboxExchangeGuid> - check connectivity with the specified mailbox";
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022E8 File Offset: 0x000004E8
		internal override HealthHandlerResult GetExchangeDiagnosticsInfoData(DiagnosableParameters parameters)
		{
			string argument = parameters.Argument;
			if (string.IsNullOrWhiteSpace(argument))
			{
				return this.AuditLogSearchHealth;
			}
			if (argument.Equals("/run", StringComparison.InvariantCultureIgnoreCase))
			{
				this.RunSearchNowEvent.Set();
				return new HealthHandlerResult("Ok");
			}
			if (argument.Equals("/check", StringComparison.InvariantCultureIgnoreCase))
			{
				Search search = (from s in this.AuditLogSearchHealth.Searches
				where s.ExceptionDetails != null
				select s).FirstOrDefault<Search>() ?? this.AuditLogSearchHealth.Searches.FirstOrDefault<Search>();
				if (search == null)
				{
					return new HealthHandlerResult("There are no faulted or any searches.");
				}
				MailboxConnectivity mailboxConnectivity = new MailboxConnectivity
				{
					Message = string.Empty,
					TenantAcceptedDomain = search.UserPrincipalName.Split(new char[]
					{
						'@'
					}).LastOrDefault<string>(),
					ExchangeUserId = (string.IsNullOrWhiteSpace(search.LastProcessedMailbox) ? Guid.Empty : Guid.Parse(search.LastProcessedMailbox))
				};
				return AuditLogSearchHealthHandler.CheckMailboxConnectivity(mailboxConnectivity);
			}
			else
			{
				Match match = Regex.Match(argument, "^\\s* [/-]?check \\s* (?<org>\\S+) (\\s* [/\\s] \\s* (?<mailbox>\\S+))? \\s*$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
				if (match.Success)
				{
					MailboxConnectivity mailboxConnectivity2 = new MailboxConnectivity
					{
						Exception = string.Empty,
						Message = string.Empty
					};
					string value = match.Groups["org"].Value;
					string value2 = match.Groups["mailbox"].Value;
					Guid empty = Guid.Empty;
					if (!string.IsNullOrEmpty(value) && (string.IsNullOrWhiteSpace(value2) || Guid.TryParse(value2, out empty)))
					{
						mailboxConnectivity2.TenantAcceptedDomain = value;
						mailboxConnectivity2.ExchangeUserId = empty;
						mailboxConnectivity2 = AuditLogSearchHealthHandler.CheckMailboxConnectivity(mailboxConnectivity2);
					}
					else
					{
						mailboxConnectivity2.Message = "A domain name and a mailbox GUID are expected.";
					}
					return mailboxConnectivity2;
				}
				return new HealthHandlerResult("Invalid argument.");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000024C7 File Offset: 0x000006C7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000024CF File Offset: 0x000006CF
		internal EventWaitHandle RunSearchNowEvent { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x000024D8 File Offset: 0x000006D8
		private static MailboxConnectivity CheckMailboxConnectivity(MailboxConnectivity mailboxConnectivity)
		{
			try
			{
				mailboxConnectivity.Message = "Getting ADSessionSettings";
				ADSessionSettings adsessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(mailboxConnectivity.TenantAcceptedDomain);
				if (adsessionSettings == null)
				{
					mailboxConnectivity.Message = "ADSessionSettings were not found.";
					return mailboxConnectivity;
				}
				mailboxConnectivity.Message = "Creating IRecipientSession";
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, adsessionSettings, 223, "CheckMailboxConnectivity", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\AuditLogSearch\\Program\\AuditLogSearchHealthHandler.cs");
				if (tenantOrRootOrgRecipientSession == null)
				{
					mailboxConnectivity.Message = "IRecipientSession was not found.";
					return mailboxConnectivity;
				}
				bool flag = mailboxConnectivity.ExchangeUserId.CompareTo(Guid.Empty) == 0;
				ADUser aduser;
				if (flag)
				{
					mailboxConnectivity.Message = "Getting OrganizationId";
					OrganizationId organizationId = OrganizationId.FromAcceptedDomain(mailboxConnectivity.TenantAcceptedDomain);
					mailboxConnectivity.Message = "Finding arbitration mailbox";
					aduser = AdminAuditLogHelper.GetTenantArbitrationMailbox(organizationId);
					mailboxConnectivity.ExchangeUserId = aduser.ExchangeGuid;
				}
				else
				{
					mailboxConnectivity.Message = "Finding ADRecipient";
					ADRecipient adrecipient = tenantOrRootOrgRecipientSession.FindByExchangeGuidIncludingAlternate(mailboxConnectivity.ExchangeUserId);
					aduser = (adrecipient as ADUser);
				}
				if (aduser == null)
				{
					mailboxConnectivity.Message = "ADUser was not found.";
					return mailboxConnectivity;
				}
				mailboxConnectivity.Message = "Getting ExchangePrincipal";
				ExchangePrincipal principal = ExchangePrincipal.FromADUser(aduser, null);
				mailboxConnectivity.Message = "Creating EwsAuditClient";
				EwsAuditClient client = new EwsAuditClient(new EwsConnectionManager(principal, OpenAsAdminOrSystemServiceBudgetTypeType.Default, ExTraceGlobals.ServiceletTracer), TimeSpan.FromSeconds(5.0), ExTraceGlobals.ServiceletTracer);
				mailboxConnectivity.Message = "Checking connectivity";
				AuditLogSearchHealthHandler.CheckMailboxConnectivity(client, false);
				mailboxConnectivity.Message = "Checking connectivity (archive)";
				AuditLogSearchHealthHandler.CheckMailboxConnectivity(client, true);
				mailboxConnectivity.Message = "Ok";
				mailboxConnectivity.Success = true;
			}
			catch (Exception ex)
			{
				mailboxConnectivity.Message += " FAILED.";
				mailboxConnectivity.Exception = ex.ToString();
			}
			return mailboxConnectivity;
		}

		// Token: 0x04000007 RID: 7
		private static AuditLogSearchHealthHandler instance = null;

		// Token: 0x04000008 RID: 8
		private static readonly object instanceLock = new object();

		// Token: 0x04000009 RID: 9
		private AuditLogSearchHealth auditLogSearchHealth;

		// Token: 0x0400000A RID: 10
		private static readonly DistinguishedFolderIdType RecoverableItemsRootId = new DistinguishedFolderIdType
		{
			Id = DistinguishedFolderIdNameType.recoverableitemsroot
		};

		// Token: 0x0400000B RID: 11
		private static readonly DistinguishedFolderIdType ArchiveRecoverableItemsRootId = new DistinguishedFolderIdType
		{
			Id = DistinguishedFolderIdNameType.archiverecoverableitemsroot
		};
	}
}
