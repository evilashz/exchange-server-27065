using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F8A RID: 3978
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorNonSmtpAddressSpaceOnEdgeException : LocalizedException
	{
		// Token: 0x0600AC8D RID: 44173 RVA: 0x00290620 File Offset: 0x0028E820
		public SendConnectorNonSmtpAddressSpaceOnEdgeException(string addressSpace) : base(Strings.SendConnectorNonSmtpAddressSpaceOnEdge(addressSpace))
		{
			this.addressSpace = addressSpace;
		}

		// Token: 0x0600AC8E RID: 44174 RVA: 0x00290635 File Offset: 0x0028E835
		public SendConnectorNonSmtpAddressSpaceOnEdgeException(string addressSpace, Exception innerException) : base(Strings.SendConnectorNonSmtpAddressSpaceOnEdge(addressSpace), innerException)
		{
			this.addressSpace = addressSpace;
		}

		// Token: 0x0600AC8F RID: 44175 RVA: 0x0029064B File Offset: 0x0028E84B
		protected SendConnectorNonSmtpAddressSpaceOnEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.addressSpace = (string)info.GetValue("addressSpace", typeof(string));
		}

		// Token: 0x0600AC90 RID: 44176 RVA: 0x00290675 File Offset: 0x0028E875
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("addressSpace", this.addressSpace);
		}

		// Token: 0x17003782 RID: 14210
		// (get) Token: 0x0600AC91 RID: 44177 RVA: 0x00290690 File Offset: 0x0028E890
		public string AddressSpace
		{
			get
			{
				return this.addressSpace;
			}
		}

		// Token: 0x040060E8 RID: 24808
		private readonly string addressSpace;
	}
}
