using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EEF RID: 3823
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BestEffortNotAllowedInServerModeException : LocalizedException
	{
		// Token: 0x0600A996 RID: 43414 RVA: 0x0028C09E File Offset: 0x0028A29E
		public BestEffortNotAllowedInServerModeException() : base(Strings.BestEffortNotAllowedInServerModeException)
		{
		}

		// Token: 0x0600A997 RID: 43415 RVA: 0x0028C0AB File Offset: 0x0028A2AB
		public BestEffortNotAllowedInServerModeException(Exception innerException) : base(Strings.BestEffortNotAllowedInServerModeException, innerException)
		{
		}

		// Token: 0x0600A998 RID: 43416 RVA: 0x0028C0B9 File Offset: 0x0028A2B9
		protected BestEffortNotAllowedInServerModeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A999 RID: 43417 RVA: 0x0028C0C3 File Offset: 0x0028A2C3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
