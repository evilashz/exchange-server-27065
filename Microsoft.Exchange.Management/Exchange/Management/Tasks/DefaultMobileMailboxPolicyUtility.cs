using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200043A RID: 1082
	internal class DefaultMobileMailboxPolicyUtility<T> : DefaultMailboxPolicyUtility<T> where T : MobileMailboxPolicy, new()
	{
		// Token: 0x06002617 RID: 9751 RVA: 0x00098382 File Offset: 0x00096582
		public static IList<T> GetDefaultPolicies(IConfigurationSession session)
		{
			return DefaultMailboxPolicyUtility<T>.GetDefaultPolicies(session, DefaultMobileMailboxPolicyUtility<T>.filter, null);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x00098390 File Offset: 0x00096590
		public static IList<T> GetDefaultPolicies(IConfigurationSession session, QueryFilter extraFilter)
		{
			return DefaultMailboxPolicyUtility<T>.GetDefaultPolicies(session, DefaultMobileMailboxPolicyUtility<T>.filter, extraFilter);
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000983A0 File Offset: 0x000965A0
		internal static bool ValidateLength(IEnumerable collection, int maxListLength, int maxMemberLength)
		{
			if (collection != null)
			{
				foreach (object obj in collection)
				{
					int length = obj.ToString().Length;
					if (maxMemberLength < length)
					{
						return false;
					}
					maxListLength -= length;
					if (maxListLength < 0)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x04001D7D RID: 7549
		private static readonly QueryFilter filter = new BitMaskAndFilter(MobileMailboxPolicySchema.MobileFlags, 4096UL);
	}
}
