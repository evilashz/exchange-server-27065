using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000010 RID: 16
	internal sealed class StoreDriverConfig
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003C08 File Offset: 0x00001E08
		private StoreDriverConfig()
		{
			StoreDriverParameters storeDriverParameters = new StoreDriverParameters();
			try
			{
				storeDriverParameters.Load(new StoreDriverParameterHandler(this.StoreDriverParameterParser));
			}
			catch (Exception arg)
			{
				StoreDriverConfig.diag.TraceDebug<Exception>(0L, "StoreDriver encountered an exception {0} when loading parameters from storedriver.config.", arg);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003C64 File Offset: 0x00001E64
		internal static StoreDriverConfig Instance
		{
			get
			{
				if (StoreDriverConfig.instance == null)
				{
					lock (StoreDriverConfig.syncRoot)
					{
						if (StoreDriverConfig.instance == null)
						{
							StoreDriverConfig.instance = new StoreDriverConfig();
						}
					}
				}
				return StoreDriverConfig.instance;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003CBC File Offset: 0x00001EBC
		internal bool AlwaysSetReminderOnAppointment
		{
			get
			{
				return this.alwaysSetReminderOnAppointment;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003CC4 File Offset: 0x00001EC4
		internal bool IsUserQuarantineMailbox(string userAddress, string qMailbox, OrganizationId orgId)
		{
			SmtpAddress recipientAddress = new SmtpAddress(qMailbox);
			if (!recipientAddress.IsValidAddress)
			{
				return false;
			}
			bool result;
			try
			{
				bool flag = false;
				if (!qMailbox.Equals(this.quarantineMailbox, StringComparison.OrdinalIgnoreCase))
				{
					this.quarantineMailbox = qMailbox;
					flag = true;
				}
				if (flag || this.quarantineMailboxProxies == null)
				{
					this.quarantineMailboxProxies = StoreDriverConfig.LookupProxyAddresses(recipientAddress, orgId);
				}
				if (this.quarantineMailboxProxies == null)
				{
					ExTraceGlobals.StoreDriverTracer.TraceError((long)this.GetHashCode(), "Failed to load quarantine mailbox proxies");
					result = false;
				}
				else
				{
					result = StoreDriverConfig.IsUserAmongProxies(userAddress, this.quarantineMailboxProxies);
				}
			}
			catch (StoreDriverConfigurationLoadException ex)
			{
				ExTraceGlobals.StoreDriverTracer.TraceError<string>((long)this.GetHashCode(), "Failed to load quarantine mailbox proxies: {0}", ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003D7C File Offset: 0x00001F7C
		private static bool IsUserAmongProxies(string userAddress, string[] proxies)
		{
			foreach (string text in proxies)
			{
				if (text.Equals(userAddress, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003DB0 File Offset: 0x00001FB0
		private static string[] LookupProxyAddresses(SmtpAddress recipientAddress, OrganizationId orgId)
		{
			string addressString = recipientAddress.ToString();
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 212, "LookupProxyAddresses", "f:\\15.00.1497\\sources\\dev\\MailboxTransport\\src\\StoreDriver\\StoreDriverConfig.cs");
			ProxyAddress proxyAddress = ProxyAddress.Parse(ProxyAddressPrefix.Smtp.PrimaryPrefix, addressString);
			Exception ex = null;
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			list.Add(ADRecipientSchema.EmailAddresses);
			list.Add(ADRecipientSchema.LegacyExchangeDN);
			ADRawEntry adrawEntry = null;
			try
			{
				adrawEntry = tenantOrRootOrgRecipientSession.FindByProxyAddress(proxyAddress, list);
			}
			catch (TransientException ex2)
			{
				ex = ex2;
			}
			catch (DataValidationException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new StoreDriverConfigurationLoadException(ex.Message);
			}
			if (adrawEntry == null)
			{
				return null;
			}
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)adrawEntry[ADRecipientSchema.EmailAddresses];
			string[] array = new string[proxyAddressCollection.Count + 1];
			string defaultDomainName = Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName;
			string text = (string)adrawEntry[ADRecipientSchema.LegacyExchangeDN];
			SmtpProxyAddress smtpProxyAddress;
			if (!SmtpProxyAddress.TryEncapsulate(ProxyAddressPrefix.LegacyDN.PrimaryPrefix, text, defaultDomainName, out smtpProxyAddress))
			{
				throw new StoreDriverConfigurationLoadException(string.Format("Could not encapsulate legacyExchangeDN: {0}", text));
			}
			array[0] = smtpProxyAddress.AddressString;
			int num = 1;
			foreach (ProxyAddress proxyAddress2 in proxyAddressCollection)
			{
				SmtpProxyAddress smtpProxyAddress2 = null;
				if (proxyAddress2.Prefix == ProxyAddressPrefix.Smtp)
				{
					array[num++] = proxyAddress2.AddressString;
				}
				else
				{
					if (!SmtpProxyAddress.TryEncapsulate(proxyAddress2, defaultDomainName, out smtpProxyAddress2))
					{
						throw new StoreDriverConfigurationLoadException(string.Format("Could not encapsulate this proxy-address: {0}", proxyAddress2.ProxyAddressString));
					}
					array[num++] = smtpProxyAddress2.AddressString;
				}
			}
			return array;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003F88 File Offset: 0x00002188
		private void StoreDriverParameterParser(string key, string value)
		{
			if (string.Compare("AlwaysSetReminderOnAppointment", key, StringComparison.OrdinalIgnoreCase) == 0 && !bool.TryParse(value, out this.alwaysSetReminderOnAppointment))
			{
				this.alwaysSetReminderOnAppointment = true;
			}
		}

		// Token: 0x0400004E RID: 78
		private static readonly Trace diag = ExTraceGlobals.StoreDriverTracer;

		// Token: 0x0400004F RID: 79
		private static object syncRoot = new object();

		// Token: 0x04000050 RID: 80
		private static StoreDriverConfig instance;

		// Token: 0x04000051 RID: 81
		private bool alwaysSetReminderOnAppointment = true;

		// Token: 0x04000052 RID: 82
		private string quarantineMailbox;

		// Token: 0x04000053 RID: 83
		private string[] quarantineMailboxProxies;
	}
}
