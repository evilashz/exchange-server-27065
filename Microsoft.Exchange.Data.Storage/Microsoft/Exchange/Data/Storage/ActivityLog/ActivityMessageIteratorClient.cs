using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x02000340 RID: 832
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ActivityMessageIteratorClient : DisposableObject, IMessageIteratorClient, IDisposable
	{
		// Token: 0x060024E5 RID: 9445 RVA: 0x00094EC4 File Offset: 0x000930C4
		public ActivityMessageIteratorClient(Action<Activity> deserializationAction)
		{
			Util.ThrowOnNullArgument(deserializationAction, "deserializationAction");
			this.deserializationAction = deserializationAction;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00094EDE File Offset: 0x000930DE
		public IMessage UploadMessage(bool isAssociatedMessage)
		{
			if (isAssociatedMessage)
			{
				throw new NotSupportedException("Activity cannot have isAssociatedMessage flag set.");
			}
			return Activity.CreateMessageAdapter(this.deserializationAction);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00094EF9 File Offset: 0x000930F9
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ActivityMessageIteratorClient>(this);
		}

		// Token: 0x0400165C RID: 5724
		private readonly Action<Activity> deserializationAction;
	}
}
