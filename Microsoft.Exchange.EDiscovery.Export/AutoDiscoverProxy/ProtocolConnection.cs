using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000086 RID: 134
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class ProtocolConnection
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001F6D6 File Offset: 0x0001D8D6
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0001F6DE File Offset: 0x0001D8DE
		[XmlElement(IsNullable = true)]
		public string Hostname
		{
			get
			{
				return this.hostnameField;
			}
			set
			{
				this.hostnameField = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001F6E7 File Offset: 0x0001D8E7
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0001F6EF File Offset: 0x0001D8EF
		public int Port
		{
			get
			{
				return this.portField;
			}
			set
			{
				this.portField = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0001F700 File Offset: 0x0001D900
		[XmlElement(IsNullable = true)]
		public string EncryptionMethod
		{
			get
			{
				return this.encryptionMethodField;
			}
			set
			{
				this.encryptionMethodField = value;
			}
		}

		// Token: 0x0400032C RID: 812
		private string hostnameField;

		// Token: 0x0400032D RID: 813
		private int portField;

		// Token: 0x0400032E RID: 814
		private string encryptionMethodField;
	}
}
