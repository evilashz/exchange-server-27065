using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E0 RID: 1760
	[DataContract]
	public class StringLengthValidatorExInfo : ValidatorInfo
	{
		// Token: 0x06004A45 RID: 19013 RVA: 0x000E35C6 File Offset: 0x000E17C6
		public StringLengthValidatorExInfo(string binderControlForMinLength, string binderControlForMaxLength) : this("StringLengthValidatorEx", binderControlForMinLength, binderControlForMaxLength)
		{
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x000E35D5 File Offset: 0x000E17D5
		protected StringLengthValidatorExInfo(string validatorType, string binderControlForMinLength, string binderControlForMaxLength) : base(validatorType)
		{
			this.BinderControlForMinLength = binderControlForMinLength;
			this.BinderControlForMaxLength = binderControlForMaxLength;
		}

		// Token: 0x1700281D RID: 10269
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x000E35EC File Offset: 0x000E17EC
		// (set) Token: 0x06004A48 RID: 19016 RVA: 0x000E35F4 File Offset: 0x000E17F4
		public string BinderControlForMinLength { get; set; }

		// Token: 0x1700281E RID: 10270
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x000E35FD File Offset: 0x000E17FD
		// (set) Token: 0x06004A4A RID: 19018 RVA: 0x000E3605 File Offset: 0x000E1805
		public string BinderControlForMaxLength { get; set; }
	}
}
