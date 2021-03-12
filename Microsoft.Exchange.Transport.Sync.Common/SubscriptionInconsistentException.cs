using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SubscriptionInconsistentException : LocalizedException
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00004DBD File Offset: 0x00002FBD
		public SubscriptionInconsistentException(string name) : base(Strings.SubscriptionInconsistent(name))
		{
			this.name = name;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004DD2 File Offset: 0x00002FD2
		public SubscriptionInconsistentException(string name, Exception innerException) : base(Strings.SubscriptionInconsistent(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004DE8 File Offset: 0x00002FE8
		protected SubscriptionInconsistentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004E12 File Offset: 0x00003012
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004E2D File Offset: 0x0000302D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040000D6 RID: 214
		private readonly string name;
	}
}
