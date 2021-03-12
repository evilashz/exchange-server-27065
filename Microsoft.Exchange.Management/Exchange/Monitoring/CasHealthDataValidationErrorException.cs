using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F1D RID: 3869
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthDataValidationErrorException : LocalizedException
	{
		// Token: 0x0600AA82 RID: 43650 RVA: 0x0028D88E File Offset: 0x0028BA8E
		public CasHealthDataValidationErrorException(string additionalInformation) : base(Strings.CasHealthDataValidationError(additionalInformation))
		{
			this.additionalInformation = additionalInformation;
		}

		// Token: 0x0600AA83 RID: 43651 RVA: 0x0028D8A3 File Offset: 0x0028BAA3
		public CasHealthDataValidationErrorException(string additionalInformation, Exception innerException) : base(Strings.CasHealthDataValidationError(additionalInformation), innerException)
		{
			this.additionalInformation = additionalInformation;
		}

		// Token: 0x0600AA84 RID: 43652 RVA: 0x0028D8B9 File Offset: 0x0028BAB9
		protected CasHealthDataValidationErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.additionalInformation = (string)info.GetValue("additionalInformation", typeof(string));
		}

		// Token: 0x0600AA85 RID: 43653 RVA: 0x0028D8E3 File Offset: 0x0028BAE3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("additionalInformation", this.additionalInformation);
		}

		// Token: 0x1700372B RID: 14123
		// (get) Token: 0x0600AA86 RID: 43654 RVA: 0x0028D8FE File Offset: 0x0028BAFE
		public string AdditionalInformation
		{
			get
			{
				return this.additionalInformation;
			}
		}

		// Token: 0x04006091 RID: 24721
		private readonly string additionalInformation;
	}
}
