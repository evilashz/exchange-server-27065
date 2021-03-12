using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200111D RID: 4381
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationMaxConcurrentIncrementalSyncsVerificationFailedException : LocalizedException
	{
		// Token: 0x0600B47E RID: 46206 RVA: 0x0029CD8E File Offset: 0x0029AF8E
		public MigrationMaxConcurrentIncrementalSyncsVerificationFailedException(Unlimited<int> value, Unlimited<int> maxValue) : base(Strings.MigrationMaxConcurrentIncrementalSyncsVerificationFailed(value, maxValue))
		{
			this.value = value;
			this.maxValue = maxValue;
		}

		// Token: 0x0600B47F RID: 46207 RVA: 0x0029CDAB File Offset: 0x0029AFAB
		public MigrationMaxConcurrentIncrementalSyncsVerificationFailedException(Unlimited<int> value, Unlimited<int> maxValue, Exception innerException) : base(Strings.MigrationMaxConcurrentIncrementalSyncsVerificationFailed(value, maxValue), innerException)
		{
			this.value = value;
			this.maxValue = maxValue;
		}

		// Token: 0x0600B480 RID: 46208 RVA: 0x0029CDCC File Offset: 0x0029AFCC
		protected MigrationMaxConcurrentIncrementalSyncsVerificationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.value = (Unlimited<int>)info.GetValue("value", typeof(Unlimited<int>));
			this.maxValue = (Unlimited<int>)info.GetValue("maxValue", typeof(Unlimited<int>));
		}

		// Token: 0x0600B481 RID: 46209 RVA: 0x0029CE21 File Offset: 0x0029B021
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("value", this.value);
			info.AddValue("maxValue", this.maxValue);
		}

		// Token: 0x17003927 RID: 14631
		// (get) Token: 0x0600B482 RID: 46210 RVA: 0x0029CE57 File Offset: 0x0029B057
		public Unlimited<int> Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17003928 RID: 14632
		// (get) Token: 0x0600B483 RID: 46211 RVA: 0x0029CE5F File Offset: 0x0029B05F
		public Unlimited<int> MaxValue
		{
			get
			{
				return this.maxValue;
			}
		}

		// Token: 0x0400628D RID: 25229
		private readonly Unlimited<int> value;

		// Token: 0x0400628E RID: 25230
		private readonly Unlimited<int> maxValue;
	}
}
