using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E1 RID: 1761
	[DataContract]
	public class ADObjectNameStringLengthValidatorInfo : StringLengthValidatorInfo
	{
		// Token: 0x06004A4B RID: 19019 RVA: 0x000E360E File Offset: 0x000E180E
		internal ADObjectNameStringLengthValidatorInfo(ADObjectNameStringLengthConstraint constraint) : base("ADObjectNameStringLengthValidator", constraint.MinLength, constraint.MaxLength)
		{
		}
	}
}
