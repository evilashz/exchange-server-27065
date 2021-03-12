using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000107 RID: 263
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class PartnershipValue
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00020029 File Offset: 0x0001E229
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00020031 File Offset: 0x0001E231
		[XmlAttribute]
		public string PartnerContextId
		{
			get
			{
				return this.partnerContextIdField;
			}
			set
			{
				this.partnerContextIdField = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0002003A File Offset: 0x0001E23A
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x00020042 File Offset: 0x0001E242
		[XmlAttribute]
		public int PartnerType
		{
			get
			{
				return this.partnerTypeField;
			}
			set
			{
				this.partnerTypeField = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0002004B File Offset: 0x0001E24B
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00020053 File Offset: 0x0001E253
		[XmlAttribute]
		public bool LoggingEnabled
		{
			get
			{
				return this.loggingEnabledField;
			}
			set
			{
				this.loggingEnabledField = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0002005C File Offset: 0x0001E25C
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00020064 File Offset: 0x0001E264
		[XmlAttribute]
		public bool SupportPartner
		{
			get
			{
				return this.supportPartnerField;
			}
			set
			{
				this.supportPartnerField = value;
			}
		}

		// Token: 0x0400040C RID: 1036
		private string partnerContextIdField;

		// Token: 0x0400040D RID: 1037
		private int partnerTypeField;

		// Token: 0x0400040E RID: 1038
		private bool loggingEnabledField;

		// Token: 0x0400040F RID: 1039
		private bool supportPartnerField;
	}
}
