using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D8 RID: 728
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestGuidNotUniquePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023F3 RID: 9203 RVA: 0x0004F38E File Offset: 0x0004D58E
		public RequestGuidNotUniquePermanentException(string guid, string type) : base(MrsStrings.RequestGuidNotUnique(guid, type))
		{
			this.guid = guid;
			this.type = type;
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0004F3AB File Offset: 0x0004D5AB
		public RequestGuidNotUniquePermanentException(string guid, string type, Exception innerException) : base(MrsStrings.RequestGuidNotUnique(guid, type), innerException)
		{
			this.guid = guid;
			this.type = type;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x0004F3CC File Offset: 0x0004D5CC
		protected RequestGuidNotUniquePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (string)info.GetValue("guid", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x0004F421 File Offset: 0x0004D621
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
			info.AddValue("type", this.type);
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x0004F44D File Offset: 0x0004D64D
		public string Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x0004F455 File Offset: 0x0004D655
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000FDC RID: 4060
		private readonly string guid;

		// Token: 0x04000FDD RID: 4061
		private readonly string type;
	}
}
