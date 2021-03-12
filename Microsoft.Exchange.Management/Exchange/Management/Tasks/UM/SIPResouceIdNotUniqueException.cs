using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011AF RID: 4527
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SIPResouceIdNotUniqueException : LocalizedException
	{
		// Token: 0x0600B857 RID: 47191 RVA: 0x002A475E File Offset: 0x002A295E
		public SIPResouceIdNotUniqueException() : base(Strings.ExceptionSipResourceIdNotUnique)
		{
		}

		// Token: 0x0600B858 RID: 47192 RVA: 0x002A476B File Offset: 0x002A296B
		public SIPResouceIdNotUniqueException(Exception innerException) : base(Strings.ExceptionSipResourceIdNotUnique, innerException)
		{
		}

		// Token: 0x0600B859 RID: 47193 RVA: 0x002A4779 File Offset: 0x002A2979
		protected SIPResouceIdNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B85A RID: 47194 RVA: 0x002A4783 File Offset: 0x002A2983
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
