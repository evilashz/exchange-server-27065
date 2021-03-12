using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E34 RID: 3636
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIdentityTypeForClearCmdletException : LocalizedException
	{
		// Token: 0x0600A60D RID: 42509 RVA: 0x002870ED File Offset: 0x002852ED
		public InvalidIdentityTypeForClearCmdletException(string identity) : base(Strings.InvalidIdentityTypeForClearCmdletException(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A60E RID: 42510 RVA: 0x00287102 File Offset: 0x00285302
		public InvalidIdentityTypeForClearCmdletException(string identity, Exception innerException) : base(Strings.InvalidIdentityTypeForClearCmdletException(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A60F RID: 42511 RVA: 0x00287118 File Offset: 0x00285318
		protected InvalidIdentityTypeForClearCmdletException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A610 RID: 42512 RVA: 0x00287142 File Offset: 0x00285342
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x1700365A RID: 13914
		// (get) Token: 0x0600A611 RID: 42513 RVA: 0x0028715D File Offset: 0x0028535D
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04005FC0 RID: 24512
		private readonly string identity;
	}
}
