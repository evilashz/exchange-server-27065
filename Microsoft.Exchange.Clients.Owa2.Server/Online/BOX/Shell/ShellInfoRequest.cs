using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x0200006C RID: 108
	[DataContract(Name = "ShellInfoRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ShellInfoRequest : NavBarInfoRequest
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000DD66 File Offset: 0x0000BF66
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000DD6E File Offset: 0x0000BF6E
		[DataMember]
		public bool ExcludeMSAjax
		{
			get
			{
				return this.ExcludeMSAjaxField;
			}
			set
			{
				this.ExcludeMSAjaxField = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000DD77 File Offset: 0x0000BF77
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000DD7F File Offset: 0x0000BF7F
		[DataMember]
		public ShellBaseFlight? ShellBaseFlight
		{
			get
			{
				return this.ShellBaseFlightField;
			}
			set
			{
				this.ShellBaseFlightField = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000DD88 File Offset: 0x0000BF88
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000DD90 File Offset: 0x0000BF90
		[DataMember]
		public string TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000DD99 File Offset: 0x0000BF99
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000DDA1 File Offset: 0x0000BFA1
		[DataMember]
		public string UserThemeId
		{
			get
			{
				return this.UserThemeIdField;
			}
			set
			{
				this.UserThemeIdField = value;
			}
		}

		// Token: 0x040001C5 RID: 453
		private bool ExcludeMSAjaxField;

		// Token: 0x040001C6 RID: 454
		private ShellBaseFlight? ShellBaseFlightField;

		// Token: 0x040001C7 RID: 455
		private string TenantIdField;

		// Token: 0x040001C8 RID: 456
		private string UserThemeIdField;
	}
}
