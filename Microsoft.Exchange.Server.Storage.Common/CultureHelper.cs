using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000022 RID: 34
	public static class CultureHelper
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000791A File Offset: 0x00005B1A
		public static CultureInfo GetCultureFromLcid(int lcid)
		{
			return CultureInfo.GetCultureInfo(lcid);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007922 File Offset: 0x00005B22
		public static int GetLcidFromCulture(CultureInfo culture)
		{
			return culture.LCID;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000792C File Offset: 0x00005B2C
		public static bool IsValidCodePage(IExecutionContext context, int codePage)
		{
			bool result;
			try
			{
				CodePageMap.GetEncoding(codePage);
				result = true;
			}
			catch (ArgumentException exception)
			{
				context.Diagnostics.OnExceptionCatch(exception);
				result = false;
			}
			catch (NotSupportedException exception2)
			{
				context.Diagnostics.OnExceptionCatch(exception2);
				result = false;
			}
			return result;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007984 File Offset: 0x00005B84
		public static bool IsValidLcid(IExecutionContext context, int lcid)
		{
			if (lcid == CultureHelper.Invariant || lcid == 0 || lcid == 2048 || lcid == 1024 || lcid == 4096)
			{
				return true;
			}
			if (CultureHelper.frequentlyUsedCultures.Contains(lcid))
			{
				return true;
			}
			bool result;
			try
			{
				CultureHelper.GetCultureFromLcid(lcid);
				result = true;
			}
			catch (ArgumentException exception)
			{
				context.Diagnostics.OnExceptionCatch(exception);
				result = false;
			}
			return result;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000079F4 File Offset: 0x00005BF4
		public static CultureInfo TranslateLcid(int lcid)
		{
			if (CultureHelper.Invariant == lcid)
			{
				return CultureHelper.DefaultCultureInfo;
			}
			if (lcid == 0)
			{
				return CultureHelper.DefaultCultureInfo;
			}
			if (2048 == lcid)
			{
				return CultureHelper.DefaultCultureInfo;
			}
			if (1024 == lcid)
			{
				return CultureHelper.DefaultCultureInfo;
			}
			if (4096 == lcid)
			{
				return CultureHelper.DefaultCultureInfo;
			}
			CultureInfo cultureInfo = null;
			if (CultureHelper.frequentlyUsedCultures.TryGetValue(lcid, out cultureInfo))
			{
				return cultureInfo;
			}
			try
			{
				cultureInfo = CultureHelper.GetCultureFromLcid(lcid);
			}
			catch (ArgumentException ex)
			{
				Globals.AssertRetail(false, string.Concat(new object[]
				{
					"Unexpected culture in TranslateLCID. Lcid = [",
					lcid,
					"] exception = [",
					ex.ToString(),
					"]"
				}));
				return CultureHelper.DefaultCultureInfo;
			}
			PersistentAvlTree<int, CultureInfo> persistentAvlTree;
			PersistentAvlTree<int, CultureInfo> value;
			do
			{
				persistentAvlTree = CultureHelper.frequentlyUsedCultures;
				if (persistentAvlTree.Contains(lcid))
				{
					break;
				}
				value = persistentAvlTree.Add(lcid, cultureInfo);
			}
			while (!object.ReferenceEquals(Interlocked.CompareExchange<PersistentAvlTree<int, CultureInfo>>(ref CultureHelper.frequentlyUsedCultures, value, persistentAvlTree), persistentAvlTree));
			return cultureInfo;
		}

		// Token: 0x040003EB RID: 1003
		public const int LocaleNeutral = 0;

		// Token: 0x040003EC RID: 1004
		public const int SystemDefault = 2048;

		// Token: 0x040003ED RID: 1005
		public const int UserDefault = 1024;

		// Token: 0x040003EE RID: 1006
		public const int CustomCulture = 4096;

		// Token: 0x040003EF RID: 1007
		public static readonly int Invariant = CultureInfo.InvariantCulture.LCID;

		// Token: 0x040003F0 RID: 1008
		public static readonly CultureInfo DefaultCultureInfo = new CultureInfo("en-US");

		// Token: 0x040003F1 RID: 1009
		private static PersistentAvlTree<int, CultureInfo> frequentlyUsedCultures = PersistentAvlTree<int, CultureInfo>.Empty;
	}
}
