using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000618 RID: 1560
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public abstract class PopImapAdConfiguration : ADEmailTransport
	{
		// Token: 0x060049DD RID: 18909 RVA: 0x00111CCB File Offset: 0x0010FECB
		public PopImapAdConfiguration()
		{
		}

		// Token: 0x17001877 RID: 6263
		// (get) Token: 0x060049DE RID: 18910
		public abstract string ProtocolName { get; }

		// Token: 0x17001878 RID: 6264
		// (get) Token: 0x060049DF RID: 18911 RVA: 0x00111CD3 File Offset: 0x0010FED3
		// (set) Token: 0x060049E0 RID: 18912 RVA: 0x00111CE5 File Offset: 0x0010FEE5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPBinding> UnencryptedOrTLSBindings
		{
			get
			{
				return (MultiValuedProperty<IPBinding>)this[PopImapAdConfigurationSchema.UnencryptedOrTLSBindings];
			}
			set
			{
				this[PopImapAdConfigurationSchema.UnencryptedOrTLSBindings] = value;
			}
		}

		// Token: 0x17001879 RID: 6265
		// (get) Token: 0x060049E1 RID: 18913 RVA: 0x00111CF3 File Offset: 0x0010FEF3
		// (set) Token: 0x060049E2 RID: 18914 RVA: 0x00111D05 File Offset: 0x0010FF05
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPBinding> SSLBindings
		{
			get
			{
				return (MultiValuedProperty<IPBinding>)this[PopImapAdConfigurationSchema.SSLBindings];
			}
			set
			{
				this[PopImapAdConfigurationSchema.SSLBindings] = value;
			}
		}

		// Token: 0x1700187A RID: 6266
		// (get) Token: 0x060049E3 RID: 18915 RVA: 0x00111D13 File Offset: 0x0010FF13
		// (set) Token: 0x060049E4 RID: 18916 RVA: 0x00111D25 File Offset: 0x0010FF25
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ProtocolConnectionSettings> InternalConnectionSettings
		{
			get
			{
				return (MultiValuedProperty<ProtocolConnectionSettings>)this[PopImapAdConfigurationSchema.InternalConnectionSettings];
			}
			set
			{
				this[PopImapAdConfigurationSchema.InternalConnectionSettings] = value;
			}
		}

		// Token: 0x1700187B RID: 6267
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x00111D33 File Offset: 0x0010FF33
		// (set) Token: 0x060049E6 RID: 18918 RVA: 0x00111D45 File Offset: 0x0010FF45
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ProtocolConnectionSettings> ExternalConnectionSettings
		{
			get
			{
				return (MultiValuedProperty<ProtocolConnectionSettings>)this[PopImapAdConfigurationSchema.ExternalConnectionSettings];
			}
			set
			{
				this[PopImapAdConfigurationSchema.ExternalConnectionSettings] = value;
			}
		}

		// Token: 0x1700187C RID: 6268
		// (get) Token: 0x060049E7 RID: 18919 RVA: 0x00111D53 File Offset: 0x0010FF53
		// (set) Token: 0x060049E8 RID: 18920 RVA: 0x00111D65 File Offset: 0x0010FF65
		[Parameter(Mandatory = false)]
		public string X509CertificateName
		{
			get
			{
				return (string)this[PopImapAdConfigurationSchema.X509CertificateName];
			}
			set
			{
				this[PopImapAdConfigurationSchema.X509CertificateName] = value;
			}
		}

		// Token: 0x1700187D RID: 6269
		// (get) Token: 0x060049E9 RID: 18921 RVA: 0x00111D73 File Offset: 0x0010FF73
		// (set) Token: 0x060049EA RID: 18922 RVA: 0x00111D85 File Offset: 0x0010FF85
		[Parameter(Mandatory = false)]
		public string Banner
		{
			get
			{
				return (string)this[PopImapAdConfigurationSchema.Banner];
			}
			set
			{
				this[PopImapAdConfigurationSchema.Banner] = value;
			}
		}

		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x060049EB RID: 18923 RVA: 0x00111D93 File Offset: 0x0010FF93
		// (set) Token: 0x060049EC RID: 18924 RVA: 0x00111DA5 File Offset: 0x0010FFA5
		[Parameter(Mandatory = false)]
		public LoginOptions LoginType
		{
			get
			{
				return (LoginOptions)this[PopImapAdConfigurationSchema.LoginType];
			}
			set
			{
				this[PopImapAdConfigurationSchema.LoginType] = value;
			}
		}

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x060049ED RID: 18925 RVA: 0x00111DB8 File Offset: 0x0010FFB8
		// (set) Token: 0x060049EE RID: 18926 RVA: 0x00111DCA File Offset: 0x0010FFCA
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan AuthenticatedConnectionTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[PopImapAdConfigurationSchema.AuthenticatedConnectionTimeout];
			}
			set
			{
				this[PopImapAdConfigurationSchema.AuthenticatedConnectionTimeout] = value;
			}
		}

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x060049EF RID: 18927 RVA: 0x00111DDD File Offset: 0x0010FFDD
		// (set) Token: 0x060049F0 RID: 18928 RVA: 0x00111DEF File Offset: 0x0010FFEF
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan PreAuthenticatedConnectionTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[PopImapAdConfigurationSchema.PreAuthenticatedConnectionTimeout];
			}
			set
			{
				this[PopImapAdConfigurationSchema.PreAuthenticatedConnectionTimeout] = value;
			}
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x060049F1 RID: 18929 RVA: 0x00111E02 File Offset: 0x00110002
		// (set) Token: 0x060049F2 RID: 18930 RVA: 0x00111E14 File Offset: 0x00110014
		[Parameter(Mandatory = false)]
		public int MaxConnections
		{
			get
			{
				return (int)this[PopImapAdConfigurationSchema.MaxConnections];
			}
			set
			{
				this[PopImapAdConfigurationSchema.MaxConnections] = value;
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x060049F3 RID: 18931 RVA: 0x00111E27 File Offset: 0x00110027
		// (set) Token: 0x060049F4 RID: 18932 RVA: 0x00111E39 File Offset: 0x00110039
		[Parameter(Mandatory = false)]
		public int MaxConnectionFromSingleIP
		{
			get
			{
				return (int)this[PopImapAdConfigurationSchema.MaxConnectionFromSingleIP];
			}
			set
			{
				this[PopImapAdConfigurationSchema.MaxConnectionFromSingleIP] = value;
			}
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x060049F5 RID: 18933 RVA: 0x00111E4C File Offset: 0x0011004C
		// (set) Token: 0x060049F6 RID: 18934 RVA: 0x00111E5E File Offset: 0x0011005E
		[Parameter(Mandatory = false)]
		public int MaxConnectionsPerUser
		{
			get
			{
				return (int)this[PopImapAdConfigurationSchema.MaxConnectionsPerUser];
			}
			set
			{
				this[PopImapAdConfigurationSchema.MaxConnectionsPerUser] = value;
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x00111E71 File Offset: 0x00110071
		// (set) Token: 0x060049F8 RID: 18936 RVA: 0x00111E83 File Offset: 0x00110083
		[Parameter(Mandatory = false)]
		public MimeTextFormat MessageRetrievalMimeFormat
		{
			get
			{
				return (MimeTextFormat)this[PopImapAdConfigurationSchema.MessageRetrievalMimeFormat];
			}
			set
			{
				this[PopImapAdConfigurationSchema.MessageRetrievalMimeFormat] = value;
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x060049F9 RID: 18937 RVA: 0x00111E96 File Offset: 0x00110096
		// (set) Token: 0x060049FA RID: 18938 RVA: 0x00111EA8 File Offset: 0x001100A8
		[Parameter(Mandatory = false)]
		public int ProxyTargetPort
		{
			get
			{
				return (int)this[PopImapAdConfigurationSchema.ProxyTargetPort];
			}
			set
			{
				this[PopImapAdConfigurationSchema.ProxyTargetPort] = value;
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x00111EBB File Offset: 0x001100BB
		// (set) Token: 0x060049FC RID: 18940 RVA: 0x00111ECD File Offset: 0x001100CD
		[Parameter(Mandatory = false)]
		public CalendarItemRetrievalOptions CalendarItemRetrievalOption
		{
			get
			{
				return (CalendarItemRetrievalOptions)this[PopImapAdConfigurationSchema.CalendarItemRetrievalOption];
			}
			set
			{
				this[PopImapAdConfigurationSchema.CalendarItemRetrievalOption] = value;
			}
		}

		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x060049FD RID: 18941 RVA: 0x00111EE0 File Offset: 0x001100E0
		// (set) Token: 0x060049FE RID: 18942 RVA: 0x00111EF2 File Offset: 0x001100F2
		[Parameter(Mandatory = false)]
		public Uri OwaServerUrl
		{
			get
			{
				return (Uri)this[PopImapAdConfigurationSchema.OwaServerUrl];
			}
			set
			{
				this[PopImapAdConfigurationSchema.OwaServerUrl] = value;
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x060049FF RID: 18943 RVA: 0x00111F00 File Offset: 0x00110100
		// (set) Token: 0x06004A00 RID: 18944 RVA: 0x00111F12 File Offset: 0x00110112
		[Parameter(Mandatory = false)]
		public bool EnableExactRFC822Size
		{
			get
			{
				return (bool)this[PopImapAdConfigurationSchema.EnableExactRFC822Size];
			}
			set
			{
				this[PopImapAdConfigurationSchema.EnableExactRFC822Size] = value;
			}
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06004A01 RID: 18945 RVA: 0x00111F25 File Offset: 0x00110125
		// (set) Token: 0x06004A02 RID: 18946 RVA: 0x00111F37 File Offset: 0x00110137
		[Parameter(Mandatory = false)]
		public bool LiveIdBasicAuthReplacement
		{
			get
			{
				return (bool)this[PopImapAdConfigurationSchema.LiveIdBasicAuthReplacement];
			}
			set
			{
				this[PopImapAdConfigurationSchema.LiveIdBasicAuthReplacement] = value;
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06004A03 RID: 18947 RVA: 0x00111F4A File Offset: 0x0011014A
		// (set) Token: 0x06004A04 RID: 18948 RVA: 0x00111F5C File Offset: 0x0011015C
		[Parameter(Mandatory = false)]
		public bool SuppressReadReceipt
		{
			get
			{
				return (bool)this[PopImapAdConfigurationSchema.SuppressReadReceipt];
			}
			set
			{
				this[PopImapAdConfigurationSchema.SuppressReadReceipt] = value;
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x06004A05 RID: 18949
		// (set) Token: 0x06004A06 RID: 18950
		public abstract int MaxCommandSize { get; set; }

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x00111F6F File Offset: 0x0011016F
		// (set) Token: 0x06004A08 RID: 18952 RVA: 0x00111F81 File Offset: 0x00110181
		[Parameter(Mandatory = false)]
		public bool ProtocolLogEnabled
		{
			get
			{
				return (bool)this[PopImapAdConfigurationSchema.ProtocolLogEnabled];
			}
			set
			{
				this[PopImapAdConfigurationSchema.ProtocolLogEnabled] = value;
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x06004A09 RID: 18953 RVA: 0x00111F94 File Offset: 0x00110194
		// (set) Token: 0x06004A0A RID: 18954 RVA: 0x00111FA6 File Offset: 0x001101A6
		[Parameter(Mandatory = false)]
		public bool EnforceCertificateErrors
		{
			get
			{
				return (bool)this[PopImapAdConfigurationSchema.EnforceCertificateErrors];
			}
			set
			{
				this[PopImapAdConfigurationSchema.EnforceCertificateErrors] = value;
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x00111FB9 File Offset: 0x001101B9
		// (set) Token: 0x06004A0C RID: 18956 RVA: 0x00111FCB File Offset: 0x001101CB
		[Parameter(Mandatory = false)]
		public string LogFileLocation
		{
			get
			{
				return (string)this[PopImapAdConfigurationSchema.LogFileLocation];
			}
			set
			{
				this[PopImapAdConfigurationSchema.LogFileLocation] = value;
			}
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x00111FD9 File Offset: 0x001101D9
		// (set) Token: 0x06004A0E RID: 18958 RVA: 0x00111FEB File Offset: 0x001101EB
		[Parameter(Mandatory = false)]
		public LogFileRollOver LogFileRollOverSettings
		{
			get
			{
				return (LogFileRollOver)this[PopImapAdConfigurationSchema.LogFileRollOverSettings];
			}
			set
			{
				this[PopImapAdConfigurationSchema.LogFileRollOverSettings] = value;
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06004A0F RID: 18959 RVA: 0x00111FFE File Offset: 0x001101FE
		// (set) Token: 0x06004A10 RID: 18960 RVA: 0x00112010 File Offset: 0x00110210
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> LogPerFileSizeQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[PopImapAdConfigurationSchema.LogPerFileSizeQuota];
			}
			set
			{
				this[PopImapAdConfigurationSchema.LogPerFileSizeQuota] = value;
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06004A11 RID: 18961 RVA: 0x00112023 File Offset: 0x00110223
		// (set) Token: 0x06004A12 RID: 18962 RVA: 0x00112035 File Offset: 0x00110235
		[Parameter(Mandatory = false)]
		public ExtendedProtectionTokenCheckingMode ExtendedProtectionPolicy
		{
			get
			{
				return (ExtendedProtectionTokenCheckingMode)this[PopImapAdConfigurationSchema.ExtendedProtectionPolicy];
			}
			set
			{
				this[PopImapAdConfigurationSchema.ExtendedProtectionPolicy] = value;
			}
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x00112048 File Offset: 0x00110248
		// (set) Token: 0x06004A14 RID: 18964 RVA: 0x0011205D File Offset: 0x0011025D
		[Parameter(Mandatory = false)]
		public bool EnableGSSAPIAndNTLMAuth
		{
			get
			{
				return !(bool)this[PopImapAdConfigurationSchema.EnableGSSAPIAndNTLMAuth];
			}
			set
			{
				this[PopImapAdConfigurationSchema.EnableGSSAPIAndNTLMAuth] = !value;
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06004A15 RID: 18965 RVA: 0x00112073 File Offset: 0x00110273
		internal static ExchangeObjectVersion MinimumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x0011207A File Offset: 0x0011027A
		internal static ObjectId GetRootId(Server server, string protocolName)
		{
			return server.Id.GetChildId("Protocols").GetChildId(protocolName);
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x00112094 File Offset: 0x00110294
		internal static TResult FindOne<TResult>(ITopologyConfigurationSession session) where TResult : PopImapAdConfiguration, new()
		{
			Server server = session.FindLocalServer();
			return PopImapAdConfiguration.FindOne<TResult>(session, server);
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x001120B0 File Offset: 0x001102B0
		internal static TResult FindOne<TResult>(ITopologyConfigurationSession session, string serverFqdn) where TResult : PopImapAdConfiguration, new()
		{
			Server server = session.FindServerByFqdn(serverFqdn);
			return PopImapAdConfiguration.FindOne<TResult>(session, server);
		}

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06004A19 RID: 18969 RVA: 0x001120CC File Offset: 0x001102CC
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x001120D4 File Offset: 0x001102D4
		internal List<IPBinding> GetBindings()
		{
			List<IPBinding> list = new List<IPBinding>(this.UnencryptedOrTLSBindings.Count + this.SSLBindings.Count);
			list.AddRange(this.UnencryptedOrTLSBindings);
			list.AddRange(this.SSLBindings);
			return list;
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x00112118 File Offset: 0x00110318
		internal string DisplayString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
			{
				object obj = this[propertyDefinition];
				if (obj is MultiValuedProperty<IPBinding>)
				{
					stringBuilder.AppendFormat("{0}: {{", propertyDefinition.Name);
					foreach (IPBinding arg in ((MultiValuedProperty<IPBinding>)obj))
					{
						stringBuilder.AppendFormat("{0}, ", arg);
					}
					stringBuilder.Append("}\r\n");
				}
				else if (obj is MultiValuedProperty<ProtocolConnectionSettings>)
				{
					stringBuilder.AppendFormat("{0}: {{", propertyDefinition.Name);
					foreach (ProtocolConnectionSettings arg2 in ((MultiValuedProperty<ProtocolConnectionSettings>)obj))
					{
						stringBuilder.AppendFormat("{0}, ", arg2);
					}
					stringBuilder.Append("}\r\n");
				}
				else
				{
					stringBuilder.AppendFormat("{0}: {1}\r\n", propertyDefinition.Name, obj);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x001122A0 File Offset: 0x001104A0
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.CalendarItemRetrievalOption == CalendarItemRetrievalOptions.Custom && this.OwaServerUrl == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExArgumentNullException(PopImapAdConfigurationSchema.OwaServerUrl.Name), PopImapAdConfigurationSchema.OwaServerUrl, this.OwaServerUrl));
			}
			if (!string.IsNullOrEmpty(this.X509CertificateName) && !PopImapAdConfiguration.IsValidProtocolCertificate(this.X509CertificateName))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InvalidCertificateName(this.X509CertificateName), PopImapAdConfigurationSchema.X509CertificateName, this.X509CertificateName));
			}
			if (!this.LogPerFileSizeQuota.IsUnlimited && this.LogPerFileSizeQuota.Value > ByteQuantifiedSize.Zero && this.LogPerFileSizeQuota.Value < ByteQuantifiedSize.FromMB(1UL))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExArgumentOutOfRangeException(PopImapAdConfigurationSchema.LogPerFileSizeQuota.Name, this.LogPerFileSizeQuota), PopImapAdConfigurationSchema.LogPerFileSizeQuota, this.LogPerFileSizeQuota));
			}
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x001123A6 File Offset: 0x001105A6
		private static bool IsValidProtocolCertificate(string certificateName)
		{
			return Dns.IsValidName(certificateName);
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x001123B0 File Offset: 0x001105B0
		private static TResult FindOne<TResult>(ITopologyConfigurationSession session, Server server) where TResult : PopImapAdConfiguration, new()
		{
			TResult tresult = Activator.CreateInstance<TResult>();
			string protocolName = tresult.ProtocolName;
			ObjectId rootId = PopImapAdConfiguration.GetRootId(server, protocolName);
			if (rootId == null)
			{
				return default(TResult);
			}
			TResult[] array = session.Find<TResult>(rootId as ADObjectId, QueryScope.OneLevel, null, null, 2);
			if (array == null || array.Length <= 0)
			{
				return default(TResult);
			}
			return array[0];
		}

		// Token: 0x02000619 RID: 1561
		internal enum PopImapFlag
		{
			// Token: 0x0400334E RID: 13134
			MessageRetrievalSortOrder,
			// Token: 0x0400334F RID: 13135
			ShowHiddenFoldersEnabled = 0,
			// Token: 0x04003350 RID: 13136
			EnableExactRFC822Size,
			// Token: 0x04003351 RID: 13137
			LiveIdBasicAuthReplacement,
			// Token: 0x04003352 RID: 13138
			SuppressReadReceipt = 4,
			// Token: 0x04003353 RID: 13139
			EnableGSSAPIAndNTLMAuth = 8
		}
	}
}
