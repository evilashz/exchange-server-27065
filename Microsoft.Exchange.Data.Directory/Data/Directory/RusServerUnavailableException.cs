using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A58 RID: 2648
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RusServerUnavailableException : RusTransientException
	{
		// Token: 0x06007EB8 RID: 32440 RVA: 0x001A38C9 File Offset: 0x001A1AC9
		public RusServerUnavailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x001A38D2 File Offset: 0x001A1AD2
		public RusServerUnavailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x001A38DC File Offset: 0x001A1ADC
		protected RusServerUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x001A38E6 File Offset: 0x001A1AE6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
