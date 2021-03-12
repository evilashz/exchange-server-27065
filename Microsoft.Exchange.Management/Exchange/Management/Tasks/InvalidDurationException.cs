using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001182 RID: 4482
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDurationException : LocalizedException
	{
		// Token: 0x0600B67A RID: 46714 RVA: 0x0029FEF5 File Offset: 0x0029E0F5
		public InvalidDurationException(string minDuration, string maxDuration) : base(Strings.InvalidDuration(minDuration, maxDuration))
		{
			this.minDuration = minDuration;
			this.maxDuration = maxDuration;
		}

		// Token: 0x0600B67B RID: 46715 RVA: 0x0029FF12 File Offset: 0x0029E112
		public InvalidDurationException(string minDuration, string maxDuration, Exception innerException) : base(Strings.InvalidDuration(minDuration, maxDuration), innerException)
		{
			this.minDuration = minDuration;
			this.maxDuration = maxDuration;
		}

		// Token: 0x0600B67C RID: 46716 RVA: 0x0029FF30 File Offset: 0x0029E130
		protected InvalidDurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.minDuration = (string)info.GetValue("minDuration", typeof(string));
			this.maxDuration = (string)info.GetValue("maxDuration", typeof(string));
		}

		// Token: 0x0600B67D RID: 46717 RVA: 0x0029FF85 File Offset: 0x0029E185
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("minDuration", this.minDuration);
			info.AddValue("maxDuration", this.maxDuration);
		}

		// Token: 0x1700398F RID: 14735
		// (get) Token: 0x0600B67E RID: 46718 RVA: 0x0029FFB1 File Offset: 0x0029E1B1
		public string MinDuration
		{
			get
			{
				return this.minDuration;
			}
		}

		// Token: 0x17003990 RID: 14736
		// (get) Token: 0x0600B67F RID: 46719 RVA: 0x0029FFB9 File Offset: 0x0029E1B9
		public string MaxDuration
		{
			get
			{
				return this.maxDuration;
			}
		}

		// Token: 0x040062F5 RID: 25333
		private readonly string minDuration;

		// Token: 0x040062F6 RID: 25334
		private readonly string maxDuration;
	}
}
