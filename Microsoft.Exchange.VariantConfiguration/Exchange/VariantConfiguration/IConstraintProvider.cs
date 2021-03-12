using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000010 RID: 16
	public interface IConstraintProvider
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86
		ConstraintCollection Constraints { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000057 RID: 87
		string RampId { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000058 RID: 88
		string RotationId { get; }
	}
}
