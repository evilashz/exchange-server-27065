using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000243 RID: 579
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataProviderConvertingException : PermanentDALException
	{
		// Token: 0x06001721 RID: 5921 RVA: 0x000478AB File Offset: 0x00045AAB
		public DataProviderConvertingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000478B4 File Offset: 0x00045AB4
		public DataProviderConvertingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000478BE File Offset: 0x00045ABE
		protected DataProviderConvertingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000478C8 File Offset: 0x00045AC8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
