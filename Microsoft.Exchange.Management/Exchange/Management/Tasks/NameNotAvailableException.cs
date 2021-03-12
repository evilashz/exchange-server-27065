using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E4A RID: 3658
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NameNotAvailableException : RecipientTaskException
	{
		// Token: 0x0600A66F RID: 42607 RVA: 0x0028777D File Offset: 0x0028597D
		public NameNotAvailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A670 RID: 42608 RVA: 0x00287786 File Offset: 0x00285986
		public NameNotAvailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A671 RID: 42609 RVA: 0x00287790 File Offset: 0x00285990
		protected NameNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A672 RID: 42610 RVA: 0x0028779A File Offset: 0x0028599A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
