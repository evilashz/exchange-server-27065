using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED6 RID: 3798
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderFilterInvalidPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A907 RID: 43271 RVA: 0x0028AEEF File Offset: 0x002890EF
		public FolderFilterInvalidPermanentException(LocalizedString errorMessage) : base(Strings.ErrorFolderFilterInvalid(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600A908 RID: 43272 RVA: 0x0028AF04 File Offset: 0x00289104
		public FolderFilterInvalidPermanentException(LocalizedString errorMessage, Exception innerException) : base(Strings.ErrorFolderFilterInvalid(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600A909 RID: 43273 RVA: 0x0028AF1A File Offset: 0x0028911A
		protected FolderFilterInvalidPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (LocalizedString)info.GetValue("errorMessage", typeof(LocalizedString));
		}

		// Token: 0x0600A90A RID: 43274 RVA: 0x0028AF44 File Offset: 0x00289144
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x170036CC RID: 14028
		// (get) Token: 0x0600A90B RID: 43275 RVA: 0x0028AF64 File Offset: 0x00289164
		public LocalizedString ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04006032 RID: 24626
		private readonly LocalizedString errorMessage;
	}
}
