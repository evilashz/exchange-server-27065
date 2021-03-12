using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000E3 RID: 227
	public class SupplementalData
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x00012E2B File Offset: 0x0001102B
		public SupplementalData()
		{
			this.data = new Dictionary<string, Dictionary<string, string>>();
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00012E40 File Offset: 0x00011040
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

		// Token: 0x060005E5 RID: 1509 RVA: 0x00012EAC File Offset: 0x000110AC
		public Dictionary<string, string> Get(string dataType)
		{
			Dictionary<string, string> result;
			if (this.data.TryGetValue(dataType, out result))
			{
				return result;
			}
			return new Dictionary<string, string>();
		}

		// Token: 0x0400039E RID: 926
		private readonly Dictionary<string, Dictionary<string, string>> data;
	}
}
