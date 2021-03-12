using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000444 RID: 1092
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederCatalogNotHealthyException : SeederServerException
	{
		// Token: 0x06002AE2 RID: 10978 RVA: 0x000BC672 File Offset: 0x000BA872
		public SeederCatalogNotHealthyException(string reason) : base(ReplayStrings.SeederCatalogNotHealthyErr(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000BC68C File Offset: 0x000BA88C
		public SeederCatalogNotHealthyException(string reason, Exception innerException) : base(ReplayStrings.SeederCatalogNotHealthyErr(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000BC6A7 File Offset: 0x000BA8A7
		protected SeederCatalogNotHealthyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000BC6D1 File Offset: 0x000BA8D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x000BC6EC File Offset: 0x000BA8EC
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04001479 RID: 5241
		private readonly string reason;
	}
}
