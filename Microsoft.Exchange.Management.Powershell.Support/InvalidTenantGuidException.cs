using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000054 RID: 84
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidTenantGuidException : LocalizedException
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x00011369 File Offset: 0x0000F569
		public InvalidTenantGuidException(string id) : base(Strings.InvalidTenantGuidError(id))
		{
			this.id = id;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001137E File Offset: 0x0000F57E
		public InvalidTenantGuidException(string id, Exception innerException) : base(Strings.InvalidTenantGuidError(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00011394 File Offset: 0x0000F594
		protected InvalidTenantGuidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000113BE File Offset: 0x0000F5BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000113D9 File Offset: 0x0000F5D9
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x040001C4 RID: 452
		private readonly string id;
	}
}
