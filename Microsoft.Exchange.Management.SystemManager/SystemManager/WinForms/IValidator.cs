using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200004E RID: 78
	public interface IValidator
	{
		// Token: 0x060002F8 RID: 760
		ValidationError[] Validate();
	}
}
