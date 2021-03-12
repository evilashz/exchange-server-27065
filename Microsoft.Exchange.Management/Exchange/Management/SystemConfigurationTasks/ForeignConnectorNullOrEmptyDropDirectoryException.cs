using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000FA0 RID: 4000
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ForeignConnectorNullOrEmptyDropDirectoryException : LocalizedException
	{
		// Token: 0x0600ACEF RID: 44271 RVA: 0x00290D04 File Offset: 0x0028EF04
		public ForeignConnectorNullOrEmptyDropDirectoryException() : base(Strings.ForeignConnectorNullOrEmptyDropDirectory)
		{
		}

		// Token: 0x0600ACF0 RID: 44272 RVA: 0x00290D11 File Offset: 0x0028EF11
		public ForeignConnectorNullOrEmptyDropDirectoryException(Exception innerException) : base(Strings.ForeignConnectorNullOrEmptyDropDirectory, innerException)
		{
		}

		// Token: 0x0600ACF1 RID: 44273 RVA: 0x00290D1F File Offset: 0x0028EF1F
		protected ForeignConnectorNullOrEmptyDropDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACF2 RID: 44274 RVA: 0x00290D29 File Offset: 0x0028EF29
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
