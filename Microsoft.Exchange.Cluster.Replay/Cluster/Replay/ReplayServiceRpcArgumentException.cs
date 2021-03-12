using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F4 RID: 1012
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceRpcArgumentException : TaskServerException
	{
		// Token: 0x0600293A RID: 10554 RVA: 0x000B9641 File Offset: 0x000B7841
		public ReplayServiceRpcArgumentException(string argument) : base(ReplayStrings.ReplayServiceRpcArgumentException(argument))
		{
			this.argument = argument;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000B965B File Offset: 0x000B785B
		public ReplayServiceRpcArgumentException(string argument, Exception innerException) : base(ReplayStrings.ReplayServiceRpcArgumentException(argument), innerException)
		{
			this.argument = argument;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000B9676 File Offset: 0x000B7876
		protected ReplayServiceRpcArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.argument = (string)info.GetValue("argument", typeof(string));
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000B96A0 File Offset: 0x000B78A0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("argument", this.argument);
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x000B96BB File Offset: 0x000B78BB
		public string Argument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x04001411 RID: 5137
		private readonly string argument;
	}
}
