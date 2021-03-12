using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000192 RID: 402
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class ServiceInstance : DirectoryObject
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00021A7A File Offset: 0x0001FC7A
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x00021A82 File Offset: 0x0001FC82
		public DirectoryPropertyStringLength1To1024 AuthorizedIdentity
		{
			get
			{
				return this.authorizedIdentityField;
			}
			set
			{
				this.authorizedIdentityField = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00021A8B File Offset: 0x0001FC8B
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00021A93 File Offset: 0x0001FC93
		public DirectoryPropertyXmlGeographicLocation GeographicLocation
		{
			get
			{
				return this.geographicLocationField;
			}
			set
			{
				this.geographicLocationField = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00021A9C File Offset: 0x0001FC9C
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00021AA4 File Offset: 0x0001FCA4
		public DirectoryPropertyBinarySingleLength1To4000 SecretEncryptionCertificate
		{
			get
			{
				return this.secretEncryptionCertificateField;
			}
			set
			{
				this.secretEncryptionCertificateField = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00021AAD File Offset: 0x0001FCAD
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x00021AB5 File Offset: 0x0001FCB5
		public DirectoryPropertyXmlServiceInstanceInfo ServiceInstanceConfig
		{
			get
			{
				return this.serviceInstanceConfigField;
			}
			set
			{
				this.serviceInstanceConfigField = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00021ABE File Offset: 0x0001FCBE
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x00021AC6 File Offset: 0x0001FCC6
		public DirectoryPropertyXmlServiceInstanceInfo ServiceInstanceInfo
		{
			get
			{
				return this.serviceInstanceInfoField;
			}
			set
			{
				this.serviceInstanceInfoField = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00021ACF File Offset: 0x0001FCCF
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x00021AD7 File Offset: 0x0001FCD7
		public DirectoryPropertyStringSingleLength1To256 ServiceInstanceName
		{
			get
			{
				return this.serviceInstanceNameField;
			}
			set
			{
				this.serviceInstanceNameField = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00021AE0 File Offset: 0x0001FCE0
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		public DirectoryPropertyInt32SingleMin0 ServiceInstanceOptions
		{
			get
			{
				return this.serviceInstanceOptionsField;
			}
			set
			{
				this.serviceInstanceOptionsField = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00021AF1 File Offset: 0x0001FCF1
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00021AF9 File Offset: 0x0001FCF9
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

		// Token: 0x04000572 RID: 1394
		private DirectoryPropertyStringLength1To1024 authorizedIdentityField;

		// Token: 0x04000573 RID: 1395
		private DirectoryPropertyXmlGeographicLocation geographicLocationField;

		// Token: 0x04000574 RID: 1396
		private DirectoryPropertyBinarySingleLength1To4000 secretEncryptionCertificateField;

		// Token: 0x04000575 RID: 1397
		private DirectoryPropertyXmlServiceInstanceInfo serviceInstanceConfigField;

		// Token: 0x04000576 RID: 1398
		private DirectoryPropertyXmlServiceInstanceInfo serviceInstanceInfoField;

		// Token: 0x04000577 RID: 1399
		private DirectoryPropertyStringSingleLength1To256 serviceInstanceNameField;

		// Token: 0x04000578 RID: 1400
		private DirectoryPropertyInt32SingleMin0 serviceInstanceOptionsField;

		// Token: 0x04000579 RID: 1401
		private XmlAttribute[] anyAttrField;
	}
}
