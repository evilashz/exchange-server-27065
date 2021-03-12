using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005BC RID: 1468
	[Serializable]
	public class SmtpSendConnectorConfig : MailGateway
	{
		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06004366 RID: 17254 RVA: 0x000FD67C File Offset: 0x000FB87C
		internal override ADObjectSchema Schema
		{
			get
			{
				return SmtpSendConnectorConfig.schema;
			}
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x000FD683 File Offset: 0x000FB883
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SmtpSendConnectorConfig.MostDerivedClass;
			}
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06004368 RID: 17256 RVA: 0x000FD68A File Offset: 0x000FB88A
		// (set) Token: 0x06004369 RID: 17257 RVA: 0x000FD6B5 File Offset: 0x000FB8B5
		[Parameter(Mandatory = false)]
		public bool DNSRoutingEnabled
		{
			get
			{
				if (this[SmtpSendConnectorConfigSchema.DNSRoutingEnabled] == null)
				{
					return string.IsNullOrEmpty(this.SmartHostsString);
				}
				return (bool)this[SmtpSendConnectorConfigSchema.DNSRoutingEnabled];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.DNSRoutingEnabled] = value;
			}
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x000FD6C8 File Offset: 0x000FB8C8
		// (set) Token: 0x0600436B RID: 17259 RVA: 0x000FD6DA File Offset: 0x000FB8DA
		[Parameter(Mandatory = false)]
		public SmtpDomainWithSubdomains TlsDomain
		{
			get
			{
				return (SmtpDomainWithSubdomains)this[SmtpSendConnectorConfigSchema.TlsDomain];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.TlsDomain] = value;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x000FD6E8 File Offset: 0x000FB8E8
		// (set) Token: 0x0600436D RID: 17261 RVA: 0x000FD6FA File Offset: 0x000FB8FA
		[Parameter(Mandatory = false)]
		public TlsAuthLevel? TlsAuthLevel
		{
			get
			{
				return (TlsAuthLevel?)this[SmtpSendConnectorConfigSchema.TlsAuthLevel];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.TlsAuthLevel] = value;
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x0600436E RID: 17262 RVA: 0x000FD70D File Offset: 0x000FB90D
		// (set) Token: 0x0600436F RID: 17263 RVA: 0x000FD71F File Offset: 0x000FB91F
		[Parameter(Mandatory = false)]
		public ErrorPolicies ErrorPolicies
		{
			get
			{
				return (ErrorPolicies)this[SmtpSendConnectorConfigSchema.ErrorPolicies];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.ErrorPolicies] = value;
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x000FD732 File Offset: 0x000FB932
		// (set) Token: 0x06004371 RID: 17265 RVA: 0x000FD744 File Offset: 0x000FB944
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmartHost> SmartHosts
		{
			get
			{
				return (MultiValuedProperty<SmartHost>)this[SmtpSendConnectorConfigSchema.SmartHosts];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.SmartHosts] = value;
			}
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06004372 RID: 17266 RVA: 0x000FD752 File Offset: 0x000FB952
		// (set) Token: 0x06004373 RID: 17267 RVA: 0x000FD764 File Offset: 0x000FB964
		[Parameter(Mandatory = false)]
		public int Port
		{
			get
			{
				return (int)this[SmtpSendConnectorConfigSchema.Port];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.Port] = value;
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06004374 RID: 17268 RVA: 0x000FD777 File Offset: 0x000FB977
		// (set) Token: 0x06004375 RID: 17269 RVA: 0x000FD789 File Offset: 0x000FB989
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectionInactivityTimeOut
		{
			get
			{
				return (EnhancedTimeSpan)this[SmtpSendConnectorConfigSchema.ConnectionInactivityTimeout];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.ConnectionInactivityTimeout] = value;
			}
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x000FD79C File Offset: 0x000FB99C
		// (set) Token: 0x06004377 RID: 17271 RVA: 0x000FD7AE File Offset: 0x000FB9AE
		[Parameter(Mandatory = false)]
		public bool ForceHELO
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.ForceHELO];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.ForceHELO] = value;
			}
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x000FD7C1 File Offset: 0x000FB9C1
		// (set) Token: 0x06004379 RID: 17273 RVA: 0x000FD7D3 File Offset: 0x000FB9D3
		[Parameter(Mandatory = false)]
		public bool FrontendProxyEnabled
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.FrontendProxyEnabled];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.FrontendProxyEnabled] = value;
			}
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x000FD7E6 File Offset: 0x000FB9E6
		// (set) Token: 0x0600437B RID: 17275 RVA: 0x000FD7F8 File Offset: 0x000FB9F8
		[Parameter(Mandatory = false)]
		public bool IgnoreSTARTTLS
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.IgnoreSTARTTLS];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.IgnoreSTARTTLS] = value;
			}
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x0600437C RID: 17276 RVA: 0x000FD80B File Offset: 0x000FBA0B
		// (set) Token: 0x0600437D RID: 17277 RVA: 0x000FD81D File Offset: 0x000FBA1D
		[Parameter(Mandatory = false)]
		public bool CloudServicesMailEnabled
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.CloudServicesMailEnabled];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.CloudServicesMailEnabled] = value;
			}
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x000FD830 File Offset: 0x000FBA30
		// (set) Token: 0x0600437F RID: 17279 RVA: 0x000FD842 File Offset: 0x000FBA42
		[Parameter(Mandatory = false)]
		public Fqdn Fqdn
		{
			get
			{
				return (Fqdn)this[SmtpSendConnectorConfigSchema.Fqdn];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.Fqdn] = value;
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x000FD850 File Offset: 0x000FBA50
		// (set) Token: 0x06004381 RID: 17281 RVA: 0x000FD862 File Offset: 0x000FBA62
		[Parameter(Mandatory = false)]
		public SmtpX509Identifier TlsCertificateName
		{
			get
			{
				return (SmtpX509Identifier)this[SmtpSendConnectorConfigSchema.TlsCertificateName];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.TlsCertificateName] = value;
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x000FD870 File Offset: 0x000FBA70
		// (set) Token: 0x06004383 RID: 17283 RVA: 0x000FD882 File Offset: 0x000FBA82
		[Parameter(Mandatory = false)]
		public bool RequireTLS
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.RequireTLS];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.RequireTLS] = value;
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x000FD895 File Offset: 0x000FBA95
		// (set) Token: 0x06004385 RID: 17285 RVA: 0x000FD8A7 File Offset: 0x000FBAA7
		[Parameter(Mandatory = false)]
		public bool RequireOorg
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.RequireOorg];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.RequireOorg] = value;
			}
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x000FD8BA File Offset: 0x000FBABA
		// (set) Token: 0x06004387 RID: 17287 RVA: 0x000FD8CC File Offset: 0x000FBACC
		[Parameter(Mandatory = false)]
		public override bool Enabled
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.Enabled];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.Enabled] = value;
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x000FD8DF File Offset: 0x000FBADF
		// (set) Token: 0x06004389 RID: 17289 RVA: 0x000FD8F1 File Offset: 0x000FBAF1
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel ProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[SmtpSendConnectorConfigSchema.ProtocolLoggingLevel];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.ProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x0600438A RID: 17290 RVA: 0x000FD904 File Offset: 0x000FBB04
		// (set) Token: 0x0600438B RID: 17291 RVA: 0x000FD916 File Offset: 0x000FBB16
		[Parameter(Mandatory = false)]
		public SmtpSendConnectorConfig.AuthMechanisms SmartHostAuthMechanism
		{
			get
			{
				return (SmtpSendConnectorConfig.AuthMechanisms)this[SmtpSendConnectorConfigSchema.SmartHostAuthMechanism];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.SmartHostAuthMechanism] = value;
			}
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x0600438C RID: 17292 RVA: 0x000FD929 File Offset: 0x000FBB29
		// (set) Token: 0x0600438D RID: 17293 RVA: 0x000FD93B File Offset: 0x000FBB3B
		[Parameter(Mandatory = false)]
		public PSCredential AuthenticationCredential
		{
			get
			{
				return (PSCredential)this[SmtpSendConnectorConfigSchema.AuthenticationCredential];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.AuthenticationCredential] = value;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x0600438E RID: 17294 RVA: 0x000FD949 File Offset: 0x000FBB49
		// (set) Token: 0x0600438F RID: 17295 RVA: 0x000FD95B File Offset: 0x000FBB5B
		[Parameter(Mandatory = false)]
		public bool UseExternalDNSServersEnabled
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.UseExternalDNSServersEnabled];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.UseExternalDNSServersEnabled] = value;
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x000FD96E File Offset: 0x000FBB6E
		// (set) Token: 0x06004391 RID: 17297 RVA: 0x000FD980 File Offset: 0x000FBB80
		[Parameter(Mandatory = false)]
		public bool DomainSecureEnabled
		{
			get
			{
				return (bool)this[SmtpSendConnectorConfigSchema.DomainSecureEnabled];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.DomainSecureEnabled] = value;
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06004392 RID: 17298 RVA: 0x000FD993 File Offset: 0x000FBB93
		// (set) Token: 0x06004393 RID: 17299 RVA: 0x000FD9A5 File Offset: 0x000FBBA5
		[Parameter(Mandatory = false)]
		public IPAddress SourceIPAddress
		{
			get
			{
				return (IPAddress)this[SmtpSendConnectorConfigSchema.SourceIPAddress];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.SourceIPAddress] = value;
			}
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x000FD9B3 File Offset: 0x000FBBB3
		// (set) Token: 0x06004395 RID: 17301 RVA: 0x000FD9C5 File Offset: 0x000FBBC5
		[Parameter(Mandatory = false)]
		public int SmtpMaxMessagesPerConnection
		{
			get
			{
				return (int)this[SmtpSendConnectorConfigSchema.SmtpMaxMessagesPerConnection];
			}
			set
			{
				this[SmtpSendConnectorConfigSchema.SmtpMaxMessagesPerConnection] = value;
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x000FD9D8 File Offset: 0x000FBBD8
		public string SmartHostsString
		{
			get
			{
				return (string)this[SmtpSendConnectorConfigSchema.SmartHostsString];
			}
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x000FD9EA File Offset: 0x000FBBEA
		// (set) Token: 0x06004398 RID: 17304 RVA: 0x000FD9F2 File Offset: 0x000FBBF2
		public string CertificateSubject
		{
			get
			{
				return this.certificateSubject;
			}
			set
			{
				this.certificateSubject = value;
			}
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x000FDA04 File Offset: 0x000FBC04
		internal static object SmartHostsGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[SmtpSendConnectorConfigSchema.SmartHostsString];
			if (string.IsNullOrEmpty(text))
			{
				return new MultiValuedProperty<SmartHost>(false, SmtpSendConnectorConfigSchema.SmartHosts, new SmartHost[0]);
			}
			List<SmartHost> routingHostsFromString = RoutingHost.GetRoutingHostsFromString<SmartHost>(text, (RoutingHost routingHost) => new SmartHost(routingHost));
			return new MultiValuedProperty<SmartHost>(false, SmtpSendConnectorConfigSchema.SmartHosts, routingHostsFromString);
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x000FDA74 File Offset: 0x000FBC74
		internal static void SmartHostsSetter(object value, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				propertyBag[SmtpSendConnectorConfigSchema.SmartHostsString] = string.Empty;
				return;
			}
			MultiValuedProperty<SmartHost> routingHostWrappers = (MultiValuedProperty<SmartHost>)value;
			string value2 = RoutingHost.ConvertRoutingHostsToString<SmartHost>(routingHostWrappers, (SmartHost host) => host.InnerRoutingHost);
			propertyBag[SmtpSendConnectorConfigSchema.SmartHostsString] = value2;
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000FDACC File Offset: 0x000FBCCC
		internal static object AuthenticationCredentialGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[SmtpSendConnectorConfigSchema.AuthenticationUserName];
			string text2 = (string)propertyBag[SmtpSendConnectorConfigSchema.EncryptedAuthenticationPassword];
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				SecureString secureString = new SecureString();
				foreach (char c in text2.ToCharArray())
				{
					secureString.AppendChar(c);
				}
				return new PSCredential(text, secureString);
			}
			return null;
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000FDB44 File Offset: 0x000FBD44
		internal static void AuthenticationCredentialSetter(object value, IPropertyBag propertyBag)
		{
			PSCredential pscredential = value as PSCredential;
			if (pscredential == null)
			{
				propertyBag[SmtpSendConnectorConfigSchema.AuthenticationUserName] = null;
				propertyBag[SmtpSendConnectorConfigSchema.EncryptedAuthenticationPassword] = null;
				return;
			}
			string value2 = string.Empty;
			if (pscredential.Password == null || pscredential.Password.Length == 0)
			{
				return;
			}
			value2 = pscredential.Password.ConvertToUnsecureString();
			propertyBag[SmtpSendConnectorConfigSchema.AuthenticationUserName] = pscredential.UserName;
			propertyBag[SmtpSendConnectorConfigSchema.EncryptedAuthenticationPassword] = value2;
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000FDBBC File Offset: 0x000FBDBC
		internal static object SmartHostAuthMechanismGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[SmtpSendConnectorConfigSchema.SendConnectorFlags];
			return (SmtpSendConnectorConfig.AuthMechanisms)(((long)num & (long)((ulong)-1048576)) >> 20);
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000FDBEC File Offset: 0x000FBDEC
		internal static void SmartHostAuthMechanismSetter(object value, IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[SmtpSendConnectorConfigSchema.SendConnectorFlags];
			num = (((int)value & 4095) << 20 | (num & 1048575));
			propertyBag[SmtpSendConnectorConfigSchema.SendConnectorFlags] = num;
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000FDC33 File Offset: 0x000FBE33
		internal static object TlsAuthLevelGetter(IPropertyBag propertyBag)
		{
			return SmtpSendConnectorConfig.TlsAuthLevelGetter(propertyBag, SmtpSendConnectorConfigSchema.SendConnectorFlags);
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000FDC40 File Offset: 0x000FBE40
		internal static object TlsAuthLevelGetter(IPropertyBag propertyBag, ADPropertyDefinition flagsProperty)
		{
			int num = (int)propertyBag[flagsProperty];
			TlsAuthLevel tlsAuthLevel = (TlsAuthLevel)((num & 4080) >> 4);
			return (tlsAuthLevel == (TlsAuthLevel)0) ? null : new TlsAuthLevel?(tlsAuthLevel);
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000FDC7D File Offset: 0x000FBE7D
		internal static void TlsAuthLevelSetter(object value, IPropertyBag propertyBag)
		{
			SmtpSendConnectorConfig.TlsAuthLevelSetter(value, propertyBag, SmtpSendConnectorConfigSchema.SendConnectorFlags);
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000FDC8C File Offset: 0x000FBE8C
		internal static void TlsAuthLevelSetter(object value, IPropertyBag propertyBag, ADPropertyDefinition flagsProperty)
		{
			if (value == null)
			{
				value = 0;
			}
			int num = (int)propertyBag[flagsProperty];
			num = (((int)value & 255) << 4 | (int)((uint)((long)num & (long)((ulong)-4081))));
			propertyBag[flagsProperty] = num;
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000FDCD8 File Offset: 0x000FBED8
		internal string GetAuthenticationUserName()
		{
			return (string)this[SmtpSendConnectorConfigSchema.AuthenticationUserName];
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000FDCEC File Offset: 0x000FBEEC
		internal bool IsInitialSendConnector()
		{
			MultiValuedProperty<AddressSpace> addressSpaces = base.AddressSpaces;
			if (addressSpaces != null)
			{
				foreach (AddressSpace addressSpace in addressSpaces)
				{
					if (addressSpace.Address.Equals("--"))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000FDD58 File Offset: 0x000FBF58
		internal SecureString GetSmartHostPassword()
		{
			SecureString secureString = new SecureString();
			string text = (string)this[SmtpSendConnectorConfigSchema.EncryptedAuthenticationPassword];
			foreach (char c in text.ToCharArray())
			{
				secureString.AppendChar(c);
			}
			return secureString;
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000FDDA2 File Offset: 0x000FBFA2
		internal virtual RawSecurityDescriptor GetSecurityDescriptor()
		{
			if (this.securityDescriptor == null)
			{
				this.securityDescriptor = base.ReadSecurityDescriptor();
			}
			return this.securityDescriptor;
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x000FDDBE File Offset: 0x000FBFBE
		internal Permission GetAnonymousPermissions()
		{
			if (!this.anonymousPermissionsSet)
			{
				this.DetermineAnonymousPermissions();
			}
			return this.anonymousPermissions;
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000FDDD4 File Offset: 0x000FBFD4
		internal Permission GetPartnerPermissions()
		{
			if (!this.partnerPermissionsSet)
			{
				this.DeterminePartnerPermissions();
			}
			return this.partnerPermissions;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000FDDEC File Offset: 0x000FBFEC
		private static int GetFlagsValue(IPropertyBag propertyBag, ProviderPropertyDefinition property, int flags)
		{
			int num = (int)propertyBag[property];
			return num & flags;
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000FDE0C File Offset: 0x000FC00C
		private Permission DeterminePermissions(SecurityIdentifier sid)
		{
			Permission result = Permission.None;
			RawSecurityDescriptor rawSecurityDescriptor = this.GetSecurityDescriptor();
			try
			{
				if (rawSecurityDescriptor != null)
				{
					result = AuthzAuthorization.CheckPermissions(sid, rawSecurityDescriptor, null);
				}
			}
			catch (Win32Exception)
			{
			}
			return result;
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000FDE44 File Offset: 0x000FC044
		private void DetermineAnonymousPermissions()
		{
			this.anonymousPermissions = this.DeterminePermissions(SmtpSendConnectorConfig.AnonymousSecurityIdentifier);
			this.anonymousPermissionsSet = true;
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000FDE5E File Offset: 0x000FC05E
		private void DeterminePartnerPermissions()
		{
			this.partnerPermissions = this.DeterminePermissions(WellKnownSids.PartnerServers);
			this.partnerPermissionsSet = true;
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000FDE78 File Offset: 0x000FC078
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x000FDE8C File Offset: 0x000FC08C
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (MultiValuedPropertyBase.IsNullOrEmpty(base.ConnectedDomains) && MultiValuedPropertyBase.IsNullOrEmpty(base.AddressSpaces))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.NoAddressSpaces, MailGatewaySchema.AddressSpaces, this));
			}
			if (!this.DNSRoutingEnabled && MultiValuedPropertyBase.IsNullOrEmpty(this.SmartHosts))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.SmartHostNotSet, SmtpSendConnectorConfigSchema.SmartHosts, this));
			}
			IPvxAddress pvxAddress = new IPvxAddress(this.SourceIPAddress);
			if (pvxAddress.IsMulticast || pvxAddress.IsBroadcast)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InvalidSourceAddressSetting, SmtpSendConnectorConfigSchema.SourceIPAddress, this));
			}
			if ((this.SmartHostAuthMechanism == SmtpSendConnectorConfig.AuthMechanisms.BasicAuth || this.SmartHostAuthMechanism == SmtpSendConnectorConfig.AuthMechanisms.BasicAuthRequireTLS) && this.AuthenticationCredential == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.AuthenticationCredentialNotSet, SmtpSendConnectorConfigSchema.AuthenticationCredential, this));
			}
			if (this.DomainSecureEnabled)
			{
				if (!this.DNSRoutingEnabled)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.DomainSecureWithoutDNSRoutingEnabled, SmtpSendConnectorConfigSchema.DNSRoutingEnabled, this));
				}
				if (this.IgnoreSTARTTLS)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.DomainSecureWithIgnoreStartTLSEnabled, SmtpSendConnectorConfigSchema.IgnoreSTARTTLS, this));
				}
			}
			if (this.TlsAuthLevel != null)
			{
				if (!this.RequireTLS)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.TlsAuthLevelWithRequireTlsDisabled, SmtpSendConnectorConfigSchema.TlsAuthLevel, this));
				}
				if (this.DomainSecureEnabled)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.TlsAuthLevelWithDomainSecureEnabled, SmtpSendConnectorConfigSchema.TlsAuthLevel, this));
				}
			}
			if (this.TlsDomain != null)
			{
				if (this.TlsAuthLevel == null || this.TlsAuthLevel.Value != Microsoft.Exchange.Data.TlsAuthLevel.DomainValidation)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.TlsDomainWithIncorrectTlsAuthLevel, SmtpSendConnectorConfigSchema.TlsDomain, this));
					return;
				}
			}
			else if (!this.DNSRoutingEnabled && this.TlsAuthLevel != null && this.TlsAuthLevel.Value == Microsoft.Exchange.Data.TlsAuthLevel.DomainValidation)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.TlsAuthLevelWithNoDomainOnSmartHost, SmtpSendConnectorConfigSchema.TlsDomain, this));
			}
		}

		// Token: 0x04002DE0 RID: 11744
		public new static string MostDerivedClass = "msExchRoutingSMTPConnector";

		// Token: 0x04002DE1 RID: 11745
		private static SmtpSendConnectorConfigSchema schema = ObjectSchema.GetInstance<SmtpSendConnectorConfigSchema>();

		// Token: 0x04002DE2 RID: 11746
		protected static readonly SecurityIdentifier AnonymousSecurityIdentifier = new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);

		// Token: 0x04002DE3 RID: 11747
		[NonSerialized]
		private RawSecurityDescriptor securityDescriptor;

		// Token: 0x04002DE4 RID: 11748
		[NonSerialized]
		private Permission anonymousPermissions;

		// Token: 0x04002DE5 RID: 11749
		private bool anonymousPermissionsSet;

		// Token: 0x04002DE6 RID: 11750
		[NonSerialized]
		private Permission partnerPermissions;

		// Token: 0x04002DE7 RID: 11751
		private bool partnerPermissionsSet;

		// Token: 0x04002DE8 RID: 11752
		private string certificateSubject;

		// Token: 0x020005BD RID: 1469
		public enum AuthMechanisms
		{
			// Token: 0x04002DEC RID: 11756
			[LocDescription(DirectoryStrings.IDs.SendAuthMechanismNone)]
			None,
			// Token: 0x04002DED RID: 11757
			[LocDescription(DirectoryStrings.IDs.SendAuthMechanismBasicAuth)]
			BasicAuth = 2,
			// Token: 0x04002DEE RID: 11758
			[LocDescription(DirectoryStrings.IDs.SendAuthMechanismBasicAuthPlusTls)]
			BasicAuthRequireTLS = 4,
			// Token: 0x04002DEF RID: 11759
			[LocDescription(DirectoryStrings.IDs.SendAuthMechanismExchangeServer)]
			ExchangeServer = 8,
			// Token: 0x04002DF0 RID: 11760
			[LocDescription(DirectoryStrings.IDs.SendAuthMechanismExternalAuthoritative)]
			ExternalAuthoritative = 16
		}
	}
}
