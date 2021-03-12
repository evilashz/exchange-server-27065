using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200009B RID: 155
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class GetUserSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001F9A7 File Offset: 0x0001DBA7
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0001F9AF File Offset: 0x0001DBAF
		[XmlArray(IsNullable = true)]
		public User[] Users
		{
			get
			{
				return this.usersField;
			}
			set
			{
				this.usersField = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001F9B8 File Offset: 0x0001DBB8
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0001F9C0 File Offset: 0x0001DBC0
		[XmlArrayItem("Setting")]
		[XmlArray(IsNullable = true)]
		public string[] RequestedSettings
		{
			get
			{
				return this.requestedSettingsField;
			}
			set
			{
				this.requestedSettingsField = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001F9C9 File Offset: 0x0001DBC9
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0001F9D1 File Offset: 0x0001DBD1
		[XmlElement(IsNullable = true)]
		public ExchangeVersion? RequestedVersion
		{
			get
			{
				return this.requestedVersionField;
			}
			set
			{
				this.requestedVersionField = value;
			}
		}

		// Token: 0x04000353 RID: 851
		private User[] usersField;

		// Token: 0x04000354 RID: 852
		private string[] requestedSettingsField;

		// Token: 0x04000355 RID: 853
		private ExchangeVersion? requestedVersionField;
	}
}
