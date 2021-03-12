using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ADA RID: 2778
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoLocatorInformationInMServException : ADOperationException
	{
		// Token: 0x06008100 RID: 33024 RVA: 0x001A61D1 File Offset: 0x001A43D1
		public NoLocatorInformationInMServException() : base(DirectoryStrings.NoLocatorInformationInMServException)
		{
		}

		// Token: 0x06008101 RID: 33025 RVA: 0x001A61DE File Offset: 0x001A43DE
		public NoLocatorInformationInMServException(Exception innerException) : base(DirectoryStrings.NoLocatorInformationInMServException, innerException)
		{
		}

		// Token: 0x06008102 RID: 33026 RVA: 0x001A61EC File Offset: 0x001A43EC
		protected NoLocatorInformationInMServException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008103 RID: 33027 RVA: 0x001A61F6 File Offset: 0x001A43F6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
