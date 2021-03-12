using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AA1 RID: 2721
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrgContainerAmbiguousException : ADOperationException
	{
		// Token: 0x06007FF1 RID: 32753 RVA: 0x001A4ACF File Offset: 0x001A2CCF
		public OrgContainerAmbiguousException() : base(DirectoryStrings.OrgContainerAmbiguousException)
		{
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x001A4ADC File Offset: 0x001A2CDC
		public OrgContainerAmbiguousException(Exception innerException) : base(DirectoryStrings.OrgContainerAmbiguousException, innerException)
		{
		}

		// Token: 0x06007FF3 RID: 32755 RVA: 0x001A4AEA File Offset: 0x001A2CEA
		protected OrgContainerAmbiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FF4 RID: 32756 RVA: 0x001A4AF4 File Offset: 0x001A2CF4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
