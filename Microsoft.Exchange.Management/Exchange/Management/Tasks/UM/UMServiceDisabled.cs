using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D5 RID: 4565
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMServiceDisabled : LocalizedException
	{
		// Token: 0x0600B906 RID: 47366 RVA: 0x002A54CC File Offset: 0x002A36CC
		public UMServiceDisabled(string serviceName, string serverName) : base(Strings.UMServiceDisabledException(serviceName, serverName))
		{
			this.serviceName = serviceName;
			this.serverName = serverName;
		}

		// Token: 0x0600B907 RID: 47367 RVA: 0x002A54E9 File Offset: 0x002A36E9
		public UMServiceDisabled(string serviceName, string serverName, Exception innerException) : base(Strings.UMServiceDisabledException(serviceName, serverName), innerException)
		{
			this.serviceName = serviceName;
			this.serverName = serverName;
		}

		// Token: 0x0600B908 RID: 47368 RVA: 0x002A5508 File Offset: 0x002A3708
		protected UMServiceDisabled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceName = (string)info.GetValue("serviceName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B909 RID: 47369 RVA: 0x002A555D File Offset: 0x002A375D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceName", this.serviceName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003A2F RID: 14895
		// (get) Token: 0x0600B90A RID: 47370 RVA: 0x002A5589 File Offset: 0x002A3789
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x17003A30 RID: 14896
		// (get) Token: 0x0600B90B RID: 47371 RVA: 0x002A5591 File Offset: 0x002A3791
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400644A RID: 25674
		private readonly string serviceName;

		// Token: 0x0400644B RID: 25675
		private readonly string serverName;
	}
}
