using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000008 RID: 8
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotMapInvalidSmtpAddressToPhotoFileException : LocalizedException
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000034E8 File Offset: 0x000016E8
		public CannotMapInvalidSmtpAddressToPhotoFileException(string address) : base(Strings.CannotMapInvalidSmtpAddressToPhotoFile(address))
		{
			this.address = address;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034FD File Offset: 0x000016FD
		public CannotMapInvalidSmtpAddressToPhotoFileException(string address, Exception innerException) : base(Strings.CannotMapInvalidSmtpAddressToPhotoFile(address), innerException)
		{
			this.address = address;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003513 File Offset: 0x00001713
		protected CannotMapInvalidSmtpAddressToPhotoFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.address = (string)info.GetValue("address", typeof(string));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000353D File Offset: 0x0000173D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("address", this.address);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003558 File Offset: 0x00001758
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x04000066 RID: 102
		private readonly string address;
	}
}
