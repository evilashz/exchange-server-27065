using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003DC RID: 988
	internal class ComputerNameCharacterConstraint : CharacterRegexConstraint
	{
		// Token: 0x06002D57 RID: 11607 RVA: 0x000BA784 File Offset: 0x000B8984
		public ComputerNameCharacterConstraint() : base("[a-z0-9A-Z\\-]")
		{
		}

		// Token: 0x04001E8B RID: 7819
		public static ComputerNameCharacterConstraint DefaultConstraint = new ComputerNameCharacterConstraint();
	}
}
