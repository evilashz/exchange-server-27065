using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000426 RID: 1062
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AcllLossDeterminationFailedException : TransientException
	{
		// Token: 0x06002A3C RID: 10812 RVA: 0x000BB293 File Offset: 0x000B9493
		public AcllLossDeterminationFailedException(string error) : base(ReplayStrings.AcllLossDeterminationFailedException(error))
		{
			this.error = error;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000BB2A8 File Offset: 0x000B94A8
		public AcllLossDeterminationFailedException(string error, Exception innerException) : base(ReplayStrings.AcllLossDeterminationFailedException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000BB2BE File Offset: 0x000B94BE
		protected AcllLossDeterminationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000BB2E8 File Offset: 0x000B94E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000BB303 File Offset: 0x000B9503
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400144B RID: 5195
		private readonly string error;
	}
}
