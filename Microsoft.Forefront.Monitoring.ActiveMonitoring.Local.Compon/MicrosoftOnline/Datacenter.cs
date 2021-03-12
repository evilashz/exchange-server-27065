using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000188 RID: 392
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class Datacenter : DirectoryObject
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00020B28 File Offset: 0x0001ED28
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00020B30 File Offset: 0x0001ED30
		public DirectoryPropertyXmlDatacenterRedirection DatacenterRedirections
		{
			get
			{
				return this.datacenterRedirectionsField;
			}
			set
			{
				this.datacenterRedirectionsField = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00020B39 File Offset: 0x0001ED39
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00020B41 File Offset: 0x0001ED41
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

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00020B4A File Offset: 0x0001ED4A
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00020B52 File Offset: 0x0001ED52
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00020B5B File Offset: 0x0001ED5B
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00020B63 File Offset: 0x0001ED63
		public DirectoryPropertyStringSingleLength1To64 SiteName
		{
			get
			{
				return this.siteNameField;
			}
			set
			{
				this.siteNameField = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00020B6C File Offset: 0x0001ED6C
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x00020B74 File Offset: 0x0001ED74
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

		// Token: 0x04000490 RID: 1168
		private DirectoryPropertyXmlDatacenterRedirection datacenterRedirectionsField;

		// Token: 0x04000491 RID: 1169
		private DirectoryPropertyXmlGeographicLocation geographicLocationField;

		// Token: 0x04000492 RID: 1170
		private DirectoryPropertyXmlServiceEndpoint serviceEndpointsField;

		// Token: 0x04000493 RID: 1171
		private DirectoryPropertyStringSingleLength1To64 siteNameField;

		// Token: 0x04000494 RID: 1172
		private XmlAttribute[] anyAttrField;
	}
}
