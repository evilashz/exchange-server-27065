using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000364 RID: 868
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnknownRestrictionTypeException : MailboxReplicationPermanentException
	{
		// Token: 0x060026B1 RID: 9905 RVA: 0x00053862 File Offset: 0x00051A62
		public UnknownRestrictionTypeException(string type) : base(MrsStrings.UnknownRestrictionType(type))
		{
			this.type = type;
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x00053877 File Offset: 0x00051A77
		public UnknownRestrictionTypeException(string type, Exception innerException) : base(MrsStrings.UnknownRestrictionType(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x0005388D File Offset: 0x00051A8D
		protected UnknownRestrictionTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000538B7 File Offset: 0x00051AB7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000538D2 File Offset: 0x00051AD2
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0400106A RID: 4202
		private readonly string type;
	}
}
