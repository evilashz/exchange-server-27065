using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.BestPracticesAnalyzer
{
	// Token: 0x02000F39 RID: 3897
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BPAConfigurationErrorFoundException : LocalizedException
	{
		// Token: 0x0600AB18 RID: 43800 RVA: 0x0028E915 File Offset: 0x0028CB15
		public BPAConfigurationErrorFoundException(string error) : base(Strings.BPAConfigurationErrorFound(error))
		{
			this.error = error;
		}

		// Token: 0x0600AB19 RID: 43801 RVA: 0x0028E92A File Offset: 0x0028CB2A
		public BPAConfigurationErrorFoundException(string error, Exception innerException) : base(Strings.BPAConfigurationErrorFound(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600AB1A RID: 43802 RVA: 0x0028E940 File Offset: 0x0028CB40
		protected BPAConfigurationErrorFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600AB1B RID: 43803 RVA: 0x0028E96A File Offset: 0x0028CB6A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17003751 RID: 14161
		// (get) Token: 0x0600AB1C RID: 43804 RVA: 0x0028E985 File Offset: 0x0028CB85
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040060B7 RID: 24759
		private readonly string error;
	}
}
