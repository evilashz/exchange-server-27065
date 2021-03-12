using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EDE RID: 3806
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveCompletedDuringCancelPermanentException : CannotSetCompletedPermanentException
	{
		// Token: 0x0600A936 RID: 43318 RVA: 0x0028B4F1 File Offset: 0x002896F1
		public CannotRemoveCompletedDuringCancelPermanentException(string indexEntry) : base(Strings.ErrorRequestCompletedDuringCancellation(indexEntry))
		{
			this.indexEntry = indexEntry;
		}

		// Token: 0x0600A937 RID: 43319 RVA: 0x0028B506 File Offset: 0x00289706
		public CannotRemoveCompletedDuringCancelPermanentException(string indexEntry, Exception innerException) : base(Strings.ErrorRequestCompletedDuringCancellation(indexEntry), innerException)
		{
			this.indexEntry = indexEntry;
		}

		// Token: 0x0600A938 RID: 43320 RVA: 0x0028B51C File Offset: 0x0028971C
		protected CannotRemoveCompletedDuringCancelPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.indexEntry = (string)info.GetValue("indexEntry", typeof(string));
		}

		// Token: 0x0600A939 RID: 43321 RVA: 0x0028B546 File Offset: 0x00289746
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("indexEntry", this.indexEntry);
		}

		// Token: 0x170036DB RID: 14043
		// (get) Token: 0x0600A93A RID: 43322 RVA: 0x0028B561 File Offset: 0x00289761
		public string IndexEntry
		{
			get
			{
				return this.indexEntry;
			}
		}

		// Token: 0x04006041 RID: 24641
		private readonly string indexEntry;
	}
}
