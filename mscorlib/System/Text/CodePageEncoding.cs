using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A2F RID: 2607
	[Serializable]
	internal sealed class CodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x06006642 RID: 26178 RVA: 0x001586DC File Offset: 0x001568DC
		internal CodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			try
			{
				this.m_isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.m_deserializedFromEverett = true;
				this.m_isReadOnly = true;
			}
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x001587A0 File Offset: 0x001569A0
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			this.realEncoding = Encoding.GetEncoding(this.m_codePage);
			if (!this.m_deserializedFromEverett && !this.m_isReadOnly)
			{
				this.realEncoding = (Encoding)this.realEncoding.Clone();
				this.realEncoding.EncoderFallback = this.encoderFallback;
				this.realEncoding.DecoderFallback = this.decoderFallback;
			}
			return this.realEncoding;
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x0015880C File Offset: 0x00156A0C
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x04002D79 RID: 11641
		[NonSerialized]
		private int m_codePage;

		// Token: 0x04002D7A RID: 11642
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x04002D7B RID: 11643
		[NonSerialized]
		private bool m_deserializedFromEverett;

		// Token: 0x04002D7C RID: 11644
		[NonSerialized]
		private EncoderFallback encoderFallback;

		// Token: 0x04002D7D RID: 11645
		[NonSerialized]
		private DecoderFallback decoderFallback;

		// Token: 0x04002D7E RID: 11646
		[NonSerialized]
		private Encoding realEncoding;

		// Token: 0x02000C7A RID: 3194
		[Serializable]
		internal sealed class Decoder : ISerializable, IObjectReference
		{
			// Token: 0x0600702B RID: 28715 RVA: 0x00181357 File Offset: 0x0017F557
			internal Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
			}

			// Token: 0x0600702C RID: 28716 RVA: 0x0018138D File Offset: 0x0017F58D
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetDecoder();
			}

			// Token: 0x0600702D RID: 28717 RVA: 0x0018139A File Offset: 0x0017F59A
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x040037BD RID: 14269
			[NonSerialized]
			private Encoding realEncoding;
		}
	}
}
