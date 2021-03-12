using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001133 RID: 4403
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotUpdateServiceInstanceMapException : LocalizedException
	{
		// Token: 0x0600B4E9 RID: 46313 RVA: 0x0029D74A File Offset: 0x0029B94A
		public CouldNotUpdateServiceInstanceMapException(string reason) : base(Strings.CouldNotUpdateServiceInstanceMap(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600B4EA RID: 46314 RVA: 0x0029D75F File Offset: 0x0029B95F
		public CouldNotUpdateServiceInstanceMapException(string reason, Exception innerException) : base(Strings.CouldNotUpdateServiceInstanceMap(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600B4EB RID: 46315 RVA: 0x0029D775 File Offset: 0x0029B975
		protected CouldNotUpdateServiceInstanceMapException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600B4EC RID: 46316 RVA: 0x0029D79F File Offset: 0x0029B99F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x1700393A RID: 14650
		// (get) Token: 0x0600B4ED RID: 46317 RVA: 0x0029D7BA File Offset: 0x0029B9BA
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040062A0 RID: 25248
		private readonly string reason;
	}
}
