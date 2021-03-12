using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200010C RID: 268
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class EncryptionConfigurationResponseType : ResponseMessageType
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00021323 File Offset: 0x0001F523
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x0002132B File Offset: 0x0001F52B
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

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00021334 File Offset: 0x0001F534
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x0002133C File Offset: 0x0001F53C
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

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00021345 File Offset: 0x0001F545
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0002134D File Offset: 0x0001F54D
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

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00021356 File Offset: 0x0001F556
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0002135E File Offset: 0x0001F55E
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

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00021367 File Offset: 0x0001F567
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x0002136F File Offset: 0x0001F56F
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

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00021378 File Offset: 0x0001F578
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x00021380 File Offset: 0x0001F580
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

		// Token: 0x0400086A RID: 2154
		private string imageBase64Field;

		// Token: 0x0400086B RID: 2155
		private string emailTextField;

		// Token: 0x0400086C RID: 2156
		private string portalTextField;

		// Token: 0x0400086D RID: 2157
		private string disclaimerTextField;

		// Token: 0x0400086E RID: 2158
		private bool oTPEnabledField;

		// Token: 0x0400086F RID: 2159
		private bool oTPEnabledFieldSpecified;
	}
}
