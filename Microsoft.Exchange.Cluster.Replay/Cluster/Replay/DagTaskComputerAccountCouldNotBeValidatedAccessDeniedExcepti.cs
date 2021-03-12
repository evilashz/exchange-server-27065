using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003DA RID: 986
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException : DagTaskServerException
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x000B856F File Offset: 0x000B676F
		public DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException(string computerAccount, string userAccount) : base(ReplayStrings.DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException(computerAccount, userAccount))
		{
			this.computerAccount = computerAccount;
			this.userAccount = userAccount;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x000B8591 File Offset: 0x000B6791
		public DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException(string computerAccount, string userAccount, Exception innerException) : base(ReplayStrings.DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException(computerAccount, userAccount), innerException)
		{
			this.computerAccount = computerAccount;
			this.userAccount = userAccount;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000B85B4 File Offset: 0x000B67B4
		protected DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.computerAccount = (string)info.GetValue("computerAccount", typeof(string));
			this.userAccount = (string)info.GetValue("userAccount", typeof(string));
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000B8609 File Offset: 0x000B6809
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("computerAccount", this.computerAccount);
			info.AddValue("userAccount", this.userAccount);
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000B8635 File Offset: 0x000B6835
		public string ComputerAccount
		{
			get
			{
				return this.computerAccount;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000B863D File Offset: 0x000B683D
		public string UserAccount
		{
			get
			{
				return this.userAccount;
			}
		}

		// Token: 0x040013EC RID: 5100
		private readonly string computerAccount;

		// Token: 0x040013ED RID: 5101
		private readonly string userAccount;
	}
}
