using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000A1 RID: 161
	public class ExProgressRecord : ProgressRecord
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x00017E41 File Offset: 0x00016041
		public ExProgressRecord(int activityId, LocalizedString activity, LocalizedString statusDescription) : base(activityId, activity.ToString(), statusDescription.ToString())
		{
		}
	}
}
