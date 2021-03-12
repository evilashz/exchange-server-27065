using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000DA RID: 218
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServerDagNotFound : AmServerException
	{
		// Token: 0x060012BF RID: 4799 RVA: 0x000682AB File Offset: 0x000664AB
		public AmServerDagNotFound(string serverName) : base(ServerStrings.AmServerDagNotFound(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x000682C5 File Offset: 0x000664C5
		public AmServerDagNotFound(string serverName, Exception innerException) : base(ServerStrings.AmServerDagNotFound(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000682E0 File Offset: 0x000664E0
		protected AmServerDagNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0006830A File Offset: 0x0006650A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00068325 File Offset: 0x00066525
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000969 RID: 2409
		private readonly string serverName;
	}
}
