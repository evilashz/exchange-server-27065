using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001221 RID: 4641
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamInstallErrorException : LocalizedException
	{
		// Token: 0x0600BB01 RID: 47873 RVA: 0x002A9915 File Offset: 0x002A7B15
		public AdamInstallErrorException(string error) : base(Strings.AdamInstallError(error))
		{
			this.error = error;
		}

		// Token: 0x0600BB02 RID: 47874 RVA: 0x002A992A File Offset: 0x002A7B2A
		public AdamInstallErrorException(string error, Exception innerException) : base(Strings.AdamInstallError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600BB03 RID: 47875 RVA: 0x002A9940 File Offset: 0x002A7B40
		protected AdamInstallErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600BB04 RID: 47876 RVA: 0x002A996A File Offset: 0x002A7B6A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17003ADD RID: 15069
		// (get) Token: 0x0600BB05 RID: 47877 RVA: 0x002A9985 File Offset: 0x002A7B85
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006565 RID: 25957
		private readonly string error;
	}
}
