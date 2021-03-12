using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000FC9 RID: 4041
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NeedToSpecifyADSiteObjectException : LocalizedException
	{
		// Token: 0x0600ADC4 RID: 44484 RVA: 0x0029232D File Offset: 0x0029052D
		public NeedToSpecifyADSiteObjectException() : base(Strings.NeedToSpecifyADSiteObject)
		{
		}

		// Token: 0x0600ADC5 RID: 44485 RVA: 0x0029233A File Offset: 0x0029053A
		public NeedToSpecifyADSiteObjectException(Exception innerException) : base(Strings.NeedToSpecifyADSiteObject, innerException)
		{
		}

		// Token: 0x0600ADC6 RID: 44486 RVA: 0x00292348 File Offset: 0x00290548
		protected NeedToSpecifyADSiteObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADC7 RID: 44487 RVA: 0x00292352 File Offset: 0x00290552
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
