using System;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000983 RID: 2435
	internal class CmdletException : Exception
	{
		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x060045B2 RID: 17842 RVA: 0x000F4CB1 File Offset: 0x000F2EB1
		// (set) Token: 0x060045B3 RID: 17843 RVA: 0x000F4CB9 File Offset: 0x000F2EB9
		public OptionsActionError ErrorCode { get; set; }

		// Token: 0x060045B4 RID: 17844 RVA: 0x000F4CC2 File Offset: 0x000F2EC2
		public CmdletException(OptionsActionError errorCode, string errorMessage) : base(errorMessage)
		{
			this.ErrorCode = errorCode;
		}
	}
}
