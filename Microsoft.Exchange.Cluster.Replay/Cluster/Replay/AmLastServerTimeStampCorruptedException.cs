using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000465 RID: 1125
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmLastServerTimeStampCorruptedException : AmCommonException
	{
		// Token: 0x06002B8E RID: 11150 RVA: 0x000BD995 File Offset: 0x000BBB95
		public AmLastServerTimeStampCorruptedException(string property, string corruptedValue) : base(ReplayStrings.AmLastServerTimeStampCorruptedException(property, corruptedValue))
		{
			this.property = property;
			this.corruptedValue = corruptedValue;
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000BD9B7 File Offset: 0x000BBBB7
		public AmLastServerTimeStampCorruptedException(string property, string corruptedValue, Exception innerException) : base(ReplayStrings.AmLastServerTimeStampCorruptedException(property, corruptedValue), innerException)
		{
			this.property = property;
			this.corruptedValue = corruptedValue;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000BD9DC File Offset: 0x000BBBDC
		protected AmLastServerTimeStampCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.property = (string)info.GetValue("property", typeof(string));
			this.corruptedValue = (string)info.GetValue("corruptedValue", typeof(string));
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000BDA31 File Offset: 0x000BBC31
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("property", this.property);
			info.AddValue("corruptedValue", this.corruptedValue);
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000BDA5D File Offset: 0x000BBC5D
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000BDA65 File Offset: 0x000BBC65
		public string CorruptedValue
		{
			get
			{
				return this.corruptedValue;
			}
		}

		// Token: 0x040014A1 RID: 5281
		private readonly string property;

		// Token: 0x040014A2 RID: 5282
		private readonly string corruptedValue;
	}
}
