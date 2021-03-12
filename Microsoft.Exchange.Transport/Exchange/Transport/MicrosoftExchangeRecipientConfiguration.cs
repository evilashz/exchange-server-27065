using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002DC RID: 732
	internal sealed class MicrosoftExchangeRecipientConfiguration : GlobalConfigurationBase<MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>
	{
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0007C05C File Offset: 0x0007A25C
		public RoutingAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x0007C064 File Offset: 0x0007A264
		public bool UsingDefaultAddress
		{
			get
			{
				return this.usingDefault;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x0007C06C File Offset: 0x0007A26C
		protected override string ConfigObjectName
		{
			get
			{
				return "MicrosoftExchangeRecipient";
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x0007C073 File Offset: 0x0007A273
		protected override string ReloadFailedString
		{
			get
			{
				return Strings.ReadMicrosoftExchangeRecipientFailed;
			}
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0007C07F File Offset: 0x0007A27F
		protected override ADObjectId GetObjectId(IConfigurationSession session)
		{
			return ADMicrosoftExchangeRecipient.GetDefaultId(session);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0007C088 File Offset: 0x0007A288
		protected override void HandleObjectLoaded()
		{
			if (base.ConfigObject.PrimarySmtpAddress.IsValidAddress)
			{
				this.usingDefault = false;
				this.address = new RoutingAddress(base.ConfigObject.PrimarySmtpAddress.ToString());
				return;
			}
			foreach (ProxyAddress proxyAddress in base.ConfigObject.EmailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp && SmtpAddress.IsValidSmtpAddress(proxyAddress.AddressString))
				{
					this.usingDefault = false;
					this.address = new RoutingAddress(proxyAddress.AddressString);
					ExTraceGlobals.ConfigurationTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Using '{0}' as MicrosoftExchangeRecipient address because primary SMTP proxy not found.", this.address);
					return;
				}
			}
			this.SetDefaultAddress();
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0007C178 File Offset: 0x0007A378
		protected override bool HandleObjectNotFound()
		{
			if (Components.IsBridgehead)
			{
				return false;
			}
			this.SetDefaultAddress();
			return true;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0007C18C File Offset: 0x0007A38C
		private void SetDefaultAddress()
		{
			string domain;
			if (!string.IsNullOrEmpty(Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName))
			{
				domain = Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName;
			}
			else
			{
				domain = Components.Configuration.LocalServer.TransportServer.GetDomainOrComputerName();
			}
			this.usingDefault = true;
			this.address = new RoutingAddress(ADMicrosoftExchangeRecipient.DefaultName, domain);
			ExTraceGlobals.ConfigurationTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Using '{0}' as MicrosoftExchangeRecipient address.", this.address);
		}

		// Token: 0x040010FA RID: 4346
		private RoutingAddress address;

		// Token: 0x040010FB RID: 4347
		private bool usingDefault;
	}
}
