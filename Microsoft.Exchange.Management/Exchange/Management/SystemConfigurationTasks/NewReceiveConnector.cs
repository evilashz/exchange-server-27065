using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B3D RID: 2877
	[Cmdlet("New", "ReceiveConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Custom")]
	public sealed class NewReceiveConnector : NewSystemConfigurationObjectTask<ReceiveConnector>
	{
		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x060067D4 RID: 26580 RVA: 0x001AD4E9 File Offset: 0x001AB6E9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewReceiveConnector(base.Name, base.FormatMultiValuedProperty(this.Bindings), base.FormatMultiValuedProperty(this.RemoteIPRanges));
			}
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x001AD510 File Offset: 0x001AB710
		internal static NewReceiveConnector.UsageType[] TransportUsage()
		{
			return new NewReceiveConnector.UsageType[]
			{
				NewReceiveConnector.UsageType.Custom,
				NewReceiveConnector.UsageType.Internet,
				NewReceiveConnector.UsageType.Internal,
				NewReceiveConnector.UsageType.Client,
				NewReceiveConnector.UsageType.Partner
			};
		}

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x060067D6 RID: 26582 RVA: 0x001AD539 File Offset: 0x001AB739
		// (set) Token: 0x060067D7 RID: 26583 RVA: 0x001AD550 File Offset: 0x001AB750
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x060067D8 RID: 26584 RVA: 0x001AD563 File Offset: 0x001AB763
		// (set) Token: 0x060067D9 RID: 26585 RVA: 0x001AD573 File Offset: 0x001AB773
		[Parameter(ParameterSetName = "Internet", Mandatory = true)]
		public SwitchParameter Internet
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewReceiveConnector.UsageType.Internet);
			}
			set
			{
				this.Usage = NewReceiveConnector.UsageType.Internet;
			}
		}

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x060067DA RID: 26586 RVA: 0x001AD57C File Offset: 0x001AB77C
		// (set) Token: 0x060067DB RID: 26587 RVA: 0x001AD58C File Offset: 0x001AB78C
		[Parameter(ParameterSetName = "Internal", Mandatory = true)]
		public SwitchParameter Internal
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewReceiveConnector.UsageType.Internal);
			}
			set
			{
				this.Usage = NewReceiveConnector.UsageType.Internal;
			}
		}

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x060067DC RID: 26588 RVA: 0x001AD595 File Offset: 0x001AB795
		// (set) Token: 0x060067DD RID: 26589 RVA: 0x001AD5A5 File Offset: 0x001AB7A5
		[Parameter(ParameterSetName = "Client", Mandatory = true)]
		public SwitchParameter Client
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewReceiveConnector.UsageType.Client);
			}
			set
			{
				this.Usage = NewReceiveConnector.UsageType.Client;
			}
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x060067DE RID: 26590 RVA: 0x001AD5AE File Offset: 0x001AB7AE
		// (set) Token: 0x060067DF RID: 26591 RVA: 0x001AD5BE File Offset: 0x001AB7BE
		[Parameter(ParameterSetName = "Partner", Mandatory = true)]
		public SwitchParameter Partner
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewReceiveConnector.UsageType.Partner);
			}
			set
			{
				this.Usage = NewReceiveConnector.UsageType.Partner;
			}
		}

		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x060067E0 RID: 26592 RVA: 0x001AD5C7 File Offset: 0x001AB7C7
		// (set) Token: 0x060067E1 RID: 26593 RVA: 0x001AD5D7 File Offset: 0x001AB7D7
		[Parameter(ParameterSetName = "Custom", Mandatory = false)]
		public SwitchParameter Custom
		{
			internal get
			{
				return new SwitchParameter(this.Usage == NewReceiveConnector.UsageType.Custom);
			}
			set
			{
				this.Usage = NewReceiveConnector.UsageType.Custom;
			}
		}

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x060067E2 RID: 26594 RVA: 0x001AD5E0 File Offset: 0x001AB7E0
		// (set) Token: 0x060067E3 RID: 26595 RVA: 0x001AD5E8 File Offset: 0x001AB7E8
		[Parameter(ParameterSetName = "UsageType", Mandatory = true)]
		public NewReceiveConnector.UsageType Usage
		{
			internal get
			{
				return this.usage;
			}
			set
			{
				this.usage = value;
				this.isUsageSet = true;
			}
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x060067E4 RID: 26596 RVA: 0x001AD5F8 File Offset: 0x001AB7F8
		// (set) Token: 0x060067E5 RID: 26597 RVA: 0x001AD605 File Offset: 0x001AB805
		[Parameter(Mandatory = false)]
		public AuthMechanisms AuthMechanism
		{
			get
			{
				return this.DataObject.AuthMechanism;
			}
			set
			{
				this.DataObject.AuthMechanism = value;
			}
		}

		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x060067E6 RID: 26598 RVA: 0x001AD613 File Offset: 0x001AB813
		// (set) Token: 0x060067E7 RID: 26599 RVA: 0x001AD620 File Offset: 0x001AB820
		[Parameter(Mandatory = false)]
		[Parameter(ParameterSetName = "UsageType", Mandatory = false)]
		[Parameter(ParameterSetName = "Internet", Mandatory = true)]
		[Parameter(ParameterSetName = "Custom", Mandatory = true)]
		[Parameter(ParameterSetName = "Partner", Mandatory = true)]
		public MultiValuedProperty<IPBinding> Bindings
		{
			get
			{
				return this.DataObject.Bindings;
			}
			set
			{
				this.DataObject.Bindings = value;
			}
		}

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x060067E8 RID: 26600 RVA: 0x001AD62E File Offset: 0x001AB82E
		// (set) Token: 0x060067E9 RID: 26601 RVA: 0x001AD63B File Offset: 0x001AB83B
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

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x060067EA RID: 26602 RVA: 0x001AD649 File Offset: 0x001AB849
		// (set) Token: 0x060067EB RID: 26603 RVA: 0x001AD656 File Offset: 0x001AB856
		[Parameter(Mandatory = false)]
		public bool RequireEHLODomain
		{
			get
			{
				return this.DataObject.RequireEHLODomain;
			}
			set
			{
				this.DataObject.RequireEHLODomain = value;
			}
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x060067EC RID: 26604 RVA: 0x001AD664 File Offset: 0x001AB864
		// (set) Token: 0x060067ED RID: 26605 RVA: 0x001AD671 File Offset: 0x001AB871
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

		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x060067EE RID: 26606 RVA: 0x001AD67F File Offset: 0x001AB87F
		// (set) Token: 0x060067EF RID: 26607 RVA: 0x001AD68C File Offset: 0x001AB88C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectionTimeout
		{
			get
			{
				return this.DataObject.ConnectionTimeout;
			}
			set
			{
				this.DataObject.ConnectionTimeout = value;
			}
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x060067F0 RID: 26608 RVA: 0x001AD69A File Offset: 0x001AB89A
		// (set) Token: 0x060067F1 RID: 26609 RVA: 0x001AD6A7 File Offset: 0x001AB8A7
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectionInactivityTimeout
		{
			get
			{
				return this.DataObject.ConnectionInactivityTimeout;
			}
			set
			{
				this.DataObject.ConnectionInactivityTimeout = value;
			}
		}

		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x060067F2 RID: 26610 RVA: 0x001AD6B5 File Offset: 0x001AB8B5
		// (set) Token: 0x060067F3 RID: 26611 RVA: 0x001AD6CC File Offset: 0x001AB8CC
		[Parameter(Mandatory = false)]
		public AcceptedDomainIdParameter DefaultDomain
		{
			get
			{
				return (AcceptedDomainIdParameter)base.Fields["DefaultDomain"];
			}
			set
			{
				base.Fields["DefaultDomain"] = value;
			}
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x060067F4 RID: 26612 RVA: 0x001AD6DF File Offset: 0x001AB8DF
		// (set) Token: 0x060067F5 RID: 26613 RVA: 0x001AD6EC File Offset: 0x001AB8EC
		[Parameter(Mandatory = false)]
		public Fqdn Fqdn
		{
			get
			{
				return this.DataObject.Fqdn;
			}
			set
			{
				this.DataObject.Fqdn = value;
			}
		}

		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x060067F6 RID: 26614 RVA: 0x001AD6FA File Offset: 0x001AB8FA
		// (set) Token: 0x060067F7 RID: 26615 RVA: 0x001AD707 File Offset: 0x001AB907
		[Parameter(Mandatory = false)]
		public Fqdn ServiceDiscoveryFqdn
		{
			get
			{
				return this.DataObject.ServiceDiscoveryFqdn;
			}
			set
			{
				this.DataObject.ServiceDiscoveryFqdn = value;
			}
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x060067F8 RID: 26616 RVA: 0x001AD715 File Offset: 0x001AB915
		// (set) Token: 0x060067F9 RID: 26617 RVA: 0x001AD722 File Offset: 0x001AB922
		[Parameter(Mandatory = false)]
		public SmtpX509Identifier TlsCertificateName
		{
			get
			{
				return this.DataObject.TlsCertificateName;
			}
			set
			{
				this.DataObject.TlsCertificateName = value;
			}
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x060067FA RID: 26618 RVA: 0x001AD730 File Offset: 0x001AB930
		// (set) Token: 0x060067FB RID: 26619 RVA: 0x001AD73D File Offset: 0x001AB93D
		[Parameter(Mandatory = false)]
		public Unlimited<int> MessageRateLimit
		{
			get
			{
				return this.DataObject.MessageRateLimit;
			}
			set
			{
				this.DataObject.MessageRateLimit = value;
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x060067FC RID: 26620 RVA: 0x001AD74B File Offset: 0x001AB94B
		// (set) Token: 0x060067FD RID: 26621 RVA: 0x001AD758 File Offset: 0x001AB958
		[Parameter(Mandatory = false)]
		public MessageRateSourceFlags MessageRateSource
		{
			get
			{
				return this.DataObject.MessageRateSource;
			}
			set
			{
				this.DataObject.MessageRateSource = value;
			}
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x060067FE RID: 26622 RVA: 0x001AD766 File Offset: 0x001AB966
		// (set) Token: 0x060067FF RID: 26623 RVA: 0x001AD773 File Offset: 0x001AB973
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxInboundConnection
		{
			get
			{
				return this.DataObject.MaxInboundConnection;
			}
			set
			{
				this.DataObject.MaxInboundConnection = value;
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x06006800 RID: 26624 RVA: 0x001AD781 File Offset: 0x001AB981
		// (set) Token: 0x06006801 RID: 26625 RVA: 0x001AD78E File Offset: 0x001AB98E
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxInboundConnectionPerSource
		{
			get
			{
				return this.DataObject.MaxInboundConnectionPerSource;
			}
			set
			{
				this.DataObject.MaxInboundConnectionPerSource = value;
			}
		}

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x06006802 RID: 26626 RVA: 0x001AD79C File Offset: 0x001AB99C
		// (set) Token: 0x06006803 RID: 26627 RVA: 0x001AD7A9 File Offset: 0x001AB9A9
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MaxHeaderSize
		{
			get
			{
				return this.DataObject.MaxHeaderSize;
			}
			set
			{
				this.DataObject.MaxHeaderSize = value;
			}
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x06006804 RID: 26628 RVA: 0x001AD7B7 File Offset: 0x001AB9B7
		// (set) Token: 0x06006805 RID: 26629 RVA: 0x001AD7C4 File Offset: 0x001AB9C4
		[Parameter(Mandatory = false)]
		public int MaxHopCount
		{
			get
			{
				return this.DataObject.MaxHopCount;
			}
			set
			{
				this.DataObject.MaxHopCount = value;
			}
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x06006806 RID: 26630 RVA: 0x001AD7D2 File Offset: 0x001AB9D2
		// (set) Token: 0x06006807 RID: 26631 RVA: 0x001AD7DF File Offset: 0x001AB9DF
		[Parameter(Mandatory = false)]
		public int MaxLocalHopCount
		{
			get
			{
				return this.DataObject.MaxLocalHopCount;
			}
			set
			{
				this.DataObject.MaxLocalHopCount = value;
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x06006808 RID: 26632 RVA: 0x001AD7ED File Offset: 0x001AB9ED
		// (set) Token: 0x06006809 RID: 26633 RVA: 0x001AD7FA File Offset: 0x001AB9FA
		[Parameter(Mandatory = false)]
		public int MaxLogonFailures
		{
			get
			{
				return this.DataObject.MaxLogonFailures;
			}
			set
			{
				this.DataObject.MaxLogonFailures = value;
			}
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x0600680A RID: 26634 RVA: 0x001AD808 File Offset: 0x001ABA08
		// (set) Token: 0x0600680B RID: 26635 RVA: 0x001AD815 File Offset: 0x001ABA15
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MaxMessageSize
		{
			get
			{
				return this.DataObject.MaxMessageSize;
			}
			set
			{
				this.DataObject.MaxMessageSize = value;
			}
		}

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x0600680C RID: 26636 RVA: 0x001AD823 File Offset: 0x001ABA23
		// (set) Token: 0x0600680D RID: 26637 RVA: 0x001AD830 File Offset: 0x001ABA30
		[Parameter(Mandatory = false)]
		public int MaxInboundConnectionPercentagePerSource
		{
			get
			{
				return this.DataObject.MaxInboundConnectionPercentagePerSource;
			}
			set
			{
				this.DataObject.MaxInboundConnectionPercentagePerSource = value;
			}
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x0600680E RID: 26638 RVA: 0x001AD83E File Offset: 0x001ABA3E
		// (set) Token: 0x0600680F RID: 26639 RVA: 0x001AD84B File Offset: 0x001ABA4B
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxProtocolErrors
		{
			get
			{
				return this.DataObject.MaxProtocolErrors;
			}
			set
			{
				this.DataObject.MaxProtocolErrors = value;
			}
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x06006810 RID: 26640 RVA: 0x001AD859 File Offset: 0x001ABA59
		// (set) Token: 0x06006811 RID: 26641 RVA: 0x001AD866 File Offset: 0x001ABA66
		[Parameter(Mandatory = false)]
		public int MaxRecipientsPerMessage
		{
			get
			{
				return this.DataObject.MaxRecipientsPerMessage;
			}
			set
			{
				this.DataObject.MaxRecipientsPerMessage = value;
			}
		}

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x06006812 RID: 26642 RVA: 0x001AD874 File Offset: 0x001ABA74
		// (set) Token: 0x06006813 RID: 26643 RVA: 0x001AD881 File Offset: 0x001ABA81
		[Parameter(Mandatory = false)]
		public PermissionGroups PermissionGroups
		{
			get
			{
				return this.DataObject.PermissionGroups;
			}
			set
			{
				this.DataObject.PermissionGroups = value;
			}
		}

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x06006814 RID: 26644 RVA: 0x001AD88F File Offset: 0x001ABA8F
		// (set) Token: 0x06006815 RID: 26645 RVA: 0x001AD89C File Offset: 0x001ABA9C
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel ProtocolLoggingLevel
		{
			get
			{
				return this.DataObject.ProtocolLoggingLevel;
			}
			set
			{
				this.DataObject.ProtocolLoggingLevel = value;
			}
		}

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x06006816 RID: 26646 RVA: 0x001AD8AA File Offset: 0x001ABAAA
		// (set) Token: 0x06006817 RID: 26647 RVA: 0x001AD8B7 File Offset: 0x001ABAB7
		[Parameter(ParameterSetName = "Client", Mandatory = true)]
		[Parameter(ParameterSetName = "UsageType", Mandatory = false)]
		[Parameter(Mandatory = false)]
		[Parameter(ParameterSetName = "Internal", Mandatory = true)]
		[Parameter(ParameterSetName = "Custom", Mandatory = true)]
		[Parameter(ParameterSetName = "Partner", Mandatory = true)]
		public MultiValuedProperty<IPRange> RemoteIPRanges
		{
			get
			{
				return this.DataObject.RemoteIPRanges;
			}
			set
			{
				this.DataObject.RemoteIPRanges = value;
			}
		}

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x06006818 RID: 26648 RVA: 0x001AD8C5 File Offset: 0x001ABAC5
		// (set) Token: 0x06006819 RID: 26649 RVA: 0x001AD8D2 File Offset: 0x001ABAD2
		[Parameter(Mandatory = false)]
		public bool EightBitMimeEnabled
		{
			get
			{
				return this.DataObject.EightBitMimeEnabled;
			}
			set
			{
				this.DataObject.EightBitMimeEnabled = value;
			}
		}

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x0600681A RID: 26650 RVA: 0x001AD8E0 File Offset: 0x001ABAE0
		// (set) Token: 0x0600681B RID: 26651 RVA: 0x001AD8ED File Offset: 0x001ABAED
		[Parameter(Mandatory = false)]
		public string Banner
		{
			get
			{
				return this.DataObject.Banner;
			}
			set
			{
				this.DataObject.Banner = value;
			}
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x0600681C RID: 26652 RVA: 0x001AD8FB File Offset: 0x001ABAFB
		// (set) Token: 0x0600681D RID: 26653 RVA: 0x001AD908 File Offset: 0x001ABB08
		[Parameter(Mandatory = false)]
		public bool BinaryMimeEnabled
		{
			get
			{
				return this.DataObject.BinaryMimeEnabled;
			}
			set
			{
				this.DataObject.BinaryMimeEnabled = value;
			}
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x0600681E RID: 26654 RVA: 0x001AD916 File Offset: 0x001ABB16
		// (set) Token: 0x0600681F RID: 26655 RVA: 0x001AD923 File Offset: 0x001ABB23
		[Parameter(Mandatory = false)]
		public bool ChunkingEnabled
		{
			get
			{
				return this.DataObject.ChunkingEnabled;
			}
			set
			{
				this.DataObject.ChunkingEnabled = value;
			}
		}

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x06006820 RID: 26656 RVA: 0x001AD931 File Offset: 0x001ABB31
		// (set) Token: 0x06006821 RID: 26657 RVA: 0x001AD93E File Offset: 0x001ABB3E
		[Parameter(Mandatory = false)]
		public bool DeliveryStatusNotificationEnabled
		{
			get
			{
				return this.DataObject.DeliveryStatusNotificationEnabled;
			}
			set
			{
				this.DataObject.DeliveryStatusNotificationEnabled = value;
			}
		}

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x06006822 RID: 26658 RVA: 0x001AD94C File Offset: 0x001ABB4C
		// (set) Token: 0x06006823 RID: 26659 RVA: 0x001AD959 File Offset: 0x001ABB59
		[Parameter(Mandatory = false)]
		public bool EnhancedStatusCodesEnabled
		{
			get
			{
				return this.DataObject.EnhancedStatusCodesEnabled;
			}
			set
			{
				this.DataObject.EnhancedStatusCodesEnabled = value;
			}
		}

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x06006824 RID: 26660 RVA: 0x001AD967 File Offset: 0x001ABB67
		// (set) Token: 0x06006825 RID: 26661 RVA: 0x001AD974 File Offset: 0x001ABB74
		[Parameter(Mandatory = false)]
		public SizeMode SizeEnabled
		{
			get
			{
				return this.DataObject.SizeEnabled;
			}
			set
			{
				this.DataObject.SizeEnabled = value;
			}
		}

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x06006826 RID: 26662 RVA: 0x001AD982 File Offset: 0x001ABB82
		// (set) Token: 0x06006827 RID: 26663 RVA: 0x001AD98F File Offset: 0x001ABB8F
		[Parameter(Mandatory = false)]
		public bool PipeliningEnabled
		{
			get
			{
				return this.DataObject.PipeliningEnabled;
			}
			set
			{
				this.DataObject.PipeliningEnabled = value;
			}
		}

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x06006828 RID: 26664 RVA: 0x001AD99D File Offset: 0x001ABB9D
		// (set) Token: 0x06006829 RID: 26665 RVA: 0x001AD9AA File Offset: 0x001ABBAA
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TarpitInterval
		{
			get
			{
				return this.DataObject.TarpitInterval;
			}
			set
			{
				this.DataObject.TarpitInterval = value;
			}
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x0600682A RID: 26666 RVA: 0x001AD9B8 File Offset: 0x001ABBB8
		// (set) Token: 0x0600682B RID: 26667 RVA: 0x001AD9C5 File Offset: 0x001ABBC5
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MaxAcknowledgementDelay
		{
			get
			{
				return this.DataObject.MaxAcknowledgementDelay;
			}
			set
			{
				this.DataObject.MaxAcknowledgementDelay = value;
			}
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x0600682C RID: 26668 RVA: 0x001AD9D3 File Offset: 0x001ABBD3
		// (set) Token: 0x0600682D RID: 26669 RVA: 0x001AD9E0 File Offset: 0x001ABBE0
		[Parameter(Mandatory = false)]
		public bool RequireTLS
		{
			get
			{
				return this.DataObject.RequireTLS;
			}
			set
			{
				this.DataObject.RequireTLS = value;
			}
		}

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x0600682E RID: 26670 RVA: 0x001AD9EE File Offset: 0x001ABBEE
		// (set) Token: 0x0600682F RID: 26671 RVA: 0x001AD9FB File Offset: 0x001ABBFB
		[Parameter(Mandatory = false)]
		public bool EnableAuthGSSAPI
		{
			get
			{
				return this.DataObject.EnableAuthGSSAPI;
			}
			set
			{
				this.DataObject.EnableAuthGSSAPI = value;
			}
		}

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x06006830 RID: 26672 RVA: 0x001ADA09 File Offset: 0x001ABC09
		// (set) Token: 0x06006831 RID: 26673 RVA: 0x001ADA16 File Offset: 0x001ABC16
		[Parameter(Mandatory = false)]
		public ExtendedProtectionPolicySetting ExtendedProtectionPolicy
		{
			get
			{
				return this.DataObject.ExtendedProtectionPolicy;
			}
			set
			{
				this.DataObject.ExtendedProtectionPolicy = value;
			}
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x06006832 RID: 26674 RVA: 0x001ADA24 File Offset: 0x001ABC24
		// (set) Token: 0x06006833 RID: 26675 RVA: 0x001ADA31 File Offset: 0x001ABC31
		[Parameter(Mandatory = false)]
		public bool LiveCredentialEnabled
		{
			get
			{
				return this.DataObject.LiveCredentialEnabled;
			}
			set
			{
				this.DataObject.LiveCredentialEnabled = value;
			}
		}

		// Token: 0x1700201A RID: 8218
		// (get) Token: 0x06006834 RID: 26676 RVA: 0x001ADA3F File Offset: 0x001ABC3F
		// (set) Token: 0x06006835 RID: 26677 RVA: 0x001ADA4C File Offset: 0x001ABC4C
		[Parameter(Mandatory = false)]
		public bool DomainSecureEnabled
		{
			get
			{
				return this.DataObject.DomainSecureEnabled;
			}
			set
			{
				this.DataObject.DomainSecureEnabled = value;
			}
		}

		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x06006836 RID: 26678 RVA: 0x001ADA5A File Offset: 0x001ABC5A
		// (set) Token: 0x06006837 RID: 26679 RVA: 0x001ADA67 File Offset: 0x001ABC67
		[Parameter(Mandatory = false)]
		public bool LongAddressesEnabled
		{
			get
			{
				return this.DataObject.LongAddressesEnabled;
			}
			set
			{
				this.DataObject.LongAddressesEnabled = value;
			}
		}

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x06006838 RID: 26680 RVA: 0x001ADA75 File Offset: 0x001ABC75
		// (set) Token: 0x06006839 RID: 26681 RVA: 0x001ADA82 File Offset: 0x001ABC82
		[Parameter(Mandatory = false)]
		public bool OrarEnabled
		{
			get
			{
				return this.DataObject.OrarEnabled;
			}
			set
			{
				this.DataObject.OrarEnabled = value;
			}
		}

		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x0600683A RID: 26682 RVA: 0x001ADA90 File Offset: 0x001ABC90
		// (set) Token: 0x0600683B RID: 26683 RVA: 0x001ADA9D File Offset: 0x001ABC9D
		[Parameter(Mandatory = false)]
		public bool SuppressXAnonymousTls
		{
			get
			{
				return this.DataObject.SuppressXAnonymousTls;
			}
			set
			{
				this.DataObject.SuppressXAnonymousTls = value;
			}
		}

		// Token: 0x1700201E RID: 8222
		// (get) Token: 0x0600683C RID: 26684 RVA: 0x001ADAAB File Offset: 0x001ABCAB
		// (set) Token: 0x0600683D RID: 26685 RVA: 0x001ADAB8 File Offset: 0x001ABCB8
		[Parameter(Mandatory = false)]
		public bool AdvertiseClientSettings
		{
			get
			{
				return this.DataObject.AdvertiseClientSettings;
			}
			set
			{
				this.DataObject.AdvertiseClientSettings = value;
			}
		}

		// Token: 0x1700201F RID: 8223
		// (get) Token: 0x0600683E RID: 26686 RVA: 0x001ADAC6 File Offset: 0x001ABCC6
		// (set) Token: 0x0600683F RID: 26687 RVA: 0x001ADAD3 File Offset: 0x001ABCD3
		[Parameter(Mandatory = false)]
		public bool ProxyEnabled
		{
			get
			{
				return this.DataObject.ProxyEnabled;
			}
			set
			{
				this.DataObject.ProxyEnabled = value;
			}
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x06006840 RID: 26688 RVA: 0x001ADAE1 File Offset: 0x001ABCE1
		// (set) Token: 0x06006841 RID: 26689 RVA: 0x001ADAEE File Offset: 0x001ABCEE
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
		{
			get
			{
				return this.DataObject.TlsDomainCapabilities;
			}
			set
			{
				this.DataObject.TlsDomainCapabilities = value;
			}
		}

		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x06006842 RID: 26690 RVA: 0x001ADAFC File Offset: 0x001ABCFC
		// (set) Token: 0x06006843 RID: 26691 RVA: 0x001ADB09 File Offset: 0x001ABD09
		[Parameter(Mandatory = false)]
		public ServerRole TransportRole
		{
			get
			{
				return this.DataObject.TransportRole;
			}
			set
			{
				this.DataObject.TransportRole = value;
			}
		}

		// Token: 0x06006844 RID: 26692 RVA: 0x001ADB18 File Offset: 0x001ABD18
		protected override void WriteResult(IConfigurable dataObject)
		{
			ReceiveConnector receiveConnector = dataObject as ReceiveConnector;
			if (receiveConnector != null && !receiveConnector.IsReadOnly)
			{
				receiveConnector.PermissionGroups = this.DataObject.PermissionGroups;
			}
			receiveConnector.ResetChangeTracking();
			base.WriteResult(dataObject);
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x001ADB58 File Offset: 0x001ABD58
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				base.GetType().FullName
			});
			this.CheckServerAndSetReceiveConnectorID();
			this.CheckParameters();
			this.InitializeDefaults();
			this.CalculateProperties();
			base.InternalValidate();
			if (this.AdvertiseClientSettings && (this.PermissionGroups & PermissionGroups.ExchangeUsers) != PermissionGroups.ExchangeUsers)
			{
				base.WriteError(new AdvertiseClientSettingsWithoutExchangeUsersPermissionGroupsException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (base.HasErrors)
			{
				return;
			}
			LocalizedException exception;
			if (!ReceiveConnectorNoMappingConflictCondition.Verify(this.DataObject, base.DataSession as IConfigurationSession, out exception))
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x001ADBFC File Offset: 0x001ABDFC
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			ReceiveConnector receiveConnector = null;
			try
			{
				receiveConnector = configurationSession.Read<ReceiveConnector>(this.DataObject.OriginalId);
				base.WriteVerbose(Strings.NewReceiveConnectorAddingPermissionsMsg);
				receiveConnector.PermissionGroups = this.DataObject.PermissionGroups;
				try
				{
					receiveConnector.SaveNewSecurityDescriptor(this.serverObject);
				}
				catch (OverflowException ex)
				{
					base.WriteDebug(ex.ToString());
					throw new ReceiveConnectorAclOverflowException(ex.Message);
				}
				base.WriteVerbose(Strings.NewReceiveConnectorAddingPermissionsDoneMsg);
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, receiveConnector);
			}
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x001ADCA8 File Offset: 0x001ABEA8
		internal static bool ValidataName(string connectorName, out string exceptionString)
		{
			if (!string.IsNullOrEmpty(connectorName) && connectorName.Contains("\\"))
			{
				exceptionString = Strings.ErrorInvalidCharactersInParameterValue("Name", connectorName, string.Format("{0}{1}{2}", "{ '", "\\", "' }"));
				return false;
			}
			exceptionString = null;
			return true;
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x001ADCFC File Offset: 0x001ABEFC
		private void CheckServerAndSetReceiveConnectorID()
		{
			string message;
			if (!NewReceiveConnector.ValidataName(this.DataObject.Name, out message))
			{
				base.WriteError(new ArgumentException(message), ErrorCategory.InvalidArgument, null);
			}
			if (this.Server == null)
			{
				try
				{
					this.serverObject = ((ITopologyConfigurationSession)base.DataSession).FindLocalServer();
					goto IL_AD;
				}
				catch (LocalServerNotFoundException)
				{
					base.WriteError(new NeedToSpecifyServerObjectException(), ErrorCategory.InvalidOperation, this.DataObject);
					goto IL_AD;
				}
			}
			this.serverObject = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			if (this.serverObject == null)
			{
				return;
			}
			IL_AD:
			this.isEdgeRole = this.serverObject.IsEdgeServer;
			if (!this.isEdgeRole && !this.serverObject.IsHubTransportServer && !this.serverObject.IsFrontendTransportServer && !this.serverObject.IsMailboxServer)
			{
				base.WriteError(new ServerNotHubOrEdgeException(), ErrorCategory.InvalidOperation, this.serverObject);
			}
			ADObjectId id = this.serverObject.Id;
			ADObjectId childId = id.GetChildId("Protocols").GetChildId("SMTP Receive Connectors").GetChildId(this.DataObject.Name);
			this.DataObject.SetId(childId);
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x001ADE58 File Offset: 0x001AC058
		private void CalculateProperties()
		{
			AcceptedDomainIdParameter defaultDomain = this.DefaultDomain;
			if (defaultDomain != null)
			{
				AcceptedDomain acceptedDomain = (AcceptedDomain)base.GetDataObject<AcceptedDomain>(defaultDomain, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorDefaultDomainNotFound(defaultDomain)), new LocalizedString?(Strings.ErrorDefaultDomainNotUnique(defaultDomain)));
				this.DataObject.DefaultDomain = acceptedDomain.Id;
			}
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x001ADEB0 File Offset: 0x001AC0B0
		private void CheckParameters()
		{
			if (this.usage == NewReceiveConnector.UsageType.Internet && !this.DataObject.IsModified(ReceiveConnectorSchema.Bindings))
			{
				base.WriteError(new ParameterErrorForInternetUsageException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			else if (this.usage == NewReceiveConnector.UsageType.Internal && !this.DataObject.IsModified(ReceiveConnectorSchema.RemoteIPRanges))
			{
				base.WriteError(new ParameterErrorForInternalUsageException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			else if (this.usage == NewReceiveConnector.UsageType.Custom && (!this.DataObject.IsModified(ReceiveConnectorSchema.RemoteIPRanges) || !this.DataObject.IsModified(ReceiveConnectorSchema.Bindings)))
			{
				base.WriteError(new ParameterErrorForDefaultUsageException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.PermissionGroups))
			{
				if (this.isEdgeRole && (this.DataObject.PermissionGroups & PermissionGroups.ExchangeLegacyServers) != PermissionGroups.None)
				{
					base.WriteError(new UnSupportedPermissionGroupsForEdgeException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
				if ((this.DataObject.PermissionGroups & PermissionGroups.Custom) != PermissionGroups.None)
				{
					base.WriteError(new CustomCannotBeSetForPermissionGroupsException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.ChunkingEnabled) && !this.DataObject.ChunkingEnabled && this.DataObject.BinaryMimeEnabled)
			{
				base.WriteError(new ChunkingEnabledSettingConflictException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.LongAddressesEnabled) && this.DataObject.LongAddressesEnabled && this.isEdgeRole)
			{
				base.WriteError(new LongAddressesEnabledOnEdgeException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.SuppressXAnonymousTls))
			{
				if (this.DataObject.SuppressXAnonymousTls && this.serverObject.IsEdgeServer)
				{
					base.WriteError(new SuppressXAnonymousTlsEnabledOnEdgeException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
				if (this.DataObject.SuppressXAnonymousTls && !this.serverObject.UseDowngradedExchangeServerAuth)
				{
					base.WriteError(new SuppressXAnonymousTlsEnabledWithoutDowngradedExchangeAuthException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			if (this.DataObject.IsModified(ReceiveConnectorSchema.TransportRole) && (this.DataObject.TransportRole & (ServerRole.HubTransport | ServerRole.Edge | ServerRole.FrontendTransport)) == ServerRole.None)
			{
				base.WriteError(new InvalidTransportRoleOnReceiveConnectorException(), ErrorCategory.InvalidData, this.DataObject);
			}
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x001AE0D0 File Offset: 0x001AC2D0
		private void InitializeDefaults()
		{
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.MaxInboundConnection))
			{
				this.MaxInboundConnection = 5000;
			}
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.MaxInboundConnectionPerSource))
			{
				this.MaxInboundConnectionPerSource = 20;
			}
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.MaxProtocolErrors))
			{
				this.MaxProtocolErrors = 5;
			}
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.Fqdn))
			{
				string fqdn = this.serverObject.Fqdn;
				SmtpDomain smtpDomain;
				if (SmtpDomain.TryParse(fqdn, out smtpDomain))
				{
					this.Fqdn = new Fqdn(fqdn);
				}
				else if (SmtpDomain.TryParse(this.serverObject.Name, out smtpDomain))
				{
					this.Fqdn = new Fqdn(this.serverObject.Name);
				}
				else
				{
					base.WriteError(new InvalidFqdnException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.TransportRole))
			{
				if ((this.serverObject.CurrentServerRole & ServerRole.Edge) != ServerRole.None)
				{
					this.TransportRole = ServerRole.HubTransport;
				}
				else if ((this.serverObject.CurrentServerRole & ServerRole.HubTransport) != ServerRole.None)
				{
					this.TransportRole = ServerRole.HubTransport;
				}
				else if ((this.serverObject.CurrentServerRole & ServerRole.FrontendTransport) != ServerRole.None)
				{
					this.TransportRole = ServerRole.FrontendTransport;
				}
				else
				{
					this.TransportRole = ServerRole.HubTransport;
				}
			}
			if (this.isEdgeRole)
			{
				if (!this.DataObject.IsModified(ReceiveConnectorSchema.ConnectionTimeout))
				{
					this.DataObject.ConnectionTimeout = EnhancedTimeSpan.FromMinutes(5.0);
				}
				if (!this.DataObject.IsModified(ReceiveConnectorSchema.ConnectionInactivityTimeout))
				{
					this.DataObject.ConnectionInactivityTimeout = EnhancedTimeSpan.OneMinute;
				}
				if (!this.DataObject.IsModified(ReceiveConnectorSchema.MessageRateLimit))
				{
					this.DataObject.MessageRateLimit = 600;
				}
				if (!this.DataObject.IsModified(ReceiveConnectorSchema.MessageRateSource))
				{
					this.DataObject.MessageRateSource = MessageRateSourceFlags.IPAddress;
				}
			}
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.PermissionGroups))
			{
				this.SetPermissionGroups();
			}
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.SecurityFlags))
			{
				this.SetAuthMechanism();
			}
			if (this.isUsageSet && this.usage == NewReceiveConnector.UsageType.Internal)
			{
				this.SetUsageInternalProperties();
			}
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.Bindings) && this.usage == NewReceiveConnector.UsageType.Client)
			{
				this.DataObject.Bindings[0].Port = 587;
			}
			LocalizedException exception;
			if (!this.isEdgeRole && (this.AuthMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None && !ReceiveConnectorFqdnCondition.Verify(this.DataObject, this.serverObject, out exception))
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.DataObject);
			}
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x001AE370 File Offset: 0x001AC570
		private void SetPermissionGroups()
		{
			if (this.isUsageSet)
			{
				switch (this.usage)
				{
				case NewReceiveConnector.UsageType.Custom:
					this.DataObject.PermissionGroups = PermissionGroups.None;
					return;
				case NewReceiveConnector.UsageType.Internet:
					this.DataObject.PermissionGroups = PermissionGroups.AnonymousUsers;
					return;
				case NewReceiveConnector.UsageType.Internal:
					this.DataObject.PermissionGroups = (PermissionGroups.ExchangeServers | PermissionGroups.ExchangeLegacyServers);
					return;
				case NewReceiveConnector.UsageType.Client:
					this.DataObject.PermissionGroups = PermissionGroups.ExchangeUsers;
					return;
				case NewReceiveConnector.UsageType.Partner:
					this.DataObject.PermissionGroups = PermissionGroups.Partners;
					return;
				default:
					return;
				}
			}
			else
			{
				if (this.isEdgeRole)
				{
					this.DataObject.PermissionGroups = (PermissionGroups.AnonymousUsers | PermissionGroups.ExchangeServers | PermissionGroups.Partners);
					return;
				}
				this.DataObject.PermissionGroups = (PermissionGroups.ExchangeUsers | PermissionGroups.ExchangeServers | PermissionGroups.ExchangeLegacyServers);
				return;
			}
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x001AE410 File Offset: 0x001AC610
		private void SetAuthMechanism()
		{
			if (this.isUsageSet)
			{
				switch (this.usage)
				{
				case NewReceiveConnector.UsageType.Custom:
					this.DataObject.AuthMechanism = AuthMechanisms.Tls;
					return;
				case NewReceiveConnector.UsageType.Internet:
					this.DataObject.AuthMechanism = AuthMechanisms.Tls;
					return;
				case NewReceiveConnector.UsageType.Internal:
					this.DataObject.AuthMechanism = (AuthMechanisms.Tls | AuthMechanisms.ExchangeServer);
					return;
				case NewReceiveConnector.UsageType.Client:
					this.DataObject.AuthMechanism = (AuthMechanisms.Tls | AuthMechanisms.Integrated | AuthMechanisms.BasicAuth | AuthMechanisms.BasicAuthRequireTLS);
					return;
				case NewReceiveConnector.UsageType.Partner:
					this.DataObject.AuthMechanism = AuthMechanisms.Tls;
					this.DataObject.DomainSecureEnabled = true;
					return;
				default:
					return;
				}
			}
			else
			{
				if (this.isEdgeRole)
				{
					this.DataObject.AuthMechanism = (AuthMechanisms.Tls | AuthMechanisms.ExchangeServer);
					return;
				}
				this.DataObject.AuthMechanism = (AuthMechanisms.Tls | AuthMechanisms.Integrated | AuthMechanisms.BasicAuth | AuthMechanisms.BasicAuthRequireTLS | AuthMechanisms.ExchangeServer);
				return;
			}
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x001AE4BC File Offset: 0x001AC6BC
		private void SetUsageInternalProperties()
		{
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.SizeEnabled) && !this.isEdgeRole)
			{
				this.DataObject.SizeEnabled = SizeMode.EnabledWithoutValue;
			}
			if (!this.DataObject.IsModified(ReceiveConnectorSchema.MaxInboundConnectionPercentagePerSource) && !this.isEdgeRole)
			{
				this.MaxInboundConnectionPercentagePerSource = 100;
			}
		}

		// Token: 0x04003672 RID: 13938
		private const string CustomParameterSetName = "Custom";

		// Token: 0x04003673 RID: 13939
		private const string InternetParameterSetName = "Internet";

		// Token: 0x04003674 RID: 13940
		private const string InternalParameterSetName = "Internal";

		// Token: 0x04003675 RID: 13941
		private const string ClientParameterSetName = "Client";

		// Token: 0x04003676 RID: 13942
		private const string PartnerParameterSetName = "Partner";

		// Token: 0x04003677 RID: 13943
		private const string UsageTypeParameterSetName = "UsageType";

		// Token: 0x04003678 RID: 13944
		private const string CommonNameSeperatorChar = "\\";

		// Token: 0x04003679 RID: 13945
		private const string protocolsContainer = "Protocols";

		// Token: 0x0400367A RID: 13946
		private const string smtpContainer = "SMTP Receive Connectors";

		// Token: 0x0400367B RID: 13947
		private NewReceiveConnector.UsageType usage;

		// Token: 0x0400367C RID: 13948
		private bool isUsageSet;

		// Token: 0x0400367D RID: 13949
		private bool isEdgeRole = true;

		// Token: 0x0400367E RID: 13950
		private Server serverObject;

		// Token: 0x02000B3E RID: 2878
		public enum UsageType
		{
			// Token: 0x04003680 RID: 13952
			[LocDescription(Strings.IDs.UsageTypeCustom)]
			Custom,
			// Token: 0x04003681 RID: 13953
			[LocDescription(Strings.IDs.UsageTypeInternet)]
			Internet,
			// Token: 0x04003682 RID: 13954
			[LocDescription(Strings.IDs.UsageTypeInternal)]
			Internal,
			// Token: 0x04003683 RID: 13955
			[LocDescription(Strings.IDs.UsageTypeClient)]
			Client,
			// Token: 0x04003684 RID: 13956
			[LocDescription(Strings.IDs.UsageTypePartner)]
			Partner
		}
	}
}
