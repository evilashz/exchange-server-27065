using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000005 RID: 5
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[Serializable]
	public class Admin
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00006890 File Offset: 0x00004A90
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00006898 File Offset: 0x00004A98
		public string NetId
		{
			get
			{
				return this.netIdField;
			}
			set
			{
				this.netIdField = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000068A1 File Offset: 0x00004AA1
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000068A9 File Offset: 0x00004AA9
		public string ByodAuthToken
		{
			get
			{
				return this.byodAuthTokenField;
			}
			set
			{
				this.byodAuthTokenField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000068B2 File Offset: 0x00004AB2
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000068BA File Offset: 0x00004ABA
		public bool IsEnabled
		{
			get
			{
				return this.isEnabledField;
			}
			set
			{
				this.isEnabledField = value;
			}
		}

		// Token: 0x04000064 RID: 100
		private string netIdField;

		// Token: 0x04000065 RID: 101
		private string byodAuthTokenField;

		// Token: 0x04000066 RID: 102
		private bool isEnabledField;
	}
}
