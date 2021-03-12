using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000EFB RID: 3835
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotReadRolesFromRegistryException : LocalizedException
	{
		// Token: 0x0600A9D0 RID: 43472 RVA: 0x0028C5BD File Offset: 0x0028A7BD
		public CannotReadRolesFromRegistryException(string errorMsg) : base(Strings.CannotReadRolesFromRegistry(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A9D1 RID: 43473 RVA: 0x0028C5D2 File Offset: 0x0028A7D2
		public CannotReadRolesFromRegistryException(string errorMsg, Exception innerException) : base(Strings.CannotReadRolesFromRegistry(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A9D2 RID: 43474 RVA: 0x0028C5E8 File Offset: 0x0028A7E8
		protected CannotReadRolesFromRegistryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600A9D3 RID: 43475 RVA: 0x0028C612 File Offset: 0x0028A812
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17003701 RID: 14081
		// (get) Token: 0x0600A9D4 RID: 43476 RVA: 0x0028C62D File Offset: 0x0028A82D
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04006067 RID: 24679
		private readonly string errorMsg;
	}
}
