using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F8B RID: 3979
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorNonSmtpAddressSpaceOnDNSConnectorException : LocalizedException
	{
		// Token: 0x0600AC92 RID: 44178 RVA: 0x00290698 File Offset: 0x0028E898
		public SendConnectorNonSmtpAddressSpaceOnDNSConnectorException(string addressSpace) : base(Strings.SendConnectorNonSmtpAddressSpaceOnDNSConnector(addressSpace))
		{
			this.addressSpace = addressSpace;
		}

		// Token: 0x0600AC93 RID: 44179 RVA: 0x002906AD File Offset: 0x0028E8AD
		public SendConnectorNonSmtpAddressSpaceOnDNSConnectorException(string addressSpace, Exception innerException) : base(Strings.SendConnectorNonSmtpAddressSpaceOnDNSConnector(addressSpace), innerException)
		{
			this.addressSpace = addressSpace;
		}

		// Token: 0x0600AC94 RID: 44180 RVA: 0x002906C3 File Offset: 0x0028E8C3
		protected SendConnectorNonSmtpAddressSpaceOnDNSConnectorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.addressSpace = (string)info.GetValue("addressSpace", typeof(string));
		}

		// Token: 0x0600AC95 RID: 44181 RVA: 0x002906ED File Offset: 0x0028E8ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("addressSpace", this.addressSpace);
		}

		// Token: 0x17003783 RID: 14211
		// (get) Token: 0x0600AC96 RID: 44182 RVA: 0x00290708 File Offset: 0x0028E908
		public string AddressSpace
		{
			get
			{
				return this.addressSpace;
			}
		}

		// Token: 0x040060E9 RID: 24809
		private readonly string addressSpace;
	}
}
