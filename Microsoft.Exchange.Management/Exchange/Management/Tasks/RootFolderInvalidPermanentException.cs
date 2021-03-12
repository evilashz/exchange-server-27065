using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED4 RID: 3796
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RootFolderInvalidPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8FD RID: 43261 RVA: 0x0028ADF5 File Offset: 0x00288FF5
		public RootFolderInvalidPermanentException(LocalizedString errorMessage) : base(Strings.ErrorRootFolderInvalid(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600A8FE RID: 43262 RVA: 0x0028AE0A File Offset: 0x0028900A
		public RootFolderInvalidPermanentException(LocalizedString errorMessage, Exception innerException) : base(Strings.ErrorRootFolderInvalid(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600A8FF RID: 43263 RVA: 0x0028AE20 File Offset: 0x00289020
		protected RootFolderInvalidPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (LocalizedString)info.GetValue("errorMessage", typeof(LocalizedString));
		}

		// Token: 0x0600A900 RID: 43264 RVA: 0x0028AE4A File Offset: 0x0028904A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x170036CA RID: 14026
		// (get) Token: 0x0600A901 RID: 43265 RVA: 0x0028AE6A File Offset: 0x0028906A
		public LocalizedString ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04006030 RID: 24624
		private readonly LocalizedString errorMessage;
	}
}
