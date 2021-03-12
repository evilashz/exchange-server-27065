using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000449 RID: 1097
	internal static class RoleFlagsFormat
	{
		// Token: 0x06003187 RID: 12679 RVA: 0x000C6F54 File Offset: 0x000C5154
		internal static object GetRoleState(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ExchangeRoleSchema.RoleFlags];
			int num2 = num & 1;
			return (RoleState)num2;
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000C6F7C File Offset: 0x000C517C
		internal static void SetRoleState(object value, IPropertyBag propertyBag)
		{
			RoleState roleState = (RoleState)value;
			int num = (int)propertyBag[ExchangeRoleSchema.RoleFlags];
			num |= 1;
			if (roleState == RoleState.Usable)
			{
				num &= -2;
			}
			propertyBag[ExchangeRoleSchema.RoleFlags] = num;
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000C6FC0 File Offset: 0x000C51C0
		internal static object GetIsEndUserRole(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ExchangeRoleSchema.RoleFlags];
			int num2 = num >> 31 & 1;
			return num2 != 0;
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000C6FF4 File Offset: 0x000C51F4
		internal static void SetIsEndUserRole(object value, IPropertyBag propertyBag)
		{
			uint num = ((bool)value) ? 1U : 0U;
			int num2 = (int)propertyBag[ExchangeRoleSchema.RoleFlags];
			propertyBag[ExchangeRoleSchema.RoleFlags] = ((num2 & int.MaxValue) | (int)((int)(num & 1U) << 31));
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000C703D File Offset: 0x000C523D
		internal static QueryFilter IsEndUserRoleFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ExchangeRoleSchema.RoleFlags, (ulong)int.MinValue));
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000C7058 File Offset: 0x000C5258
		private static uint GetScopeBits(IPropertyBag propertyBag, ScopeLocation scopeLocation)
		{
			int num = (int)propertyBag[ExchangeRoleSchema.RoleFlags];
			return (uint)num >> RoleFlagsFormat.ScopeShifts[(int)scopeLocation] & 31U;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000C7088 File Offset: 0x000C5288
		private static void SetScopeBits(uint valueToSet, IPropertyBag propertyBag, ScopeLocation scopeLocation)
		{
			int num = (int)propertyBag[ExchangeRoleSchema.RoleFlags];
			propertyBag[ExchangeRoleSchema.RoleFlags] = (int)(((ulong)num & (ulong)(~(31L << (RoleFlagsFormat.ScopeShifts[(int)scopeLocation] & 31)))) | (ulong)((ulong)(valueToSet & 31U) << RoleFlagsFormat.ScopeShifts[(int)scopeLocation]));
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000C70F8 File Offset: 0x000C52F8
		internal static GetterDelegate ScopeTypeGetterDelegate(ScopeLocation scopeLocation)
		{
			return (IPropertyBag propertyBag) => (ScopeType)RoleFlagsFormat.GetScopeBits(propertyBag, scopeLocation);
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000C713C File Offset: 0x000C533C
		internal static SetterDelegate ScopeTypeSetterDelegate(ScopeLocation scopeLocation)
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				RoleFlagsFormat.SetScopeBits((uint)((ScopeType)value), propertyBag, scopeLocation);
			};
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000C7162 File Offset: 0x000C5362
		internal static object GetIsDeprecated(IPropertyBag propertyBag)
		{
			return RoleState.Deprecated.Equals(RoleFlagsFormat.GetRoleState(propertyBag));
		}

		// Token: 0x04002156 RID: 8534
		internal const int IsEndUserRoleShift = 31;

		// Token: 0x04002157 RID: 8535
		internal const int ScopeMask = 31;

		// Token: 0x04002158 RID: 8536
		internal const int RoleStateMask = 1;

		// Token: 0x04002159 RID: 8537
		internal const int IsEndUserRoleMask = 1;

		// Token: 0x0400215A RID: 8538
		internal static readonly int[] ScopeShifts = new int[]
		{
			24,
			17,
			10,
			3
		};
	}
}
