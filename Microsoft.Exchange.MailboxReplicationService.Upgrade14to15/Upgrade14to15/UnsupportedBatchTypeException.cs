using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E4 RID: 228
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnsupportedBatchTypeException : MigrationTransientException
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x0000FA6E File Offset: 0x0000DC6E
		public UnsupportedBatchTypeException(string batchType) : base(UpgradeHandlerStrings.UnsupportedBatchType(batchType))
		{
			this.batchType = batchType;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000FA83 File Offset: 0x0000DC83
		public UnsupportedBatchTypeException(string batchType, Exception innerException) : base(UpgradeHandlerStrings.UnsupportedBatchType(batchType), innerException)
		{
			this.batchType = batchType;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000FA99 File Offset: 0x0000DC99
		protected UnsupportedBatchTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.batchType = (string)info.GetValue("batchType", typeof(string));
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000FAC3 File Offset: 0x0000DCC3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("batchType", this.batchType);
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0000FADE File Offset: 0x0000DCDE
		public string BatchType
		{
			get
			{
				return this.batchType;
			}
		}

		// Token: 0x04000395 RID: 917
		private readonly string batchType;
	}
}
