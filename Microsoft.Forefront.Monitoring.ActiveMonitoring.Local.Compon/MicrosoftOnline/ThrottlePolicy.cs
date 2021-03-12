using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200018B RID: 395
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class ThrottlePolicy : DirectoryObject
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000216AC File Offset: 0x0001F8AC
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x000216B4 File Offset: 0x0001F8B4
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000216BD File Offset: 0x0001F8BD
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x000216C5 File Offset: 0x0001F8C5
		public DirectoryPropertyXmlThrottleLimit ThrottleLimits
		{
			get
			{
				return this.throttleLimitsField;
			}
			set
			{
				this.throttleLimitsField = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x000216CE File Offset: 0x0001F8CE
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x000216D6 File Offset: 0x0001F8D6
		public DirectoryPropertyGuidSingle ThrottlePolicyId
		{
			get
			{
				return this.throttlePolicyIdField;
			}
			set
			{
				this.throttlePolicyIdField = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000216DF File Offset: 0x0001F8DF
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x000216E7 File Offset: 0x0001F8E7
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

		// Token: 0x0400053C RID: 1340
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x0400053D RID: 1341
		private DirectoryPropertyXmlThrottleLimit throttleLimitsField;

		// Token: 0x0400053E RID: 1342
		private DirectoryPropertyGuidSingle throttlePolicyIdField;

		// Token: 0x0400053F RID: 1343
		private XmlAttribute[] anyAttrField;
	}
}
