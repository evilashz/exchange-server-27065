using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200051D RID: 1309
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SafetyNetVersionCheckException : DumpsterRedeliveryException
	{
		// Token: 0x06002FA8 RID: 12200 RVA: 0x000C5F37 File Offset: 0x000C4137
		public SafetyNetVersionCheckException(string error) : base(ReplayStrings.SafetyNetVersionCheckException(error))
		{
			this.error = error;
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000C5F51 File Offset: 0x000C4151
		public SafetyNetVersionCheckException(string error, Exception innerException) : base(ReplayStrings.SafetyNetVersionCheckException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000C5F6C File Offset: 0x000C416C
		protected SafetyNetVersionCheckException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x000C5F96 File Offset: 0x000C4196
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000C5FB1 File Offset: 0x000C41B1
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040015DB RID: 5595
		private readonly string error;
	}
}
