using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000172 RID: 370
	[ClassAccessLevel(AccessLevel.Consumer)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADInitializationException : TaskTransientException
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003FBF4 File Offset: 0x0003DDF4
		public ADInitializationException(LocalizedString reason) : base(Strings.ADInitializationException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0003FC0E File Offset: 0x0003DE0E
		public ADInitializationException(LocalizedString reason, Exception innerException) : base(Strings.ADInitializationException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0003FC29 File Offset: 0x0003DE29
		protected ADInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (LocalizedString)info.GetValue("reason", typeof(LocalizedString));
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0003FC53 File Offset: 0x0003DE53
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003FC73 File Offset: 0x0003DE73
		public LocalizedString Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040006A0 RID: 1696
		private LocalizedString reason;
	}
}
