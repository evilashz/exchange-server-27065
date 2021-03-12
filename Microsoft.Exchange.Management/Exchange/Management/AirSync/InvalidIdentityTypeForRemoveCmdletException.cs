using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E33 RID: 3635
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIdentityTypeForRemoveCmdletException : LocalizedException
	{
		// Token: 0x0600A608 RID: 42504 RVA: 0x00287075 File Offset: 0x00285275
		public InvalidIdentityTypeForRemoveCmdletException(string identity) : base(Strings.InvalidIdentityTypeForRemoveCmdletException(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A609 RID: 42505 RVA: 0x0028708A File Offset: 0x0028528A
		public InvalidIdentityTypeForRemoveCmdletException(string identity, Exception innerException) : base(Strings.InvalidIdentityTypeForRemoveCmdletException(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A60A RID: 42506 RVA: 0x002870A0 File Offset: 0x002852A0
		protected InvalidIdentityTypeForRemoveCmdletException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A60B RID: 42507 RVA: 0x002870CA File Offset: 0x002852CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17003659 RID: 13913
		// (get) Token: 0x0600A60C RID: 42508 RVA: 0x002870E5 File Offset: 0x002852E5
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04005FBF RID: 24511
		private readonly string identity;
	}
}
