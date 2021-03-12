using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200026C RID: 620
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUserOofSettingsResponse
	{
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x000273E4 File Offset: 0x000255E4
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x000273EC File Offset: 0x000255EC
		public ResponseMessageType ResponseMessage
		{
			get
			{
				return this.responseMessageField;
			}
			set
			{
				this.responseMessageField = value;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x000273F5 File Offset: 0x000255F5
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x000273FD File Offset: 0x000255FD
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public UserOofSettings OofSettings
		{
			get
			{
				return this.oofSettingsField;
			}
			set
			{
				this.oofSettingsField = value;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00027406 File Offset: 0x00025606
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x0002740E File Offset: 0x0002560E
		public ExternalAudience AllowExternalOof
		{
			get
			{
				return this.allowExternalOofField;
			}
			set
			{
				this.allowExternalOofField = value;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00027417 File Offset: 0x00025617
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x0002741F File Offset: 0x0002561F
		[XmlIgnore]
		public bool AllowExternalOofSpecified
		{
			get
			{
				return this.allowExternalOofFieldSpecified;
			}
			set
			{
				this.allowExternalOofFieldSpecified = value;
			}
		}

		// Token: 0x04000FB4 RID: 4020
		private ResponseMessageType responseMessageField;

		// Token: 0x04000FB5 RID: 4021
		private UserOofSettings oofSettingsField;

		// Token: 0x04000FB6 RID: 4022
		private ExternalAudience allowExternalOofField;

		// Token: 0x04000FB7 RID: 4023
		private bool allowExternalOofFieldSpecified;
	}
}
