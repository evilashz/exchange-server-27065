using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E91 RID: 3729
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MaxConcurrentMigrationsExceededException : MailboxReplicationTransientException
	{
		// Token: 0x0600A7AD RID: 42925 RVA: 0x00288D58 File Offset: 0x00286F58
		public MaxConcurrentMigrationsExceededException(int currentMax) : base(Strings.ErrorMaxConcurrentMigrationsExceeded(currentMax))
		{
			this.currentMax = currentMax;
		}

		// Token: 0x0600A7AE RID: 42926 RVA: 0x00288D6D File Offset: 0x00286F6D
		public MaxConcurrentMigrationsExceededException(int currentMax, Exception innerException) : base(Strings.ErrorMaxConcurrentMigrationsExceeded(currentMax), innerException)
		{
			this.currentMax = currentMax;
		}

		// Token: 0x0600A7AF RID: 42927 RVA: 0x00288D83 File Offset: 0x00286F83
		protected MaxConcurrentMigrationsExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.currentMax = (int)info.GetValue("currentMax", typeof(int));
		}

		// Token: 0x0600A7B0 RID: 42928 RVA: 0x00288DAD File Offset: 0x00286FAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("currentMax", this.currentMax);
		}

		// Token: 0x17003686 RID: 13958
		// (get) Token: 0x0600A7B1 RID: 42929 RVA: 0x00288DC8 File Offset: 0x00286FC8
		public int CurrentMax
		{
			get
			{
				return this.currentMax;
			}
		}

		// Token: 0x04005FEC RID: 24556
		private readonly int currentMax;
	}
}
