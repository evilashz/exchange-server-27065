using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000E80 RID: 3712
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MustSpecifyEitherAccessOrExtendedRightsException : LocalizedException
	{
		// Token: 0x0600A753 RID: 42835 RVA: 0x00288399 File Offset: 0x00286599
		public MustSpecifyEitherAccessOrExtendedRightsException() : base(Strings.MustSpecifyEitherAccessOrExtendedRightsException)
		{
		}

		// Token: 0x0600A754 RID: 42836 RVA: 0x002883A6 File Offset: 0x002865A6
		public MustSpecifyEitherAccessOrExtendedRightsException(Exception innerException) : base(Strings.MustSpecifyEitherAccessOrExtendedRightsException, innerException)
		{
		}

		// Token: 0x0600A755 RID: 42837 RVA: 0x002883B4 File Offset: 0x002865B4
		protected MustSpecifyEitherAccessOrExtendedRightsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A756 RID: 42838 RVA: 0x002883BE File Offset: 0x002865BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
