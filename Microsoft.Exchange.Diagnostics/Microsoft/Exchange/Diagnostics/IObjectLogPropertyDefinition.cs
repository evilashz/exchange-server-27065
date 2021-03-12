using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001BA RID: 442
	internal interface IObjectLogPropertyDefinition<T>
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000C30 RID: 3120
		string FieldName { get; }

		// Token: 0x06000C31 RID: 3121
		object GetValue(T objectToLog);
	}
}
