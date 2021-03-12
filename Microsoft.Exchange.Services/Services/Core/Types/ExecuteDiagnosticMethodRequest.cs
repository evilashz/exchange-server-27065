using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200041A RID: 1050
	[XmlType("ExecuteDiagnosticMethodRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ExecuteDiagnosticMethodRequest : BaseRequest
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x0009FC88 File Offset: 0x0009DE88
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x0009FC90 File Offset: 0x0009DE90
		[XmlElement("Verb")]
		public string Verb { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x0009FC99 File Offset: 0x0009DE99
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x0009FCA1 File Offset: 0x0009DEA1
		[XmlElement]
		public XmlNode Parameter { get; set; }

		// Token: 0x06001E2F RID: 7727 RVA: 0x0009FCB2 File Offset: 0x0009DEB2
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new ExecuteDiagnosticMethod(callContext, this);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0009FCBB File Offset: 0x0009DEBB
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0009FCBE File Offset: 0x0009DEBE
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
