using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000558 RID: 1368
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class ReceiveConnector : ADConfigurationObject
	{
		// Token: 0x06003D56 RID: 15702 RVA: 0x000EA46C File Offset: 0x000E866C
		public ReceiveConnector()
		{
			this.Bindings = new MultiValuedProperty<IPBinding>(false, ReceiveConnectorSchema.Bindings, new IPBinding[]
			{
				IPBinding.Parse("0.0.0.0:25")
			});
			this.RemoteIPRanges = new MultiValuedProperty<IPRange>(false, ReceiveConnectorSchema.RemoteIPRanges, new IPRange[]
			{
				IPRange.Parse("0.0.0.0-255.255.255.255")
			});
			base.ResetChangeTracking();
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000EA4D1 File Offset: 0x000E86D1
		public ReceiveConnector(string name, ADObjectId connectorCollectionId) : this()
		{
			base.SetId(connectorCollectionId.GetChildId(name));
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x000EA4E6 File Offset: 0x000E86E6
		internal override ADObjectSchema Schema
		{
			get
			{
				return ReceiveConnector.schema;
			}
		}

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x000EA4ED File Offset: 0x000E86ED
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchSmtpReceiveConnector";
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06003D5A RID: 15706 RVA: 0x000EA4F4 File Offset: 0x000E86F4
		// (set) Token: 0x06003D5B RID: 15707 RVA: 0x000EA506 File Offset: 0x000E8706
		[Parameter(Mandatory = false)]
		public AuthMechanisms AuthMechanism
		{
			get
			{
				return (AuthMechanisms)this[ReceiveConnectorSchema.SecurityFlags];
			}
			set
			{
				this[ReceiveConnectorSchema.SecurityFlags] = (int)value;
			}
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06003D5C RID: 15708 RVA: 0x000EA519 File Offset: 0x000E8719
		// (set) Token: 0x06003D5D RID: 15709 RVA: 0x000EA52B File Offset: 0x000E872B
		[Parameter(Mandatory = false)]
		public string Banner
		{
			get
			{
				return (string)this[ReceiveConnectorSchema.Banner];
			}
			set
			{
				this[ReceiveConnectorSchema.Banner] = value;
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000EA539 File Offset: 0x000E8739
		// (set) Token: 0x06003D5F RID: 15711 RVA: 0x000EA54B File Offset: 0x000E874B
		[Parameter(Mandatory = false)]
		public bool BinaryMimeEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.BinaryMimeEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.BinaryMimeEnabled] = value;
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06003D60 RID: 15712 RVA: 0x000EA55E File Offset: 0x000E875E
		// (set) Token: 0x06003D61 RID: 15713 RVA: 0x000EA570 File Offset: 0x000E8770
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPBinding> Bindings
		{
			get
			{
				return (MultiValuedProperty<IPBinding>)this[ReceiveConnectorSchema.Bindings];
			}
			set
			{
				this[ReceiveConnectorSchema.Bindings] = value;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x000EA57E File Offset: 0x000E877E
		// (set) Token: 0x06003D63 RID: 15715 RVA: 0x000EA590 File Offset: 0x000E8790
		[Parameter(Mandatory = false)]
		public bool ChunkingEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.ChunkingEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.ChunkingEnabled] = value;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06003D64 RID: 15716 RVA: 0x000EA5A3 File Offset: 0x000E87A3
		// (set) Token: 0x06003D65 RID: 15717 RVA: 0x000EA5B5 File Offset: 0x000E87B5
		public ADObjectId DefaultDomain
		{
			get
			{
				return (ADObjectId)this[ReceiveConnectorSchema.DefaultDomain];
			}
			set
			{
				this[ReceiveConnectorSchema.DefaultDomain] = value;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06003D66 RID: 15718 RVA: 0x000EA5C3 File Offset: 0x000E87C3
		// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000EA5D5 File Offset: 0x000E87D5
		[Parameter(Mandatory = false)]
		public bool DeliveryStatusNotificationEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.DeliveryStatusNotificationEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.DeliveryStatusNotificationEnabled] = value;
			}
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000EA5E8 File Offset: 0x000E87E8
		// (set) Token: 0x06003D69 RID: 15721 RVA: 0x000EA5FA File Offset: 0x000E87FA
		[Parameter(Mandatory = false)]
		public bool EightBitMimeEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.EightBitMimeEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.EightBitMimeEnabled] = value;
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x000EA60D File Offset: 0x000E880D
		// (set) Token: 0x06003D6B RID: 15723 RVA: 0x000EA61F File Offset: 0x000E881F
		[Parameter(Mandatory = false)]
		public bool SmtpUtf8Enabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.SmtpUtf8Enabled];
			}
			set
			{
				this[ReceiveConnectorSchema.SmtpUtf8Enabled] = value;
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000EA632 File Offset: 0x000E8832
		// (set) Token: 0x06003D6D RID: 15725 RVA: 0x000EA644 File Offset: 0x000E8844
		[Parameter(Mandatory = false)]
		public bool BareLinefeedRejectionEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.BareLinefeedRejectionEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.BareLinefeedRejectionEnabled] = value;
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06003D6E RID: 15726 RVA: 0x000EA657 File Offset: 0x000E8857
		// (set) Token: 0x06003D6F RID: 15727 RVA: 0x000EA669 File Offset: 0x000E8869
		[Parameter(Mandatory = false)]
		public bool DomainSecureEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.DomainSecureEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.DomainSecureEnabled] = value;
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06003D70 RID: 15728 RVA: 0x000EA67C File Offset: 0x000E887C
		// (set) Token: 0x06003D71 RID: 15729 RVA: 0x000EA68E File Offset: 0x000E888E
		[Parameter(Mandatory = false)]
		public bool EnhancedStatusCodesEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.EnhancedStatusCodesEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.EnhancedStatusCodesEnabled] = value;
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000EA6A1 File Offset: 0x000E88A1
		// (set) Token: 0x06003D73 RID: 15731 RVA: 0x000EA6B3 File Offset: 0x000E88B3
		[Parameter(Mandatory = false)]
		public bool LongAddressesEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.LongAddressesEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.LongAddressesEnabled] = value;
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x000EA6C6 File Offset: 0x000E88C6
		// (set) Token: 0x06003D75 RID: 15733 RVA: 0x000EA6D8 File Offset: 0x000E88D8
		[Parameter(Mandatory = false)]
		public bool OrarEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.OrarEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.OrarEnabled] = value;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x000EA6EB File Offset: 0x000E88EB
		// (set) Token: 0x06003D77 RID: 15735 RVA: 0x000EA6FD File Offset: 0x000E88FD
		[Parameter(Mandatory = false)]
		public bool SuppressXAnonymousTls
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.SuppressXAnonymousTls];
			}
			set
			{
				this[ReceiveConnectorSchema.SuppressXAnonymousTls] = value;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x000EA710 File Offset: 0x000E8910
		// (set) Token: 0x06003D79 RID: 15737 RVA: 0x000EA722 File Offset: 0x000E8922
		[Parameter(Mandatory = false)]
		public bool ProxyEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.ProxyEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.ProxyEnabled] = value;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x000EA735 File Offset: 0x000E8935
		// (set) Token: 0x06003D7B RID: 15739 RVA: 0x000EA747 File Offset: 0x000E8947
		[Parameter(Mandatory = false)]
		public bool AdvertiseClientSettings
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.AdvertiseClientSettings];
			}
			set
			{
				this[ReceiveConnectorSchema.AdvertiseClientSettings] = value;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000EA75A File Offset: 0x000E895A
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x000EA76C File Offset: 0x000E896C
		[Parameter(Mandatory = false)]
		public Fqdn Fqdn
		{
			get
			{
				return (Fqdn)this[ReceiveConnectorSchema.Fqdn];
			}
			set
			{
				this[ReceiveConnectorSchema.Fqdn] = value;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x000EA77A File Offset: 0x000E897A
		// (set) Token: 0x06003D7F RID: 15743 RVA: 0x000EA78C File Offset: 0x000E898C
		[Parameter(Mandatory = false)]
		public Fqdn ServiceDiscoveryFqdn
		{
			get
			{
				return (Fqdn)this[ReceiveConnectorSchema.ServiceDiscoveryFqdn];
			}
			set
			{
				this[ReceiveConnectorSchema.ServiceDiscoveryFqdn] = value;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000EA79A File Offset: 0x000E899A
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000EA7AC File Offset: 0x000E89AC
		[Parameter(Mandatory = false)]
		public SmtpX509Identifier TlsCertificateName
		{
			get
			{
				return (SmtpX509Identifier)this[ReceiveConnectorSchema.TlsCertificateName];
			}
			set
			{
				this[ReceiveConnectorSchema.TlsCertificateName] = value;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x000EA7BA File Offset: 0x000E89BA
		// (set) Token: 0x06003D83 RID: 15747 RVA: 0x000EA7CC File Offset: 0x000E89CC
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)this[ReceiveConnectorSchema.Comment];
			}
			set
			{
				this[ReceiveConnectorSchema.Comment] = value;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x000EA7DA File Offset: 0x000E89DA
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x000EA7EC File Offset: 0x000E89EC
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.Enabled];
			}
			set
			{
				this[ReceiveConnectorSchema.Enabled] = value;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x000EA7FF File Offset: 0x000E89FF
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x000EA811 File Offset: 0x000E8A11
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectionTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[ReceiveConnectorSchema.ConnectionTimeout];
			}
			set
			{
				this[ReceiveConnectorSchema.ConnectionTimeout] = value;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x000EA824 File Offset: 0x000E8A24
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x000EA836 File Offset: 0x000E8A36
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectionInactivityTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[ReceiveConnectorSchema.ConnectionInactivityTimeout];
			}
			set
			{
				this[ReceiveConnectorSchema.ConnectionInactivityTimeout] = value;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x000EA849 File Offset: 0x000E8A49
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x000EA85B File Offset: 0x000E8A5B
		[Parameter(Mandatory = false)]
		public Unlimited<int> MessageRateLimit
		{
			get
			{
				return (Unlimited<int>)this[ReceiveConnectorSchema.MessageRateLimit];
			}
			set
			{
				this[ReceiveConnectorSchema.MessageRateLimit] = value;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000EA86E File Offset: 0x000E8A6E
		// (set) Token: 0x06003D8D RID: 15757 RVA: 0x000EA880 File Offset: 0x000E8A80
		[Parameter(Mandatory = false)]
		public MessageRateSourceFlags MessageRateSource
		{
			get
			{
				return (MessageRateSourceFlags)this[ReceiveConnectorSchema.MessageRateSource];
			}
			set
			{
				this[ReceiveConnectorSchema.MessageRateSource] = (int)value;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x000EA893 File Offset: 0x000E8A93
		// (set) Token: 0x06003D8F RID: 15759 RVA: 0x000EA8A5 File Offset: 0x000E8AA5
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxInboundConnection
		{
			get
			{
				return (Unlimited<int>)this[ReceiveConnectorSchema.MaxInboundConnection];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxInboundConnection] = value;
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x000EA8B8 File Offset: 0x000E8AB8
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x000EA8CA File Offset: 0x000E8ACA
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxInboundConnectionPerSource
		{
			get
			{
				return (Unlimited<int>)this[ReceiveConnectorSchema.MaxInboundConnectionPerSource];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxInboundConnectionPerSource] = value;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000EA8DD File Offset: 0x000E8ADD
		// (set) Token: 0x06003D93 RID: 15763 RVA: 0x000EA8EF File Offset: 0x000E8AEF
		[Parameter(Mandatory = false)]
		public int MaxInboundConnectionPercentagePerSource
		{
			get
			{
				return (int)this[ReceiveConnectorSchema.MaxInboundConnectionPercentagePerSource];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxInboundConnectionPercentagePerSource] = value;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x000EA902 File Offset: 0x000E8B02
		// (set) Token: 0x06003D95 RID: 15765 RVA: 0x000EA914 File Offset: 0x000E8B14
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MaxHeaderSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ReceiveConnectorSchema.MaxHeaderSize];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxHeaderSize] = value;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06003D96 RID: 15766 RVA: 0x000EA927 File Offset: 0x000E8B27
		// (set) Token: 0x06003D97 RID: 15767 RVA: 0x000EA939 File Offset: 0x000E8B39
		[Parameter(Mandatory = false)]
		public int MaxHopCount
		{
			get
			{
				return (int)this[ReceiveConnectorSchema.MaxHopCount];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxHopCount] = value;
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000EA94C File Offset: 0x000E8B4C
		// (set) Token: 0x06003D99 RID: 15769 RVA: 0x000EA95E File Offset: 0x000E8B5E
		[Parameter(Mandatory = false)]
		public int MaxLocalHopCount
		{
			get
			{
				return (int)this[ReceiveConnectorSchema.MaxLocalHopCount];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxLocalHopCount] = value;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x000EA971 File Offset: 0x000E8B71
		// (set) Token: 0x06003D9B RID: 15771 RVA: 0x000EA983 File Offset: 0x000E8B83
		[Parameter(Mandatory = false)]
		public int MaxLogonFailures
		{
			get
			{
				return (int)this[ReceiveConnectorSchema.MaxLogonFailures];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxLogonFailures] = value;
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06003D9C RID: 15772 RVA: 0x000EA996 File Offset: 0x000E8B96
		// (set) Token: 0x06003D9D RID: 15773 RVA: 0x000EA9A8 File Offset: 0x000E8BA8
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MaxMessageSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ReceiveConnectorSchema.MaxMessageSize];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxMessageSize] = value;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x000EA9BB File Offset: 0x000E8BBB
		// (set) Token: 0x06003D9F RID: 15775 RVA: 0x000EA9CD File Offset: 0x000E8BCD
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxProtocolErrors
		{
			get
			{
				return (Unlimited<int>)this[ReceiveConnectorSchema.MaxProtocolErrors];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxProtocolErrors] = value;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x000EA9E0 File Offset: 0x000E8BE0
		// (set) Token: 0x06003DA1 RID: 15777 RVA: 0x000EA9F2 File Offset: 0x000E8BF2
		[Parameter(Mandatory = false)]
		public int MaxRecipientsPerMessage
		{
			get
			{
				return (int)this[ReceiveConnectorSchema.MaxRecipientsPerMessage];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxRecipientsPerMessage] = value;
			}
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x000EAA05 File Offset: 0x000E8C05
		// (set) Token: 0x06003DA3 RID: 15779 RVA: 0x000EAA17 File Offset: 0x000E8C17
		[Parameter(Mandatory = false)]
		public PermissionGroups PermissionGroups
		{
			get
			{
				return (PermissionGroups)this[ReceiveConnectorSchema.PermissionGroups];
			}
			set
			{
				this[ReceiveConnectorSchema.PermissionGroups] = value;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x000EAA2A File Offset: 0x000E8C2A
		// (set) Token: 0x06003DA5 RID: 15781 RVA: 0x000EAA3C File Offset: 0x000E8C3C
		[Parameter(Mandatory = false)]
		public bool PipeliningEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.PipeliningEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.PipeliningEnabled] = value;
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x000EAA4F File Offset: 0x000E8C4F
		// (set) Token: 0x06003DA7 RID: 15783 RVA: 0x000EAA61 File Offset: 0x000E8C61
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel ProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[ReceiveConnectorSchema.ProtocolLoggingLevel];
			}
			set
			{
				this[ReceiveConnectorSchema.ProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x000EAA74 File Offset: 0x000E8C74
		// (set) Token: 0x06003DA9 RID: 15785 RVA: 0x000EAA86 File Offset: 0x000E8C86
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> RemoteIPRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[ReceiveConnectorSchema.RemoteIPRanges];
			}
			set
			{
				this[ReceiveConnectorSchema.RemoteIPRanges] = value;
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x000EAA94 File Offset: 0x000E8C94
		// (set) Token: 0x06003DAB RID: 15787 RVA: 0x000EAAA6 File Offset: 0x000E8CA6
		[Parameter(Mandatory = false)]
		public bool RequireEHLODomain
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.RequireEHLODomain];
			}
			set
			{
				this[ReceiveConnectorSchema.RequireEHLODomain] = value;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x000EAAB9 File Offset: 0x000E8CB9
		// (set) Token: 0x06003DAD RID: 15789 RVA: 0x000EAACB File Offset: 0x000E8CCB
		[Parameter(Mandatory = false)]
		public bool RequireTLS
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.RequireTLS];
			}
			set
			{
				this[ReceiveConnectorSchema.RequireTLS] = value;
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06003DAE RID: 15790 RVA: 0x000EAADE File Offset: 0x000E8CDE
		// (set) Token: 0x06003DAF RID: 15791 RVA: 0x000EAAF0 File Offset: 0x000E8CF0
		[Parameter(Mandatory = false)]
		public bool EnableAuthGSSAPI
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.EnableAuthGSSAPI];
			}
			set
			{
				this[ReceiveConnectorSchema.EnableAuthGSSAPI] = value;
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06003DB0 RID: 15792 RVA: 0x000EAB03 File Offset: 0x000E8D03
		// (set) Token: 0x06003DB1 RID: 15793 RVA: 0x000EAB15 File Offset: 0x000E8D15
		[Parameter(Mandatory = false)]
		public ExtendedProtectionPolicySetting ExtendedProtectionPolicy
		{
			get
			{
				return (ExtendedProtectionPolicySetting)this[ReceiveConnectorSchema.ExtendedProtectionPolicy];
			}
			set
			{
				this[ReceiveConnectorSchema.ExtendedProtectionPolicy] = (int)value;
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06003DB2 RID: 15794 RVA: 0x000EAB28 File Offset: 0x000E8D28
		// (set) Token: 0x06003DB3 RID: 15795 RVA: 0x000EAB3A File Offset: 0x000E8D3A
		[Parameter(Mandatory = false)]
		public bool LiveCredentialEnabled
		{
			get
			{
				return (bool)this[ReceiveConnectorSchema.LiveCredentialEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.LiveCredentialEnabled] = value;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06003DB4 RID: 15796 RVA: 0x000EAB4D File Offset: 0x000E8D4D
		// (set) Token: 0x06003DB5 RID: 15797 RVA: 0x000EAB5F File Offset: 0x000E8D5F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
		{
			get
			{
				return (MultiValuedProperty<SmtpReceiveDomainCapabilities>)this[ReceiveConnectorSchema.TlsDomainCapabilities];
			}
			set
			{
				this[ReceiveConnectorSchema.TlsDomainCapabilities] = value;
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x000EAB6D File Offset: 0x000E8D6D
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[ReceiveConnectorSchema.Server];
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06003DB7 RID: 15799 RVA: 0x000EAB7F File Offset: 0x000E8D7F
		// (set) Token: 0x06003DB8 RID: 15800 RVA: 0x000EAB91 File Offset: 0x000E8D91
		[Parameter(Mandatory = false)]
		public ServerRole TransportRole
		{
			get
			{
				return (ServerRole)this[ReceiveConnectorSchema.TransportRole];
			}
			set
			{
				this[ReceiveConnectorSchema.TransportRole] = (int)value;
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x000EABA4 File Offset: 0x000E8DA4
		internal bool HasNoAuthMechanisms
		{
			get
			{
				return this.AuthMechanism == AuthMechanisms.None;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x000EABAF File Offset: 0x000E8DAF
		internal bool HasTlsAuthMechanism
		{
			get
			{
				return (this.AuthMechanism & AuthMechanisms.Tls) != AuthMechanisms.None;
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x000EABBF File Offset: 0x000E8DBF
		internal bool HasIntegratedAuthMechanism
		{
			get
			{
				return (this.AuthMechanism & AuthMechanisms.Integrated) != AuthMechanisms.None;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x000EABCF File Offset: 0x000E8DCF
		internal bool HasBasicAuthAuthMechanism
		{
			get
			{
				return (this.AuthMechanism & AuthMechanisms.BasicAuth) != AuthMechanisms.None;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x000EABDF File Offset: 0x000E8DDF
		internal bool HasBasicAuthRequireTlsAuthMechanism
		{
			get
			{
				return (this.AuthMechanism & AuthMechanisms.BasicAuthRequireTLS) != AuthMechanisms.None;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000EABEF File Offset: 0x000E8DEF
		internal bool HasExchangeServerAuthMechanism
		{
			get
			{
				return (this.AuthMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None;
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x000EAC00 File Offset: 0x000E8E00
		internal bool HasExternalAuthoritativeAuthMechanism
		{
			get
			{
				return (this.AuthMechanism & AuthMechanisms.ExternalAuthoritative) != AuthMechanisms.None;
			}
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x000EAC14 File Offset: 0x000E8E14
		internal void SetPermissionGroupsBasedOnSecurityDescriptor(Server server)
		{
			RawSecurityDescriptor rsd = base.Session.ReadSecurityDescriptor(base.Id);
			ReceiveConnector.PermissionGroupInfo[] permissionGroupInfos = ReceiveConnector.PermissionGroupPermissions.GetPermissionGroupInfos(server);
			this.PermissionGroups = ReceiveConnector.GetPermissionGroups(rsd, permissionGroupInfos, server);
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x000EAC48 File Offset: 0x000E8E48
		internal static object ServerGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Server", string.Empty), ReceiveConnectorSchema.Server, propertyBag[ADObjectSchema.Id]), null);
				}
				result = adobjectId.DescendantDN(8);
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Server", ex.Message), ReceiveConnectorSchema.Server, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x000EACDC File Offset: 0x000E8EDC
		internal void SaveNewSecurityDescriptor(Server server)
		{
			if (!base.IsModified(ReceiveConnectorSchema.PermissionGroups))
			{
				return;
			}
			ReceiveConnector.PermissionGroupInfo[] permissionGroupInfos = ReceiveConnector.PermissionGroupPermissions.GetPermissionGroupInfos(server);
			PrincipalPermissionList principalPermissionList = new PrincipalPermissionList();
			PrincipalPermissionList principalPermissionList2 = new PrincipalPermissionList();
			foreach (ReceiveConnector.PermissionGroupInfo permissionGroupInfo in permissionGroupInfos)
			{
				for (int j = 0; j < permissionGroupInfo.Sids.Length; j++)
				{
					principalPermissionList2.Add(permissionGroupInfo.Sids[j], permissionGroupInfo.DefaultPermission[j]);
				}
				if ((this.PermissionGroups & permissionGroupInfo.PermissionGroup) != PermissionGroups.None)
				{
					for (int k = 0; k < permissionGroupInfo.Sids.Length; k++)
					{
						principalPermissionList.Add(permissionGroupInfo.Sids[k], permissionGroupInfo.DefaultPermission[k]);
						if (permissionGroupInfo.PermissionGroup == PermissionGroups.ExchangeLegacyServers)
						{
							principalPermissionList.AddDeny(permissionGroupInfo.Sids[k], Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders);
						}
					}
				}
			}
			RawSecurityDescriptor rawSecurityDescriptor = base.ReadSecurityDescriptor();
			if (rawSecurityDescriptor != null)
			{
				rawSecurityDescriptor = principalPermissionList2.RemoveExtendedRightsFromSecurityDescriptor(rawSecurityDescriptor);
				rawSecurityDescriptor = principalPermissionList.AddExtendedRightsToSecurityDescriptor(rawSecurityDescriptor);
				base.SaveSecurityDescriptor(rawSecurityDescriptor);
				return;
			}
			if (principalPermissionList.Count != 0)
			{
				SecurityIdentifier principal = principalPermissionList[0].Principal;
				SecurityIdentifier group = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
				rawSecurityDescriptor = principalPermissionList.CreateExtendedRightsSecurityDescriptor(principal, group);
				rawSecurityDescriptor = principalPermissionList.AddExtendedRightsToSecurityDescriptor(rawSecurityDescriptor);
				base.SaveSecurityDescriptor(rawSecurityDescriptor);
			}
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x000EAE1D File Offset: 0x000E901D
		internal virtual RawSecurityDescriptor GetSecurityDescriptor()
		{
			if (this.securityDescriptor == null)
			{
				this.securityDescriptor = base.ReadSecurityDescriptor();
			}
			return this.securityDescriptor;
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x000EAE3C File Offset: 0x000E903C
		private static PermissionGroups GetPermissionGroups(RawSecurityDescriptor rsd, ReceiveConnector.PermissionGroupInfo[] permissionGroupInfos, Server server)
		{
			PermissionGroups permissionGroups = PermissionGroups.None;
			if (rsd == null)
			{
				return permissionGroups;
			}
			Dictionary<SecurityIdentifier, bool> dictionary = new Dictionary<SecurityIdentifier, bool>();
			Permission permission = Permission.SMTPSubmitForMLS | Permission.SMTPSendEXCH50 | Permission.SendRoutingHeaders | Permission.SendForestHeaders | Permission.SendOrganizationHeaders | Permission.SendAs | Permission.SMTPSendXShadow;
			if (!server.IsE14OrLater)
			{
				permission |= Permission.SMTPAcceptXShadow;
			}
			Permission permission2 = ~permission;
			foreach (ReceiveConnector.PermissionGroupInfo permissionGroupInfo in permissionGroupInfos)
			{
				bool flag = true;
				for (int j = 0; j < permissionGroupInfo.Sids.Length; j++)
				{
					Permission permission3;
					try
					{
						permission3 = AuthzAuthorization.CheckPermissions(permissionGroupInfo.Sids[j], rsd, null);
					}
					catch (Win32Exception ex)
					{
						throw new LocalizedException(DirectoryStrings.ExceptionWin32OperationFailed(ex.NativeErrorCode, ex.Message), ex);
					}
					permission3 &= permission2;
					Permission permission4 = permissionGroupInfo.DefaultPermission[j];
					if (!server.IsE14OrLater)
					{
						permission4 &= ~Permission.SMTPAcceptXShadow;
					}
					if ((permission3 & permission4) != permission4)
					{
						flag = false;
					}
					if (permission3 != Permission.None && permission3 != permission4)
					{
						permissionGroups |= PermissionGroups.Custom;
					}
					dictionary.Add(permissionGroupInfo.Sids[j], true);
				}
				if (flag)
				{
					permissionGroups |= permissionGroupInfo.PermissionGroup;
				}
			}
			if ((permissionGroups & PermissionGroups.Custom) != PermissionGroups.Custom)
			{
				ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
				activeDirectorySecurity.SetSecurityDescriptorSddlForm(rsd.GetSddlForm(AccessControlSections.All));
				foreach (object obj in activeDirectorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier)))
				{
					AuthorizationRule authorizationRule = (AuthorizationRule)obj;
					if (!authorizationRule.IsInherited && authorizationRule is AccessRule)
					{
						SecurityIdentifier securityIdentifier = authorizationRule.IdentityReference as SecurityIdentifier;
						if (securityIdentifier != null && !dictionary.ContainsKey(securityIdentifier))
						{
							Permission permission5 = AuthzAuthorization.CheckPermissions(securityIdentifier, rsd, null);
							if (permission5 != Permission.None && !ReceiveConnector.IsPredefinedPermissionGroup(permission5))
							{
								permissionGroups |= PermissionGroups.Custom;
								break;
							}
							dictionary.Add(securityIdentifier, true);
						}
					}
				}
			}
			return permissionGroups;
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000EB028 File Offset: 0x000E9228
		private static bool IsPredefinedPermissionGroup(Permission permission)
		{
			return (Permission.SMTPSubmit | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.AcceptRoutingHeaders) == permission || (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow | Permission.SMTPAcceptXProxyFrom | Permission.SMTPAcceptXSessionParams | Permission.SMTPAcceptXMessageContextADRecipientCache | Permission.SMTPAcceptXMessageContextExtendedProperties | Permission.SMTPAcceptXMessageContextFastIndex | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe) == permission || (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders) == permission || (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow) == permission || (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.BypassAntiSpam | Permission.AcceptRoutingHeaders) == permission || (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders) == permission || (Permission.SMTPSubmit | Permission.AcceptRoutingHeaders) == permission;
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000EB065 File Offset: 0x000E9265
		// (set) Token: 0x06003DC7 RID: 15815 RVA: 0x000EB077 File Offset: 0x000E9277
		[Parameter(Mandatory = false)]
		public SizeMode SizeEnabled
		{
			get
			{
				return (SizeMode)this[ReceiveConnectorSchema.SizeEnabled];
			}
			set
			{
				this[ReceiveConnectorSchema.SizeEnabled] = value;
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000EB08A File Offset: 0x000E928A
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x000EB09C File Offset: 0x000E929C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TarpitInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[ReceiveConnectorSchema.TarpitInterval];
			}
			set
			{
				this[ReceiveConnectorSchema.TarpitInterval] = value;
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000EB0AF File Offset: 0x000E92AF
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x000EB0C1 File Offset: 0x000E92C1
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MaxAcknowledgementDelay
		{
			get
			{
				return (EnhancedTimeSpan)this[ReceiveConnectorSchema.MaxAcknowledgementDelay];
			}
			set
			{
				this[ReceiveConnectorSchema.MaxAcknowledgementDelay] = value;
			}
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000EB0D4 File Offset: 0x000E92D4
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			foreach (IPBinding ipbinding in this.Bindings)
			{
				IPvxAddress pvxAddress = new IPvxAddress(ipbinding.Address);
				if (pvxAddress.IsMulticast || pvxAddress.IsBroadcast)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidBindingAddressSetting, ReceiveConnectorSchema.Bindings, this));
					break;
				}
			}
			if (!string.IsNullOrEmpty(this.Banner))
			{
				SmtpResponse smtpResponse;
				if (!SmtpResponse.TryParse(this.Banner, out smtpResponse) || !string.Equals("220", smtpResponse.StatusCode, StringComparison.Ordinal))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidBannerSetting, ReceiveConnectorSchema.Banner, this));
				}
				else if (smtpResponse.StatusText != null)
				{
					bool flag = smtpResponse.StatusText.Length <= 50;
					if (flag)
					{
						foreach (string text in smtpResponse.StatusText)
						{
							if (text.Length > 2000)
							{
								flag = false;
								break;
							}
						}
					}
					if (!flag)
					{
						errors.Add(new PropertyValidationError(DirectoryStrings.InvalidBannerSetting, ReceiveConnectorSchema.Banner, this));
					}
				}
			}
			bool flag2 = (this.AuthMechanism & AuthMechanisms.ExternalAuthoritative) != AuthMechanisms.None;
			bool flag3 = (this.AuthMechanism & AuthMechanisms.BasicAuth) != AuthMechanisms.None;
			bool flag4 = (this.AuthMechanism & AuthMechanisms.BasicAuthRequireTLS) != AuthMechanisms.None;
			bool flag5 = (this.AuthMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None;
			bool flag6 = (this.AuthMechanism & AuthMechanisms.Integrated) != AuthMechanisms.None;
			bool flag7 = (this.AuthMechanism & AuthMechanisms.Tls) != AuthMechanisms.None;
			if (flag4 && !flag7)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.BasicAfterTLSWithoutTLS, ReceiveConnectorSchema.SecurityFlags, this));
			}
			if (flag4 && !flag3)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.BasicAfterTLSWithoutBasic, ReceiveConnectorSchema.SecurityFlags, this));
			}
			if (this.RequireTLS && !flag7)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.RequireTLSWithoutTLS, ReceiveConnectorSchema.SecurityFlags, this));
			}
			if (flag2 && (flag3 || flag4 || flag5 || flag6))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExternalAndAuthSet, ReceiveConnectorSchema.SecurityFlags, this));
			}
			if (this.LiveCredentialEnabled && !flag3 && !flag4)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.LiveCredentialWithoutBasic, ReceiveConnectorSchema.SecurityFlags, this));
			}
			if (EnhancedTimeSpan.Compare(this.ConnectionInactivityTimeout, this.ConnectionTimeout) > 0)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ConnectionTimeoutLessThanInactivityTimeout, ReceiveConnectorSchema.ConnectionTimeout, this));
			}
			if (this.DomainSecureEnabled)
			{
				if (!flag7)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.DomainSecureEnabledWithoutTls, ReceiveConnectorSchema.DomainSecureEnabled, this));
				}
				if (flag2)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.DomainSecureEnabledWithExternalAuthoritative, ReceiveConnectorSchema.DomainSecureEnabled, this));
				}
			}
			if (flag2 && (this.PermissionGroups & PermissionGroups.ExchangeServers) == PermissionGroups.None)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExternalAuthoritativeWithoutExchangeServerPermission, ReceiveConnectorSchema.DomainSecureEnabled, this));
			}
			if (this.ExtendedProtectionPolicy != ExtendedProtectionPolicySetting.None && !this.RequireTLS)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ExtendedProtectionNonTlsTerminatingProxyScenarioRequireTls, ReceiveConnectorSchema.ExtendedProtectionPolicy, this));
			}
			this.ValidateTlsDomainCapabilities(errors);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000EB3E8 File Offset: 0x000E95E8
		private void ValidateTlsDomainCapabilities(List<ValidationError> errors)
		{
			HashSet<SmtpDomainWithSubdomains> hashSet = new HashSet<SmtpDomainWithSubdomains>();
			foreach (SmtpReceiveDomainCapabilities smtpReceiveDomainCapabilities in this.TlsDomainCapabilities)
			{
				if (smtpReceiveDomainCapabilities.Domain.SmtpDomain == null)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.StarTlsDomainCapabilitiesNotAllowed, ReceiveConnectorSchema.TlsDomainCapabilities, this));
				}
				else if (!hashSet.Add(smtpReceiveDomainCapabilities.Domain))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.DuplicateTlsDomainCapabilitiesNotAllowed(smtpReceiveDomainCapabilities.Domain), ReceiveConnectorSchema.TlsDomainCapabilities, this));
				}
			}
		}

		// Token: 0x040029C8 RID: 10696
		public const PermissionGroups UnsupportedEdgePermissionGroups = PermissionGroups.ExchangeLegacyServers;

		// Token: 0x040029C9 RID: 10697
		public const string MostDerivedClass = "msExchSmtpReceiveConnector";

		// Token: 0x040029CA RID: 10698
		internal static readonly string DefaultName = "SMTP Receive Connectors";

		// Token: 0x040029CB RID: 10699
		private static ReceiveConnectorSchema schema = ObjectSchema.GetInstance<ReceiveConnectorSchema>();

		// Token: 0x040029CC RID: 10700
		[NonSerialized]
		private RawSecurityDescriptor securityDescriptor;

		// Token: 0x02000559 RID: 1369
		internal static class PermissionGroupPermissions
		{
			// Token: 0x06003DCF RID: 15823 RVA: 0x000EB4A4 File Offset: 0x000E96A4
			internal static ReceiveConnector.PermissionGroupInfo[] GetPermissionGroupInfos(Server server)
			{
				ReceiveConnector.PermissionGroupInfo[] array;
				if (server.IsEdgeServer)
				{
					array = new ReceiveConnector.PermissionGroupInfo[4];
				}
				else
				{
					array = new ReceiveConnector.PermissionGroupInfo[5];
				}
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1215, "GetPermissionGroupInfos", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ReceiveConnector.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1220, "GetPermissionGroupInfos", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ReceiveConnector.cs");
				array[0] = new ReceiveConnector.PermissionGroupInfo(PermissionGroups.AnonymousUsers, new SecurityIdentifier(WellKnownSidType.AnonymousSid, null), Permission.SMTPSubmit | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.AcceptRoutingHeaders);
				array[1] = new ReceiveConnector.PermissionGroupInfo(PermissionGroups.ExchangeUsers, new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null), Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.BypassAntiSpam | Permission.AcceptRoutingHeaders);
				SecurityIdentifier[] array2;
				Permission[] array3;
				if (server.IsEdgeServer)
				{
					array2 = new SecurityIdentifier[3];
					array3 = new Permission[3];
				}
				else
				{
					array2 = new SecurityIdentifier[4];
					array3 = new Permission[4];
					array2[3] = ReceiveConnector.PermissionGroupPermissions.GetSidForExchangeKnownGuid(tenantOrRootOrgRecipientSession, WellKnownGuid.ExSWkGuid, tenantOrTopologyConfigurationSession.ConfigurationNamingContext.DistinguishedName);
					array3[3] = (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow | Permission.SMTPAcceptXProxyFrom | Permission.SMTPAcceptXSessionParams | Permission.SMTPAcceptXMessageContextADRecipientCache | Permission.SMTPAcceptXMessageContextExtendedProperties | Permission.SMTPAcceptXMessageContextFastIndex | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe);
				}
				Permission permission;
				if (server.IsE15OrLater)
				{
					permission = (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow | Permission.SMTPAcceptXProxyFrom | Permission.SMTPAcceptXSessionParams | Permission.SMTPAcceptXMessageContextADRecipientCache | Permission.SMTPAcceptXMessageContextExtendedProperties | Permission.SMTPAcceptXMessageContextFastIndex | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe);
				}
				else if (server.IsE14OrLater)
				{
					permission = (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow);
				}
				else if (server.IsExchange2007OrLater)
				{
					permission = (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders);
				}
				else
				{
					permission = (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow | Permission.SMTPAcceptXProxyFrom | Permission.SMTPAcceptXSessionParams | Permission.SMTPAcceptXMessageContextADRecipientCache | Permission.SMTPAcceptXMessageContextExtendedProperties | Permission.SMTPAcceptXMessageContextFastIndex | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe);
				}
				array2[0] = WellKnownSids.EdgeTransportServers;
				array3[0] = permission;
				array2[1] = WellKnownSids.ExternallySecuredServers;
				array3[1] = (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders);
				array2[2] = WellKnownSids.HubTransportServers;
				array3[2] = permission;
				array[2] = new ReceiveConnector.PermissionGroupInfo(PermissionGroups.ExchangeServers, array2, array3);
				array[3] = new ReceiveConnector.PermissionGroupInfo(PermissionGroups.Partners, WellKnownSids.PartnerServers, Permission.SMTPSubmit | Permission.AcceptRoutingHeaders);
				if (!server.IsEdgeServer)
				{
					SecurityIdentifier sidForExchangeKnownGuid = ReceiveConnector.PermissionGroupPermissions.GetSidForExchangeKnownGuid(tenantOrRootOrgRecipientSession, WellKnownGuid.E3iWkGuid, tenantOrTopologyConfigurationSession.ConfigurationNamingContext.DistinguishedName);
					array[4] = new ReceiveConnector.PermissionGroupInfo(PermissionGroups.ExchangeLegacyServers, sidForExchangeKnownGuid, Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders);
				}
				return array;
			}

			// Token: 0x06003DD0 RID: 15824 RVA: 0x000EB668 File Offset: 0x000E9868
			internal static SecurityIdentifier GetSidForExchangeKnownGuid(IRecipientSession session, Guid knownGuid, string containerDN)
			{
				ADGroup exchangeAccount = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					exchangeAccount = session.ResolveWellKnownGuid<ADGroup>(knownGuid, containerDN);
				}, 3);
				if (!adoperationResult.Succeeded || exchangeAccount == null)
				{
					throw new ErrorExchangeGroupNotFoundException(knownGuid, adoperationResult.Exception);
				}
				return exchangeAccount.Sid;
			}

			// Token: 0x040029CD RID: 10701
			public const Permission Anonymous = Permission.SMTPSubmit | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.AcceptRoutingHeaders;

			// Token: 0x040029CE RID: 10702
			public const Permission ExchangeUsers = Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.BypassAntiSpam | Permission.AcceptRoutingHeaders;

			// Token: 0x040029CF RID: 10703
			public const Permission ExchangeServersE12 = Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders;

			// Token: 0x040029D0 RID: 10704
			public const Permission ExchangeServersE14 = Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow;

			// Token: 0x040029D1 RID: 10705
			public const Permission ExchangeServers = Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow | Permission.SMTPAcceptXProxyFrom | Permission.SMTPAcceptXSessionParams | Permission.SMTPAcceptXMessageContextADRecipientCache | Permission.SMTPAcceptXMessageContextExtendedProperties | Permission.SMTPAcceptXMessageContextFastIndex | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe;

			// Token: 0x040029D2 RID: 10706
			public const Permission ExternallySecuredServers = Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders;

			// Token: 0x040029D3 RID: 10707
			public const Permission ExchangeLegacyServers = Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders;

			// Token: 0x040029D4 RID: 10708
			public const Permission ExchangeLegacyServersExplicitDeny = Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders;

			// Token: 0x040029D5 RID: 10709
			public const Permission Partners = Permission.SMTPSubmit | Permission.AcceptRoutingHeaders;

			// Token: 0x040029D6 RID: 10710
			public const Permission AcceptCrossForestEmail = Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe;

			// Token: 0x040029D7 RID: 10711
			public const Permission AcceptCloudServicesEmail = Permission.SMTPSubmit | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.AcceptRoutingHeaders;

			// Token: 0x040029D8 RID: 10712
			public const Permission AllowSubmit = Permission.SMTPSubmit | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.AcceptRoutingHeaders;
		}

		// Token: 0x0200055A RID: 1370
		internal class PermissionGroupInfo
		{
			// Token: 0x06003DD1 RID: 15825 RVA: 0x000EB6D8 File Offset: 0x000E98D8
			public PermissionGroupInfo(PermissionGroups permissionGroup, SecurityIdentifier sid, Permission defaultPermission)
			{
				this.PermissionGroup = permissionGroup;
				this.Sids = new SecurityIdentifier[]
				{
					sid
				};
				this.DefaultPermission = new Permission[]
				{
					defaultPermission
				};
			}

			// Token: 0x06003DD2 RID: 15826 RVA: 0x000EB716 File Offset: 0x000E9916
			public PermissionGroupInfo(PermissionGroups permissionGroup, SecurityIdentifier[] sid, Permission[] defaultPermission)
			{
				if (sid.Length != defaultPermission.Length)
				{
					throw new ArgumentException("Bug: number of sid must match number of defaultPermission");
				}
				this.PermissionGroup = permissionGroup;
				this.Sids = sid;
				this.DefaultPermission = defaultPermission;
			}

			// Token: 0x040029D9 RID: 10713
			public readonly PermissionGroups PermissionGroup;

			// Token: 0x040029DA RID: 10714
			public readonly SecurityIdentifier[] Sids;

			// Token: 0x040029DB RID: 10715
			public readonly Permission[] DefaultPermission;
		}
	}
}
