using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006DB RID: 1755
	[DataContract]
	public class ComputerNameCharacterValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A36 RID: 18998 RVA: 0x000E34F7 File Offset: 0x000E16F7
		internal ComputerNameCharacterValidatorInfo(ComputerNameCharacterConstraint constraint) : base("ComputerNameCharacterValidator")
		{
		}
	}
}
