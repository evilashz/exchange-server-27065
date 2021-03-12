using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200034F RID: 847
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToStreamPSTPropPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002641 RID: 9793 RVA: 0x00052C17 File Offset: 0x00050E17
		public UnableToStreamPSTPropPermanentException(uint propTag, int offset, int bytesToRead, long length) : base(MrsStrings.UnableToStreamPSTProp(propTag, offset, bytesToRead, length))
		{
			this.propTag = propTag;
			this.offset = offset;
			this.bytesToRead = bytesToRead;
			this.length = length;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00052C46 File Offset: 0x00050E46
		public UnableToStreamPSTPropPermanentException(uint propTag, int offset, int bytesToRead, long length, Exception innerException) : base(MrsStrings.UnableToStreamPSTProp(propTag, offset, bytesToRead, length), innerException)
		{
			this.propTag = propTag;
			this.offset = offset;
			this.bytesToRead = bytesToRead;
			this.length = length;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00052C78 File Offset: 0x00050E78
		protected UnableToStreamPSTPropPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propTag = (uint)info.GetValue("propTag", typeof(uint));
			this.offset = (int)info.GetValue("offset", typeof(int));
			this.bytesToRead = (int)info.GetValue("bytesToRead", typeof(int));
			this.length = (long)info.GetValue("length", typeof(long));
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00052D10 File Offset: 0x00050F10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propTag", this.propTag);
			info.AddValue("offset", this.offset);
			info.AddValue("bytesToRead", this.bytesToRead);
			info.AddValue("length", this.length);
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x00052D69 File Offset: 0x00050F69
		public uint PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x00052D71 File Offset: 0x00050F71
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x00052D79 File Offset: 0x00050F79
		public int BytesToRead
		{
			get
			{
				return this.bytesToRead;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x00052D81 File Offset: 0x00050F81
		public long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x0400104E RID: 4174
		private readonly uint propTag;

		// Token: 0x0400104F RID: 4175
		private readonly int offset;

		// Token: 0x04001050 RID: 4176
		private readonly int bytesToRead;

		// Token: 0x04001051 RID: 4177
		private readonly long length;
	}
}
