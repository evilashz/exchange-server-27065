using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006D8 RID: 1752
	[DataContract]
	public class AsciiCharactersOnlyValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A29 RID: 18985 RVA: 0x000E342B File Offset: 0x000E162B
		public AsciiCharactersOnlyValidatorInfo() : base("AsciiCharactersOnlyValidator")
		{
		}
	}
}
