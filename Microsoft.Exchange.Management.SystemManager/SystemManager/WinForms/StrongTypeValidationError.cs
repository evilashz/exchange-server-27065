using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000163 RID: 355
	public class StrongTypeValidationError : ValidationError
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x000379C0 File Offset: 0x00035BC0
		public StrongTypeValidationError(LocalizedString description, string propertyName) : base(description, propertyName)
		{
		}
	}
}
