using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation
{
	// Token: 0x020001FA RID: 506
	internal sealed class DebugContext
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0003CFBB File Offset: 0x0003B1BB
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x0003CFC2 File Offset: 0x0003B1C2
		internal static bool TestOnlyDebugInfoOff
		{
			get
			{
				return DebugContext.testOnlyDebugInfoOff;
			}
			set
			{
				DebugContext.testOnlyDebugInfoOff = value;
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003CFCA File Offset: 0x0003B1CA
		internal static void SetActivityId(Guid value)
		{
			DebugContext.SetProperty(DebugProperties.ActivityId, value);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003CFD8 File Offset: 0x0003B1D8
		[Conditional("DEBUG")]
		internal static void SetComponent(string value)
		{
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003CFDA File Offset: 0x0003B1DA
		[Conditional("DEBUG")]
		internal static void SetComponentInstance(string value)
		{
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003CFDC File Offset: 0x0003B1DC
		[Conditional("DEBUG")]
		internal static void SetAction(string value)
		{
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003CFDE File Offset: 0x0003B1DE
		internal static void UpdateFrom(IActivityScope activityScope)
		{
			DebugContext.SetActivityId(activityScope.ActivityId);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003CFEB File Offset: 0x0003B1EB
		internal static object GetDebugProperty(DebugProperties debugProperty)
		{
			return CallContext.LogicalGetData(DebugContext.propertyNames[(int)debugProperty]);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003CFF9 File Offset: 0x0003B1F9
		internal static string GetDebugInfo()
		{
			return string.Empty;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003D000 File Offset: 0x0003B200
		[Conditional("DEBUG")]
		internal static void Refresh()
		{
			int? num = new int?(Environment.CurrentManagedThreadId);
			int? num2 = (int?)DebugContext.GetDebugProperty(DebugProperties.ThreadId);
			if (num2 != null)
			{
				int? num3 = num;
				int? num4 = num2;
				if (num3.GetValueOrDefault() == num4.GetValueOrDefault())
				{
					bool flag = num3 != null != (num4 != null);
				}
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003D059 File Offset: 0x0003B259
		internal static void Clear()
		{
			CallContext.FreeNamedDataSlot(DebugContext.propertyNames[1]);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003D067 File Offset: 0x0003B267
		[Conditional("DEBUG")]
		private static void SetDebugProperty(DebugProperties debugProperty, object value)
		{
			DebugContext.SetProperty(debugProperty, value);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003D070 File Offset: 0x0003B270
		private static void SetProperty(DebugProperties debugProperty, object value)
		{
			if (value != null)
			{
				value.GetType();
				CallContext.LogicalSetData(DebugContext.propertyNames[(int)debugProperty], value);
				return;
			}
			CallContext.FreeNamedDataSlot(DebugContext.propertyNames[(int)debugProperty]);
		}

		// Token: 0x04000AA6 RID: 2726
		internal static readonly bool TestOnlyIsDebugBuild = false;

		// Token: 0x04000AA7 RID: 2727
		private static readonly string[] propertyNames = Enum.GetNames(typeof(DebugProperties));

		// Token: 0x04000AA8 RID: 2728
		private static bool testOnlyDebugInfoOff = false;
	}
}
