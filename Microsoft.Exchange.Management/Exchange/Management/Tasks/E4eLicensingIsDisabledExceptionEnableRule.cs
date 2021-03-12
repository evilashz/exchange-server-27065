using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200100D RID: 4109
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class E4eLicensingIsDisabledExceptionEnableRule : LocalizedException
	{
		// Token: 0x0600AF00 RID: 44800 RVA: 0x00293C62 File Offset: 0x00291E62
		public E4eLicensingIsDisabledExceptionEnableRule() : base(Strings.E4eLicensingIsDisabledEnableRule)
		{
		}

		// Token: 0x0600AF01 RID: 44801 RVA: 0x00293C6F File Offset: 0x00291E6F
		public E4eLicensingIsDisabledExceptionEnableRule(Exception innerException) : base(Strings.E4eLicensingIsDisabledEnableRule, innerException)
		{
		}

		// Token: 0x0600AF02 RID: 44802 RVA: 0x00293C7D File Offset: 0x00291E7D
		protected E4eLicensingIsDisabledExceptionEnableRule(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF03 RID: 44803 RVA: 0x00293C87 File Offset: 0x00291E87
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
