using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200102A RID: 4138
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SmartHostsIPValidationFailedException : LocalizedException
	{
		// Token: 0x0600AF8C RID: 44940 RVA: 0x002948B4 File Offset: 0x00292AB4
		public SmartHostsIPValidationFailedException(string ipAddress) : base(Strings.SmartHostsIPValidationFailedId(ipAddress))
		{
			this.ipAddress = ipAddress;
		}

		// Token: 0x0600AF8D RID: 44941 RVA: 0x002948C9 File Offset: 0x00292AC9
		public SmartHostsIPValidationFailedException(string ipAddress, Exception innerException) : base(Strings.SmartHostsIPValidationFailedId(ipAddress), innerException)
		{
			this.ipAddress = ipAddress;
		}

		// Token: 0x0600AF8E RID: 44942 RVA: 0x002948DF File Offset: 0x00292ADF
		protected SmartHostsIPValidationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipAddress = (string)info.GetValue("ipAddress", typeof(string));
		}

		// Token: 0x0600AF8F RID: 44943 RVA: 0x00294909 File Offset: 0x00292B09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipAddress", this.ipAddress);
		}

		// Token: 0x17003801 RID: 14337
		// (get) Token: 0x0600AF90 RID: 44944 RVA: 0x00294924 File Offset: 0x00292B24
		public string IpAddress
		{
			get
			{
				return this.ipAddress;
			}
		}

		// Token: 0x04006167 RID: 24935
		private readonly string ipAddress;
	}
}
