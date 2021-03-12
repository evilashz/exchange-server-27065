using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200016E RID: 366
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorCompliancePolicyIsDeletedException : LocalizedException
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x00035DC0 File Offset: 0x00033FC0
		public ErrorCompliancePolicyIsDeletedException(string name) : base(Strings.ErrorCompliancePolicyIsDeleted(name))
		{
			this.name = name;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00035DD5 File Offset: 0x00033FD5
		public ErrorCompliancePolicyIsDeletedException(string name, Exception innerException) : base(Strings.ErrorCompliancePolicyIsDeleted(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00035DEB File Offset: 0x00033FEB
		protected ErrorCompliancePolicyIsDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00035E15 File Offset: 0x00034015
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00035E30 File Offset: 0x00034030
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000676 RID: 1654
		private readonly string name;
	}
}
