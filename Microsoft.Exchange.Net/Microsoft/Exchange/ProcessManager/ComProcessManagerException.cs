using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007F9 RID: 2041
	internal abstract class ComProcessManagerException : LocalizedException
	{
		// Token: 0x06002AE6 RID: 10982 RVA: 0x0005DB97 File Offset: 0x0005BD97
		internal ComProcessManagerException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0005DBA5 File Offset: 0x0005BDA5
		internal ComProcessManagerException(string message, Exception inner) : base(new LocalizedString(message), inner)
		{
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0005DBB4 File Offset: 0x0005BDB4
		public ComProcessManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
