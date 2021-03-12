using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ECC RID: 3788
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotModifyCompletingRequestPermanentException : CannotSetCompletingPermanentException
	{
		// Token: 0x0600A8D5 RID: 43221 RVA: 0x0028AA19 File Offset: 0x00288C19
		public CannotModifyCompletingRequestPermanentException(string name) : base(Strings.ErrorCannotModifyRequestAlreadyCompleting(name))
		{
			this.name = name;
		}

		// Token: 0x0600A8D6 RID: 43222 RVA: 0x0028AA2E File Offset: 0x00288C2E
		public CannotModifyCompletingRequestPermanentException(string name, Exception innerException) : base(Strings.ErrorCannotModifyRequestAlreadyCompleting(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600A8D7 RID: 43223 RVA: 0x0028AA44 File Offset: 0x00288C44
		protected CannotModifyCompletingRequestPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600A8D8 RID: 43224 RVA: 0x0028AA6E File Offset: 0x00288C6E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170036C2 RID: 14018
		// (get) Token: 0x0600A8D9 RID: 43225 RVA: 0x0028AA89 File Offset: 0x00288C89
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006028 RID: 24616
		private readonly string name;
	}
}
