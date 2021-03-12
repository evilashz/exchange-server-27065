using System;
using System.Management.Automation;
using System.ServiceModel;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA4 RID: 2724
	internal sealed class AcceptedDomainUtility
	{
		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x06006058 RID: 24664 RVA: 0x001916D1 File Offset: 0x0018F8D1
		internal static bool IsDatacenter
		{
			get
			{
				AcceptedDomainUtility.ReloadProvisioningParameters();
				if (AcceptedDomainUtility.isDatacenter == null)
				{
					AcceptedDomainUtility.isDatacenter = new bool?(Datacenter.IsMicrosoftHostedOnly(true));
				}
				return AcceptedDomainUtility.isDatacenter.Value;
			}
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x00191700 File Offset: 0x0018F900
		internal static void ReloadProvisioningParameters()
		{
			if (AcceptedDomainUtility.lastReadTime + AcceptedDomainUtility.reloadTimeSpan > DateTime.UtcNow)
			{
				return;
			}
			lock (AcceptedDomainUtility.LockObject)
			{
				if (!(AcceptedDomainUtility.lastReadTime + AcceptedDomainUtility.reloadTimeSpan > DateTime.UtcNow))
				{
					ServiceEndpoint serviceEndpoint = null;
					ServiceEndpoint serviceEndpoint2 = null;
					ServiceEndpoint serviceEndpoint3 = null;
					ServiceEndpoint serviceEndpoint4 = null;
					ServiceEndpoint serviceEndpoint5 = null;
					try
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 138, "ReloadProvisioningParameters", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\AcceptedDomain\\AcceptedDomainUtility.cs");
						ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
						serviceEndpoint = endpointContainer.GetEndpoint(ServiceEndpointId.CoexistenceParentDomain);
						serviceEndpoint2 = endpointContainer.GetEndpoint(ServiceEndpointId.CoexistenceDnsEndpoint);
						serviceEndpoint3 = endpointContainer.GetEndpoint(ServiceEndpointId.CoexistenceMailDomainFfo15);
						serviceEndpoint4 = endpointContainer.GetEndpoint(ServiceEndpointId.CoexistenceDnsCname);
						serviceEndpoint5 = endpointContainer.GetEndpoint(ServiceEndpointId.CoexistenceDnsText);
					}
					catch (EndpointContainerNotFoundException)
					{
						TaskLogger.Trace("EndpointContainer was not found.", new object[0]);
					}
					catch (ServiceEndpointNotFoundException)
					{
						TaskLogger.Trace("At least one Coexistence Endpoint was not found.", new object[0]);
					}
					if (serviceEndpoint2 != null)
					{
						AcceptedDomainUtility.dnsRegistrationEndpoint = serviceEndpoint2.Uri;
						if (string.IsNullOrEmpty(serviceEndpoint2.CertificateSubject))
						{
							throw new Exception("Unable to find the certificate.");
						}
						AcceptedDomainUtility.dnsRegistrationCertificateSubject = serviceEndpoint2.CertificateSubject;
						TaskLogger.Trace("Dns Registration Endpoint set to '{0}'", new object[]
						{
							AcceptedDomainUtility.dnsRegistrationEndpoint.ToString()
						});
						TaskLogger.Trace("Dns Registration Subject set to '{0}'", new object[]
						{
							AcceptedDomainUtility.dnsRegistrationCertificateSubject
						});
					}
					AcceptedDomainUtility.coexistenceMailDomainFfo15 = ((serviceEndpoint3 != null) ? serviceEndpoint3.Uri.Host : string.Empty);
					TaskLogger.Trace("Coexistence Mail Domain with FFO 15 set to '{0}'", new object[]
					{
						AcceptedDomainUtility.coexistenceMailDomainFfo15
					});
					AcceptedDomainUtility.coexistenceDnsCnameValue = ((serviceEndpoint4 != null) ? serviceEndpoint4.Uri.Host : "autodiscover.outlook.com");
					TaskLogger.Trace("Coexistence DNS CNAME value set to '{0}'", new object[]
					{
						AcceptedDomainUtility.coexistenceDnsCnameValue
					});
					AcceptedDomainUtility.coexistenceDnsTextValue = string.Format("v=spf1 include:{0} -all", (serviceEndpoint5 != null) ? serviceEndpoint5.Uri.Host : "outlook.com");
					TaskLogger.Trace("Coexistence DNS TEXT value set to '{0}'", new object[]
					{
						AcceptedDomainUtility.coexistenceDnsTextValue
					});
					AcceptedDomainUtility.lastReadTime = DateTime.UtcNow;
					string text = (serviceEndpoint != null) ? serviceEndpoint.Uri.Host : string.Empty;
					if (text != null && !text.StartsWith("."))
					{
						text = "." + text;
					}
					AcceptedDomainUtility.coexistenceParentDomain = ((text != null) ? text : string.Empty);
					TaskLogger.Trace("Coexistence Parent Domain set to '{0}'", new object[]
					{
						AcceptedDomainUtility.coexistenceParentDomain
					});
				}
			}
		}

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x0600605A RID: 24666 RVA: 0x001919E8 File Offset: 0x0018FBE8
		internal static string CoexistenceMailDomainFfo15
		{
			get
			{
				AcceptedDomainUtility.ReloadProvisioningParameters();
				return AcceptedDomainUtility.coexistenceMailDomainFfo15;
			}
		}

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x0600605B RID: 24667 RVA: 0x001919F4 File Offset: 0x0018FBF4
		internal static string CoexistenceDnsCnameValue
		{
			get
			{
				AcceptedDomainUtility.ReloadProvisioningParameters();
				return AcceptedDomainUtility.coexistenceDnsCnameValue;
			}
		}

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x0600605C RID: 24668 RVA: 0x00191A00 File Offset: 0x0018FC00
		internal static string CoexistenceDnsTextValue
		{
			get
			{
				AcceptedDomainUtility.ReloadProvisioningParameters();
				return AcceptedDomainUtility.coexistenceDnsTextValue;
			}
		}

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x0600605D RID: 24669 RVA: 0x00191A0C File Offset: 0x0018FC0C
		internal static string CoexistenceParentDomain
		{
			get
			{
				AcceptedDomainUtility.ReloadProvisioningParameters();
				return AcceptedDomainUtility.coexistenceParentDomain;
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x0600605E RID: 24670 RVA: 0x00191A18 File Offset: 0x0018FC18
		internal static Uri DnsRegistrationEndpoint
		{
			get
			{
				AcceptedDomainUtility.ReloadProvisioningParameters();
				return AcceptedDomainUtility.dnsRegistrationEndpoint;
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x0600605F RID: 24671 RVA: 0x00191A24 File Offset: 0x0018FC24
		internal static string DnsRegistrationCertificateSubject
		{
			get
			{
				AcceptedDomainUtility.ReloadProvisioningParameters();
				return AcceptedDomainUtility.dnsRegistrationCertificateSubject;
			}
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x00191A30 File Offset: 0x0018FC30
		internal static bool IsCoexistenceDomain(string domainName)
		{
			return AcceptedDomainUtility.IsDatacenter && !string.IsNullOrEmpty(AcceptedDomainUtility.CoexistenceParentDomain) && domainName.EndsWith(AcceptedDomainUtility.CoexistenceParentDomain, StringComparison.InvariantCultureIgnoreCase) && domainName.Length > AcceptedDomainUtility.CoexistenceParentDomain.Length;
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x00191A68 File Offset: 0x0018FC68
		internal static void RegisterCoexistenceDomain(string domainName)
		{
			if (string.IsNullOrEmpty(AcceptedDomainUtility.DnsRegistrationEndpoint.ToString()) || string.IsNullOrEmpty(AcceptedDomainUtility.DnsRegistrationCertificateSubject))
			{
				throw new CommunicationException("Cannot Register Coexistence Domain");
			}
			WebSvcDns webSvcDns = new WebSvcDns(AcceptedDomainUtility.DnsRegistrationEndpoint, AcceptedDomainUtility.DnsRegistrationCertificateSubject);
			webSvcDns.RegisterDomain(domainName, AcceptedDomainUtility.CoexistenceMailDomainFfo15, AcceptedDomainUtility.CoexistenceDnsCnameValue, AcceptedDomainUtility.CoexistenceDnsTextValue);
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x00191AC4 File Offset: 0x0018FCC4
		internal static void DeregisterCoexistenceDomain(string domainName)
		{
			if (string.IsNullOrEmpty(AcceptedDomainUtility.DnsRegistrationEndpoint.ToString()) || string.IsNullOrEmpty(AcceptedDomainUtility.DnsRegistrationCertificateSubject))
			{
				throw new CommunicationException("Cannot Deregister Coexistence Domain");
			}
			WebSvcDns webSvcDns = new WebSvcDns(AcceptedDomainUtility.DnsRegistrationEndpoint, AcceptedDomainUtility.DnsRegistrationCertificateSubject);
			webSvcDns.DeregisterDomain(domainName);
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x00191B10 File Offset: 0x0018FD10
		internal static void ValidateCatchAllRecipient(ADRecipient resolvedCatchAllRecipient, AcceptedDomain dataObject, bool catchAllRecipientModified, Task.TaskErrorLoggingDelegate errorWriter)
		{
			if (resolvedCatchAllRecipient != null)
			{
				if (resolvedCatchAllRecipient.RecipientType != RecipientType.SystemAttendantMailbox && resolvedCatchAllRecipient.RecipientType != RecipientType.SystemMailbox && resolvedCatchAllRecipient.RecipientType != RecipientType.UserMailbox && resolvedCatchAllRecipient.RecipientType != RecipientType.MailContact && resolvedCatchAllRecipient.RecipientType != RecipientType.MailUser)
				{
					errorWriter(new CatchAllRecipientTypeNotAllowedException(), ErrorCategory.InvalidArgument, null);
				}
				if (resolvedCatchAllRecipient.RecipientTypeDetails == RecipientTypeDetails.ArbitrationMailbox)
				{
					errorWriter(new CatchAllRecipientNotAllowedForArbitrationMailboxException(), ErrorCategory.InvalidArgument, null);
				}
			}
			bool flag;
			if (catchAllRecipientModified)
			{
				flag = (resolvedCatchAllRecipient != null);
			}
			else
			{
				flag = (dataObject.CatchAllRecipientID != null);
			}
			if (flag && dataObject.DomainType != AcceptedDomainType.Authoritative)
			{
				errorWriter(new CatchAllRecipientNotAllowedForNonAuthoritativeAcceptedDomainsException(), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x00191BAC File Offset: 0x0018FDAC
		internal static void ValidateIfOutboundConnectorToRouteDomainIsPresent(IConfigDataProvider dataSession, AcceptedDomain dataObject, Task.TaskWarningLoggingDelegate errorWriter)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.EnforceOutboundConnectorAndAcceptedDomainsRestriction.Enabled && dataObject.DomainType != AcceptedDomainType.Authoritative && !ManageTenantOutboundConnectors.ValidateIfAcceptedDomainCanBeRoutedUsingConnectors(dataSession, dataObject.DomainName))
			{
				errorWriter(Strings.OutboundConnectorToRouteAcceptedDomainNotFound);
			}
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x00191BF3 File Offset: 0x0018FDF3
		internal static void ValidateMatchSubDomains(bool matchSubDomains, AcceptedDomainType domainType, Task.TaskErrorLoggingDelegate errorWriter)
		{
			if (matchSubDomains && domainType != AcceptedDomainType.InternalRelay)
			{
				errorWriter(new MatchSubDomainsIsInternalRelayOnlyException(), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0400353B RID: 13627
		private static object LockObject = new object();

		// Token: 0x0400353C RID: 13628
		private static TimeSpan reloadTimeSpan = TimeSpan.Parse("00:15:00");

		// Token: 0x0400353D RID: 13629
		private static DateTime lastReadTime;

		// Token: 0x0400353E RID: 13630
		private static string coexistenceParentDomain;

		// Token: 0x0400353F RID: 13631
		private static string coexistenceMailDomainFfo15;

		// Token: 0x04003540 RID: 13632
		private static string coexistenceDnsCnameValue;

		// Token: 0x04003541 RID: 13633
		private static string coexistenceDnsTextValue;

		// Token: 0x04003542 RID: 13634
		private static Uri dnsRegistrationEndpoint;

		// Token: 0x04003543 RID: 13635
		private static string dnsRegistrationCertificateSubject;

		// Token: 0x04003544 RID: 13636
		private static bool? isDatacenter;
	}
}
