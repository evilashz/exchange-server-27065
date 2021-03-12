using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000352 RID: 850
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SetTeamMailboxRequestType : BaseRequestType
	{
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00029777 File Offset: 0x00027977
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x0002977F File Offset: 0x0002797F
		public EmailAddressType EmailAddress
		{
			get
			{
				return this.emailAddressField;
			}
			set
			{
				this.emailAddressField = value;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00029788 File Offset: 0x00027988
		// (set) Token: 0x06001B7A RID: 7034 RVA: 0x00029790 File Offset: 0x00027990
		public string SharePointSiteUrl
		{
			get
			{
				return this.sharePointSiteUrlField;
			}
			set
			{
				this.sharePointSiteUrlField = value;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x00029799 File Offset: 0x00027999
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x000297A1 File Offset: 0x000279A1
		public TeamMailboxLifecycleStateType State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x04001250 RID: 4688
		private EmailAddressType emailAddressField;

		// Token: 0x04001251 RID: 4689
		private string sharePointSiteUrlField;

		// Token: 0x04001252 RID: 4690
		private TeamMailboxLifecycleStateType stateField;
	}
}
