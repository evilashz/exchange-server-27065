using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200100E RID: 4110
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class E4eLicensingIsDisabledExceptionCreateRule : LocalizedException
	{
		// Token: 0x0600AF04 RID: 44804 RVA: 0x00293C91 File Offset: 0x00291E91
		public E4eLicensingIsDisabledExceptionCreateRule() : base(Strings.E4eLicensingIsDisabledCreateRule)
		{
		}

		// Token: 0x0600AF05 RID: 44805 RVA: 0x00293C9E File Offset: 0x00291E9E
		public E4eLicensingIsDisabledExceptionCreateRule(Exception innerException) : base(Strings.E4eLicensingIsDisabledCreateRule, innerException)
		{
		}

		// Token: 0x0600AF06 RID: 44806 RVA: 0x00293CAC File Offset: 0x00291EAC
		protected E4eLicensingIsDisabledExceptionCreateRule(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF07 RID: 44807 RVA: 0x00293CB6 File Offset: 0x00291EB6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
