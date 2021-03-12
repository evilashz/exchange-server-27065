using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A68 RID: 2664
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NameConversionException : DataSourceOperationException
	{
		// Token: 0x06007EFD RID: 32509 RVA: 0x001A3CCD File Offset: 0x001A1ECD
		public NameConversionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x001A3CD6 File Offset: 0x001A1ED6
		public NameConversionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x001A3CE0 File Offset: 0x001A1EE0
		protected NameConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x001A3CEA File Offset: 0x001A1EEA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
