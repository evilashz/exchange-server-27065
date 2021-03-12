using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000164 RID: 356
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserDuplicateInOtherBatchException : MigrationPermanentException
	{
		// Token: 0x0600165E RID: 5726 RVA: 0x0006F55C File Offset: 0x0006D75C
		public UserDuplicateInOtherBatchException(string alias, string batchName) : base(Strings.UserDuplicateInOtherBatch(alias, batchName))
		{
			this.alias = alias;
			this.batchName = batchName;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0006F579 File Offset: 0x0006D779
		public UserDuplicateInOtherBatchException(string alias, string batchName, Exception innerException) : base(Strings.UserDuplicateInOtherBatch(alias, batchName), innerException)
		{
			this.alias = alias;
			this.batchName = batchName;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0006F598 File Offset: 0x0006D798
		protected UserDuplicateInOtherBatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.alias = (string)info.GetValue("alias", typeof(string));
			this.batchName = (string)info.GetValue("batchName", typeof(string));
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0006F5ED File Offset: 0x0006D7ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("alias", this.alias);
			info.AddValue("batchName", this.batchName);
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0006F619 File Offset: 0x0006D819
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0006F621 File Offset: 0x0006D821
		public string BatchName
		{
			get
			{
				return this.batchName;
			}
		}

		// Token: 0x04000AF6 RID: 2806
		private readonly string alias;

		// Token: 0x04000AF7 RID: 2807
		private readonly string batchName;
	}
}
