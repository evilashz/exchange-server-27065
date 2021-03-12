using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C8 RID: 200
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ResPropTypeNotSupportedException : ClusCommonFailException
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x0001B91E File Offset: 0x00019B1E
		public ResPropTypeNotSupportedException(string propType) : base(Strings.ResPropTypeNotSupportedException(propType))
		{
			this.propType = propType;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001B938 File Offset: 0x00019B38
		public ResPropTypeNotSupportedException(string propType, Exception innerException) : base(Strings.ResPropTypeNotSupportedException(propType), innerException)
		{
			this.propType = propType;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001B953 File Offset: 0x00019B53
		protected ResPropTypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propType = (string)info.GetValue("propType", typeof(string));
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001B97D File Offset: 0x00019B7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propType", this.propType);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001B998 File Offset: 0x00019B98
		public string PropType
		{
			get
			{
				return this.propType;
			}
		}

		// Token: 0x0400071C RID: 1820
		private readonly string propType;
	}
}
