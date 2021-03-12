using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000036 RID: 54
	internal sealed class InvokeArgs
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00009927 File Offset: 0x00007B27
		private InvokeArgs(StoreSession storeSession, MailboxData mailboxData)
		{
			this.StoreSession = storeSession;
			this.mailboxData = mailboxData;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000993D File Offset: 0x00007B3D
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00009945 File Offset: 0x00007B45
		public Guid ActivityId { get; internal set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000994E File Offset: 0x00007B4E
		public MailboxData MailboxData
		{
			get
			{
				return this.mailboxData;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00009956 File Offset: 0x00007B56
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000995E File Offset: 0x00007B5E
		public string Parameters { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00009967 File Offset: 0x00007B67
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000996F File Offset: 0x00007B6F
		public StoreSession StoreSession { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00009978 File Offset: 0x00007B78
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00009980 File Offset: 0x00007B80
		public TimeSpan TimePerTask { get; private set; }

		// Token: 0x060001CF RID: 463 RVA: 0x0000998C File Offset: 0x00007B8C
		public static InvokeArgs Create(StoreSession storeSession)
		{
			StoreMailboxData storeMailboxData = new StoreMailboxData(storeSession.MailboxGuid, storeSession.MdbGuid, storeSession.DisplayAddress, null);
			return InvokeArgs.Create(storeSession, storeMailboxData);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000099BC File Offset: 0x00007BBC
		public static InvokeArgs Create(StoreSession storeSession, MailboxData mailboxData)
		{
			return new InvokeArgs(storeSession, mailboxData)
			{
				TimePerTask = TimeSpan.Zero
			};
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000099E0 File Offset: 0x00007BE0
		public static InvokeArgs Create(StoreSession storeSession, TimeSpan timePerTask, MailboxData mailboxData)
		{
			InvokeArgs invokeArgs = InvokeArgs.Create(storeSession, mailboxData);
			invokeArgs.TimePerTask = timePerTask;
			MailboxDataForDemandJob mailboxDataForDemandJob = mailboxData as MailboxDataForDemandJob;
			if (mailboxDataForDemandJob != null)
			{
				invokeArgs.Parameters = mailboxDataForDemandJob.Parameters;
			}
			return invokeArgs;
		}

		// Token: 0x0400016C RID: 364
		private MailboxData mailboxData;
	}
}
