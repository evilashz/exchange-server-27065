using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001077 RID: 4215
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoveDagNeedsZeroServersException : LocalizedException
	{
		// Token: 0x0600B138 RID: 45368 RVA: 0x00297AD6 File Offset: 0x00295CD6
		public RemoveDagNeedsZeroServersException(int serverCount) : base(Strings.RemoveDagNeedsZeroServersException(serverCount))
		{
			this.serverCount = serverCount;
		}

		// Token: 0x0600B139 RID: 45369 RVA: 0x00297AEB File Offset: 0x00295CEB
		public RemoveDagNeedsZeroServersException(int serverCount, Exception innerException) : base(Strings.RemoveDagNeedsZeroServersException(serverCount), innerException)
		{
			this.serverCount = serverCount;
		}

		// Token: 0x0600B13A RID: 45370 RVA: 0x00297B01 File Offset: 0x00295D01
		protected RemoveDagNeedsZeroServersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverCount = (int)info.GetValue("serverCount", typeof(int));
		}

		// Token: 0x0600B13B RID: 45371 RVA: 0x00297B2B File Offset: 0x00295D2B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverCount", this.serverCount);
		}

		// Token: 0x17003879 RID: 14457
		// (get) Token: 0x0600B13C RID: 45372 RVA: 0x00297B46 File Offset: 0x00295D46
		public int ServerCount
		{
			get
			{
				return this.serverCount;
			}
		}

		// Token: 0x040061DF RID: 25055
		private readonly int serverCount;
	}
}
