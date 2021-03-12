using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000054 RID: 84
	internal class ProgressTrackerTransmitter : DisposableWrapper<IDataImport>, IDataImport, IDisposable
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x00019F9B File Offset: 0x0001819B
		public ProgressTrackerTransmitter(IDataImport destination, BaseJob job) : base(destination, true)
		{
			this.destination = base.WrappedObject;
			this.job = job;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00019FB8 File Offset: 0x000181B8
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage request)
		{
			this.UpdateTracker(request);
			IDataMessage dataMessage = this.destination.SendMessageAndWaitForReply(request);
			this.UpdateTracker(dataMessage);
			return dataMessage;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00019FE1 File Offset: 0x000181E1
		void IDataImport.SendMessage(IDataMessage message)
		{
			this.UpdateTracker(message);
			this.destination.SendMessage(message);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00019FF8 File Offset: 0x000181F8
		private void UpdateTracker(IDataMessage message)
		{
			if (this.job.ProgressTracker != null && message != null)
			{
				uint size = (uint)message.GetSize();
				this.job.ProgressTracker.AddBytes(size);
				MRSResource.Cache.GetInstance(MRSResource.Id.ObjectGuid, this.job.WorkloadTypeFromJob).Charge(size);
			}
		}

		// Token: 0x040001DF RID: 479
		private IDataImport destination;

		// Token: 0x040001E0 RID: 480
		private BaseJob job;
	}
}
