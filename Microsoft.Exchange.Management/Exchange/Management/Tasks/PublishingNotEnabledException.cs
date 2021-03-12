using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010A9 RID: 4265
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PublishingNotEnabledException : LocalizedException
	{
		// Token: 0x0600B245 RID: 45637 RVA: 0x002998D6 File Offset: 0x00297AD6
		public PublishingNotEnabledException() : base(Strings.PublishingNotEnabled)
		{
		}

		// Token: 0x0600B246 RID: 45638 RVA: 0x002998E3 File Offset: 0x00297AE3
		public PublishingNotEnabledException(Exception innerException) : base(Strings.PublishingNotEnabled, innerException)
		{
		}

		// Token: 0x0600B247 RID: 45639 RVA: 0x002998F1 File Offset: 0x00297AF1
		protected PublishingNotEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B248 RID: 45640 RVA: 0x002998FB File Offset: 0x00297AFB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
