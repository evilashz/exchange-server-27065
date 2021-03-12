using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200034A RID: 842
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetServiceConfigurationType : BaseRequestType
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x00029659 File Offset: 0x00027859
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x00029661 File Offset: 0x00027861
		public EmailAddressType ActingAs
		{
			get
			{
				return this.actingAsField;
			}
			set
			{
				this.actingAsField = value;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0002966A File Offset: 0x0002786A
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x00029672 File Offset: 0x00027872
		[XmlArrayItem("ConfigurationName", IsNullable = false)]
		public ServiceConfigurationType[] RequestedConfiguration
		{
			get
			{
				return this.requestedConfigurationField;
			}
			set
			{
				this.requestedConfigurationField = value;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0002967B File Offset: 0x0002787B
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x00029683 File Offset: 0x00027883
		public ConfigurationRequestDetailsType ConfigurationRequestDetails
		{
			get
			{
				return this.configurationRequestDetailsField;
			}
			set
			{
				this.configurationRequestDetailsField = value;
			}
		}

		// Token: 0x04001239 RID: 4665
		private EmailAddressType actingAsField;

		// Token: 0x0400123A RID: 4666
		private ServiceConfigurationType[] requestedConfigurationField;

		// Token: 0x0400123B RID: 4667
		private ConfigurationRequestDetailsType configurationRequestDetailsField;
	}
}
