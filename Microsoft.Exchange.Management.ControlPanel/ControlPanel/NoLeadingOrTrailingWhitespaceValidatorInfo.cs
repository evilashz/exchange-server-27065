using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006DD RID: 1757
	[DataContract]
	public class NoLeadingOrTrailingWhitespaceValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A38 RID: 19000 RVA: 0x000E3511 File Offset: 0x000E1711
		public NoLeadingOrTrailingWhitespaceValidatorInfo() : base("NoLeadingOrTrailingWhitespaceValidator")
		{
		}
	}
}
