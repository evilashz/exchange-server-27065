using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000496 RID: 1174
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncSeedConfigNotSupportedException : TransientException
	{
		// Token: 0x06002C9E RID: 11422 RVA: 0x000BFB11 File Offset: 0x000BDD11
		public IncSeedConfigNotSupportedException(string field) : base(ReplayStrings.IncSeedConfigNotSupportedError(field))
		{
			this.field = field;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000BFB26 File Offset: 0x000BDD26
		public IncSeedConfigNotSupportedException(string field, Exception innerException) : base(ReplayStrings.IncSeedConfigNotSupportedError(field), innerException)
		{
			this.field = field;
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000BFB3C File Offset: 0x000BDD3C
		protected IncSeedConfigNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.field = (string)info.GetValue("field", typeof(string));
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000BFB66 File Offset: 0x000BDD66
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("field", this.field);
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x000BFB81 File Offset: 0x000BDD81
		public string Field
		{
			get
			{
				return this.field;
			}
		}

		// Token: 0x040014ED RID: 5357
		private readonly string field;
	}
}
