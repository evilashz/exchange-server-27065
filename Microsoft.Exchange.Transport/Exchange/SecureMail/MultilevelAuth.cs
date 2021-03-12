using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.SecureMail
{
	// Token: 0x0200051C RID: 1308
	internal static class MultilevelAuth
	{
		// Token: 0x06003D24 RID: 15652 RVA: 0x000FF688 File Offset: 0x000FD888
		public static bool TryGetOrganizationScopeResult(IReadOnlyMailItem mailItem, ITransportConfiguration configuration, out OrganizationScopeResult organizationScope)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			if (mailItem.Directionality != MailDirectionality.Originating)
			{
				organizationScope = new OrganizationScopeResult(false, true, "Directionality is not originating");
				return true;
			}
			if (MultilevelAuth.IsInternalMail(mailItem))
			{
				organizationScope = new OrganizationScopeResult(true, true, "Auth-As header indicating it is internal found");
				return true;
			}
			if (mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Cross-Premises-Headers-Promoted") != null)
			{
				organizationScope = new OrganizationScopeResult(false, true, "Cross-premise headers were promoted and Auth-As header was not internal");
				return true;
			}
			if (mailItem.From == RoutingAddress.Empty)
			{
				organizationScope = new OrganizationScopeResult(false, false, "Mail From is empty");
				return true;
			}
			string domainPart = mailItem.From.DomainPart;
			if (string.IsNullOrEmpty(domainPart))
			{
				organizationScope = new OrganizationScopeResult(false, false, string.Format("Domain part of Mail From {0} is empty", mailItem.From.ToString()));
				return true;
			}
			if (mailItem.OrganizationId == null)
			{
				organizationScope = new OrganizationScopeResult(false, false, "Mail has not been attributed");
				return true;
			}
			PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
			if (!configuration.TryGetAcceptedDomainTable(mailItem.OrganizationId, out perTenantAcceptedDomainTable))
			{
				organizationScope = null;
				return false;
			}
			if (perTenantAcceptedDomainTable.AcceptedDomainTable.Find(domainPart) != null)
			{
				organizationScope = new OrganizationScopeResult(true, false, string.Format("Sender domain {0} is an accepted domain", domainPart));
				return true;
			}
			organizationScope = new OrganizationScopeResult(false, true, string.Format("No criteria matches", domainPart));
			return true;
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x000FF7D1 File Offset: 0x000FD9D1
		public static bool IsAuthenticated(IReadOnlyMailItem mailItem)
		{
			return MultilevelAuth.IsAuthenticated(mailItem.RootPart.Headers);
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x000FF7E3 File Offset: 0x000FD9E3
		public static bool IsInternalMail(IReadOnlyMailItem mailItem)
		{
			return MultilevelAuth.IsInternalMail(mailItem.RootPart.Headers);
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000FF7F5 File Offset: 0x000FD9F5
		public static bool IsAuthenticated(EmailMessage message)
		{
			return MultilevelAuth.IsAuthenticated(message.MimeDocument.RootPart.Headers);
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000FF80C File Offset: 0x000FDA0C
		public static bool IsInternalMail(EmailMessage message)
		{
			return MultilevelAuth.IsInternalMail(message.MimeDocument.RootPart.Headers);
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x000FF824 File Offset: 0x000FDA24
		public static bool IsAuthenticated(HeaderList headers)
		{
			string text = MultilevelAuth.OptHeaderValue(headers, "X-MS-Exchange-Organization-AuthAs");
			return text != null && !SubmitAuthCategory.Anonymous.Matches(text);
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x000FF850 File Offset: 0x000FDA50
		public static bool IsInternalMail(HeaderList headers)
		{
			string optCategoryName = MultilevelAuth.OptHeaderValue(headers, "X-MS-Exchange-Organization-AuthAs");
			return SubmitAuthCategory.Internal.Matches(optCategoryName);
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x000FF874 File Offset: 0x000FDA74
		public static void EnsureSecurityAttributesByAgent(ITransportMailItemFacade transportMailItem)
		{
			TransportMailItem transportMailItem2 = (TransportMailItem)transportMailItem;
			MultilevelAuth.EnsureSecurityAttributes(transportMailItem2, SubmitAuthCategory.Internal, MultilevelAuthMechanism.SecureInternalSubmit, null, null, transportMailItem2.RootPart.Headers);
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000FF8A1 File Offset: 0x000FDAA1
		public static void EnsureSecurityAttributes(TransportMailItem transportMailItem, SubmitAuthCategory authCategory, MultilevelAuthMechanism mechanism, string authDomain)
		{
			MultilevelAuth.EnsureSecurityAttributes(transportMailItem, authCategory, mechanism, authDomain, null, transportMailItem.RootPart.Headers);
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x000FF8B8 File Offset: 0x000FDAB8
		public static void EnsureSecurityAttributes(TransportMailItem transportMailItem, SubmitAuthCategory authCategory, MultilevelAuthMechanism mechanism, string authDomain, X509Certificate remoteCertificate, HeaderList headers)
		{
			string dnsPhysicalFullyQualifiedDomainName = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
			MultilevelAuth.EnsureSecurityAttributes(transportMailItem, dnsPhysicalFullyQualifiedDomainName, authCategory, mechanism, authDomain, remoteCertificate, headers);
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000FF8D9 File Offset: 0x000FDAD9
		internal static bool MayWriteAuthDomain(MultilevelAuthMechanism mechanism)
		{
			return mechanism == MultilevelAuthMechanism.MutualTLS;
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x000FF8E0 File Offset: 0x000FDAE0
		private static void EnsureSecurityAttributes(TransportMailItem transportMailItem, string source, SubmitAuthCategory authCategory, MultilevelAuthMechanism mechanism, string authDomain, X509Certificate remoteCertificate, HeaderList headers)
		{
			string text = MultilevelAuth.OptHeaderValue(headers, "X-MS-Exchange-Organization-AuthAs");
			string messageTrackingSecurityInfo;
			if (authCategory.IsAnonymous)
			{
				if (authCategory.Matches(text))
				{
					source = (MultilevelAuth.OptHeaderValue(headers, "X-MS-Exchange-Organization-AuthSource") ?? source);
				}
				MultilevelAuth.WriteAnonymousHeaders(headers, source);
				messageTrackingSecurityInfo = authCategory.FormatLog(mechanism, authDomain);
			}
			else if (text != null)
			{
				messageTrackingSecurityInfo = SubmitAuthCategory.FormatExisting(mechanism, text);
			}
			else
			{
				MultilevelAuth.WriteAuthenticatedHeaders(headers, source, authCategory, mechanism, authDomain, remoteCertificate);
				messageTrackingSecurityInfo = authCategory.FormatLog(mechanism, authDomain);
			}
			transportMailItem.MessageTrackingSecurityInfo = messageTrackingSecurityInfo;
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x000FF960 File Offset: 0x000FDB60
		private static void WriteAuthenticatedHeaders(HeaderList headers, string source, SubmitAuthCategory category, MultilevelAuthMechanism mechanism, string domain, X509Certificate remoteCertificate)
		{
			MultilevelAuth.RemoveAuthenticationHeaders(headers);
			MultilevelAuth.AddHeader(headers, "X-MS-Exchange-Organization-AuthSource", source);
			MultilevelAuth.AddHeader(headers, "X-MS-Exchange-Organization-AuthAs", category.Name);
			MultilevelAuth.AddHeader(headers, "X-MS-Exchange-Organization-AuthMechanism", string.Format("{0:x2}", (int)mechanism));
			if (category != SubmitAuthCategory.Internal && MultilevelAuth.MayWriteAuthDomain(mechanism))
			{
				MultilevelAuth.AddHeader(headers, "X-MS-Exchange-Organization-AuthDomain", domain);
			}
			if (category.IsPartner && !ClassificationUtils.ExtractClassifications(headers).Contains("030e9e2f-134b-4020-861c-5bfc616f113d"))
			{
				MultilevelAuth.AddHeader(headers, "X-MS-Exchange-Organization-Classification", "030e9e2f-134b-4020-861c-5bfc616f113d");
			}
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000FF9F2 File Offset: 0x000FDBF2
		private static void WriteAnonymousHeaders(HeaderList headers, string source)
		{
			MultilevelAuth.RemoveAuthenticationHeaders(headers);
			MultilevelAuth.AddHeader(headers, "X-MS-Exchange-Organization-AuthSource", source);
			MultilevelAuth.AddHeader(headers, "X-MS-Exchange-Organization-AuthAs", SubmitAuthCategory.Anonymous.Name);
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000FFA1C File Offset: 0x000FDC1C
		private static void RemoveAuthenticationHeaders(HeaderList headers)
		{
			headers.RemoveAll("X-MS-Exchange-Organization-AuthDomain");
			headers.RemoveAll("X-MS-Exchange-Organization-AuthMechanism");
			headers.RemoveAll("X-MS-Exchange-Organization-AuthSource");
			headers.RemoveAll("X-MS-Exchange-Organization-AuthAs");
			headers.RemoveAll("X-MS-Exchange-Organization-AuthCertificateSubject");
			headers.RemoveAll("X-MS-Exchange-Organization-AuthCertificateIssuer");
			headers.RemoveAll("X-MS-Exchange-Organization-AuthCertificateSerialNumber");
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000FFA78 File Offset: 0x000FDC78
		private static string OptHeaderValue(HeaderList headers, string name)
		{
			Header header = headers.FindFirst(name);
			if (header == null)
			{
				return null;
			}
			string text = string.Empty;
			try
			{
				text = header.Value.Trim();
			}
			catch (ExchangeDataException)
			{
			}
			if (text.Length <= 0)
			{
				return null;
			}
			return text;
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000FFAC8 File Offset: 0x000FDCC8
		private static void AddHeader(HeaderList headers, string key, string value)
		{
			headers.AppendChild(new AsciiTextHeader(key, value));
		}

		// Token: 0x04001F1A RID: 7962
		public const string AuthDomainHeader = "X-MS-Exchange-Organization-AuthDomain";

		// Token: 0x04001F1B RID: 7963
		public const string AuthMechanismHeader = "X-MS-Exchange-Organization-AuthMechanism";

		// Token: 0x04001F1C RID: 7964
		public const string AuthSourceHeader = "X-MS-Exchange-Organization-AuthSource";

		// Token: 0x04001F1D RID: 7965
		public const string AuthAsHeader = "X-MS-Exchange-Organization-AuthAs";

		// Token: 0x04001F1E RID: 7966
		public const string AuthCertificateSubject = "X-MS-Exchange-Organization-AuthCertificateSubject";

		// Token: 0x04001F1F RID: 7967
		public const string AuthCertificateIssuer = "X-MS-Exchange-Organization-AuthCertificateIssuer";

		// Token: 0x04001F20 RID: 7968
		public const string AuthCertificateSerialNumber = "X-MS-Exchange-Organization-AuthCertificateSerialNumber";
	}
}
