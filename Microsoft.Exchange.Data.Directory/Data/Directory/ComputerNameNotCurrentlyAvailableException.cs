using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A5F RID: 2655
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ComputerNameNotCurrentlyAvailableException : ADTransientException
	{
		// Token: 0x06007ED9 RID: 32473 RVA: 0x001A3B6E File Offset: 0x001A1D6E
		public ComputerNameNotCurrentlyAvailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x001A3B77 File Offset: 0x001A1D77
		public ComputerNameNotCurrentlyAvailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x001A3B81 File Offset: 0x001A1D81
		protected ComputerNameNotCurrentlyAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x001A3B8B File Offset: 0x001A1D8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
