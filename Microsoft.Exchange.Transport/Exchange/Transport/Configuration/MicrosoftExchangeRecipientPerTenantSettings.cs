using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002DB RID: 731
	internal sealed class MicrosoftExchangeRecipientPerTenantSettings : TenantConfigurationCacheableItem<MicrosoftExchangeRecipient>
	{
		// Token: 0x0600205D RID: 8285 RVA: 0x0007BD9F File Offset: 0x00079F9F
		public MicrosoftExchangeRecipientPerTenantSettings()
		{
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0007BDA7 File Offset: 0x00079FA7
		public MicrosoftExchangeRecipientPerTenantSettings(MicrosoftExchangeRecipient microsoftExchangeRecipient, OrganizationId orgId) : base(true)
		{
			this.SetInternalData(microsoftExchangeRecipient, orgId);
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x0007BDB8 File Offset: 0x00079FB8
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.primarySmtpAddress;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x0007BDC7 File Offset: 0x00079FC7
		public bool UsingDefault
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.usingDefault;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x0007BDD6 File Offset: 0x00079FD6
		public override long ItemSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return (long)this.estimatedSize;
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0007BE08 File Offset: 0x0007A008
		public override void ReadData(IConfigurationSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			MicrosoftExchangeRecipient[] results = null;
			MicrosoftExchangeRecipient microsoftExchangeRecipient = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				results = session.Find<MicrosoftExchangeRecipient>(null, QueryScope.SubTree, null, null, 1);
			});
			if (adoperationResult.Succeeded && results != null && results.Length != 0)
			{
				microsoftExchangeRecipient = results[0];
			}
			this.SetInternalData(microsoftExchangeRecipient, session.SessionSettings.CurrentOrganizationId);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0007BE8C File Offset: 0x0007A08C
		private void SetInternalData(MicrosoftExchangeRecipient microsoftExchangeRecipient, OrganizationId orgId)
		{
			if (microsoftExchangeRecipient != null && this.ParseMicrosoftExchangeRecipientAndSetAddress(microsoftExchangeRecipient))
			{
				this.usingDefault = false;
			}
			else
			{
				this.usingDefault = true;
				this.SetPostmasterSettings(orgId);
			}
			this.estimatedSize = ((this.primarySmtpAddress == SmtpAddress.Empty) ? 1 : (1 + this.primarySmtpAddress.Length * 2));
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0007BEE8 File Offset: 0x0007A0E8
		private void SetPostmasterSettings(OrganizationId orgId)
		{
			PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
			AcceptedDomainEntry defaultDomain;
			if (Components.Configuration.TryGetAcceptedDomainTable(orgId, out perTenantAcceptedDomainTable))
			{
				defaultDomain = perTenantAcceptedDomainTable.AcceptedDomainTable.DefaultDomain;
			}
			else
			{
				defaultDomain = Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomain;
			}
			string domain;
			if (defaultDomain != null && !string.IsNullOrEmpty(defaultDomain.DomainName.Domain))
			{
				domain = defaultDomain.DomainName.Domain;
			}
			else
			{
				domain = Components.Configuration.LocalServer.TransportServer.GetDomainOrComputerName();
			}
			this.primarySmtpAddress = new SmtpAddress(ADMicrosoftExchangeRecipient.DefaultName, domain);
			ExTraceGlobals.ConfigurationTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "Using '{0}' as MicrosoftExchangeRecipient address.", this.primarySmtpAddress);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0007BF88 File Offset: 0x0007A188
		private bool ParseMicrosoftExchangeRecipientAndSetAddress(MicrosoftExchangeRecipient mer)
		{
			if (mer == null)
			{
				throw new ArgumentNullException("mer");
			}
			if (mer.PrimarySmtpAddress.IsValidAddress)
			{
				this.primarySmtpAddress = mer.PrimarySmtpAddress;
				return true;
			}
			foreach (ProxyAddress proxyAddress in mer.EmailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp && SmtpAddress.IsValidSmtpAddress(proxyAddress.AddressString))
				{
					this.primarySmtpAddress = new SmtpAddress(proxyAddress.AddressString);
					ExTraceGlobals.ConfigurationTracer.TraceDebug<string>((long)this.GetHashCode(), "Using '{0}' as MicrosoftExchangeRecipient address because primary SMTP proxy not found.", proxyAddress.AddressString);
					return true;
				}
			}
			return false;
		}

		// Token: 0x040010F7 RID: 4343
		private SmtpAddress primarySmtpAddress;

		// Token: 0x040010F8 RID: 4344
		private bool usingDefault;

		// Token: 0x040010F9 RID: 4345
		private int estimatedSize;
	}
}
