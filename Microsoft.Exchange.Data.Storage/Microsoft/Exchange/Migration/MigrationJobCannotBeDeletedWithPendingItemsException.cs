using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000145 RID: 325
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationJobCannotBeDeletedWithPendingItemsException : MigrationPermanentException
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x0006E6D9 File Offset: 0x0006C8D9
		public MigrationJobCannotBeDeletedWithPendingItemsException(int count) : base(Strings.MigrationJobCannotBeDeletedWithPendingItems(count))
		{
			this.count = count;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0006E6EE File Offset: 0x0006C8EE
		public MigrationJobCannotBeDeletedWithPendingItemsException(int count, Exception innerException) : base(Strings.MigrationJobCannotBeDeletedWithPendingItems(count), innerException)
		{
			this.count = count;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0006E704 File Offset: 0x0006C904
		protected MigrationJobCannotBeDeletedWithPendingItemsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.count = (int)info.GetValue("count", typeof(int));
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0006E72E File Offset: 0x0006C92E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("count", this.count);
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0006E749 File Offset: 0x0006C949
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x04000AD8 RID: 2776
		private readonly int count;
	}
}
