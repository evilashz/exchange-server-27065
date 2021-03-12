using System;
using Microsoft.Exchange.Management.Deployment.Analysis;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000077 RID: 119
	internal sealed class RoleFeature : Feature
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x0002680B File Offset: 0x00024A0B
		public RoleFeature(SetupRole roles)
		{
			this.role = roles;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0002681A File Offset: 0x00024A1A
		public SetupRole Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00026822 File Offset: 0x00024A22
		public bool Contains(SetupRole role)
		{
			return (this.role & role) > SetupRole.None;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002682F File Offset: 0x00024A2F
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.role.ToString());
		}

		// Token: 0x040005C7 RID: 1479
		private readonly SetupRole role;
	}
}
