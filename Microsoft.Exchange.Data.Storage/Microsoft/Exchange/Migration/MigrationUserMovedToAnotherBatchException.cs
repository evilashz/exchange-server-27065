using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000143 RID: 323
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationUserMovedToAnotherBatchException : MigrationPermanentException
	{
		// Token: 0x060015B9 RID: 5561 RVA: 0x0006E593 File Offset: 0x0006C793
		public MigrationUserMovedToAnotherBatchException(string batchName) : base(Strings.MigrationUserMovedToAnotherBatch(batchName))
		{
			this.batchName = batchName;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0006E5A8 File Offset: 0x0006C7A8
		public MigrationUserMovedToAnotherBatchException(string batchName, Exception innerException) : base(Strings.MigrationUserMovedToAnotherBatch(batchName), innerException)
		{
			this.batchName = batchName;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0006E5BE File Offset: 0x0006C7BE
		protected MigrationUserMovedToAnotherBatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.batchName = (string)info.GetValue("batchName", typeof(string));
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0006E5E8 File Offset: 0x0006C7E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("batchName", this.batchName);
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0006E603 File Offset: 0x0006C803
		public string BatchName
		{
			get
			{
				return this.batchName;
			}
		}

		// Token: 0x04000AD5 RID: 2773
		private readonly string batchName;
	}
}
