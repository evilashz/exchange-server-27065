using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200034B RID: 843
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToGetPSTHierarchyPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600262D RID: 9773 RVA: 0x00052A37 File Offset: 0x00050C37
		public UnableToGetPSTHierarchyPermanentException(string filePath) : base(MrsStrings.UnableToGetPSTHierarchy(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00052A4C File Offset: 0x00050C4C
		public UnableToGetPSTHierarchyPermanentException(string filePath, Exception innerException) : base(MrsStrings.UnableToGetPSTHierarchy(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00052A62 File Offset: 0x00050C62
		protected UnableToGetPSTHierarchyPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00052A8C File Offset: 0x00050C8C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x00052AA7 File Offset: 0x00050CA7
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x0400104A RID: 4170
		private readonly string filePath;
	}
}
