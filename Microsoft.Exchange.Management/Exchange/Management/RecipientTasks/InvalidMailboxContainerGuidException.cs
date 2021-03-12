using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000EAB RID: 3755
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidMailboxContainerGuidException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A82A RID: 43050 RVA: 0x00289879 File Offset: 0x00287A79
		public InvalidMailboxContainerGuidException(string specifiedMailboxContainerGuid, string linkedMailboxContainerGuid) : base(Strings.InvalidMailboxContainerGuid(specifiedMailboxContainerGuid, linkedMailboxContainerGuid))
		{
			this.specifiedMailboxContainerGuid = specifiedMailboxContainerGuid;
			this.linkedMailboxContainerGuid = linkedMailboxContainerGuid;
		}

		// Token: 0x0600A82B RID: 43051 RVA: 0x00289896 File Offset: 0x00287A96
		public InvalidMailboxContainerGuidException(string specifiedMailboxContainerGuid, string linkedMailboxContainerGuid, Exception innerException) : base(Strings.InvalidMailboxContainerGuid(specifiedMailboxContainerGuid, linkedMailboxContainerGuid), innerException)
		{
			this.specifiedMailboxContainerGuid = specifiedMailboxContainerGuid;
			this.linkedMailboxContainerGuid = linkedMailboxContainerGuid;
		}

		// Token: 0x0600A82C RID: 43052 RVA: 0x002898B4 File Offset: 0x00287AB4
		protected InvalidMailboxContainerGuidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.specifiedMailboxContainerGuid = (string)info.GetValue("specifiedMailboxContainerGuid", typeof(string));
			this.linkedMailboxContainerGuid = (string)info.GetValue("linkedMailboxContainerGuid", typeof(string));
		}

		// Token: 0x0600A82D RID: 43053 RVA: 0x00289909 File Offset: 0x00287B09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("specifiedMailboxContainerGuid", this.specifiedMailboxContainerGuid);
			info.AddValue("linkedMailboxContainerGuid", this.linkedMailboxContainerGuid);
		}

		// Token: 0x1700369B RID: 13979
		// (get) Token: 0x0600A82E RID: 43054 RVA: 0x00289935 File Offset: 0x00287B35
		public string SpecifiedMailboxContainerGuid
		{
			get
			{
				return this.specifiedMailboxContainerGuid;
			}
		}

		// Token: 0x1700369C RID: 13980
		// (get) Token: 0x0600A82F RID: 43055 RVA: 0x0028993D File Offset: 0x00287B3D
		public string LinkedMailboxContainerGuid
		{
			get
			{
				return this.linkedMailboxContainerGuid;
			}
		}

		// Token: 0x04006001 RID: 24577
		private readonly string specifiedMailboxContainerGuid;

		// Token: 0x04006002 RID: 24578
		private readonly string linkedMailboxContainerGuid;
	}
}
