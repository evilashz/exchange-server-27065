using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF6 RID: 3830
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnexpectedRemoveStoreMailboxStatePermanentException : LocalizedException
	{
		// Token: 0x0600A9B6 RID: 43446 RVA: 0x0028C316 File Offset: 0x0028A516
		public UnexpectedRemoveStoreMailboxStatePermanentException(string identity, string state, string argument) : base(Strings.ErrorUnexpectedRemoveStoreMailboxState(identity, state, argument))
		{
			this.identity = identity;
			this.state = state;
			this.argument = argument;
		}

		// Token: 0x0600A9B7 RID: 43447 RVA: 0x0028C33B File Offset: 0x0028A53B
		public UnexpectedRemoveStoreMailboxStatePermanentException(string identity, string state, string argument, Exception innerException) : base(Strings.ErrorUnexpectedRemoveStoreMailboxState(identity, state, argument), innerException)
		{
			this.identity = identity;
			this.state = state;
			this.argument = argument;
		}

		// Token: 0x0600A9B8 RID: 43448 RVA: 0x0028C364 File Offset: 0x0028A564
		protected UnexpectedRemoveStoreMailboxStatePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.state = (string)info.GetValue("state", typeof(string));
			this.argument = (string)info.GetValue("argument", typeof(string));
		}

		// Token: 0x0600A9B9 RID: 43449 RVA: 0x0028C3D9 File Offset: 0x0028A5D9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("state", this.state);
			info.AddValue("argument", this.argument);
		}

		// Token: 0x170036FB RID: 14075
		// (get) Token: 0x0600A9BA RID: 43450 RVA: 0x0028C416 File Offset: 0x0028A616
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170036FC RID: 14076
		// (get) Token: 0x0600A9BB RID: 43451 RVA: 0x0028C41E File Offset: 0x0028A61E
		public string State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170036FD RID: 14077
		// (get) Token: 0x0600A9BC RID: 43452 RVA: 0x0028C426 File Offset: 0x0028A626
		public string Argument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x04006061 RID: 24673
		private readonly string identity;

		// Token: 0x04006062 RID: 24674
		private readonly string state;

		// Token: 0x04006063 RID: 24675
		private readonly string argument;
	}
}
