using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200071F RID: 1823
	[Serializable]
	public sealed class MailboxTransportServerPresentationObject : ADPresentationObject
	{
		// Token: 0x06005625 RID: 22053 RVA: 0x00137539 File Offset: 0x00135739
		public MailboxTransportServerPresentationObject()
		{
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x00137541 File Offset: 0x00135741
		public MailboxTransportServerPresentationObject(MailboxTransportServer dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x06005627 RID: 22055 RVA: 0x0013754A File Offset: 0x0013574A
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return MailboxTransportServerPresentationObject.schema;
			}
		}

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x06005628 RID: 22056 RVA: 0x00137551 File Offset: 0x00135751
		public new string Name
		{
			get
			{
				return ((ADObjectId)this[ADObjectSchema.Id]).Parent.Parent.Name;
			}
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x06005629 RID: 22057 RVA: 0x00137572 File Offset: 0x00135772
		// (set) Token: 0x0600562A RID: 22058 RVA: 0x00137584 File Offset: 0x00135784
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[MailboxTransportServerSchema.AdminDisplayVersion];
			}
			internal set
			{
				this[MailboxTransportServerSchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x0600562B RID: 22059 RVA: 0x00137592 File Offset: 0x00135792
		// (set) Token: 0x0600562C RID: 22060 RVA: 0x001375A4 File Offset: 0x001357A4
		public ServerEditionType Edition
		{
			get
			{
				return (ServerEditionType)this[MailboxTransportServerSchema.Edition];
			}
			internal set
			{
				this[MailboxTransportServerSchema.Edition] = value;
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x0600562D RID: 22061 RVA: 0x001375B7 File Offset: 0x001357B7
		// (set) Token: 0x0600562E RID: 22062 RVA: 0x001375C9 File Offset: 0x001357C9
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[MailboxTransportServerSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[MailboxTransportServerSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x0600562F RID: 22063 RVA: 0x001375D7 File Offset: 0x001357D7
		// (set) Token: 0x06005630 RID: 22064 RVA: 0x001375E9 File Offset: 0x001357E9
		public bool IsMailboxServer
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.IsMailboxServer];
			}
			internal set
			{
				this[MailboxTransportServerSchema.IsMailboxServer] = value;
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x06005631 RID: 22065 RVA: 0x001375FC File Offset: 0x001357FC
		// (set) Token: 0x06005632 RID: 22066 RVA: 0x0013760E File Offset: 0x0013580E
		public bool IsProvisionedServer
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.IsProvisionedServer];
			}
			internal set
			{
				this[MailboxTransportServerSchema.IsProvisionedServer] = value;
			}
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x06005633 RID: 22067 RVA: 0x00137621 File Offset: 0x00135821
		// (set) Token: 0x06005634 RID: 22068 RVA: 0x00137633 File Offset: 0x00135833
		public NetworkAddressCollection NetworkAddress
		{
			get
			{
				return (NetworkAddressCollection)this[MailboxTransportServerSchema.NetworkAddress];
			}
			internal set
			{
				this[MailboxTransportServerSchema.NetworkAddress] = value;
			}
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x06005635 RID: 22069 RVA: 0x00137641 File Offset: 0x00135841
		// (set) Token: 0x06005636 RID: 22070 RVA: 0x00137653 File Offset: 0x00135853
		public int VersionNumber
		{
			get
			{
				return (int)this[MailboxTransportServerSchema.VersionNumber];
			}
			internal set
			{
				this[MailboxTransportServerSchema.VersionNumber] = value;
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x06005637 RID: 22071 RVA: 0x00137666 File Offset: 0x00135866
		// (set) Token: 0x06005638 RID: 22072 RVA: 0x00137678 File Offset: 0x00135878
		[Parameter(Mandatory = false)]
		public bool ConnectivityLogEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.ConnectivityLogEnabled];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogEnabled] = value;
			}
		}

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x06005639 RID: 22073 RVA: 0x0013768B File Offset: 0x0013588B
		// (set) Token: 0x0600563A RID: 22074 RVA: 0x0013769D File Offset: 0x0013589D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectivityLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.ConnectivityLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogMaxAge] = value;
			}
		}

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x0600563B RID: 22075 RVA: 0x001376B0 File Offset: 0x001358B0
		// (set) Token: 0x0600563C RID: 22076 RVA: 0x001376C2 File Offset: 0x001358C2
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.ConnectivityLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x0600563D RID: 22077 RVA: 0x001376D5 File Offset: 0x001358D5
		// (set) Token: 0x0600563E RID: 22078 RVA: 0x001376E7 File Offset: 0x001358E7
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.ConnectivityLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x0600563F RID: 22079 RVA: 0x001376FA File Offset: 0x001358FA
		// (set) Token: 0x06005640 RID: 22080 RVA: 0x0013770C File Offset: 0x0013590C
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ConnectivityLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.ConnectivityLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.ConnectivityLogPath] = value;
			}
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x06005641 RID: 22081 RVA: 0x0013771A File Offset: 0x0013591A
		// (set) Token: 0x06005642 RID: 22082 RVA: 0x0013772C File Offset: 0x0013592C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ReceiveProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.ReceiveProtocolLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.ReceiveProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x06005643 RID: 22083 RVA: 0x0013773F File Offset: 0x0013593F
		// (set) Token: 0x06005644 RID: 22084 RVA: 0x00137751 File Offset: 0x00135951
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.ReceiveProtocolLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.ReceiveProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x06005645 RID: 22085 RVA: 0x00137764 File Offset: 0x00135964
		// (set) Token: 0x06005646 RID: 22086 RVA: 0x00137776 File Offset: 0x00135976
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.ReceiveProtocolLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.ReceiveProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x06005647 RID: 22087 RVA: 0x00137789 File Offset: 0x00135989
		// (set) Token: 0x06005648 RID: 22088 RVA: 0x0013779B File Offset: 0x0013599B
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ReceiveProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.ReceiveProtocolLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.ReceiveProtocolLogPath] = value;
			}
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x06005649 RID: 22089 RVA: 0x001377A9 File Offset: 0x001359A9
		// (set) Token: 0x0600564A RID: 22090 RVA: 0x001377BB File Offset: 0x001359BB
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan SendProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.SendProtocolLogMaxAge];
			}
			set
			{
				this[MailboxTransportServerSchema.SendProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x0600564B RID: 22091 RVA: 0x001377CE File Offset: 0x001359CE
		// (set) Token: 0x0600564C RID: 22092 RVA: 0x001377E0 File Offset: 0x001359E0
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.SendProtocolLogMaxDirectorySize];
			}
			set
			{
				this[MailboxTransportServerSchema.SendProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x0600564D RID: 22093 RVA: 0x001377F3 File Offset: 0x001359F3
		// (set) Token: 0x0600564E RID: 22094 RVA: 0x00137805 File Offset: 0x00135A05
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.SendProtocolLogMaxFileSize];
			}
			set
			{
				this[MailboxTransportServerSchema.SendProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x0600564F RID: 22095 RVA: 0x00137818 File Offset: 0x00135A18
		// (set) Token: 0x06005650 RID: 22096 RVA: 0x0013782A File Offset: 0x00135A2A
		[Parameter(Mandatory = false)]
		public LocalLongFullPath SendProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.SendProtocolLogPath];
			}
			set
			{
				this[MailboxTransportServerSchema.SendProtocolLogPath] = value;
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x06005651 RID: 22097 RVA: 0x00137838 File Offset: 0x00135A38
		// (set) Token: 0x06005652 RID: 22098 RVA: 0x0013784A File Offset: 0x00135A4A
		[Parameter(Mandatory = false)]
		public int MaxConcurrentMailboxDeliveries
		{
			get
			{
				return (int)this[MailboxTransportServerSchema.MaxConcurrentMailboxDeliveries];
			}
			set
			{
				this[MailboxTransportServerSchema.MaxConcurrentMailboxDeliveries] = value;
			}
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x06005653 RID: 22099 RVA: 0x0013785D File Offset: 0x00135A5D
		// (set) Token: 0x06005654 RID: 22100 RVA: 0x0013786F File Offset: 0x00135A6F
		[Parameter(Mandatory = false)]
		public int MaxConcurrentMailboxSubmissions
		{
			get
			{
				return (int)this[MailboxTransportServerSchema.MaxConcurrentMailboxSubmissions];
			}
			set
			{
				this[MailboxTransportServerSchema.MaxConcurrentMailboxSubmissions] = value;
			}
		}

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x06005655 RID: 22101 RVA: 0x00137882 File Offset: 0x00135A82
		// (set) Token: 0x06005656 RID: 22102 RVA: 0x00137894 File Offset: 0x00135A94
		[Parameter(Mandatory = false)]
		public bool PipelineTracingEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.PipelineTracingEnabled];
			}
			set
			{
				this[TransportServerSchema.PipelineTracingEnabled] = value;
			}
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x06005657 RID: 22103 RVA: 0x001378A7 File Offset: 0x00135AA7
		// (set) Token: 0x06005658 RID: 22104 RVA: 0x001378B9 File Offset: 0x00135AB9
		[Parameter(Mandatory = false)]
		public bool ContentConversionTracingEnabled
		{
			get
			{
				return (bool)this[TransportServerSchema.ContentConversionTracingEnabled];
			}
			set
			{
				this[TransportServerSchema.ContentConversionTracingEnabled] = value;
			}
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x06005659 RID: 22105 RVA: 0x001378CC File Offset: 0x00135ACC
		// (set) Token: 0x0600565A RID: 22106 RVA: 0x001378DE File Offset: 0x00135ADE
		[Parameter(Mandatory = false)]
		public LocalLongFullPath PipelineTracingPath
		{
			get
			{
				return (LocalLongFullPath)this[TransportServerSchema.PipelineTracingPath];
			}
			set
			{
				this[TransportServerSchema.PipelineTracingPath] = value;
			}
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x0600565B RID: 22107 RVA: 0x001378EC File Offset: 0x00135AEC
		// (set) Token: 0x0600565C RID: 22108 RVA: 0x001378FE File Offset: 0x00135AFE
		[Parameter(Mandatory = false)]
		public SmtpAddress? PipelineTracingSenderAddress
		{
			get
			{
				return (SmtpAddress?)this[TransportServerSchema.PipelineTracingSenderAddress];
			}
			set
			{
				this[TransportServerSchema.PipelineTracingSenderAddress] = value;
			}
		}

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x0600565D RID: 22109 RVA: 0x00137911 File Offset: 0x00135B11
		// (set) Token: 0x0600565E RID: 22110 RVA: 0x00137923 File Offset: 0x00135B23
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel MailboxDeliveryConnectorProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[MailboxTransportServerSchema.MailboxDeliveryConnectorProtocolLoggingLevel];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryConnectorProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x0600565F RID: 22111 RVA: 0x00137936 File Offset: 0x00135B36
		// (set) Token: 0x06005660 RID: 22112 RVA: 0x00137948 File Offset: 0x00135B48
		[Parameter(Mandatory = false)]
		public bool MailboxDeliveryConnectorSmtpUtf8Enabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.MailboxDeliveryConnectorSmtpUtf8Enabled];
			}
			set
			{
				this[MailboxTransportServerSchema.MailboxDeliveryConnectorSmtpUtf8Enabled] = value;
			}
		}

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x06005661 RID: 22113 RVA: 0x0013795B File Offset: 0x00135B5B
		public EnhancedTimeSpan MailboxSubmissionAgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxAge];
			}
		}

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x06005662 RID: 22114 RVA: 0x0013796D File Offset: 0x00135B6D
		public Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxDirectorySize];
			}
		}

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x06005663 RID: 22115 RVA: 0x0013797F File Offset: 0x00135B7F
		public Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogMaxFileSize];
			}
		}

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x06005664 RID: 22116 RVA: 0x00137991 File Offset: 0x00135B91
		public LocalLongFullPath MailboxSubmissionAgentLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogPath];
			}
		}

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x06005665 RID: 22117 RVA: 0x001379A3 File Offset: 0x00135BA3
		public bool MailboxSubmissionAgentLogEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.MailboxSubmissionAgentLogEnabled];
			}
		}

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x06005666 RID: 22118 RVA: 0x001379B5 File Offset: 0x00135BB5
		public EnhancedTimeSpan MailboxDeliveryAgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxAge];
			}
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x06005667 RID: 22119 RVA: 0x001379C7 File Offset: 0x00135BC7
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxDirectorySize];
			}
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x06005668 RID: 22120 RVA: 0x001379D9 File Offset: 0x00135BD9
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogMaxFileSize];
			}
		}

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x06005669 RID: 22121 RVA: 0x001379EB File Offset: 0x00135BEB
		public LocalLongFullPath MailboxDeliveryAgentLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogPath];
			}
		}

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x0600566A RID: 22122 RVA: 0x001379FD File Offset: 0x00135BFD
		public bool MailboxDeliveryAgentLogEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.MailboxDeliveryAgentLogEnabled];
			}
		}

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x0600566B RID: 22123 RVA: 0x00137A0F File Offset: 0x00135C0F
		public bool MailboxDeliveryThrottlingLogEnabled
		{
			get
			{
				return (bool)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogEnabled];
			}
		}

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x0600566C RID: 22124 RVA: 0x00137A21 File Offset: 0x00135C21
		public EnhancedTimeSpan MailboxDeliveryThrottlingLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxAge];
			}
		}

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x0600566D RID: 22125 RVA: 0x00137A33 File Offset: 0x00135C33
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxDirectorySize];
			}
		}

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x0600566E RID: 22126 RVA: 0x00137A45 File Offset: 0x00135C45
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogMaxFileSize];
			}
		}

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x0600566F RID: 22127 RVA: 0x00137A57 File Offset: 0x00135C57
		public LocalLongFullPath MailboxDeliveryThrottlingLogPath
		{
			get
			{
				return (LocalLongFullPath)this[MailboxTransportServerSchema.MailboxDeliveryThrottlingLogPath];
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x06005670 RID: 22128 RVA: 0x00137A69 File Offset: 0x00135C69
		public ServerRole ServerRole
		{
			get
			{
				return (ServerRole)this[MailboxTransportServerSchema.CurrentServerRole];
			}
		}

		// Token: 0x04003A7D RID: 14973
		private static MailboxTransportServerSchema schema = ObjectSchema.GetInstance<MailboxTransportServerSchema>();
	}
}
