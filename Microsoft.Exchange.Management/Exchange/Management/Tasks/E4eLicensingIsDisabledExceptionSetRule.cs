using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200100F RID: 4111
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class E4eLicensingIsDisabledExceptionSetRule : LocalizedException
	{
		// Token: 0x0600AF08 RID: 44808 RVA: 0x00293CC0 File Offset: 0x00291EC0
		public E4eLicensingIsDisabledExceptionSetRule() : base(Strings.E4eLicensingIsDisabledSetRule)
		{
		}

		// Token: 0x0600AF09 RID: 44809 RVA: 0x00293CCD File Offset: 0x00291ECD
		public E4eLicensingIsDisabledExceptionSetRule(Exception innerException) : base(Strings.E4eLicensingIsDisabledSetRule, innerException)
		{
		}

		// Token: 0x0600AF0A RID: 44810 RVA: 0x00293CDB File Offset: 0x00291EDB
		protected E4eLicensingIsDisabledExceptionSetRule(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF0B RID: 44811 RVA: 0x00293CE5 File Offset: 0x00291EE5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
