using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A4C RID: 2636
	[Serializable]
	internal sealed class MLangCodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x06006797 RID: 26519 RVA: 0x0015D5D0 File Offset: 0x0015B7D0
		internal MLangCodePageEncoding(SerializationInfo info, StreamingContext context)
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

		// Token: 0x06006798 RID: 26520 RVA: 0x0015D694 File Offset: 0x0015B894
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

		// Token: 0x06006799 RID: 26521 RVA: 0x0015D700 File Offset: 0x0015B900
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x04002E1B RID: 11803
		[NonSerialized]
		private int m_codePage;

		// Token: 0x04002E1C RID: 11804
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x04002E1D RID: 11805
		[NonSerialized]
		private bool m_deserializedFromEverett;

		// Token: 0x04002E1E RID: 11806
		[NonSerialized]
		private EncoderFallback encoderFallback;

		// Token: 0x04002E1F RID: 11807
		[NonSerialized]
		private DecoderFallback decoderFallback;

		// Token: 0x04002E20 RID: 11808
		[NonSerialized]
		private Encoding realEncoding;

		// Token: 0x02000C81 RID: 3201
		[Serializable]
		internal sealed class MLangEncoder : ISerializable, IObjectReference
		{
			// Token: 0x06007062 RID: 28770 RVA: 0x00181D64 File Offset: 0x0017FF64
			internal MLangEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x06007063 RID: 28771 RVA: 0x00181D9A File Offset: 0x0017FF9A
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetEncoder();
			}

			// Token: 0x06007064 RID: 28772 RVA: 0x00181DA7 File Offset: 0x0017FFA7
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x040037E0 RID: 14304
			[NonSerialized]
			private Encoding realEncoding;
		}

		// Token: 0x02000C82 RID: 3202
		[Serializable]
		internal sealed class MLangDecoder : ISerializable, IObjectReference
		{
			// Token: 0x06007065 RID: 28773 RVA: 0x00181DB8 File Offset: 0x0017FFB8
			internal MLangDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x06007066 RID: 28774 RVA: 0x00181DEE File Offset: 0x0017FFEE
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetDecoder();
			}

			// Token: 0x06007067 RID: 28775 RVA: 0x00181DFB File Offset: 0x0017FFFB
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x040037E1 RID: 14305
			[NonSerialized]
			private Encoding realEncoding;
		}
	}
}
