using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000307 RID: 775
	public class SetPopSettingsCommand : SyntheticCommandWithPipelineInputNoOutput<Pop3AdConfiguration>
	{
		// Token: 0x06003385 RID: 13189 RVA: 0x0005AAE8 File Offset: 0x00058CE8
		private SetPopSettingsCommand() : base("Set-PopSettings")
		{
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x0005AAF5 File Offset: 0x00058CF5
		public SetPopSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x0005AB04 File Offset: 0x00058D04
		public virtual SetPopSettingsCommand SetParameters(SetPopSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000308 RID: 776
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001930 RID: 6448
			// (set) Token: 0x06003388 RID: 13192 RVA: 0x0005AB0E File Offset: 0x00058D0E
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17001931 RID: 6449
			// (set) Token: 0x06003389 RID: 13193 RVA: 0x0005AB21 File Offset: 0x00058D21
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001932 RID: 6450
			// (set) Token: 0x0600338A RID: 13194 RVA: 0x0005AB34 File Offset: 0x00058D34
			public virtual int MaxCommandSize
			{
				set
				{
					base.PowerSharpParameters["MaxCommandSize"] = value;
				}
			}

			// Token: 0x17001933 RID: 6451
			// (set) Token: 0x0600338B RID: 13195 RVA: 0x0005AB4C File Offset: 0x00058D4C
			public virtual SortOrder MessageRetrievalSortOrder
			{
				set
				{
					base.PowerSharpParameters["MessageRetrievalSortOrder"] = value;
				}
			}

			// Token: 0x17001934 RID: 6452
			// (set) Token: 0x0600338C RID: 13196 RVA: 0x0005AB64 File Offset: 0x00058D64
			public virtual MultiValuedProperty<IPBinding> UnencryptedOrTLSBindings
			{
				set
				{
					base.PowerSharpParameters["UnencryptedOrTLSBindings"] = value;
				}
			}

			// Token: 0x17001935 RID: 6453
			// (set) Token: 0x0600338D RID: 13197 RVA: 0x0005AB77 File Offset: 0x00058D77
			public virtual MultiValuedProperty<IPBinding> SSLBindings
			{
				set
				{
					base.PowerSharpParameters["SSLBindings"] = value;
				}
			}

			// Token: 0x17001936 RID: 6454
			// (set) Token: 0x0600338E RID: 13198 RVA: 0x0005AB8A File Offset: 0x00058D8A
			public virtual MultiValuedProperty<ProtocolConnectionSettings> InternalConnectionSettings
			{
				set
				{
					base.PowerSharpParameters["InternalConnectionSettings"] = value;
				}
			}

			// Token: 0x17001937 RID: 6455
			// (set) Token: 0x0600338F RID: 13199 RVA: 0x0005AB9D File Offset: 0x00058D9D
			public virtual MultiValuedProperty<ProtocolConnectionSettings> ExternalConnectionSettings
			{
				set
				{
					base.PowerSharpParameters["ExternalConnectionSettings"] = value;
				}
			}

			// Token: 0x17001938 RID: 6456
			// (set) Token: 0x06003390 RID: 13200 RVA: 0x0005ABB0 File Offset: 0x00058DB0
			public virtual string X509CertificateName
			{
				set
				{
					base.PowerSharpParameters["X509CertificateName"] = value;
				}
			}

			// Token: 0x17001939 RID: 6457
			// (set) Token: 0x06003391 RID: 13201 RVA: 0x0005ABC3 File Offset: 0x00058DC3
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x1700193A RID: 6458
			// (set) Token: 0x06003392 RID: 13202 RVA: 0x0005ABD6 File Offset: 0x00058DD6
			public virtual LoginOptions LoginType
			{
				set
				{
					base.PowerSharpParameters["LoginType"] = value;
				}
			}

			// Token: 0x1700193B RID: 6459
			// (set) Token: 0x06003393 RID: 13203 RVA: 0x0005ABEE File Offset: 0x00058DEE
			public virtual EnhancedTimeSpan AuthenticatedConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["AuthenticatedConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700193C RID: 6460
			// (set) Token: 0x06003394 RID: 13204 RVA: 0x0005AC06 File Offset: 0x00058E06
			public virtual EnhancedTimeSpan PreAuthenticatedConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["PreAuthenticatedConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700193D RID: 6461
			// (set) Token: 0x06003395 RID: 13205 RVA: 0x0005AC1E File Offset: 0x00058E1E
			public virtual int MaxConnections
			{
				set
				{
					base.PowerSharpParameters["MaxConnections"] = value;
				}
			}

			// Token: 0x1700193E RID: 6462
			// (set) Token: 0x06003396 RID: 13206 RVA: 0x0005AC36 File Offset: 0x00058E36
			public virtual int MaxConnectionFromSingleIP
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionFromSingleIP"] = value;
				}
			}

			// Token: 0x1700193F RID: 6463
			// (set) Token: 0x06003397 RID: 13207 RVA: 0x0005AC4E File Offset: 0x00058E4E
			public virtual int MaxConnectionsPerUser
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionsPerUser"] = value;
				}
			}

			// Token: 0x17001940 RID: 6464
			// (set) Token: 0x06003398 RID: 13208 RVA: 0x0005AC66 File Offset: 0x00058E66
			public virtual MimeTextFormat MessageRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["MessageRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x17001941 RID: 6465
			// (set) Token: 0x06003399 RID: 13209 RVA: 0x0005AC7E File Offset: 0x00058E7E
			public virtual int ProxyTargetPort
			{
				set
				{
					base.PowerSharpParameters["ProxyTargetPort"] = value;
				}
			}

			// Token: 0x17001942 RID: 6466
			// (set) Token: 0x0600339A RID: 13210 RVA: 0x0005AC96 File Offset: 0x00058E96
			public virtual CalendarItemRetrievalOptions CalendarItemRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["CalendarItemRetrievalOption"] = value;
				}
			}

			// Token: 0x17001943 RID: 6467
			// (set) Token: 0x0600339B RID: 13211 RVA: 0x0005ACAE File Offset: 0x00058EAE
			public virtual Uri OwaServerUrl
			{
				set
				{
					base.PowerSharpParameters["OwaServerUrl"] = value;
				}
			}

			// Token: 0x17001944 RID: 6468
			// (set) Token: 0x0600339C RID: 13212 RVA: 0x0005ACC1 File Offset: 0x00058EC1
			public virtual bool EnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["EnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x17001945 RID: 6469
			// (set) Token: 0x0600339D RID: 13213 RVA: 0x0005ACD9 File Offset: 0x00058ED9
			public virtual bool LiveIdBasicAuthReplacement
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthReplacement"] = value;
				}
			}

			// Token: 0x17001946 RID: 6470
			// (set) Token: 0x0600339E RID: 13214 RVA: 0x0005ACF1 File Offset: 0x00058EF1
			public virtual bool SuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["SuppressReadReceipt"] = value;
				}
			}

			// Token: 0x17001947 RID: 6471
			// (set) Token: 0x0600339F RID: 13215 RVA: 0x0005AD09 File Offset: 0x00058F09
			public virtual bool ProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x17001948 RID: 6472
			// (set) Token: 0x060033A0 RID: 13216 RVA: 0x0005AD21 File Offset: 0x00058F21
			public virtual bool EnforceCertificateErrors
			{
				set
				{
					base.PowerSharpParameters["EnforceCertificateErrors"] = value;
				}
			}

			// Token: 0x17001949 RID: 6473
			// (set) Token: 0x060033A1 RID: 13217 RVA: 0x0005AD39 File Offset: 0x00058F39
			public virtual string LogFileLocation
			{
				set
				{
					base.PowerSharpParameters["LogFileLocation"] = value;
				}
			}

			// Token: 0x1700194A RID: 6474
			// (set) Token: 0x060033A2 RID: 13218 RVA: 0x0005AD4C File Offset: 0x00058F4C
			public virtual LogFileRollOver LogFileRollOverSettings
			{
				set
				{
					base.PowerSharpParameters["LogFileRollOverSettings"] = value;
				}
			}

			// Token: 0x1700194B RID: 6475
			// (set) Token: 0x060033A3 RID: 13219 RVA: 0x0005AD64 File Offset: 0x00058F64
			public virtual Unlimited<ByteQuantifiedSize> LogPerFileSizeQuota
			{
				set
				{
					base.PowerSharpParameters["LogPerFileSizeQuota"] = value;
				}
			}

			// Token: 0x1700194C RID: 6476
			// (set) Token: 0x060033A4 RID: 13220 RVA: 0x0005AD7C File Offset: 0x00058F7C
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x1700194D RID: 6477
			// (set) Token: 0x060033A5 RID: 13221 RVA: 0x0005AD94 File Offset: 0x00058F94
			public virtual bool EnableGSSAPIAndNTLMAuth
			{
				set
				{
					base.PowerSharpParameters["EnableGSSAPIAndNTLMAuth"] = value;
				}
			}

			// Token: 0x1700194E RID: 6478
			// (set) Token: 0x060033A6 RID: 13222 RVA: 0x0005ADAC File Offset: 0x00058FAC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700194F RID: 6479
			// (set) Token: 0x060033A7 RID: 13223 RVA: 0x0005ADC4 File Offset: 0x00058FC4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001950 RID: 6480
			// (set) Token: 0x060033A8 RID: 13224 RVA: 0x0005ADDC File Offset: 0x00058FDC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001951 RID: 6481
			// (set) Token: 0x060033A9 RID: 13225 RVA: 0x0005ADF4 File Offset: 0x00058FF4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001952 RID: 6482
			// (set) Token: 0x060033AA RID: 13226 RVA: 0x0005AE0C File Offset: 0x0005900C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
