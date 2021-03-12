using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200034E RID: 846
	public sealed class DeletePolicyContextMenu : PolicyContextMenuBase
	{
		// Token: 0x06001FDE RID: 8158 RVA: 0x000B8651 File Offset: 0x000B6851
		private DeletePolicyContextMenu(UserContext userContext) : base("divDelPtgM", userContext, "DeletePolicyContextMenu")
		{
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000B8664 File Offset: 0x000B6864
		internal static DeletePolicyContextMenu Create(UserContext userContext)
		{
			return new DeletePolicyContextMenu(userContext);
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x000B866C File Offset: 0x000B686C
		// (set) Token: 0x06001FE1 RID: 8161 RVA: 0x000B8674 File Offset: 0x000B6874
		internal bool RenderCheckedOnly { get; set; }

		// Token: 0x06001FE2 RID: 8162 RVA: 0x000B8680 File Offset: 0x000B6880
		internal static void RenderAsSubmenu(TextWriter output, UserContext userContext, RenderMenuItemDelegate renderMenuItem)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (renderMenuItem == null)
			{
				throw new ArgumentNullException("renderMenuItem");
			}
			if (PolicyProvider.DeletePolicyProvider.IsPolicyEnabled(userContext.MailboxSession))
			{
				renderMenuItem(output, 1463778657, ThemeFileId.None, "divDelPtg", null, false, null, null, DeletePolicyContextMenu.Create(userContext));
			}
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x000B86D8 File Offset: 0x000B68D8
		protected override void RenderMenuItems(TextWriter output)
		{
			if (PolicyProvider.DeletePolicyProvider.IsPolicyEnabled(this.userContext.MailboxSession))
			{
				PolicyTagList allPolicies = PolicyProvider.DeletePolicyProvider.GetAllPolicies(this.userContext.MailboxSession);
				List<PolicyTag> list = new List<PolicyTag>(allPolicies.Values.Count);
				foreach (PolicyTag policyTag in allPolicies.Values)
				{
					if ((!this.RenderCheckedOnly && policyTag.IsVisible) || object.Equals(policyTag.PolicyGuid, base.PolicyChecked))
					{
						list.Add(policyTag);
					}
				}
				list.Sort(new Comparison<PolicyTag>(DeletePolicyContextMenu.CompareDeletePolicyValues));
				foreach (PolicyTag policyTag2 in list)
				{
					base.RenderPolicyTagMenuItem(output, policyTag2.PolicyGuid, PolicyContextMenuBase.GetDefaultDisplayName(policyTag2), this.RenderCheckedOnly);
				}
				if (!this.RenderCheckedOnly || base.InheritChecked)
				{
					base.RenderInheritPolicyMenuItem(output, !this.RenderCheckedOnly, this.RenderCheckedOnly);
				}
			}
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000B8824 File Offset: 0x000B6A24
		private static int CompareDeletePolicyValues(PolicyTag policyTag1, PolicyTag policyTag2)
		{
			return string.Compare(policyTag1.Name, policyTag2.Name, StringComparison.CurrentCultureIgnoreCase);
		}
	}
}
