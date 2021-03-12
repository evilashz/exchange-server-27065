using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000460 RID: 1120
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionWrapperTransientException : AmDbActionTransientException
	{
		// Token: 0x06002B75 RID: 11125 RVA: 0x000BD6FE File Offset: 0x000BB8FE
		public AmDbActionWrapperTransientException(string dbActionError) : base(ReplayStrings.AmDbActionWrapperTransientException(dbActionError))
		{
			this.dbActionError = dbActionError;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000BD718 File Offset: 0x000BB918
		public AmDbActionWrapperTransientException(string dbActionError, Exception innerException) : base(ReplayStrings.AmDbActionWrapperTransientException(dbActionError), innerException)
		{
			this.dbActionError = dbActionError;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000BD733 File Offset: 0x000BB933
		protected AmDbActionWrapperTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbActionError = (string)info.GetValue("dbActionError", typeof(string));
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000BD75D File Offset: 0x000BB95D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbActionError", this.dbActionError);
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x000BD778 File Offset: 0x000BB978
		public string DbActionError
		{
			get
			{
				return this.dbActionError;
			}
		}

		// Token: 0x0400149C RID: 5276
		private readonly string dbActionError;
	}
}
