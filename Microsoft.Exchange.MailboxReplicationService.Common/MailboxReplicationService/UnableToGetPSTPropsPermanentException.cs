using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200034D RID: 845
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToGetPSTPropsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002637 RID: 9783 RVA: 0x00052B27 File Offset: 0x00050D27
		public UnableToGetPSTPropsPermanentException(string filePath) : base(MrsStrings.UnableToGetPSTProps(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00052B3C File Offset: 0x00050D3C
		public UnableToGetPSTPropsPermanentException(string filePath, Exception innerException) : base(MrsStrings.UnableToGetPSTProps(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00052B52 File Offset: 0x00050D52
		protected UnableToGetPSTPropsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x00052B7C File Offset: 0x00050D7C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00052B97 File Offset: 0x00050D97
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x0400104C RID: 4172
		private readonly string filePath;
	}
}
