using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.Versioning
{
	// Token: 0x020006FA RID: 1786
	public static class CompatibilitySwitch
	{
		// Token: 0x0600504A RID: 20554 RVA: 0x0011A696 File Offset: 0x00118896
		[SecurityCritical]
		public static bool IsEnabled(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.IsEnabledInternalCall(compatibilitySwitchName, true);
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x0011A69F File Offset: 0x0011889F
		[SecurityCritical]
		public static string GetValue(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.GetValueInternalCall(compatibilitySwitchName, true);
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0011A6A8 File Offset: 0x001188A8
		[SecurityCritical]
		internal static bool IsEnabledInternal(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.IsEnabledInternalCall(compatibilitySwitchName, false);
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0011A6B1 File Offset: 0x001188B1
		[SecurityCritical]
		internal static string GetValueInternal(string compatibilitySwitchName)
		{
			return CompatibilitySwitch.GetValueInternalCall(compatibilitySwitchName, false);
		}

		// Token: 0x0600504E RID: 20558
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetAppContextOverridesInternalCall();

		// Token: 0x0600504F RID: 20559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEnabledInternalCall(string compatibilitySwitchName, bool onlyDB);

		// Token: 0x06005050 RID: 20560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetValueInternalCall(string compatibilitySwitchName, bool onlyDB);
	}
}
