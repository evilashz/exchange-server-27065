using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E6 RID: 998
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskFswNeedsCnoPermissionException : DagTaskServerException
	{
		// Token: 0x060028EC RID: 10476 RVA: 0x000B8C86 File Offset: 0x000B6E86
		public DagTaskFswNeedsCnoPermissionException(string fswPath, string accountName) : base(ReplayStrings.DagTaskFswNeedsCnoPermissionException(fswPath, accountName))
		{
			this.fswPath = fswPath;
			this.accountName = accountName;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x000B8CA8 File Offset: 0x000B6EA8
		public DagTaskFswNeedsCnoPermissionException(string fswPath, string accountName, Exception innerException) : base(ReplayStrings.DagTaskFswNeedsCnoPermissionException(fswPath, accountName), innerException)
		{
			this.fswPath = fswPath;
			this.accountName = accountName;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000B8CCC File Offset: 0x000B6ECC
		protected DagTaskFswNeedsCnoPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswPath = (string)info.GetValue("fswPath", typeof(string));
			this.accountName = (string)info.GetValue("accountName", typeof(string));
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000B8D21 File Offset: 0x000B6F21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswPath", this.fswPath);
			info.AddValue("accountName", this.accountName);
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x000B8D4D File Offset: 0x000B6F4D
		public string FswPath
		{
			get
			{
				return this.fswPath;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x000B8D55 File Offset: 0x000B6F55
		public string AccountName
		{
			get
			{
				return this.accountName;
			}
		}

		// Token: 0x040013FB RID: 5115
		private readonly string fswPath;

		// Token: 0x040013FC RID: 5116
		private readonly string accountName;
	}
}
