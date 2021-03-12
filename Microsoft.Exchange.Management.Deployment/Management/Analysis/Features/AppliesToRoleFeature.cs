using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x0200005F RID: 95
	internal class AppliesToRoleFeature : Feature
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000834B File Offset: 0x0000654B
		public AppliesToRoleFeature(SetupRole roles) : base(true, true)
		{
			this.Role = roles;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000835C File Offset: 0x0000655C
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00008364 File Offset: 0x00006564
		public SetupRole Role { get; private set; }

		// Token: 0x06000245 RID: 581 RVA: 0x0000836D File Offset: 0x0000656D
		public bool Contains(SetupRole role)
		{
			return (this.Role & role) > SetupRole.None;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000837A File Offset: 0x0000657A
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.Role.ToString());
		}
	}
}
