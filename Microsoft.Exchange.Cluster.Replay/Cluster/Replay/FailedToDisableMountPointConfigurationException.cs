using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000508 RID: 1288
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToDisableMountPointConfigurationException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F37 RID: 12087 RVA: 0x000C51EA File Offset: 0x000C33EA
		public FailedToDisableMountPointConfigurationException(string regkeyroot) : base(ReplayStrings.FailedToDisableMountPointConfigurationException(regkeyroot))
		{
			this.regkeyroot = regkeyroot;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000C5204 File Offset: 0x000C3404
		public FailedToDisableMountPointConfigurationException(string regkeyroot, Exception innerException) : base(ReplayStrings.FailedToDisableMountPointConfigurationException(regkeyroot), innerException)
		{
			this.regkeyroot = regkeyroot;
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000C521F File Offset: 0x000C341F
		protected FailedToDisableMountPointConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.regkeyroot = (string)info.GetValue("regkeyroot", typeof(string));
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000C5249 File Offset: 0x000C3449
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("regkeyroot", this.regkeyroot);
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000C5264 File Offset: 0x000C3464
		public string Regkeyroot
		{
			get
			{
				return this.regkeyroot;
			}
		}

		// Token: 0x040015BE RID: 5566
		private readonly string regkeyroot;
	}
}
