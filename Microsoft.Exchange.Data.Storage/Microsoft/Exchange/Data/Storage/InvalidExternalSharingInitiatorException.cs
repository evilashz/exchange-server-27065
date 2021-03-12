using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F5 RID: 245
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidExternalSharingInitiatorException : StoragePermanentException
	{
		// Token: 0x06001359 RID: 4953 RVA: 0x0006933F File Offset: 0x0006753F
		public InvalidExternalSharingInitiatorException(string initiator, string sender) : base(ServerStrings.InvalidExternalSharingInitiatorException(initiator, sender))
		{
			this.initiator = initiator;
			this.sender = sender;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0006935C File Offset: 0x0006755C
		public InvalidExternalSharingInitiatorException(string initiator, string sender, Exception innerException) : base(ServerStrings.InvalidExternalSharingInitiatorException(initiator, sender), innerException)
		{
			this.initiator = initiator;
			this.sender = sender;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0006937C File Offset: 0x0006757C
		protected InvalidExternalSharingInitiatorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.initiator = (string)info.GetValue("initiator", typeof(string));
			this.sender = (string)info.GetValue("sender", typeof(string));
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000693D1 File Offset: 0x000675D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("initiator", this.initiator);
			info.AddValue("sender", this.sender);
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x000693FD File Offset: 0x000675FD
		public string Initiator
		{
			get
			{
				return this.initiator;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00069405 File Offset: 0x00067605
		public string Sender
		{
			get
			{
				return this.sender;
			}
		}

		// Token: 0x0400098C RID: 2444
		private readonly string initiator;

		// Token: 0x0400098D RID: 2445
		private readonly string sender;
	}
}
