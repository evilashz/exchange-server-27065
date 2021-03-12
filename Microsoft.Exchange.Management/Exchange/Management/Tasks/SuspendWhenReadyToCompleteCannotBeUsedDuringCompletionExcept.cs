using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EDB RID: 3803
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendWhenReadyToCompleteCannotBeUsedDuringCompletionException : CannotSetCompletingPermanentException
	{
		// Token: 0x0600A926 RID: 43302 RVA: 0x0028B335 File Offset: 0x00289535
		public SuspendWhenReadyToCompleteCannotBeUsedDuringCompletionException(string name) : base(Strings.ErrorSuspendWhenReadyToCompleteCannotBeUsedDuringCompletion(name))
		{
			this.name = name;
		}

		// Token: 0x0600A927 RID: 43303 RVA: 0x0028B34A File Offset: 0x0028954A
		public SuspendWhenReadyToCompleteCannotBeUsedDuringCompletionException(string name, Exception innerException) : base(Strings.ErrorSuspendWhenReadyToCompleteCannotBeUsedDuringCompletion(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600A928 RID: 43304 RVA: 0x0028B360 File Offset: 0x00289560
		protected SuspendWhenReadyToCompleteCannotBeUsedDuringCompletionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600A929 RID: 43305 RVA: 0x0028B38A File Offset: 0x0028958A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170036D7 RID: 14039
		// (get) Token: 0x0600A92A RID: 43306 RVA: 0x0028B3A5 File Offset: 0x002895A5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400603D RID: 24637
		private readonly string name;
	}
}
