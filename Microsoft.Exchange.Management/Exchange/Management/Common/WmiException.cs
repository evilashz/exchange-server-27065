using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public class WmiException : LocalizedException
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x0001D593 File Offset: 0x0001B793
		public WmiException(Exception e, string computerName) : base((e == null) ? Strings.ErrorWMIException(computerName) : Strings.ErrorWMIException2(computerName, e.Message), e)
		{
		}
	}
}
