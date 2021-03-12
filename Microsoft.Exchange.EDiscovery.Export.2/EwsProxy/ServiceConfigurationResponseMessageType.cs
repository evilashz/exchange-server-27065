using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C1 RID: 449
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ServiceConfigurationResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x000252DF File Offset: 0x000234DF
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x000252E7 File Offset: 0x000234E7
		public MailTipsServiceConfiguration MailTipsConfiguration
		{
			get
			{
				return this.mailTipsConfigurationField;
			}
			set
			{
				this.mailTipsConfigurationField = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x000252F0 File Offset: 0x000234F0
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x000252F8 File Offset: 0x000234F8
		public UnifiedMessageServiceConfiguration UnifiedMessagingConfiguration
		{
			get
			{
				return this.unifiedMessagingConfigurationField;
			}
			set
			{
				this.unifiedMessagingConfigurationField = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00025301 File Offset: 0x00023501
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x00025309 File Offset: 0x00023509
		public ProtectionRulesServiceConfiguration ProtectionRulesConfiguration
		{
			get
			{
				return this.protectionRulesConfigurationField;
			}
			set
			{
				this.protectionRulesConfigurationField = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00025312 File Offset: 0x00023512
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x0002531A File Offset: 0x0002351A
		public PolicyNudgeRulesServiceConfiguration PolicyNudgeRulesConfiguration
		{
			get
			{
				return this.policyNudgeRulesConfigurationField;
			}
			set
			{
				this.policyNudgeRulesConfigurationField = value;
			}
		}

		// Token: 0x04000D66 RID: 3430
		private MailTipsServiceConfiguration mailTipsConfigurationField;

		// Token: 0x04000D67 RID: 3431
		private UnifiedMessageServiceConfiguration unifiedMessagingConfigurationField;

		// Token: 0x04000D68 RID: 3432
		private ProtectionRulesServiceConfiguration protectionRulesConfigurationField;

		// Token: 0x04000D69 RID: 3433
		private PolicyNudgeRulesServiceConfiguration policyNudgeRulesConfigurationField;
	}
}
