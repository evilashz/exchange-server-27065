using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A5 RID: 165
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreCommandConstraintFailedException : DxStoreInstanceServerException
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x000146CA File Offset: 0x000128CA
		public DxStoreCommandConstraintFailedException(string phase) : base(Strings.DxStoreCommandConstraintFailed(phase))
		{
			this.phase = phase;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000146E4 File Offset: 0x000128E4
		public DxStoreCommandConstraintFailedException(string phase, Exception innerException) : base(Strings.DxStoreCommandConstraintFailed(phase), innerException)
		{
			this.phase = phase;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000146FF File Offset: 0x000128FF
		protected DxStoreCommandConstraintFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.phase = (string)info.GetValue("phase", typeof(string));
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00014729 File Offset: 0x00012929
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("phase", this.phase);
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00014744 File Offset: 0x00012944
		public string Phase
		{
			get
			{
				return this.phase;
			}
		}

		// Token: 0x04000293 RID: 659
		private readonly string phase;
	}
}
