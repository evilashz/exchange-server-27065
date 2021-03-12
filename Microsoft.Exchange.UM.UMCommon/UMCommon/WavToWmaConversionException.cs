using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001BB RID: 443
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WavToWmaConversionException : AudioConversionException
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x00035958 File Offset: 0x00033B58
		public WavToWmaConversionException(string wav, string wma) : base(Strings.WavToWmaConversion(wav, wma))
		{
			this.wav = wav;
			this.wma = wma;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003597A File Offset: 0x00033B7A
		public WavToWmaConversionException(string wav, string wma, Exception innerException) : base(Strings.WavToWmaConversion(wav, wma), innerException)
		{
			this.wav = wav;
			this.wma = wma;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000359A0 File Offset: 0x00033BA0
		protected WavToWmaConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.wav = (string)info.GetValue("wav", typeof(string));
			this.wma = (string)info.GetValue("wma", typeof(string));
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000359F5 File Offset: 0x00033BF5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("wav", this.wav);
			info.AddValue("wma", this.wma);
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00035A21 File Offset: 0x00033C21
		public string Wav
		{
			get
			{
				return this.wav;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00035A29 File Offset: 0x00033C29
		public string Wma
		{
			get
			{
				return this.wma;
			}
		}

		// Token: 0x04000794 RID: 1940
		private readonly string wav;

		// Token: 0x04000795 RID: 1941
		private readonly string wma;
	}
}
