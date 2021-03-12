using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002BD RID: 701
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ManagementObjectAlreadyExistsException : LocalizedException
	{
		// Token: 0x06001924 RID: 6436 RVA: 0x0005CD20 File Offset: 0x0005AF20
		public ManagementObjectAlreadyExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0005CD29 File Offset: 0x0005AF29
		public ManagementObjectAlreadyExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0005CD33 File Offset: 0x0005AF33
		protected ManagementObjectAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0005CD3D File Offset: 0x0005AF3D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
