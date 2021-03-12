using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200003B RID: 59
	public class SupplementalData
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00006D23 File Offset: 0x00004F23
		public SupplementalData()
		{
			this.data = new Dictionary<string, Dictionary<string, string>>();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006D38 File Offset: 0x00004F38
		public void Add(string dataType, KeyValuePair<string, string> supplementalData)
		{
			Dictionary<string, string> dictionary;
			if (this.data.TryGetValue(dataType, out dictionary))
			{
				if (!dictionary.ContainsKey(supplementalData.Key))
				{
					dictionary.Add(supplementalData.Key, supplementalData.Value);
					return;
				}
			}
			else
			{
				dictionary = new Dictionary<string, string>(1);
				dictionary.Add(supplementalData.Key, supplementalData.Value);
				this.data[dataType] = dictionary;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public Dictionary<string, string> Get(string dataType)
		{
			Dictionary<string, string> result;
			if (this.data.TryGetValue(dataType, out result))
			{
				return result;
			}
			return new Dictionary<string, string>();
		}

		// Token: 0x040000B1 RID: 177
		private readonly Dictionary<string, Dictionary<string, string>> data;
	}
}
