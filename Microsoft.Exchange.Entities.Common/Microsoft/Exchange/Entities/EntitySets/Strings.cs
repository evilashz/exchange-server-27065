using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000007 RID: 7
	internal static class Strings
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000024C4 File Offset: 0x000006C4
		static Strings()
		{
			Strings.stringIDs.Add(52452988U, "UnsupportedEstimatedTotalCount");
			Strings.stringIDs.Add(1964246015U, "StringCompareMustCompareToZero");
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002527 File Offset: 0x00000727
		public static LocalizedString UnsupportedEstimatedTotalCount
		{
			get
			{
				return new LocalizedString("UnsupportedEstimatedTotalCount", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000253E File Offset: 0x0000073E
		public static LocalizedString StringCompareMustCompareToZero
		{
			get
			{
				return new LocalizedString("StringCompareMustCompareToZero", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002558 File Offset: 0x00000758
		public static LocalizedString UnsupportedPropertyValue(Expression expression)
		{
			return new LocalizedString("UnsupportedPropertyValue", Strings.ResourceManager, new object[]
			{
				expression
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002580 File Offset: 0x00000780
		public static LocalizedString UnsupportedOperatorWithNull(ExpressionType operatorType)
		{
			return new LocalizedString("UnsupportedOperatorWithNull", Strings.ResourceManager, new object[]
			{
				operatorType
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025B0 File Offset: 0x000007B0
		public static LocalizedString UnsupportedOperator(ExpressionType operatorType)
		{
			return new LocalizedString("UnsupportedOperator", Strings.ResourceManager, new object[]
			{
				operatorType
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025E0 File Offset: 0x000007E0
		public static LocalizedString UnsupportedOperatorWithBlob(ExpressionType operatorType)
		{
			return new LocalizedString("UnsupportedOperatorWithBlob", Strings.ResourceManager, new object[]
			{
				operatorType
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002610 File Offset: 0x00000810
		public static LocalizedString UnsupportedMethodCall(MethodInfo method)
		{
			return new LocalizedString("UnsupportedMethodCall", Strings.ResourceManager, new object[]
			{
				method
			});
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002638 File Offset: 0x00000838
		public static LocalizedString UnsupportedFilterExpression(Expression expression)
		{
			return new LocalizedString("UnsupportedFilterExpression", Strings.ResourceManager, new object[]
			{
				expression
			});
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002660 File Offset: 0x00000860
		public static LocalizedString UnsupportedPropertyExpression(Expression expression)
		{
			return new LocalizedString("UnsupportedPropertyExpression", Strings.ResourceManager, new object[]
			{
				expression
			});
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002688 File Offset: 0x00000888
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000017 RID: 23
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x04000018 RID: 24
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Entities.EntitySets.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000008 RID: 8
		public enum IDs : uint
		{
			// Token: 0x0400001A RID: 26
			UnsupportedEstimatedTotalCount = 52452988U,
			// Token: 0x0400001B RID: 27
			StringCompareMustCompareToZero = 1964246015U
		}

		// Token: 0x02000009 RID: 9
		private enum ParamIDs
		{
			// Token: 0x0400001D RID: 29
			UnsupportedPropertyValue,
			// Token: 0x0400001E RID: 30
			UnsupportedOperatorWithNull,
			// Token: 0x0400001F RID: 31
			UnsupportedOperator,
			// Token: 0x04000020 RID: 32
			UnsupportedOperatorWithBlob,
			// Token: 0x04000021 RID: 33
			UnsupportedMethodCall,
			// Token: 0x04000022 RID: 34
			UnsupportedFilterExpression,
			// Token: 0x04000023 RID: 35
			UnsupportedPropertyExpression
		}
	}
}
