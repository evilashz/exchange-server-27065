using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000FB4 RID: 4020
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoRemoteInstallPathException : LocalizedException
	{
		// Token: 0x0600AD5D RID: 44381 RVA: 0x002919C9 File Offset: 0x0028FBC9
		public NoRemoteInstallPathException(string s) : base(Strings.NoRemoteInstallPath(s))
		{
			this.s = s;
		}

		// Token: 0x0600AD5E RID: 44382 RVA: 0x002919DE File Offset: 0x0028FBDE
		public NoRemoteInstallPathException(string s, Exception innerException) : base(Strings.NoRemoteInstallPath(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x0600AD5F RID: 44383 RVA: 0x002919F4 File Offset: 0x0028FBF4
		protected NoRemoteInstallPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x0600AD60 RID: 44384 RVA: 0x00291A1E File Offset: 0x0028FC1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x170037AA RID: 14250
		// (get) Token: 0x0600AD61 RID: 44385 RVA: 0x00291A39 File Offset: 0x0028FC39
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x04006110 RID: 24848
		private readonly string s;
	}
}
