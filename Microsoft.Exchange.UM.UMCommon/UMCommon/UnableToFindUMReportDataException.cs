using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E0 RID: 480
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToFindUMReportDataException : LocalizedException
	{
		// Token: 0x06000F80 RID: 3968 RVA: 0x00036875 File Offset: 0x00034A75
		public UnableToFindUMReportDataException(string mailboxOwner) : base(Strings.UnableToFindUMReportData(mailboxOwner))
		{
			this.mailboxOwner = mailboxOwner;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0003688A File Offset: 0x00034A8A
		public UnableToFindUMReportDataException(string mailboxOwner, Exception innerException) : base(Strings.UnableToFindUMReportData(mailboxOwner), innerException)
		{
			this.mailboxOwner = mailboxOwner;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000368A0 File Offset: 0x00034AA0
		protected UnableToFindUMReportDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxOwner = (string)info.GetValue("mailboxOwner", typeof(string));
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000368CA File Offset: 0x00034ACA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxOwner", this.mailboxOwner);
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000368E5 File Offset: 0x00034AE5
		public string MailboxOwner
		{
			get
			{
				return this.mailboxOwner;
			}
		}

		// Token: 0x040007B0 RID: 1968
		private readonly string mailboxOwner;
	}
}
