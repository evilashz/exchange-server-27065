using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000423 RID: 1059
	internal sealed class PropertyAnalysis
	{
		// Token: 0x0600353D RID: 13629 RVA: 0x000CE7D9 File Offset: 0x000CC9D9
		public PropertyAnalysis(string name, MethodInfo getterInfo, TraceLoggingTypeInfo typeInfo, EventFieldAttribute fieldAttribute)
		{
			this.name = name;
			this.getterInfo = getterInfo;
			this.typeInfo = typeInfo;
			this.fieldAttribute = fieldAttribute;
		}

		// Token: 0x0400179B RID: 6043
		internal readonly string name;

		// Token: 0x0400179C RID: 6044
		internal readonly MethodInfo getterInfo;

		// Token: 0x0400179D RID: 6045
		internal readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x0400179E RID: 6046
		internal readonly EventFieldAttribute fieldAttribute;
	}
}
