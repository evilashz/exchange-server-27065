using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001009 RID: 4105
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DlpPolicyParsingException : LocalizedException
	{
		// Token: 0x0600AEEE RID: 44782 RVA: 0x00293B14 File Offset: 0x00291D14
		public DlpPolicyParsingException(string error) : base(Strings.DlpPolicyParsingError(error))
		{
			this.error = error;
		}

		// Token: 0x0600AEEF RID: 44783 RVA: 0x00293B29 File Offset: 0x00291D29
		public DlpPolicyParsingException(string error, Exception innerException) : base(Strings.DlpPolicyParsingError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600AEF0 RID: 44784 RVA: 0x00293B3F File Offset: 0x00291D3F
		protected DlpPolicyParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600AEF1 RID: 44785 RVA: 0x00293B69 File Offset: 0x00291D69
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170037E7 RID: 14311
		// (get) Token: 0x0600AEF2 RID: 44786 RVA: 0x00293B84 File Offset: 0x00291D84
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400614D RID: 24909
		private readonly string error;
	}
}
