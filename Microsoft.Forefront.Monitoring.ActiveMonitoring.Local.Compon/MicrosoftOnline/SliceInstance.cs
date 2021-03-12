using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000186 RID: 390
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class SliceInstance : DirectoryObject
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x000209D5 File Offset: 0x0001EBD5
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x000209DD File Offset: 0x0001EBDD
		public DirectoryPropertyStringSingleLength1To20 BuildNumber
		{
			get
			{
				return this.buildNumberField;
			}
			set
			{
				this.buildNumberField = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000209E6 File Offset: 0x0001EBE6
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x000209EE File Offset: 0x0001EBEE
		public DirectoryPropertyInt32Single SliceId
		{
			get
			{
				return this.sliceIdField;
			}
			set
			{
				this.sliceIdField = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x000209F7 File Offset: 0x0001EBF7
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x000209FF File Offset: 0x0001EBFF
		public DirectoryPropertyInt32SingleMin0 SliceStatus
		{
			get
			{
				return this.sliceStatusField;
			}
			set
			{
				this.sliceStatusField = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00020A08 File Offset: 0x0001EC08
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00020A10 File Offset: 0x0001EC10
		public DirectoryPropertyXmlServiceEndpoint ServiceEndpoints
		{
			get
			{
				return this.serviceEndpointsField;
			}
			set
			{
				this.serviceEndpointsField = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00020A19 File Offset: 0x0001EC19
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x00020A21 File Offset: 0x0001EC21
		public DirectoryPropertyBinarySingleLength1To256 TenantRange
		{
			get
			{
				return this.tenantRangeField;
			}
			set
			{
				this.tenantRangeField = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00020A2A File Offset: 0x0001EC2A
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x00020A32 File Offset: 0x0001EC32
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x0400047D RID: 1149
		private DirectoryPropertyStringSingleLength1To20 buildNumberField;

		// Token: 0x0400047E RID: 1150
		private DirectoryPropertyInt32Single sliceIdField;

		// Token: 0x0400047F RID: 1151
		private DirectoryPropertyInt32SingleMin0 sliceStatusField;

		// Token: 0x04000480 RID: 1152
		private DirectoryPropertyXmlServiceEndpoint serviceEndpointsField;

		// Token: 0x04000481 RID: 1153
		private DirectoryPropertyBinarySingleLength1To256 tenantRangeField;

		// Token: 0x04000482 RID: 1154
		private XmlAttribute[] anyAttrField;
	}
}
