using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x020008A0 RID: 2208
	[XmlRoot(Namespace = "DeltaSyncV2:", IsNullable = false)]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Provision
	{
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x0006A90D File Offset: 0x00068B0D
		// (set) Token: 0x06002F48 RID: 12104 RVA: 0x0006A915 File Offset: 0x00068B15
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x0006A91E File Offset: 0x00068B1E
		// (set) Token: 0x06002F4A RID: 12106 RVA: 0x0006A926 File Offset: 0x00068B26
		public Fault Fault
		{
			get
			{
				return this.faultField;
			}
			set
			{
				this.faultField = value;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x0006A92F File Offset: 0x00068B2F
		// (set) Token: 0x06002F4C RID: 12108 RVA: 0x0006A937 File Offset: 0x00068B37
		public ProvisionResponses Responses
		{
			get
			{
				return this.responsesField;
			}
			set
			{
				this.responsesField = value;
			}
		}

		// Token: 0x04002908 RID: 10504
		private int statusField;

		// Token: 0x04002909 RID: 10505
		private Fault faultField;

		// Token: 0x0400290A RID: 10506
		private ProvisionResponses responsesField;
	}
}
