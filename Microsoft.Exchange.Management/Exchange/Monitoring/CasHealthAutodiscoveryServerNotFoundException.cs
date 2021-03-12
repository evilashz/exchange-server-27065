using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F0B RID: 3851
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthAutodiscoveryServerNotFoundException : LocalizedException
	{
		// Token: 0x0600AA1F RID: 43551 RVA: 0x0028CCFE File Offset: 0x0028AEFE
		public CasHealthAutodiscoveryServerNotFoundException(string smtpAddress, string additionalInformation) : base(Strings.CasHealthAutodiscoveryServerNotFound(smtpAddress, additionalInformation))
		{
			this.smtpAddress = smtpAddress;
			this.additionalInformation = additionalInformation;
		}

		// Token: 0x0600AA20 RID: 43552 RVA: 0x0028CD1B File Offset: 0x0028AF1B
		public CasHealthAutodiscoveryServerNotFoundException(string smtpAddress, string additionalInformation, Exception innerException) : base(Strings.CasHealthAutodiscoveryServerNotFound(smtpAddress, additionalInformation), innerException)
		{
			this.smtpAddress = smtpAddress;
			this.additionalInformation = additionalInformation;
		}

		// Token: 0x0600AA21 RID: 43553 RVA: 0x0028CD3C File Offset: 0x0028AF3C
		protected CasHealthAutodiscoveryServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.smtpAddress = (string)info.GetValue("smtpAddress", typeof(string));
			this.additionalInformation = (string)info.GetValue("additionalInformation", typeof(string));
		}

		// Token: 0x0600AA22 RID: 43554 RVA: 0x0028CD91 File Offset: 0x0028AF91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("smtpAddress", this.smtpAddress);
			info.AddValue("additionalInformation", this.additionalInformation);
		}

		// Token: 0x17003710 RID: 14096
		// (get) Token: 0x0600AA23 RID: 43555 RVA: 0x0028CDBD File Offset: 0x0028AFBD
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x17003711 RID: 14097
		// (get) Token: 0x0600AA24 RID: 43556 RVA: 0x0028CDC5 File Offset: 0x0028AFC5
		public string AdditionalInformation
		{
			get
			{
				return this.additionalInformation;
			}
		}

		// Token: 0x04006076 RID: 24694
		private readonly string smtpAddress;

		// Token: 0x04006077 RID: 24695
		private readonly string additionalInformation;
	}
}
