using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E1 RID: 737
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidHandleTypePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002425 RID: 9253 RVA: 0x0004F97C File Offset: 0x0004DB7C
		public InvalidHandleTypePermanentException(long handle, string handleType, string expectedType) : base(MrsStrings.InvalidHandleType(handle, handleType, expectedType))
		{
			this.handle = handle;
			this.handleType = handleType;
			this.expectedType = expectedType;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0004F9A1 File Offset: 0x0004DBA1
		public InvalidHandleTypePermanentException(long handle, string handleType, string expectedType, Exception innerException) : base(MrsStrings.InvalidHandleType(handle, handleType, expectedType), innerException)
		{
			this.handle = handle;
			this.handleType = handleType;
			this.expectedType = expectedType;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x0004F9C8 File Offset: 0x0004DBC8
		protected InvalidHandleTypePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.handle = (long)info.GetValue("handle", typeof(long));
			this.handleType = (string)info.GetValue("handleType", typeof(string));
			this.expectedType = (string)info.GetValue("expectedType", typeof(string));
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0004FA3D File Offset: 0x0004DC3D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("handle", this.handle);
			info.AddValue("handleType", this.handleType);
			info.AddValue("expectedType", this.expectedType);
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x0004FA7A File Offset: 0x0004DC7A
		public long Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0004FA82 File Offset: 0x0004DC82
		public string HandleType
		{
			get
			{
				return this.handleType;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x0004FA8A File Offset: 0x0004DC8A
		public string ExpectedType
		{
			get
			{
				return this.expectedType;
			}
		}

		// Token: 0x04000FEA RID: 4074
		private readonly long handle;

		// Token: 0x04000FEB RID: 4075
		private readonly string handleType;

		// Token: 0x04000FEC RID: 4076
		private readonly string expectedType;
	}
}
