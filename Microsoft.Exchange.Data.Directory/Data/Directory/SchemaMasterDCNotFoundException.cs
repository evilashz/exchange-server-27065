using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A5B RID: 2651
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SchemaMasterDCNotFoundException : ADTransientException
	{
		// Token: 0x06007EC5 RID: 32453 RVA: 0x001A398F File Offset: 0x001A1B8F
		public SchemaMasterDCNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EC6 RID: 32454 RVA: 0x001A3998 File Offset: 0x001A1B98
		public SchemaMasterDCNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EC7 RID: 32455 RVA: 0x001A39A2 File Offset: 0x001A1BA2
		protected SchemaMasterDCNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EC8 RID: 32456 RVA: 0x001A39AC File Offset: 0x001A1BAC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
