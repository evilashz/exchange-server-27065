using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A52 RID: 2642
	[Serializable]
	internal sealed class SurrogateEncoder : ISerializable, IObjectReference
	{
		// Token: 0x060067BF RID: 26559 RVA: 0x0015F507 File Offset: 0x0015D707
		internal SurrogateEncoder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x0015F53D File Offset: 0x0015D73D
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			return this.realEncoding.GetEncoder();
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x0015F54A File Offset: 0x0015D74A
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x04002E52 RID: 11858
		[NonSerialized]
		private Encoding realEncoding;
	}
}
