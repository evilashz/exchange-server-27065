using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010EF RID: 4335
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OldRmsOnlineImportAfterRmsOnlineForwardSync : LocalizedException
	{
		// Token: 0x0600B398 RID: 45976 RVA: 0x0029B765 File Offset: 0x00299965
		public OldRmsOnlineImportAfterRmsOnlineForwardSync() : base(Strings.OldRmsOnlineImportAfterRmsOnlineForwardSync)
		{
		}

		// Token: 0x0600B399 RID: 45977 RVA: 0x0029B772 File Offset: 0x00299972
		public OldRmsOnlineImportAfterRmsOnlineForwardSync(Exception innerException) : base(Strings.OldRmsOnlineImportAfterRmsOnlineForwardSync, innerException)
		{
		}

		// Token: 0x0600B39A RID: 45978 RVA: 0x0029B780 File Offset: 0x00299980
		protected OldRmsOnlineImportAfterRmsOnlineForwardSync(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B39B RID: 45979 RVA: 0x0029B78A File Offset: 0x0029998A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
