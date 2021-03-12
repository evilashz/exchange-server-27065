using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B2 RID: 690
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BlockedTypeException : MailboxReplicationPermanentException
	{
		// Token: 0x0600233A RID: 9018 RVA: 0x0004E298 File Offset: 0x0004C498
		public BlockedTypeException(string type) : base(MrsStrings.BlockedType(type))
		{
			this.type = type;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0004E2AD File Offset: 0x0004C4AD
		public BlockedTypeException(string type, Exception innerException) : base(MrsStrings.BlockedType(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0004E2C3 File Offset: 0x0004C4C3
		protected BlockedTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0004E2ED File Offset: 0x0004C4ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x0004E308 File Offset: 0x0004C508
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000FBB RID: 4027
		private readonly string type;
	}
}
