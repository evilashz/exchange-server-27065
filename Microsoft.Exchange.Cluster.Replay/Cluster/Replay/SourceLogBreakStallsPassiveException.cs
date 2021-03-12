using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003BF RID: 959
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceLogBreakStallsPassiveException : TransientException
	{
		// Token: 0x06002815 RID: 10261 RVA: 0x000B7265 File Offset: 0x000B5465
		public SourceLogBreakStallsPassiveException(string sourceServerName, string error) : base(ReplayStrings.SourceLogBreakStallsPassiveError(sourceServerName, error))
		{
			this.sourceServerName = sourceServerName;
			this.error = error;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000B7282 File Offset: 0x000B5482
		public SourceLogBreakStallsPassiveException(string sourceServerName, string error, Exception innerException) : base(ReplayStrings.SourceLogBreakStallsPassiveError(sourceServerName, error), innerException)
		{
			this.sourceServerName = sourceServerName;
			this.error = error;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000B72A0 File Offset: 0x000B54A0
		protected SourceLogBreakStallsPassiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceServerName = (string)info.GetValue("sourceServerName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000B72F5 File Offset: 0x000B54F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceServerName", this.sourceServerName);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x000B7321 File Offset: 0x000B5521
		public string SourceServerName
		{
			get
			{
				return this.sourceServerName;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x000B7329 File Offset: 0x000B5529
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040013C0 RID: 5056
		private readonly string sourceServerName;

		// Token: 0x040013C1 RID: 5057
		private readonly string error;
	}
}
