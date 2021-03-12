using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000241 RID: 577
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PermanentDALException : DataSourceOperationException
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x0004785D File Offset: 0x00045A5D
		public PermanentDALException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00047866 File Offset: 0x00045A66
		public PermanentDALException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00047870 File Offset: 0x00045A70
		protected PermanentDALException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0004787A File Offset: 0x00045A7A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
