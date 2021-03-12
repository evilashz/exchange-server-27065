using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000242 RID: 578
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PhoneCallInformationType
	{
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0002678C File Offset: 0x0002498C
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x00026794 File Offset: 0x00024994
		public PhoneCallStateType PhoneCallState
		{
			get
			{
				return this.phoneCallStateField;
			}
			set
			{
				this.phoneCallStateField = value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0002679D File Offset: 0x0002499D
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x000267A5 File Offset: 0x000249A5
		public ConnectionFailureCauseType ConnectionFailureCause
		{
			get
			{
				return this.connectionFailureCauseField;
			}
			set
			{
				this.connectionFailureCauseField = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x000267AE File Offset: 0x000249AE
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x000267B6 File Offset: 0x000249B6
		public string SIPResponseText
		{
			get
			{
				return this.sIPResponseTextField;
			}
			set
			{
				this.sIPResponseTextField = value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x000267BF File Offset: 0x000249BF
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x000267C7 File Offset: 0x000249C7
		public int SIPResponseCode
		{
			get
			{
				return this.sIPResponseCodeField;
			}
			set
			{
				this.sIPResponseCodeField = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x000267D0 File Offset: 0x000249D0
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x000267D8 File Offset: 0x000249D8
		[XmlIgnore]
		public bool SIPResponseCodeSpecified
		{
			get
			{
				return this.sIPResponseCodeFieldSpecified;
			}
			set
			{
				this.sIPResponseCodeFieldSpecified = value;
			}
		}

		// Token: 0x04000EED RID: 3821
		private PhoneCallStateType phoneCallStateField;

		// Token: 0x04000EEE RID: 3822
		private ConnectionFailureCauseType connectionFailureCauseField;

		// Token: 0x04000EEF RID: 3823
		private string sIPResponseTextField;

		// Token: 0x04000EF0 RID: 3824
		private int sIPResponseCodeField;

		// Token: 0x04000EF1 RID: 3825
		private bool sIPResponseCodeFieldSpecified;
	}
}
