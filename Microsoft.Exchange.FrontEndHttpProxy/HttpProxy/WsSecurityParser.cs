﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IO;
using System.Security.Principal;
using System.ServiceModel;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.X509CertAuth;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000080 RID: 128
	internal class WsSecurityParser
	{
		// Token: 0x060003DE RID: 990 RVA: 0x0001704F File Offset: 0x0001524F
		public WsSecurityParser(int traceContext)
		{
			this.TraceContext = traceContext;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0001705E File Offset: 0x0001525E
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00017066 File Offset: 0x00015266
		private int TraceContext { get; set; }

		// Token: 0x060003E1 RID: 993 RVA: 0x00017070 File Offset: 0x00015270
		internal KeyValuePair<string, bool> FindAddressFromWsSecurityRequest(Stream stream)
		{
			bool value = false;
			string key = this.FindAddressFromWsSecurity(stream, WsSecurityHeaderType.WSSecurityAuth, out value);
			return new KeyValuePair<string, bool>(key, value);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00017094 File Offset: 0x00015294
		internal string FindAddressFromPartnerAuthRequest(Stream stream)
		{
			bool flag;
			return this.FindAddressFromWsSecurity(stream, WsSecurityHeaderType.PartnerAuth, out flag);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000170AC File Offset: 0x000152AC
		internal string FindAddressFromX509CertAuthRequest(Stream stream)
		{
			bool flag;
			return this.FindAddressFromWsSecurity(stream, WsSecurityHeaderType.X509CertAuth, out flag);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000170C4 File Offset: 0x000152C4
		internal string FindAddressFromWsSecurity(Stream stream, WsSecurityHeaderType headerType, out bool isDelegationToken)
		{
			isDelegationToken = false;
			long position = stream.Position;
			try
			{
				XmlReader xmlReader = XmlReader.Create(stream);
				if (xmlReader.Settings != null && xmlReader.Settings.DtdProcessing != DtdProcessing.Prohibit)
				{
					xmlReader.Settings.DtdProcessing = DtdProcessing.Prohibit;
				}
				XmlElement xmlElement = null;
				XmlElement xmlElement2 = null;
				XmlElement xmlElement3 = null;
				xmlReader.MoveToContent();
				bool flag = true;
				while (flag && xmlReader.Read())
				{
					if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Header")
					{
						if (!(xmlReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/"))
						{
							if (!(xmlReader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
							{
								continue;
							}
						}
						while (flag && xmlReader.Read())
						{
							if (stream.Position > 73628L)
							{
								throw new QuotaExceededException();
							}
							if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.LocalName == "Header" && (xmlReader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/" || xmlReader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
							{
								ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WsSecurityParser::FindAddressFromWsSecurity]: Context {0}; Hit the end of the SOAP header unexpectedly", this.TraceContext);
								flag = false;
								break;
							}
							if (headerType != WsSecurityHeaderType.PartnerAuth)
							{
								if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Security" && xmlReader.NamespaceURI == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")
								{
									while (flag)
									{
										if (!xmlReader.Read())
										{
											break;
										}
										if (stream.Position > 73628L)
										{
											throw new QuotaExceededException();
										}
										if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.LocalName == "Security" && xmlReader.NamespaceURI == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")
										{
											ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WsSecurityParser::FindAddressFromWsSecurity]: Context {0}; Hit the end of the WS-Security header unexpectedly", this.TraceContext);
											flag = false;
											break;
										}
										if (xmlReader.NodeType == XmlNodeType.Element && ((headerType == WsSecurityHeaderType.WSSecurityAuth && xmlReader.LocalName == "EncryptedData" && xmlReader.NamespaceURI == "http://www.w3.org/2001/04/xmlenc#") || (headerType == WsSecurityHeaderType.X509CertAuth && xmlReader.LocalName == "BinarySecurityToken" && xmlReader.NamespaceURI == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")))
										{
											using (XmlReader xmlReader2 = xmlReader.ReadSubtree())
											{
												XmlDocument xmlDocument = new XmlDocument();
												xmlDocument.Load(xmlReader2);
												xmlElement = xmlDocument.DocumentElement;
											}
											flag = false;
											break;
										}
									}
								}
							}
							else
							{
								if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "ExchangeImpersonation" && xmlReader.NamespaceURI == "http://schemas.microsoft.com/exchange/services/2006/types")
								{
									using (XmlReader xmlReader3 = xmlReader.ReadSubtree())
									{
										XmlDocument xmlDocument2 = new XmlDocument();
										xmlDocument2.Load(xmlReader3);
										xmlElement2 = xmlDocument2.DocumentElement;
									}
									flag = false;
									break;
								}
								if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Attribute" && xmlReader.NamespaceURI == "urn:oasis:names:tc:SAML:1.0:assertion" && xmlReader.HasAttributes)
								{
									string attribute = xmlReader.GetAttribute("AttributeName");
									if (attribute == "targettenant")
									{
										using (XmlReader xmlReader4 = xmlReader.ReadSubtree())
										{
											XmlDocument xmlDocument3 = new XmlDocument();
											xmlDocument3.Load(xmlReader4);
											xmlElement3 = xmlDocument3.DocumentElement;
										}
										flag = false;
										break;
									}
								}
							}
						}
					}
				}
				if (headerType == WsSecurityHeaderType.PartnerAuth)
				{
					if (xmlElement2 != null)
					{
						if (xmlElement2.NodeType == XmlNodeType.Element && xmlElement2.FirstChild.NodeType == XmlNodeType.Element && xmlElement2.FirstChild.LocalName == "ConnectingSID")
						{
							XmlNode firstChild = xmlElement2.FirstChild.FirstChild;
							if (firstChild.LocalName == "PrincipalName" || firstChild.LocalName == "PrimarySmtpAddress" || firstChild.LocalName == "SmtpAddress" || firstChild.LocalName == "SID")
							{
								return firstChild.InnerXml;
							}
							ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[WsSecurityParser::FindAddressFromWsSecurity]: Context {0}; Unexpected type {1} in ConnectingSID in impersonation header", this.TraceContext, firstChild.Name);
							return null;
						}
					}
					else if (xmlElement3 != null && xmlElement3.NodeType == XmlNodeType.Element && xmlElement3.FirstChild.LocalName == "AttributeValue")
					{
						return xmlElement3.FirstChild.InnerText;
					}
				}
				else if (xmlElement != null)
				{
					ExternalAuthentication current = ExternalAuthentication.GetCurrent();
					if (!current.Enabled)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[WsSecurityParser::FindAddressFromWsSecurity]: ExternalAuthentication is not enabled");
						return null;
					}
					if (headerType == WsSecurityHeaderType.X509CertAuth)
					{
						AuthorizationContext authorizationContext;
						TokenValidationResults tokenValidationResults = current.TokenValidator.AuthorizationContextFromToken(xmlElement, out authorizationContext);
						if (tokenValidationResults.Result == TokenValidationResult.Valid)
						{
							ReadOnlyCollection<ClaimSet> claimSets = authorizationContext.ClaimSets;
							X509CertUser x509CertUser = null;
							if (!X509CertUser.TryCreateX509CertUser(claimSets, out x509CertUser))
							{
								ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WsSecurityParser::FindAddressFromWsSecurity]: Context {0}; Unable to create the x509certuser", this.TraceContext);
								return null;
							}
							OrganizationId organizationId;
							WindowsIdentity windowsIdentity;
							string arg;
							if (!x509CertUser.TryGetWindowsIdentity(out organizationId, out windowsIdentity, out arg))
							{
								ExTraceGlobals.VerboseTracer.TraceDebug<int, X509CertUser, string>((long)this.GetHashCode(), "[WsSecurityParser::FindAddressFromWsSecurity]: Context {0}; unable to find the windows identity for cert user: {1}, reason: {2}", this.TraceContext, x509CertUser, arg);
								return null;
							}
							return x509CertUser.UserPrincipalName;
						}
					}
					else
					{
						TokenValidationResults tokenValidationResults2 = current.TokenValidator.FindEmailAddress(xmlElement, out isDelegationToken);
						if (!string.IsNullOrEmpty(tokenValidationResults2.EmailAddress))
						{
							return tokenValidationResults2.EmailAddress;
						}
					}
				}
			}
			catch (XmlException arg2)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, XmlException>((long)this.GetHashCode(), "[WsSecurityParser::FindAddressFromWsSecurity]: Context {0}; XmlException {1}", this.TraceContext, arg2);
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[WsSecurityParser::FindAddressFromWsSecurity]: Context {0}; No email address found in Ws-Trust token", this.TraceContext);
			return null;
		}

		// Token: 0x040002D3 RID: 723
		internal const string SoapHeaderElementName = "Header";

		// Token: 0x040002D4 RID: 724
		internal const string SecurityHeaderElementName = "Security";

		// Token: 0x040002D5 RID: 725
		internal const string EncryptedDataElementName = "EncryptedData";

		// Token: 0x040002D6 RID: 726
		internal const string BinarySecurityTokenElementName = "BinarySecurityToken";

		// Token: 0x040002D7 RID: 727
		internal const string ExchangeImpersonationElementName = "ExchangeImpersonation";

		// Token: 0x040002D8 RID: 728
		internal const string ActionElementName = "Action";

		// Token: 0x040002D9 RID: 729
		internal const string BodyElementName = "Body";

		// Token: 0x040002DA RID: 730
		internal const string GetFederationInformationElementName = "GetFederationInformationRequestMessage";

		// Token: 0x040002DB RID: 731
		internal const string RequestElementName = "Request";

		// Token: 0x040002DC RID: 732
		internal const string DomainElementName = "Domain";

		// Token: 0x040002DD RID: 733
		internal const string ConnectingSIDElementName = "ConnectingSID";

		// Token: 0x040002DE RID: 734
		internal const string PrincipalNameElementName = "PrincipalName";

		// Token: 0x040002DF RID: 735
		internal const string PrimarySmtpAddressElementName = "PrimarySmtpAddress";

		// Token: 0x040002E0 RID: 736
		internal const string SmtpAddressElementName = "SmtpAddress";

		// Token: 0x040002E1 RID: 737
		internal const string SIDElementName = "SID";

		// Token: 0x040002E2 RID: 738
		internal const string SamlAttributeElementName = "Attribute";

		// Token: 0x040002E3 RID: 739
		internal const string AttributeNameElementName = "AttributeName";

		// Token: 0x040002E4 RID: 740
		internal const string AttributeValueElementName = "AttributeValue";

		// Token: 0x040002E5 RID: 741
		internal const string TargetTenantAttributeName = "targettenant";

		// Token: 0x040002E6 RID: 742
		internal const string Soap11Namespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x040002E7 RID: 743
		internal const string Soap12Namespace = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x040002E8 RID: 744
		internal const string WSSecurity200401SNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		// Token: 0x040002E9 RID: 745
		internal const string XmlEncryptionNamespace = "http://www.w3.org/2001/04/xmlenc#";

		// Token: 0x040002EA RID: 746
		internal const string AddressingNamespace = "http://www.w3.org/2005/08/addressing";

		// Token: 0x040002EB RID: 747
		internal const string NamespaceBase = "http://schemas.microsoft.com/exchange/services/2006";

		// Token: 0x040002EC RID: 748
		internal const string TypeNamespace = "http://schemas.microsoft.com/exchange/services/2006/types";

		// Token: 0x040002ED RID: 749
		internal const string SamlNamespace = "urn:oasis:names:tc:SAML:1.0:assertion";

		// Token: 0x040002EE RID: 750
		internal const int MaxSizeOfHeaders = 73628;
	}
}
