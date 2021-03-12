using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E93 RID: 3731
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncrementalMergesRequireSuspendWhenReadyToCompleteException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7B7 RID: 42935 RVA: 0x00288E48 File Offset: 0x00287048
		public IncrementalMergesRequireSuspendWhenReadyToCompleteException(string name) : base(Strings.ErrorIncrementalMergesRequireSuspendWhenReadyToComplete(name))
		{
			this.name = name;
		}

		// Token: 0x0600A7B8 RID: 42936 RVA: 0x00288E5D File Offset: 0x0028705D
		public IncrementalMergesRequireSuspendWhenReadyToCompleteException(string name, Exception innerException) : base(Strings.ErrorIncrementalMergesRequireSuspendWhenReadyToComplete(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600A7B9 RID: 42937 RVA: 0x00288E73 File Offset: 0x00287073
		protected IncrementalMergesRequireSuspendWhenReadyToCompleteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600A7BA RID: 42938 RVA: 0x00288E9D File Offset: 0x0028709D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003688 RID: 13960
		// (get) Token: 0x0600A7BB RID: 42939 RVA: 0x00288EB8 File Offset: 0x002870B8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04005FEE RID: 24558
		private readonly string name;
	}
}
