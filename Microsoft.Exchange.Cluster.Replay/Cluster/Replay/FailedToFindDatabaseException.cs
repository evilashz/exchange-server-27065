using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000414 RID: 1044
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToFindDatabaseException : TransientException
	{
		// Token: 0x060029D3 RID: 10707 RVA: 0x000BA4C1 File Offset: 0x000B86C1
		public FailedToFindDatabaseException(string databaseName) : base(ReplayStrings.FailedToFindDatabaseException(databaseName))
		{
			this.databaseName = databaseName;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000BA4D6 File Offset: 0x000B86D6
		public FailedToFindDatabaseException(string databaseName, Exception innerException) : base(ReplayStrings.FailedToFindDatabaseException(databaseName), innerException)
		{
			this.databaseName = databaseName;
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000BA4EC File Offset: 0x000B86EC
		protected FailedToFindDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000BA516 File Offset: 0x000B8716
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x000BA531 File Offset: 0x000B8731
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x0400142A RID: 5162
		private readonly string databaseName;
	}
}
