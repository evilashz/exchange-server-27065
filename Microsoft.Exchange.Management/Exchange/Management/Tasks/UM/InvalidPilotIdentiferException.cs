using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E6 RID: 4582
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPilotIdentiferException : LocalizedException
	{
		// Token: 0x0600B95E RID: 47454 RVA: 0x002A5DE0 File Offset: 0x002A3FE0
		public InvalidPilotIdentiferException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B95F RID: 47455 RVA: 0x002A5DE9 File Offset: 0x002A3FE9
		public InvalidPilotIdentiferException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B960 RID: 47456 RVA: 0x002A5DF3 File Offset: 0x002A3FF3
		protected InvalidPilotIdentiferException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B961 RID: 47457 RVA: 0x002A5DFD File Offset: 0x002A3FFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
