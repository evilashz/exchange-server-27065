using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000361 RID: 865
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptNamedPropDataException : MailboxReplicationPermanentException
	{
		// Token: 0x060026A3 RID: 9891 RVA: 0x00053743 File Offset: 0x00051943
		public CorruptNamedPropDataException(string type) : base(MrsStrings.CorruptNamedPropData(type))
		{
			this.type = type;
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x00053758 File Offset: 0x00051958
		public CorruptNamedPropDataException(string type, Exception innerException) : base(MrsStrings.CorruptNamedPropData(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x0005376E File Offset: 0x0005196E
		protected CorruptNamedPropDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x00053798 File Offset: 0x00051998
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000537B3 File Offset: 0x000519B3
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04001068 RID: 4200
		private readonly string type;
	}
}
