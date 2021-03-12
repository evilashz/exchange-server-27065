using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003C5 RID: 965
	public sealed class MovePolicyContextMenu : PolicyContextMenuBase
	{
		// Token: 0x060023FD RID: 9213 RVA: 0x000CFD2D File Offset: 0x000CDF2D
		private MovePolicyContextMenu(UserContext userContext) : base("divMvPtgM", userContext, "MovePolicyContextMenu")
		{
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000CFD40 File Offset: 0x000CDF40
		internal static MovePolicyContextMenu Create(UserContext userContext)
		{
			return new MovePolicyContextMenu(userContext);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000CFD48 File Offset: 0x000CDF48
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
			if (PolicyProvider.MovePolicyProvider.IsPolicyEnabled(userContext.MailboxSession) && Utilities.HasArchive(userContext))
			{
				renderMenuItem(output, 1060619217, ThemeFileId.None, "divMvPtg", null, false, null, null, MovePolicyContextMenu.Create(userContext));
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000CFDA8 File Offset: 0x000CDFA8
		protected override void RenderMenuItems(TextWriter output)
		{
			if (PolicyProvider.MovePolicyProvider.IsPolicyEnabled(this.userContext.MailboxSession))
			{
				PolicyTagList allPolicies = PolicyProvider.MovePolicyProvider.GetAllPolicies(this.userContext.MailboxSession);
				List<PolicyTag> list = new List<PolicyTag>(allPolicies.Values.Count);
				foreach (PolicyTag policyTag in allPolicies.Values)
				{
					if (policyTag.IsVisible || object.Equals(policyTag.PolicyGuid, base.PolicyChecked))
					{
						list.Add(policyTag);
					}
				}
				list.Sort(new Comparison<PolicyTag>(MovePolicyContextMenu.CompareMovePolicyValues));
				foreach (PolicyTag policyTag2 in list)
				{
					base.RenderPolicyTagMenuItem(output, policyTag2.PolicyGuid, PolicyContextMenuBase.GetDefaultDisplayName(policyTag2), false);
				}
				base.RenderInheritPolicyMenuItem(output, true, false);
			}
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000CFEC8 File Offset: 0x000CE0C8
		private static int CompareMovePolicyValues(PolicyTag policyTag1, PolicyTag policyTag2)
		{
			return policyTag1.TimeSpanForRetention.CompareTo(policyTag2.TimeSpanForRetention);
		}
	}
}
