using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003E0 RID: 992
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskComponentManagerServerManagerCmdFailure : DagTaskServerException
	{
		// Token: 0x060028CD RID: 10445 RVA: 0x000B8925 File Offset: 0x000B6B25
		public DagTaskComponentManagerServerManagerCmdFailure(string error) : base(ReplayStrings.DagTaskComponentManagerServerManagerCmdFailure(error))
		{
			this.error = error;
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000B893F File Offset: 0x000B6B3F
		public DagTaskComponentManagerServerManagerCmdFailure(string error, Exception innerException) : base(ReplayStrings.DagTaskComponentManagerServerManagerCmdFailure(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000B895A File Offset: 0x000B6B5A
		protected DagTaskComponentManagerServerManagerCmdFailure(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000B8984 File Offset: 0x000B6B84
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x000B899F File Offset: 0x000B6B9F
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040013F4 RID: 5108
		private readonly string error;
	}
}
