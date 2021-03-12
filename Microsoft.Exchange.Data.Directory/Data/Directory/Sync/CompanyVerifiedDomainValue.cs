using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000821 RID: 2081
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class CompanyVerifiedDomainValue : CompanyDomainValue
	{
		// Token: 0x060066F2 RID: 26354 RVA: 0x0016BECA File Offset: 0x0016A0CA
		public override string ToString()
		{
			return string.Format("{0} defaultField={1} initialField={2}", base.ToString(), this.defaultField, this.initialField);
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x0016BEF2 File Offset: 0x0016A0F2
		public CompanyVerifiedDomainValue()
		{
			this.defaultField = false;
			this.initialField = false;
			this.verificationMethodField = 0;
			this.requiresDnsPublishingField = false;
		}

		// Token: 0x17002458 RID: 9304
		// (get) Token: 0x060066F4 RID: 26356 RVA: 0x0016BF16 File Offset: 0x0016A116
		// (set) Token: 0x060066F5 RID: 26357 RVA: 0x0016BF1E File Offset: 0x0016A11E
		[DefaultValue(false)]
		[XmlAttribute]
		public bool Default
		{
			get
			{
				return this.defaultField;
			}
			set
			{
				this.defaultField = value;
			}
		}

		// Token: 0x17002459 RID: 9305
		// (get) Token: 0x060066F6 RID: 26358 RVA: 0x0016BF27 File Offset: 0x0016A127
		// (set) Token: 0x060066F7 RID: 26359 RVA: 0x0016BF2F File Offset: 0x0016A12F
		[DefaultValue(false)]
		[XmlAttribute]
		public bool Initial
		{
			get
			{
				return this.initialField;
			}
			set
			{
				this.initialField = value;
			}
		}

		// Token: 0x1700245A RID: 9306
		// (get) Token: 0x060066F8 RID: 26360 RVA: 0x0016BF38 File Offset: 0x0016A138
		// (set) Token: 0x060066F9 RID: 26361 RVA: 0x0016BF40 File Offset: 0x0016A140
		[DefaultValue(0)]
		[XmlAttribute]
		public int VerificationMethod
		{
			get
			{
				return this.verificationMethodField;
			}
			set
			{
				this.verificationMethodField = value;
			}
		}

		// Token: 0x1700245B RID: 9307
		// (get) Token: 0x060066FA RID: 26362 RVA: 0x0016BF49 File Offset: 0x0016A149
		// (set) Token: 0x060066FB RID: 26363 RVA: 0x0016BF51 File Offset: 0x0016A151
		[XmlAttribute]
		[DefaultValue(false)]
		public bool RequiresDnsPublishing
		{
			get
			{
				return this.requiresDnsPublishingField;
			}
			set
			{
				this.requiresDnsPublishingField = value;
			}
		}

		// Token: 0x040043E5 RID: 17381
		private bool defaultField;

		// Token: 0x040043E6 RID: 17382
		private bool initialField;

		// Token: 0x040043E7 RID: 17383
		private int verificationMethodField;

		// Token: 0x040043E8 RID: 17384
		private bool requiresDnsPublishingField;
	}
}
