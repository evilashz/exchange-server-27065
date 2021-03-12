using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000191 RID: 401
	public class ValueReference
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00052916 File Offset: 0x00050B16
		public bool IsZero
		{
			get
			{
				return object.ReferenceEquals(this, ValueReference.Zero);
			}
		}

		// Token: 0x040009C0 RID: 2496
		public static readonly ValueReference Zero = new ValueReference();
	}
}
