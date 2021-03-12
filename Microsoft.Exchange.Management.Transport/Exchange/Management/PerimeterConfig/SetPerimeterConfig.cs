using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PerimeterConfig
{
	// Token: 0x0200005F RID: 95
	[Cmdlet("Set", "PerimeterConfig", SupportsShouldProcess = true)]
	public sealed class SetPerimeterConfig : SetMultitenancySingletonSystemConfigurationObjectTask<PerimeterConfig>
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000CF2A File Offset: 0x0000B12A
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000CF41 File Offset: 0x0000B141
		[Parameter(Mandatory = false)]
		public MailFlowPartnerIdParameter MailFlowPartner
		{
			get
			{
				return (MailFlowPartnerIdParameter)base.Fields[PerimeterConfigSchema.MailFlowPartner];
			}
			set
			{
				base.Fields[PerimeterConfigSchema.MailFlowPartner] = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000CF54 File Offset: 0x0000B154
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetPerimeterConfig;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000CF5C File Offset: 0x0000B15C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			PerimeterConfig perimeterConfig = (PerimeterConfig)this.GetDynamicParameters();
			if (base.Fields.IsModified(PerimeterConfigSchema.MailFlowPartner))
			{
				MailFlowPartnerIdParameter mailFlowPartner = this.MailFlowPartner;
				if (mailFlowPartner != null)
				{
					IConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 81, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\PerimeterConfig\\SetPerimeterConfig.cs");
					MailFlowPartner mailFlowPartner2 = (MailFlowPartner)base.GetDataObject<MailFlowPartner>(mailFlowPartner, session, this.RootId, new LocalizedString?(Strings.MailFlowPartnerNotExists(mailFlowPartner)), new LocalizedString?(Strings.MailFlowPartnerNotUnique(mailFlowPartner)), ExchangeErrorCategory.Client);
					perimeterConfig.MailFlowPartner = (ADObjectId)mailFlowPartner2.Identity;
					return;
				}
				perimeterConfig.MailFlowPartner = null;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000D008 File Offset: 0x0000B208
		protected override void InternalValidate()
		{
			base.InternalValidate();
			PerimeterConfig dataObject = this.DataObject;
			this.CheckForDuplicates(dataObject);
			IPAddress ipaddress;
			if (dataObject.IsChanged(PerimeterConfigSchema.InternalServerIPAddresses) && this.CheckIPv6AddressIsPresent(dataObject.InternalServerIPAddresses, out ipaddress))
			{
				base.WriteError(new IPv6AddressesAreNotAllowedInInternalServerIPAddressesException(ipaddress.ToString()), ErrorCategory.InvalidOperation, null);
			}
			if (dataObject.IsChanged(PerimeterConfigSchema.GatewayIPAddresses))
			{
				SetPerimeterConfig.IPAddressType ipaddressType;
				if (this.CheckForInvalidIPAddress(dataObject.GatewayIPAddresses, out ipaddressType, out ipaddress))
				{
					switch (ipaddressType)
					{
					case SetPerimeterConfig.IPAddressType.IPv6:
						base.WriteError(new IPv6AddressesAreNotAllowedInGatewayIPAddressesException(ipaddress.ToString()), ErrorCategory.InvalidOperation, null);
						break;
					case SetPerimeterConfig.IPAddressType.InvalidIPv4:
						base.WriteError(new InvalidIPv4AddressesAreNotAllowedInGatewayIPAddressesException(ipaddress.ToString()), ErrorCategory.InvalidOperation, null);
						break;
					case SetPerimeterConfig.IPAddressType.ReservedIPv4:
						base.WriteError(new ReservedIPv4AddressesAreNotAllowedInGatewayIPAddressesException(ipaddress.ToString()), ErrorCategory.InvalidOperation, null);
						break;
					}
				}
				if (dataObject.GatewayIPAddresses.Count > 40)
				{
					base.WriteError(new MaximumAllowedNumberOfGatewayIPAddressesExceededException(40), ErrorCategory.InvalidOperation, null);
				}
			}
			if (!dataObject.IPSafelistingSyncEnabled && dataObject.EhfConfigSyncEnabled && (dataObject.IsChanged(PerimeterConfigSchema.GatewayIPAddresses) || dataObject.IsChanged(PerimeterConfigSchema.InternalServerIPAddresses)))
			{
				base.WriteError(new CannotAddIPSafelistsIfIPSafelistingSyncDisabledException(), ErrorCategory.InvalidOperation, null);
			}
			if (dataObject.IsChanged(PerimeterConfigSchema.PerimeterOrgId) && !string.IsNullOrEmpty(dataObject.PerimeterOrgId) && !dataObject.EhfConfigSyncEnabled)
			{
				base.WriteError(new CannotSetPerimeterOrgIdIfEhfConfigSyncIsDisabledException(), ErrorCategory.InvalidOperation, null);
			}
			if ((dataObject.IsChanged(PerimeterConfigSchema.RouteOutboundViaFfoFrontendEnabled) || dataObject.IsChanged(PerimeterConfigSchema.RouteOutboundViaEhfEnabled)) && dataObject.RouteOutboundViaFfoFrontendEnabled == dataObject.RouteOutboundViaEhfEnabled)
			{
				base.WriteError(new CannotSetBothEhfAndFfoRoutingException(), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000D18C File Offset: 0x0000B38C
		private bool CheckIPv6AddressIsPresent(MultiValuedProperty<IPAddress> ipList, out IPAddress ipv6Address)
		{
			ipv6Address = null;
			foreach (IPAddress ipaddress in ipList)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					ipv6Address = ipaddress;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		private bool CheckForInvalidIPAddress(MultiValuedProperty<IPAddress> ipList, out SetPerimeterConfig.IPAddressType ipType, out IPAddress ipAddress)
		{
			ipType = SetPerimeterConfig.IPAddressType.IPv4;
			ipAddress = null;
			foreach (IPAddress ipaddress in ipList)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					ipAddress = ipaddress;
					ipType = SetPerimeterConfig.IPAddressType.IPv6;
					return true;
				}
				if (!IPAddressValidation.IsValidIPv4Address(ipaddress.ToString()))
				{
					ipAddress = ipaddress;
					ipType = SetPerimeterConfig.IPAddressType.InvalidIPv4;
					return true;
				}
				if (IPAddressValidation.IsReservedIPv4Address(ipaddress.ToString()))
				{
					ipAddress = ipaddress;
					ipType = SetPerimeterConfig.IPAddressType.ReservedIPv4;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000D280 File Offset: 0x0000B480
		private void CheckForDuplicates(PerimeterConfig config)
		{
			if (config.GatewayIPAddresses == null || config.InternalServerIPAddresses == null || config.GatewayIPAddresses.Count == 0 || config.InternalServerIPAddresses.Count == 0)
			{
				return;
			}
			HashSet<IPAddress> hashSet = new HashSet<IPAddress>(config.GatewayIPAddresses);
			foreach (IPAddress ipaddress in config.InternalServerIPAddresses)
			{
				if (hashSet.Contains(ipaddress))
				{
					base.WriteError(new DuplicateItemInGatewayIpAddressListException(ipaddress.ToString()), ErrorCategory.InvalidOperation, ipaddress);
				}
			}
		}

		// Token: 0x04000137 RID: 311
		private const int maxGatewayIPAddressCount = 40;

		// Token: 0x02000060 RID: 96
		private enum IPAddressType
		{
			// Token: 0x04000139 RID: 313
			IPv4,
			// Token: 0x0400013A RID: 314
			IPv6,
			// Token: 0x0400013B RID: 315
			InvalidIPv4,
			// Token: 0x0400013C RID: 316
			ReservedIPv4
		}
	}
}
