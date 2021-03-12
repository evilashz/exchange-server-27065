using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x020000E2 RID: 226
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedAudioFormat : AudioConversionException
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00015C47 File Offset: 0x00013E47
		public UnsupportedAudioFormat(string fileName) : base(NetException.UnsupportedAudioFormat(fileName))
		{
			this.fileName = fileName;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00015C61 File Offset: 0x00013E61
		public UnsupportedAudioFormat(string fileName, Exception innerException) : base(NetException.UnsupportedAudioFormat(fileName), innerException)
		{
			this.fileName = fileName;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00015C7C File Offset: 0x00013E7C
		protected UnsupportedAudioFormat(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00015CA6 File Offset: 0x00013EA6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00015CC1 File Offset: 0x00013EC1
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x040004F4 RID: 1268
		private readonly string fileName;
	}
}
