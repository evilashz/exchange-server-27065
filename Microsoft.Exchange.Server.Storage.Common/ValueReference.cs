using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C8 RID: 200
	public class ValueReference
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001D539 File Offset: 0x0001B739
		public bool IsZero
		{
			get
			{
				return object.ReferenceEquals(this, ValueReference.Zero);
			}
		}

		// Token: 0x04000773 RID: 1907
		public static readonly ValueReference Zero = new ValueReference();
	}
}
