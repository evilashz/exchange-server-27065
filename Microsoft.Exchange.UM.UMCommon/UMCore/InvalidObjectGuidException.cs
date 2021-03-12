using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F7 RID: 503
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidObjectGuidException : LocalizedException
	{
		// Token: 0x0600109B RID: 4251 RVA: 0x000390E9 File Offset: 0x000372E9
		public InvalidObjectGuidException(string smtpAddress) : base(Strings.InvalidObjectGuidException(smtpAddress))
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000390FE File Offset: 0x000372FE
		public InvalidObjectGuidException(string smtpAddress, Exception innerException) : base(Strings.InvalidObjectGuidException(smtpAddress), innerException)
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00039114 File Offset: 0x00037314
		protected InvalidObjectGuidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.smtpAddress = (string)info.GetValue("smtpAddress", typeof(string));
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0003913E File Offset: 0x0003733E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("smtpAddress", this.smtpAddress);
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00039159 File Offset: 0x00037359
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x0400087D RID: 2173
		private readonly string smtpAddress;
	}
}
