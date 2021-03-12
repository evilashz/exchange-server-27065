using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F9 RID: 249
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotShareFolderException : StoragePermanentException
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x000694E3 File Offset: 0x000676E3
		public CannotShareFolderException(string reason) : base(ServerStrings.CannotShareFolderException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000694F8 File Offset: 0x000676F8
		public CannotShareFolderException(string reason, Exception innerException) : base(ServerStrings.CannotShareFolderException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0006950E File Offset: 0x0006770E
		protected CannotShareFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00069538 File Offset: 0x00067738
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00069553 File Offset: 0x00067753
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400098F RID: 2447
		private readonly string reason;
	}
}
