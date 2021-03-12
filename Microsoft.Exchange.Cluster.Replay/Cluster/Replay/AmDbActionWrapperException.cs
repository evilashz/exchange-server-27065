using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000461 RID: 1121
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionWrapperException : AmDbActionException
	{
		// Token: 0x06002B7A RID: 11130 RVA: 0x000BD780 File Offset: 0x000BB980
		public AmDbActionWrapperException(string dbActionError) : base(ReplayStrings.AmDbActionWrapperException(dbActionError))
		{
			this.dbActionError = dbActionError;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000BD79A File Offset: 0x000BB99A
		public AmDbActionWrapperException(string dbActionError, Exception innerException) : base(ReplayStrings.AmDbActionWrapperException(dbActionError), innerException)
		{
			this.dbActionError = dbActionError;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000BD7B5 File Offset: 0x000BB9B5
		protected AmDbActionWrapperException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbActionError = (string)info.GetValue("dbActionError", typeof(string));
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000BD7DF File Offset: 0x000BB9DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbActionError", this.dbActionError);
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002B7E RID: 11134 RVA: 0x000BD7FA File Offset: 0x000BB9FA
		public string DbActionError
		{
			get
			{
				return this.dbActionError;
			}
		}

		// Token: 0x0400149D RID: 5277
		private readonly string dbActionError;
	}
}
