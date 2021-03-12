using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200034E RID: 846
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToGetPSTPropsTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600263C RID: 9788 RVA: 0x00052B9F File Offset: 0x00050D9F
		public UnableToGetPSTPropsTransientException(string filePath) : base(MrsStrings.UnableToGetPSTProps(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00052BB4 File Offset: 0x00050DB4
		public UnableToGetPSTPropsTransientException(string filePath, Exception innerException) : base(MrsStrings.UnableToGetPSTProps(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x00052BCA File Offset: 0x00050DCA
		protected UnableToGetPSTPropsTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00052BF4 File Offset: 0x00050DF4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x00052C0F File Offset: 0x00050E0F
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x0400104D RID: 4173
		private readonly string filePath;
	}
}
