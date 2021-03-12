using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000333 RID: 819
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetEncryptionConfigurationType : BaseRequestType
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x00028E7B File Offset: 0x0002707B
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x00028E83 File Offset: 0x00027083
		public string ImageBase64
		{
			get
			{
				return this.imageBase64Field;
			}
			set
			{
				this.imageBase64Field = value;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00028E8C File Offset: 0x0002708C
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x00028E94 File Offset: 0x00027094
		public string EmailText
		{
			get
			{
				return this.emailTextField;
			}
			set
			{
				this.emailTextField = value;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00028E9D File Offset: 0x0002709D
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x00028EA5 File Offset: 0x000270A5
		public string PortalText
		{
			get
			{
				return this.portalTextField;
			}
			set
			{
				this.portalTextField = value;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00028EAE File Offset: 0x000270AE
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x00028EB6 File Offset: 0x000270B6
		public string DisclaimerText
		{
			get
			{
				return this.disclaimerTextField;
			}
			set
			{
				this.disclaimerTextField = value;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00028EBF File Offset: 0x000270BF
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x00028EC7 File Offset: 0x000270C7
		public bool OTPEnabled
		{
			get
			{
				return this.oTPEnabledField;
			}
			set
			{
				this.oTPEnabledField = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00028ED0 File Offset: 0x000270D0
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x00028ED8 File Offset: 0x000270D8
		[XmlIgnore]
		public bool OTPEnabledSpecified
		{
			get
			{
				return this.oTPEnabledFieldSpecified;
			}
			set
			{
				this.oTPEnabledFieldSpecified = value;
			}
		}

		// Token: 0x040011AF RID: 4527
		private string imageBase64Field;

		// Token: 0x040011B0 RID: 4528
		private string emailTextField;

		// Token: 0x040011B1 RID: 4529
		private string portalTextField;

		// Token: 0x040011B2 RID: 4530
		private string disclaimerTextField;

		// Token: 0x040011B3 RID: 4531
		private bool oTPEnabledField;

		// Token: 0x040011B4 RID: 4532
		private bool oTPEnabledFieldSpecified;
	}
}
