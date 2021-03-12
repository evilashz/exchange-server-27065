using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E6 RID: 1766
	[DataContract]
	public abstract class CompareValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A67 RID: 19047 RVA: 0x000E3813 File Offset: 0x000E1A13
		protected CompareValidatorInfo(string validatorType, string controlToCompare) : base(validatorType)
		{
			this.ControlToCompare = controlToCompare;
		}

		// Token: 0x17002827 RID: 10279
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x000E3823 File Offset: 0x000E1A23
		// (set) Token: 0x06004A69 RID: 19049 RVA: 0x000E382B File Offset: 0x000E1A2B
		[DataMember]
		public string ControlToCompare { get; set; }
	}
}
