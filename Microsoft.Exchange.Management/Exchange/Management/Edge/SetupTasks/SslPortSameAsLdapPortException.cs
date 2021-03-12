using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200122C RID: 4652
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SslPortSameAsLdapPortException : LocalizedException
	{
		// Token: 0x0600BB3C RID: 47932 RVA: 0x002A9F86 File Offset: 0x002A8186
		public SslPortSameAsLdapPortException() : base(Strings.SslPortSameAsLdapPort)
		{
		}

		// Token: 0x0600BB3D RID: 47933 RVA: 0x002A9F93 File Offset: 0x002A8193
		public SslPortSameAsLdapPortException(Exception innerException) : base(Strings.SslPortSameAsLdapPort, innerException)
		{
		}

		// Token: 0x0600BB3E RID: 47934 RVA: 0x002A9FA1 File Offset: 0x002A81A1
		protected SslPortSameAsLdapPortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600BB3F RID: 47935 RVA: 0x002A9FAB File Offset: 0x002A81AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
