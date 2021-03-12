using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E92 RID: 3730
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendWhenReadyToCompleteNotSupportedException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7B2 RID: 42930 RVA: 0x00288DD0 File Offset: 0x00286FD0
		public SuspendWhenReadyToCompleteNotSupportedException(string name) : base(Strings.ErrorSuspendWhenReadyToCompleteNotSupported(name))
		{
			this.name = name;
		}

		// Token: 0x0600A7B3 RID: 42931 RVA: 0x00288DE5 File Offset: 0x00286FE5
		public SuspendWhenReadyToCompleteNotSupportedException(string name, Exception innerException) : base(Strings.ErrorSuspendWhenReadyToCompleteNotSupported(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600A7B4 RID: 42932 RVA: 0x00288DFB File Offset: 0x00286FFB
		protected SuspendWhenReadyToCompleteNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600A7B5 RID: 42933 RVA: 0x00288E25 File Offset: 0x00287025
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003687 RID: 13959
		// (get) Token: 0x0600A7B6 RID: 42934 RVA: 0x00288E40 File Offset: 0x00287040
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04005FED RID: 24557
		private readonly string name;
	}
}
