using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000367 RID: 871
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class StorageConnectionTypeException : MailboxReplicationPermanentException
	{
		// Token: 0x060026C0 RID: 9920 RVA: 0x000539CA File Offset: 0x00051BCA
		public StorageConnectionTypeException(string type) : base(MrsStrings.StorageConnectionType(type))
		{
			this.type = type;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000539DF File Offset: 0x00051BDF
		public StorageConnectionTypeException(string type, Exception innerException) : base(MrsStrings.StorageConnectionType(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000539F5 File Offset: 0x00051BF5
		protected StorageConnectionTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00053A1F File Offset: 0x00051C1F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x00053A3A File Offset: 0x00051C3A
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0400106D RID: 4205
		private readonly string type;
	}
}
