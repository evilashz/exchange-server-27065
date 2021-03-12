using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000669 RID: 1641
	public class SetMailboxTransportServiceCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxTransportServerPresentationObject>
	{
		// Token: 0x06005568 RID: 21864 RVA: 0x0008649A File Offset: 0x0008469A
		private SetMailboxTransportServiceCommand() : base("Set-MailboxTransportService")
		{
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x000864A7 File Offset: 0x000846A7
		public SetMailboxTransportServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600556A RID: 21866 RVA: 0x000864B6 File Offset: 0x000846B6
		public virtual SetMailboxTransportServiceCommand SetParameters(SetMailboxTransportServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x000864C0 File Offset: 0x000846C0
		public virtual SetMailboxTransportServiceCommand SetParameters(SetMailboxTransportServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200066A RID: 1642
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700344F RID: 13391
			// (set) Token: 0x0600556C RID: 21868 RVA: 0x000864CA File Offset: 0x000846CA
			public virtual EnhancedTimeSpan MailboxSubmissionAgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogMaxAge"] = value;
				}
			}

			// Token: 0x17003450 RID: 13392
			// (set) Token: 0x0600556D RID: 21869 RVA: 0x000864E2 File Offset: 0x000846E2
			public virtual Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003451 RID: 13393
			// (set) Token: 0x0600556E RID: 21870 RVA: 0x000864FA File Offset: 0x000846FA
			public virtual Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003452 RID: 13394
			// (set) Token: 0x0600556F RID: 21871 RVA: 0x00086512 File Offset: 0x00084712
			public virtual LocalLongFullPath MailboxSubmissionAgentLogPath
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogPath"] = value;
				}
			}

			// Token: 0x17003453 RID: 13395
			// (set) Token: 0x06005570 RID: 21872 RVA: 0x00086525 File Offset: 0x00084725
			public virtual bool MailboxSubmissionAgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogEnabled"] = value;
				}
			}

			// Token: 0x17003454 RID: 13396
			// (set) Token: 0x06005571 RID: 21873 RVA: 0x0008653D File Offset: 0x0008473D
			public virtual EnhancedTimeSpan MailboxDeliveryAgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogMaxAge"] = value;
				}
			}

			// Token: 0x17003455 RID: 13397
			// (set) Token: 0x06005572 RID: 21874 RVA: 0x00086555 File Offset: 0x00084755
			public virtual Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003456 RID: 13398
			// (set) Token: 0x06005573 RID: 21875 RVA: 0x0008656D File Offset: 0x0008476D
			public virtual Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003457 RID: 13399
			// (set) Token: 0x06005574 RID: 21876 RVA: 0x00086585 File Offset: 0x00084785
			public virtual LocalLongFullPath MailboxDeliveryAgentLogPath
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogPath"] = value;
				}
			}

			// Token: 0x17003458 RID: 13400
			// (set) Token: 0x06005575 RID: 21877 RVA: 0x00086598 File Offset: 0x00084798
			public virtual bool MailboxDeliveryAgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogEnabled"] = value;
				}
			}

			// Token: 0x17003459 RID: 13401
			// (set) Token: 0x06005576 RID: 21878 RVA: 0x000865B0 File Offset: 0x000847B0
			public virtual bool MailboxDeliveryThrottlingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogEnabled"] = value;
				}
			}

			// Token: 0x1700345A RID: 13402
			// (set) Token: 0x06005577 RID: 21879 RVA: 0x000865C8 File Offset: 0x000847C8
			public virtual EnhancedTimeSpan MailboxDeliveryThrottlingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogMaxAge"] = value;
				}
			}

			// Token: 0x1700345B RID: 13403
			// (set) Token: 0x06005578 RID: 21880 RVA: 0x000865E0 File Offset: 0x000847E0
			public virtual Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700345C RID: 13404
			// (set) Token: 0x06005579 RID: 21881 RVA: 0x000865F8 File Offset: 0x000847F8
			public virtual Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700345D RID: 13405
			// (set) Token: 0x0600557A RID: 21882 RVA: 0x00086610 File Offset: 0x00084810
			public virtual LocalLongFullPath MailboxDeliveryThrottlingLogPath
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogPath"] = value;
				}
			}

			// Token: 0x1700345E RID: 13406
			// (set) Token: 0x0600557B RID: 21883 RVA: 0x00086623 File Offset: 0x00084823
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700345F RID: 13407
			// (set) Token: 0x0600557C RID: 21884 RVA: 0x00086636 File Offset: 0x00084836
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x17003460 RID: 13408
			// (set) Token: 0x0600557D RID: 21885 RVA: 0x0008664E File Offset: 0x0008484E
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x17003461 RID: 13409
			// (set) Token: 0x0600557E RID: 21886 RVA: 0x00086666 File Offset: 0x00084866
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003462 RID: 13410
			// (set) Token: 0x0600557F RID: 21887 RVA: 0x0008667E File Offset: 0x0008487E
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003463 RID: 13411
			// (set) Token: 0x06005580 RID: 21888 RVA: 0x00086696 File Offset: 0x00084896
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x17003464 RID: 13412
			// (set) Token: 0x06005581 RID: 21889 RVA: 0x000866A9 File Offset: 0x000848A9
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003465 RID: 13413
			// (set) Token: 0x06005582 RID: 21890 RVA: 0x000866C1 File Offset: 0x000848C1
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003466 RID: 13414
			// (set) Token: 0x06005583 RID: 21891 RVA: 0x000866D9 File Offset: 0x000848D9
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003467 RID: 13415
			// (set) Token: 0x06005584 RID: 21892 RVA: 0x000866F1 File Offset: 0x000848F1
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003468 RID: 13416
			// (set) Token: 0x06005585 RID: 21893 RVA: 0x00086704 File Offset: 0x00084904
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003469 RID: 13417
			// (set) Token: 0x06005586 RID: 21894 RVA: 0x0008671C File Offset: 0x0008491C
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700346A RID: 13418
			// (set) Token: 0x06005587 RID: 21895 RVA: 0x00086734 File Offset: 0x00084934
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700346B RID: 13419
			// (set) Token: 0x06005588 RID: 21896 RVA: 0x0008674C File Offset: 0x0008494C
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x1700346C RID: 13420
			// (set) Token: 0x06005589 RID: 21897 RVA: 0x0008675F File Offset: 0x0008495F
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x1700346D RID: 13421
			// (set) Token: 0x0600558A RID: 21898 RVA: 0x00086777 File Offset: 0x00084977
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x1700346E RID: 13422
			// (set) Token: 0x0600558B RID: 21899 RVA: 0x0008678F File Offset: 0x0008498F
			public virtual bool PipelineTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingEnabled"] = value;
				}
			}

			// Token: 0x1700346F RID: 13423
			// (set) Token: 0x0600558C RID: 21900 RVA: 0x000867A7 File Offset: 0x000849A7
			public virtual bool ContentConversionTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["ContentConversionTracingEnabled"] = value;
				}
			}

			// Token: 0x17003470 RID: 13424
			// (set) Token: 0x0600558D RID: 21901 RVA: 0x000867BF File Offset: 0x000849BF
			public virtual LocalLongFullPath PipelineTracingPath
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingPath"] = value;
				}
			}

			// Token: 0x17003471 RID: 13425
			// (set) Token: 0x0600558E RID: 21902 RVA: 0x000867D2 File Offset: 0x000849D2
			public virtual SmtpAddress? PipelineTracingSenderAddress
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingSenderAddress"] = value;
				}
			}

			// Token: 0x17003472 RID: 13426
			// (set) Token: 0x0600558F RID: 21903 RVA: 0x000867EA File Offset: 0x000849EA
			public virtual ProtocolLoggingLevel MailboxDeliveryConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17003473 RID: 13427
			// (set) Token: 0x06005590 RID: 21904 RVA: 0x00086802 File Offset: 0x00084A02
			public virtual bool MailboxDeliveryConnectorSmtpUtf8Enabled
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryConnectorSmtpUtf8Enabled"] = value;
				}
			}

			// Token: 0x17003474 RID: 13428
			// (set) Token: 0x06005591 RID: 21905 RVA: 0x0008681A File Offset: 0x00084A1A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003475 RID: 13429
			// (set) Token: 0x06005592 RID: 21906 RVA: 0x00086832 File Offset: 0x00084A32
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003476 RID: 13430
			// (set) Token: 0x06005593 RID: 21907 RVA: 0x0008684A File Offset: 0x00084A4A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003477 RID: 13431
			// (set) Token: 0x06005594 RID: 21908 RVA: 0x00086862 File Offset: 0x00084A62
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003478 RID: 13432
			// (set) Token: 0x06005595 RID: 21909 RVA: 0x0008687A File Offset: 0x00084A7A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200066B RID: 1643
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003479 RID: 13433
			// (set) Token: 0x06005597 RID: 21911 RVA: 0x0008689A File Offset: 0x00084A9A
			public virtual MailboxTransportServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700347A RID: 13434
			// (set) Token: 0x06005598 RID: 21912 RVA: 0x000868AD File Offset: 0x00084AAD
			public virtual EnhancedTimeSpan MailboxSubmissionAgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogMaxAge"] = value;
				}
			}

			// Token: 0x1700347B RID: 13435
			// (set) Token: 0x06005599 RID: 21913 RVA: 0x000868C5 File Offset: 0x00084AC5
			public virtual Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700347C RID: 13436
			// (set) Token: 0x0600559A RID: 21914 RVA: 0x000868DD File Offset: 0x00084ADD
			public virtual Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700347D RID: 13437
			// (set) Token: 0x0600559B RID: 21915 RVA: 0x000868F5 File Offset: 0x00084AF5
			public virtual LocalLongFullPath MailboxSubmissionAgentLogPath
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogPath"] = value;
				}
			}

			// Token: 0x1700347E RID: 13438
			// (set) Token: 0x0600559C RID: 21916 RVA: 0x00086908 File Offset: 0x00084B08
			public virtual bool MailboxSubmissionAgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxSubmissionAgentLogEnabled"] = value;
				}
			}

			// Token: 0x1700347F RID: 13439
			// (set) Token: 0x0600559D RID: 21917 RVA: 0x00086920 File Offset: 0x00084B20
			public virtual EnhancedTimeSpan MailboxDeliveryAgentLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogMaxAge"] = value;
				}
			}

			// Token: 0x17003480 RID: 13440
			// (set) Token: 0x0600559E RID: 21918 RVA: 0x00086938 File Offset: 0x00084B38
			public virtual Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003481 RID: 13441
			// (set) Token: 0x0600559F RID: 21919 RVA: 0x00086950 File Offset: 0x00084B50
			public virtual Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003482 RID: 13442
			// (set) Token: 0x060055A0 RID: 21920 RVA: 0x00086968 File Offset: 0x00084B68
			public virtual LocalLongFullPath MailboxDeliveryAgentLogPath
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogPath"] = value;
				}
			}

			// Token: 0x17003483 RID: 13443
			// (set) Token: 0x060055A1 RID: 21921 RVA: 0x0008697B File Offset: 0x00084B7B
			public virtual bool MailboxDeliveryAgentLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryAgentLogEnabled"] = value;
				}
			}

			// Token: 0x17003484 RID: 13444
			// (set) Token: 0x060055A2 RID: 21922 RVA: 0x00086993 File Offset: 0x00084B93
			public virtual bool MailboxDeliveryThrottlingLogEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogEnabled"] = value;
				}
			}

			// Token: 0x17003485 RID: 13445
			// (set) Token: 0x060055A3 RID: 21923 RVA: 0x000869AB File Offset: 0x00084BAB
			public virtual EnhancedTimeSpan MailboxDeliveryThrottlingLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogMaxAge"] = value;
				}
			}

			// Token: 0x17003486 RID: 13446
			// (set) Token: 0x060055A4 RID: 21924 RVA: 0x000869C3 File Offset: 0x00084BC3
			public virtual Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003487 RID: 13447
			// (set) Token: 0x060055A5 RID: 21925 RVA: 0x000869DB File Offset: 0x00084BDB
			public virtual Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003488 RID: 13448
			// (set) Token: 0x060055A6 RID: 21926 RVA: 0x000869F3 File Offset: 0x00084BF3
			public virtual LocalLongFullPath MailboxDeliveryThrottlingLogPath
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryThrottlingLogPath"] = value;
				}
			}

			// Token: 0x17003489 RID: 13449
			// (set) Token: 0x060055A7 RID: 21927 RVA: 0x00086A06 File Offset: 0x00084C06
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700348A RID: 13450
			// (set) Token: 0x060055A8 RID: 21928 RVA: 0x00086A19 File Offset: 0x00084C19
			public virtual bool ConnectivityLogEnabled
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogEnabled"] = value;
				}
			}

			// Token: 0x1700348B RID: 13451
			// (set) Token: 0x060055A9 RID: 21929 RVA: 0x00086A31 File Offset: 0x00084C31
			public virtual EnhancedTimeSpan ConnectivityLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxAge"] = value;
				}
			}

			// Token: 0x1700348C RID: 13452
			// (set) Token: 0x060055AA RID: 21930 RVA: 0x00086A49 File Offset: 0x00084C49
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x1700348D RID: 13453
			// (set) Token: 0x060055AB RID: 21931 RVA: 0x00086A61 File Offset: 0x00084C61
			public virtual Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogMaxFileSize"] = value;
				}
			}

			// Token: 0x1700348E RID: 13454
			// (set) Token: 0x060055AC RID: 21932 RVA: 0x00086A79 File Offset: 0x00084C79
			public virtual LocalLongFullPath ConnectivityLogPath
			{
				set
				{
					base.PowerSharpParameters["ConnectivityLogPath"] = value;
				}
			}

			// Token: 0x1700348F RID: 13455
			// (set) Token: 0x060055AD RID: 21933 RVA: 0x00086A8C File Offset: 0x00084C8C
			public virtual EnhancedTimeSpan ReceiveProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003490 RID: 13456
			// (set) Token: 0x060055AE RID: 21934 RVA: 0x00086AA4 File Offset: 0x00084CA4
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003491 RID: 13457
			// (set) Token: 0x060055AF RID: 21935 RVA: 0x00086ABC File Offset: 0x00084CBC
			public virtual Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003492 RID: 13458
			// (set) Token: 0x060055B0 RID: 21936 RVA: 0x00086AD4 File Offset: 0x00084CD4
			public virtual LocalLongFullPath ReceiveProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["ReceiveProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003493 RID: 13459
			// (set) Token: 0x060055B1 RID: 21937 RVA: 0x00086AE7 File Offset: 0x00084CE7
			public virtual EnhancedTimeSpan SendProtocolLogMaxAge
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxAge"] = value;
				}
			}

			// Token: 0x17003494 RID: 13460
			// (set) Token: 0x060055B2 RID: 21938 RVA: 0x00086AFF File Offset: 0x00084CFF
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxDirectorySize"] = value;
				}
			}

			// Token: 0x17003495 RID: 13461
			// (set) Token: 0x060055B3 RID: 21939 RVA: 0x00086B17 File Offset: 0x00084D17
			public virtual Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogMaxFileSize"] = value;
				}
			}

			// Token: 0x17003496 RID: 13462
			// (set) Token: 0x060055B4 RID: 21940 RVA: 0x00086B2F File Offset: 0x00084D2F
			public virtual LocalLongFullPath SendProtocolLogPath
			{
				set
				{
					base.PowerSharpParameters["SendProtocolLogPath"] = value;
				}
			}

			// Token: 0x17003497 RID: 13463
			// (set) Token: 0x060055B5 RID: 21941 RVA: 0x00086B42 File Offset: 0x00084D42
			public virtual int MaxConcurrentMailboxDeliveries
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxDeliveries"] = value;
				}
			}

			// Token: 0x17003498 RID: 13464
			// (set) Token: 0x060055B6 RID: 21942 RVA: 0x00086B5A File Offset: 0x00084D5A
			public virtual int MaxConcurrentMailboxSubmissions
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMailboxSubmissions"] = value;
				}
			}

			// Token: 0x17003499 RID: 13465
			// (set) Token: 0x060055B7 RID: 21943 RVA: 0x00086B72 File Offset: 0x00084D72
			public virtual bool PipelineTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingEnabled"] = value;
				}
			}

			// Token: 0x1700349A RID: 13466
			// (set) Token: 0x060055B8 RID: 21944 RVA: 0x00086B8A File Offset: 0x00084D8A
			public virtual bool ContentConversionTracingEnabled
			{
				set
				{
					base.PowerSharpParameters["ContentConversionTracingEnabled"] = value;
				}
			}

			// Token: 0x1700349B RID: 13467
			// (set) Token: 0x060055B9 RID: 21945 RVA: 0x00086BA2 File Offset: 0x00084DA2
			public virtual LocalLongFullPath PipelineTracingPath
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingPath"] = value;
				}
			}

			// Token: 0x1700349C RID: 13468
			// (set) Token: 0x060055BA RID: 21946 RVA: 0x00086BB5 File Offset: 0x00084DB5
			public virtual SmtpAddress? PipelineTracingSenderAddress
			{
				set
				{
					base.PowerSharpParameters["PipelineTracingSenderAddress"] = value;
				}
			}

			// Token: 0x1700349D RID: 13469
			// (set) Token: 0x060055BB RID: 21947 RVA: 0x00086BCD File Offset: 0x00084DCD
			public virtual ProtocolLoggingLevel MailboxDeliveryConnectorProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryConnectorProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700349E RID: 13470
			// (set) Token: 0x060055BC RID: 21948 RVA: 0x00086BE5 File Offset: 0x00084DE5
			public virtual bool MailboxDeliveryConnectorSmtpUtf8Enabled
			{
				set
				{
					base.PowerSharpParameters["MailboxDeliveryConnectorSmtpUtf8Enabled"] = value;
				}
			}

			// Token: 0x1700349F RID: 13471
			// (set) Token: 0x060055BD RID: 21949 RVA: 0x00086BFD File Offset: 0x00084DFD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170034A0 RID: 13472
			// (set) Token: 0x060055BE RID: 21950 RVA: 0x00086C15 File Offset: 0x00084E15
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170034A1 RID: 13473
			// (set) Token: 0x060055BF RID: 21951 RVA: 0x00086C2D File Offset: 0x00084E2D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170034A2 RID: 13474
			// (set) Token: 0x060055C0 RID: 21952 RVA: 0x00086C45 File Offset: 0x00084E45
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170034A3 RID: 13475
			// (set) Token: 0x060055C1 RID: 21953 RVA: 0x00086C5D File Offset: 0x00084E5D
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
