using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000038 RID: 56
	internal static class ConditionComparer
	{
		// Token: 0x0600010D RID: 269 RVA: 0x000070A8 File Offset: 0x000052A8
		internal static bool Equals(Restriction a, Restriction b)
		{
			if (a == null || b == null)
			{
				return a == b;
			}
			if (a.GetType() != b.GetType())
			{
				return false;
			}
			if (a is Restriction.PropertyRestriction)
			{
				return ConditionComparer.Equals((Restriction.PropertyRestriction)a, (Restriction.PropertyRestriction)b);
			}
			if (a is Restriction.ContentRestriction)
			{
				return ConditionComparer.Equals((Restriction.ContentRestriction)a, (Restriction.ContentRestriction)b);
			}
			return a is Restriction.AndRestriction && ConditionComparer.Equals((Restriction.AndRestriction)a, (Restriction.AndRestriction)b);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007128 File Offset: 0x00005328
		private static bool Equals(Restriction.PropertyRestriction a, Restriction.PropertyRestriction b)
		{
			return a.Op == b.Op && a.PropTag == b.PropTag && a.PropValue.Equals(b.PropValue);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007167 File Offset: 0x00005367
		private static bool Equals(Restriction.AndRestriction a, Restriction.AndRestriction b)
		{
			return ConditionComparer.Equals(a.Restrictions, b.Restrictions);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000717A File Offset: 0x0000537A
		private static bool Equals(Restriction.OrRestriction a, Restriction.OrRestriction b)
		{
			return ConditionComparer.Equals(a.Restrictions, b.Restrictions);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007190 File Offset: 0x00005390
		private static bool Equals(Restriction[] aArray, Restriction[] bArray)
		{
			foreach (Restriction condition in aArray)
			{
				if (!ConditionComparer.Contains(condition, bArray))
				{
					return false;
				}
			}
			foreach (Restriction condition2 in bArray)
			{
				if (!ConditionComparer.Contains(condition2, aArray))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000071F0 File Offset: 0x000053F0
		private static bool Contains(Restriction condition, Restriction[] conditionArray)
		{
			foreach (Restriction b in conditionArray)
			{
				if (ConditionComparer.Equals(condition, b))
				{
					return true;
				}
			}
			return false;
		}
	}
}
