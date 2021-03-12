using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A3 RID: 675
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnexpectedErrorPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022EB RID: 8939 RVA: 0x0004DA17 File Offset: 0x0004BC17
		public UnexpectedErrorPermanentException(int hr) : base(MrsStrings.UnexpectedError(hr))
		{
			this.hr = hr;
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0004DA2C File Offset: 0x0004BC2C
		public UnexpectedErrorPermanentException(int hr, Exception innerException) : base(MrsStrings.UnexpectedError(hr), innerException)
		{
			this.hr = hr;
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0004DA42 File Offset: 0x0004BC42
		protected UnexpectedErrorPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.hr = (int)info.GetValue("hr", typeof(int));
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x0004DA6C File Offset: 0x0004BC6C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hr", this.hr);
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0004DA87 File Offset: 0x0004BC87
		public int Hr
		{
			get
			{
				return this.hr;
			}
		}

		// Token: 0x04000FA8 RID: 4008
		private readonly int hr;
	}
}
