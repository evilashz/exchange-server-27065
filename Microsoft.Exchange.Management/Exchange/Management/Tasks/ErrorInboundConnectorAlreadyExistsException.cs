using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001011 RID: 4113
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorInboundConnectorAlreadyExistsException : ManagementObjectAlreadyExistsException
	{
		// Token: 0x0600AF11 RID: 44817 RVA: 0x00293D67 File Offset: 0x00291F67
		public ErrorInboundConnectorAlreadyExistsException(string name) : base(Strings.ErrorInboundConnectorAlreadyExists(name))
		{
			this.name = name;
		}

		// Token: 0x0600AF12 RID: 44818 RVA: 0x00293D7C File Offset: 0x00291F7C
		public ErrorInboundConnectorAlreadyExistsException(string name, Exception innerException) : base(Strings.ErrorInboundConnectorAlreadyExists(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AF13 RID: 44819 RVA: 0x00293D92 File Offset: 0x00291F92
		protected ErrorInboundConnectorAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AF14 RID: 44820 RVA: 0x00293DBC File Offset: 0x00291FBC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170037EA RID: 14314
		// (get) Token: 0x0600AF15 RID: 44821 RVA: 0x00293DD7 File Offset: 0x00291FD7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006150 RID: 24912
		private readonly string name;
	}
}
