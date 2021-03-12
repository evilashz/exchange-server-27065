using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000F5 RID: 245
	internal class PartnerRole : Role
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0001DDDF File Offset: 0x0001BFDF
		public PartnerRole()
		{
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001DDE7 File Offset: 0x0001BFE7
		public PartnerRole(string callerId, string roleName)
		{
			if (string.IsNullOrEmpty(callerId))
			{
				throw new ArgumentNullException("callerId");
			}
			if (string.IsNullOrEmpty(roleName))
			{
				throw new ArgumentNullException("roleName");
			}
			this.PartnerCallerId = callerId;
			base.RoleName = roleName;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0001DE23 File Offset: 0x0001C023
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0001DE35 File Offset: 0x0001C035
		public string PartnerCallerId
		{
			get
			{
				return this[PartnerRole.PartnerCallerIdDef] as string;
			}
			internal set
			{
				this[PartnerRole.PartnerCallerIdDef] = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001DE44 File Offset: 0x0001C044
		public override ObjectId Identity
		{
			get
			{
				if (string.IsNullOrWhiteSpace(base.RoleGroupName))
				{
					return new ConfigObjectId(string.Format("{0}_{1}_{2}", this.PartnerCallerId, base.RoleName, base.RoleEntryName));
				}
				return new ConfigObjectId(string.Format("{0}_{1}_{2}_{3}", new object[]
				{
					this.PartnerCallerId,
					base.RoleGroupName,
					base.RoleName,
					base.RoleEntryName
				}));
			}
		}

		// Token: 0x04000515 RID: 1301
		internal static readonly HygienePropertyDefinition PartnerCallerIdDef = new HygienePropertyDefinition("callerId", typeof(string));
	}
}
