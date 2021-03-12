using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000007 RID: 7
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Member
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000068F5 File Offset: 0x00004AF5
		// (set) Token: 0x06000190 RID: 400 RVA: 0x000068FD File Offset: 0x00004AFD
		public string MemberName
		{
			get
			{
				return this.memberNameField;
			}
			set
			{
				this.memberNameField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006906 File Offset: 0x00004B06
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000690E File Offset: 0x00004B0E
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

		// Token: 0x04000069 RID: 105
		private string memberNameField;

		// Token: 0x0400006A RID: 106
		private string netIdField;
	}
}
