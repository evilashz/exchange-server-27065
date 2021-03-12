using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E7C RID: 3708
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToCheckDiscoveryHoldStatusException : RecipientTaskException
	{
		// Token: 0x0600A740 RID: 42816 RVA: 0x00288201 File Offset: 0x00286401
		public FailedToCheckDiscoveryHoldStatusException(LocalizedString error) : base(Strings.FailedToCheckDiscoveryHoldStatusException(error))
		{
			this.error = error;
		}

		// Token: 0x0600A741 RID: 42817 RVA: 0x00288216 File Offset: 0x00286416
		public FailedToCheckDiscoveryHoldStatusException(LocalizedString error, Exception innerException) : base(Strings.FailedToCheckDiscoveryHoldStatusException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600A742 RID: 42818 RVA: 0x0028822C File Offset: 0x0028642C
		protected FailedToCheckDiscoveryHoldStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (LocalizedString)info.GetValue("error", typeof(LocalizedString));
		}

		// Token: 0x0600A743 RID: 42819 RVA: 0x00288256 File Offset: 0x00286456
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700366D RID: 13933
		// (get) Token: 0x0600A744 RID: 42820 RVA: 0x00288276 File Offset: 0x00286476
		public LocalizedString Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04005FD3 RID: 24531
		private readonly LocalizedString error;
	}
}
