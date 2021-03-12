using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000105 RID: 261
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CompanyUnverifiedDomainValue : CompanyDomainValue
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x0001FFDF File Offset: 0x0001E1DF
		public CompanyUnverifiedDomainValue()
		{
			this.pendingDeletionField = false;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001FFEE File Offset: 0x0001E1EE
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001FFF6 File Offset: 0x0001E1F6
		[XmlAttribute]
		[DefaultValue(false)]
		public bool PendingDeletion
		{
			get
			{
				return this.pendingDeletionField;
			}
			set
			{
				this.pendingDeletionField = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001FFFF File Offset: 0x0001E1FF
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00020007 File Offset: 0x0001E207
		[XmlAttribute]
		public string VerificationCode
		{
			get
			{
				return this.verificationCodeField;
			}
			set
			{
				this.verificationCodeField = value;
			}
		}

		// Token: 0x04000409 RID: 1033
		private bool pendingDeletionField;

		// Token: 0x0400040A RID: 1034
		private string verificationCodeField;
	}
}
