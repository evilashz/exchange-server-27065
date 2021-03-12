using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006D6 RID: 1750
	[DataContract]
	public class NotNullOrEmptyValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A25 RID: 18981 RVA: 0x000E33F4 File Offset: 0x000E15F4
		public NotNullOrEmptyValidatorInfo() : base("NotNullOrEmptyValidator")
		{
		}
	}
}
