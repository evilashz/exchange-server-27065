using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200111C RID: 4380
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationMaxConcurrentMigrationsVerificationFailedException : LocalizedException
	{
		// Token: 0x0600B477 RID: 46199 RVA: 0x0029CC75 File Offset: 0x0029AE75
		public MigrationMaxConcurrentMigrationsVerificationFailedException(int value, int minValue, int maxValue) : base(Strings.MigrationMaxConcurrentMigrationsVerificationFailed(value, minValue, maxValue))
		{
			this.value = value;
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		// Token: 0x0600B478 RID: 46200 RVA: 0x0029CC9A File Offset: 0x0029AE9A
		public MigrationMaxConcurrentMigrationsVerificationFailedException(int value, int minValue, int maxValue, Exception innerException) : base(Strings.MigrationMaxConcurrentMigrationsVerificationFailed(value, minValue, maxValue), innerException)
		{
			this.value = value;
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		// Token: 0x0600B479 RID: 46201 RVA: 0x0029CCC4 File Offset: 0x0029AEC4
		protected MigrationMaxConcurrentMigrationsVerificationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.value = (int)info.GetValue("value", typeof(int));
			this.minValue = (int)info.GetValue("minValue", typeof(int));
			this.maxValue = (int)info.GetValue("maxValue", typeof(int));
		}

		// Token: 0x0600B47A RID: 46202 RVA: 0x0029CD39 File Offset: 0x0029AF39
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("value", this.value);
			info.AddValue("minValue", this.minValue);
			info.AddValue("maxValue", this.maxValue);
		}

		// Token: 0x17003924 RID: 14628
		// (get) Token: 0x0600B47B RID: 46203 RVA: 0x0029CD76 File Offset: 0x0029AF76
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17003925 RID: 14629
		// (get) Token: 0x0600B47C RID: 46204 RVA: 0x0029CD7E File Offset: 0x0029AF7E
		public int MinValue
		{
			get
			{
				return this.minValue;
			}
		}

		// Token: 0x17003926 RID: 14630
		// (get) Token: 0x0600B47D RID: 46205 RVA: 0x0029CD86 File Offset: 0x0029AF86
		public int MaxValue
		{
			get
			{
				return this.maxValue;
			}
		}

		// Token: 0x0400628A RID: 25226
		private readonly int value;

		// Token: 0x0400628B RID: 25227
		private readonly int minValue;

		// Token: 0x0400628C RID: 25228
		private readonly int maxValue;
	}
}
