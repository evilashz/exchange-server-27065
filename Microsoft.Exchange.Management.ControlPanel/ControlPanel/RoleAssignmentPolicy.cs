using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000533 RID: 1331
	[DataContract]
	[KnownType(typeof(RoleAssignmentPolicy))]
	public class RoleAssignmentPolicy : RoleAssignmentPolicyRow
	{
		// Token: 0x170024A2 RID: 9378
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x000BE109 File Offset: 0x000BC309
		// (set) Token: 0x06003F1A RID: 16154 RVA: 0x000BE111 File Offset: 0x000BC311
		[DataMember]
		public string CaptionText { get; private set; }

		// Token: 0x170024A3 RID: 9379
		// (get) Token: 0x06003F1B RID: 16155 RVA: 0x000BE11A File Offset: 0x000BC31A
		// (set) Token: 0x06003F1C RID: 16156 RVA: 0x000BE122 File Offset: 0x000BC322
		[DataMember]
		public string Description { get; private set; }

		// Token: 0x170024A4 RID: 9380
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x000BE168 File Offset: 0x000BC368
		[DataMember]
		public IEnumerable<Identity> AssignedEndUserRoles
		{
			get
			{
				if (this.assignedEndUserRoles == null)
				{
					IEnumerable<ExchangeRoleObject> source = ExchangeRoleObjectResolver.Instance.ResolveObjects(this.assignedRoles);
					IEnumerable<ExchangeRoleObject> enumerable = from role in source
					where role.IsEndUserRole
					select role;
					if (Util.IsDataCenter)
					{
						bool flag = false;
						foreach (ExchangeRoleObject exchangeRoleObject in enumerable)
						{
							if (!string.IsNullOrEmpty(exchangeRoleObject.MailboxPlanIndex))
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							string mailboxplan = this.MailboxPlanIndex;
							enumerable = from role in enumerable
							where string.IsNullOrEmpty(role.MailboxPlanIndex) || role.MailboxPlanIndex == mailboxplan
							select role;
						}
					}
					this.assignedEndUserRoles = from role in enumerable
					select role.Identity;
				}
				return this.assignedEndUserRoles;
			}
		}

		// Token: 0x170024A5 RID: 9381
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x000BE288 File Offset: 0x000BC488
		internal string MailboxPlanIndex
		{
			get
			{
				if (Util.IsDataCenter)
				{
					if (string.IsNullOrEmpty(this.mailboxPlanIndex))
					{
						PowerShellResults<MailboxPlan> list = new MailboxPlans().GetList(null, null);
						if (!list.Succeeded)
						{
							ErrorHandlingUtil.TransferToErrorPage("notsupportrap");
						}
						MailboxPlan[] array = Array.FindAll<MailboxPlan>(list.Output, (MailboxPlan x) => x.RoleAssignmentPolicy.ToIdentity().RawIdentity == base.Identity.RawIdentity);
						if (array == null || array.Length != 1)
						{
							ErrorHandlingUtil.TransferToErrorPage("notsupportrap");
						}
						else
						{
							this.mailboxPlanIndex = array[0].MailboxPlanIndex;
						}
					}
					return this.mailboxPlanIndex;
				}
				throw new NotSupportedException("MailboxPlanIndex for RoleAssignmentPolicy is not supported in non-Datacenter environment");
			}
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x000BE31D File Offset: 0x000BC51D
		public RoleAssignmentPolicy(RoleAssignmentPolicy policy) : base(policy)
		{
			this.CaptionText = Strings.EditRoleAssignmentPolicyCaption(policy.Name);
			this.Description = policy.Description;
			this.assignedRoles = policy.AssignedRoles;
		}

		// Token: 0x040028D6 RID: 10454
		private IEnumerable<Identity> assignedEndUserRoles;

		// Token: 0x040028D7 RID: 10455
		private IEnumerable<ADObjectId> assignedRoles;

		// Token: 0x040028D8 RID: 10456
		private string mailboxPlanIndex;
	}
}
