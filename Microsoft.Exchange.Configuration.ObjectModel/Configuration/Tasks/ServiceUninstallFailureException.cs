using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002B6 RID: 694
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceUninstallFailureException : LocalizedException
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x0005C9CD File Offset: 0x0005ABCD
		public ServiceUninstallFailureException(string servicename, string error) : base(Strings.ServiceUninstallFailure(servicename, error))
		{
			this.servicename = servicename;
			this.error = error;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0005C9EA File Offset: 0x0005ABEA
		public ServiceUninstallFailureException(string servicename, string error, Exception innerException) : base(Strings.ServiceUninstallFailure(servicename, error), innerException)
		{
			this.servicename = servicename;
			this.error = error;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0005CA08 File Offset: 0x0005AC08
		protected ServiceUninstallFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.servicename = (string)info.GetValue("servicename", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0005CA5D File Offset: 0x0005AC5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("servicename", this.servicename);
			info.AddValue("error", this.error);
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0005CA89 File Offset: 0x0005AC89
		public string Servicename
		{
			get
			{
				return this.servicename;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0005CA91 File Offset: 0x0005AC91
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400098E RID: 2446
		private readonly string servicename;

		// Token: 0x0400098F RID: 2447
		private readonly string error;
	}
}
