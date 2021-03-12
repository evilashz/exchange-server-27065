using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class CookieRecords
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000083CF File Offset: 0x000065CF
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000083D7 File Offset: 0x000065D7
		public MultiValuedProperty<CookieRecord> Records
		{
			get
			{
				return this.cookieRecords;
			}
			set
			{
				this.cookieRecords = value;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000083E0 File Offset: 0x000065E0
		public void Load(Dictionary<string, Cookie> cookies)
		{
			foreach (Cookie cookie in cookies.Values)
			{
				CookieRecord cookieRecord = new CookieRecord();
				cookieRecord.BaseDN = cookie.BaseDN;
				cookieRecord.DomainController = cookie.DomainController;
				cookieRecord.LastUpdated = cookie.LastUpdated;
				cookieRecord.CookieLength = cookie.CookieValue.Length;
				this.cookieRecords.Add(cookieRecord);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008470 File Offset: 0x00006670
		public override string ToString()
		{
			return "Number of cookies " + this.Records.Count;
		}

		// Token: 0x04000104 RID: 260
		private MultiValuedProperty<CookieRecord> cookieRecords = new MultiValuedProperty<CookieRecord>();
	}
}
