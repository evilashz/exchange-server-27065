using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008AC RID: 2220
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ExchangeFaultinStatus
	{
		// Token: 0x06006E25 RID: 28197 RVA: 0x00176102 File Offset: 0x00174302
		public ExchangeFaultinStatus()
		{
			this.exchangeFaultinStatusCodeField = 0;
		}

		// Token: 0x1700272B RID: 10027
		// (get) Token: 0x06006E26 RID: 28198 RVA: 0x00176111 File Offset: 0x00174311
		// (set) Token: 0x06006E27 RID: 28199 RVA: 0x00176119 File Offset: 0x00174319
		[XmlElement(IsNullable = true, Order = 0)]
		public string ErrorDescription
		{
			get
			{
				return this.errorDescriptionField;
			}
			set
			{
				this.errorDescriptionField = value;
			}
		}

		// Token: 0x1700272C RID: 10028
		// (get) Token: 0x06006E28 RID: 28200 RVA: 0x00176122 File Offset: 0x00174322
		// (set) Token: 0x06006E29 RID: 28201 RVA: 0x0017612A File Offset: 0x0017432A
		[XmlAttribute]
		public string ContextId
		{
			get
			{
				return this.contextIdField;
			}
			set
			{
				this.contextIdField = value;
			}
		}

		// Token: 0x1700272D RID: 10029
		// (get) Token: 0x06006E2A RID: 28202 RVA: 0x00176133 File Offset: 0x00174333
		// (set) Token: 0x06006E2B RID: 28203 RVA: 0x0017613B File Offset: 0x0017433B
		[DefaultValue(0)]
		[XmlAttribute]
		public int ExchangeFaultinStatusCode
		{
			get
			{
				return this.exchangeFaultinStatusCodeField;
			}
			set
			{
				this.exchangeFaultinStatusCodeField = value;
			}
		}

		// Token: 0x040047B4 RID: 18356
		private string errorDescriptionField;

		// Token: 0x040047B5 RID: 18357
		private string contextIdField;

		// Token: 0x040047B6 RID: 18358
		private int exchangeFaultinStatusCodeField;
	}
}
