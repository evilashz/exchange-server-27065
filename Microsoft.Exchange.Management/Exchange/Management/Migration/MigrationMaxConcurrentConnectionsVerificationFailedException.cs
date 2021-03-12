using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001113 RID: 4371
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationMaxConcurrentConnectionsVerificationFailedException : LocalizedException
	{
		// Token: 0x0600B44D RID: 46157 RVA: 0x0029C909 File Offset: 0x0029AB09
		public MigrationMaxConcurrentConnectionsVerificationFailedException(string value, string minValue, string maxValue) : base(Strings.MigrationMaxConcurrentConnectionsVerificationFailed(value, minValue, maxValue))
		{
			this.value = value;
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		// Token: 0x0600B44E RID: 46158 RVA: 0x0029C92E File Offset: 0x0029AB2E
		public MigrationMaxConcurrentConnectionsVerificationFailedException(string value, string minValue, string maxValue, Exception innerException) : base(Strings.MigrationMaxConcurrentConnectionsVerificationFailed(value, minValue, maxValue), innerException)
		{
			this.value = value;
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		// Token: 0x0600B44F RID: 46159 RVA: 0x0029C958 File Offset: 0x0029AB58
		protected MigrationMaxConcurrentConnectionsVerificationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			this.minValue = (string)info.GetValue("minValue", typeof(string));
			this.maxValue = (string)info.GetValue("maxValue", typeof(string));
		}

		// Token: 0x0600B450 RID: 46160 RVA: 0x0029C9CD File Offset: 0x0029ABCD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("value", this.value);
			info.AddValue("minValue", this.minValue);
			info.AddValue("maxValue", this.maxValue);
		}

		// Token: 0x1700391E RID: 14622
		// (get) Token: 0x0600B451 RID: 46161 RVA: 0x0029CA0A File Offset: 0x0029AC0A
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700391F RID: 14623
		// (get) Token: 0x0600B452 RID: 46162 RVA: 0x0029CA12 File Offset: 0x0029AC12
		public string MinValue
		{
			get
			{
				return this.minValue;
			}
		}

		// Token: 0x17003920 RID: 14624
		// (get) Token: 0x0600B453 RID: 46163 RVA: 0x0029CA1A File Offset: 0x0029AC1A
		public string MaxValue
		{
			get
			{
				return this.maxValue;
			}
		}

		// Token: 0x04006284 RID: 25220
		private readonly string value;

		// Token: 0x04006285 RID: 25221
		private readonly string minValue;

		// Token: 0x04006286 RID: 25222
		private readonly string maxValue;
	}
}
