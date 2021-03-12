using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001118 RID: 4376
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SkipStepNotSupportedException : LocalizedException
	{
		// Token: 0x0600B466 RID: 46182 RVA: 0x0029CB70 File Offset: 0x0029AD70
		public SkipStepNotSupportedException(string step) : base(Strings.MigrationSkipStepNotSupported(step))
		{
			this.step = step;
		}

		// Token: 0x0600B467 RID: 46183 RVA: 0x0029CB85 File Offset: 0x0029AD85
		public SkipStepNotSupportedException(string step, Exception innerException) : base(Strings.MigrationSkipStepNotSupported(step), innerException)
		{
			this.step = step;
		}

		// Token: 0x0600B468 RID: 46184 RVA: 0x0029CB9B File Offset: 0x0029AD9B
		protected SkipStepNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.step = (string)info.GetValue("step", typeof(string));
		}

		// Token: 0x0600B469 RID: 46185 RVA: 0x0029CBC5 File Offset: 0x0029ADC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("step", this.step);
		}

		// Token: 0x17003923 RID: 14627
		// (get) Token: 0x0600B46A RID: 46186 RVA: 0x0029CBE0 File Offset: 0x0029ADE0
		public string Step
		{
			get
			{
				return this.step;
			}
		}

		// Token: 0x04006289 RID: 25225
		private readonly string step;
	}
}
