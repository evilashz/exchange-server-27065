using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000371 RID: 881
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedSyncProtocolException : MailboxReplicationPermanentException
	{
		// Token: 0x060026EF RID: 9967 RVA: 0x00053DAD File Offset: 0x00051FAD
		public UnsupportedSyncProtocolException(string protocol) : base(MrsStrings.UnsupportedSyncProtocol(protocol))
		{
			this.protocol = protocol;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00053DC2 File Offset: 0x00051FC2
		public UnsupportedSyncProtocolException(string protocol, Exception innerException) : base(MrsStrings.UnsupportedSyncProtocol(protocol), innerException)
		{
			this.protocol = protocol;
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00053DD8 File Offset: 0x00051FD8
		protected UnsupportedSyncProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.protocol = (string)info.GetValue("protocol", typeof(string));
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00053E02 File Offset: 0x00052002
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("protocol", this.protocol);
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x00053E1D File Offset: 0x0005201D
		public string Protocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x04001074 RID: 4212
		private readonly string protocol;
	}
}
