using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F0A RID: 3850
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasTransactionResultToStringCaseNotHandledException : LocalizedException
	{
		// Token: 0x0600AA1A RID: 43546 RVA: 0x0028CC81 File Offset: 0x0028AE81
		public CasTransactionResultToStringCaseNotHandledException(CasTransactionResultEnum result) : base(Strings.CasTransactionResultCaseNotHandled(result))
		{
			this.result = result;
		}

		// Token: 0x0600AA1B RID: 43547 RVA: 0x0028CC96 File Offset: 0x0028AE96
		public CasTransactionResultToStringCaseNotHandledException(CasTransactionResultEnum result, Exception innerException) : base(Strings.CasTransactionResultCaseNotHandled(result), innerException)
		{
			this.result = result;
		}

		// Token: 0x0600AA1C RID: 43548 RVA: 0x0028CCAC File Offset: 0x0028AEAC
		protected CasTransactionResultToStringCaseNotHandledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.result = (CasTransactionResultEnum)info.GetValue("result", typeof(CasTransactionResultEnum));
		}

		// Token: 0x0600AA1D RID: 43549 RVA: 0x0028CCD6 File Offset: 0x0028AED6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("result", this.result);
		}

		// Token: 0x1700370F RID: 14095
		// (get) Token: 0x0600AA1E RID: 43550 RVA: 0x0028CCF6 File Offset: 0x0028AEF6
		public CasTransactionResultEnum Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04006075 RID: 24693
		private readonly CasTransactionResultEnum result;
	}
}
