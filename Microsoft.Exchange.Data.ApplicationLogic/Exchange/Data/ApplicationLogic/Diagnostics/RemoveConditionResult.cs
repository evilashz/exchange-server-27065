using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000E3 RID: 227
	public class RemoveConditionResult
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x00025752 File Offset: 0x00023952
		public RemoveConditionResult()
		{
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002575C File Offset: 0x0002395C
		public RemoveConditionResult(string cookie, bool removed)
		{
			this.Results = new List<SingleCookieRemoveResult>();
			this.Results.Add(new SingleCookieRemoveResult
			{
				Cookie = cookie,
				Removed = removed
			});
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002579C File Offset: 0x0002399C
		public RemoveConditionResult(List<string> cookiesRemoved)
		{
			this.Results = new List<SingleCookieRemoveResult>(cookiesRemoved.Count);
			foreach (string cookie in cookiesRemoved)
			{
				this.Results.Add(new SingleCookieRemoveResult
				{
					Cookie = cookie,
					Removed = true
				});
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0002581C File Offset: 0x00023A1C
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00025824 File Offset: 0x00023A24
		[XmlArrayItem("Conditional")]
		public List<SingleCookieRemoveResult> Results { get; set; }
	}
}
