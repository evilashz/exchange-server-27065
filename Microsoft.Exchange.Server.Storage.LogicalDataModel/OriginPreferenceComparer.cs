using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000A3 RID: 163
	internal sealed class OriginPreferenceComparer
	{
		// Token: 0x06000954 RID: 2388 RVA: 0x0004DDE6 File Offset: 0x0004BFE6
		private OriginPreferenceComparer()
		{
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0004DDF0 File Offset: 0x0004BFF0
		internal int Compare(Context context, Mailbox mailbox, StorePropTag proptag, object value1, string origin1, DateTime creationTime1, object value2, string origin2, DateTime creationTime2)
		{
			if (object.ReferenceEquals(value1, value2))
			{
				return 0;
			}
			int num = this.CompareStringValue(context, mailbox, value1, origin1, value2, origin2);
			if (num == 0)
			{
				return Comparer<DateTime>.Default.Compare(creationTime1, creationTime2);
			}
			return num;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0004DE30 File Offset: 0x0004C030
		private int CompareStringValue(Context context, Mailbox mailbox, object value1, string origin1, object value2, string origin2)
		{
			int y = this.MapValueAndOriginToPriority(new Func<Context, Mailbox, object, bool>(this.NotBlank), context, mailbox, value1, origin1);
			int x = this.MapValueAndOriginToPriority(new Func<Context, Mailbox, object, bool>(this.NotBlank), context, mailbox, value2, origin2);
			return Comparer<int>.Default.Compare(x, y);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0004DE7A File Offset: 0x0004C07A
		private int MapValueAndOriginToPriority(Func<Context, Mailbox, object, bool> valuePreconditionEvaluator, Context context, Mailbox mailbox, object value, string origin)
		{
			if (!valuePreconditionEvaluator(context, mailbox, value))
			{
				return int.MaxValue;
			}
			return this.MapOriginToPriority(origin);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0004DE96 File Offset: 0x0004C096
		private int MapOriginToPriority(string origin)
		{
			if (string.IsNullOrWhiteSpace(origin))
			{
				return 0;
			}
			if (string.Equals(origin, OriginPreferenceComparer.WellKnownNetworkNames.GAL))
			{
				return 1;
			}
			if (string.Equals(origin, OriginPreferenceComparer.WellKnownNetworkNames.LinkedIn))
			{
				return 4;
			}
			if (string.Equals(origin, OriginPreferenceComparer.WellKnownNetworkNames.Facebook))
			{
				return 5;
			}
			return 9;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0004DED1 File Offset: 0x0004C0D1
		private bool NotBlank(Context context, Mailbox mailbox, object value)
		{
			return value != null && !string.IsNullOrWhiteSpace(value.ToString());
		}

		// Token: 0x0400048B RID: 1163
		internal static readonly OriginPreferenceComparer Instance = new OriginPreferenceComparer();

		// Token: 0x020000A4 RID: 164
		private static class WellKnownNetworkNames
		{
			// Token: 0x0400048C RID: 1164
			public static readonly string Facebook = "Facebook";

			// Token: 0x0400048D RID: 1165
			public static readonly string LinkedIn = "LinkedIn";

			// Token: 0x0400048E RID: 1166
			public static readonly string Sharepoint = "Sharepoint";

			// Token: 0x0400048F RID: 1167
			public static readonly string GAL = "GAL";

			// Token: 0x04000490 RID: 1168
			public static readonly string QuickContacts = "QuickContacts";

			// Token: 0x04000491 RID: 1169
			public static readonly string LyncContacts = "LyncContacts";

			// Token: 0x04000492 RID: 1170
			public static readonly string RecipientCache = "RecipientCache";
		}
	}
}
