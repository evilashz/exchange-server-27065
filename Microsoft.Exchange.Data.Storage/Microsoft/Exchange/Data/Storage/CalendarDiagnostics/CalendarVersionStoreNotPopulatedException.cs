using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000373 RID: 883
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class CalendarVersionStoreNotPopulatedException : StorageTransientException
	{
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x0009C4B2 File Offset: 0x0009A6B2
		// (set) Token: 0x060026FB RID: 9979 RVA: 0x0009C4BA File Offset: 0x0009A6BA
		public bool IsCreated { get; private set; }

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x0009C4C3 File Offset: 0x0009A6C3
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x0009C4CB File Offset: 0x0009A6CB
		public SearchState FolderState { get; private set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x0009C4D4 File Offset: 0x0009A6D4
		// (set) Token: 0x060026FF RID: 9983 RVA: 0x0009C4DC File Offset: 0x0009A6DC
		public TimeSpan WaitTimeBeforeThrow { get; private set; }

		// Token: 0x06002700 RID: 9984 RVA: 0x0009C4E5 File Offset: 0x0009A6E5
		public CalendarVersionStoreNotPopulatedException(bool isCreated, SearchState folderState, TimeSpan waitTime) : this(isCreated, folderState, waitTime, null)
		{
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0009C4F1 File Offset: 0x0009A6F1
		public CalendarVersionStoreNotPopulatedException(bool isCreated, SearchState folderState, TimeSpan waitTime, Exception innerException) : base(ServerStrings.CVSPopulationTimedout, innerException)
		{
			this.IsCreated = isCreated;
			this.FolderState = folderState;
			this.WaitTimeBeforeThrow = waitTime;
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x0009C518 File Offset: 0x0009A718
		protected CalendarVersionStoreNotPopulatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this.IsCreated = info.GetBoolean("IsCreated");
				this.FolderState = (SearchState)info.GetValue("FolderState", typeof(SearchState));
				this.WaitTimeBeforeThrow = (TimeSpan)info.GetValue("WaitTimeBeforeThrow", typeof(TimeSpan));
			}
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x0009C584 File Offset: 0x0009A784
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("IsCreated", this.IsCreated, typeof(bool));
			info.AddValue("FolderState", this.FolderState, typeof(SearchState));
			info.AddValue("WaitTimeBeforeThrow", this.WaitTimeBeforeThrow, typeof(TimeSpan));
		}

		// Token: 0x04001726 RID: 5926
		private const string IsCreatedKey = "IsCreated";

		// Token: 0x04001727 RID: 5927
		private const string FolderStateKey = "FolderState";

		// Token: 0x04001728 RID: 5928
		private const string WaitTimeBeforeThrowKey = "WaitTimeBeforeThrow";
	}
}
