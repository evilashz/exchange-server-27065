using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F0 RID: 496
	public class QueryStringParameters : Dictionary<string, string>
	{
		// Token: 0x06001026 RID: 4134 RVA: 0x000645FD File Offset: 0x000627FD
		public string GetValue(string name)
		{
			if (base.ContainsKey(name))
			{
				return base[name];
			}
			return null;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00064614 File Offset: 0x00062814
		public string QueryString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append("?");
				bool flag = false;
				foreach (string text in base.Keys)
				{
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = base[text];
						if (!string.IsNullOrEmpty(text2))
						{
							if (flag)
							{
								stringBuilder.Append("&");
							}
							else
							{
								flag = true;
							}
							stringBuilder.Append(text);
							stringBuilder.Append("=");
							stringBuilder.Append(Utilities.UrlEncode(text2));
						}
					}
				}
				return stringBuilder.ToString();
			}
		}
	}
}
