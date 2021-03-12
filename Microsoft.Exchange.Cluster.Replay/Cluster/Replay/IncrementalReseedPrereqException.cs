using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A0 RID: 1184
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncrementalReseedPrereqException : TransientException
	{
		// Token: 0x06002CD7 RID: 11479 RVA: 0x000C0249 File Offset: 0x000BE449
		public IncrementalReseedPrereqException(string error) : base(ReplayStrings.IncrementalReseedPrereqException(error))
		{
			this.error = error;
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000C025E File Offset: 0x000BE45E
		public IncrementalReseedPrereqException(string error, Exception innerException) : base(ReplayStrings.IncrementalReseedPrereqException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000C0274 File Offset: 0x000BE474
		protected IncrementalReseedPrereqException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000C029E File Offset: 0x000BE49E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x000C02B9 File Offset: 0x000BE4B9
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040014FE RID: 5374
		private readonly string error;
	}
}
