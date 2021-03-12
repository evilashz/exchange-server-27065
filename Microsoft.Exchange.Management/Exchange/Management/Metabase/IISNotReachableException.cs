using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000DDF RID: 3551
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IISNotReachableException : DataSourceTransientException
	{
		// Token: 0x0600A448 RID: 42056 RVA: 0x00283F44 File Offset: 0x00282144
		public IISNotReachableException(string serverName, string errorMsg) : base(Strings.IISNotReachableException(serverName, errorMsg))
		{
			this.serverName = serverName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A449 RID: 42057 RVA: 0x00283F61 File Offset: 0x00282161
		public IISNotReachableException(string serverName, string errorMsg, Exception innerException) : base(Strings.IISNotReachableException(serverName, errorMsg), innerException)
		{
			this.serverName = serverName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A44A RID: 42058 RVA: 0x00283F80 File Offset: 0x00282180
		protected IISNotReachableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600A44B RID: 42059 RVA: 0x00283FD5 File Offset: 0x002821D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x170035E9 RID: 13801
		// (get) Token: 0x0600A44C RID: 42060 RVA: 0x00284001 File Offset: 0x00282201
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x170035EA RID: 13802
		// (get) Token: 0x0600A44D RID: 42061 RVA: 0x00284009 File Offset: 0x00282209
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04005F4F RID: 24399
		private readonly string serverName;

		// Token: 0x04005F50 RID: 24400
		private readonly string errorMsg;
	}
}
