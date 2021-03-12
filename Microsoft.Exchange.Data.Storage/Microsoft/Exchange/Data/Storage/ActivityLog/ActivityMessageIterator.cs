using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x0200033E RID: 830
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ActivityMessageIterator : DisposableObject, IMessageIterator, IDisposable
	{
		// Token: 0x060024D6 RID: 9430 RVA: 0x00094B10 File Offset: 0x00092D10
		public ActivityMessageIterator(IEnumerable<Activity> activities)
		{
			Util.ThrowOnNullArgument(activities, "activities");
			this.activities = activities;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00094C64 File Offset: 0x00092E64
		public IEnumerator<IMessage> GetMessages()
		{
			foreach (Activity activity in this.activities)
			{
				yield return activity.CreateMessageAdapter();
			}
			yield break;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00094C80 File Offset: 0x00092E80
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ActivityMessageIterator>(this);
		}

		// Token: 0x04001658 RID: 5720
		private readonly IEnumerable<Activity> activities;
	}
}
