using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C9D RID: 3229
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PermissionSchema : Schema
	{
		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x060070B7 RID: 28855 RVA: 0x001F323F File Offset: 0x001F143F
		public new static PermissionSchema Instance
		{
			get
			{
				if (PermissionSchema.instance == null)
				{
					PermissionSchema.instance = new PermissionSchema();
				}
				return PermissionSchema.instance;
			}
		}

		// Token: 0x04004DEE RID: 19950
		public static readonly StorePropertyDefinition MemberId = InternalSchema.MemberId;

		// Token: 0x04004DEF RID: 19951
		public static readonly StorePropertyDefinition MemberEntryId = InternalSchema.MemberEntryId;

		// Token: 0x04004DF0 RID: 19952
		public static readonly StorePropertyDefinition MemberName = InternalSchema.MemberName;

		// Token: 0x04004DF1 RID: 19953
		public static readonly StorePropertyDefinition MemberRights = InternalSchema.MemberRights;

		// Token: 0x04004DF2 RID: 19954
		public static readonly StorePropertyDefinition MemberSecurityIdentifier = InternalSchema.MemberSecurityIdentifier;

		// Token: 0x04004DF3 RID: 19955
		public static readonly StorePropertyDefinition MemberIsGroup = InternalSchema.MemberIsGroup;

		// Token: 0x04004DF4 RID: 19956
		private static PermissionSchema instance = null;
	}
}
