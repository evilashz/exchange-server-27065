using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000369 RID: 873
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FastTransferBufferException : MailboxReplicationPermanentException
	{
		// Token: 0x060026CA RID: 9930 RVA: 0x00053ABA File Offset: 0x00051CBA
		public FastTransferBufferException(string property, int value) : base(MrsStrings.FastTransferBuffer(property, value))
		{
			this.property = property;
			this.value = value;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00053AD7 File Offset: 0x00051CD7
		public FastTransferBufferException(string property, int value, Exception innerException) : base(MrsStrings.FastTransferBuffer(property, value), innerException)
		{
			this.property = property;
			this.value = value;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x00053AF8 File Offset: 0x00051CF8
		protected FastTransferBufferException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
			this.value = (int)info.GetValue("value", typeof(int));
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x00053B4D File Offset: 0x00051D4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
			info.AddValue("value", this.value);
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x00053B79 File Offset: 0x00051D79
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x00053B81 File Offset: 0x00051D81
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400106F RID: 4207
		private readonly string property;

		// Token: 0x04001070 RID: 4208
		private readonly int value;
	}
}
