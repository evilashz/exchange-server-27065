using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000301 RID: 769
	public class SetImapSettingsCommand : SyntheticCommandWithPipelineInputNoOutput<Imap4AdConfiguration>
	{
		// Token: 0x0600334A RID: 13130 RVA: 0x0005A63C File Offset: 0x0005883C
		private SetImapSettingsCommand() : base("Set-ImapSettings")
		{
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0005A649 File Offset: 0x00058849
		public SetImapSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x0005A658 File Offset: 0x00058858
		public virtual SetImapSettingsCommand SetParameters(SetImapSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000302 RID: 770
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001901 RID: 6401
			// (set) Token: 0x0600334D RID: 13133 RVA: 0x0005A662 File Offset: 0x00058862
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17001902 RID: 6402
			// (set) Token: 0x0600334E RID: 13134 RVA: 0x0005A675 File Offset: 0x00058875
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001903 RID: 6403
			// (set) Token: 0x0600334F RID: 13135 RVA: 0x0005A688 File Offset: 0x00058888
			public virtual int MaxCommandSize
			{
				set
				{
					base.PowerSharpParameters["MaxCommandSize"] = value;
				}
			}

			// Token: 0x17001904 RID: 6404
			// (set) Token: 0x06003350 RID: 13136 RVA: 0x0005A6A0 File Offset: 0x000588A0
			public virtual bool ShowHiddenFoldersEnabled
			{
				set
				{
					base.PowerSharpParameters["ShowHiddenFoldersEnabled"] = value;
				}
			}

			// Token: 0x17001905 RID: 6405
			// (set) Token: 0x06003351 RID: 13137 RVA: 0x0005A6B8 File Offset: 0x000588B8
			public virtual MultiValuedProperty<IPBinding> UnencryptedOrTLSBindings
			{
				set
				{
					base.PowerSharpParameters["UnencryptedOrTLSBindings"] = value;
				}
			}

			// Token: 0x17001906 RID: 6406
			// (set) Token: 0x06003352 RID: 13138 RVA: 0x0005A6CB File Offset: 0x000588CB
			public virtual MultiValuedProperty<IPBinding> SSLBindings
			{
				set
				{
					base.PowerSharpParameters["SSLBindings"] = value;
				}
			}

			// Token: 0x17001907 RID: 6407
			// (set) Token: 0x06003353 RID: 13139 RVA: 0x0005A6DE File Offset: 0x000588DE
			public virtual MultiValuedProperty<ProtocolConnectionSettings> InternalConnectionSettings
			{
				set
				{
					base.PowerSharpParameters["InternalConnectionSettings"] = value;
				}
			}

			// Token: 0x17001908 RID: 6408
			// (set) Token: 0x06003354 RID: 13140 RVA: 0x0005A6F1 File Offset: 0x000588F1
			public virtual MultiValuedProperty<ProtocolConnectionSettings> ExternalConnectionSettings
			{
				set
				{
					base.PowerSharpParameters["ExternalConnectionSettings"] = value;
				}
			}

			// Token: 0x17001909 RID: 6409
			// (set) Token: 0x06003355 RID: 13141 RVA: 0x0005A704 File Offset: 0x00058904
			public virtual string X509CertificateName
			{
				set
				{
					base.PowerSharpParameters["X509CertificateName"] = value;
				}
			}

			// Token: 0x1700190A RID: 6410
			// (set) Token: 0x06003356 RID: 13142 RVA: 0x0005A717 File Offset: 0x00058917
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x1700190B RID: 6411
			// (set) Token: 0x06003357 RID: 13143 RVA: 0x0005A72A File Offset: 0x0005892A
			public virtual LoginOptions LoginType
			{
				set
				{
					base.PowerSharpParameters["LoginType"] = value;
				}
			}

			// Token: 0x1700190C RID: 6412
			// (set) Token: 0x06003358 RID: 13144 RVA: 0x0005A742 File Offset: 0x00058942
			public virtual EnhancedTimeSpan AuthenticatedConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["AuthenticatedConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700190D RID: 6413
			// (set) Token: 0x06003359 RID: 13145 RVA: 0x0005A75A File Offset: 0x0005895A
			public virtual EnhancedTimeSpan PreAuthenticatedConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["PreAuthenticatedConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700190E RID: 6414
			// (set) Token: 0x0600335A RID: 13146 RVA: 0x0005A772 File Offset: 0x00058972
			public virtual int MaxConnections
			{
				set
				{
					base.PowerSharpParameters["MaxConnections"] = value;
				}
			}

			// Token: 0x1700190F RID: 6415
			// (set) Token: 0x0600335B RID: 13147 RVA: 0x0005A78A File Offset: 0x0005898A
			public virtual int MaxConnectionFromSingleIP
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionFromSingleIP"] = value;
				}
			}

			// Token: 0x17001910 RID: 6416
			// (set) Token: 0x0600335C RID: 13148 RVA: 0x0005A7A2 File Offset: 0x000589A2
			public virtual int MaxConnectionsPerUser
			{
				set
				{
					base.PowerSharpParameters["MaxConnectionsPerUser"] = value;
				}
			}

			// Token: 0x17001911 RID: 6417
			// (set) Token: 0x0600335D RID: 13149 RVA: 0x0005A7BA File Offset: 0x000589BA
			public virtual MimeTextFormat MessageRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["MessageRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x17001912 RID: 6418
			// (set) Token: 0x0600335E RID: 13150 RVA: 0x0005A7D2 File Offset: 0x000589D2
			public virtual int ProxyTargetPort
			{
				set
				{
					base.PowerSharpParameters["ProxyTargetPort"] = value;
				}
			}

			// Token: 0x17001913 RID: 6419
			// (set) Token: 0x0600335F RID: 13151 RVA: 0x0005A7EA File Offset: 0x000589EA
			public virtual CalendarItemRetrievalOptions CalendarItemRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["CalendarItemRetrievalOption"] = value;
				}
			}

			// Token: 0x17001914 RID: 6420
			// (set) Token: 0x06003360 RID: 13152 RVA: 0x0005A802 File Offset: 0x00058A02
			public virtual Uri OwaServerUrl
			{
				set
				{
					base.PowerSharpParameters["OwaServerUrl"] = value;
				}
			}

			// Token: 0x17001915 RID: 6421
			// (set) Token: 0x06003361 RID: 13153 RVA: 0x0005A815 File Offset: 0x00058A15
			public virtual bool EnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["EnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x17001916 RID: 6422
			// (set) Token: 0x06003362 RID: 13154 RVA: 0x0005A82D File Offset: 0x00058A2D
			public virtual bool LiveIdBasicAuthReplacement
			{
				set
				{
					base.PowerSharpParameters["LiveIdBasicAuthReplacement"] = value;
				}
			}

			// Token: 0x17001917 RID: 6423
			// (set) Token: 0x06003363 RID: 13155 RVA: 0x0005A845 File Offset: 0x00058A45
			public virtual bool SuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["SuppressReadReceipt"] = value;
				}
			}

			// Token: 0x17001918 RID: 6424
			// (set) Token: 0x06003364 RID: 13156 RVA: 0x0005A85D File Offset: 0x00058A5D
			public virtual bool ProtocolLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ProtocolLogEnabled"] = value;
				}
			}

			// Token: 0x17001919 RID: 6425
			// (set) Token: 0x06003365 RID: 13157 RVA: 0x0005A875 File Offset: 0x00058A75
			public virtual bool EnforceCertificateErrors
			{
				set
				{
					base.PowerSharpParameters["EnforceCertificateErrors"] = value;
				}
			}

			// Token: 0x1700191A RID: 6426
			// (set) Token: 0x06003366 RID: 13158 RVA: 0x0005A88D File Offset: 0x00058A8D
			public virtual string LogFileLocation
			{
				set
				{
					base.PowerSharpParameters["LogFileLocation"] = value;
				}
			}

			// Token: 0x1700191B RID: 6427
			// (set) Token: 0x06003367 RID: 13159 RVA: 0x0005A8A0 File Offset: 0x00058AA0
			public virtual LogFileRollOver LogFileRollOverSettings
			{
				set
				{
					base.PowerSharpParameters["LogFileRollOverSettings"] = value;
				}
			}

			// Token: 0x1700191C RID: 6428
			// (set) Token: 0x06003368 RID: 13160 RVA: 0x0005A8B8 File Offset: 0x00058AB8
			public virtual Unlimited<ByteQuantifiedSize> LogPerFileSizeQuota
			{
				set
				{
					base.PowerSharpParameters["LogPerFileSizeQuota"] = value;
				}
			}

			// Token: 0x1700191D RID: 6429
			// (set) Token: 0x06003369 RID: 13161 RVA: 0x0005A8D0 File Offset: 0x00058AD0
			public virtual ExtendedProtectionTokenCheckingMode ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x1700191E RID: 6430
			// (set) Token: 0x0600336A RID: 13162 RVA: 0x0005A8E8 File Offset: 0x00058AE8
			public virtual bool EnableGSSAPIAndNTLMAuth
			{
				set
				{
					base.PowerSharpParameters["EnableGSSAPIAndNTLMAuth"] = value;
				}
			}

			// Token: 0x1700191F RID: 6431
			// (set) Token: 0x0600336B RID: 13163 RVA: 0x0005A900 File Offset: 0x00058B00
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001920 RID: 6432
			// (set) Token: 0x0600336C RID: 13164 RVA: 0x0005A918 File Offset: 0x00058B18
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001921 RID: 6433
			// (set) Token: 0x0600336D RID: 13165 RVA: 0x0005A930 File Offset: 0x00058B30
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001922 RID: 6434
			// (set) Token: 0x0600336E RID: 13166 RVA: 0x0005A948 File Offset: 0x00058B48
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001923 RID: 6435
			// (set) Token: 0x0600336F RID: 13167 RVA: 0x0005A960 File Offset: 0x00058B60
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
