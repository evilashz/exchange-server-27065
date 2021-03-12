using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F1E RID: 3870
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InstructResetCredentialsException : LocalizedException
	{
		// Token: 0x0600AA87 RID: 43655 RVA: 0x0028D906 File Offset: 0x0028BB06
		public InstructResetCredentialsException(string detailedInformation) : base(Strings.InstructResetCredentials(detailedInformation))
		{
			this.detailedInformation = detailedInformation;
		}

		// Token: 0x0600AA88 RID: 43656 RVA: 0x0028D91B File Offset: 0x0028BB1B
		public InstructResetCredentialsException(string detailedInformation, Exception innerException) : base(Strings.InstructResetCredentials(detailedInformation), innerException)
		{
			this.detailedInformation = detailedInformation;
		}

		// Token: 0x0600AA89 RID: 43657 RVA: 0x0028D931 File Offset: 0x0028BB31
		protected InstructResetCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.detailedInformation = (string)info.GetValue("detailedInformation", typeof(string));
		}

		// Token: 0x0600AA8A RID: 43658 RVA: 0x0028D95B File Offset: 0x0028BB5B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("detailedInformation", this.detailedInformation);
		}

		// Token: 0x1700372C RID: 14124
		// (get) Token: 0x0600AA8B RID: 43659 RVA: 0x0028D976 File Offset: 0x0028BB76
		public string DetailedInformation
		{
			get
			{
				return this.detailedInformation;
			}
		}

		// Token: 0x04006092 RID: 24722
		private readonly string detailedInformation;
	}
}
