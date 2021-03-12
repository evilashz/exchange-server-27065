using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200094A RID: 2378
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class PartnershipValue
	{
		// Token: 0x170027DF RID: 10207
		// (get) Token: 0x0600701B RID: 28699 RVA: 0x00177189 File Offset: 0x00175389
		// (set) Token: 0x0600701C RID: 28700 RVA: 0x00177191 File Offset: 0x00175391
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

		// Token: 0x170027E0 RID: 10208
		// (get) Token: 0x0600701D RID: 28701 RVA: 0x0017719A File Offset: 0x0017539A
		// (set) Token: 0x0600701E RID: 28702 RVA: 0x001771A2 File Offset: 0x001753A2
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

		// Token: 0x170027E1 RID: 10209
		// (get) Token: 0x0600701F RID: 28703 RVA: 0x001771AB File Offset: 0x001753AB
		// (set) Token: 0x06007020 RID: 28704 RVA: 0x001771B3 File Offset: 0x001753B3
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

		// Token: 0x170027E2 RID: 10210
		// (get) Token: 0x06007021 RID: 28705 RVA: 0x001771BC File Offset: 0x001753BC
		// (set) Token: 0x06007022 RID: 28706 RVA: 0x001771C4 File Offset: 0x001753C4
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

		// Token: 0x040048C2 RID: 18626
		private string partnerContextIdField;

		// Token: 0x040048C3 RID: 18627
		private int partnerTypeField;

		// Token: 0x040048C4 RID: 18628
		private bool loggingEnabledField;

		// Token: 0x040048C5 RID: 18629
		private bool supportPartnerField;
	}
}
