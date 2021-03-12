using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E5 RID: 1765
	[DataContract]
	public class RangeNumberValidatorInfo : RangeValidatorInfo<double>
	{
		// Token: 0x06004A64 RID: 19044 RVA: 0x000E37BB File Offset: 0x000E19BB
		public RangeNumberValidatorInfo(double minValue, double maxValue, bool acceptNull, bool acceptUnlimited) : base("RangeNumberValidator", minValue, maxValue, acceptNull, acceptUnlimited)
		{
		}

		// Token: 0x06004A65 RID: 19045 RVA: 0x000E37CD File Offset: 0x000E19CD
		internal RangeNumberValidatorInfo(RangedValueConstraint<int> constraint) : this((double)constraint.MinimumValue, (double)constraint.MaximumValue, RangeValidatorInfo<double>.IsNullable<int>(constraint), RangeValidatorInfo<double>.IsUnlimited<int>(constraint))
		{
		}

		// Token: 0x06004A66 RID: 19046 RVA: 0x000E37EF File Offset: 0x000E19EF
		internal RangeNumberValidatorInfo(RangedValueConstraint<uint> constraint) : this(constraint.MinimumValue, constraint.MaximumValue, RangeValidatorInfo<double>.IsNullable<uint>(constraint), RangeValidatorInfo<double>.IsUnlimited<uint>(constraint))
		{
		}
	}
}
