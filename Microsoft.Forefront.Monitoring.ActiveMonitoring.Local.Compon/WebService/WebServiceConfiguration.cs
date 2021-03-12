using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x0200028F RID: 655
	public class WebServiceConfiguration
	{
		// Token: 0x06001589 RID: 5513 RVA: 0x00044BF4 File Offset: 0x00042DF4
		public WebServiceConfiguration()
		{
			this.BindingType = "WSHttpBinding";
			this.CloseTimeout = TimeSpan.MaxValue;
			this.OpenTimeout = TimeSpan.MaxValue;
			this.ReceiveTimeout = TimeSpan.MaxValue;
			this.SendTimeout = TimeSpan.MaxValue;
			this.InactivityTimeout = TimeSpan.MaxValue;
			this.AllowCookies = null;
			this.BypassProxyOnLocal = null;
			this.TransactionFlow = null;
			this.Ordered = null;
			this.ReliableSessionEnabled = null;
			this.EstablishSecurityContext = null;
			this.NegotiateServiceCredential = null;
			this.MaxConnections = 0;
			this.MaxDepth = 0;
			this.MaxStringContentLength = 0;
			this.MaxArrayLength = 0;
			this.MaxBytesPerRead = 0;
			this.MaxNameTableCharCount = 0;
			this.MaxReceivedMessageSize = 0L;
			this.maxBufferPoolSize = 0;
			this.MaxBufferSize = 0;
			this.FwLinkEnabled = false;
			this.WebProxyEnabled = false;
			this.UseDefaultWebProxy = true;
			this.WebProxyPort = 80;
			this.WebProxyCredentialsRequired = false;
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00044CE6 File Offset: 0x00042EE6
		// (set) Token: 0x0600158B RID: 5515 RVA: 0x00044CEE File Offset: 0x00042EEE
		internal string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00044CF7 File Offset: 0x00042EF7
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x00044CFF File Offset: 0x00042EFF
		internal string BindingType
		{
			get
			{
				return this.bindingType;
			}
			set
			{
				this.bindingType = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00044D08 File Offset: 0x00042F08
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x00044D10 File Offset: 0x00042F10
		internal string AllowCookies
		{
			get
			{
				return this.allowCookies;
			}
			set
			{
				this.allowCookies = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00044D19 File Offset: 0x00042F19
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x00044D21 File Offset: 0x00042F21
		internal string BypassProxyOnLocal
		{
			get
			{
				return this.bypassProxyOnLocal;
			}
			set
			{
				this.bypassProxyOnLocal = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x00044D2A File Offset: 0x00042F2A
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x00044D32 File Offset: 0x00042F32
		internal string ClientBaseAddress
		{
			get
			{
				return this.clientBaseAddress;
			}
			set
			{
				this.clientBaseAddress = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00044D3B File Offset: 0x00042F3B
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x00044D43 File Offset: 0x00042F43
		internal TimeSpan CloseTimeout
		{
			get
			{
				return this.closeTimeout;
			}
			set
			{
				this.closeTimeout = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00044D4C File Offset: 0x00042F4C
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x00044D54 File Offset: 0x00042F54
		internal string HostNameComparisonMode
		{
			get
			{
				return this.hostNameComparisonMode;
			}
			set
			{
				this.hostNameComparisonMode = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x00044D5D File Offset: 0x00042F5D
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x00044D65 File Offset: 0x00042F65
		internal int ListenBacklog
		{
			get
			{
				return this.listenBacklog;
			}
			set
			{
				this.listenBacklog = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00044D6E File Offset: 0x00042F6E
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x00044D76 File Offset: 0x00042F76
		internal int MaxBufferPoolSize
		{
			get
			{
				return this.maxBufferPoolSize;
			}
			set
			{
				this.maxBufferPoolSize = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00044D7F File Offset: 0x00042F7F
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x00044D87 File Offset: 0x00042F87
		internal int MaxBufferSize
		{
			get
			{
				return this.maxBufferSize;
			}
			set
			{
				this.maxBufferSize = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00044D90 File Offset: 0x00042F90
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x00044D98 File Offset: 0x00042F98
		internal int MaxConnections
		{
			get
			{
				return this.maxConnections;
			}
			set
			{
				this.maxConnections = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00044DA1 File Offset: 0x00042FA1
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00044DA9 File Offset: 0x00042FA9
		internal long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
			set
			{
				this.maxReceivedMessageSize = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00044DB2 File Offset: 0x00042FB2
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x00044DBA File Offset: 0x00042FBA
		internal string MessageEncoding
		{
			get
			{
				return this.messageEncoding;
			}
			set
			{
				this.messageEncoding = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00044DC3 File Offset: 0x00042FC3
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x00044DCB File Offset: 0x00042FCB
		internal string BindingName
		{
			get
			{
				return this.bindingName;
			}
			set
			{
				this.bindingName = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00044DD4 File Offset: 0x00042FD4
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x00044DDC File Offset: 0x00042FDC
		internal string BindingNamespace
		{
			get
			{
				return this.bindingNamespace;
			}
			set
			{
				this.bindingNamespace = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00044DE5 File Offset: 0x00042FE5
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00044DED File Offset: 0x00042FED
		internal TimeSpan OpenTimeout
		{
			get
			{
				return this.openTimeout;
			}
			set
			{
				this.openTimeout = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00044DF6 File Offset: 0x00042FF6
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x00044DFE File Offset: 0x00042FFE
		internal TimeSpan ReceiveTimeout
		{
			get
			{
				return this.receiveTimeout;
			}
			set
			{
				this.receiveTimeout = value;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00044E07 File Offset: 0x00043007
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x00044E0F File Offset: 0x0004300F
		internal TimeSpan SendTimeout
		{
			get
			{
				return this.sendTimeout;
			}
			set
			{
				this.sendTimeout = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x00044E18 File Offset: 0x00043018
		// (set) Token: 0x060015AF RID: 5551 RVA: 0x00044E20 File Offset: 0x00043020
		internal string TextEncoding
		{
			get
			{
				return this.textEncoding;
			}
			set
			{
				this.textEncoding = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00044E29 File Offset: 0x00043029
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x00044E31 File Offset: 0x00043031
		internal string TransactionFlow
		{
			get
			{
				return this.transactionFlow;
			}
			set
			{
				this.transactionFlow = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00044E3A File Offset: 0x0004303A
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x00044E42 File Offset: 0x00043042
		internal string TransferMode
		{
			get
			{
				return this.transferMode;
			}
			set
			{
				this.transferMode = value;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00044E4B File Offset: 0x0004304B
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x00044E53 File Offset: 0x00043053
		internal string TransactionProtocol
		{
			get
			{
				return this.transactionProtocol;
			}
			set
			{
				this.transactionProtocol = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00044E5C File Offset: 0x0004305C
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x00044E64 File Offset: 0x00043064
		internal int MaxDepth
		{
			get
			{
				return this.maxDepth;
			}
			set
			{
				this.maxDepth = value;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00044E6D File Offset: 0x0004306D
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x00044E75 File Offset: 0x00043075
		internal int MaxStringContentLength
		{
			get
			{
				return this.maxStringContentLength;
			}
			set
			{
				this.maxStringContentLength = value;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00044E7E File Offset: 0x0004307E
		// (set) Token: 0x060015BB RID: 5563 RVA: 0x00044E86 File Offset: 0x00043086
		internal int MaxArrayLength
		{
			get
			{
				return this.maxArrayLength;
			}
			set
			{
				this.maxArrayLength = value;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00044E8F File Offset: 0x0004308F
		// (set) Token: 0x060015BD RID: 5565 RVA: 0x00044E97 File Offset: 0x00043097
		internal int MaxBytesPerRead
		{
			get
			{
				return this.maxBytesPerRead;
			}
			set
			{
				this.maxBytesPerRead = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00044EA0 File Offset: 0x000430A0
		// (set) Token: 0x060015BF RID: 5567 RVA: 0x00044EA8 File Offset: 0x000430A8
		internal int MaxNameTableCharCount
		{
			get
			{
				return this.maxNameTableCharCount;
			}
			set
			{
				this.maxNameTableCharCount = value;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00044EB1 File Offset: 0x000430B1
		// (set) Token: 0x060015C1 RID: 5569 RVA: 0x00044EB9 File Offset: 0x000430B9
		internal string Ordered
		{
			get
			{
				return this.ordered;
			}
			set
			{
				this.ordered = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00044EC2 File Offset: 0x000430C2
		// (set) Token: 0x060015C3 RID: 5571 RVA: 0x00044ECA File Offset: 0x000430CA
		internal TimeSpan InactivityTimeout
		{
			get
			{
				return this.inactivityTimeout;
			}
			set
			{
				this.inactivityTimeout = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00044ED3 File Offset: 0x000430D3
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x00044EDB File Offset: 0x000430DB
		internal bool? ReliableSessionEnabled
		{
			get
			{
				return this.reliableSessionEnabled;
			}
			set
			{
				this.reliableSessionEnabled = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x00044EE4 File Offset: 0x000430E4
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x00044EEC File Offset: 0x000430EC
		internal string SecurityMode
		{
			get
			{
				return this.securityMode;
			}
			set
			{
				this.securityMode = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00044EF8 File Offset: 0x000430F8
		internal bool UseCertificateAuthentication
		{
			get
			{
				return (!string.IsNullOrWhiteSpace(this.MessageCredentialType) && this.MessageCredentialType.Equals("Certificate", StringComparison.OrdinalIgnoreCase)) || (!string.IsNullOrWhiteSpace(this.TransportCredentialType) && this.TransportCredentialType.Equals("Certificate", StringComparison.OrdinalIgnoreCase));
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x00044F47 File Offset: 0x00043147
		internal bool UseUserNameAuthentication
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.MessageCredentialType) && this.MessageCredentialType.Equals("UserName", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x00044F69 File Offset: 0x00043169
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x00044F71 File Offset: 0x00043171
		internal string TransportCredentialType
		{
			get
			{
				return this.transportCredentialType;
			}
			set
			{
				this.transportCredentialType = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00044F7A File Offset: 0x0004317A
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x00044F82 File Offset: 0x00043182
		internal string Realm
		{
			get
			{
				return this.realm;
			}
			set
			{
				this.realm = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00044F8B File Offset: 0x0004318B
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x00044F93 File Offset: 0x00043193
		internal string ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				this.protectionLevel = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00044F9C File Offset: 0x0004319C
		// (set) Token: 0x060015D1 RID: 5585 RVA: 0x00044FA4 File Offset: 0x000431A4
		internal string MessageCredentialType
		{
			get
			{
				return this.messageCredentialType;
			}
			set
			{
				this.messageCredentialType = value;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x00044FAD File Offset: 0x000431AD
		// (set) Token: 0x060015D3 RID: 5587 RVA: 0x00044FB5 File Offset: 0x000431B5
		internal string AlgorithmSuite
		{
			get
			{
				return this.algorithmSuite;
			}
			set
			{
				this.algorithmSuite = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00044FBE File Offset: 0x000431BE
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x00044FC6 File Offset: 0x000431C6
		internal string EstablishSecurityContext
		{
			get
			{
				return this.establishSecurityContext;
			}
			set
			{
				this.establishSecurityContext = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00044FCF File Offset: 0x000431CF
		// (set) Token: 0x060015D7 RID: 5591 RVA: 0x00044FD7 File Offset: 0x000431D7
		internal string NegotiateServiceCredential
		{
			get
			{
				return this.negotiateServiceCredential;
			}
			set
			{
				this.negotiateServiceCredential = value;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00044FE0 File Offset: 0x000431E0
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00044FE8 File Offset: 0x000431E8
		internal string Username
		{
			get
			{
				return this.username;
			}
			set
			{
				this.username = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00044FF1 File Offset: 0x000431F1
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x00044FF9 File Offset: 0x000431F9
		internal string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00045002 File Offset: 0x00043202
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x0004500A File Offset: 0x0004320A
		internal StoreLocation StoreLocation
		{
			get
			{
				return this.storeLocation;
			}
			set
			{
				this.storeLocation = value;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00045013 File Offset: 0x00043213
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0004501B File Offset: 0x0004321B
		internal StoreName StoreName
		{
			get
			{
				return this.storeName;
			}
			set
			{
				this.storeName = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00045024 File Offset: 0x00043224
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x0004502C File Offset: 0x0004322C
		internal X509FindType X509FindType
		{
			get
			{
				return this.x509FindType;
			}
			set
			{
				this.x509FindType = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00045035 File Offset: 0x00043235
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0004503D File Offset: 0x0004323D
		internal string FindValue
		{
			get
			{
				return this.findValue;
			}
			set
			{
				this.findValue = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00045046 File Offset: 0x00043246
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0004504E File Offset: 0x0004324E
		internal string ServiceCertificateValidationMode
		{
			get
			{
				return this.serviceCertificateValidationMode;
			}
			set
			{
				this.serviceCertificateValidationMode = value;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00045057 File Offset: 0x00043257
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0004505F File Offset: 0x0004325F
		internal string ProxyClassName
		{
			get
			{
				return this.proxyClassName;
			}
			set
			{
				this.proxyClassName = value;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00045068 File Offset: 0x00043268
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x00045070 File Offset: 0x00043270
		internal string ProxyValidatorClassName
		{
			get
			{
				return this.proxyValidatorClassName;
			}
			set
			{
				this.proxyValidatorClassName = value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00045079 File Offset: 0x00043279
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x00045081 File Offset: 0x00043281
		internal string ProxyValidatorMethodName
		{
			get
			{
				return this.proxyValidatorMethodName;
			}
			set
			{
				this.proxyValidatorMethodName = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0004508A File Offset: 0x0004328A
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x00045092 File Offset: 0x00043292
		internal string ProxyDiagnosticsInfoMethodName
		{
			get
			{
				return this.proxyDiagnosticsInfoMethodName;
			}
			set
			{
				this.proxyDiagnosticsInfoMethodName = value;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0004509B File Offset: 0x0004329B
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x000450A3 File Offset: 0x000432A3
		internal bool DumpDiagnosticsInfoOnSuccess
		{
			get
			{
				return this.dumpDiagnosticsInfoOnSuccess;
			}
			set
			{
				this.dumpDiagnosticsInfoOnSuccess = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000450AC File Offset: 0x000432AC
		// (set) Token: 0x060015F1 RID: 5617 RVA: 0x000450B4 File Offset: 0x000432B4
		internal string ProxyAssembly
		{
			get
			{
				return this.proxyAssembly;
			}
			set
			{
				this.proxyAssembly = value;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x000450BD File Offset: 0x000432BD
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x000450C5 File Offset: 0x000432C5
		internal bool ProxyGenerated
		{
			get
			{
				return this.proxyGenerated;
			}
			set
			{
				this.proxyGenerated = value;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000450CE File Offset: 0x000432CE
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x000450D6 File Offset: 0x000432D6
		internal bool ProxyInstanceMethodIsStatic
		{
			get
			{
				return this.proxyInstanceMethodIsStatic;
			}
			set
			{
				this.proxyInstanceMethodIsStatic = value;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000450DF File Offset: 0x000432DF
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x000450E7 File Offset: 0x000432E7
		internal Operation ProxyInstanceMethod
		{
			get
			{
				return this.proxyInstanceMethod;
			}
			set
			{
				this.proxyInstanceMethod = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x000450F0 File Offset: 0x000432F0
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x000450F8 File Offset: 0x000432F8
		internal bool WebProxyEnabled
		{
			get
			{
				return this.webProxyEnabled;
			}
			set
			{
				this.webProxyEnabled = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00045101 File Offset: 0x00043301
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x00045109 File Offset: 0x00043309
		internal bool UseDefaultWebProxy
		{
			get
			{
				return this.useDefaultWebProxy;
			}
			set
			{
				this.useDefaultWebProxy = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00045112 File Offset: 0x00043312
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x0004511A File Offset: 0x0004331A
		internal string WebProxyServerUri
		{
			get
			{
				return this.webProxyServerUri;
			}
			set
			{
				this.webProxyServerUri = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00045123 File Offset: 0x00043323
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x0004512B File Offset: 0x0004332B
		internal int WebProxyPort
		{
			get
			{
				return this.webProxyPort;
			}
			set
			{
				this.webProxyPort = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00045134 File Offset: 0x00043334
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x0004513C File Offset: 0x0004333C
		internal bool WebProxyCredentialsRequired
		{
			get
			{
				return this.webProxyCredentialsRequired;
			}
			set
			{
				this.webProxyCredentialsRequired = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00045145 File Offset: 0x00043345
		// (set) Token: 0x06001603 RID: 5635 RVA: 0x0004514D File Offset: 0x0004334D
		internal string WebProxyUsername
		{
			get
			{
				return this.webProxyUsername;
			}
			set
			{
				this.webProxyUsername = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00045156 File Offset: 0x00043356
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x0004515E File Offset: 0x0004335E
		internal string WebProxyPassword
		{
			get
			{
				return this.webProxyPassword;
			}
			set
			{
				this.webProxyPassword = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00045167 File Offset: 0x00043367
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x0004516F File Offset: 0x0004336F
		internal string WebProxyDomain
		{
			get
			{
				return this.webProxyDomain;
			}
			set
			{
				this.webProxyDomain = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00045178 File Offset: 0x00043378
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x00045180 File Offset: 0x00043380
		internal bool FwLinkEnabled
		{
			get
			{
				return this.fwLinkEnabled;
			}
			set
			{
				this.fwLinkEnabled = value;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00045189 File Offset: 0x00043389
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x00045191 File Offset: 0x00043391
		internal string FwLinkUri
		{
			get
			{
				return this.fwLinkUri;
			}
			set
			{
				this.fwLinkUri = value;
			}
		}

		// Token: 0x04000A75 RID: 2677
		private string uri;

		// Token: 0x04000A76 RID: 2678
		private string bindingType;

		// Token: 0x04000A77 RID: 2679
		private string allowCookies;

		// Token: 0x04000A78 RID: 2680
		private string bypassProxyOnLocal;

		// Token: 0x04000A79 RID: 2681
		private string clientBaseAddress;

		// Token: 0x04000A7A RID: 2682
		private TimeSpan closeTimeout;

		// Token: 0x04000A7B RID: 2683
		private string hostNameComparisonMode;

		// Token: 0x04000A7C RID: 2684
		private int listenBacklog;

		// Token: 0x04000A7D RID: 2685
		private int maxConnections;

		// Token: 0x04000A7E RID: 2686
		private int maxBufferPoolSize;

		// Token: 0x04000A7F RID: 2687
		private int maxBufferSize;

		// Token: 0x04000A80 RID: 2688
		private long maxReceivedMessageSize;

		// Token: 0x04000A81 RID: 2689
		private string messageEncoding;

		// Token: 0x04000A82 RID: 2690
		private string bindingName;

		// Token: 0x04000A83 RID: 2691
		private string bindingNamespace;

		// Token: 0x04000A84 RID: 2692
		private TimeSpan openTimeout;

		// Token: 0x04000A85 RID: 2693
		private TimeSpan receiveTimeout;

		// Token: 0x04000A86 RID: 2694
		private TimeSpan sendTimeout;

		// Token: 0x04000A87 RID: 2695
		private string textEncoding;

		// Token: 0x04000A88 RID: 2696
		private string transactionFlow;

		// Token: 0x04000A89 RID: 2697
		private string transferMode;

		// Token: 0x04000A8A RID: 2698
		private string transactionProtocol;

		// Token: 0x04000A8B RID: 2699
		private int maxDepth;

		// Token: 0x04000A8C RID: 2700
		private int maxStringContentLength;

		// Token: 0x04000A8D RID: 2701
		private int maxArrayLength;

		// Token: 0x04000A8E RID: 2702
		private int maxBytesPerRead;

		// Token: 0x04000A8F RID: 2703
		private int maxNameTableCharCount;

		// Token: 0x04000A90 RID: 2704
		private string ordered;

		// Token: 0x04000A91 RID: 2705
		private TimeSpan inactivityTimeout;

		// Token: 0x04000A92 RID: 2706
		private bool? reliableSessionEnabled;

		// Token: 0x04000A93 RID: 2707
		private string securityMode;

		// Token: 0x04000A94 RID: 2708
		private string transportCredentialType;

		// Token: 0x04000A95 RID: 2709
		private string realm;

		// Token: 0x04000A96 RID: 2710
		private string protectionLevel;

		// Token: 0x04000A97 RID: 2711
		private string messageCredentialType;

		// Token: 0x04000A98 RID: 2712
		private string algorithmSuite;

		// Token: 0x04000A99 RID: 2713
		private string establishSecurityContext;

		// Token: 0x04000A9A RID: 2714
		private string negotiateServiceCredential;

		// Token: 0x04000A9B RID: 2715
		private string username;

		// Token: 0x04000A9C RID: 2716
		private string password;

		// Token: 0x04000A9D RID: 2717
		private StoreLocation storeLocation;

		// Token: 0x04000A9E RID: 2718
		private StoreName storeName;

		// Token: 0x04000A9F RID: 2719
		private X509FindType x509FindType;

		// Token: 0x04000AA0 RID: 2720
		private string findValue;

		// Token: 0x04000AA1 RID: 2721
		private string serviceCertificateValidationMode;

		// Token: 0x04000AA2 RID: 2722
		private string proxyClassName;

		// Token: 0x04000AA3 RID: 2723
		private string proxyValidatorClassName;

		// Token: 0x04000AA4 RID: 2724
		private string proxyValidatorMethodName;

		// Token: 0x04000AA5 RID: 2725
		private string proxyDiagnosticsInfoMethodName;

		// Token: 0x04000AA6 RID: 2726
		private bool dumpDiagnosticsInfoOnSuccess;

		// Token: 0x04000AA7 RID: 2727
		private string proxyAssembly;

		// Token: 0x04000AA8 RID: 2728
		private bool proxyGenerated;

		// Token: 0x04000AA9 RID: 2729
		private bool proxyInstanceMethodIsStatic;

		// Token: 0x04000AAA RID: 2730
		private Operation proxyInstanceMethod;

		// Token: 0x04000AAB RID: 2731
		private bool webProxyEnabled;

		// Token: 0x04000AAC RID: 2732
		private bool useDefaultWebProxy;

		// Token: 0x04000AAD RID: 2733
		private string webProxyServerUri;

		// Token: 0x04000AAE RID: 2734
		private int webProxyPort;

		// Token: 0x04000AAF RID: 2735
		private bool webProxyCredentialsRequired;

		// Token: 0x04000AB0 RID: 2736
		private string webProxyUsername;

		// Token: 0x04000AB1 RID: 2737
		private string webProxyPassword;

		// Token: 0x04000AB2 RID: 2738
		private string webProxyDomain;

		// Token: 0x04000AB3 RID: 2739
		private bool fwLinkEnabled;

		// Token: 0x04000AB4 RID: 2740
		private string fwLinkUri;
	}
}
