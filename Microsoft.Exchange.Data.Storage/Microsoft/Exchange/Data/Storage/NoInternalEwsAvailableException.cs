using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F7 RID: 247
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoInternalEwsAvailableException : StoragePermanentException
	{
		// Token: 0x06001363 RID: 4963 RVA: 0x0006943C File Offset: 0x0006763C
		public NoInternalEwsAvailableException(string mailbox) : base(ServerStrings.NoInternalEwsAvailableException(mailbox))
		{
			this.mailbox = mailbox;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00069451 File Offset: 0x00067651
		public NoInternalEwsAvailableException(string mailbox, Exception innerException) : base(ServerStrings.NoInternalEwsAvailableException(mailbox), innerException)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00069467 File Offset: 0x00067667
		protected NoInternalEwsAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00069491 File Offset: 0x00067691
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x000694AC File Offset: 0x000676AC
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x0400098E RID: 2446
		private readonly string mailbox;
	}
}
