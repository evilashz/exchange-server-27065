using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionRequest
{
	// Token: 0x02000897 RID: 2199
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "DeltaSyncV2:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AccountType
	{
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x00069B38 File Offset: 0x00067D38
		// (set) Token: 0x06002F15 RID: 12053 RVA: 0x00069B40 File Offset: 0x00067D40
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x00069B49 File Offset: 0x00067D49
		// (set) Token: 0x06002F17 RID: 12055 RVA: 0x00069B51 File Offset: 0x00067D51
		public AccountTypeType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x00069B5A File Offset: 0x00067D5A
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x00069B62 File Offset: 0x00067D62
		public string PartnerID
		{
			get
			{
				return this.partnerIDField;
			}
			set
			{
				this.partnerIDField = value;
			}
		}

		// Token: 0x040028EE RID: 10478
		private string nameField;

		// Token: 0x040028EF RID: 10479
		private AccountTypeType typeField;

		// Token: 0x040028F0 RID: 10480
		private string partnerIDField;
	}
}
