using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000162 RID: 354
	public class StrongTypeException : Exception
	{
		// Token: 0x06000E85 RID: 3717 RVA: 0x000379A8 File Offset: 0x00035BA8
		public StrongTypeException(string message, bool isTargetProperty) : base(message)
		{
			this.isTargetProperty = isTargetProperty;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x000379B8 File Offset: 0x00035BB8
		public bool IsTargetProperty
		{
			get
			{
				return this.isTargetProperty;
			}
		}

		// Token: 0x040005E0 RID: 1504
		private bool isTargetProperty;
	}
}
