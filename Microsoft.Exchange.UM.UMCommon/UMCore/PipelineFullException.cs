using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020A RID: 522
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PipelineFullException : LocalizedException
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x00039614 File Offset: 0x00037814
		public PipelineFullException(string user) : base(Strings.PipelineFull(user))
		{
			this.user = user;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00039629 File Offset: 0x00037829
		public PipelineFullException(string user, Exception innerException) : base(Strings.PipelineFull(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003963F File Offset: 0x0003783F
		protected PipelineFullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00039669 File Offset: 0x00037869
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00039684 File Offset: 0x00037884
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04000883 RID: 2179
		private readonly string user;
	}
}
