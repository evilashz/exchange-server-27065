using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000251 RID: 593
	public class SmtpConnectionProbeWorkDefinition
	{
		// Token: 0x060013DC RID: 5084 RVA: 0x0003AC7B File Offset: 0x00038E7B
		public SmtpConnectionProbeWorkDefinition(string xml, bool loadFromXml = true)
		{
			this.LoadDefaultConfiguration();
			if (loadFromXml)
			{
				this.LoadContext(xml);
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0003AC93 File Offset: 0x00038E93
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0003AC9B File Offset: 0x00038E9B
		public bool ResolveEndPoint
		{
			get
			{
				return this.resolveEndPoint;
			}
			internal set
			{
				this.resolveEndPoint = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0003ACA4 File Offset: 0x00038EA4
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0003ACAC File Offset: 0x00038EAC
		public string SmtpServer
		{
			get
			{
				return this.smtpServer;
			}
			internal set
			{
				this.smtpServer = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0003ACB5 File Offset: 0x00038EB5
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x0003ACBD File Offset: 0x00038EBD
		public int Port
		{
			get
			{
				return this.port;
			}
			internal set
			{
				this.port = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0003ACC6 File Offset: 0x00038EC6
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x0003ACCE File Offset: 0x00038ECE
		public AuthenticationType AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
			internal set
			{
				this.authenticationType = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0003ACD7 File Offset: 0x00038ED7
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0003ACDF File Offset: 0x00038EDF
		public ConnectionLostPoint ExpectedConnectionLostPoint
		{
			get
			{
				return this.expectedConnectionLostPoint;
			}
			internal set
			{
				this.expectedConnectionLostPoint = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0003ACE8 File Offset: 0x00038EE8
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0003ACF0 File Offset: 0x00038EF0
		public string HeloDomain
		{
			get
			{
				return this.heloDomain;
			}
			internal set
			{
				this.heloDomain = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0003ACF9 File Offset: 0x00038EF9
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x0003AD01 File Offset: 0x00038F01
		public Account AuthenticationAccount
		{
			get
			{
				return this.authenticationAccount;
			}
			internal set
			{
				this.authenticationAccount = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0003AD0A File Offset: 0x00038F0A
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x0003AD12 File Offset: 0x00038F12
		public string MailFrom
		{
			get
			{
				return this.mailFrom;
			}
			internal set
			{
				this.mailFrom = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0003AD1B File Offset: 0x00038F1B
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x0003AD23 File Offset: 0x00038F23
		public ICollection<SmtpRecipient> MailTo
		{
			get
			{
				return this.mailTo;
			}
			internal set
			{
				this.mailTo = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0003AD2C File Offset: 0x00038F2C
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x0003AD34 File Offset: 0x00038F34
		public bool UseSsl
		{
			get
			{
				return this.useSsl;
			}
			internal set
			{
				this.useSsl = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0003AD3D File Offset: 0x00038F3D
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x0003AD45 File Offset: 0x00038F45
		public string Data
		{
			get
			{
				return this.data;
			}
			internal set
			{
				this.data = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0003AD4E File Offset: 0x00038F4E
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0003AD56 File Offset: 0x00038F56
		public SmtpExpectedResponse ExpectedResponseOnConnect
		{
			get
			{
				return this.expectedResponseOnConnect;
			}
			internal set
			{
				this.expectedResponseOnConnect = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0003AD5F File Offset: 0x00038F5F
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0003AD67 File Offset: 0x00038F67
		public SmtpExpectedResponse ExpectedResponseOnHelo
		{
			get
			{
				return this.expectedResponseOnHelo;
			}
			internal set
			{
				this.expectedResponseOnHelo = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0003AD70 File Offset: 0x00038F70
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x0003AD78 File Offset: 0x00038F78
		public SmtpExpectedResponse ExpectedResponseOnStartTls
		{
			get
			{
				return this.expectedResponseOnStartTls;
			}
			internal set
			{
				this.expectedResponseOnStartTls = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0003AD81 File Offset: 0x00038F81
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x0003AD89 File Offset: 0x00038F89
		public SmtpExpectedResponse ExpectedResponseOnHeloAfterStartTls
		{
			get
			{
				return this.expectedResponseOnHeloAfterStartTls;
			}
			internal set
			{
				this.expectedResponseOnHeloAfterStartTls = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0003AD92 File Offset: 0x00038F92
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x0003AD9A File Offset: 0x00038F9A
		public SmtpExpectedResponse ExpectedResponseOnAuthenticate
		{
			get
			{
				return this.expectedResponseOnAuthenticate;
			}
			internal set
			{
				this.expectedResponseOnAuthenticate = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0003ADA3 File Offset: 0x00038FA3
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x0003ADAB File Offset: 0x00038FAB
		public SmtpExpectedResponse ExpectedResponseOnMailFrom
		{
			get
			{
				return this.expectedResponseOnMailFrom;
			}
			internal set
			{
				this.expectedResponseOnMailFrom = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0003ADB4 File Offset: 0x00038FB4
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x0003ADBC File Offset: 0x00038FBC
		public SmtpExpectedResponse ExpectedResponseOnData
		{
			get
			{
				return this.expectedResponseOnData;
			}
			internal set
			{
				this.expectedResponseOnData = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0003ADC5 File Offset: 0x00038FC5
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x0003ADCD File Offset: 0x00038FCD
		public bool ClientCertificateValid
		{
			get
			{
				return this.clientCertificateValid;
			}
			internal set
			{
				this.clientCertificateValid = value;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0003ADD6 File Offset: 0x00038FD6
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x0003ADDE File Offset: 0x00038FDE
		public ClientCertificateCriteria ClientCertificate
		{
			get
			{
				return this.clientCertificate;
			}
			internal set
			{
				this.clientCertificate = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0003ADE7 File Offset: 0x00038FE7
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0003ADEF File Offset: 0x00038FEF
		public bool ExpectedServerCertificateValid
		{
			get
			{
				return this.expectedServerCertificateValid;
			}
			internal set
			{
				this.expectedServerCertificateValid = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0003AE00 File Offset: 0x00039000
		public SmtpConnectionProbeWorkDefinition.CertificateProperties ExpectedServerCertificate
		{
			get
			{
				return this.expectedServerCertificate;
			}
			internal set
			{
				this.expectedServerCertificate = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0003AE09 File Offset: 0x00039009
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x0003AE11 File Offset: 0x00039011
		public ICollection<SmtpCustomCommand> CustomCommands
		{
			get
			{
				return this.customCommands;
			}
			internal set
			{
				this.customCommands = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0003AE1A File Offset: 0x0003901A
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x0003AE22 File Offset: 0x00039022
		public bool IgnoreCertificateNameMismatchPolicyError
		{
			get
			{
				return this.ignoreCertificateNameMismatchPolicyError;
			}
			internal set
			{
				this.ignoreCertificateNameMismatchPolicyError = value;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0003AE2B File Offset: 0x0003902B
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x0003AE33 File Offset: 0x00039033
		public bool IgnoreCertificateChainPolicyErrorForSelfSigned
		{
			get
			{
				return this.ignoreCertificateChainPolicyErrorForSelfSigned;
			}
			internal set
			{
				this.ignoreCertificateChainPolicyErrorForSelfSigned = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0003AE3C File Offset: 0x0003903C
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x0003AE44 File Offset: 0x00039044
		public string SenderTenantID
		{
			get
			{
				return this.senderTenantID;
			}
			internal set
			{
				this.senderTenantID = value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0003AE4D File Offset: 0x0003904D
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x0003AE55 File Offset: 0x00039055
		public string RecipientTenantID
		{
			get
			{
				return this.recipientTenantID;
			}
			internal set
			{
				this.recipientTenantID = value;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0003AE5E File Offset: 0x0003905E
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x0003AE66 File Offset: 0x00039066
		public bool AddAttributions
		{
			get
			{
				return this.addAttributions;
			}
			internal set
			{
				this.addAttributions = value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0003AE6F File Offset: 0x0003906F
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x0003AE77 File Offset: 0x00039077
		public Directionality Direction
		{
			get
			{
				return this.direction;
			}
			internal set
			{
				this.direction = value;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0003AE80 File Offset: 0x00039080
		public void LoadDefaultConfiguration()
		{
			this.smtpServer = "127.0.0.1";
			this.resolveEndPoint = false;
			this.port = 25;
			this.useSsl = false;
			this.ignoreCertificateNameMismatchPolicyError = false;
			this.ignoreCertificateChainPolicyErrorForSelfSigned = false;
			this.authenticationType = AuthenticationType.Anonymous;
			this.expectedConnectionLostPoint = ConnectionLostPoint.None;
			this.heloDomain = string.Empty;
			this.mailFrom = string.Empty;
			this.mailTo = new List<SmtpRecipient>();
			this.data = string.Empty;
			this.addAttributions = true;
			this.direction = Directionality.Incoming;
			this.expectedResponseOnConnect = new SmtpExpectedResponse
			{
				Type = ExpectedResponseType.ResponseCode,
				ResponseCode = SimpleSmtpClient.SmtpResponseCode.ServiceReady
			};
			this.expectedResponseOnHelo = new SmtpExpectedResponse
			{
				Type = ExpectedResponseType.ResponseCode,
				ResponseCode = SimpleSmtpClient.SmtpResponseCode.OK
			};
			this.expectedResponseOnStartTls = new SmtpExpectedResponse
			{
				Type = ExpectedResponseType.ResponseCode,
				ResponseCode = SimpleSmtpClient.SmtpResponseCode.ServiceReady
			};
			this.expectedResponseOnHeloAfterStartTls = new SmtpExpectedResponse
			{
				Type = ExpectedResponseType.ResponseCode,
				ResponseCode = SimpleSmtpClient.SmtpResponseCode.OK
			};
			this.expectedResponseOnAuthenticate = new SmtpExpectedResponse
			{
				Type = ExpectedResponseType.ResponseCode,
				ResponseCode = SimpleSmtpClient.SmtpResponseCode.AuthAccepted
			};
			this.expectedResponseOnMailFrom = new SmtpExpectedResponse
			{
				Type = ExpectedResponseType.ResponseCode,
				ResponseCode = SimpleSmtpClient.SmtpResponseCode.OK
			};
			this.expectedResponseOnData = new SmtpExpectedResponse
			{
				Type = ExpectedResponseType.ResponseCode,
				ResponseCode = SimpleSmtpClient.SmtpResponseCode.OK
			};
			this.clientCertificateValid = false;
			this.clientCertificate = new ClientCertificateCriteria();
			this.expectedServerCertificateValid = false;
			this.expectedServerCertificate = new SmtpConnectionProbeWorkDefinition.CertificateProperties();
			this.customCommands = new List<SmtpCustomCommand>();
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0003B018 File Offset: 0x00039218
		public void LoadContext(string xml)
		{
			if (string.IsNullOrWhiteSpace(xml))
			{
				throw new ArgumentException("Work Definition XML is not valid.");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(xml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("//SmtpServer");
			this.smtpServer = "127.0.0.1";
			if (xmlNode != null && !string.IsNullOrWhiteSpace(xmlNode.InnerText))
			{
				this.smtpServer = xmlNode.InnerText;
			}
			XmlAttribute xmlAttribute = xmlNode.Attributes["ResolveEndPoint"];
			if (xmlAttribute != null)
			{
				this.resolveEndPoint = Utils.GetBoolean(xmlAttribute.Value, "ResolveEndPoint", false);
			}
			xmlNode = xmlDocument.SelectSingleNode("//Port");
			this.port = 25;
			if (xmlNode != null)
			{
				this.port = Utils.GetPositiveInteger(xmlNode.InnerText, "Port");
			}
			xmlNode = xmlDocument.SelectSingleNode("//UseSsl");
			if (xmlNode != null)
			{
				this.useSsl = Utils.GetBoolean(xmlNode.InnerText, "UseSsl", false);
				this.ignoreCertificateNameMismatchPolicyError = Utils.GetOptionalXmlAttribute<bool>(xmlNode, "IgnoreCertificateNameMismatchPolicyError", false);
				this.ignoreCertificateChainPolicyErrorForSelfSigned = Utils.GetOptionalXmlAttribute<bool>(xmlNode, "IgnoreCertificateChainPolicyErrorForSelfSigned", false);
			}
			xmlNode = xmlDocument.SelectSingleNode("//AuthenticationType");
			this.authenticationType = AuthenticationType.Anonymous;
			if (xmlNode != null)
			{
				this.authenticationType = Utils.GetEnumValue<AuthenticationType>(xmlNode.InnerText, "AuthenticationType");
			}
			xmlNode = xmlDocument.SelectSingleNode("//AuthenticationAccount");
			if (xmlNode != null)
			{
				this.authenticationAccount = Account.FromXml(xmlNode);
			}
			if (this.authenticationType == AuthenticationType.AuthLogin && (this.authenticationAccount == null || string.IsNullOrEmpty(this.authenticationAccount.Password)))
			{
				throw new ArgumentException(string.Format("Authentication account must be provided when authentication type is {0}.", this.authenticationType));
			}
			xmlNode = xmlDocument.SelectSingleNode("//ExpectedConnectionLostPoint");
			this.expectedConnectionLostPoint = ConnectionLostPoint.None;
			if (xmlNode != null)
			{
				this.expectedConnectionLostPoint = Utils.GetEnumValue<ConnectionLostPoint>(xmlNode.InnerText, "ExpectedConnectionLostPoint");
			}
			xmlNode = xmlDocument.SelectSingleNode("//HeloDomain");
			this.heloDomain = string.Empty;
			if (xmlNode != null)
			{
				this.heloDomain = xmlNode.InnerText;
			}
			XmlElement xmlElement = xmlDocument.SelectSingleNode("//MailFrom") as XmlElement;
			if (xmlElement != null)
			{
				this.mailFrom = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Username"), "MailFrom Username");
			}
			SmtpExpectedResponse smtpExpectedResponse = SmtpExpectedResponse.FromXml(xmlDocument.SelectSingleNode("//ExpectedResponseOnRcptTo"), "ExpectedResponseOnRcptTo", SimpleSmtpClient.SmtpResponseCode.OK, false);
			List<SmtpRecipient> list = new List<SmtpRecipient>();
			foreach (object obj in xmlDocument.SelectNodes("//MailTo"))
			{
				XmlElement xmlElement2 = (XmlElement)obj;
				SmtpExpectedResponse expectedResponse = SmtpExpectedResponse.FromXml(xmlElement2.SelectSingleNode("ExpectedResponse"), "ExpectedResponse", smtpExpectedResponse.ResponseCode, false);
				SmtpRecipient item = new SmtpRecipient
				{
					Username = Utils.CheckNullOrWhiteSpace(xmlElement2.GetAttribute("Username"), "MailTo Username"),
					ExpectedResponse = expectedResponse
				};
				list.Add(item);
			}
			this.mailTo = list;
			xmlNode = xmlDocument.SelectSingleNode("//WorkContext");
			if (xmlNode != null)
			{
				this.senderTenantID = Utils.GetOptionalXmlAttribute<string>(xmlNode, "SenderTenantID", string.Empty);
				this.recipientTenantID = Utils.GetOptionalXmlAttribute<string>(xmlNode, "RecipientTenantID", string.Empty);
			}
			xmlNode = xmlDocument.SelectSingleNode("//Data");
			if (xmlNode != null)
			{
				this.data = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "Data");
				this.addAttributions = Utils.GetOptionalXmlAttribute<bool>(xmlNode, "AddAttributions", true);
				this.direction = Utils.GetOptionalXmlEnumAttribute<Directionality>(xmlNode, "Direction", Directionality.Incoming);
			}
			this.expectedResponseOnConnect = SmtpExpectedResponse.FromXml(xmlDocument.SelectSingleNode("//ExpectedResponseOnConnect"), "ExpectedResponseOnConnect", SimpleSmtpClient.SmtpResponseCode.ServiceReady, false);
			this.expectedResponseOnHelo = SmtpExpectedResponse.FromXml(xmlDocument.SelectSingleNode("//ExpectedResponseOnHelo"), "ExpectedResponseOnHelo", SimpleSmtpClient.SmtpResponseCode.OK, false);
			this.expectedResponseOnStartTls = SmtpExpectedResponse.FromXml(xmlDocument.SelectSingleNode("//ExpectedResponseOnStartTls"), "ExpectedResponseOnStartTls", SimpleSmtpClient.SmtpResponseCode.ServiceReady, false);
			this.expectedResponseOnHeloAfterStartTls = SmtpExpectedResponse.FromXml(xmlDocument.SelectSingleNode("//ExpectedResponseOnHeloAfterStartTls"), "ExpectedResponseOnHeloAfterStartTls", SimpleSmtpClient.SmtpResponseCode.OK, false);
			this.expectedResponseOnAuthenticate = SmtpExpectedResponse.FromXml(xmlDocument.SelectSingleNode("//ExpectedResponseOnAuthenticate"), "ExpectedResponseOnAuthenticate", SimpleSmtpClient.SmtpResponseCode.AuthAccepted, false);
			this.expectedResponseOnMailFrom = SmtpExpectedResponse.FromXml(xmlDocument.SelectSingleNode("//ExpectedResponseOnMailFrom"), "ExpectedResponseOnMailFrom", SimpleSmtpClient.SmtpResponseCode.OK, false);
			this.expectedResponseOnData = SmtpExpectedResponse.FromXml(xmlDocument.SelectSingleNode("//ExpectedResponseOnData"), "ExpectedResponseOnData", SimpleSmtpClient.SmtpResponseCode.OK, false);
			this.clientCertificate = ClientCertificateCriteria.FromXml(xmlDocument.SelectSingleNode("//ClientCertificate"), out this.clientCertificateValid);
			this.expectedServerCertificate = SmtpConnectionProbeWorkDefinition.CertificateProperties.FromXml(xmlDocument.SelectSingleNode("//ExpectedServerCertificate"), out this.expectedServerCertificateValid);
			this.customCommands = SmtpCustomCommand.FromXml(xmlDocument);
		}

		// Token: 0x04000960 RID: 2400
		private bool resolveEndPoint;

		// Token: 0x04000961 RID: 2401
		private string smtpServer;

		// Token: 0x04000962 RID: 2402
		private int port;

		// Token: 0x04000963 RID: 2403
		private AuthenticationType authenticationType;

		// Token: 0x04000964 RID: 2404
		private ConnectionLostPoint expectedConnectionLostPoint;

		// Token: 0x04000965 RID: 2405
		private string heloDomain;

		// Token: 0x04000966 RID: 2406
		private Account authenticationAccount;

		// Token: 0x04000967 RID: 2407
		private string mailFrom;

		// Token: 0x04000968 RID: 2408
		private ICollection<SmtpRecipient> mailTo;

		// Token: 0x04000969 RID: 2409
		private bool useSsl;

		// Token: 0x0400096A RID: 2410
		private string data;

		// Token: 0x0400096B RID: 2411
		private SmtpExpectedResponse expectedResponseOnConnect;

		// Token: 0x0400096C RID: 2412
		private SmtpExpectedResponse expectedResponseOnHelo;

		// Token: 0x0400096D RID: 2413
		private SmtpExpectedResponse expectedResponseOnStartTls;

		// Token: 0x0400096E RID: 2414
		private SmtpExpectedResponse expectedResponseOnHeloAfterStartTls;

		// Token: 0x0400096F RID: 2415
		private SmtpExpectedResponse expectedResponseOnAuthenticate;

		// Token: 0x04000970 RID: 2416
		private SmtpExpectedResponse expectedResponseOnMailFrom;

		// Token: 0x04000971 RID: 2417
		private SmtpExpectedResponse expectedResponseOnData;

		// Token: 0x04000972 RID: 2418
		private bool clientCertificateValid;

		// Token: 0x04000973 RID: 2419
		private ClientCertificateCriteria clientCertificate;

		// Token: 0x04000974 RID: 2420
		private bool expectedServerCertificateValid;

		// Token: 0x04000975 RID: 2421
		private SmtpConnectionProbeWorkDefinition.CertificateProperties expectedServerCertificate;

		// Token: 0x04000976 RID: 2422
		private ICollection<SmtpCustomCommand> customCommands;

		// Token: 0x04000977 RID: 2423
		private bool ignoreCertificateNameMismatchPolicyError;

		// Token: 0x04000978 RID: 2424
		private bool ignoreCertificateChainPolicyErrorForSelfSigned;

		// Token: 0x04000979 RID: 2425
		private string senderTenantID;

		// Token: 0x0400097A RID: 2426
		private string recipientTenantID;

		// Token: 0x0400097B RID: 2427
		private bool addAttributions;

		// Token: 0x0400097C RID: 2428
		private Directionality direction;

		// Token: 0x02000252 RID: 594
		public class CertificateProperties
		{
			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001419 RID: 5145 RVA: 0x0003B4A8 File Offset: 0x000396A8
			// (set) Token: 0x0600141A RID: 5146 RVA: 0x0003B4B0 File Offset: 0x000396B0
			public string Subject
			{
				get
				{
					return this.subject;
				}
				internal set
				{
					this.subject = value;
				}
			}

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x0600141B RID: 5147 RVA: 0x0003B4B9 File Offset: 0x000396B9
			// (set) Token: 0x0600141C RID: 5148 RVA: 0x0003B4C1 File Offset: 0x000396C1
			public string Issuer
			{
				get
				{
					return this.issuer;
				}
				internal set
				{
					this.issuer = value;
				}
			}

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x0600141D RID: 5149 RVA: 0x0003B4CA File Offset: 0x000396CA
			// (set) Token: 0x0600141E RID: 5150 RVA: 0x0003B4D2 File Offset: 0x000396D2
			public DateTime? ValidFrom
			{
				get
				{
					return this.validFrom;
				}
				internal set
				{
					this.validFrom = value;
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x0600141F RID: 5151 RVA: 0x0003B4DB File Offset: 0x000396DB
			// (set) Token: 0x06001420 RID: 5152 RVA: 0x0003B4E3 File Offset: 0x000396E3
			public DateTime? ValidTo
			{
				get
				{
					return this.validTo;
				}
				internal set
				{
					this.validTo = value;
				}
			}

			// Token: 0x06001421 RID: 5153 RVA: 0x0003B4EC File Offset: 0x000396EC
			public static SmtpConnectionProbeWorkDefinition.CertificateProperties FromXml(XmlNode workContext, out bool validCertificate)
			{
				SmtpConnectionProbeWorkDefinition.CertificateProperties certificateProperties = null;
				validCertificate = false;
				if (workContext != null)
				{
					certificateProperties = new SmtpConnectionProbeWorkDefinition.CertificateProperties();
					bool flag = false;
					XmlNode xmlNode = workContext.SelectSingleNode("Subject");
					if (xmlNode != null)
					{
						certificateProperties.subject = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "ExpectedServerCertificate Subject");
						flag = true;
					}
					xmlNode = workContext.SelectSingleNode("Issuer");
					if (xmlNode != null)
					{
						certificateProperties.issuer = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "ExpectedServerCertificate Issuer");
						flag = true;
					}
					xmlNode = workContext.SelectSingleNode("ValidFrom");
					if (xmlNode != null)
					{
						certificateProperties.validFrom = new DateTime?(Utils.GetDateTime(xmlNode.InnerText, "ExpectedServerCertificate ValidFrom"));
						flag = true;
					}
					xmlNode = workContext.SelectSingleNode("ValidTo");
					if (xmlNode != null)
					{
						certificateProperties.validTo = new DateTime?(Utils.GetDateTime(xmlNode.InnerText, "ExpectedServerCertificate ValidTo"));
						flag = true;
					}
					if (!flag)
					{
						throw new ArgumentException("The ExpectedServerCertificate node is specified, but no valid child nodes exist.");
					}
					validCertificate = true;
				}
				return certificateProperties;
			}

			// Token: 0x0400097D RID: 2429
			private string subject;

			// Token: 0x0400097E RID: 2430
			private string issuer;

			// Token: 0x0400097F RID: 2431
			private DateTime? validFrom;

			// Token: 0x04000980 RID: 2432
			private DateTime? validTo;
		}
	}
}
