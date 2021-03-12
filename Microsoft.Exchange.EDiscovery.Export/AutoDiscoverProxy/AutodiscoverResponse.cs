using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200008E RID: 142
	[XmlInclude(typeof(DomainResponse))]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[XmlInclude(typeof(GetDomainSettingsResponse))]
	[XmlInclude(typeof(UserResponse))]
	[XmlInclude(typeof(GetUserSettingsResponse))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(GetOrganizationRelationshipSettingsResponse))]
	[XmlInclude(typeof(GetFederationInformationResponse))]
	[DebuggerStepThrough]
	[Serializable]
	public class AutodiscoverResponse
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001F7E2 File Offset: 0x0001D9E2
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0001F7EA File Offset: 0x0001D9EA
		public ErrorCode ErrorCode
		{
			get
			{
				return this.errorCodeField;
			}
			set
			{
				this.errorCodeField = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001F7F3 File Offset: 0x0001D9F3
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0001F7FB File Offset: 0x0001D9FB
		[XmlIgnore]
		public bool ErrorCodeSpecified
		{
			get
			{
				return this.errorCodeFieldSpecified;
			}
			set
			{
				this.errorCodeFieldSpecified = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001F804 File Offset: 0x0001DA04
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0001F80C File Offset: 0x0001DA0C
		[XmlElement(IsNullable = true)]
		public string ErrorMessage
		{
			get
			{
				return this.errorMessageField;
			}
			set
			{
				this.errorMessageField = value;
			}
		}

		// Token: 0x04000338 RID: 824
		private ErrorCode errorCodeField;

		// Token: 0x04000339 RID: 825
		private bool errorCodeFieldSpecified;

		// Token: 0x0400033A RID: 826
		private string errorMessageField;
	}
}
