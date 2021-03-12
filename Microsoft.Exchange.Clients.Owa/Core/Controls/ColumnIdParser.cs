using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002B8 RID: 696
	internal sealed class ColumnIdParser
	{
		// Token: 0x06001B38 RID: 6968 RVA: 0x0009B9CC File Offset: 0x00099BCC
		private ColumnIdParser()
		{
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0009B9D4 File Offset: 0x00099BD4
		public static string GetString(ColumnId value)
		{
			return ColumnIdParser.columnId.GetString((int)value);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0009B9E4 File Offset: 0x00099BE4
		public static ColumnId Parse(string value)
		{
			object obj = ColumnIdParser.columnId.Parse(value);
			if (obj == null)
			{
				return ColumnId.Count;
			}
			return (ColumnId)obj;
		}

		// Token: 0x04001399 RID: 5017
		private static FastEnumParser columnId = new FastEnumParser(typeof(ColumnId));
	}
}
