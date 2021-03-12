using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000146 RID: 326
	public class TaskDiagnosticInformation
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x000403CC File Offset: 0x0003E5CC
		public TaskDiagnosticInformation(TaskTypeId taskTypeId, ClientType clientType, Guid databaseGuid) : this(taskTypeId, clientType, databaseGuid, Guid.Empty, Guid.Empty, null, null, null)
		{
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000403F0 File Offset: 0x0003E5F0
		public TaskDiagnosticInformation(TaskTypeId taskTypeId, ClientType clientType, Guid databaseGuid, Guid mailboxGuid, Guid clientActivityId, string clientComponentName, string clientProtocolName, string clientActionString)
		{
			this.taskTypeId = taskTypeId;
			this.clientType = clientType;
			this.databaseGuid = databaseGuid;
			this.mailboxGuid = mailboxGuid;
			this.clientActivityId = clientActivityId;
			this.clientComponentName = clientComponentName;
			this.clientProtocolName = clientProtocolName;
			this.clientActionString = clientActionString;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00040440 File Offset: 0x0003E640
		public TaskTypeId TaskTypeId
		{
			get
			{
				return this.taskTypeId;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00040448 File Offset: 0x0003E648
		public ClientType ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00040450 File Offset: 0x0003E650
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00040458 File Offset: 0x0003E658
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00040460 File Offset: 0x0003E660
		public Guid ClientActivityId
		{
			get
			{
				return this.clientActivityId;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00040468 File Offset: 0x0003E668
		public string ClientComponentName
		{
			get
			{
				return this.clientComponentName;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00040470 File Offset: 0x0003E670
		public string ClientProtocolName
		{
			get
			{
				return this.clientProtocolName;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00040478 File Offset: 0x0003E678
		public string ClientActionString
		{
			get
			{
				return this.clientActionString;
			}
		}

		// Token: 0x04000727 RID: 1831
		private readonly TaskTypeId taskTypeId;

		// Token: 0x04000728 RID: 1832
		private readonly ClientType clientType;

		// Token: 0x04000729 RID: 1833
		private readonly Guid databaseGuid;

		// Token: 0x0400072A RID: 1834
		private readonly Guid mailboxGuid;

		// Token: 0x0400072B RID: 1835
		private readonly Guid clientActivityId;

		// Token: 0x0400072C RID: 1836
		private readonly string clientComponentName;

		// Token: 0x0400072D RID: 1837
		private readonly string clientProtocolName;

		// Token: 0x0400072E RID: 1838
		private readonly string clientActionString;
	}
}
