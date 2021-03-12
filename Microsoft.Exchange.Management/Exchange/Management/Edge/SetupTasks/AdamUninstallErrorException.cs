using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001222 RID: 4642
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamUninstallErrorException : LocalizedException
	{
		// Token: 0x0600BB06 RID: 47878 RVA: 0x002A998D File Offset: 0x002A7B8D
		public AdamUninstallErrorException(string error) : base(Strings.AdamUninstallError(error))
		{
			this.error = error;
		}

		// Token: 0x0600BB07 RID: 47879 RVA: 0x002A99A2 File Offset: 0x002A7BA2
		public AdamUninstallErrorException(string error, Exception innerException) : base(Strings.AdamUninstallError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600BB08 RID: 47880 RVA: 0x002A99B8 File Offset: 0x002A7BB8
		protected AdamUninstallErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600BB09 RID: 47881 RVA: 0x002A99E2 File Offset: 0x002A7BE2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17003ADE RID: 15070
		// (get) Token: 0x0600BB0A RID: 47882 RVA: 0x002A99FD File Offset: 0x002A7BFD
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006566 RID: 25958
		private readonly string error;
	}
}
