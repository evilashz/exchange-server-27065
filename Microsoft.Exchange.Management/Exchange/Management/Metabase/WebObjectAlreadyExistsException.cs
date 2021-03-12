using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FA5 RID: 4005
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WebObjectAlreadyExistsException : DataSourceOperationException
	{
		// Token: 0x0600AD04 RID: 44292 RVA: 0x00290E30 File Offset: 0x0028F030
		public WebObjectAlreadyExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AD05 RID: 44293 RVA: 0x00290E39 File Offset: 0x0028F039
		public WebObjectAlreadyExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AD06 RID: 44294 RVA: 0x00290E43 File Offset: 0x0028F043
		protected WebObjectAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD07 RID: 44295 RVA: 0x00290E4D File Offset: 0x0028F04D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
