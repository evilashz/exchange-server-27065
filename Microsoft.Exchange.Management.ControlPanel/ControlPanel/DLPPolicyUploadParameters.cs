using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001BC RID: 444
	[DataContract]
	public class DLPPolicyUploadParameters : SetObjectProperties
	{
		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x0006E5A1 File Offset: 0x0006C7A1
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-DLPPolicy";
			}
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x0006E5A8 File Offset: 0x0006C7A8
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x0006E5AF File Offset: 0x0006C7AF
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x0006E5C1 File Offset: 0x0006C7C1
		[DataMember]
		public Identity Identity
		{
			get
			{
				return (Identity)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x0006E5CF File Offset: 0x0006C7CF
		// (set) Token: 0x060023FE RID: 9214 RVA: 0x0006E5E1 File Offset: 0x0006C7E1
		public byte[] TemplateData
		{
			get
			{
				return (byte[])base["TemplateData"];
			}
			set
			{
				base["TemplateData"] = value;
			}
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x0006E5EF File Offset: 0x0006C7EF
		// (set) Token: 0x06002400 RID: 9216 RVA: 0x0006E601 File Offset: 0x0006C801
		[DataMember]
		public string State
		{
			get
			{
				return (string)base["State"];
			}
			set
			{
				base["State"] = value;
			}
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x0006E60F File Offset: 0x0006C80F
		// (set) Token: 0x06002402 RID: 9218 RVA: 0x0006E621 File Offset: 0x0006C821
		[DataMember]
		public string Mode
		{
			get
			{
				return (string)base["Mode"];
			}
			set
			{
				base["Mode"] = value;
			}
		}
	}
}
