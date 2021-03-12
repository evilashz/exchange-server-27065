using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000450 RID: 1104
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederSuspendFailedException : SeedInProgressException
	{
		// Token: 0x06002B27 RID: 11047 RVA: 0x000BCF95 File Offset: 0x000BB195
		public SeederSuspendFailedException(string specificError) : base(ReplayStrings.SeederSuspendFailedException(specificError))
		{
			this.specificError = specificError;
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000BCFAF File Offset: 0x000BB1AF
		public SeederSuspendFailedException(string specificError, Exception innerException) : base(ReplayStrings.SeederSuspendFailedException(specificError), innerException)
		{
			this.specificError = specificError;
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000BCFCA File Offset: 0x000BB1CA
		protected SeederSuspendFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.specificError = (string)info.GetValue("specificError", typeof(string));
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000BCFF4 File Offset: 0x000BB1F4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("specificError", this.specificError);
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000BD00F File Offset: 0x000BB20F
		public string SpecificError
		{
			get
			{
				return this.specificError;
			}
		}

		// Token: 0x0400148E RID: 5262
		private readonly string specificError;
	}
}
