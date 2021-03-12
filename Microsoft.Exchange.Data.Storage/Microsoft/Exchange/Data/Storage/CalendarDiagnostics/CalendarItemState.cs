using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000362 RID: 866
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarItemState
	{
		// Token: 0x0600268F RID: 9871 RVA: 0x0009A914 File Offset: 0x00098B14
		internal CalendarItemState()
		{
			this.stateData = new Dictionary<string, object>();
		}

		// Token: 0x17000CD9 RID: 3289
		internal object this[string key]
		{
			get
			{
				return this.stateData[key];
			}
			set
			{
				this.stateData[key] = value;
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x0009A944 File Offset: 0x00098B44
		internal bool ContainsKey(string key)
		{
			return this.stateData.ContainsKey(key);
		}

		// Token: 0x040016FF RID: 5887
		private Dictionary<string, object> stateData;
	}
}
