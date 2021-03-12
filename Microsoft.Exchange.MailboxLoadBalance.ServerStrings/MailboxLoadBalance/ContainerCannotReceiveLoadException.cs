using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ContainerCannotReceiveLoadException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002943 File Offset: 0x00000B43
		public ContainerCannotReceiveLoadException(string containerGuid) : base(MigrationWorkflowServiceStrings.ErrorContainerCannotTakeLoad(containerGuid))
		{
			this.containerGuid = containerGuid;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002958 File Offset: 0x00000B58
		public ContainerCannotReceiveLoadException(string containerGuid, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorContainerCannotTakeLoad(containerGuid), innerException)
		{
			this.containerGuid = containerGuid;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000296E File Offset: 0x00000B6E
		protected ContainerCannotReceiveLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.containerGuid = (string)info.GetValue("containerGuid", typeof(string));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002998 File Offset: 0x00000B98
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("containerGuid", this.containerGuid);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000029B3 File Offset: 0x00000BB3
		public string ContainerGuid
		{
			get
			{
				return this.containerGuid;
			}
		}

		// Token: 0x04000025 RID: 37
		private readonly string containerGuid;
	}
}
