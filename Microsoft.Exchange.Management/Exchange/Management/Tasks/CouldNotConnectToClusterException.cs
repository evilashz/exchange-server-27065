using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F2B RID: 3883
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotConnectToClusterException : LocalizedException
	{
		// Token: 0x0600AACC RID: 43724 RVA: 0x0028E07D File Offset: 0x0028C27D
		public CouldNotConnectToClusterException(string machineName) : base(Strings.CouldNotConnectToCluster(machineName))
		{
			this.machineName = machineName;
		}

		// Token: 0x0600AACD RID: 43725 RVA: 0x0028E092 File Offset: 0x0028C292
		public CouldNotConnectToClusterException(string machineName, Exception innerException) : base(Strings.CouldNotConnectToCluster(machineName), innerException)
		{
			this.machineName = machineName;
		}

		// Token: 0x0600AACE RID: 43726 RVA: 0x0028E0A8 File Offset: 0x0028C2A8
		protected CouldNotConnectToClusterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineName = (string)info.GetValue("machineName", typeof(string));
		}

		// Token: 0x0600AACF RID: 43727 RVA: 0x0028E0D2 File Offset: 0x0028C2D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineName", this.machineName);
		}

		// Token: 0x1700373D RID: 14141
		// (get) Token: 0x0600AAD0 RID: 43728 RVA: 0x0028E0ED File Offset: 0x0028C2ED
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x040060A3 RID: 24739
		private readonly string machineName;
	}
}
