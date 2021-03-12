using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200005D RID: 93
	public interface IDataObjectValidator
	{
		// Token: 0x060003AF RID: 943
		ValidationError[] Validate(object dataObject);
	}
}
