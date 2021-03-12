using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002A7 RID: 679
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SchemaMappingException : LocalizedException
	{
		// Token: 0x060018B7 RID: 6327 RVA: 0x0005C2F4 File Offset: 0x0005A4F4
		public SchemaMappingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0005C2FD File Offset: 0x0005A4FD
		public SchemaMappingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0005C307 File Offset: 0x0005A507
		protected SchemaMappingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0005C311 File Offset: 0x0005A511
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
