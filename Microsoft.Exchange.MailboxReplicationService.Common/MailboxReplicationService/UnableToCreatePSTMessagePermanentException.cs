using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000354 RID: 852
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToCreatePSTMessagePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002661 RID: 9825 RVA: 0x000530B9 File Offset: 0x000512B9
		public UnableToCreatePSTMessagePermanentException(string filePath) : base(MrsStrings.UnableToCreatePSTMessage(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000530CE File Offset: 0x000512CE
		public UnableToCreatePSTMessagePermanentException(string filePath, Exception innerException) : base(MrsStrings.UnableToCreatePSTMessage(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000530E4 File Offset: 0x000512E4
		protected UnableToCreatePSTMessagePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0005310E File Offset: 0x0005130E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x00053129 File Offset: 0x00051329
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x0400105A RID: 4186
		private readonly string filePath;
	}
}
