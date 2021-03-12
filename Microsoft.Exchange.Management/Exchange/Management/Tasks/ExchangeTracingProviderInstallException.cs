using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F70 RID: 3952
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeTracingProviderInstallException : LocalizedException
	{
		// Token: 0x0600AC1C RID: 44060 RVA: 0x0028FEBE File Offset: 0x0028E0BE
		public ExchangeTracingProviderInstallException(int errorHresult) : base(Strings.ExchangeTracingProviderInstallFailed(errorHresult))
		{
			this.errorHresult = errorHresult;
		}

		// Token: 0x0600AC1D RID: 44061 RVA: 0x0028FED3 File Offset: 0x0028E0D3
		public ExchangeTracingProviderInstallException(int errorHresult, Exception innerException) : base(Strings.ExchangeTracingProviderInstallFailed(errorHresult), innerException)
		{
			this.errorHresult = errorHresult;
		}

		// Token: 0x0600AC1E RID: 44062 RVA: 0x0028FEE9 File Offset: 0x0028E0E9
		protected ExchangeTracingProviderInstallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorHresult = (int)info.GetValue("errorHresult", typeof(int));
		}

		// Token: 0x0600AC1F RID: 44063 RVA: 0x0028FF13 File Offset: 0x0028E113
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorHresult", this.errorHresult);
		}

		// Token: 0x17003779 RID: 14201
		// (get) Token: 0x0600AC20 RID: 44064 RVA: 0x0028FF2E File Offset: 0x0028E12E
		public int ErrorHresult
		{
			get
			{
				return this.errorHresult;
			}
		}

		// Token: 0x040060DF RID: 24799
		private readonly int errorHresult;
	}
}
