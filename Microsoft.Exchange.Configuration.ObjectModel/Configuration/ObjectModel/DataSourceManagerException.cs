using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002A6 RID: 678
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataSourceManagerException : LocalizedException
	{
		// Token: 0x060018B3 RID: 6323 RVA: 0x0005C2CD File Offset: 0x0005A4CD
		public DataSourceManagerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0005C2D6 File Offset: 0x0005A4D6
		public DataSourceManagerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0005C2E0 File Offset: 0x0005A4E0
		protected DataSourceManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0005C2EA File Offset: 0x0005A4EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
