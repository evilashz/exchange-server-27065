using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200100A RID: 4106
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DlpPolicyInvalidCollectionException : LocalizedException
	{
		// Token: 0x0600AEF3 RID: 44787 RVA: 0x00293B8C File Offset: 0x00291D8C
		public DlpPolicyInvalidCollectionException() : base(Strings.DlpPolicyInvalidCollectionError)
		{
		}

		// Token: 0x0600AEF4 RID: 44788 RVA: 0x00293B99 File Offset: 0x00291D99
		public DlpPolicyInvalidCollectionException(Exception innerException) : base(Strings.DlpPolicyInvalidCollectionError, innerException)
		{
		}

		// Token: 0x0600AEF5 RID: 44789 RVA: 0x00293BA7 File Offset: 0x00291DA7
		protected DlpPolicyInvalidCollectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEF6 RID: 44790 RVA: 0x00293BB1 File Offset: 0x00291DB1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
