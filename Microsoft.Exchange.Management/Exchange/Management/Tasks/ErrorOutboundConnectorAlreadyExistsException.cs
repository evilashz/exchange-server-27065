using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001013 RID: 4115
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorOutboundConnectorAlreadyExistsException : ManagementObjectAlreadyExistsException
	{
		// Token: 0x0600AF1B RID: 44827 RVA: 0x00293E57 File Offset: 0x00292057
		public ErrorOutboundConnectorAlreadyExistsException(string name) : base(Strings.ErrorOutboundConnectorAlreadyExists(name))
		{
			this.name = name;
		}

		// Token: 0x0600AF1C RID: 44828 RVA: 0x00293E6C File Offset: 0x0029206C
		public ErrorOutboundConnectorAlreadyExistsException(string name, Exception innerException) : base(Strings.ErrorOutboundConnectorAlreadyExists(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AF1D RID: 44829 RVA: 0x00293E82 File Offset: 0x00292082
		protected ErrorOutboundConnectorAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AF1E RID: 44830 RVA: 0x00293EAC File Offset: 0x002920AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170037EC RID: 14316
		// (get) Token: 0x0600AF1F RID: 44831 RVA: 0x00293EC7 File Offset: 0x002920C7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006152 RID: 24914
		private readonly string name;
	}
}
