using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200040F RID: 1039
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceRestartInvalidSeedingException : TaskServerException
	{
		// Token: 0x060029BA RID: 10682 RVA: 0x000BA241 File Offset: 0x000B8441
		public ReplayServiceRestartInvalidSeedingException(string dbCopy) : base(ReplayStrings.ReplayServiceRestartInvalidSeedingException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000BA25B File Offset: 0x000B845B
		public ReplayServiceRestartInvalidSeedingException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceRestartInvalidSeedingException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000BA276 File Offset: 0x000B8476
		protected ReplayServiceRestartInvalidSeedingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000BA2A0 File Offset: 0x000B84A0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x000BA2BB File Offset: 0x000B84BB
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001425 RID: 5157
		private readonly string dbCopy;
	}
}
