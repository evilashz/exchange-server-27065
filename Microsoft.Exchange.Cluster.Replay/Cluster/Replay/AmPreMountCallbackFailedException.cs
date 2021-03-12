using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200041D RID: 1053
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmPreMountCallbackFailedException : TransientException
	{
		// Token: 0x06002A05 RID: 10757 RVA: 0x000BAAE2 File Offset: 0x000B8CE2
		public AmPreMountCallbackFailedException(string dbName, string error) : base(ReplayStrings.AmPreMountCallbackFailedException(dbName, error))
		{
			this.dbName = dbName;
			this.error = error;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000BAAFF File Offset: 0x000B8CFF
		public AmPreMountCallbackFailedException(string dbName, string error, Exception innerException) : base(ReplayStrings.AmPreMountCallbackFailedException(dbName, error), innerException)
		{
			this.dbName = dbName;
			this.error = error;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000BAB20 File Offset: 0x000B8D20
		protected AmPreMountCallbackFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000BAB75 File Offset: 0x000B8D75
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x000BABA1 File Offset: 0x000B8DA1
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000BABA9 File Offset: 0x000B8DA9
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001438 RID: 5176
		private readonly string dbName;

		// Token: 0x04001439 RID: 5177
		private readonly string error;
	}
}
