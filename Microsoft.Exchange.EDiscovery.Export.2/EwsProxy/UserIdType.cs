using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001EB RID: 491
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UserIdType
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0002592D File Offset: 0x00023B2D
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00025935 File Offset: 0x00023B35
		public string SID
		{
			get
			{
				return this.sIDField;
			}
			set
			{
				this.sIDField = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0002593E File Offset: 0x00023B3E
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x00025946 File Offset: 0x00023B46
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddressField;
			}
			set
			{
				this.primarySmtpAddressField = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0002594F File Offset: 0x00023B4F
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x00025957 File Offset: 0x00023B57
		public string DisplayName
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

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00025960 File Offset: 0x00023B60
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x00025968 File Offset: 0x00023B68
		public DistinguishedUserType DistinguishedUser
		{
			get
			{
				return this.distinguishedUserField;
			}
			set
			{
				this.distinguishedUserField = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00025971 File Offset: 0x00023B71
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x00025979 File Offset: 0x00023B79
		[XmlIgnore]
		public bool DistinguishedUserSpecified
		{
			get
			{
				return this.distinguishedUserFieldSpecified;
			}
			set
			{
				this.distinguishedUserFieldSpecified = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00025982 File Offset: 0x00023B82
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x0002598A File Offset: 0x00023B8A
		public string ExternalUserIdentity
		{
			get
			{
				return this.externalUserIdentityField;
			}
			set
			{
				this.externalUserIdentityField = value;
			}
		}

		// Token: 0x04000DD6 RID: 3542
		private string sIDField;

		// Token: 0x04000DD7 RID: 3543
		private string primarySmtpAddressField;

		// Token: 0x04000DD8 RID: 3544
		private string displayNameField;

		// Token: 0x04000DD9 RID: 3545
		private DistinguishedUserType distinguishedUserField;

		// Token: 0x04000DDA RID: 3546
		private bool distinguishedUserFieldSpecified;

		// Token: 0x04000DDB RID: 3547
		private string externalUserIdentityField;
	}
}
