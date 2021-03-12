using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000326 RID: 806
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class AddDistributionGroupToImListType : BaseRequestType
	{
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00028CB6 File Offset: 0x00026EB6
		// (set) Token: 0x06001A32 RID: 6706 RVA: 0x00028CBE File Offset: 0x00026EBE
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddressField;
			}
			set
			{
				this.smtpAddressField = value;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00028CC7 File Offset: 0x00026EC7
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x00028CCF File Offset: 0x00026ECF
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

		// Token: 0x04001195 RID: 4501
		private string smtpAddressField;

		// Token: 0x04001196 RID: 4502
		private string displayNameField;
	}
}
