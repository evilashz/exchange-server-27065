using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DatacenterStrings
{
	// Token: 0x02000006 RID: 6
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IDSInternalExceptionErrorBlob : IDSInternalException
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000041B5 File Offset: 0x000023B5
		public IDSInternalExceptionErrorBlob(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000041BE File Offset: 0x000023BE
		public IDSInternalExceptionErrorBlob(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000041C8 File Offset: 0x000023C8
		protected IDSInternalExceptionErrorBlob(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000041D2 File Offset: 0x000023D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
