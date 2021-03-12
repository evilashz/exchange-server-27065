using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008A9 RID: 2217
	public class NewReceiveConnectorCommand : SyntheticCommandWithPipelineInput<ReceiveConnector, ReceiveConnector>
	{
		// Token: 0x06006DDE RID: 28126 RVA: 0x000A61AB File Offset: 0x000A43AB
		private NewReceiveConnectorCommand() : base("New-ReceiveConnector")
		{
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x000A61B8 File Offset: 0x000A43B8
		public NewReceiveConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006DE0 RID: 28128 RVA: 0x000A61C7 File Offset: 0x000A43C7
		public virtual NewReceiveConnectorCommand SetParameters(NewReceiveConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006DE1 RID: 28129 RVA: 0x000A61D1 File Offset: 0x000A43D1
		public virtual NewReceiveConnectorCommand SetParameters(NewReceiveConnectorCommand.InternetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006DE2 RID: 28130 RVA: 0x000A61DB File Offset: 0x000A43DB
		public virtual NewReceiveConnectorCommand SetParameters(NewReceiveConnectorCommand.InternalParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x000A61E5 File Offset: 0x000A43E5
		public virtual NewReceiveConnectorCommand SetParameters(NewReceiveConnectorCommand.ClientParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006DE4 RID: 28132 RVA: 0x000A61EF File Offset: 0x000A43EF
		public virtual NewReceiveConnectorCommand SetParameters(NewReceiveConnectorCommand.PartnerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006DE5 RID: 28133 RVA: 0x000A61F9 File Offset: 0x000A43F9
		public virtual NewReceiveConnectorCommand SetParameters(NewReceiveConnectorCommand.CustomParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006DE6 RID: 28134 RVA: 0x000A6203 File Offset: 0x000A4403
		public virtual NewReceiveConnectorCommand SetParameters(NewReceiveConnectorCommand.UsageTypeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008AA RID: 2218
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004845 RID: 18501
			// (set) Token: 0x06006DE7 RID: 28135 RVA: 0x000A620D File Offset: 0x000A440D
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004846 RID: 18502
			// (set) Token: 0x06006DE8 RID: 28136 RVA: 0x000A6220 File Offset: 0x000A4420
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x17004847 RID: 18503
			// (set) Token: 0x06006DE9 RID: 28137 RVA: 0x000A6238 File Offset: 0x000A4438
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x17004848 RID: 18504
			// (set) Token: 0x06006DEA RID: 28138 RVA: 0x000A624B File Offset: 0x000A444B
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004849 RID: 18505
			// (set) Token: 0x06006DEB RID: 28139 RVA: 0x000A625E File Offset: 0x000A445E
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x1700484A RID: 18506
			// (set) Token: 0x06006DEC RID: 28140 RVA: 0x000A6276 File Offset: 0x000A4476
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700484B RID: 18507
			// (set) Token: 0x06006DED RID: 28141 RVA: 0x000A628E File Offset: 0x000A448E
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700484C RID: 18508
			// (set) Token: 0x06006DEE RID: 28142 RVA: 0x000A62A6 File Offset: 0x000A44A6
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x1700484D RID: 18509
			// (set) Token: 0x06006DEF RID: 28143 RVA: 0x000A62BE File Offset: 0x000A44BE
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x1700484E RID: 18510
			// (set) Token: 0x06006DF0 RID: 28144 RVA: 0x000A62D1 File Offset: 0x000A44D1
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x1700484F RID: 18511
			// (set) Token: 0x06006DF1 RID: 28145 RVA: 0x000A62E4 File Offset: 0x000A44E4
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x17004850 RID: 18512
			// (set) Token: 0x06006DF2 RID: 28146 RVA: 0x000A62F7 File Offset: 0x000A44F7
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17004851 RID: 18513
			// (set) Token: 0x06006DF3 RID: 28147 RVA: 0x000A630A File Offset: 0x000A450A
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x17004852 RID: 18514
			// (set) Token: 0x06006DF4 RID: 28148 RVA: 0x000A6322 File Offset: 0x000A4522
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x17004853 RID: 18515
			// (set) Token: 0x06006DF5 RID: 28149 RVA: 0x000A633A File Offset: 0x000A453A
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x17004854 RID: 18516
			// (set) Token: 0x06006DF6 RID: 28150 RVA: 0x000A6352 File Offset: 0x000A4552
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x17004855 RID: 18517
			// (set) Token: 0x06006DF7 RID: 28151 RVA: 0x000A636A File Offset: 0x000A456A
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x17004856 RID: 18518
			// (set) Token: 0x06006DF8 RID: 28152 RVA: 0x000A6382 File Offset: 0x000A4582
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x17004857 RID: 18519
			// (set) Token: 0x06006DF9 RID: 28153 RVA: 0x000A639A File Offset: 0x000A459A
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x17004858 RID: 18520
			// (set) Token: 0x06006DFA RID: 28154 RVA: 0x000A63B2 File Offset: 0x000A45B2
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x17004859 RID: 18521
			// (set) Token: 0x06006DFB RID: 28155 RVA: 0x000A63CA File Offset: 0x000A45CA
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x1700485A RID: 18522
			// (set) Token: 0x06006DFC RID: 28156 RVA: 0x000A63E2 File Offset: 0x000A45E2
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x1700485B RID: 18523
			// (set) Token: 0x06006DFD RID: 28157 RVA: 0x000A63FA File Offset: 0x000A45FA
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x1700485C RID: 18524
			// (set) Token: 0x06006DFE RID: 28158 RVA: 0x000A6412 File Offset: 0x000A4612
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x1700485D RID: 18525
			// (set) Token: 0x06006DFF RID: 28159 RVA: 0x000A642A File Offset: 0x000A462A
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x1700485E RID: 18526
			// (set) Token: 0x06006E00 RID: 28160 RVA: 0x000A6442 File Offset: 0x000A4642
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700485F RID: 18527
			// (set) Token: 0x06006E01 RID: 28161 RVA: 0x000A645A File Offset: 0x000A465A
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x17004860 RID: 18528
			// (set) Token: 0x06006E02 RID: 28162 RVA: 0x000A646D File Offset: 0x000A466D
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x17004861 RID: 18529
			// (set) Token: 0x06006E03 RID: 28163 RVA: 0x000A6485 File Offset: 0x000A4685
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x17004862 RID: 18530
			// (set) Token: 0x06006E04 RID: 28164 RVA: 0x000A6498 File Offset: 0x000A4698
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x17004863 RID: 18531
			// (set) Token: 0x06006E05 RID: 28165 RVA: 0x000A64B0 File Offset: 0x000A46B0
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x17004864 RID: 18532
			// (set) Token: 0x06006E06 RID: 28166 RVA: 0x000A64C8 File Offset: 0x000A46C8
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x17004865 RID: 18533
			// (set) Token: 0x06006E07 RID: 28167 RVA: 0x000A64E0 File Offset: 0x000A46E0
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x17004866 RID: 18534
			// (set) Token: 0x06006E08 RID: 28168 RVA: 0x000A64F8 File Offset: 0x000A46F8
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x17004867 RID: 18535
			// (set) Token: 0x06006E09 RID: 28169 RVA: 0x000A6510 File Offset: 0x000A4710
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x17004868 RID: 18536
			// (set) Token: 0x06006E0A RID: 28170 RVA: 0x000A6528 File Offset: 0x000A4728
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x17004869 RID: 18537
			// (set) Token: 0x06006E0B RID: 28171 RVA: 0x000A6540 File Offset: 0x000A4740
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x1700486A RID: 18538
			// (set) Token: 0x06006E0C RID: 28172 RVA: 0x000A6558 File Offset: 0x000A4758
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x1700486B RID: 18539
			// (set) Token: 0x06006E0D RID: 28173 RVA: 0x000A6570 File Offset: 0x000A4770
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x1700486C RID: 18540
			// (set) Token: 0x06006E0E RID: 28174 RVA: 0x000A6588 File Offset: 0x000A4788
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x1700486D RID: 18541
			// (set) Token: 0x06006E0F RID: 28175 RVA: 0x000A65A0 File Offset: 0x000A47A0
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x1700486E RID: 18542
			// (set) Token: 0x06006E10 RID: 28176 RVA: 0x000A65B8 File Offset: 0x000A47B8
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x1700486F RID: 18543
			// (set) Token: 0x06006E11 RID: 28177 RVA: 0x000A65D0 File Offset: 0x000A47D0
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x17004870 RID: 18544
			// (set) Token: 0x06006E12 RID: 28178 RVA: 0x000A65E8 File Offset: 0x000A47E8
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x17004871 RID: 18545
			// (set) Token: 0x06006E13 RID: 28179 RVA: 0x000A6600 File Offset: 0x000A4800
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x17004872 RID: 18546
			// (set) Token: 0x06006E14 RID: 28180 RVA: 0x000A6618 File Offset: 0x000A4818
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x17004873 RID: 18547
			// (set) Token: 0x06006E15 RID: 28181 RVA: 0x000A6630 File Offset: 0x000A4830
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x17004874 RID: 18548
			// (set) Token: 0x06006E16 RID: 28182 RVA: 0x000A6648 File Offset: 0x000A4848
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x17004875 RID: 18549
			// (set) Token: 0x06006E17 RID: 28183 RVA: 0x000A665B File Offset: 0x000A485B
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x17004876 RID: 18550
			// (set) Token: 0x06006E18 RID: 28184 RVA: 0x000A6673 File Offset: 0x000A4873
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004877 RID: 18551
			// (set) Token: 0x06006E19 RID: 28185 RVA: 0x000A6686 File Offset: 0x000A4886
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004878 RID: 18552
			// (set) Token: 0x06006E1A RID: 28186 RVA: 0x000A6699 File Offset: 0x000A4899
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004879 RID: 18553
			// (set) Token: 0x06006E1B RID: 28187 RVA: 0x000A66B1 File Offset: 0x000A48B1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700487A RID: 18554
			// (set) Token: 0x06006E1C RID: 28188 RVA: 0x000A66C9 File Offset: 0x000A48C9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700487B RID: 18555
			// (set) Token: 0x06006E1D RID: 28189 RVA: 0x000A66E1 File Offset: 0x000A48E1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700487C RID: 18556
			// (set) Token: 0x06006E1E RID: 28190 RVA: 0x000A66F9 File Offset: 0x000A48F9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008AB RID: 2219
		public class InternetParameters : ParametersBase
		{
			// Token: 0x1700487D RID: 18557
			// (set) Token: 0x06006E20 RID: 28192 RVA: 0x000A6719 File Offset: 0x000A4919
			public virtual SwitchParameter Internet
			{
				set
				{
					base.PowerSharpParameters["Internet"] = value;
				}
			}

			// Token: 0x1700487E RID: 18558
			// (set) Token: 0x06006E21 RID: 28193 RVA: 0x000A6731 File Offset: 0x000A4931
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x1700487F RID: 18559
			// (set) Token: 0x06006E22 RID: 28194 RVA: 0x000A6744 File Offset: 0x000A4944
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004880 RID: 18560
			// (set) Token: 0x06006E23 RID: 28195 RVA: 0x000A6757 File Offset: 0x000A4957
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x17004881 RID: 18561
			// (set) Token: 0x06006E24 RID: 28196 RVA: 0x000A676F File Offset: 0x000A496F
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004882 RID: 18562
			// (set) Token: 0x06006E25 RID: 28197 RVA: 0x000A6782 File Offset: 0x000A4982
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x17004883 RID: 18563
			// (set) Token: 0x06006E26 RID: 28198 RVA: 0x000A679A File Offset: 0x000A499A
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004884 RID: 18564
			// (set) Token: 0x06006E27 RID: 28199 RVA: 0x000A67B2 File Offset: 0x000A49B2
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x17004885 RID: 18565
			// (set) Token: 0x06006E28 RID: 28200 RVA: 0x000A67CA File Offset: 0x000A49CA
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x17004886 RID: 18566
			// (set) Token: 0x06006E29 RID: 28201 RVA: 0x000A67E2 File Offset: 0x000A49E2
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x17004887 RID: 18567
			// (set) Token: 0x06006E2A RID: 28202 RVA: 0x000A67F5 File Offset: 0x000A49F5
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x17004888 RID: 18568
			// (set) Token: 0x06006E2B RID: 28203 RVA: 0x000A6808 File Offset: 0x000A4A08
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x17004889 RID: 18569
			// (set) Token: 0x06006E2C RID: 28204 RVA: 0x000A681B File Offset: 0x000A4A1B
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x1700488A RID: 18570
			// (set) Token: 0x06006E2D RID: 28205 RVA: 0x000A682E File Offset: 0x000A4A2E
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x1700488B RID: 18571
			// (set) Token: 0x06006E2E RID: 28206 RVA: 0x000A6846 File Offset: 0x000A4A46
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x1700488C RID: 18572
			// (set) Token: 0x06006E2F RID: 28207 RVA: 0x000A685E File Offset: 0x000A4A5E
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x1700488D RID: 18573
			// (set) Token: 0x06006E30 RID: 28208 RVA: 0x000A6876 File Offset: 0x000A4A76
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x1700488E RID: 18574
			// (set) Token: 0x06006E31 RID: 28209 RVA: 0x000A688E File Offset: 0x000A4A8E
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x1700488F RID: 18575
			// (set) Token: 0x06006E32 RID: 28210 RVA: 0x000A68A6 File Offset: 0x000A4AA6
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x17004890 RID: 18576
			// (set) Token: 0x06006E33 RID: 28211 RVA: 0x000A68BE File Offset: 0x000A4ABE
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x17004891 RID: 18577
			// (set) Token: 0x06006E34 RID: 28212 RVA: 0x000A68D6 File Offset: 0x000A4AD6
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x17004892 RID: 18578
			// (set) Token: 0x06006E35 RID: 28213 RVA: 0x000A68EE File Offset: 0x000A4AEE
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004893 RID: 18579
			// (set) Token: 0x06006E36 RID: 28214 RVA: 0x000A6906 File Offset: 0x000A4B06
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x17004894 RID: 18580
			// (set) Token: 0x06006E37 RID: 28215 RVA: 0x000A691E File Offset: 0x000A4B1E
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x17004895 RID: 18581
			// (set) Token: 0x06006E38 RID: 28216 RVA: 0x000A6936 File Offset: 0x000A4B36
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17004896 RID: 18582
			// (set) Token: 0x06006E39 RID: 28217 RVA: 0x000A694E File Offset: 0x000A4B4E
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x17004897 RID: 18583
			// (set) Token: 0x06006E3A RID: 28218 RVA: 0x000A6966 File Offset: 0x000A4B66
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17004898 RID: 18584
			// (set) Token: 0x06006E3B RID: 28219 RVA: 0x000A697E File Offset: 0x000A4B7E
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x17004899 RID: 18585
			// (set) Token: 0x06006E3C RID: 28220 RVA: 0x000A6991 File Offset: 0x000A4B91
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x1700489A RID: 18586
			// (set) Token: 0x06006E3D RID: 28221 RVA: 0x000A69A9 File Offset: 0x000A4BA9
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x1700489B RID: 18587
			// (set) Token: 0x06006E3E RID: 28222 RVA: 0x000A69BC File Offset: 0x000A4BBC
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x1700489C RID: 18588
			// (set) Token: 0x06006E3F RID: 28223 RVA: 0x000A69D4 File Offset: 0x000A4BD4
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x1700489D RID: 18589
			// (set) Token: 0x06006E40 RID: 28224 RVA: 0x000A69EC File Offset: 0x000A4BEC
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x1700489E RID: 18590
			// (set) Token: 0x06006E41 RID: 28225 RVA: 0x000A6A04 File Offset: 0x000A4C04
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x1700489F RID: 18591
			// (set) Token: 0x06006E42 RID: 28226 RVA: 0x000A6A1C File Offset: 0x000A4C1C
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x170048A0 RID: 18592
			// (set) Token: 0x06006E43 RID: 28227 RVA: 0x000A6A34 File Offset: 0x000A4C34
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x170048A1 RID: 18593
			// (set) Token: 0x06006E44 RID: 28228 RVA: 0x000A6A4C File Offset: 0x000A4C4C
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x170048A2 RID: 18594
			// (set) Token: 0x06006E45 RID: 28229 RVA: 0x000A6A64 File Offset: 0x000A4C64
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x170048A3 RID: 18595
			// (set) Token: 0x06006E46 RID: 28230 RVA: 0x000A6A7C File Offset: 0x000A4C7C
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x170048A4 RID: 18596
			// (set) Token: 0x06006E47 RID: 28231 RVA: 0x000A6A94 File Offset: 0x000A4C94
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x170048A5 RID: 18597
			// (set) Token: 0x06006E48 RID: 28232 RVA: 0x000A6AAC File Offset: 0x000A4CAC
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x170048A6 RID: 18598
			// (set) Token: 0x06006E49 RID: 28233 RVA: 0x000A6AC4 File Offset: 0x000A4CC4
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x170048A7 RID: 18599
			// (set) Token: 0x06006E4A RID: 28234 RVA: 0x000A6ADC File Offset: 0x000A4CDC
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x170048A8 RID: 18600
			// (set) Token: 0x06006E4B RID: 28235 RVA: 0x000A6AF4 File Offset: 0x000A4CF4
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x170048A9 RID: 18601
			// (set) Token: 0x06006E4C RID: 28236 RVA: 0x000A6B0C File Offset: 0x000A4D0C
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x170048AA RID: 18602
			// (set) Token: 0x06006E4D RID: 28237 RVA: 0x000A6B24 File Offset: 0x000A4D24
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x170048AB RID: 18603
			// (set) Token: 0x06006E4E RID: 28238 RVA: 0x000A6B3C File Offset: 0x000A4D3C
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x170048AC RID: 18604
			// (set) Token: 0x06006E4F RID: 28239 RVA: 0x000A6B54 File Offset: 0x000A4D54
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x170048AD RID: 18605
			// (set) Token: 0x06006E50 RID: 28240 RVA: 0x000A6B6C File Offset: 0x000A4D6C
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x170048AE RID: 18606
			// (set) Token: 0x06006E51 RID: 28241 RVA: 0x000A6B7F File Offset: 0x000A4D7F
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x170048AF RID: 18607
			// (set) Token: 0x06006E52 RID: 28242 RVA: 0x000A6B97 File Offset: 0x000A4D97
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170048B0 RID: 18608
			// (set) Token: 0x06006E53 RID: 28243 RVA: 0x000A6BAA File Offset: 0x000A4DAA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170048B1 RID: 18609
			// (set) Token: 0x06006E54 RID: 28244 RVA: 0x000A6BBD File Offset: 0x000A4DBD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170048B2 RID: 18610
			// (set) Token: 0x06006E55 RID: 28245 RVA: 0x000A6BD5 File Offset: 0x000A4DD5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170048B3 RID: 18611
			// (set) Token: 0x06006E56 RID: 28246 RVA: 0x000A6BED File Offset: 0x000A4DED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170048B4 RID: 18612
			// (set) Token: 0x06006E57 RID: 28247 RVA: 0x000A6C05 File Offset: 0x000A4E05
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170048B5 RID: 18613
			// (set) Token: 0x06006E58 RID: 28248 RVA: 0x000A6C1D File Offset: 0x000A4E1D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008AC RID: 2220
		public class InternalParameters : ParametersBase
		{
			// Token: 0x170048B6 RID: 18614
			// (set) Token: 0x06006E5A RID: 28250 RVA: 0x000A6C3D File Offset: 0x000A4E3D
			public virtual SwitchParameter Internal
			{
				set
				{
					base.PowerSharpParameters["Internal"] = value;
				}
			}

			// Token: 0x170048B7 RID: 18615
			// (set) Token: 0x06006E5B RID: 28251 RVA: 0x000A6C55 File Offset: 0x000A4E55
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x170048B8 RID: 18616
			// (set) Token: 0x06006E5C RID: 28252 RVA: 0x000A6C68 File Offset: 0x000A4E68
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170048B9 RID: 18617
			// (set) Token: 0x06006E5D RID: 28253 RVA: 0x000A6C7B File Offset: 0x000A4E7B
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x170048BA RID: 18618
			// (set) Token: 0x06006E5E RID: 28254 RVA: 0x000A6C93 File Offset: 0x000A4E93
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x170048BB RID: 18619
			// (set) Token: 0x06006E5F RID: 28255 RVA: 0x000A6CA6 File Offset: 0x000A4EA6
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170048BC RID: 18620
			// (set) Token: 0x06006E60 RID: 28256 RVA: 0x000A6CB9 File Offset: 0x000A4EB9
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x170048BD RID: 18621
			// (set) Token: 0x06006E61 RID: 28257 RVA: 0x000A6CD1 File Offset: 0x000A4ED1
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170048BE RID: 18622
			// (set) Token: 0x06006E62 RID: 28258 RVA: 0x000A6CE9 File Offset: 0x000A4EE9
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x170048BF RID: 18623
			// (set) Token: 0x06006E63 RID: 28259 RVA: 0x000A6D01 File Offset: 0x000A4F01
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x170048C0 RID: 18624
			// (set) Token: 0x06006E64 RID: 28260 RVA: 0x000A6D19 File Offset: 0x000A4F19
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x170048C1 RID: 18625
			// (set) Token: 0x06006E65 RID: 28261 RVA: 0x000A6D2C File Offset: 0x000A4F2C
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x170048C2 RID: 18626
			// (set) Token: 0x06006E66 RID: 28262 RVA: 0x000A6D3F File Offset: 0x000A4F3F
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x170048C3 RID: 18627
			// (set) Token: 0x06006E67 RID: 28263 RVA: 0x000A6D52 File Offset: 0x000A4F52
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x170048C4 RID: 18628
			// (set) Token: 0x06006E68 RID: 28264 RVA: 0x000A6D65 File Offset: 0x000A4F65
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x170048C5 RID: 18629
			// (set) Token: 0x06006E69 RID: 28265 RVA: 0x000A6D7D File Offset: 0x000A4F7D
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x170048C6 RID: 18630
			// (set) Token: 0x06006E6A RID: 28266 RVA: 0x000A6D95 File Offset: 0x000A4F95
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x170048C7 RID: 18631
			// (set) Token: 0x06006E6B RID: 28267 RVA: 0x000A6DAD File Offset: 0x000A4FAD
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x170048C8 RID: 18632
			// (set) Token: 0x06006E6C RID: 28268 RVA: 0x000A6DC5 File Offset: 0x000A4FC5
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x170048C9 RID: 18633
			// (set) Token: 0x06006E6D RID: 28269 RVA: 0x000A6DDD File Offset: 0x000A4FDD
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x170048CA RID: 18634
			// (set) Token: 0x06006E6E RID: 28270 RVA: 0x000A6DF5 File Offset: 0x000A4FF5
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x170048CB RID: 18635
			// (set) Token: 0x06006E6F RID: 28271 RVA: 0x000A6E0D File Offset: 0x000A500D
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x170048CC RID: 18636
			// (set) Token: 0x06006E70 RID: 28272 RVA: 0x000A6E25 File Offset: 0x000A5025
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x170048CD RID: 18637
			// (set) Token: 0x06006E71 RID: 28273 RVA: 0x000A6E3D File Offset: 0x000A503D
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x170048CE RID: 18638
			// (set) Token: 0x06006E72 RID: 28274 RVA: 0x000A6E55 File Offset: 0x000A5055
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x170048CF RID: 18639
			// (set) Token: 0x06006E73 RID: 28275 RVA: 0x000A6E6D File Offset: 0x000A506D
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x170048D0 RID: 18640
			// (set) Token: 0x06006E74 RID: 28276 RVA: 0x000A6E85 File Offset: 0x000A5085
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x170048D1 RID: 18641
			// (set) Token: 0x06006E75 RID: 28277 RVA: 0x000A6E9D File Offset: 0x000A509D
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170048D2 RID: 18642
			// (set) Token: 0x06006E76 RID: 28278 RVA: 0x000A6EB5 File Offset: 0x000A50B5
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x170048D3 RID: 18643
			// (set) Token: 0x06006E77 RID: 28279 RVA: 0x000A6ECD File Offset: 0x000A50CD
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x170048D4 RID: 18644
			// (set) Token: 0x06006E78 RID: 28280 RVA: 0x000A6EE0 File Offset: 0x000A50E0
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x170048D5 RID: 18645
			// (set) Token: 0x06006E79 RID: 28281 RVA: 0x000A6EF8 File Offset: 0x000A50F8
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x170048D6 RID: 18646
			// (set) Token: 0x06006E7A RID: 28282 RVA: 0x000A6F10 File Offset: 0x000A5110
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x170048D7 RID: 18647
			// (set) Token: 0x06006E7B RID: 28283 RVA: 0x000A6F28 File Offset: 0x000A5128
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x170048D8 RID: 18648
			// (set) Token: 0x06006E7C RID: 28284 RVA: 0x000A6F40 File Offset: 0x000A5140
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x170048D9 RID: 18649
			// (set) Token: 0x06006E7D RID: 28285 RVA: 0x000A6F58 File Offset: 0x000A5158
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x170048DA RID: 18650
			// (set) Token: 0x06006E7E RID: 28286 RVA: 0x000A6F70 File Offset: 0x000A5170
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x170048DB RID: 18651
			// (set) Token: 0x06006E7F RID: 28287 RVA: 0x000A6F88 File Offset: 0x000A5188
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x170048DC RID: 18652
			// (set) Token: 0x06006E80 RID: 28288 RVA: 0x000A6FA0 File Offset: 0x000A51A0
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x170048DD RID: 18653
			// (set) Token: 0x06006E81 RID: 28289 RVA: 0x000A6FB8 File Offset: 0x000A51B8
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x170048DE RID: 18654
			// (set) Token: 0x06006E82 RID: 28290 RVA: 0x000A6FD0 File Offset: 0x000A51D0
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x170048DF RID: 18655
			// (set) Token: 0x06006E83 RID: 28291 RVA: 0x000A6FE8 File Offset: 0x000A51E8
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x170048E0 RID: 18656
			// (set) Token: 0x06006E84 RID: 28292 RVA: 0x000A7000 File Offset: 0x000A5200
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x170048E1 RID: 18657
			// (set) Token: 0x06006E85 RID: 28293 RVA: 0x000A7018 File Offset: 0x000A5218
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x170048E2 RID: 18658
			// (set) Token: 0x06006E86 RID: 28294 RVA: 0x000A7030 File Offset: 0x000A5230
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x170048E3 RID: 18659
			// (set) Token: 0x06006E87 RID: 28295 RVA: 0x000A7048 File Offset: 0x000A5248
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x170048E4 RID: 18660
			// (set) Token: 0x06006E88 RID: 28296 RVA: 0x000A7060 File Offset: 0x000A5260
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x170048E5 RID: 18661
			// (set) Token: 0x06006E89 RID: 28297 RVA: 0x000A7078 File Offset: 0x000A5278
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x170048E6 RID: 18662
			// (set) Token: 0x06006E8A RID: 28298 RVA: 0x000A7090 File Offset: 0x000A5290
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x170048E7 RID: 18663
			// (set) Token: 0x06006E8B RID: 28299 RVA: 0x000A70A3 File Offset: 0x000A52A3
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x170048E8 RID: 18664
			// (set) Token: 0x06006E8C RID: 28300 RVA: 0x000A70BB File Offset: 0x000A52BB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170048E9 RID: 18665
			// (set) Token: 0x06006E8D RID: 28301 RVA: 0x000A70CE File Offset: 0x000A52CE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170048EA RID: 18666
			// (set) Token: 0x06006E8E RID: 28302 RVA: 0x000A70E1 File Offset: 0x000A52E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170048EB RID: 18667
			// (set) Token: 0x06006E8F RID: 28303 RVA: 0x000A70F9 File Offset: 0x000A52F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170048EC RID: 18668
			// (set) Token: 0x06006E90 RID: 28304 RVA: 0x000A7111 File Offset: 0x000A5311
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170048ED RID: 18669
			// (set) Token: 0x06006E91 RID: 28305 RVA: 0x000A7129 File Offset: 0x000A5329
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170048EE RID: 18670
			// (set) Token: 0x06006E92 RID: 28306 RVA: 0x000A7141 File Offset: 0x000A5341
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008AD RID: 2221
		public class ClientParameters : ParametersBase
		{
			// Token: 0x170048EF RID: 18671
			// (set) Token: 0x06006E94 RID: 28308 RVA: 0x000A7161 File Offset: 0x000A5361
			public virtual SwitchParameter Client
			{
				set
				{
					base.PowerSharpParameters["Client"] = value;
				}
			}

			// Token: 0x170048F0 RID: 18672
			// (set) Token: 0x06006E95 RID: 28309 RVA: 0x000A7179 File Offset: 0x000A5379
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x170048F1 RID: 18673
			// (set) Token: 0x06006E96 RID: 28310 RVA: 0x000A718C File Offset: 0x000A538C
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170048F2 RID: 18674
			// (set) Token: 0x06006E97 RID: 28311 RVA: 0x000A719F File Offset: 0x000A539F
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x170048F3 RID: 18675
			// (set) Token: 0x06006E98 RID: 28312 RVA: 0x000A71B7 File Offset: 0x000A53B7
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x170048F4 RID: 18676
			// (set) Token: 0x06006E99 RID: 28313 RVA: 0x000A71CA File Offset: 0x000A53CA
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170048F5 RID: 18677
			// (set) Token: 0x06006E9A RID: 28314 RVA: 0x000A71DD File Offset: 0x000A53DD
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x170048F6 RID: 18678
			// (set) Token: 0x06006E9B RID: 28315 RVA: 0x000A71F5 File Offset: 0x000A53F5
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170048F7 RID: 18679
			// (set) Token: 0x06006E9C RID: 28316 RVA: 0x000A720D File Offset: 0x000A540D
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x170048F8 RID: 18680
			// (set) Token: 0x06006E9D RID: 28317 RVA: 0x000A7225 File Offset: 0x000A5425
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x170048F9 RID: 18681
			// (set) Token: 0x06006E9E RID: 28318 RVA: 0x000A723D File Offset: 0x000A543D
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x170048FA RID: 18682
			// (set) Token: 0x06006E9F RID: 28319 RVA: 0x000A7250 File Offset: 0x000A5450
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x170048FB RID: 18683
			// (set) Token: 0x06006EA0 RID: 28320 RVA: 0x000A7263 File Offset: 0x000A5463
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x170048FC RID: 18684
			// (set) Token: 0x06006EA1 RID: 28321 RVA: 0x000A7276 File Offset: 0x000A5476
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x170048FD RID: 18685
			// (set) Token: 0x06006EA2 RID: 28322 RVA: 0x000A7289 File Offset: 0x000A5489
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x170048FE RID: 18686
			// (set) Token: 0x06006EA3 RID: 28323 RVA: 0x000A72A1 File Offset: 0x000A54A1
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x170048FF RID: 18687
			// (set) Token: 0x06006EA4 RID: 28324 RVA: 0x000A72B9 File Offset: 0x000A54B9
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x17004900 RID: 18688
			// (set) Token: 0x06006EA5 RID: 28325 RVA: 0x000A72D1 File Offset: 0x000A54D1
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x17004901 RID: 18689
			// (set) Token: 0x06006EA6 RID: 28326 RVA: 0x000A72E9 File Offset: 0x000A54E9
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x17004902 RID: 18690
			// (set) Token: 0x06006EA7 RID: 28327 RVA: 0x000A7301 File Offset: 0x000A5501
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x17004903 RID: 18691
			// (set) Token: 0x06006EA8 RID: 28328 RVA: 0x000A7319 File Offset: 0x000A5519
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x17004904 RID: 18692
			// (set) Token: 0x06006EA9 RID: 28329 RVA: 0x000A7331 File Offset: 0x000A5531
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x17004905 RID: 18693
			// (set) Token: 0x06006EAA RID: 28330 RVA: 0x000A7349 File Offset: 0x000A5549
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004906 RID: 18694
			// (set) Token: 0x06006EAB RID: 28331 RVA: 0x000A7361 File Offset: 0x000A5561
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x17004907 RID: 18695
			// (set) Token: 0x06006EAC RID: 28332 RVA: 0x000A7379 File Offset: 0x000A5579
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x17004908 RID: 18696
			// (set) Token: 0x06006EAD RID: 28333 RVA: 0x000A7391 File Offset: 0x000A5591
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17004909 RID: 18697
			// (set) Token: 0x06006EAE RID: 28334 RVA: 0x000A73A9 File Offset: 0x000A55A9
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x1700490A RID: 18698
			// (set) Token: 0x06006EAF RID: 28335 RVA: 0x000A73C1 File Offset: 0x000A55C1
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700490B RID: 18699
			// (set) Token: 0x06006EB0 RID: 28336 RVA: 0x000A73D9 File Offset: 0x000A55D9
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x1700490C RID: 18700
			// (set) Token: 0x06006EB1 RID: 28337 RVA: 0x000A73F1 File Offset: 0x000A55F1
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x1700490D RID: 18701
			// (set) Token: 0x06006EB2 RID: 28338 RVA: 0x000A7404 File Offset: 0x000A5604
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x1700490E RID: 18702
			// (set) Token: 0x06006EB3 RID: 28339 RVA: 0x000A741C File Offset: 0x000A561C
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x1700490F RID: 18703
			// (set) Token: 0x06006EB4 RID: 28340 RVA: 0x000A7434 File Offset: 0x000A5634
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x17004910 RID: 18704
			// (set) Token: 0x06006EB5 RID: 28341 RVA: 0x000A744C File Offset: 0x000A564C
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x17004911 RID: 18705
			// (set) Token: 0x06006EB6 RID: 28342 RVA: 0x000A7464 File Offset: 0x000A5664
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x17004912 RID: 18706
			// (set) Token: 0x06006EB7 RID: 28343 RVA: 0x000A747C File Offset: 0x000A567C
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x17004913 RID: 18707
			// (set) Token: 0x06006EB8 RID: 28344 RVA: 0x000A7494 File Offset: 0x000A5694
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x17004914 RID: 18708
			// (set) Token: 0x06006EB9 RID: 28345 RVA: 0x000A74AC File Offset: 0x000A56AC
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x17004915 RID: 18709
			// (set) Token: 0x06006EBA RID: 28346 RVA: 0x000A74C4 File Offset: 0x000A56C4
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x17004916 RID: 18710
			// (set) Token: 0x06006EBB RID: 28347 RVA: 0x000A74DC File Offset: 0x000A56DC
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x17004917 RID: 18711
			// (set) Token: 0x06006EBC RID: 28348 RVA: 0x000A74F4 File Offset: 0x000A56F4
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x17004918 RID: 18712
			// (set) Token: 0x06006EBD RID: 28349 RVA: 0x000A750C File Offset: 0x000A570C
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x17004919 RID: 18713
			// (set) Token: 0x06006EBE RID: 28350 RVA: 0x000A7524 File Offset: 0x000A5724
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x1700491A RID: 18714
			// (set) Token: 0x06006EBF RID: 28351 RVA: 0x000A753C File Offset: 0x000A573C
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x1700491B RID: 18715
			// (set) Token: 0x06006EC0 RID: 28352 RVA: 0x000A7554 File Offset: 0x000A5754
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x1700491C RID: 18716
			// (set) Token: 0x06006EC1 RID: 28353 RVA: 0x000A756C File Offset: 0x000A576C
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x1700491D RID: 18717
			// (set) Token: 0x06006EC2 RID: 28354 RVA: 0x000A7584 File Offset: 0x000A5784
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x1700491E RID: 18718
			// (set) Token: 0x06006EC3 RID: 28355 RVA: 0x000A759C File Offset: 0x000A579C
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x1700491F RID: 18719
			// (set) Token: 0x06006EC4 RID: 28356 RVA: 0x000A75B4 File Offset: 0x000A57B4
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x17004920 RID: 18720
			// (set) Token: 0x06006EC5 RID: 28357 RVA: 0x000A75C7 File Offset: 0x000A57C7
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x17004921 RID: 18721
			// (set) Token: 0x06006EC6 RID: 28358 RVA: 0x000A75DF File Offset: 0x000A57DF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004922 RID: 18722
			// (set) Token: 0x06006EC7 RID: 28359 RVA: 0x000A75F2 File Offset: 0x000A57F2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004923 RID: 18723
			// (set) Token: 0x06006EC8 RID: 28360 RVA: 0x000A7605 File Offset: 0x000A5805
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004924 RID: 18724
			// (set) Token: 0x06006EC9 RID: 28361 RVA: 0x000A761D File Offset: 0x000A581D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004925 RID: 18725
			// (set) Token: 0x06006ECA RID: 28362 RVA: 0x000A7635 File Offset: 0x000A5835
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004926 RID: 18726
			// (set) Token: 0x06006ECB RID: 28363 RVA: 0x000A764D File Offset: 0x000A584D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004927 RID: 18727
			// (set) Token: 0x06006ECC RID: 28364 RVA: 0x000A7665 File Offset: 0x000A5865
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008AE RID: 2222
		public class PartnerParameters : ParametersBase
		{
			// Token: 0x17004928 RID: 18728
			// (set) Token: 0x06006ECE RID: 28366 RVA: 0x000A7685 File Offset: 0x000A5885
			public virtual SwitchParameter Partner
			{
				set
				{
					base.PowerSharpParameters["Partner"] = value;
				}
			}

			// Token: 0x17004929 RID: 18729
			// (set) Token: 0x06006ECF RID: 28367 RVA: 0x000A769D File Offset: 0x000A589D
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x1700492A RID: 18730
			// (set) Token: 0x06006ED0 RID: 28368 RVA: 0x000A76B0 File Offset: 0x000A58B0
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x1700492B RID: 18731
			// (set) Token: 0x06006ED1 RID: 28369 RVA: 0x000A76C3 File Offset: 0x000A58C3
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700492C RID: 18732
			// (set) Token: 0x06006ED2 RID: 28370 RVA: 0x000A76D6 File Offset: 0x000A58D6
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x1700492D RID: 18733
			// (set) Token: 0x06006ED3 RID: 28371 RVA: 0x000A76EE File Offset: 0x000A58EE
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x1700492E RID: 18734
			// (set) Token: 0x06006ED4 RID: 28372 RVA: 0x000A7701 File Offset: 0x000A5901
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x1700492F RID: 18735
			// (set) Token: 0x06006ED5 RID: 28373 RVA: 0x000A7719 File Offset: 0x000A5919
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004930 RID: 18736
			// (set) Token: 0x06006ED6 RID: 28374 RVA: 0x000A7731 File Offset: 0x000A5931
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x17004931 RID: 18737
			// (set) Token: 0x06006ED7 RID: 28375 RVA: 0x000A7749 File Offset: 0x000A5949
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x17004932 RID: 18738
			// (set) Token: 0x06006ED8 RID: 28376 RVA: 0x000A7761 File Offset: 0x000A5961
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x17004933 RID: 18739
			// (set) Token: 0x06006ED9 RID: 28377 RVA: 0x000A7774 File Offset: 0x000A5974
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x17004934 RID: 18740
			// (set) Token: 0x06006EDA RID: 28378 RVA: 0x000A7787 File Offset: 0x000A5987
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x17004935 RID: 18741
			// (set) Token: 0x06006EDB RID: 28379 RVA: 0x000A779A File Offset: 0x000A599A
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17004936 RID: 18742
			// (set) Token: 0x06006EDC RID: 28380 RVA: 0x000A77AD File Offset: 0x000A59AD
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x17004937 RID: 18743
			// (set) Token: 0x06006EDD RID: 28381 RVA: 0x000A77C5 File Offset: 0x000A59C5
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x17004938 RID: 18744
			// (set) Token: 0x06006EDE RID: 28382 RVA: 0x000A77DD File Offset: 0x000A59DD
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x17004939 RID: 18745
			// (set) Token: 0x06006EDF RID: 28383 RVA: 0x000A77F5 File Offset: 0x000A59F5
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x1700493A RID: 18746
			// (set) Token: 0x06006EE0 RID: 28384 RVA: 0x000A780D File Offset: 0x000A5A0D
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x1700493B RID: 18747
			// (set) Token: 0x06006EE1 RID: 28385 RVA: 0x000A7825 File Offset: 0x000A5A25
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x1700493C RID: 18748
			// (set) Token: 0x06006EE2 RID: 28386 RVA: 0x000A783D File Offset: 0x000A5A3D
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x1700493D RID: 18749
			// (set) Token: 0x06006EE3 RID: 28387 RVA: 0x000A7855 File Offset: 0x000A5A55
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x1700493E RID: 18750
			// (set) Token: 0x06006EE4 RID: 28388 RVA: 0x000A786D File Offset: 0x000A5A6D
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x1700493F RID: 18751
			// (set) Token: 0x06006EE5 RID: 28389 RVA: 0x000A7885 File Offset: 0x000A5A85
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x17004940 RID: 18752
			// (set) Token: 0x06006EE6 RID: 28390 RVA: 0x000A789D File Offset: 0x000A5A9D
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x17004941 RID: 18753
			// (set) Token: 0x06006EE7 RID: 28391 RVA: 0x000A78B5 File Offset: 0x000A5AB5
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17004942 RID: 18754
			// (set) Token: 0x06006EE8 RID: 28392 RVA: 0x000A78CD File Offset: 0x000A5ACD
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x17004943 RID: 18755
			// (set) Token: 0x06006EE9 RID: 28393 RVA: 0x000A78E5 File Offset: 0x000A5AE5
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17004944 RID: 18756
			// (set) Token: 0x06006EEA RID: 28394 RVA: 0x000A78FD File Offset: 0x000A5AFD
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x17004945 RID: 18757
			// (set) Token: 0x06006EEB RID: 28395 RVA: 0x000A7915 File Offset: 0x000A5B15
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x17004946 RID: 18758
			// (set) Token: 0x06006EEC RID: 28396 RVA: 0x000A7928 File Offset: 0x000A5B28
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x17004947 RID: 18759
			// (set) Token: 0x06006EED RID: 28397 RVA: 0x000A7940 File Offset: 0x000A5B40
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x17004948 RID: 18760
			// (set) Token: 0x06006EEE RID: 28398 RVA: 0x000A7958 File Offset: 0x000A5B58
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x17004949 RID: 18761
			// (set) Token: 0x06006EEF RID: 28399 RVA: 0x000A7970 File Offset: 0x000A5B70
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x1700494A RID: 18762
			// (set) Token: 0x06006EF0 RID: 28400 RVA: 0x000A7988 File Offset: 0x000A5B88
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x1700494B RID: 18763
			// (set) Token: 0x06006EF1 RID: 28401 RVA: 0x000A79A0 File Offset: 0x000A5BA0
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x1700494C RID: 18764
			// (set) Token: 0x06006EF2 RID: 28402 RVA: 0x000A79B8 File Offset: 0x000A5BB8
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x1700494D RID: 18765
			// (set) Token: 0x06006EF3 RID: 28403 RVA: 0x000A79D0 File Offset: 0x000A5BD0
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x1700494E RID: 18766
			// (set) Token: 0x06006EF4 RID: 28404 RVA: 0x000A79E8 File Offset: 0x000A5BE8
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x1700494F RID: 18767
			// (set) Token: 0x06006EF5 RID: 28405 RVA: 0x000A7A00 File Offset: 0x000A5C00
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x17004950 RID: 18768
			// (set) Token: 0x06006EF6 RID: 28406 RVA: 0x000A7A18 File Offset: 0x000A5C18
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x17004951 RID: 18769
			// (set) Token: 0x06006EF7 RID: 28407 RVA: 0x000A7A30 File Offset: 0x000A5C30
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x17004952 RID: 18770
			// (set) Token: 0x06006EF8 RID: 28408 RVA: 0x000A7A48 File Offset: 0x000A5C48
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x17004953 RID: 18771
			// (set) Token: 0x06006EF9 RID: 28409 RVA: 0x000A7A60 File Offset: 0x000A5C60
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x17004954 RID: 18772
			// (set) Token: 0x06006EFA RID: 28410 RVA: 0x000A7A78 File Offset: 0x000A5C78
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x17004955 RID: 18773
			// (set) Token: 0x06006EFB RID: 28411 RVA: 0x000A7A90 File Offset: 0x000A5C90
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x17004956 RID: 18774
			// (set) Token: 0x06006EFC RID: 28412 RVA: 0x000A7AA8 File Offset: 0x000A5CA8
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x17004957 RID: 18775
			// (set) Token: 0x06006EFD RID: 28413 RVA: 0x000A7AC0 File Offset: 0x000A5CC0
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x17004958 RID: 18776
			// (set) Token: 0x06006EFE RID: 28414 RVA: 0x000A7AD8 File Offset: 0x000A5CD8
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x17004959 RID: 18777
			// (set) Token: 0x06006EFF RID: 28415 RVA: 0x000A7AEB File Offset: 0x000A5CEB
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x1700495A RID: 18778
			// (set) Token: 0x06006F00 RID: 28416 RVA: 0x000A7B03 File Offset: 0x000A5D03
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700495B RID: 18779
			// (set) Token: 0x06006F01 RID: 28417 RVA: 0x000A7B16 File Offset: 0x000A5D16
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700495C RID: 18780
			// (set) Token: 0x06006F02 RID: 28418 RVA: 0x000A7B29 File Offset: 0x000A5D29
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700495D RID: 18781
			// (set) Token: 0x06006F03 RID: 28419 RVA: 0x000A7B41 File Offset: 0x000A5D41
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700495E RID: 18782
			// (set) Token: 0x06006F04 RID: 28420 RVA: 0x000A7B59 File Offset: 0x000A5D59
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700495F RID: 18783
			// (set) Token: 0x06006F05 RID: 28421 RVA: 0x000A7B71 File Offset: 0x000A5D71
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004960 RID: 18784
			// (set) Token: 0x06006F06 RID: 28422 RVA: 0x000A7B89 File Offset: 0x000A5D89
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008AF RID: 2223
		public class CustomParameters : ParametersBase
		{
			// Token: 0x17004961 RID: 18785
			// (set) Token: 0x06006F08 RID: 28424 RVA: 0x000A7BA9 File Offset: 0x000A5DA9
			public virtual SwitchParameter Custom
			{
				set
				{
					base.PowerSharpParameters["Custom"] = value;
				}
			}

			// Token: 0x17004962 RID: 18786
			// (set) Token: 0x06006F09 RID: 28425 RVA: 0x000A7BC1 File Offset: 0x000A5DC1
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x17004963 RID: 18787
			// (set) Token: 0x06006F0A RID: 28426 RVA: 0x000A7BD4 File Offset: 0x000A5DD4
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x17004964 RID: 18788
			// (set) Token: 0x06006F0B RID: 28427 RVA: 0x000A7BE7 File Offset: 0x000A5DE7
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004965 RID: 18789
			// (set) Token: 0x06006F0C RID: 28428 RVA: 0x000A7BFA File Offset: 0x000A5DFA
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x17004966 RID: 18790
			// (set) Token: 0x06006F0D RID: 28429 RVA: 0x000A7C12 File Offset: 0x000A5E12
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004967 RID: 18791
			// (set) Token: 0x06006F0E RID: 28430 RVA: 0x000A7C25 File Offset: 0x000A5E25
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x17004968 RID: 18792
			// (set) Token: 0x06006F0F RID: 28431 RVA: 0x000A7C3D File Offset: 0x000A5E3D
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004969 RID: 18793
			// (set) Token: 0x06006F10 RID: 28432 RVA: 0x000A7C55 File Offset: 0x000A5E55
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x1700496A RID: 18794
			// (set) Token: 0x06006F11 RID: 28433 RVA: 0x000A7C6D File Offset: 0x000A5E6D
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x1700496B RID: 18795
			// (set) Token: 0x06006F12 RID: 28434 RVA: 0x000A7C85 File Offset: 0x000A5E85
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x1700496C RID: 18796
			// (set) Token: 0x06006F13 RID: 28435 RVA: 0x000A7C98 File Offset: 0x000A5E98
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x1700496D RID: 18797
			// (set) Token: 0x06006F14 RID: 28436 RVA: 0x000A7CAB File Offset: 0x000A5EAB
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x1700496E RID: 18798
			// (set) Token: 0x06006F15 RID: 28437 RVA: 0x000A7CBE File Offset: 0x000A5EBE
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x1700496F RID: 18799
			// (set) Token: 0x06006F16 RID: 28438 RVA: 0x000A7CD1 File Offset: 0x000A5ED1
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x17004970 RID: 18800
			// (set) Token: 0x06006F17 RID: 28439 RVA: 0x000A7CE9 File Offset: 0x000A5EE9
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x17004971 RID: 18801
			// (set) Token: 0x06006F18 RID: 28440 RVA: 0x000A7D01 File Offset: 0x000A5F01
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x17004972 RID: 18802
			// (set) Token: 0x06006F19 RID: 28441 RVA: 0x000A7D19 File Offset: 0x000A5F19
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x17004973 RID: 18803
			// (set) Token: 0x06006F1A RID: 28442 RVA: 0x000A7D31 File Offset: 0x000A5F31
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x17004974 RID: 18804
			// (set) Token: 0x06006F1B RID: 28443 RVA: 0x000A7D49 File Offset: 0x000A5F49
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x17004975 RID: 18805
			// (set) Token: 0x06006F1C RID: 28444 RVA: 0x000A7D61 File Offset: 0x000A5F61
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x17004976 RID: 18806
			// (set) Token: 0x06006F1D RID: 28445 RVA: 0x000A7D79 File Offset: 0x000A5F79
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x17004977 RID: 18807
			// (set) Token: 0x06006F1E RID: 28446 RVA: 0x000A7D91 File Offset: 0x000A5F91
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004978 RID: 18808
			// (set) Token: 0x06006F1F RID: 28447 RVA: 0x000A7DA9 File Offset: 0x000A5FA9
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x17004979 RID: 18809
			// (set) Token: 0x06006F20 RID: 28448 RVA: 0x000A7DC1 File Offset: 0x000A5FC1
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x1700497A RID: 18810
			// (set) Token: 0x06006F21 RID: 28449 RVA: 0x000A7DD9 File Offset: 0x000A5FD9
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x1700497B RID: 18811
			// (set) Token: 0x06006F22 RID: 28450 RVA: 0x000A7DF1 File Offset: 0x000A5FF1
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x1700497C RID: 18812
			// (set) Token: 0x06006F23 RID: 28451 RVA: 0x000A7E09 File Offset: 0x000A6009
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x1700497D RID: 18813
			// (set) Token: 0x06006F24 RID: 28452 RVA: 0x000A7E21 File Offset: 0x000A6021
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x1700497E RID: 18814
			// (set) Token: 0x06006F25 RID: 28453 RVA: 0x000A7E39 File Offset: 0x000A6039
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x1700497F RID: 18815
			// (set) Token: 0x06006F26 RID: 28454 RVA: 0x000A7E4C File Offset: 0x000A604C
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x17004980 RID: 18816
			// (set) Token: 0x06006F27 RID: 28455 RVA: 0x000A7E64 File Offset: 0x000A6064
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x17004981 RID: 18817
			// (set) Token: 0x06006F28 RID: 28456 RVA: 0x000A7E7C File Offset: 0x000A607C
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x17004982 RID: 18818
			// (set) Token: 0x06006F29 RID: 28457 RVA: 0x000A7E94 File Offset: 0x000A6094
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x17004983 RID: 18819
			// (set) Token: 0x06006F2A RID: 28458 RVA: 0x000A7EAC File Offset: 0x000A60AC
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x17004984 RID: 18820
			// (set) Token: 0x06006F2B RID: 28459 RVA: 0x000A7EC4 File Offset: 0x000A60C4
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x17004985 RID: 18821
			// (set) Token: 0x06006F2C RID: 28460 RVA: 0x000A7EDC File Offset: 0x000A60DC
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x17004986 RID: 18822
			// (set) Token: 0x06006F2D RID: 28461 RVA: 0x000A7EF4 File Offset: 0x000A60F4
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x17004987 RID: 18823
			// (set) Token: 0x06006F2E RID: 28462 RVA: 0x000A7F0C File Offset: 0x000A610C
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x17004988 RID: 18824
			// (set) Token: 0x06006F2F RID: 28463 RVA: 0x000A7F24 File Offset: 0x000A6124
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x17004989 RID: 18825
			// (set) Token: 0x06006F30 RID: 28464 RVA: 0x000A7F3C File Offset: 0x000A613C
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x1700498A RID: 18826
			// (set) Token: 0x06006F31 RID: 28465 RVA: 0x000A7F54 File Offset: 0x000A6154
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x1700498B RID: 18827
			// (set) Token: 0x06006F32 RID: 28466 RVA: 0x000A7F6C File Offset: 0x000A616C
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x1700498C RID: 18828
			// (set) Token: 0x06006F33 RID: 28467 RVA: 0x000A7F84 File Offset: 0x000A6184
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x1700498D RID: 18829
			// (set) Token: 0x06006F34 RID: 28468 RVA: 0x000A7F9C File Offset: 0x000A619C
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x1700498E RID: 18830
			// (set) Token: 0x06006F35 RID: 28469 RVA: 0x000A7FB4 File Offset: 0x000A61B4
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x1700498F RID: 18831
			// (set) Token: 0x06006F36 RID: 28470 RVA: 0x000A7FCC File Offset: 0x000A61CC
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x17004990 RID: 18832
			// (set) Token: 0x06006F37 RID: 28471 RVA: 0x000A7FE4 File Offset: 0x000A61E4
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x17004991 RID: 18833
			// (set) Token: 0x06006F38 RID: 28472 RVA: 0x000A7FFC File Offset: 0x000A61FC
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x17004992 RID: 18834
			// (set) Token: 0x06006F39 RID: 28473 RVA: 0x000A800F File Offset: 0x000A620F
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x17004993 RID: 18835
			// (set) Token: 0x06006F3A RID: 28474 RVA: 0x000A8027 File Offset: 0x000A6227
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004994 RID: 18836
			// (set) Token: 0x06006F3B RID: 28475 RVA: 0x000A803A File Offset: 0x000A623A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004995 RID: 18837
			// (set) Token: 0x06006F3C RID: 28476 RVA: 0x000A804D File Offset: 0x000A624D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004996 RID: 18838
			// (set) Token: 0x06006F3D RID: 28477 RVA: 0x000A8065 File Offset: 0x000A6265
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004997 RID: 18839
			// (set) Token: 0x06006F3E RID: 28478 RVA: 0x000A807D File Offset: 0x000A627D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004998 RID: 18840
			// (set) Token: 0x06006F3F RID: 28479 RVA: 0x000A8095 File Offset: 0x000A6295
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004999 RID: 18841
			// (set) Token: 0x06006F40 RID: 28480 RVA: 0x000A80AD File Offset: 0x000A62AD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008B0 RID: 2224
		public class UsageTypeParameters : ParametersBase
		{
			// Token: 0x1700499A RID: 18842
			// (set) Token: 0x06006F42 RID: 28482 RVA: 0x000A80CD File Offset: 0x000A62CD
			public virtual NewReceiveConnector.UsageType Usage
			{
				set
				{
					base.PowerSharpParameters["Usage"] = value;
				}
			}

			// Token: 0x1700499B RID: 18843
			// (set) Token: 0x06006F43 RID: 28483 RVA: 0x000A80E5 File Offset: 0x000A62E5
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x1700499C RID: 18844
			// (set) Token: 0x06006F44 RID: 28484 RVA: 0x000A80F8 File Offset: 0x000A62F8
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x1700499D RID: 18845
			// (set) Token: 0x06006F45 RID: 28485 RVA: 0x000A810B File Offset: 0x000A630B
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700499E RID: 18846
			// (set) Token: 0x06006F46 RID: 28486 RVA: 0x000A811E File Offset: 0x000A631E
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x1700499F RID: 18847
			// (set) Token: 0x06006F47 RID: 28487 RVA: 0x000A8136 File Offset: 0x000A6336
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170049A0 RID: 18848
			// (set) Token: 0x06006F48 RID: 28488 RVA: 0x000A8149 File Offset: 0x000A6349
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x170049A1 RID: 18849
			// (set) Token: 0x06006F49 RID: 28489 RVA: 0x000A8161 File Offset: 0x000A6361
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170049A2 RID: 18850
			// (set) Token: 0x06006F4A RID: 28490 RVA: 0x000A8179 File Offset: 0x000A6379
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x170049A3 RID: 18851
			// (set) Token: 0x06006F4B RID: 28491 RVA: 0x000A8191 File Offset: 0x000A6391
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x170049A4 RID: 18852
			// (set) Token: 0x06006F4C RID: 28492 RVA: 0x000A81A9 File Offset: 0x000A63A9
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x170049A5 RID: 18853
			// (set) Token: 0x06006F4D RID: 28493 RVA: 0x000A81BC File Offset: 0x000A63BC
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x170049A6 RID: 18854
			// (set) Token: 0x06006F4E RID: 28494 RVA: 0x000A81CF File Offset: 0x000A63CF
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x170049A7 RID: 18855
			// (set) Token: 0x06006F4F RID: 28495 RVA: 0x000A81E2 File Offset: 0x000A63E2
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x170049A8 RID: 18856
			// (set) Token: 0x06006F50 RID: 28496 RVA: 0x000A81F5 File Offset: 0x000A63F5
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x170049A9 RID: 18857
			// (set) Token: 0x06006F51 RID: 28497 RVA: 0x000A820D File Offset: 0x000A640D
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x170049AA RID: 18858
			// (set) Token: 0x06006F52 RID: 28498 RVA: 0x000A8225 File Offset: 0x000A6425
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x170049AB RID: 18859
			// (set) Token: 0x06006F53 RID: 28499 RVA: 0x000A823D File Offset: 0x000A643D
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x170049AC RID: 18860
			// (set) Token: 0x06006F54 RID: 28500 RVA: 0x000A8255 File Offset: 0x000A6455
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x170049AD RID: 18861
			// (set) Token: 0x06006F55 RID: 28501 RVA: 0x000A826D File Offset: 0x000A646D
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x170049AE RID: 18862
			// (set) Token: 0x06006F56 RID: 28502 RVA: 0x000A8285 File Offset: 0x000A6485
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x170049AF RID: 18863
			// (set) Token: 0x06006F57 RID: 28503 RVA: 0x000A829D File Offset: 0x000A649D
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x170049B0 RID: 18864
			// (set) Token: 0x06006F58 RID: 28504 RVA: 0x000A82B5 File Offset: 0x000A64B5
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x170049B1 RID: 18865
			// (set) Token: 0x06006F59 RID: 28505 RVA: 0x000A82CD File Offset: 0x000A64CD
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x170049B2 RID: 18866
			// (set) Token: 0x06006F5A RID: 28506 RVA: 0x000A82E5 File Offset: 0x000A64E5
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x170049B3 RID: 18867
			// (set) Token: 0x06006F5B RID: 28507 RVA: 0x000A82FD File Offset: 0x000A64FD
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x170049B4 RID: 18868
			// (set) Token: 0x06006F5C RID: 28508 RVA: 0x000A8315 File Offset: 0x000A6515
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x170049B5 RID: 18869
			// (set) Token: 0x06006F5D RID: 28509 RVA: 0x000A832D File Offset: 0x000A652D
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170049B6 RID: 18870
			// (set) Token: 0x06006F5E RID: 28510 RVA: 0x000A8345 File Offset: 0x000A6545
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x170049B7 RID: 18871
			// (set) Token: 0x06006F5F RID: 28511 RVA: 0x000A835D File Offset: 0x000A655D
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x170049B8 RID: 18872
			// (set) Token: 0x06006F60 RID: 28512 RVA: 0x000A8370 File Offset: 0x000A6570
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x170049B9 RID: 18873
			// (set) Token: 0x06006F61 RID: 28513 RVA: 0x000A8388 File Offset: 0x000A6588
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x170049BA RID: 18874
			// (set) Token: 0x06006F62 RID: 28514 RVA: 0x000A83A0 File Offset: 0x000A65A0
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x170049BB RID: 18875
			// (set) Token: 0x06006F63 RID: 28515 RVA: 0x000A83B8 File Offset: 0x000A65B8
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x170049BC RID: 18876
			// (set) Token: 0x06006F64 RID: 28516 RVA: 0x000A83D0 File Offset: 0x000A65D0
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x170049BD RID: 18877
			// (set) Token: 0x06006F65 RID: 28517 RVA: 0x000A83E8 File Offset: 0x000A65E8
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x170049BE RID: 18878
			// (set) Token: 0x06006F66 RID: 28518 RVA: 0x000A8400 File Offset: 0x000A6600
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x170049BF RID: 18879
			// (set) Token: 0x06006F67 RID: 28519 RVA: 0x000A8418 File Offset: 0x000A6618
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x170049C0 RID: 18880
			// (set) Token: 0x06006F68 RID: 28520 RVA: 0x000A8430 File Offset: 0x000A6630
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x170049C1 RID: 18881
			// (set) Token: 0x06006F69 RID: 28521 RVA: 0x000A8448 File Offset: 0x000A6648
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x170049C2 RID: 18882
			// (set) Token: 0x06006F6A RID: 28522 RVA: 0x000A8460 File Offset: 0x000A6660
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x170049C3 RID: 18883
			// (set) Token: 0x06006F6B RID: 28523 RVA: 0x000A8478 File Offset: 0x000A6678
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x170049C4 RID: 18884
			// (set) Token: 0x06006F6C RID: 28524 RVA: 0x000A8490 File Offset: 0x000A6690
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x170049C5 RID: 18885
			// (set) Token: 0x06006F6D RID: 28525 RVA: 0x000A84A8 File Offset: 0x000A66A8
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x170049C6 RID: 18886
			// (set) Token: 0x06006F6E RID: 28526 RVA: 0x000A84C0 File Offset: 0x000A66C0
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x170049C7 RID: 18887
			// (set) Token: 0x06006F6F RID: 28527 RVA: 0x000A84D8 File Offset: 0x000A66D8
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x170049C8 RID: 18888
			// (set) Token: 0x06006F70 RID: 28528 RVA: 0x000A84F0 File Offset: 0x000A66F0
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x170049C9 RID: 18889
			// (set) Token: 0x06006F71 RID: 28529 RVA: 0x000A8508 File Offset: 0x000A6708
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x170049CA RID: 18890
			// (set) Token: 0x06006F72 RID: 28530 RVA: 0x000A8520 File Offset: 0x000A6720
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x170049CB RID: 18891
			// (set) Token: 0x06006F73 RID: 28531 RVA: 0x000A8533 File Offset: 0x000A6733
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x170049CC RID: 18892
			// (set) Token: 0x06006F74 RID: 28532 RVA: 0x000A854B File Offset: 0x000A674B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170049CD RID: 18893
			// (set) Token: 0x06006F75 RID: 28533 RVA: 0x000A855E File Offset: 0x000A675E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170049CE RID: 18894
			// (set) Token: 0x06006F76 RID: 28534 RVA: 0x000A8571 File Offset: 0x000A6771
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170049CF RID: 18895
			// (set) Token: 0x06006F77 RID: 28535 RVA: 0x000A8589 File Offset: 0x000A6789
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170049D0 RID: 18896
			// (set) Token: 0x06006F78 RID: 28536 RVA: 0x000A85A1 File Offset: 0x000A67A1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170049D1 RID: 18897
			// (set) Token: 0x06006F79 RID: 28537 RVA: 0x000A85B9 File Offset: 0x000A67B9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170049D2 RID: 18898
			// (set) Token: 0x06006F7A RID: 28538 RVA: 0x000A85D1 File Offset: 0x000A67D1
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
