using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000193 RID: 403
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Service : DirectoryObject
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00021B0A File Offset: 0x0001FD0A
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00021B12 File Offset: 0x0001FD12
		public DirectoryPropertyInt32SingleMin0 ContextPriorityQuotaLimit
		{
			get
			{
				return this.contextPriorityQuotaLimitField;
			}
			set
			{
				this.contextPriorityQuotaLimitField = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00021B1B File Offset: 0x0001FD1B
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00021B23 File Offset: 0x0001FD23
		public DirectoryPropertyInt32SingleMin0 DeferredSyncCompanyQuotaLimit
		{
			get
			{
				return this.deferredSyncCompanyQuotaLimitField;
			}
			set
			{
				this.deferredSyncCompanyQuotaLimitField = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00021B2C File Offset: 0x0001FD2C
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00021B34 File Offset: 0x0001FD34
		public DirectoryPropertyGuidSingle PartnerSkuId
		{
			get
			{
				return this.partnerSkuIdField;
			}
			set
			{
				this.partnerSkuIdField = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00021B3D File Offset: 0x0001FD3D
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00021B45 File Offset: 0x0001FD45
		public DirectoryPropertyStringLength1To64 AuthorizedRestrictedSyncAttributeSet
		{
			get
			{
				return this.authorizedRestrictedSyncAttributeSetField;
			}
			set
			{
				this.authorizedRestrictedSyncAttributeSetField = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00021B4E File Offset: 0x0001FD4E
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00021B56 File Offset: 0x0001FD56
		public DirectoryPropertyStringSingleLength1To256 ServiceType
		{
			get
			{
				return this.serviceTypeField;
			}
			set
			{
				this.serviceTypeField = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00021B5F File Offset: 0x0001FD5F
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00021B67 File Offset: 0x0001FD67
		public DirectoryPropertyXmlServiceInstanceMap ServiceInstanceMap
		{
			get
			{
				return this.serviceInstanceMapField;
			}
			set
			{
				this.serviceInstanceMapField = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00021B70 File Offset: 0x0001FD70
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x00021B78 File Offset: 0x0001FD78
		public DirectoryPropertyInt32SingleMin0 ServiceOptions
		{
			get
			{
				return this.serviceOptionsField;
			}
			set
			{
				this.serviceOptionsField = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00021B81 File Offset: 0x0001FD81
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x00021B89 File Offset: 0x0001FD89
		public DirectoryPropertyXmlAnySingle ServicePrincipalTemplate
		{
			get
			{
				return this.servicePrincipalTemplateField;
			}
			set
			{
				this.servicePrincipalTemplateField = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00021B92 File Offset: 0x0001FD92
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x00021B9A File Offset: 0x0001FD9A
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

		// Token: 0x0400057A RID: 1402
		private DirectoryPropertyInt32SingleMin0 contextPriorityQuotaLimitField;

		// Token: 0x0400057B RID: 1403
		private DirectoryPropertyInt32SingleMin0 deferredSyncCompanyQuotaLimitField;

		// Token: 0x0400057C RID: 1404
		private DirectoryPropertyGuidSingle partnerSkuIdField;

		// Token: 0x0400057D RID: 1405
		private DirectoryPropertyStringLength1To64 authorizedRestrictedSyncAttributeSetField;

		// Token: 0x0400057E RID: 1406
		private DirectoryPropertyStringSingleLength1To256 serviceTypeField;

		// Token: 0x0400057F RID: 1407
		private DirectoryPropertyXmlServiceInstanceMap serviceInstanceMapField;

		// Token: 0x04000580 RID: 1408
		private DirectoryPropertyInt32SingleMin0 serviceOptionsField;

		// Token: 0x04000581 RID: 1409
		private DirectoryPropertyXmlAnySingle servicePrincipalTemplateField;

		// Token: 0x04000582 RID: 1410
		private XmlAttribute[] anyAttrField;
	}
}
