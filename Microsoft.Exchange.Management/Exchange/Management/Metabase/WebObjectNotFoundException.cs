using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FA4 RID: 4004
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WebObjectNotFoundException : DataSourceOperationException
	{
		// Token: 0x0600AD00 RID: 44288 RVA: 0x00290E09 File Offset: 0x0028F009
		public WebObjectNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AD01 RID: 44289 RVA: 0x00290E12 File Offset: 0x0028F012
		public WebObjectNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AD02 RID: 44290 RVA: 0x00290E1C File Offset: 0x0028F01C
		protected WebObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD03 RID: 44291 RVA: 0x00290E26 File Offset: 0x0028F026
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
