using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200121C RID: 4636
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamInstallFailureDataOrLogFolderNotEmptyException : LocalizedException
	{
		// Token: 0x0600BAE7 RID: 47847 RVA: 0x002A965D File Offset: 0x002A785D
		public AdamInstallFailureDataOrLogFolderNotEmptyException() : base(Strings.AdamInstallFailureDataOrLogFolderNotEmpty)
		{
		}

		// Token: 0x0600BAE8 RID: 47848 RVA: 0x002A966A File Offset: 0x002A786A
		public AdamInstallFailureDataOrLogFolderNotEmptyException(Exception innerException) : base(Strings.AdamInstallFailureDataOrLogFolderNotEmpty, innerException)
		{
		}

		// Token: 0x0600BAE9 RID: 47849 RVA: 0x002A9678 File Offset: 0x002A7878
		protected AdamInstallFailureDataOrLogFolderNotEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600BAEA RID: 47850 RVA: 0x002A9682 File Offset: 0x002A7882
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
