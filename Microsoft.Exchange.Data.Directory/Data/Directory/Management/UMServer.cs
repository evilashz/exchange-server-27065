using System;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200077F RID: 1919
	[Serializable]
	public class UMServer : ADPresentationObject
	{
		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x00146603 File Offset: 0x00144803
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return UMServer.schema;
			}
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x0014660A File Offset: 0x0014480A
		public UMServer()
		{
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x00146612 File Offset: 0x00144812
		public UMServer(Server dataObject) : base(dataObject)
		{
		}

		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x06005FB3 RID: 24499 RVA: 0x0014661B File Offset: 0x0014481B
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x170021FF RID: 8703
		// (get) Token: 0x06005FB4 RID: 24500 RVA: 0x0014662D File Offset: 0x0014482D
		// (set) Token: 0x06005FB5 RID: 24501 RVA: 0x0014663F File Offset: 0x0014483F
		[Parameter(Mandatory = false)]
		public int? MaxCallsAllowed
		{
			get
			{
				return (int?)this[UMServerSchema.MaxCallsAllowed];
			}
			set
			{
				this[UMServerSchema.MaxCallsAllowed] = value;
			}
		}

		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x06005FB6 RID: 24502 RVA: 0x00146652 File Offset: 0x00144852
		// (set) Token: 0x06005FB7 RID: 24503 RVA: 0x00146664 File Offset: 0x00144864
		[Parameter(Mandatory = false)]
		public ServerStatus Status
		{
			get
			{
				return (ServerStatus)this[UMServerSchema.Status];
			}
			set
			{
				this[UMServerSchema.Status] = value;
			}
		}

		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x06005FB8 RID: 24504 RVA: 0x00146677 File Offset: 0x00144877
		// (set) Token: 0x06005FB9 RID: 24505 RVA: 0x00146689 File Offset: 0x00144889
		[Parameter(Mandatory = false)]
		public int SipTcpListeningPort
		{
			get
			{
				return (int)this[UMServerSchema.SipTcpListeningPort];
			}
			set
			{
				this[UMServerSchema.SipTcpListeningPort] = value;
			}
		}

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x06005FBA RID: 24506 RVA: 0x0014669C File Offset: 0x0014489C
		// (set) Token: 0x06005FBB RID: 24507 RVA: 0x001466AE File Offset: 0x001448AE
		[Parameter(Mandatory = false)]
		public int SipTlsListeningPort
		{
			get
			{
				return (int)this[UMServerSchema.SipTlsListeningPort];
			}
			set
			{
				this[UMServerSchema.SipTlsListeningPort] = value;
			}
		}

		// Token: 0x17002203 RID: 8707
		// (get) Token: 0x06005FBC RID: 24508 RVA: 0x001466C1 File Offset: 0x001448C1
		// (set) Token: 0x06005FBD RID: 24509 RVA: 0x001466D3 File Offset: 0x001448D3
		public MultiValuedProperty<UMLanguage> Languages
		{
			get
			{
				return (MultiValuedProperty<UMLanguage>)this[UMServerSchema.Languages];
			}
			internal set
			{
				this[UMServerSchema.Languages] = value;
			}
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x001466E1 File Offset: 0x001448E1
		// (set) Token: 0x06005FBF RID: 24511 RVA: 0x001466F3 File Offset: 0x001448F3
		public MultiValuedProperty<ADObjectId> DialPlans
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[UMServerSchema.DialPlans];
			}
			set
			{
				this[UMServerSchema.DialPlans] = value;
			}
		}

		// Token: 0x17002205 RID: 8709
		// (get) Token: 0x06005FC0 RID: 24512 RVA: 0x00146701 File Offset: 0x00144901
		// (set) Token: 0x06005FC1 RID: 24513 RVA: 0x00146713 File Offset: 0x00144913
		[Parameter(Mandatory = false)]
		public ScheduleInterval[] GrammarGenerationSchedule
		{
			get
			{
				return (ScheduleInterval[])this[UMServerSchema.GrammarGenerationSchedule];
			}
			set
			{
				this[UMServerSchema.GrammarGenerationSchedule] = value;
			}
		}

		// Token: 0x17002206 RID: 8710
		// (get) Token: 0x06005FC2 RID: 24514 RVA: 0x00146721 File Offset: 0x00144921
		// (set) Token: 0x06005FC3 RID: 24515 RVA: 0x00146733 File Offset: 0x00144933
		[Parameter(Mandatory = false)]
		public UMSmartHost ExternalHostFqdn
		{
			get
			{
				return (UMSmartHost)this[UMServerSchema.ExternalHostFqdn];
			}
			set
			{
				this[UMServerSchema.ExternalHostFqdn] = value;
			}
		}

		// Token: 0x17002207 RID: 8711
		// (get) Token: 0x06005FC4 RID: 24516 RVA: 0x00146741 File Offset: 0x00144941
		// (set) Token: 0x06005FC5 RID: 24517 RVA: 0x00146753 File Offset: 0x00144953
		[Parameter(Mandatory = false)]
		public UMSmartHost ExternalServiceFqdn
		{
			get
			{
				return (UMSmartHost)this[UMServerSchema.ExternalServiceFqdn];
			}
			set
			{
				this[UMServerSchema.ExternalServiceFqdn] = value;
			}
		}

		// Token: 0x17002208 RID: 8712
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x00146761 File Offset: 0x00144961
		// (set) Token: 0x06005FC7 RID: 24519 RVA: 0x00146773 File Offset: 0x00144973
		[Parameter(Mandatory = false)]
		public string UMPodRedirectTemplate
		{
			get
			{
				return (string)this[UMServerSchema.UMPodRedirectTemplate];
			}
			set
			{
				this[UMServerSchema.UMPodRedirectTemplate] = value;
			}
		}

		// Token: 0x17002209 RID: 8713
		// (get) Token: 0x06005FC8 RID: 24520 RVA: 0x00146781 File Offset: 0x00144981
		// (set) Token: 0x06005FC9 RID: 24521 RVA: 0x00146793 File Offset: 0x00144993
		[Parameter(Mandatory = false)]
		public string UMForwardingAddressTemplate
		{
			get
			{
				return (string)this[UMServerSchema.UMForwardingAddressTemplate];
			}
			set
			{
				this[UMServerSchema.UMForwardingAddressTemplate] = value;
			}
		}

		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x06005FCA RID: 24522 RVA: 0x001467A1 File Offset: 0x001449A1
		// (set) Token: 0x06005FCB RID: 24523 RVA: 0x001467B3 File Offset: 0x001449B3
		public string UMCertificateThumbprint
		{
			get
			{
				return (string)this[UMServerSchema.UMCertificateThumbprint];
			}
			internal set
			{
				this[UMServerSchema.UMCertificateThumbprint] = value;
			}
		}

		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x06005FCC RID: 24524 RVA: 0x001467C1 File Offset: 0x001449C1
		// (set) Token: 0x06005FCD RID: 24525 RVA: 0x001467D3 File Offset: 0x001449D3
		[Parameter(Mandatory = false)]
		public ProtocolConnectionSettings SIPAccessService
		{
			get
			{
				return (ProtocolConnectionSettings)this[UMServerSchema.SIPAccessService];
			}
			set
			{
				this[UMServerSchema.SIPAccessService] = value;
			}
		}

		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x06005FCE RID: 24526 RVA: 0x001467E1 File Offset: 0x001449E1
		// (set) Token: 0x06005FCF RID: 24527 RVA: 0x001467F3 File Offset: 0x001449F3
		[Parameter(Mandatory = false)]
		public UMStartupMode UMStartupMode
		{
			get
			{
				return (UMStartupMode)this[UMServerSchema.UMStartupMode];
			}
			set
			{
				this[UMServerSchema.UMStartupMode] = value;
			}
		}

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x06005FD0 RID: 24528 RVA: 0x00146806 File Offset: 0x00144A06
		// (set) Token: 0x06005FD1 RID: 24529 RVA: 0x00146818 File Offset: 0x00144A18
		[Parameter(Mandatory = false)]
		public bool IrmLogEnabled
		{
			get
			{
				return (bool)this[UMServerSchema.IrmLogEnabled];
			}
			set
			{
				this[UMServerSchema.IrmLogEnabled] = value;
			}
		}

		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x06005FD2 RID: 24530 RVA: 0x0014682B File Offset: 0x00144A2B
		// (set) Token: 0x06005FD3 RID: 24531 RVA: 0x0014683D File Offset: 0x00144A3D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan IrmLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[UMServerSchema.IrmLogMaxAge];
			}
			set
			{
				this[UMServerSchema.IrmLogMaxAge] = value;
			}
		}

		// Token: 0x1700220F RID: 8719
		// (get) Token: 0x06005FD4 RID: 24532 RVA: 0x00146850 File Offset: 0x00144A50
		// (set) Token: 0x06005FD5 RID: 24533 RVA: 0x00146862 File Offset: 0x00144A62
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[UMServerSchema.IrmLogMaxDirectorySize];
			}
			set
			{
				this[UMServerSchema.IrmLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17002210 RID: 8720
		// (get) Token: 0x06005FD6 RID: 24534 RVA: 0x00146875 File Offset: 0x00144A75
		// (set) Token: 0x06005FD7 RID: 24535 RVA: 0x00146887 File Offset: 0x00144A87
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize IrmLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[UMServerSchema.IrmLogMaxFileSize];
			}
			set
			{
				this[UMServerSchema.IrmLogMaxFileSize] = value;
			}
		}

		// Token: 0x17002211 RID: 8721
		// (get) Token: 0x06005FD8 RID: 24536 RVA: 0x0014689A File Offset: 0x00144A9A
		// (set) Token: 0x06005FD9 RID: 24537 RVA: 0x001468AC File Offset: 0x00144AAC
		[Parameter(Mandatory = false)]
		public LocalLongFullPath IrmLogPath
		{
			get
			{
				return (LocalLongFullPath)this[UMServerSchema.IrmLogPath];
			}
			set
			{
				this[UMServerSchema.IrmLogPath] = value;
			}
		}

		// Token: 0x17002212 RID: 8722
		// (get) Token: 0x06005FDA RID: 24538 RVA: 0x001468BA File Offset: 0x00144ABA
		// (set) Token: 0x06005FDB RID: 24539 RVA: 0x001468CC File Offset: 0x00144ACC
		[Parameter(Mandatory = false)]
		public bool IPAddressFamilyConfigurable
		{
			get
			{
				return (bool)this[UMServerSchema.IPAddressFamilyConfigurable];
			}
			set
			{
				this[UMServerSchema.IPAddressFamilyConfigurable] = value;
			}
		}

		// Token: 0x17002213 RID: 8723
		// (get) Token: 0x06005FDC RID: 24540 RVA: 0x001468DF File Offset: 0x00144ADF
		// (set) Token: 0x06005FDD RID: 24541 RVA: 0x001468F1 File Offset: 0x00144AF1
		[Parameter(Mandatory = false)]
		public IPAddressFamily IPAddressFamily
		{
			get
			{
				return (IPAddressFamily)this[UMServerSchema.IPAddressFamily];
			}
			set
			{
				this[UMServerSchema.IPAddressFamily] = value;
			}
		}

		// Token: 0x04004053 RID: 16467
		private static UMServerSchema schema = ObjectSchema.GetInstance<UMServerSchema>();
	}
}
