using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FAE RID: 4014
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoveVirtualDirectoryCouldNotAccessWebServicesRootException : LocalizedException
	{
		// Token: 0x0600AD36 RID: 44342 RVA: 0x00291409 File Offset: 0x0028F609
		public RemoveVirtualDirectoryCouldNotAccessWebServicesRootException() : base(Strings.RemoveVirtualDirectoryCouldNotAccessWebServicesRootException)
		{
		}

		// Token: 0x0600AD37 RID: 44343 RVA: 0x00291416 File Offset: 0x0028F616
		public RemoveVirtualDirectoryCouldNotAccessWebServicesRootException(Exception innerException) : base(Strings.RemoveVirtualDirectoryCouldNotAccessWebServicesRootException, innerException)
		{
		}

		// Token: 0x0600AD38 RID: 44344 RVA: 0x00291424 File Offset: 0x0028F624
		protected RemoveVirtualDirectoryCouldNotAccessWebServicesRootException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD39 RID: 44345 RVA: 0x0029142E File Offset: 0x0028F62E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
