using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF4 RID: 3828
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssociatedMRSRequestExistsException : LocalizedException
	{
		// Token: 0x0600A9AC RID: 43436 RVA: 0x0028C226 File Offset: 0x0028A426
		public AssociatedMRSRequestExistsException(string requestType) : base(Strings.ErrorAssociatedMRSRequestExists(requestType))
		{
			this.requestType = requestType;
		}

		// Token: 0x0600A9AD RID: 43437 RVA: 0x0028C23B File Offset: 0x0028A43B
		public AssociatedMRSRequestExistsException(string requestType, Exception innerException) : base(Strings.ErrorAssociatedMRSRequestExists(requestType), innerException)
		{
			this.requestType = requestType;
		}

		// Token: 0x0600A9AE RID: 43438 RVA: 0x0028C251 File Offset: 0x0028A451
		protected AssociatedMRSRequestExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestType = (string)info.GetValue("requestType", typeof(string));
		}

		// Token: 0x0600A9AF RID: 43439 RVA: 0x0028C27B File Offset: 0x0028A47B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestType", this.requestType);
		}

		// Token: 0x170036F9 RID: 14073
		// (get) Token: 0x0600A9B0 RID: 43440 RVA: 0x0028C296 File Offset: 0x0028A496
		public string RequestType
		{
			get
			{
				return this.requestType;
			}
		}

		// Token: 0x0400605F RID: 24671
		private readonly string requestType;
	}
}
