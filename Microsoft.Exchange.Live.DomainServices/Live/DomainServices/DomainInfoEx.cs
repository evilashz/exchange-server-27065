using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200000A RID: 10
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[DebuggerStepThrough]
	[Serializable]
	public class DomainInfoEx : DomainInfo
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000069AF File Offset: 0x00004BAF
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000069B7 File Offset: 0x00004BB7
		public NamespaceState NamespaceState
		{
			get
			{
				return this.namespaceStateField;
			}
			set
			{
				this.namespaceStateField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000069C0 File Offset: 0x00004BC0
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000069C8 File Offset: 0x00004BC8
		public OpenState OpenState
		{
			get
			{
				return this.openStateField;
			}
			set
			{
				this.openStateField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000069D1 File Offset: 0x00004BD1
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000069D9 File Offset: 0x00004BD9
		public EmailState EmailState
		{
			get
			{
				return this.emailStateField;
			}
			set
			{
				this.emailStateField = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000069E2 File Offset: 0x00004BE2
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000069EA File Offset: 0x00004BEA
		public bool IsEmailSuspended
		{
			get
			{
				return this.isEmailSuspendedField;
			}
			set
			{
				this.isEmailSuspendedField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000069F3 File Offset: 0x00004BF3
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000069FB File Offset: 0x00004BFB
		public bool IsMxValid
		{
			get
			{
				return this.isMxValidField;
			}
			set
			{
				this.isMxValidField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00006A04 File Offset: 0x00004C04
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00006A0C File Offset: 0x00004C0C
		public string[] MxRecords
		{
			get
			{
				return this.mxRecordsField;
			}
			set
			{
				this.mxRecordsField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006A15 File Offset: 0x00004C15
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00006A1D File Offset: 0x00004C1D
		public EmailType EmailType
		{
			get
			{
				return this.emailTypeField;
			}
			set
			{
				this.emailTypeField = value;
			}
		}

		// Token: 0x04000077 RID: 119
		private NamespaceState namespaceStateField;

		// Token: 0x04000078 RID: 120
		private OpenState openStateField;

		// Token: 0x04000079 RID: 121
		private EmailState emailStateField;

		// Token: 0x0400007A RID: 122
		private bool isEmailSuspendedField;

		// Token: 0x0400007B RID: 123
		private bool isMxValidField;

		// Token: 0x0400007C RID: 124
		private string[] mxRecordsField;

		// Token: 0x0400007D RID: 125
		private EmailType emailTypeField;
	}
}
