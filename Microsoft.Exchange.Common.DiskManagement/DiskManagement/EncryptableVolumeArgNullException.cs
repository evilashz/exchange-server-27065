using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EncryptableVolumeArgNullException : BitlockerUtilException
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00004AD1 File Offset: 0x00002CD1
		public EncryptableVolumeArgNullException(string methodName) : base(DiskManagementStrings.EncryptableVolumeArgNullError(methodName))
		{
			this.methodName = methodName;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004AEB File Offset: 0x00002CEB
		public EncryptableVolumeArgNullException(string methodName, Exception innerException) : base(DiskManagementStrings.EncryptableVolumeArgNullError(methodName), innerException)
		{
			this.methodName = methodName;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004B06 File Offset: 0x00002D06
		protected EncryptableVolumeArgNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.methodName = (string)info.GetValue("methodName", typeof(string));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004B30 File Offset: 0x00002D30
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("methodName", this.methodName);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004B4B File Offset: 0x00002D4B
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x04000059 RID: 89
		private readonly string methodName;
	}
}
