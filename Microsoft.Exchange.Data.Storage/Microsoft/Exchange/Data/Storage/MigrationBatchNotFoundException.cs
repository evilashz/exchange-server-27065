using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000113 RID: 275
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationBatchNotFoundException : ObjectNotFoundException
	{
		// Token: 0x060013EF RID: 5103 RVA: 0x0006A005 File Offset: 0x00068205
		public MigrationBatchNotFoundException(string batchName) : base(ServerStrings.MigrationBatchNotFoundError(batchName))
		{
			this.batchName = batchName;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0006A01A File Offset: 0x0006821A
		public MigrationBatchNotFoundException(string batchName, Exception innerException) : base(ServerStrings.MigrationBatchNotFoundError(batchName), innerException)
		{
			this.batchName = batchName;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0006A030 File Offset: 0x00068230
		protected MigrationBatchNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.batchName = (string)info.GetValue("batchName", typeof(string));
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0006A05A File Offset: 0x0006825A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("batchName", this.batchName);
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0006A075 File Offset: 0x00068275
		public string BatchName
		{
			get
			{
				return this.batchName;
			}
		}

		// Token: 0x040009A2 RID: 2466
		private readonly string batchName;
	}
}
