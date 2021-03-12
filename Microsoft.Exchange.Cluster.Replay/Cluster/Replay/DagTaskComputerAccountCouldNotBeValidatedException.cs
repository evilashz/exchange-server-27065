using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003DB RID: 987
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskComputerAccountCouldNotBeValidatedException : DagTaskServerException
	{
		// Token: 0x060028B3 RID: 10419 RVA: 0x000B8645 File Offset: 0x000B6845
		public DagTaskComputerAccountCouldNotBeValidatedException(string computerAccount, string userAccount, string error) : base(ReplayStrings.DagTaskComputerAccountCouldNotBeValidatedException(computerAccount, userAccount, error))
		{
			this.computerAccount = computerAccount;
			this.userAccount = userAccount;
			this.error = error;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000B866F File Offset: 0x000B686F
		public DagTaskComputerAccountCouldNotBeValidatedException(string computerAccount, string userAccount, string error, Exception innerException) : base(ReplayStrings.DagTaskComputerAccountCouldNotBeValidatedException(computerAccount, userAccount, error), innerException)
		{
			this.computerAccount = computerAccount;
			this.userAccount = userAccount;
			this.error = error;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000B869C File Offset: 0x000B689C
		protected DagTaskComputerAccountCouldNotBeValidatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.computerAccount = (string)info.GetValue("computerAccount", typeof(string));
			this.userAccount = (string)info.GetValue("userAccount", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000B8711 File Offset: 0x000B6911
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("computerAccount", this.computerAccount);
			info.AddValue("userAccount", this.userAccount);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x000B874E File Offset: 0x000B694E
		public string ComputerAccount
		{
			get
			{
				return this.computerAccount;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x000B8756 File Offset: 0x000B6956
		public string UserAccount
		{
			get
			{
				return this.userAccount;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060028B9 RID: 10425 RVA: 0x000B875E File Offset: 0x000B695E
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040013EE RID: 5102
		private readonly string computerAccount;

		// Token: 0x040013EF RID: 5103
		private readonly string userAccount;

		// Token: 0x040013F0 RID: 5104
		private readonly string error;
	}
}
