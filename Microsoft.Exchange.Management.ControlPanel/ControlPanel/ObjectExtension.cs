using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B0 RID: 1712
	public static class ObjectExtension
	{
		// Token: 0x0600490F RID: 18703 RVA: 0x000DF646 File Offset: 0x000DD846
		public static string StringArrayJoin(this object stringsToJoin, string separator)
		{
			if (stringsToJoin is string[])
			{
				return string.Join(separator, (string[])stringsToJoin);
			}
			return null;
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x000DF65E File Offset: 0x000DD85E
		public static string ToStringWithNull(this object toStringObject)
		{
			if (toStringObject != null)
			{
				return toStringObject.ToString();
			}
			return null;
		}
	}
}
