using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200042E RID: 1070
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllInvalidForSingleCopyException : TransientException
	{
		// Token: 0x06002A6B RID: 10859 RVA: 0x000BB899 File Offset: 0x000B9A99
		public AcllInvalidForSingleCopyException(string dbCopy) : base(ReplayStrings.AcllInvalidForSingleCopyException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000BB8AE File Offset: 0x000B9AAE
		public AcllInvalidForSingleCopyException(string dbCopy, Exception innerException) : base(ReplayStrings.AcllInvalidForSingleCopyException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000BB8C4 File Offset: 0x000B9AC4
		protected AcllInvalidForSingleCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000BB8EE File Offset: 0x000B9AEE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002A6F RID: 10863 RVA: 0x000BB909 File Offset: 0x000B9B09
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x0400145A RID: 5210
		private readonly string dbCopy;
	}
}
