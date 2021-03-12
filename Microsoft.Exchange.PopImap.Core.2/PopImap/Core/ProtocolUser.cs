using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200002A RID: 42
	internal abstract class ProtocolUser
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000931C File Offset: 0x0000751C
		internal ProtocolUser(ProtocolSession session)
		{
			this.session = session;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000932B File Offset: 0x0000752B
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00009333 File Offset: 0x00007533
		public bool IsEnabled { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000933C File Offset: 0x0000753C
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00009344 File Offset: 0x00007544
		public bool UseProtocolDefaults { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000934D File Offset: 0x0000754D
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00009355 File Offset: 0x00007555
		public MimeTextFormat MessagesRetrievalMimeTextFormat { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000935E File Offset: 0x0000755E
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00009366 File Offset: 0x00007566
		public string LegacyDistinguishedName { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000936F File Offset: 0x0000756F
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00009377 File Offset: 0x00007577
		public string AcceptedDomain { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00009380 File Offset: 0x00007580
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00009388 File Offset: 0x00007588
		public string LogonName { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00009391 File Offset: 0x00007591
		public string UniqueName
		{
			get
			{
				if (this.LegacyDistinguishedName != null)
				{
					return this.LegacyDistinguishedName;
				}
				return this.LogonName;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000093A8 File Offset: 0x000075A8
		// (set) Token: 0x0600026A RID: 618 RVA: 0x000093B0 File Offset: 0x000075B0
		public string ConnectionIdentity { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000093B9 File Offset: 0x000075B9
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000093C1 File Offset: 0x000075C1
		public string PrimarySmtpAddress { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000093CA File Offset: 0x000075CA
		// (set) Token: 0x0600026E RID: 622 RVA: 0x000093D2 File Offset: 0x000075D2
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000093DB File Offset: 0x000075DB
		// (set) Token: 0x06000270 RID: 624 RVA: 0x000093E3 File Offset: 0x000075E3
		public bool EnableExactRFC822Size { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000093EC File Offset: 0x000075EC
		// (set) Token: 0x06000272 RID: 626 RVA: 0x000093F4 File Offset: 0x000075F4
		public bool SuppressReadReceipt { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000093FD File Offset: 0x000075FD
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00009405 File Offset: 0x00007605
		public bool ForceICalForCalendarRetrievalOption { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000940E File Offset: 0x0000760E
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00009416 File Offset: 0x00007616
		public ExDateTime MailboxLogTimeout { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000941F File Offset: 0x0000761F
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00009427 File Offset: 0x00007627
		public bool LrsEnabled { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000279 RID: 633
		public abstract ExEventLog.EventTuple UserExceededNumberOfConnectionsEventTuple { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600027A RID: 634
		public abstract ExEventLog.EventTuple OwaServerNotFoundEventTuple { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600027B RID: 635
		public abstract ExEventLog.EventTuple OwaServerInvalidEventTuple { get; }

		// Token: 0x0600027C RID: 636 RVA: 0x00009430 File Offset: 0x00007630
		public void Reset()
		{
			this.LegacyDistinguishedName = null;
			this.LogonName = null;
			this.ConnectionIdentity = null;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009447 File Offset: 0x00007647
		public ADSessionSettings GetSessionSettings()
		{
			if (string.IsNullOrEmpty(this.AcceptedDomain))
			{
				return ADSessionSettings.FromRootOrgScopeSet();
			}
			return ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(this.AcceptedDomain);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009468 File Offset: 0x00007668
		public override string ToString()
		{
			return string.Format("{0}, SMTP: \"{1}\", {2}, AcceptedDomain: \"{3}\"", new object[]
			{
				this.UniqueName,
				this.PrimarySmtpAddress,
				this.IsEnabled ? "enabled" : "disabled",
				this.AcceptedDomain
			});
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000094BC File Offset: 0x000076BC
		internal ADUser FindAdUser(string userName, SecurityIdentifier userSid = null, string userPuid = null)
		{
			ADUser aduser = null;
			if (string.IsNullOrEmpty(userName))
			{
				return null;
			}
			string text = null;
			SmtpProxyAddress smtpProxyAddress = null;
			try
			{
				smtpProxyAddress = new SmtpProxyAddress(userName, true);
				this.AcceptedDomain = ((SmtpAddress)smtpProxyAddress).Domain;
			}
			catch (ArgumentOutOfRangeException)
			{
				string[] array = userName.Split("\\/".ToCharArray(), 2);
				if (array.Length == 2)
				{
					text = array[1];
					this.AcceptedDomain = array[0];
				}
				else
				{
					text = userName;
				}
			}
			ADSessionSettings sessionSettings = this.GetSessionSettings();
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, 0, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 299, "FindAdUser", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\protocoluser.cs");
			ITenantRecipientSession tenantRecipientSession = tenantOrRootOrgRecipientSession as ITenantRecipientSession;
			if (userSid != null)
			{
				aduser = (tenantOrRootOrgRecipientSession.FindBySid(userSid) as ADUser);
				if (aduser == null)
				{
					ProtocolBaseServices.SessionTracer.TraceError<SecurityIdentifier>(this.session.SessionId, "FindBySid did not return AD obejct for user SID {0}", userSid);
					if (this.session.LightLogSession != null)
					{
						this.session.LightLogSession.ErrorMessage = "NoAdUserBySid";
					}
				}
			}
			else if (!string.IsNullOrEmpty(userPuid) && tenantRecipientSession != null)
			{
				ADRawEntry[] array2 = tenantRecipientSession.FindByNetID(userPuid, new PropertyDefinition[0]);
				if (array2.Length == 0)
				{
					ProtocolBaseServices.SessionTracer.TraceError<string>(this.session.SessionId, "FindByNetID did not return AD obejct for user PUID {0}", userPuid);
					if (this.session.LightLogSession != null)
					{
						this.session.LightLogSession.ErrorMessage = "NoAdUserByPuid";
					}
				}
				else if (array2.Length > 1)
				{
					ProtocolBaseServices.SessionTracer.TraceError<string, int>(this.session.SessionId, "FindByNetID found {1} AD obejcts for user PUID {0}", userPuid, array2.Length);
					if (this.session.LightLogSession != null)
					{
						this.session.LightLogSession.ErrorMessage = "TooManyAdUsersByPuid";
					}
				}
				else
				{
					aduser = (tenantOrRootOrgRecipientSession.Read(array2[0].Id) as ADUser);
				}
			}
			else if (smtpProxyAddress != null)
			{
				aduser = tenantOrRootOrgRecipientSession.FindByProxyAddress<ADUser>(smtpProxyAddress);
				if (aduser == null)
				{
					ProtocolBaseServices.SessionTracer.TraceError<SmtpProxyAddress>(this.session.SessionId, "FindByProxyAddress did not return AD obejct for user ProxyAddress {0}", smtpProxyAddress);
					if (this.session.LightLogSession != null)
					{
						this.session.LightLogSession.ErrorMessage = "NoAdUserByByProxyAddress";
					}
				}
			}
			else
			{
				aduser = (tenantOrRootOrgRecipientSession.FindByAccountName<ADUser>(this.AcceptedDomain, text) as ADUser);
				if (aduser == null)
				{
					ProtocolBaseServices.SessionTracer.TraceError<string, string>(this.session.SessionId, "FindByAccountName did not return AD obejct for user AccountName {0}, domain {1}", text, this.AcceptedDomain);
					if (this.session.LightLogSession != null)
					{
						this.session.LightLogSession.ErrorMessage = "NoAdUserByByAccountName";
					}
				}
			}
			return aduser;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009754 File Offset: 0x00007954
		internal void Configure(ADUser adUser, ADPropertyDefinition enabled, ADPropertyDefinition useProtocolDefaults, ADPropertyDefinition messagesRetrievalMimeFormat, ADPropertyDefinition enableExactRFC822Size, ADPropertyDefinition protocolLoggingEnabled, ADPropertyDefinition suppressReadReceipt, ADPropertyDefinition forceICalForCalendarRetrievalOption)
		{
			this.OrganizationId = adUser.OrganizationId;
			CASMailbox casmailbox = new CASMailbox(adUser);
			this.LegacyDistinguishedName = casmailbox.LegacyExchangeDN;
			this.PrimarySmtpAddress = casmailbox.PrimarySmtpAddress.ToString();
			this.IsEnabled = (bool)casmailbox[enabled];
			this.UseProtocolDefaults = (bool)casmailbox[useProtocolDefaults];
			this.MessagesRetrievalMimeTextFormat = (MimeTextFormat)casmailbox[messagesRetrievalMimeFormat];
			this.EnableExactRFC822Size = (bool)casmailbox[enableExactRFC822Size];
			this.SuppressReadReceipt = (bool)casmailbox[suppressReadReceipt];
			this.ForceICalForCalendarRetrievalOption = (bool)casmailbox[forceICalForCalendarRetrievalOption];
			object obj = casmailbox[protocolLoggingEnabled];
			if (obj == null)
			{
				this.MailboxLogTimeout = ExDateTime.MinValue;
			}
			else
			{
				this.MailboxLogTimeout = CASMailbox.MailboxProtocolLoggingInitialTime.AddMinutes((double)((int)obj)).AddHours(72.0);
			}
			if (ProtocolBaseServices.LrsLogEnabled)
			{
				this.LrsEnabled = true;
			}
		}

		// Token: 0x06000281 RID: 641
		protected internal abstract void Configure(ADUser adUser);

		// Token: 0x04000152 RID: 338
		private ProtocolSession session;
	}
}
