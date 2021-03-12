using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020006C4 RID: 1732
	[Serializable]
	public sealed class StringList : List<string>
	{
		// Token: 0x06002060 RID: 8288 RVA: 0x0003F92C File Offset: 0x0003DB2C
		public StringList()
		{
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x0003F934 File Offset: 0x0003DB34
		public StringList(IEnumerable<string> collection) : base(collection)
		{
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0003F940 File Offset: 0x0003DB40
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(40 * base.Count);
			foreach (string value in this)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}
	}
}
