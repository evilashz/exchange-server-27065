using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001BC RID: 444
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WmaToWavConversionException : AudioConversionException
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00035A31 File Offset: 0x00033C31
		public WmaToWavConversionException(string wma, string wav) : base(Strings.WmaToWavConversion(wma, wav))
		{
			this.wma = wma;
			this.wav = wav;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00035A53 File Offset: 0x00033C53
		public WmaToWavConversionException(string wma, string wav, Exception innerException) : base(Strings.WmaToWavConversion(wma, wav), innerException)
		{
			this.wma = wma;
			this.wav = wav;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00035A78 File Offset: 0x00033C78
		protected WmaToWavConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.wma = (string)info.GetValue("wma", typeof(string));
			this.wav = (string)info.GetValue("wav", typeof(string));
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00035ACD File Offset: 0x00033CCD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("wma", this.wma);
			info.AddValue("wav", this.wav);
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00035AF9 File Offset: 0x00033CF9
		public string Wma
		{
			get
			{
				return this.wma;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00035B01 File Offset: 0x00033D01
		public string Wav
		{
			get
			{
				return this.wav;
			}
		}

		// Token: 0x04000796 RID: 1942
		private readonly string wma;

		// Token: 0x04000797 RID: 1943
		private readonly string wav;
	}
}
