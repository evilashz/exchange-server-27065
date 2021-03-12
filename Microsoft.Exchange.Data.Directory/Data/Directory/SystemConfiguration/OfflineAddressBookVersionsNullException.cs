using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000AE0 RID: 2784
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OfflineAddressBookVersionsNullException : LocalizedException
	{
		// Token: 0x06008118 RID: 33048 RVA: 0x001A62CB File Offset: 0x001A44CB
		public OfflineAddressBookVersionsNullException() : base(DirectoryStrings.OabVersionsNullException)
		{
		}

		// Token: 0x06008119 RID: 33049 RVA: 0x001A62D8 File Offset: 0x001A44D8
		public OfflineAddressBookVersionsNullException(Exception innerException) : base(DirectoryStrings.OabVersionsNullException, innerException)
		{
		}

		// Token: 0x0600811A RID: 33050 RVA: 0x001A62E6 File Offset: 0x001A44E6
		protected OfflineAddressBookVersionsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x001A62F0 File Offset: 0x001A44F0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
