using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E7 RID: 487
	public class UIValidationEventArgs : EventArgs
	{
		// Token: 0x060015DC RID: 5596 RVA: 0x0005A0AD File Offset: 0x000582AD
		public UIValidationEventArgs(ICollection<UIValidationError> errors)
		{
			this.errors = errors;
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0005A0BC File Offset: 0x000582BC
		public ICollection<UIValidationError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x040007E8 RID: 2024
		private ICollection<UIValidationError> errors;
	}
}
