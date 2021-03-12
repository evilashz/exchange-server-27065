using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000002 RID: 2
	public sealed class BoxedConstants
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static object GetBool(bool value)
		{
			if (!value)
			{
				return BoxedConstants.False;
			}
			return BoxedConstants.True;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E0 File Offset: 0x000002E0
		public static object GetBool(bool? value)
		{
			if (value == null)
			{
				return null;
			}
			if (!value.Value)
			{
				return BoxedConstants.False;
			}
			return BoxedConstants.True;
		}

		// Token: 0x04000001 RID: 1
		public static readonly object True = true;

		// Token: 0x04000002 RID: 2
		public static readonly object False = false;
	}
}
