using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F93 RID: 3987
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorSourceAndTargetServersInOneRgException : LocalizedException
	{
		// Token: 0x0600ACB6 RID: 44214 RVA: 0x00290934 File Offset: 0x0028EB34
		public SendConnectorSourceAndTargetServersInOneRgException(string routingGroupName) : base(Strings.SendConnectorSourceAndTargetServersInOneRg(routingGroupName))
		{
			this.routingGroupName = routingGroupName;
		}

		// Token: 0x0600ACB7 RID: 44215 RVA: 0x00290949 File Offset: 0x0028EB49
		public SendConnectorSourceAndTargetServersInOneRgException(string routingGroupName, Exception innerException) : base(Strings.SendConnectorSourceAndTargetServersInOneRg(routingGroupName), innerException)
		{
			this.routingGroupName = routingGroupName;
		}

		// Token: 0x0600ACB8 RID: 44216 RVA: 0x0029095F File Offset: 0x0028EB5F
		protected SendConnectorSourceAndTargetServersInOneRgException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.routingGroupName = (string)info.GetValue("routingGroupName", typeof(string));
		}

		// Token: 0x0600ACB9 RID: 44217 RVA: 0x00290989 File Offset: 0x0028EB89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("routingGroupName", this.routingGroupName);
		}

		// Token: 0x17003787 RID: 14215
		// (get) Token: 0x0600ACBA RID: 44218 RVA: 0x002909A4 File Offset: 0x0028EBA4
		public string RoutingGroupName
		{
			get
			{
				return this.routingGroupName;
			}
		}

		// Token: 0x040060ED RID: 24813
		private readonly string routingGroupName;
	}
}
