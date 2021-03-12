using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000EC RID: 236
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxSearchEwsFailedException : StoragePermanentException
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x00069068 File Offset: 0x00067268
		public MailboxSearchEwsFailedException(string error) : base(ServerStrings.MailboxSearchEwsFailedExceptionWithError(error))
		{
			this.error = error;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0006907D File Offset: 0x0006727D
		public MailboxSearchEwsFailedException(string error, Exception innerException) : base(ServerStrings.MailboxSearchEwsFailedExceptionWithError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00069093 File Offset: 0x00067293
		protected MailboxSearchEwsFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000690BD File Offset: 0x000672BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x000690D8 File Offset: 0x000672D8
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000988 RID: 2440
		private readonly string error;
	}
}
