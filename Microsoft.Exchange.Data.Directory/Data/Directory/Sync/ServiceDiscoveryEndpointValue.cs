using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200090A RID: 2314
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class ServiceDiscoveryEndpointValue
	{
		// Token: 0x1700276D RID: 10093
		// (get) Token: 0x06006F01 RID: 28417 RVA: 0x0017682B File Offset: 0x00174A2B
		// (set) Token: 0x06006F02 RID: 28418 RVA: 0x00176833 File Offset: 0x00174A33
		[XmlAttribute]
		public string EndpointId
		{
			get
			{
				return this.endpointIdField;
			}
			set
			{
				this.endpointIdField = value;
			}
		}

		// Token: 0x1700276E RID: 10094
		// (get) Token: 0x06006F03 RID: 28419 RVA: 0x0017683C File Offset: 0x00174A3C
		// (set) Token: 0x06006F04 RID: 28420 RVA: 0x00176844 File Offset: 0x00174A44
		[XmlAttribute]
		public string Capability
		{
			get
			{
				return this.capabilityField;
			}
			set
			{
				this.capabilityField = value;
			}
		}

		// Token: 0x1700276F RID: 10095
		// (get) Token: 0x06006F05 RID: 28421 RVA: 0x0017684D File Offset: 0x00174A4D
		// (set) Token: 0x06006F06 RID: 28422 RVA: 0x00176855 File Offset: 0x00174A55
		[XmlAttribute]
		public string ServiceId
		{
			get
			{
				return this.serviceIdField;
			}
			set
			{
				this.serviceIdField = value;
			}
		}

		// Token: 0x17002770 RID: 10096
		// (get) Token: 0x06006F07 RID: 28423 RVA: 0x0017685E File Offset: 0x00174A5E
		// (set) Token: 0x06006F08 RID: 28424 RVA: 0x00176866 File Offset: 0x00174A66
		[XmlAttribute]
		public string ServiceName
		{
			get
			{
				return this.serviceNameField;
			}
			set
			{
				this.serviceNameField = value;
			}
		}

		// Token: 0x17002771 RID: 10097
		// (get) Token: 0x06006F09 RID: 28425 RVA: 0x0017686F File Offset: 0x00174A6F
		// (set) Token: 0x06006F0A RID: 28426 RVA: 0x00176877 File Offset: 0x00174A77
		[XmlAttribute]
		public string ServiceEndpointUri
		{
			get
			{
				return this.serviceEndpointUriField;
			}
			set
			{
				this.serviceEndpointUriField = value;
			}
		}

		// Token: 0x17002772 RID: 10098
		// (get) Token: 0x06006F0B RID: 28427 RVA: 0x00176880 File Offset: 0x00174A80
		// (set) Token: 0x06006F0C RID: 28428 RVA: 0x00176888 File Offset: 0x00174A88
		[XmlAttribute]
		public string ServiceResourceId
		{
			get
			{
				return this.serviceResourceIdField;
			}
			set
			{
				this.serviceResourceIdField = value;
			}
		}

		// Token: 0x04004819 RID: 18457
		private string endpointIdField;

		// Token: 0x0400481A RID: 18458
		private string capabilityField;

		// Token: 0x0400481B RID: 18459
		private string serviceIdField;

		// Token: 0x0400481C RID: 18460
		private string serviceNameField;

		// Token: 0x0400481D RID: 18461
		private string serviceEndpointUriField;

		// Token: 0x0400481E RID: 18462
		private string serviceResourceIdField;
	}
}
