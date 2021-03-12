using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000021 RID: 33
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotRetrieveCapacityDataException : MailboxLoadBalancePermanentException
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000031F7 File Offset: 0x000013F7
		public CannotRetrieveCapacityDataException(string objectIdentity) : base(MigrationWorkflowServiceStrings.ErrorCannotRetrieveCapacityData(objectIdentity))
		{
			this.objectIdentity = objectIdentity;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000320C File Offset: 0x0000140C
		public CannotRetrieveCapacityDataException(string objectIdentity, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorCannotRetrieveCapacityData(objectIdentity), innerException)
		{
			this.objectIdentity = objectIdentity;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003222 File Offset: 0x00001422
		protected CannotRetrieveCapacityDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectIdentity = (string)info.GetValue("objectIdentity", typeof(string));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000324C File Offset: 0x0000144C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectIdentity", this.objectIdentity);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003267 File Offset: 0x00001467
		public string ObjectIdentity
		{
			get
			{
				return this.objectIdentity;
			}
		}

		// Token: 0x04000038 RID: 56
		private readonly string objectIdentity;
	}
}
