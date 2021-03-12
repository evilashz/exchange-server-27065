using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B3A RID: 2874
	[Cmdlet("New", "InboundConnector", SupportsShouldProcess = true)]
	public class NewInboundConnector : NewMultitenancySystemConfigurationObjectTask<TenantInboundConnector>
	{
		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x06006762 RID: 26466 RVA: 0x001ABE23 File Offset: 0x001AA023
		// (set) Token: 0x06006763 RID: 26467 RVA: 0x001ABE30 File Offset: 0x001AA030
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x06006764 RID: 26468 RVA: 0x001ABE3E File Offset: 0x001AA03E
		// (set) Token: 0x06006765 RID: 26469 RVA: 0x001ABE4B File Offset: 0x001AA04B
		[Parameter(Mandatory = false)]
		public TenantConnectorType ConnectorType
		{
			get
			{
				return this.DataObject.ConnectorType;
			}
			set
			{
				this.DataObject.ConnectorType = value;
			}
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x06006766 RID: 26470 RVA: 0x001ABE59 File Offset: 0x001AA059
		// (set) Token: 0x06006767 RID: 26471 RVA: 0x001ABE66 File Offset: 0x001AA066
		[Parameter(Mandatory = false)]
		public TenantConnectorSource ConnectorSource
		{
			get
			{
				return this.DataObject.ConnectorSource;
			}
			set
			{
				this.DataObject.ConnectorSource = value;
			}
		}

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x06006768 RID: 26472 RVA: 0x001ABE74 File Offset: 0x001AA074
		// (set) Token: 0x06006769 RID: 26473 RVA: 0x001ABE81 File Offset: 0x001AA081
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return this.DataObject.Comment;
			}
			set
			{
				this.DataObject.Comment = value;
			}
		}

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x0600676A RID: 26474 RVA: 0x001ABE8F File Offset: 0x001AA08F
		// (set) Token: 0x0600676B RID: 26475 RVA: 0x001ABE9C File Offset: 0x001AA09C
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> SenderIPAddresses
		{
			get
			{
				return this.DataObject.SenderIPAddresses;
			}
			set
			{
				this.DataObject.SenderIPAddresses = value;
			}
		}

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x001ABEAA File Offset: 0x001AA0AA
		// (set) Token: 0x0600676D RID: 26477 RVA: 0x001ABEB7 File Offset: 0x001AA0B7
		[Parameter(Mandatory = true)]
		public MultiValuedProperty<AddressSpace> SenderDomains
		{
			get
			{
				return this.DataObject.SenderDomains;
			}
			set
			{
				this.DataObject.SenderDomains = value;
			}
		}

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x0600676E RID: 26478 RVA: 0x001ABEC5 File Offset: 0x001AA0C5
		// (set) Token: 0x0600676F RID: 26479 RVA: 0x001ABED2 File Offset: 0x001AA0D2
		[Parameter(Mandatory = false)]
		public bool RequireTls
		{
			get
			{
				return this.DataObject.RequireTls;
			}
			set
			{
				this.DataObject.RequireTls = value;
			}
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x06006770 RID: 26480 RVA: 0x001ABEE0 File Offset: 0x001AA0E0
		// (set) Token: 0x06006771 RID: 26481 RVA: 0x001ABEED File Offset: 0x001AA0ED
		[Parameter(Mandatory = false)]
		public bool RestrictDomainsToCertificate
		{
			get
			{
				return this.DataObject.RestrictDomainsToCertificate;
			}
			set
			{
				this.DataObject.RestrictDomainsToCertificate = value;
			}
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x06006772 RID: 26482 RVA: 0x001ABEFB File Offset: 0x001AA0FB
		// (set) Token: 0x06006773 RID: 26483 RVA: 0x001ABF08 File Offset: 0x001AA108
		[Parameter(Mandatory = false)]
		public bool RestrictDomainsToIPAddresses
		{
			get
			{
				return this.DataObject.RestrictDomainsToIPAddresses;
			}
			set
			{
				this.DataObject.RestrictDomainsToIPAddresses = value;
			}
		}

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x06006774 RID: 26484 RVA: 0x001ABF16 File Offset: 0x001AA116
		// (set) Token: 0x06006775 RID: 26485 RVA: 0x001ABF23 File Offset: 0x001AA123
		[Parameter(Mandatory = false)]
		public bool CloudServicesMailEnabled
		{
			get
			{
				return this.DataObject.CloudServicesMailEnabled;
			}
			set
			{
				this.DataObject.CloudServicesMailEnabled = value;
			}
		}

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x06006776 RID: 26486 RVA: 0x001ABF31 File Offset: 0x001AA131
		// (set) Token: 0x06006777 RID: 26487 RVA: 0x001ABF3E File Offset: 0x001AA13E
		[Parameter(Mandatory = false)]
		public TlsCertificate TlsSenderCertificateName
		{
			get
			{
				return this.DataObject.TlsSenderCertificateName;
			}
			set
			{
				this.DataObject.TlsSenderCertificateName = value;
			}
		}

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x06006778 RID: 26488 RVA: 0x001ABF4C File Offset: 0x001AA14C
		// (set) Token: 0x06006779 RID: 26489 RVA: 0x001ABF63 File Offset: 0x001AA163
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<AcceptedDomainIdParameter> AssociatedAcceptedDomains
		{
			get
			{
				return (MultiValuedProperty<AcceptedDomainIdParameter>)base.Fields["AssociatedAcceptedDomains"];
			}
			set
			{
				base.Fields["AssociatedAcceptedDomains"] = value;
			}
		}

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x0600677A RID: 26490 RVA: 0x001ABF76 File Offset: 0x001AA176
		// (set) Token: 0x0600677B RID: 26491 RVA: 0x001ABFA1 File Offset: 0x001AA1A1
		[Parameter(Mandatory = false)]
		public bool BypassValidation
		{
			get
			{
				return base.Fields.Contains("BypassValidation") && (bool)base.Fields["BypassValidation"];
			}
			set
			{
				base.Fields["BypassValidation"] = value;
			}
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x0600677C RID: 26492 RVA: 0x001ABFB9 File Offset: 0x001AA1B9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewInboundConnector(base.Name);
			}
		}

		// Token: 0x0600677D RID: 26493 RVA: 0x001ABFC8 File Offset: 0x001AA1C8
		protected override IConfigurable PrepareDataObject()
		{
			TenantInboundConnector tenantInboundConnector = (TenantInboundConnector)base.PrepareDataObject();
			tenantInboundConnector.SetId(base.DataSession as IConfigurationSession, base.Name);
			return tenantInboundConnector;
		}

		// Token: 0x0600677E RID: 26494 RVA: 0x001ABFFC File Offset: 0x001AA1FC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.Fields.IsModified("AssociatedAcceptedDomains"))
			{
				NewInboundConnector.ValidateAssociatedAcceptedDomains(this.AssociatedAcceptedDomains, base.DataSession, this.DataObject, this.RootId, this, new Func<IIdentityParameter, IConfigDataProvider, ObjectId, LocalizedString?, LocalizedString?, IConfigurable>(base.GetDataObject<AcceptedDomain>));
			}
			NewInboundConnector.ValidateSenderIPAddresses(this.DataObject.SenderIPAddresses, this, this.BypassValidation);
			if (this.DataObject.ConnectorType == TenantConnectorType.OnPremises && !this.BypassValidation)
			{
				MultiValuedProperty<IPRange> ffoDCIPs;
				MultiValuedProperty<SmtpX509IdentifierEx> ffoFDSmtpCerts;
				MultiValuedProperty<ServiceProviderSettings> serviceProviders;
				if (!HygieneDCSettings.GetSettings(out ffoDCIPs, out ffoFDSmtpCerts, out serviceProviders))
				{
					base.WriteError(new ConnectorValidationFailedException(), ErrorCategory.ConnectionError, null);
				}
				NewInboundConnector.ValidateSenderIPAddressRestrictions(this.DataObject.SenderIPAddresses, ffoDCIPs, serviceProviders, this);
				NewInboundConnector.ValidateTlsSenderCertificateRestrictions(this.DataObject.TlsSenderCertificateName, ffoFDSmtpCerts, serviceProviders, this);
			}
			IEnumerable<TenantInboundConnector> enumerable = base.DataSession.FindPaged<TenantInboundConnector>(null, ((IConfigurationSession)base.DataSession).GetOrgContainerId().GetDescendantId(this.DataObject.ParentPath), false, null, ADGenericPagedReader<TenantInboundConnector>.DefaultPageSize);
			foreach (TenantInboundConnector tenantInboundConnector in enumerable)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(this.DataObject.Name, tenantInboundConnector.Name))
				{
					base.WriteError(new ErrorInboundConnectorAlreadyExistsException(tenantInboundConnector.Name), ErrorCategory.InvalidOperation, null);
					break;
				}
			}
			NewInboundConnector.CheckSenderIpAddressesOverlap(base.DataSession, this.DataObject, this);
			TaskLogger.LogExit();
		}

		// Token: 0x0600677F RID: 26495 RVA: 0x001AC180 File Offset: 0x001AA380
		internal static void ValidateSenderIPAddresses(IEnumerable<IPRange> addressRanges, Task task, bool bypassValidation)
		{
			if (addressRanges == null)
			{
				return;
			}
			foreach (IPRange iprange in addressRanges)
			{
				if (iprange.LowerBound.AddressFamily == AddressFamily.InterNetworkV6 || iprange.UpperBound.AddressFamily == AddressFamily.InterNetworkV6)
				{
					task.WriteError(new IPv6AddressesRangesAreNotAllowedInConnectorException(iprange.Expression), ErrorCategory.InvalidArgument, null);
				}
				if (iprange.RangeFormat != IPRange.Format.SingleAddress && iprange.RangeFormat != IPRange.Format.CIDR)
				{
					task.WriteError(new InvalidIPRangeFormatException(iprange.Expression), ErrorCategory.InvalidArgument, null);
				}
				if (iprange.RangeFormat == IPRange.Format.CIDR && iprange.CIDRLength < 24)
				{
					task.WriteError(new InvalidCidrRangeException(iprange.Expression, 24), ErrorCategory.InvalidArgument, null);
				}
				NewInboundConnector.ValidateIPAddress(iprange, iprange.UpperBound, task, bypassValidation);
				NewInboundConnector.ValidateIPAddress(iprange, iprange.LowerBound, task, bypassValidation);
			}
		}

		// Token: 0x06006780 RID: 26496 RVA: 0x001AC290 File Offset: 0x001AA490
		internal static void CheckSenderIpAddressesOverlap(IConfigDataProvider dataSession, TenantInboundConnector dataObject, Task task)
		{
			if (task == null || dataObject.SenderIPAddresses == null)
			{
				return;
			}
			TenantInboundConnector[] array = (TenantInboundConnector[])dataSession.Find<TenantInboundConnector>(null, null, true, null);
			List<string> list = null;
			List<string> list2 = null;
			using (MultiValuedProperty<IPRange>.Enumerator enumerator = dataObject.SenderIPAddresses.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IPRange ipRange = enumerator.Current;
					bool flag = false;
					foreach (TenantInboundConnector tenantInboundConnector in array)
					{
						if (tenantInboundConnector.Enabled && tenantInboundConnector.SenderIPAddresses != null && ((ADObjectId)tenantInboundConnector.Identity).ObjectGuid != ((ADObjectId)dataObject.Identity).ObjectGuid)
						{
							if (tenantInboundConnector.SenderIPAddresses.Any((IPRange exisingSenderIpRange) => exisingSenderIpRange.Overlaps(ipRange)))
							{
								if (list != null)
								{
									list.Add(tenantInboundConnector.Name);
								}
								else
								{
									list = new List<string>
									{
										tenantInboundConnector.Name
									};
								}
								flag = true;
							}
						}
					}
					if (flag)
					{
						if (list2 != null)
						{
							list2.Add(ipRange.Expression);
						}
						else
						{
							list2 = new List<string>
							{
								ipRange.Expression
							};
						}
					}
				}
			}
			if (list != null && list2 != null)
			{
				task.WriteWarning(Strings.SenderIPAddressOverlapsExistingTenantInboundConnectors(string.Join(", ", list2), string.Join(", ", list)));
			}
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x001AC484 File Offset: 0x001AA684
		internal static void ValidateSenderIPAddressRestrictions(MultiValuedProperty<IPRange> addressRanges, MultiValuedProperty<IPRange> ffoDCIPs, MultiValuedProperty<ServiceProviderSettings> serviceProviders, Task task)
		{
			if (MultiValuedPropertyBase.IsNullOrEmpty(addressRanges))
			{
				return;
			}
			using (MultiValuedProperty<IPRange>.Enumerator enumerator = addressRanges.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IPRange ipRange = enumerator.Current;
					if (!MultiValuedPropertyBase.IsNullOrEmpty(ffoDCIPs))
					{
						if (ffoDCIPs.Any((IPRange ffoDCIP) => ffoDCIP.Overlaps(ipRange)))
						{
							task.WriteError(new SenderIPAddressOverlapsFfoDCIPAddressesException(ipRange.Expression), ErrorCategory.InvalidArgument, null);
						}
					}
					if (!MultiValuedPropertyBase.IsNullOrEmpty(serviceProviders))
					{
						if (serviceProviders.Any((ServiceProviderSettings serviceProvider) => serviceProvider.IPRanges != null && serviceProvider.IPRanges.Any((IPRange providerIPRange) => providerIPRange != null && providerIPRange.Overlaps(ipRange))))
						{
							task.WriteError(new SenderIPAddressOverlapsServiceProviderIPAddressesException(ipRange.Expression), ErrorCategory.InvalidArgument, null);
						}
					}
				}
			}
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x001AC5A8 File Offset: 0x001AA7A8
		internal static void ValidateTlsSenderCertificateRestrictions(TlsCertificate certificate, MultiValuedProperty<SmtpX509IdentifierEx> ffoFDSmtpCerts, MultiValuedProperty<ServiceProviderSettings> serviceProviders, Task task)
		{
			if (certificate == null)
			{
				return;
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(ffoFDSmtpCerts) && ffoFDSmtpCerts.Any((SmtpX509IdentifierEx ffoFDSmtpCert) => ffoFDSmtpCert.Matches(certificate)))
			{
				task.WriteError(new TlsSenderCertificateNameMatchesFfoFDSmtpCertificateException(certificate.ToString()), ErrorCategory.InvalidArgument, null);
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(serviceProviders) && serviceProviders.Any((ServiceProviderSettings serviceProvider) => serviceProvider.Certificates != null && serviceProvider.Certificates.Any((TlsCertificate providerCertificate) => providerCertificate != null && providerCertificate.Equals(certificate))))
			{
				task.WriteError(new TlsSenderCertificateNameMatchesServiceProviderCertificateException(certificate.ToString()), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x001AC634 File Offset: 0x001AA834
		internal static void ValidateAssociatedAcceptedDomains(MultiValuedProperty<AcceptedDomainIdParameter> domainIdParameters, IConfigDataProvider dataSession, TenantInboundConnector dataObject, ObjectId rootId, Task task, Func<IIdentityParameter, IConfigDataProvider, ObjectId, LocalizedString?, LocalizedString?, IConfigurable> acceptedDomainsGetter)
		{
			if (domainIdParameters != null)
			{
				NewInboundConnector.ValidateCentralizedMailControlAndAssociatedAcceptedDomainsRestriction(dataSession, dataObject, task);
				dataObject.AssociatedAcceptedDomains.Clear();
				using (MultiValuedProperty<AcceptedDomainIdParameter>.Enumerator enumerator = domainIdParameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AcceptedDomainIdParameter acceptedDomainIdParameter = enumerator.Current;
						AcceptedDomain acceptedDomain = (AcceptedDomain)acceptedDomainsGetter(acceptedDomainIdParameter, dataSession, rootId, new LocalizedString?(Strings.ErrorDefaultDomainNotFound(acceptedDomainIdParameter)), new LocalizedString?(Strings.ErrorDefaultDomainNotUnique(acceptedDomainIdParameter)));
						dataObject.AssociatedAcceptedDomains.Add(acceptedDomain.Id);
					}
					return;
				}
			}
			dataObject.AssociatedAcceptedDomains.Clear();
		}

		// Token: 0x06006784 RID: 26500 RVA: 0x001AC6D4 File Offset: 0x001AA8D4
		internal static bool FindTenantScopedOnPremiseInboundConnector(IConfigDataProvider dataSession, Func<TenantInboundConnector, bool> connectorMatches = null)
		{
			TenantInboundConnector[] array = (TenantInboundConnector[])dataSession.Find<TenantInboundConnector>(null, null, true, null);
			foreach (TenantInboundConnector tenantInboundConnector in array)
			{
				if (tenantInboundConnector.Enabled && tenantInboundConnector.ConnectorType == TenantConnectorType.OnPremises && (tenantInboundConnector.AssociatedAcceptedDomains == null || tenantInboundConnector.AssociatedAcceptedDomains.Count == 0) && (connectorMatches == null || connectorMatches(tenantInboundConnector)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x001AC778 File Offset: 0x001AA978
		private static void ValidateCentralizedMailControlAndAssociatedAcceptedDomainsRestriction(IConfigDataProvider dataSession, TenantInboundConnector dataObject, Task task)
		{
			TenantOutboundConnector[] array = (TenantOutboundConnector[])dataSession.Find<TenantOutboundConnector>(null, null, true, null);
			foreach (TenantOutboundConnector tenantOutboundConnector in array)
			{
				if (tenantOutboundConnector.Enabled && tenantOutboundConnector.RouteAllMessagesViaOnPremises)
				{
					if (!NewInboundConnector.FindTenantScopedOnPremiseInboundConnector(dataSession, (TenantInboundConnector c) => ((ADObjectId)c.Identity).ObjectGuid != ((ADObjectId)dataObject.Identity).ObjectGuid))
					{
						task.WriteError(new TenantScopedInboundConnectorRequiredForCMCConnectorException(tenantOutboundConnector.Name), ErrorCategory.InvalidArgument, null);
					}
					break;
				}
			}
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x001AC7FE File Offset: 0x001AA9FE
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<TenantInboundConnector>(this, this.DataObject, null);
			TaskLogger.LogExit();
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x001AC820 File Offset: 0x001AAA20
		private static void ValidateIPAddress(IPRange ipRange, IPAddress address, Task task, bool bypassValidation)
		{
			if (!IPAddressValidation.IsValidIPv4Address(address.ToString()))
			{
				task.WriteError(new ConnectorIPRangeContainsInvalidIPv4AddressException(ipRange.Expression), ErrorCategory.InvalidArgument, null);
			}
			if (!bypassValidation && IPAddressValidation.IsReservedIPv4Address(address.ToString()))
			{
				task.WriteError(new IPRangeInConnectorContainsReservedIPAddressesException(ipRange.Expression), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x04003668 RID: 13928
		private const string AssociatedAcceptedDomainsField = "AssociatedAcceptedDomains";
	}
}
