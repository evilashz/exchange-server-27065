using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF8 RID: 4088
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorFileHasNoTextContentException : LocalizedException
	{
		// Token: 0x0600AE9A RID: 44698 RVA: 0x00293320 File Offset: 0x00291520
		public ErrorFileHasNoTextContentException() : base(Strings.ErrorFileHasNoTextContent)
		{
		}

		// Token: 0x0600AE9B RID: 44699 RVA: 0x0029332D File Offset: 0x0029152D
		public ErrorFileHasNoTextContentException(Exception innerException) : base(Strings.ErrorFileHasNoTextContent, innerException)
		{
		}

		// Token: 0x0600AE9C RID: 44700 RVA: 0x0029333B File Offset: 0x0029153B
		protected ErrorFileHasNoTextContentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AE9D RID: 44701 RVA: 0x00293345 File Offset: 0x00291545
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
