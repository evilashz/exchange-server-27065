using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000EC RID: 236
	internal abstract class UpdateSendAsPermission : WebServiceParameters
	{
		// Token: 0x06001E7E RID: 7806 RVA: 0x0005C026 File Offset: 0x0005A226
		public UpdateSendAsPermission()
		{
			base["ExtendedRights"] = "send as";
		}

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0005C03E File Offset: 0x0005A23E
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0005C045 File Offset: 0x0005A245
		// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0005C057 File Offset: 0x0005A257
		public string User
		{
			get
			{
				return (string)base["User"];
			}
			set
			{
				base["User"] = value;
			}
		}

		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0005C065 File Offset: 0x0005A265
		// (set) Token: 0x06001E83 RID: 7811 RVA: 0x0005C077 File Offset: 0x0005A277
		public string Identity
		{
			get
			{
				return (string)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}
	}
}
