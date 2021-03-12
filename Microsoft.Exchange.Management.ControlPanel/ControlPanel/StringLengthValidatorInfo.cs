using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006DF RID: 1759
	[DataContract]
	public class StringLengthValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A3C RID: 19004 RVA: 0x000E3548 File Offset: 0x000E1748
		public StringLengthValidatorInfo(int minLength, int maxLength) : this("StringLengthValidator", minLength, maxLength)
		{
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x000E3557 File Offset: 0x000E1757
		internal StringLengthValidatorInfo(StringLengthConstraint constraint) : this(constraint.MinLength, constraint.MaxLength)
		{
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x000E356B File Offset: 0x000E176B
		internal StringLengthValidatorInfo(UIImpactStringLengthConstraint constraint) : this(constraint.MinLength, constraint.MaxLength)
		{
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x000E357F File Offset: 0x000E177F
		internal StringLengthValidatorInfo(LocalLongFullPathLengthConstraint constraint) : this(0, 247)
		{
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x000E358D File Offset: 0x000E178D
		protected StringLengthValidatorInfo(string validatorType, int minLength, int maxLength) : base(validatorType)
		{
			this.MinLength = minLength;
			this.MaxLength = maxLength;
		}

		// Token: 0x1700281B RID: 10267
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x000E35A4 File Offset: 0x000E17A4
		// (set) Token: 0x06004A42 RID: 19010 RVA: 0x000E35AC File Offset: 0x000E17AC
		[DataMember]
		public int MaxLength { get; set; }

		// Token: 0x1700281C RID: 10268
		// (get) Token: 0x06004A43 RID: 19011 RVA: 0x000E35B5 File Offset: 0x000E17B5
		// (set) Token: 0x06004A44 RID: 19012 RVA: 0x000E35BD File Offset: 0x000E17BD
		[DataMember]
		public int MinLength { get; set; }
	}
}
