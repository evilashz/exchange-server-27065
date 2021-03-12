using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000372 RID: 882
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUidException : MailboxReplicationPermanentException
	{
		// Token: 0x060026F4 RID: 9972 RVA: 0x00053E25 File Offset: 0x00052025
		public InvalidUidException(string uid) : base(MrsStrings.InvalidUid(uid))
		{
			this.uid = uid;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x00053E3A File Offset: 0x0005203A
		public InvalidUidException(string uid, Exception innerException) : base(MrsStrings.InvalidUid(uid), innerException)
		{
			this.uid = uid;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00053E50 File Offset: 0x00052050
		protected InvalidUidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uid = (string)info.GetValue("uid", typeof(string));
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x00053E7A File Offset: 0x0005207A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uid", this.uid);
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x00053E95 File Offset: 0x00052095
		public string Uid
		{
			get
			{
				return this.uid;
			}
		}

		// Token: 0x04001075 RID: 4213
		private readonly string uid;
	}
}
