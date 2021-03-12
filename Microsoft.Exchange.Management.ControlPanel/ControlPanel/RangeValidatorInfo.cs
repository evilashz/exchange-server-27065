using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E4 RID: 1764
	[DataContract(Name = "RangeValidatorInfo{0}")]
	public abstract class RangeValidatorInfo<TClientValue> : ValidatorInfo
	{
		// Token: 0x06004A59 RID: 19033 RVA: 0x000E3726 File Offset: 0x000E1926
		protected RangeValidatorInfo(string validatorType, TClientValue minValue, TClientValue maxValue, bool acceptNull, bool acceptUnlimited) : base(validatorType)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.AcceptNull = acceptNull;
			this.AcceptUnlimted = acceptUnlimited;
		}

		// Token: 0x17002823 RID: 10275
		// (get) Token: 0x06004A5A RID: 19034 RVA: 0x000E374D File Offset: 0x000E194D
		// (set) Token: 0x06004A5B RID: 19035 RVA: 0x000E3755 File Offset: 0x000E1955
		[DataMember]
		public TClientValue MinValue { get; set; }

		// Token: 0x17002824 RID: 10276
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x000E375E File Offset: 0x000E195E
		// (set) Token: 0x06004A5D RID: 19037 RVA: 0x000E3766 File Offset: 0x000E1966
		[DataMember]
		public TClientValue MaxValue { get; set; }

		// Token: 0x17002825 RID: 10277
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x000E376F File Offset: 0x000E196F
		// (set) Token: 0x06004A5F RID: 19039 RVA: 0x000E3777 File Offset: 0x000E1977
		[DataMember]
		public bool AcceptNull { get; set; }

		// Token: 0x17002826 RID: 10278
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x000E3780 File Offset: 0x000E1980
		// (set) Token: 0x06004A61 RID: 19041 RVA: 0x000E3788 File Offset: 0x000E1988
		[DataMember]
		public bool AcceptUnlimted { get; set; }

		// Token: 0x06004A62 RID: 19042 RVA: 0x000E3791 File Offset: 0x000E1991
		internal static bool IsNullable<TServerValue>(RangedValueConstraint<TServerValue> constraint) where TServerValue : struct, IComparable
		{
			return constraint is RangedNullableValueConstraint<TServerValue> || constraint is RangedNullableUnlimitedConstraint<TServerValue>;
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x000E37A6 File Offset: 0x000E19A6
		internal static bool IsUnlimited<TServerValue>(RangedValueConstraint<TServerValue> constraint) where TServerValue : struct, IComparable
		{
			return constraint is RangedUnlimitedConstraint<TServerValue> || constraint is RangedNullableUnlimitedConstraint<TServerValue>;
		}
	}
}
