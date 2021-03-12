using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200034C RID: 844
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToGetPSTHierarchyTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002632 RID: 9778 RVA: 0x00052AAF File Offset: 0x00050CAF
		public UnableToGetPSTHierarchyTransientException(string filePath) : base(MrsStrings.UnableToGetPSTHierarchy(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00052AC4 File Offset: 0x00050CC4
		public UnableToGetPSTHierarchyTransientException(string filePath, Exception innerException) : base(MrsStrings.UnableToGetPSTHierarchy(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00052ADA File Offset: 0x00050CDA
		protected UnableToGetPSTHierarchyTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00052B04 File Offset: 0x00050D04
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x00052B1F File Offset: 0x00050D1F
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x0400104B RID: 4171
		private readonly string filePath;
	}
}
