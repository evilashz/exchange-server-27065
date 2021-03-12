using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D9 RID: 4569
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SIPFEServerConfigurationNotFoundException : LocalizedException
	{
		// Token: 0x0600B919 RID: 47385 RVA: 0x002A566F File Offset: 0x002A386F
		public SIPFEServerConfigurationNotFoundException(string serverName) : base(Strings.SIPFEServerConfigurationNotFound(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B91A RID: 47386 RVA: 0x002A5684 File Offset: 0x002A3884
		public SIPFEServerConfigurationNotFoundException(string serverName, Exception innerException) : base(Strings.SIPFEServerConfigurationNotFound(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B91B RID: 47387 RVA: 0x002A569A File Offset: 0x002A389A
		protected SIPFEServerConfigurationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B91C RID: 47388 RVA: 0x002A56C4 File Offset: 0x002A38C4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003A32 RID: 14898
		// (get) Token: 0x0600B91D RID: 47389 RVA: 0x002A56DF File Offset: 0x002A38DF
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400644D RID: 25677
		private readonly string serverName;
	}
}
