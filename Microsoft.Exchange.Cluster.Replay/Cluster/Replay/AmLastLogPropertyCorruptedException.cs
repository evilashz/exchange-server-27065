using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000464 RID: 1124
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmLastLogPropertyCorruptedException : AmCommonException
	{
		// Token: 0x06002B88 RID: 11144 RVA: 0x000BD8BD File Offset: 0x000BBABD
		public AmLastLogPropertyCorruptedException(string property, string corruptedValue) : base(ReplayStrings.AmLastLogPropertyCorruptedException(property, corruptedValue))
		{
			this.property = property;
			this.corruptedValue = corruptedValue;
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000BD8DF File Offset: 0x000BBADF
		public AmLastLogPropertyCorruptedException(string property, string corruptedValue, Exception innerException) : base(ReplayStrings.AmLastLogPropertyCorruptedException(property, corruptedValue), innerException)
		{
			this.property = property;
			this.corruptedValue = corruptedValue;
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000BD904 File Offset: 0x000BBB04
		protected AmLastLogPropertyCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
			this.corruptedValue = (string)info.GetValue("corruptedValue", typeof(string));
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000BD959 File Offset: 0x000BBB59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
			info.AddValue("corruptedValue", this.corruptedValue);
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000BD985 File Offset: 0x000BBB85
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000BD98D File Offset: 0x000BBB8D
		public string CorruptedValue
		{
			get
			{
				return this.corruptedValue;
			}
		}

		// Token: 0x0400149F RID: 5279
		private readonly string property;

		// Token: 0x040014A0 RID: 5280
		private readonly string corruptedValue;
	}
}
