using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008BB RID: 2235
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
	[Serializable]
	public class CookieUpdateStatus
	{
		// Token: 0x17002754 RID: 10068
		// (get) Token: 0x06006E86 RID: 28294 RVA: 0x0017643A File Offset: 0x0017463A
		// (set) Token: 0x06006E87 RID: 28295 RVA: 0x00176442 File Offset: 0x00174642
		[XmlElement(IsNullable = true, Order = 0)]
		public string StatusMessage
		{
			get
			{
				return this.statusMessageField;
			}
			set
			{
				this.statusMessageField = value;
			}
		}

		// Token: 0x17002755 RID: 10069
		// (get) Token: 0x06006E88 RID: 28296 RVA: 0x0017644B File Offset: 0x0017464B
		// (set) Token: 0x06006E89 RID: 28297 RVA: 0x00176453 File Offset: 0x00174653
		[XmlAttribute]
		public int StatusCode
		{
			get
			{
				return this.statusCodeField;
			}
			set
			{
				this.statusCodeField = value;
			}
		}

		// Token: 0x040047DD RID: 18397
		private string statusMessageField;

		// Token: 0x040047DE RID: 18398
		private int statusCodeField;
	}
}
