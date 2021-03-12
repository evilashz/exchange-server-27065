using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED5 RID: 3797
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ContentFilterInvalidPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A902 RID: 43266 RVA: 0x0028AE72 File Offset: 0x00289072
		public ContentFilterInvalidPermanentException(LocalizedString errorMessage) : base(Strings.ErrorContentFilterInvalid(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600A903 RID: 43267 RVA: 0x0028AE87 File Offset: 0x00289087
		public ContentFilterInvalidPermanentException(LocalizedString errorMessage, Exception innerException) : base(Strings.ErrorContentFilterInvalid(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600A904 RID: 43268 RVA: 0x0028AE9D File Offset: 0x0028909D
		protected ContentFilterInvalidPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (LocalizedString)info.GetValue("errorMessage", typeof(LocalizedString));
		}

		// Token: 0x0600A905 RID: 43269 RVA: 0x0028AEC7 File Offset: 0x002890C7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x170036CB RID: 14027
		// (get) Token: 0x0600A906 RID: 43270 RVA: 0x0028AEE7 File Offset: 0x002890E7
		public LocalizedString ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04006031 RID: 24625
		private readonly LocalizedString errorMessage;
	}
}
