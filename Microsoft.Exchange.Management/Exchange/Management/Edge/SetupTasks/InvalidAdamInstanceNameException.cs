using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200122F RID: 4655
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidAdamInstanceNameException : LocalizedException
	{
		// Token: 0x0600BB49 RID: 47945 RVA: 0x002AA05C File Offset: 0x002A825C
		public InvalidAdamInstanceNameException(string instanceName) : base(Strings.InvalidAdamInstanceName(instanceName))
		{
			this.instanceName = instanceName;
		}

		// Token: 0x0600BB4A RID: 47946 RVA: 0x002AA071 File Offset: 0x002A8271
		public InvalidAdamInstanceNameException(string instanceName, Exception innerException) : base(Strings.InvalidAdamInstanceName(instanceName), innerException)
		{
			this.instanceName = instanceName;
		}

		// Token: 0x0600BB4B RID: 47947 RVA: 0x002AA087 File Offset: 0x002A8287
		protected InvalidAdamInstanceNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.instanceName = (string)info.GetValue("instanceName", typeof(string));
		}

		// Token: 0x0600BB4C RID: 47948 RVA: 0x002AA0B1 File Offset: 0x002A82B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("instanceName", this.instanceName);
		}

		// Token: 0x17003AED RID: 15085
		// (get) Token: 0x0600BB4D RID: 47949 RVA: 0x002AA0CC File Offset: 0x002A82CC
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x04006575 RID: 25973
		private readonly string instanceName;
	}
}
